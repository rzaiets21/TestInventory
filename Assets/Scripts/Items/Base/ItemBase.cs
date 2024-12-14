using UnityEngine;

namespace Items.Base
{
    public abstract class ItemBase : MonoBehaviour
    {
        [SerializeField] private Collider collider;
        [SerializeField] private Rigidbody rigidbody;
    }
}