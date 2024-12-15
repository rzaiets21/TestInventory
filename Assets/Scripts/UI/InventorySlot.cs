using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image itemImage;

        private string _itemId;
        private bool _selected;
        
        public event Action<string> onPointerUp; 
        
        public void Init(string id, Sprite image)
        {
            _itemId = id;
            itemImage.sprite = image;

            itemImage.enabled = true;
        }

        public void Clear()
        {
            _selected = false;
            itemImage.enabled = false;
            _itemId = string.Empty;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _selected = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!Input.GetMouseButton(0))
                onPointerUp?.Invoke(_itemId);
            _selected = false;
        }
    }
}