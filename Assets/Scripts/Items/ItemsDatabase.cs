using System.Collections.Generic;
using System.Linq;
using Items.Model;
using UnityEngine;

namespace Items
{
    public sealed class ItemsDatabase : ScriptableObject
    {
        [SerializeField] private List<ItemData> items = new List<ItemData>();

        public ItemData GetItemData(string id)
        {
            return items.FirstOrDefault(x => x.Id == id);
        }
    }
}