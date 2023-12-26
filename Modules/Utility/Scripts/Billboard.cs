using UnityEngine;

public class Billboard : MonoBehaviour
{
    void Awake()
    {
        _camera = Camera.main;
    }

    void LateUpdate()
    {
        transform.LookAt(_camera.transform);
        transform.Rotate(0, 180, 0);
    }

    private Camera _camera;
}