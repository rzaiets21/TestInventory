using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Items.Model
{
    [Serializable]
    public sealed class ItemData
    {
        [JsonProperty("identifier")] [field: SerializeField] public string Id { get; private set; }
        [JsonProperty("name")] [field: SerializeField] public string Name { get; private set; }
        [JsonProperty("type")] [field: SerializeField] public ItemType Type { get; private set; }
        [JsonProperty("weight")] [field: SerializeField] public float Weight { get; private set; }
    }
}