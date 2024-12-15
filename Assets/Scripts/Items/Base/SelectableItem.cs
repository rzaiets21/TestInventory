using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Items.Base
{
    public abstract class SelectableItem : ItemBase
    {
        protected bool _isSelected;

        public bool IsInteractable { get; protected set; } = true;
        
        public event Action<ItemBase> onSelect;
        public event Action<ItemBase> onRelease;

        private void OnMouseDown()
        {
            if(!IsInteractable)
                return;
            
            Select();
        }

        private void OnMouseUp()
        {
            if(!IsInteractable)
                return;
            
            Release();
        }

        protected void Select()
        {
            if(EventSystem.current.IsPointerOverGameObject())
                return;
            
            if(_isSelected)
                return;

            _isSelected = true;
            
            OnSelect();
            onSelect?.Invoke(this);
        }

        protected virtual void OnSelect() { }

        protected void Release()
        {
            if(!_isSelected)
                return;

            _isSelected = false;
            
            OnRelease();
            onRelease?.Invoke(this);
        }
        
        protected virtual void OnRelease() { }
    }
}