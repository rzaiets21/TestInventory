using System;
using System.Collections.Generic;

namespace Core
{
    public class Inventory<T>
    {
        private List<T> _items = new();

        public event Action<T> onItemAdded; 
        public event Action<T> onItemRemoved; 

        public void Add(T item)
        {
            _items.Add(item);
            onItemAdded?.Invoke(item);
        }

        public void Remove(T item)
        {
            _items.Remove(item);
            onItemRemoved?.Invoke(item);
        }

        public T[] GetItems() => _items.ToArray();
    }
}