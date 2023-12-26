using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private void Awake()
    {
        _originalPos = transform.localPosition;
    }

    public void Shake()
    {
        LeanTween.cancel(gameObject);
        ResetPosition();

        LeanTween.value(gameObject, 0f, 1f, _duration).setOnUpdate(f =>
        {
            var pos = transform.localPosition;
            float s = _strength * LeanTween.easeOutQuad(1f, 0f, f);
            pos.x = (Mathf.PerlinNoise1D(f * _speed) - 0.5f) * s;
            pos.y = (Mathf.PerlinNoise1D(f * _speed + 31.6f) - 0.5f) * s;
            transform.localPosition = pos;
        }).setOnComplete(ResetPosition);
    }

    private void ResetPosition() => transform.localPosition = _originalPos;

    [SerializeField] private float _strength;
    [SerializeField] private float _speed;
    [SerializeField] private float _duration;
    private Vector3 _originalPos;
}