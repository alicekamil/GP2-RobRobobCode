using System.Collections;
using UnityEngine;
using System;

public class DestroyFire : MonoBehaviour
{
    public event Action<DestroyFire> Destroyed;

    private void Start()
    { 
        StartCoroutine(DestroyAfterTime());
    }

    public void DestroySelf()
    {
        Destroyed?.Invoke(this);
        Destroy(gameObject);
    }
    
    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(_fireTimer);
        DestroySelf();
    }

    [SerializeField]
    private float _fireTimer = 20f;
}