using Scripts.Data;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankController : MonoBehaviour
{
    public int playerNumber = 1;
    [SerializeField] private FloatData speed;
    [SerializeField] private FloatData turnSpeed;
    [SerializeField] private GameObject barrel;
    private Vector2 lastMousePosition;

    //private CharacterController cc;
    private Rigidbody rb;
    [SerializeField] private InputActionReference moveControl;
    [SerializeField] private InputActionReference turnControl;

    private Vector2 movementInput;
    private Vector2 turnInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rb.velocity = Vector3.zero;
        moveControl.action.Enable();
        turnControl.action.Enable();
        
        moveControl.action.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        moveControl.action.canceled += ctx => movementInput = Vector2.zero;
        turnControl.action.performed += ctx => turnInput = ctx.ReadValue<Vector2>();
        turnControl.action.canceled += ctx => turnInput = Vector2.zero;
    }

    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
        moveControl.action.Disable();
        turnControl.action.Disable();
        
        moveControl.action.performed -= ctx => movementInput = ctx.ReadValue<Vector2>();
        turnControl.action.performed -= ctx => turnInput = ctx.ReadValue<Vector2>();
    }

    void Update()
    {
        // Convert mouse position to world space
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector3 targetPosition = hitInfo.point;
            targetPosition.y = barrel.transform.position.y; // Keep the barrel's height constant
            Vector3 direction = targetPosition - barrel.transform.position;

            // Directly set the barrel's rotation to face the target position
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            lookRotation *= Quaternion.Euler(0, 90, 0);
            barrel.transform.rotation = lookRotation;
        }
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        if (movementInput != Vector2.zero)
        {
            float move = movementInput.y * speed.value * Time.deltaTime;
            Vector3 movement = transform.forward * move;
            rb.MovePosition(rb.position + movement);
        }
    }

    private void Turn()
    {
        if (turnInput != Vector2.zero)
        {
            float turn = turnInput.x * turnSpeed.value * Time.deltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }
}