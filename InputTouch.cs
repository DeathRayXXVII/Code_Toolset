using UnityEngine;
using UnityEngine.InputSystem;

public class InputTouch : MonoBehaviour
{
   public GameObject followTarget;
   public PlayerInput playerInput;
   private InputAction touchPositionAction;
   private InputAction touchPressAction;
   private Camera mainCamera;

   private void Awake()
   {
      playerInput = GetComponent<PlayerInput>();
      touchPressAction = playerInput.actions.FindAction("TouchPress");
      touchPositionAction = playerInput.actions.FindAction("TouchPosition");
      mainCamera = Camera.main;
   }

   private void OnEnable()
   {
      touchPressAction.performed += TouchPressed;
   }

   private void OnDisable()
   {
      touchPressAction.performed -= TouchPressed;
   }

   private void TouchPressed(InputAction.CallbackContext context)
   {
      UpdateFollowTargetPosition();
   }

   private void LateUpdate()
   {
      UpdateFollowTargetPosition();
   }

   private void UpdateFollowTargetPosition()
   {
      Vector3 position = touchPositionAction.ReadValue<Vector2>();
      //position.z = 30;
      position = mainCamera.ScreenToWorldPoint(position);
      position.y = followTarget.transform.position.y;
      position.z = followTarget.transform.position.z;
      followTarget.transform.position = position;
   }
}
