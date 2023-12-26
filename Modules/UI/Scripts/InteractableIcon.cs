using UnityEngine;
using UnityEngine.UI;

namespace SpaceGame
{
    public class InteractableIcon : MonoBehaviour
    {
        public void SetActive(bool active)
        {
            _active = active;
            LeanTween.cancel(gameObject);
            if (active)
            {
                LeanTween.scale(gameObject, Vector3.one, 0.21f).setEaseOutBack();
            }
            else
            {
                LeanTween.scale(gameObject, Vector3.zero, 0.21f).setEaseInBack();
            }
        }

        public bool Active => _active;

        public void SetColor(Color color) => _image.color = color;

        private void Update()
        {
            _image.color = Color.Lerp(_image.color, Color.white, 8 * Time.deltaTime);
        }

        [SerializeField] private Image _image;
        private bool _active;
    }
}