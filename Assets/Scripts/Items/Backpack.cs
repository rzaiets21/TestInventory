using Core;
using Items.Base;
using Items.Model;
using UnityEngine.Events;

namespace Items
{
    public class BackpackEvent : UnityEvent<ItemData>{}
    
    public class Backpack : HoldableItem
    {
        private Inventory<ItemData> _inventory = new ();

        public BackpackEvent onItemAdded;
        public BackpackEvent onItemRemoved;
        
        public void AddToBackpack(CollectibleItem item)
        {
            var itemData = item.ItemData;
            onItemAdded?.Invoke(itemData);
        }

        public void RemoveFromBackpack(ItemData itemData)
        {
            onItemRemoved?.Invoke(itemData);
        }
    }
}