using System;
using Items;
using Items.Model;
using Newtonsoft.Json;
using Server;
using UnityEngine;

namespace Core
{
    public sealed class ItemsController : MonoBehaviour
    {
        [SerializeField] private ItemsDatabase itemsDatabase;
        
        [SerializeField] private Backpack backpack;
        [SerializeField] private CollectibleItem[] collectibleItems;

        private ServerController _serverController;

        private void Awake()
        {
            _serverController = new ServerController();
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
        }

        private void OnDisable()
        {
            backpack.onItemAdded.RemoveListener(OnItemAdded);
            backpack.onItemRemoved.RemoveListener(OnItemRemoved);
        }

        private async void SendRequestToServer(string data)
        {
            var request = await _serverController.SendRequest<ServerResponse>(data);
            Debug.Log($"{request.Data}:{request.ResponseType}");
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