// Copyright (c) 2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System;
using System.Collections.Generic;

namespace SoftCircuits.Collections
{
    public static class OrderedDictionaryExtensions
    {
        /// <summary>
        /// Creates a <see cref="Dictionary{TKey, TValue}"></see> from an <see cref="IEnumerable{T}"></see>
        /// according to a specified key selector function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
        /// <param name="source">An IEnumerable<T> to create a Dictionary<TKey,TValue> from.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <returns>The newly created <see cref="Dictionary{TKey, TValue}"></see>.</returns>
        public static OrderedDictionary<TKey, TSource> ToOrderedDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            OrderedDictionary<TKey, TSource> dictionary = new OrderedDictionary<TKey, TSource>();
            foreach (TSource item in source)
                dictionary.Add(keySelector(item), item);
            return dictionary;
        }

        /// <summary>
        /// Creates a <see cref="Dictionary{TKey, TValue}"></see> from an <see cref="IEnumerable{T}"></see>
        /// according to a specified key selector function , and a comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
        /// <param name="source">An IEnumerable<T> to create a Dictionary<TKey,TValue> from.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <returns>The newly created <see cref="Dictionary{TKey, TValue}"></see>.</returns>
        public static OrderedDictionary<TKey, TSource> ToOrderedDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            OrderedDictionary<TKey, TSource> dictionary = new OrderedDictionary<TKey, TSource>(comparer);
            foreach (TSource item in source)
                dictionary.Add(keySelector(item), item);
            return dictionary;
        }

        /// <summary>
        /// Creates a <see cref="Dictionary{TKey, TValue}"></see> from an <see cref="IEnumerable{T}"></see>
        /// according to a specified key selector function, and an element selector function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
        /// <typeparam name="TElement">The type of the value returned by <paramref name="elementSelector"/>.</typeparam>
        /// <param name="source">An IEnumerable<T> to create a Dictionary<TKey,TValue> from.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="elementSelector">A transform function to produce a result element value from each element.</param>
        /// <returns>The newly created <see cref="Dictionary{TKey, TValue}"></see>.</returns>
        public static OrderedDictionary<TKey, TElement> ToOrderedDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));
            if (elementSelector == null)
                throw new ArgumentNullException(nameof(elementSelector));

            OrderedDictionary<TKey, TElement> dictionary = new OrderedDictionary<TKey, TElement>();
            foreach (TSource item in source)
                dictionary.Add(keySelector(item), elementSelector(item));
            return dictionary;
        }

        /// <summary>
        /// Creates a <see cref="Dictionary{TKey, TValue}"></see> from an <see cref="IEnumerable{T}"></see>
        /// according to a specified key selector function, an element selector function, and a comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
        /// <typeparam name="TElement">The type of the value returned by <paramref name="elementSelector"/>.</typeparam>
        /// <param name="source">An IEnumerable<T> to create a Dictionary<TKey,TValue> from.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="elementSelector">A transform function to produce a result element value from each element.</param>
        /// <returns>The newly created <see cref="Dictionary{TKey, TValue}"></see>.</returns>
        public static OrderedDictionary<TKey, TElement> ToOrderedDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));
            if (elementSelector == null)
                throw new ArgumentNullException(nameof(elementSelector));
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            OrderedDictionary<TKey, TElement> dictionary = new OrderedDictionary<TKey, TElement>(comparer);
            foreach (TSource item in source)
                dictionary.Add(keySelector(item), elementSelector(item));
            return dictionary;
        }
    }
}
