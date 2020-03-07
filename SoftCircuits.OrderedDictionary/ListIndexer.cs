// Copyright (c) 2020 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//
using System.Collections.Generic;

namespace SoftCircuits.Collections
{
    /// <summary>
    /// Wraps a <see cref="List{T}"></see> object and exposes only its
    /// indexer property.
    /// </summary>
    public class ListIndexer<T>
    {
        private readonly List<T> Items;

        internal ListIndexer(List<T> items)
        {
            Items = items;
        }

        /// <summary>
        /// Gets or sets the value at the specified index.
        /// </summary>
        public T this[int index]
        {
            get => Items[index];
            set => Items[index] = value;
        }
    }
}
