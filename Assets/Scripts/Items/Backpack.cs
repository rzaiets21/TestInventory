using System.Collections.Generic;
using System.Linq;
using Core;
using Items.Base;
using Items.Model;
using UnityEngine.Events;

namespace Items
{
    public class BackpackEvent : UnityEvent<ItemData>{}
    
    public class Backpack : HoldableItem
    {
        private Inventory<ItemData> _inventory;
        private List<CollectibleItem> _attachedItems = new();

        public BackpackEvent onItemAdded = new BackpackEvent();
        public BackpackEvent onItemRemoved = new BackpackEvent();

        public void SetInventory(Inventory<ItemData> inventory)
        {
            _inventory = inventory;
        }
        
        public void AddToBackpack(CollectibleItem item)
        {
            var itemData = item.ItemData;
            _inventory.Add(itemData);
            onItemAdded?.Invoke(itemData);

            TryAttachItem(item);
        }

        private void TryAttachItem(CollectibleItem item)
        {
            if(item.TryAttachToBackpack(transform))
                _attachedItems.Add(item);
        }

        public bool TryRemoveFromBackpack(string itemId, out CollectibleItem item)
        {
            item = _attachedItems.FirstOrDefault(x => x.ItemId == itemId);

            if (item != null)
            {
                _attachedItems.Remove(item);
                item.DetachFromBackpack();
            }
            
            var itemData = _inventory.GetItems().FirstOrDefault(x => x.Id == itemId);
            if (itemData == null)
                return false;
            
            RemoveFromBackpack(itemData);
            return true;
        }
        
        private void RemoveFromBackpack(ItemData itemData)
        {
            _inventory.Remove(itemData);
            onItemRemoved?.Invoke(itemData);
        }
    }
}