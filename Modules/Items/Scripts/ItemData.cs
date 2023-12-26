using UnityEngine;

namespace SpaceGame
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "Data/ItemData")]
    public class ItemData : ScriptableObject
    {
        public string Id => _id;
        public GameObject GameObject => _gameObject;
        public bool IsSpecialAmmo => _isSpecialAmmo;

        [SerializeField] private string _id;
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private bool _isSpecialAmmo;
    }
}