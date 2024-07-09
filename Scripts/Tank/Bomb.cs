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
            // Optionally, you can add more logic here to handle different types of destructible objects differently
            Destroy(hitCollider.gameObject);
        }
        onExplode.Invoke(); // Trigger any additional explosion effects
        Destroy(gameObject); // Destroy the bomb itself
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
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
