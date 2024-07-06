using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public BulletData bulletData;
    public float bounce;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletData.speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else
        {
            Vector3 incomingVector = rb.velocity; // Use the bullet's current velocity
            Vector3 normalVector = collision.GetContact(0).normal;
            Vector3 reflectVector = Vector3.Reflect(incomingVector, normalVector);
            rb.velocity = reflectVector.normalized * bulletData.speed; // Apply the new velocity based on reflection

            bounce += 1;
            if (bounce >= bulletData.maxBounces)
            {
                Destroy(gameObject);
            }
        }
    }
}