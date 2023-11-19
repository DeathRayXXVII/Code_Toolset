using UnityEngine;

public class Paddle : MonoBehaviour
{

    public Rigidbody rb;
    public Vector2 direction;
    public float maxBounceAngle = 75.0f;
    public float speed = 30.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ResetPaddle()
    {
        gameObject.SetActive(true);
        rb.velocity = Vector2.zero;
        transform.position = new Vector2(0f, -7.1f);
    }

    /*private void FixedUpdate()
    {
        if (direction != Vector2.zero) {
            rb.AddForce(direction * speed);
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            Vector3 paddlePosition = transform.position;
            Vector2 ballPosition = collision.transform.position;

            float offset = paddlePosition.x - ballPosition.x;
            float width = collision.collider.bounds.size.x / 2;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rb.velocity);
            float bounceAngle = (offset / width) * maxBounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -maxBounceAngle, maxBounceAngle);
            
            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ball.rb.velocity = rotation * Vector2.up * ball.rb.velocity.magnitude;
        }
    }
}

