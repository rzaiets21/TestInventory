using System;
using UnityEngine;

namespace Items.Model
{
    [Serializable]
    public class ItemInfo
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public bool CanBeAttachedToBackpack { get; private set; }
        [field: SerializeField] public Vector3 PositionOnBackpack { get; private set; }
        [field: SerializeField] public Vector3 RotationOnBackpack { get; private set; }
    }
}