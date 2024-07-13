using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public BulletData bulletData;
    public float bounce;
    [SerializeField] private LayerMask destructibleLayer;
    public Rigidbody rb;
    private Vector3 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.velocity = transform.up * bulletData.speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (bounce >= bulletData.maxBounces)
        {
            Destroy(gameObject);
        }
        if ((destructibleLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else
        {
            var firstContact = collision.GetContact(0);
            Vector3 newVelocity = Vector3.Reflect(direction.normalized, firstContact.normal);
            Shoot(newVelocity.normalized);

            Vector3 newForward = newVelocity.normalized;
            Quaternion rotationToApply = Quaternion.LookRotation(newForward) * Quaternion.Euler(90, 0, 0);
            transform.rotation = rotationToApply;          
            bounce++;
        }
    }
    
    public void Shoot(Vector3 dir)
    {
        direction = dir;
        rb.velocity = dir * bulletData.speed;
    }
}