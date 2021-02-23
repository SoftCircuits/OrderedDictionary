// Copyright (c) 2020-2021 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SoftCircuits.Collections
{
    /// <summary>
    /// Implements a dictionary that also manages an ordered, indexable
    /// list of its items.
    /// </summary>
    public class OrderedDictionary<TKey, TValue> : IEnumerable<TValue>, IDictionary<TKey, TValue> where TKey : notnull
    {
        private readonly List<TValue> Items;
        private readonly Dictionary<TKey, int> IndexLookup;

        /// <summary>
        /// Gets or sets an item value using its 0-based, ordered index number.
        /// </summary>
        public ListIndexer<TValue> ByIndex { get; }

        /// <summary>
        /// Constructs a new <see cref="OrderedDictionary{TKey, TValue}"></see>
        /// instance.
        /// </summary>
        public OrderedDictionary()
        {
            Items = new List<TValue>();
            IndexLookup = new Dictionary<TKey, int>();
            ByIndex = new ListIndexer<TValue>(Items);
        }

        /// <summary>
        /// Constructs a new <see cref="OrderedDictionary{TKey, TValue}"></see>
        /// instance.
        /// </summary>
        /// <param name="comparer">Comparer to used when looking up items in
        /// the dictionary.</param>
        public OrderedDictionary(IEqualityComparer<TKey> comparer)
        {
            Items = new List<TValue>();
            IndexLookup = new Dictionary<TKey, int>(comparer);
            ByIndex = new ListIndexer<TValue>(Items);
        }

        /// <summary>
        /// Adds the given key/value pair to the collection.
        /// </summary>
        /// <param name="key">Item key.</param>
        /// <param name="value">Item value.</param>
        public void Add(TKey key, TValue value)
        {
            int index = Items.Count;
            Items.Add(value);
            IndexLookup.Add(key, index);
        }

        /// <summary>
        /// Adds the given <see cref="KeyValuePair{TKey, TValue}"></see> to the collection.
        /// </summary>
        /// <param name="item">The item to add to the collection.</param>
        public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);

        /// <summary>
        /// Adds a range of key/value pairs to the collection.
        /// </summary>
        /// <param name="collection">The items to add to the collection.</param>
        public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> collection)
        {
            foreach (var pair in collection)
                Add(pair.Key, pair.Value);
        }

        /// <summary>
        /// Adds all the items from the specified <see cref="OrderedDictionary{TKey, TValue}"></see>
        /// to the collection.
        /// </summary>
        /// <param name="collection">An <see cref="OrderedDictionary{TKey, TValue}"></see>
        /// with the items to add to the collection.</param>
        public void AddRange(OrderedDictionary<TKey, TValue> collection)
        {
            foreach (TKey key in collection.IndexLookup.Keys)
                Add(key, collection[key]);
        }

        /// <summary>
        /// Inserts an item at the specified index.
        /// </summary>
        /// <param name="index">The index where the item should be inserted.</param>
        /// <param name="key">The key of the item being inserted.</param>
        /// <param name="value">The value of the item being inserted.</param>
        public void Insert(int index, TKey key, TValue value)
        {
            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (index == Count)
                Add(key, value);
            else
            {
                Items.Insert(index, value);
                InsertIndexLookupItem(key, index);
            }
        }

        /// <summary>
        /// Gets the number of items contained in the collection.
        /// </summary>
        public int Count => Items.Count;

        /// <summary>
        /// Gets or sets the item with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get or set.</param>
        public TValue this[TKey key]
        {
            get => Items[IndexLookup[key]];
            set
            {
                if (IndexLookup.ContainsKey(key))
                    Items[IndexLookup[key]] = value;
                else
                    Add(key, value);
            }
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">Specifies the key of the item to return.</param>
        /// <param name="value">Returns the value with the specified key.</param>
        /// <returns>True if the item was found and returned, otherwise false.</returns>
#if NETSTANDARD2_0
        public bool TryGetValue(TKey key, out TValue value)
#else
        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
#endif
        {
            if (IndexLookup.TryGetValue(key, out int index))
            {
                value = Items[index];
                return true;
            }
            value = default;
            return false;
        }

        /// <summary>
        /// Returns true if the collection contains the specified item key.
        /// </summary>
        /// <param name="key">The key associated with the item to return.</param>
        /// <returns>True if the collection contains the key, false otherwise.</returns>
        public bool ContainsKey(TKey key) => IndexLookup.ContainsKey(key);

        /// <summary>
        /// Returns true if the collection contains the specified item. This method only
        /// checks for an item with the specified key.
        /// </summary>
        /// <param name="item">The item to find.</param>
        /// <returns>True if the collection contains the item, false otherwise.</returns>
        public bool Contains(KeyValuePair<TKey, TValue> item) => ContainsKey(item.Key);

        /// <summary>
        /// Returns the index of the item associated with the specified key. Returns -1 if
        /// the key was not found in the collection.
        /// </summary>
        /// <param name="key">The key to find.</param>
        /// <returns>Returns the index of the specified item key or -1 if the key
        /// was not found in the collection.</returns>
        public int IndexOf(TKey key) => IndexLookup.TryGetValue(key, out int index) ? index : -1;

        /// <summary>
        /// Removes the item with the specified key from the collection.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>True if the element is successfully found and removed, otherwise false.</returns>
        public bool Remove(TKey key)
        {
            if (IndexLookup.TryGetValue(key, out int index))
            {
                Items.RemoveAt(index);
                RemoveIndexLookupItem(index);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes the item with the specified key from the collection. This method simply removes
        /// the item with the specified key.
        /// </summary>
        /// <param name="key">The item to remove.</param>
        /// <returns>True if the item is successfully found and removed, otherwise false.</returns>
        public bool Remove(KeyValuePair<TKey, TValue> item) => Remove(item.Key);

        /// <summary>
        /// Removes the item at the specified index.
        /// </summary>
        /// <param name="index">The index position of the item to remove.</param>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Items.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            Items.RemoveAt(index);
            RemoveIndexLookupItem(index);
        }

        /// <summary>
        /// Removes all items from the collection.
        /// </summary>
        public void Clear()
        {
            Items.Clear();
            IndexLookup.Clear();
        }

        /// <summary>
        /// Copies elements from the collection to an existing one-dimensional array, starting at
        /// the specified array index.
        /// </summary>
        /// <param name="array">The one-dimensional Array that is the destination of the elements
        /// copied. The Array must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in array at which copying begins.</param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (index < 0 || index > array.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (index + Count > array.Length)
                throw new ArgumentException("Target array is not large enough to copy items.");

            foreach (KeyValuePair<TKey, int> pair in IndexLookup)
                array[index++] = new KeyValuePair<TKey, TValue>(pair.Key, Items[pair.Value]);
        }

        /// <summary>
        /// Returns all the keys in the collection. Returns a copy of the internal
        /// list.
        /// </summary>
        public ICollection<TKey> Keys => new List<TKey>(IndexLookup.Keys);

        /// <summary>
        /// Returns all the values in the collection. Returns a copy of the internal
        /// list.
        /// </summary>
        public ICollection<TValue> Values => new List<TValue>(Items);

        /// <summary>
        /// Always returns false.
        /// </summary>
        public bool IsReadOnly => false;

#region IEnumerable

        public IEnumerator<TValue> GetEnumerator() => Items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Items.GetEnumerator();

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

#endregion

        /// <summary>
        /// Removes the item with the given index from <see cref="IndexLookup"></see>.
        /// Adjusts the index of other items to account for the removed item.
        /// </summary>
        /// <param name="index"></param>
        private void RemoveIndexLookupItem(int index)
        {
            // Must copy all data to allow 
            foreach (var pair in IndexLookup.ToList())
            {
                if (pair.Value > index)
                    IndexLookup[pair.Key]--;
                else if (pair.Value == index)
                    IndexLookup.Remove(pair.Key);
            }
        }

        /// <summary>
        /// Inserts the given key with the specified index into <see cref="IndexLookup"></see>.
        /// Adjusts the index of other items to account for the inserted item.
        /// </summary>
        /// <param name="index"></param>
        private void InsertIndexLookupItem(TKey key, int index)
        {
            // Must copy all data to allow 
            foreach (var pair in IndexLookup.ToList())
            {
                if (pair.Value >= index)
                    IndexLookup[pair.Key]++;
            }
            IndexLookup.Add(key, index);
        }
    }
}
