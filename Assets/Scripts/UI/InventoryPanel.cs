using System;
using Core;
using Extensions;
using Items;
using Items.Model;
using UnityEngine;

namespace UI
{
    public class InventoryPanel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private ItemsDatabase itemsDatabase;
        [SerializeField] private InventorySlot[] slots;

        private Inventory<ItemData> _inventory;

        public event Action<string> onPointerUpItemSlot;

        private void OnEnable()
        {
            foreach (var inventorySlot in slots)
            {
                inventorySlot.onPointerUp += OnPointerUpItemSlot;
            }
        }

        public void SetInventory(Inventory<ItemData> inventory)
        {
            _inventory = inventory;
            _inventory.onItemAdded += UpdateUI;
            _inventory.onItemRemoved += UpdateUI;
        }

        public void Show(bool state)
        {
            canvasGroup.SetActive(state);
        }
        
        private void OnDisable()
        {
            foreach (var inventorySlot in slots)
            {
                inventorySlot.onPointerUp -= OnPointerUpItemSlot;
            }
            
            if (_inventory == null)
                return;
            _inventory.onItemAdded += UpdateUI;
            _inventory.onItemRemoved += UpdateUI;
        }

        private void UpdateUI(ItemData t)
        {
            var items = _inventory.GetItems();
            foreach (var inventorySlot in slots)
            {
                inventorySlot.Clear();
            }

            var itemsCount = items.Length;
            for (int i = 0; i < itemsCount; i++)
            {
                var itemId = items[i].Id;
                var itemInfo = itemsDatabase.GetItemInfo(itemId);
                var inventorySlot = slots[i];

                inventorySlot.Init(itemId, itemInfo.Sprite);
            }
        }
        
        private void OnPointerUpItemSlot(string id)
        {
            onPointerUpItemSlot?.Invoke(id);
        }
    }
}