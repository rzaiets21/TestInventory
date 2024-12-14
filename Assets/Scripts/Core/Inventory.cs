using System.Collections.Generic;

namespace Core
{
    public class Inventory<T>
    {
        private List<T> _items = new();

        public void Add(T item)
        {
            _items.Add(item);
        }

        public void Remove(T item)
        {
            _items.Remove(item);
        }
    }
}