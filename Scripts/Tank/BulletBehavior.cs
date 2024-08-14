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
        rb.constraints = RigidbodyConstraints.FreezeRotationY;
        //rb.velocity = transform.up * bulletData.speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (bounce >= bulletData.maxBounces)
        {
            //Destroy(gameObject);
            bounce = 0;
            gameObject.SetActive(false);
            
        }
        if ((destructibleLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            Destroy(collision.gameObject);
            //Destroy(gameObject);
            bounce = 0;
            gameObject.SetActive(false);
        }
        else
        {
            var firstContact = collision.GetContact(0);
            Vector3 newVelocity = Vector3.Reflect(direction.normalized, firstContact.normal);
            Shoot(newVelocity.normalized);
            
            Vector3 newForward = newVelocity.normalized;
            Quaternion rotationToApply = Quaternion.LookRotation(newForward) * Quaternion.Euler(0, 180, 0);
            
            transform.rotation = rotationToApply;
            
            bounce++;
        }
    }
    
    public void Shoot(Vector3 dir)
    {
        direction = dir;
        rb.velocity = dir * bulletData.speed;
    }
    
    public void ResetBullet()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}