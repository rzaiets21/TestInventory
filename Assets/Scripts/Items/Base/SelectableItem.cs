using System;

namespace Items.Base
{
    public abstract class SelectableItem : ItemBase
    {
        protected bool _isSelected;

        public event Action<ItemBase> onSelect;
        public event Action<ItemBase> onRelease;

        private void OnMouseDown()
        {
            Select();
        }

        private void OnMouseUp()
        {
            Release();
        }

        protected void Select()
        {
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