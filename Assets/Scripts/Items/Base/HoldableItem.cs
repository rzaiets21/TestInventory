using System;
using UnityEngine;

namespace Items.Base
{
    public abstract class HoldableItem : SelectableItem
    {
        private const float HoldTime = 0.45f;

        private bool _holding;
        private float _selectedTime;

        public event Action<ItemBase> onHoldBegin;
        public event Action<ItemBase> onHoldEnd;

        private bool Holding
        {
            get => _holding;
            set
            {
                if(_holding == value)
                    return;

                _holding = value;
                if (_holding)
                    OnHoldBegin();
                else
                    OnHoldEnd();
            }
        }

        private void Update()
        {
            OnUpdate();
            
            if(!_isSelected)
                return;
            
            if(Holding)
                return;

            Holding = (Time.time - _selectedTime) > HoldTime;
        }

        protected virtual void OnUpdate() { }
        
        protected override void OnSelect()
        {
            base.OnSelect();
            _selectedTime = Time.time;
        }

        protected override void OnRelease()
        {
            base.OnRelease();
            Holding = false;
        }

        protected virtual void OnHoldBegin()
        {
            onHoldBegin?.Invoke(this);
        }

        protected virtual void OnHoldEnd()
        {
            onHoldEnd?.Invoke(this);
        }
    }
}