using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.InputSystem
{
    public class ControllerInputBehavoir : MonoBehaviour
    {
        GameInputs controls;
        public Vector2 move;
        public Vector2 rotate;

        public float moveSpeed = 1f;
        public float rotateSpeed = 100f;
        public float jumpHeight = 1.0f;
        public bool isJumping = false;

        private float verticalSpeed = 0;
        private CharacterController characterController;
        //Quaternion currentRotation;
        
        public float smoothTime = 0.1f;
        private float yVelocity = 0.0f;
        private float targetAngle;
        public void Awake()
        {
            controls = new GameInputs();
            controls.Controller.Rotate.performed += Rotate;
            //controls.Controller.Rotate.canceled += Rotate;
            controls.Controller.Move.performed += Move;
            controls.Controller.Move.canceled += Move;
            controls.Controller.Jump.performed += Jump;
            controls.Controller.Jump.canceled += ctx => isJumping = false;

            characterController = GetComponent<CharacterController>();
        }

        public void Update()
        {
            
        }

        public void FixedUpdate()
        {
            if (characterController.isGrounded)
            {
                verticalSpeed = 0; // Reset the vertical speed if the character is grounded
                if (isJumping)
                {
                    verticalSpeed = Mathf.Sqrt(jumpHeight * -10f * Physics.gravity.y); // Calculate the initial vertical speed
                    isJumping = false;
                }
            }
            else
            {
                verticalSpeed += Physics.gravity.y * Time.deltaTime; // Apply gravity
            }

            // Move the character controller
            characterController.Move(new Vector3(0, verticalSpeed, 0) * Time.deltaTime);
            
            Vector3 m = new Vector3(move.x, 0, move.y) * (Time.deltaTime * moveSpeed);
            m = transform.TransformDirection(m);
            characterController.Move(m);
            
        }

        public void Jump(InputAction.CallbackContext ctx)
        {
            jumpHeight = ctx.ReadValue<float>();
            
            if (characterController.isGrounded)
            {
                isJumping = true;
            }
        }

        public void Move(InputAction.CallbackContext ctx)
        {
            move = ctx.ReadValue<Vector2>();
        }

        public void Rotate(InputAction.CallbackContext ctx)
        {
            rotate = ctx.ReadValue<Vector2>();
            if (rotate.y != 0)
            {
                targetAngle = rotate.y * 360;
            }
            float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref yVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0, smoothedAngle, 0);
            
            // Vector3 r = new Vector3(0, rotate.y * 360, 0) * rotateSpeed;
            // Quaternion targetQuaternion = Quaternion.Euler(r);
            // Quaternion smoothedRotation = Quaternion.RotateTowards(transform.rotation, targetQuaternion, Time.deltaTime * rotateSpeed);
            // transform.rotation = smoothedRotation;
        }

        public void OnEnable()
        {
            controls.Controller.Enable();
        }

        public void OnDisable()
        {
            controls.Controller.Disable();
        }
    }
}