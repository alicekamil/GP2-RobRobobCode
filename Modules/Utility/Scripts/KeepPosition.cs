using UnityEngine;

public class KeepPosition : MonoBehaviour
{
    private void Awake()
    {
        _relativePos = transform.localPosition;
        _parent = transform.parent;
    }

    private void LateUpdate()
    {
        transform.position = _parent.position + _relativePos;
    }

    private Transform _parent;
    private Vector3 _relativePos;
}
