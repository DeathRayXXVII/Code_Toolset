using UnityEngine;
using Vector2 = System.Numerics.Vector2;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
public class Paddle : MonoBehaviour
{
    
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private Vector2 touchDelta;
    private bool isDragging = false;
    private Rigidbody rb;

    public float paddleSpeed = 5.0f;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isDragging)
        {
            // Calculate the delta movement based on touch position
            touchDelta = (touchEndPos - touchStartPos) * paddleSpeed * Time.deltaTime;

            // Move the paddle
            rb.MovePosition(rb.position + new Vector3(touchDelta.X, 0));
        }
    }

    public void OnTouchStart(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isDragging = true;
            touchStartPos = context.ReadValue<Vector2>();
        }
    }

    public void OnTouchEnd(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            isDragging = false;
        }
    }

    public void OnTouchMove(InputAction.CallbackContext context)
    {
        if (isDragging)
        {
            touchEndPos = context.ReadValue<Vector2>();
        }
    }
}
   /* //public BallMovementData ballMovementDataObj;
    public Rigidbody rb;
    public Vector3 direction;
    
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        if(direction != Vector3.zero)
        {
            rb.AddForce(direction);
        }
        //Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), 0);
        //rb.velocity = movement * ballMovementDataObj.initialSpeed;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Balls ball = collision.gameObject.GetComponent<Balls>();

        if (ball != null)
        {
            Vector3 paddlePosition = ball.transform.position - transform.position;
            Vector3 contactPoint = collision.GetContact(0).point;

            float offset = paddlePosition.x - contactPoint.x;
            float width = collision.otherCollider.bounds.size.x / 2;

            float currentAngle = Vector3.SignedAngle(Vector3.up, ball.rb.velocity, Vector3.forward);
            float bounceAngle = (offset / width) * maxBounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -maxBounceAngle, maxBounceAngle);

            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ball.rb.velocity = rotation * Vector3.up * ballMovementDataObj.initialSpeed;
        
    }}*/

