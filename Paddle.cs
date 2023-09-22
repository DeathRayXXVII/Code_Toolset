using UnityEngine;

public class Paddle : MonoBehaviour
{

    public Rigidbody rb;
    public Vector3 direction = Vector3.zero;
    public float maxBounceAngle = 75.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    public void ResetPaddle()
    {
        transform.position = new Vector3(0f, transform.position.y, 0f);
        rb.velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (direction != Vector3.zero)
        {
            rb.AddForce(direction);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null )
        {   
            
            Vector3 paddlePosition = transform.position;
            Vector3 contactPoint = collision.GetContact(0).point;

            float offset = paddlePosition.x - contactPoint.x;
            float width = collision.collider.bounds.size.x / 2;

            float currentAngle = Vector3.SignedAngle(Vector3.down, ball.rb.velocity, Vector3.forward);
            float bounceAngle = (offset / width) * maxBounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -maxBounceAngle, maxBounceAngle);
            
            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ball.rb.velocity = rotation * Vector3.up * ball.rb.velocity.magnitude;

        }
    }
}

