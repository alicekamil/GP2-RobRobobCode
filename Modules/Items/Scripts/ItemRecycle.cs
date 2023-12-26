using UnityEngine;

namespace SpaceGame
{
    public class ItemRecycle : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody.TryGetComponent<ItemInteractable>(out var item))
            {
                if (item.ItemId == "wrench")
                {
                    other.attachedRigidbody.transform.position = _wrenchRespawnPoint.position;
                    other.attachedRigidbody.velocity = Vector3.zero;
                    other.attachedRigidbody.angularVelocity = Vector3.zero;
                }
                else
                {
                    Destroy(other.attachedRigidbody.gameObject);
                }
            }
        }

        [SerializeField] private Transform _wrenchRespawnPoint;
    }
}