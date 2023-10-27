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
      touchPositionAction.performed += TouchPositionChanged;
      touchPositionAction.canceled += TouchPositionChanged;
   }

   private void OnDisable()
   {
      touchPressAction.performed -= TouchPressed;
      touchPositionAction.performed -= TouchPositionChanged;
      touchPositionAction.canceled -= TouchPositionChanged;
   }

   private void TouchPressed(InputAction.CallbackContext context)
   {
      UpdateFollowTargetPosition();
   }

   private void TouchPositionChanged(InputAction.CallbackContext context)
   {
      UpdateFollowTargetPosition();
   }

   private void UpdateFollowTargetPosition()
   {
      Vector3 position = touchPositionAction.ReadValue<Vector2>();
      position.z = mainCamera.nearClipPlane;
      position = mainCamera.ScreenToWorldPoint(position);
      var position1 = followTarget.transform.position;
      position.y = position1.y;
      position.z = position1.z;
      position1 = position;
      followTarget.transform.position = position1;
   }
}
