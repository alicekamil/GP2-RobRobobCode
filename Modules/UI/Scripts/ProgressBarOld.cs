using UnityEngine;
using UnityEngine.UI;

namespace SpaceGame
{
    public class ProgressBarOld : MonoBehaviour
    {
        public float Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                if (_useGradient)
                    _progressFill.color = _colorOverProgress.Evaluate(value);
                _slider.value = _progress;
            }
        }

        public Gradient ColorOverProgress
        {
            get => _colorOverProgress;
            set
            {
                _colorOverProgress = value;
                _progressFill.color = _colorOverProgress.Evaluate(_progress);
            }
        }

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        [SerializeField] private bool _useGradient;
        [SerializeField] private Gradient _colorOverProgress;
        [SerializeField] private Image _progressFill;

        private Slider _slider;
        private float _progress;
    }
}