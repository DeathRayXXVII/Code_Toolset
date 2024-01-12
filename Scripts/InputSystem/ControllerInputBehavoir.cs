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

        public void Awake()
        {
            controls = new GameInputs();
            controls.Controller.Rotate.performed += Rotate;
            controls.Controller.Rotate.canceled += Rotate;
            controls.Controller.Move.performed += Move;
            controls.Controller.Move.canceled += Move;
            controls.Controller.Jump.performed += Jump;
            controls.Controller.Jump.canceled += ctx => isJumping = false;

            characterController = GetComponent<CharacterController>();
        }

        public void Update()
        {
            // Rotate on the y axis
            //Vector3 r = new Vector3(0, rotate.y, 0) * (Time.deltaTime * rotateSpeed);
            //transform.Rotate(r);

            // Move on the x and z axis
            Vector3 m = new Vector3(move.x, 0, move.y) * (Time.deltaTime * moveSpeed);
            m = transform.TransformDirection(m);
            characterController.Move(m);

            // Apply gravity
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
            
            //Vector3 m = new Vector3(-move.x, 0, move.y) * (Time.deltaTime * moveSpeed);
            //transform.Translate(m, Space.World);
            
        }

        public void Rotate(InputAction.CallbackContext ctx)
        {
            rotate = ctx.ReadValue<Vector2>();
            Vector3 r = new Vector3(0, rotate.y, 0) * (Time.deltaTime * rotateSpeed);
            transform.Rotate(r);
            //Quaternion rotation = Quaternion.Euler(0, rotate.y * Time.deltaTime * rotateSpeed, 0);
            //transform.rotation = rotation;
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