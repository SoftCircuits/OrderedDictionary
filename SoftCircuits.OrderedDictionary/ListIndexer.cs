// Copyright (c) 2020-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System.Collections.Generic;

namespace SoftCircuits.Collections
{
    /// <summary>
    /// Wraps a <see cref="List{T}"></see> object and exposes only its
    /// indexer property.
    /// </summary>
    public class ListIndexer<TKey, TValue>
    {
        private readonly List<KeyValuePair<TKey, TValue>> Items;

        internal ListIndexer(List<KeyValuePair<TKey, TValue>> items)
        {
            Items = items;
        }

        /// <summary>
        /// Gets or sets the value at the specified index.
        /// </summary>
        public TValue this[int index]
        {
            get => Items[index].Value;
            set
            {
                KeyValuePair<TKey, TValue> pair = Items[index];
                Items[index] = new(pair.Key, value);
            }
        }
    }
}
