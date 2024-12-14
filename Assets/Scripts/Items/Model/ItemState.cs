using Newtonsoft.Json;

namespace Items.Model
{
    public class ItemState
    {
        [JsonProperty("id")] public string Id { get; private set; }
        [JsonProperty("status")] public string Status { get; private set; }

        public ItemState(string id, string status)
        {
            Id = id;
            Status = status;
        }
    }
}