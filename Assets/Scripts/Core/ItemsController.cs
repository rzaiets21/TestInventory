using Items;
using Items.Model;
using Newtonsoft.Json;
using Server;
using UnityEngine;

namespace Core
{
    public sealed class ItemsController : MonoBehaviour
    {
        [SerializeField] private Backpack backpack;

        private ServerController _serverController;

        private void Awake()
        {
            _serverController = new ServerController();
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
            Debug.Log(request.ResponseType);
        }
        
        private void OnItemAdded(ItemData itemData)
        {
            var json = JsonConvert.SerializeObject(new ItemState("0", "Equipped"));
            SendRequestToServer(json);
        }
        
        private void OnItemRemoved(ItemData itemData)
        {
            var json = JsonConvert.SerializeObject(new ItemState("0", "Dropped"));
            SendRequestToServer(json);
        }
    }
}