using Scripts.Data;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankController : MonoBehaviour
{
    public int playerNumber = 1;
    public FloatData speed;
    public FloatData turnSpeed;

    private CharacterController cc;
    [SerializeField] private InputActionReference moveControl;
    [SerializeField] private InputActionReference turnControl;

    private Vector2 movementInput;
    private Vector2 turnInput;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        cc.enabled = true;
        moveControl.action.Enable();
        turnControl.action.Enable();
        
        moveControl.action.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        moveControl.action.canceled += ctx => movementInput = Vector2.zero;
        turnControl.action.performed += ctx => turnInput = ctx.ReadValue<Vector2>();
        turnControl.action.canceled += ctx => turnInput = Vector2.zero;
    }

    private void OnDisable()
    {
        cc.enabled = false;
        moveControl.action.Disable();
        turnControl.action.Disable();
        
        moveControl.action.performed -= ctx => movementInput = ctx.ReadValue<Vector2>();
        turnControl.action.performed -= ctx => turnInput = ctx.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        Transform cameraMainTransform = Camera.main.transform;
        Vector3 forward = cameraMainTransform.forward;
        Vector3 right = cameraMainTransform.right;
        forward.y = 0; // Ensure movement is horizontal.
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // Correcting the input direction by rotating the input vector 90 degrees clockwise
        Vector3 correctedInput = new Vector3(movementInput.y, 0, movementInput.x);

        Vector3 desiredDirection = (forward * correctedInput.x + right * correctedInput.z).normalized;

        if (desiredDirection != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(desiredDirection, Vector3.up);
            cc.transform.rotation = Quaternion.Slerp(cc.transform.rotation, desiredRotation, turnSpeed.value * Time.deltaTime);
        }

        Vector3 movement = desiredDirection * (speed.value * Time.deltaTime);
        cc.Move(movement);
    }

    private void Turn()
    {
        if (turnInput != Vector2.zero) // Only rotate if there is input
        {
            // Assuming cameraMainTransform is a reference to your main camera's transform.
            Transform cameraMainTransform = Camera.main.transform;

            // Calculate the target rotation angle based on the turn input and camera orientation.
            float targetAngle = Mathf.Atan2(turnInput.y, -turnInput.x) * Mathf.Rad2Deg + cameraMainTransform.eulerAngles.y;
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

            // Smoothly interpolate to the target rotation.
            cc.transform.rotation = Quaternion.Lerp(cc.transform.rotation, targetRotation, Time.deltaTime * turnSpeed.value);
        }
    }
}