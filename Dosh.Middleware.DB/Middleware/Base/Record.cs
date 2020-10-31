using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Dosh.Middleware.DB.Middleware.Base
{
    /// <summary>
    /// database record representation class
    /// </summary>
    public class Record : IList<string>
    {
        /// <summary>
        /// innner array items.
        /// </summary>
        private string[] items;

        /// <summary>
        /// inner array size.
        /// </summary>
        [ContractPublicPropertyName("Count")]
        private int size;

        /// <summary>
        /// versions.
        /// </summary>
        private int version;

        /// <summary>
        /// default empty array.
        /// </summary>
        private static readonly string[] emptyArray = new string[0];

        /// <summary>
        /// default array capacity.
        /// </summary>
        private const int DEFAULT_CAPACITY = 4;

        /// <summary>
        /// max array length.
        /// </summary>
        private const int MAX_ARRAY_LENGTH = 0X7FEFFFFF;

        /// <summary>
        /// Constructor
        /// </summary>
        public Record()
        {
            items = emptyArray;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="capacity">item capacity</param>
        public Record(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), capacity.ToString());
            }

            if (capacity == 0)
            {
                items = emptyArray;
            }
            else
            {
                items = new string[capacity];
            }
        }

        /// <summary>
        /// Indexer
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>value</returns>
        public string this[int index]
        {
            get
            {
                if ((uint)index >= (uint)size)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index.ToString());
                }
                Contract.EndContractBlock();
                return items[index];
            }

            set
            {
                if ((uint)index >= (uint)size)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index.ToString());
                }
                Contract.EndContractBlock();
                items[index] = value;
                version++;
            }
        }

        /// <summary>
        /// items count.
        /// </summary>
        public int Count
        {
            get
            { 
                Contract.Ensures(Contract.Result<int>() >= 0);
                return size;
            }
        }

        /// <summary>
        /// Is this List read-only check.
        /// </summary>
        public bool IsReadOnly { get; } = false;

        /// <summary>
        /// Return the capacity of the internal array.
        /// </summary>
        public int Capacity
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return items.Length;
            }

            set
            {
                if (value < size)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value.ToString());
                }
                Contract.EndContractBlock();

                if (value != items.Length)
                {
                    if (value > 0)
                    {
                        string[] newItems = new string[value];
                        if (size > 0)
                        {
                            Array.Copy(items, 0, newItems, 0, size);
                        }
                        items = newItems;
                    }
                    else
                    {
                        items = emptyArray;
                    }
                }
            }
        }

        /// <summary>
        /// Add one element.
        /// </summary>
        /// <param name="item">element.</param>
        public void Add(string item)
        {
            if (size == items.Length)
            {
                ensureCapacity(size + 1);
            }

            items[size++] = item;
            version++;
        }

        /// <summary>
        /// Discard the allocated array.
        /// </summary>
        public void Clear()
        {
            if (size > 0)
            {
                Array.Clear(items, 0, size);
                size = 0;
            }

            version++;
        }

        /// <summary>
        /// Checks for the inclusion of the specified element.
        /// </summary>
        /// <param name="item">specified element.</param>
        /// <returns>true : exists / false not exists.</returns>
        public bool Contains(string item)
        {
            if (item == null)
            {
                for(int i = 0; i < size; i++)
                {
                    if (items[i] == null)
                        return true;
                }
                return false;
            }
            else
            {
                var compare = EqualityComparer<string>.Default;
                for(int i = 0; i < size; i++)
                {
                    if (compare.Equals(items[i], item))
                        return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Copies a section of this list to the given array at the given index.
        /// </summary>
        /// <param name="array">given array</param>
        /// <param name="arrayIndex">index</param>
        public void CopyTo(string[] array, int arrayIndex)
        {
            Array.Copy(items, 0, array, arrayIndex, size);
        }

        /// <summary>
        /// Return the Enumerator of the record class.
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<string> GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <summary>
        /// Return the index of the specified element.
        /// </summary>
        /// <param name="item">specified element.</param>
        /// <returns>index</returns>
        public int IndexOf(string item)
        {
            Contract.Ensures(Contract.Result<int>() >= -1);
            Contract.Ensures(Contract.Result<int>() < Count);
            return Array.IndexOf(items, item, 0, size);
        }

        /// <summary>
        /// Insert an element into the specified index.
        /// </summary>
        /// <param name="index">insert index</param>
        /// <param name="item">element.</param>
        public void Insert(int index, string item)
        {
            if ((uint)index > (uint)size)
                throw new ArgumentOutOfRangeException(nameof(index));
            
            Contract.EndContractBlock();

            if (size == items.Length) ensureCapacity(size + 1);
            if (index < size)
            {
                Array.Copy(items, index, items, index + 1, size - index);
            }
            items[index] = item;
            size++;
            version++;
        }

        /// <summary>
        /// Removes the specified element.
        /// </summary>
        /// <param name="item">element</param>
        /// <returns>success true.</returns>
        public bool Remove(string item)
        {
            var index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Remove an element at the specified index.
        /// </summary>
        /// <param name="index">remove index.</param>
        public void RemoveAt(int index)
        {
            if ((uint)index >= (uint)size)
                throw new ArgumentOutOfRangeException(nameof(index));

            Contract.EndContractBlock();
            size--;
            if (index < size)
            {
                Array.Copy(items, index + 1, items, index, size - index);
            }

            items[size] = default;
            version++;
        }

        /// <summary>
        /// Return the Enumerator of the record class.
        /// </summary>
        /// <returns>Enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <summary>
        /// Ensure internal array capacity
        /// </summary>
        /// <param name="min">minimam capacity.</param>
        private void ensureCapacity(int min)
        {
            if (items.Length < min)
            {
                int newCapacity = items.Length == 0 ? DEFAULT_CAPACITY : items.Length * 2;
                if ((uint)newCapacity > MAX_ARRAY_LENGTH) newCapacity = MAX_ARRAY_LENGTH;
                if (newCapacity < min) newCapacity = min;
                Capacity = newCapacity;
            }
        }

        /// <summary>
        /// Record class enumerator.
        /// </summary>
        [Serializable]
        public struct Enumerator : IEnumerator<string>, IEnumerator
        {
            /// <summary>
            /// record instance.
            /// </summary>
            private Record record;

            /// <summary>
            /// current index.
            /// </summary>
            private int index;

            /// <summary>
            /// Version when creating an instance
            /// </summary>
            private int version;
            private string current;

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="record">record instance.</param>
            internal Enumerator(Record record)
            {
                this.record = record;
                index = 0;
                version = record.version;
                current = default;
            }

            /// <summary>
            /// disposing.
            /// </summary>
            public void Dispose() 
            { }

            /// <summary>
            /// Go to the next element.
            /// </summary>
            /// <returns>If it is possible to move to the next element, returns True.</returns>
            public bool MoveNext()
            {
                var localRecord = record;

                if (version == localRecord.version && ((uint)index < (uint)localRecord.size))
                {
                    current = localRecord.items[index];
                    index++;
                    return true;
                }
                return moveNextRare();
            }

            /// <summary>
            /// Makes the internal array access index out of range, making the element inaccessible.
            /// </summary>
            /// <returns></returns>
            private bool moveNextRare()
            {
                if (version != record.version)
                    throw new InvalidOperationException("The version of the internal array is different.");

                index = record.size + 1;
                current = default;
                return false;
            }

            /// <summary>
            /// Reset the access index.
            /// </summary>
            public void Reset()
            {
                if (version != record.version)
                    throw new InvalidOperationException("The version of the internal array is different.");

                index = 0;
                current = default;
            }

            /// <summary>
            /// Returns the current element.
            /// </summary>
            public string Current
            {
                get { return current; }
            }

            /// <summary>
            /// Returns the current element.
            /// </summary>
            object IEnumerator.Current
            {
                get
                {
                    if (index == 0 || index == record.size + 1)
                    {
                        throw new InvalidOperationException("The index is attempting to access an element beyond the specified range.");
                    }
                    return Current;
                }
            }
        }
    }
}
