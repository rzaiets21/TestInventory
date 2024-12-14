using System;
using Newtonsoft.Json;

namespace Items.Model
{
    [Serializable]
    public sealed class ItemData
    {
        [JsonProperty("identifier")] public string Id { get; private set; }
        [JsonProperty("name")] public string Name { get; private set; }
        [JsonProperty("type")] public ItemType Type { get; private set; }
        [JsonProperty("weight")] public float Weight { get; private set; }
    }
}