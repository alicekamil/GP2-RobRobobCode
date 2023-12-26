using UnityEngine;
using TMPro;

namespace SpaceGame
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ValueLabel : MonoBehaviour
    {
        private void Awake()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
            _rect = GetComponent<RectTransform>();
        }

        private void OnEnable() => _eventChannel.EventRaised += UpdateText;
        private void OnDisable() => _eventChannel.EventRaised -= UpdateText;
        private void UpdateText(int value)
        {
            transform.localScale = Vector3.one * 1.5f;
            LeanTween.cancel(_rect);
            LeanTween.scale(_rect, Vector3.one, 0.25f).setEaseOutBack();
            _textMesh.text = $"{_extraText}{value}";
        }

        [SerializeField]
        private IntEventChannel _eventChannel;
        [SerializeField]
        private string _extraText;
        private RectTransform _rect;

        private TextMeshProUGUI _textMesh;
    }
}