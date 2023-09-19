using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public BallMovementData ballMovementDataObj;
    private Rigidbody rb;
    private Vector3 direction;
    private float speed;

    //public bool isWall;
    //public LayerMask whatIsWall;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //direction = ballMovementDataObj.startingDirection.normalized;
        rb.velocity = ballMovementDataObj.startingDirection.normalized * ballMovementDataObj.initialSpeed;
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reflect the ball's direction upon collision with other objects
        ballMovementDataObj.startingDirection = Vector2.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
        rb.velocity = ballMovementDataObj.startingDirection * ballMovementDataObj.initialSpeed;

        /*public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                SetDirection(Vector3.Reflect(direction, other.transform.forward));
            }
        }

        public void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                SetDirection(Vector3.Reflect(direction, other.contacts[0].normal));
            }
        }
        public void SetDirection(Vector3 newDirection)
        {
            direction = newDirection.normalized;
        }

        public void IncreaseSpeed()
        {
            speed = Mathf.Clamp(speed + ballMovementDataObj.speedIncreaseRate, ballMovementDataObj.initialSpeed, ballMovementDataObj.maxSpeed);
        }*/
    }
}
