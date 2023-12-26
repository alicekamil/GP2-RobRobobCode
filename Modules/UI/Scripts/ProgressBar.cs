using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float Progress
    {
        get => _progress;
        set
        {
            _progress = value;
            _progressFill.fillAmount = _progress;
        }
    }

    public Color Color
    {
        get => _progressFill.color;
        set => _progressFill.color = value;
    }

    [SerializeField] private Image _progressFill;

    private float _progress;
}