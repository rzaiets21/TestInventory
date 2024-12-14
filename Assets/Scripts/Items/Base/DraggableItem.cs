using System;
using UnityEngine;

namespace Items.Base
{
    public abstract class DraggableItem : HoldableItem
    {
        private bool _isDragging;

        private Vector3 _selectedPosition;
        
        public event Action<ItemBase> onBeginDrag;
        public event Action<ItemBase> onDrag;
        public event Action<ItemBase> onEndDrag;

        private Camera _mainCamera;
        private Vector3 _mousePosition;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        protected override void OnSelect()
        {
            base.OnSelect();
            _selectedPosition = transform.position;
            _mousePosition = Input.mousePosition - _mainCamera.WorldToScreenPoint(_selectedPosition);
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
        }

        private void OnMouseDrag()
        {
            if (!_isSelected)
                return;
            
            if (!_isDragging)
            {
                _isDragging = true;
                OnBeginDrag();
                return;
            }

            transform.position = _mainCamera.ScreenToWorldPoint(Input.mousePosition - _mousePosition);
            OnDrag();
        }

        protected virtual void OnBeginDrag()
        {
            onBeginDrag?.Invoke(this);
        }

        protected virtual void OnDrag()
        {
            onDrag?.Invoke(this);
        }

        protected override void OnRelease()
        {
            base.OnRelease();
            
            if (_isDragging)
                OnEndDrag();
        }

        protected virtual void OnEndDrag()
        {
            onEndDrag?.Invoke(this);
        }
    }
}