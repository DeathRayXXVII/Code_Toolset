using System;
using UnityEngine;
using UnityEngine.Events;

public class Bomb : MonoBehaviour
{
    [SerializeField] private UnityEvent onExplode;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float delayBeforeExplosion = 2f;
    [SerializeField] private LayerMask destructibleLayer;

    private void Start()
    {
        Invoke(nameof(Explode), delayBeforeExplosion);
    }

    private void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, destructibleLayer);
        foreach (var hitCollider in hitColliders)
        {
            Destroy(hitCollider.gameObject);
        }
        onExplode.Invoke();
        Destroy(gameObject);
    }
    
        private void OnCollisionEnter(Collision collision)
    {
        if ((destructibleLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            onExplode.Invoke();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
