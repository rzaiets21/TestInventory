using System;
using Items.Base;
using Items.Model;
using UnityEngine;

namespace Items
{
    public class CollectibleItem : DraggableItem
    {
        public ItemData ItemData { get; private set; }
        
        private void OnTriggerEnter(Collider other)
        {
            if(!other.CompareTag("Backpack"))
                return;
            
            Destroy(gameObject);
        }
    }
}