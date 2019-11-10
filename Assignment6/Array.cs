using System;
using System.Collections;
using System.Collections.Generic;

namespace Assignment6
{
    public class ArrayCollection<T> : ICollection<T>
    {
        public int Capacity { get; }
        private int CurrentCount { get; set; }

        private List<T> _Items;

        public ArrayCollection(int width)
        {
            if(width <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "Must be greater than 0");
            }

            Capacity = width;
            _Items = new List<T>(width);
        }

        public int Count => CurrentCount;

        public bool IsReadOnly => false;

        // For the add method, I'm leaving it up to the user to correctly check for
        // If the collection is maxed before adding into it as I don't see it being more useful
        // To throw an exception over leaving that up to the user to decide that functionality
        public void Add(T item)
        {
            if (Count == Capacity)
            {
                throw new ArgumentOutOfRangeException(nameof(_Items), "Collection is full!");
            }
            else if(item is null)
            {
                throw new ArgumentNullException(nameof(item), "Item is null");
            }
            _Items.Add(item);
            CurrentCount += 1;
        }

        public void Clear()
        {
            _Items.Clear();
        }

        public bool Contains(T item)
        {
            if (_Items.Contains(item))
            {
                return true;
            }
            
            throw new ArgumentException(nameof(item), "Item not in list!");
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if(array is null)
            {
                throw new ArgumentNullException(nameof(array), "array null!");
            }

            if((arrayIndex + Count-1) > array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(array), "Not enough space!");
            }

            foreach(T item in _Items)
            {
                array[arrayIndex] = item;
                arrayIndex++;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _Items.GetEnumerator();
        }

        public bool Remove(T item)
        {
            return _Items.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _Items.GetEnumerator();
        }

        public T this[int index]
        {
            get 
            {
                T[] tempAra = _Items.ToArray();
                return tempAra[index]; 
            }
            set 
            {
                T[] tempAra = new T[Capacity];
                _Items.CopyTo(tempAra, 0);
                tempAra[index] = value;
                _Items = new List<T>(tempAra);
                CurrentCount++;   
            }
        }

        public override string ToString()
        {
            string returnString = "";
            for(int i=0; i<Count; i++)
            {
                returnString += _Items[i].ToString() + " ";
            }
            return returnString;
        }
    }
}
