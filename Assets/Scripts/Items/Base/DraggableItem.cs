using System;
using UnityEngine;

namespace Items.Base
{
    public abstract class DraggableItem : SelectableItem
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

            rigidbody.isKinematic = true;
        }

        private void OnMouseDrag()
        {
            if(!IsInteractable)
                return;
            
            if (!_isSelected)
                return;
            
            if (!_isDragging)
            {
                _isDragging = true;
                OnBeginDrag();
                return;
            }

            if (!Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out var hit, 200.0f, 1 << 6))
                return;
            var position = new Vector3(hit.point.x, 0.3f, hit.point.z) ;
            transform.position = position;
            OnDrag();
            // = _mainCamera.ScreenToWorldPoint(Input.mousePosition - _mousePosition);
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
            
            rigidbody.isKinematic = false;
        }

        protected virtual void OnEndDrag()
        {
            onEndDrag?.Invoke(this);
        }
    }
}