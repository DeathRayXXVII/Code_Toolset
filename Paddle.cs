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

    private void Start()
    {
        ResetPaddle();
    }

    public void ResetPaddle()
    {
        rb.velocity = Vector2.zero;
        transform.position = new Vector2(0f, transform.position.y);
    }

    private void FixedUpdate()
    {
        if (direction != Vector2.zero) {
            rb.AddForce(direction * speed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) {
            return;
        }

        Rigidbody ball = collision.rigidbody;
        Collider paddle = collision.collider;

        Vector2 ballDirection = ball.velocity.normalized;
        Vector2 contactDistance = paddle.bounds.center - ball.transform.position;
        
        float relativeContactPoint = Mathf.Clamp(contactDistance.x / paddle.bounds.size.x, -1f, 1f);
        float bounceAngle = Mathf.Lerp(-maxBounceAngle, maxBounceAngle, (relativeContactPoint + 1f) * 0.5f);
        ballDirection = Quaternion.AngleAxis(bounceAngle, Vector3.up) * ballDirection;

        ball.velocity = ballDirection * ball.velocity.magnitude;
    }
}

