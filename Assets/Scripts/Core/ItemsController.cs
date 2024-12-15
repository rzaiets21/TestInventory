using System;
using Items;
using Items.Base;
using Items.Model;
using Newtonsoft.Json;
using Server;
using UI;
using UnityEngine;

namespace Core
{
    public sealed class ItemsController : MonoBehaviour
    {
        [SerializeField] private ItemsDatabase itemsDatabase;
        [SerializeField] private InventoryPanel inventoryPanel;
        
        [SerializeField] private Backpack backpack;
        [SerializeField] private CollectibleItem[] collectibleItems;

        private ServerController _serverController;

        private void Awake()
        {
            _serverController = new ServerController();

            var inventory = new Inventory<ItemData>();
            backpack.SetInventory(inventory);
            inventoryPanel.SetInventory(inventory);
        }

        private void Start()
        {
            InitCollectibleItems();
        }

        private void InitCollectibleItems()
        {
            foreach (var collectibleItem in collectibleItems)
            {
                var itemData = itemsDatabase.GetItemData(collectibleItem.ItemId);
                var itemInfo = itemsDatabase.GetItemInfo(collectibleItem.ItemId);
                collectibleItem.SetItemData(itemData)
                                .SetItemInfo(itemInfo);
            }
        }

        private void OnEnable()
        {
            backpack.onItemAdded.AddListener(OnItemAdded);
            backpack.onItemRemoved.AddListener(OnItemRemoved);

            backpack.onHoldBegin += OnBackpackHolding;
            backpack.onHoldEnd += OnBackpackHoldingEnd;

            inventoryPanel.onPointerUpItemSlot += OnInventorySlotPointerUp;
        }

        private void OnDisable()
        {
            backpack.onItemAdded.RemoveListener(OnItemAdded);
            backpack.onItemRemoved.RemoveListener(OnItemRemoved);
            
            backpack.onHoldBegin -= OnBackpackHolding;
            backpack.onHoldEnd -= OnBackpackHoldingEnd;
            
            inventoryPanel.onPointerUpItemSlot -= OnInventorySlotPointerUp;
        }

        private void ShowUIPanel(bool state)
        {
            inventoryPanel.Show(state);
        }
        
        private async void SendRequestToServer(string data)
        {
            var request = await _serverController.SendRequest<ServerResponse>(data);
            Debug.Log($"{request.Data}:{request.ResponseType}");
        }

        private void OnInventorySlotPointerUp(string id)
        {
            if(!backpack.TryRemoveFromBackpack(id, out var item))
                return;
            
            if(item != null)
                return;
            
            //Spawn new object in case when item is not attached to the backpack
        }

        private void OnBackpackHolding(ItemBase item)
        {
            ShowUIPanel(true);
        }
        
        private void OnBackpackHoldingEnd(ItemBase item)
        {
            ShowUIPanel(false);
        }
        
        private void OnItemAdded(ItemData itemData)
        {
            var json = JsonConvert.SerializeObject(new ItemState(itemData.Id, "Equipped"));
            SendRequestToServer(json);
        }
        
        private void OnItemRemoved(ItemData itemData)
        {
            var json = JsonConvert.SerializeObject(new ItemState(itemData.Id, "Dropped"));
            SendRequestToServer(json);
        }
    }
}