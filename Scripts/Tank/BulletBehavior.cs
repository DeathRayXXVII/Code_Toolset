using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private BulletData bulletData;
    [SerializeField] private float bounce;
    [SerializeField] private LayerMask destructibleLayer;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletData.speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((destructibleLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else
        {
            Vector3 incomingVector = rb.velocity.normalized;
            Vector3 normalVector = collision.GetContact(0).normal;
            Vector3 reflectVector = Vector3.Reflect(incomingVector, normalVector).normalized;

            // Adjust for sliding behavior by ensuring a minimum reflection angle
            float minReflectionAngle = 30f; // Minimum angle for reflection, adjust as needed
            if (Vector3.Angle(reflectVector, -incomingVector) < minReflectionAngle)
            {
                reflectVector = Quaternion.Euler(0, minReflectionAngle, 0) * reflectVector;
            }

            // Reapply the original speed to the new direction, excluding Y-axis movement
            rb.velocity = new Vector3(reflectVector.x, 0, reflectVector.z).normalized * bulletData.speed;

            transform.rotation = Quaternion.LookRotation(rb.velocity);

            bounce++;
            if (bounce >= bulletData.maxBounces)
            {
                Destroy(gameObject);
            }
        }
    }
}