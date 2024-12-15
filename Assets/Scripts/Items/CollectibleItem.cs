using Items.Base;
using Items.Model;
using UnityEngine;

namespace Items
{
    public class CollectibleItem : DraggableItem
    {
        [field: SerializeField] public string ItemId { get; private set; }
        public ItemData ItemData { get; private set; }
        public ItemInfo ItemInfo { get; private set; }

        public CollectibleItem SetItemData(ItemData itemData)
        {
            ItemData = itemData;
            return this;
        }

        public CollectibleItem SetItemInfo(ItemInfo itemInfo)
        {
            ItemInfo = itemInfo;
            return this;
        }

        public void AttachToBackpack(Transform backpackTransform)
        {
            transform.SetParent(backpackTransform);
            transform.localPosition = ItemInfo.PositionOnBackpack;
            transform.localRotation = Quaternion.Euler(ItemInfo.RotationOnBackpack);

            rigidbody.isKinematic = true;

            IsInteractable = false;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if(!IsInteractable)
                return;
            
            if(!other.CompareTag("Backpack"))
                return;

            Release();
            
            var backpack = other.GetComponent<Backpack>();
            backpack.AddToBackpack(this);
            
            if(!ItemInfo.CanBeAttachedToBackpack)
            {
                Destroy(gameObject);
                return;
            }

            AttachToBackpack(backpack.transform);
        }
    }
}