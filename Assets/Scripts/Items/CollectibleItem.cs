using System;
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

        public bool TryAttachToBackpack(Transform backpackTransform)
        {
            if(!ItemInfo.CanBeAttachedToBackpack)
            {
                Destroy(gameObject);
                return false;
            }
            
            transform.SetParent(backpackTransform);

            rigidbody.isKinematic = true;
            
            collider.enabled = false;
            return true;
        }

        public void DetachFromBackpack()
        {
            var parent = transform.parent;
            transform.SetParent(null);

            var directionForce = (transform.position - parent.position).normalized;
            
            rigidbody.isKinematic = false;
            collider.enabled = true;
            
            rigidbody.AddForce(directionForce * 2.15f, ForceMode.Impulse);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if(!IsInteractable)
                return;
            
            if(!other.CompareTag("Backpack"))
                return;
            
            Release();

            IsInteractable = false;
            
            var backpack = other.GetComponent<Backpack>();
            backpack.AddToBackpack(this);
        }

        private void OnTriggerExit(Collider other)
        {
            IsInteractable = true;
        }
    }
}