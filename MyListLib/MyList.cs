using System;
using System.Collections;
using System.Collections.Generic;

namespace MyListLib {
    [Serializable]
    public class MyList<T> : IList<T> {
        private static readonly T[] empty = new T[0];

        private T[] items;
        private bool changed = false;

        public MyList()
            => items = empty;

        public MyList(int capacity) {
            if (capacity < 0) {
                throw new ArgumentOutOfRangeException("List capacity was less than zero");
            }

            if (capacity == 0) {
                items = empty;
            }
            else {
                items = new T[capacity];
            }
        }

        public MyList(IEnumerable<T> enumerable) {
            if (enumerable == null) {
                throw new ArgumentNullException("Enumerable was null");
            }

            if (enumerable is ICollection<T> collection) {
                int count = collection.Count;

                if (count == 0) {
                    items = empty;
                }
                else {
                    items = new T[count];
                    collection.CopyTo(items, 0);
                    Count = count;
                }
            }
            else {
                Count = 0;
                items = empty;

                foreach (var item in enumerable) {
                    Add(item);
                }
            }
        }

        public int Count { get; private set; }

        public int Capacity {
            get => items.Length;

            private set {
                if (value == items.Length) {
                    return;
                }

                if (value < Count) {
                    throw new IndexOutOfRangeException("New list capacity was smaller than item count");
                }

                if (value > 0) {
                    T[] itemsNew = new T[value];
                    Array.Copy(items, itemsNew, Count);
                    items = itemsNew;
                }
                else {
                    items = empty;
                }
            }
        }

        public bool IsReadOnly => false;

        public T this[int index] {
            get => GetItem(index);

            set {
                var item = GetItem(index);
                item = value;
                changed = true;
            }
        }

        public void Add(T item) {
            if (Count == items.Length) {
                IncreaseCapacity(Count + 1);
            }

            items[Count++] = item;

            changed = true;
        }

        public void Clear() {
            if (Count > 0) {
                Array.Clear(items, 0, Count);
                Count = 0;
                changed = true;
            }
        }

        public bool Contains(T item) {
            var comparer = EqualityComparer<T>.Default;

            for (int i = 0; i < Count; ++i) {
                if (comparer.Equals(item, items[i])) {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
            => Array.Copy(items, 0, array, arrayIndex, Count);

        public int IndexOf(T item) {
            var comparer = EqualityComparer<T>.Default;
            return Array.IndexOf(items, item, 0, Count);
        }

        public void Insert(int index, T item) {
            if (index > Count) {
                throw new IndexOutOfRangeException("Index was outside of list range");
            }

            if (Count == items.Length) {
                IncreaseCapacity(Count + 1);
            }

            if (index < Count) {
                Array.Copy(items, index, items, index + 1, Count - index);
            }

            items[index] = item;
            ++Count;

            changed = true;
        }

        public bool Remove(T item) {
            int index = IndexOf(item);

            if (index >= 0) {
                RemoveAt(index);
                return true;
            }

            return false;
        }

        public void RemoveAt(int index) {
            --Count;
            Array.Copy(items, index + 1, items, index, Count - index);
            items[Count] = default(T);
            changed = true;
        }

        public IEnumerator<T> GetEnumerator() {
            changed = false;

            foreach (var item in items) {
                if (changed == true) {
                    throw new InvalidOperationException("MyList was changed during iteration");
                }

                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        private T GetItem(int index) {
            // Supports Python-like access (for example list[-1] returns last element
            if (index < 0 && Count + index >= 0) {
                return items[Count + index];
            }
            else if (index < Count) {
                return items[index];
            }

            throw new IndexOutOfRangeException("Index was outside of list range");
        }

        private void IncreaseCapacity(int need) {
            if (Capacity >= need) {
                return;
            }

            const int   defaultCapacity = 4;
            const float factor = 1.5f; // Factor of 2 is more common, but people proved that 1.5 is a better choice

            int capacityNew = Capacity == 0 ? defaultCapacity : (int)(Capacity * factor);

            if (capacityNew < need) {
                capacityNew = need;
            }

            Capacity = capacityNew;
        }
    }
}
