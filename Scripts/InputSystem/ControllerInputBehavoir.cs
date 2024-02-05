using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class ControllerInputBehavoir : MonoBehaviour
{
    [SerializeField] 
    private InputActionReference move;
    [SerializeField] 
    private InputActionReference jump;
    
    
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField] 
    private float rotationSpeed = 4f;


    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool doubleJumped = false;
    private Transform cameraMainTransform;

    private void OnEnable()
    {
        move.action.Enable();
        jump.action.Enable();
    }

    private void OnDisable()
    {
        move.action.Disable();
        jump.action.Disable();
    }
    
    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        if (Camera.main != null) cameraMainTransform = Camera.main.transform;
    }

    private void Update()
    {
        // Info to allow Jump
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            doubleJumped = false; // Reset double jump when grounded
        }

        // Basic Movement
        Vector2 movement = move.action.ReadValue<Vector2>();
        Vector3 moves = new Vector3(movement.x, 0, movement.y);
        moves = cameraMainTransform.forward * moves.z + cameraMainTransform.right * moves.x;
        moves.y = 0f;

        controller.Move(moves * (Time.deltaTime * playerSpeed));

        // Jump
        if (jump.action.triggered)
        {
            if (groundedPlayer || !doubleJumped)
            {
                if (!groundedPlayer)
                {
                    doubleJumped = true;
                }
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Makes the character move in the direction of the camera
        if (movement != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + cameraMainTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }
}