using UnityEngine;

namespace SpaceGame
{
    /// <summary>
    /// Responsible for displaying items
    /// </summary>
    public class ItemHolder : MonoBehaviour
    {
        public string ItemId => _id;
        public ItemData Item => ItemDatabase.Get(_id);
        public Transform ItemParent => _itemParent;

        private void Awake()
        {
            _defaultScale = _itemParent.localScale;
        }

        public void SetItem(string id)
        {
            _id = id;

            if (_activeItem != null)
            {
                Destroy(_activeItem);
            }

            if (_id != null)
            {
                _activeItem = Instantiate(Item.GameObject, _itemParent);
                if (!_enableColliders)
                    _activeItem.GetComponent<Collider>().enabled = false;
                LeanTween.cancel(_itemParent.gameObject);
                _itemParent.localScale = Vector3.zero;
                LeanTween.scale(_itemParent.gameObject, _defaultScale, 0.21f).setEaseOutBack();
            }
        }

        [SerializeField] private Transform _itemParent;
        [SerializeField] private bool _enableColliders;
        private GameObject _activeItem;
        private Vector3 _defaultScale;
        private string _id;
    }
}