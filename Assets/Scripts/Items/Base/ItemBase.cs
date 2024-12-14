using UnityEngine;

namespace Items.Base
{
    public abstract class ItemBase : MonoBehaviour
    {
        [SerializeField] protected Collider collider;
        [SerializeField] protected Rigidbody rigidbody;
    }
}