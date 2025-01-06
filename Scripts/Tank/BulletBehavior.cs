using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public BulletData bulletData;
    public float bounce;
    [SerializeField] private LayerMask destructibleLayer;
    public Rigidbody rb;
    private Vector3 direction;
    private TankGameManager tankGameManager;

    private void Start()
    {
        tankGameManager = FindObjectOfType<TankGameManager>();
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationY;
        //rb.velocity = transform.up * bulletData.speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (bounce >= bulletData.maxBounces)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
            
        }
        if (collision.gameObject.layer == 8)
        {
            collision.gameObject.SetActive(false);
            tankGameManager.LevelFail();
        }
        else if ((destructibleLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            collision.gameObject.SetActive(false);
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
        rb.linearVelocity = dir * bulletData.speed;
    }
    
    public void ResetBullet()
    {
        gameObject.SetActive(false);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}