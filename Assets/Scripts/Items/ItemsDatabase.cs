using System.Collections.Generic;
using System.Linq;
using Items.Model;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/Items Database", fileName = "NewItemsDatabase")]
    public sealed class ItemsDatabase : ScriptableObject
    {
        [SerializeField] private List<ItemInfo> itemsInfo = new List<ItemInfo>();
        [SerializeField] private List<ItemData> items = new List<ItemData>();

        public ItemData GetItemData(string id)
        {
            return items.FirstOrDefault(x => x.Id == id);
        }

        public ItemInfo GetItemInfo(string id)
        {
            return itemsInfo.FirstOrDefault(x => x.Id == id);
        }
    }
}