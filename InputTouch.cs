using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class InputTouch : MonoBehaviour
{
   [SerializeField] 
   private GameObject player;
   
   private PlayerInput playerInput;
   private InputAction touchPositionAction;
   private InputAction touchPressAction;

   private void Awake()
   {
      playerInput = GetComponent<PlayerInput>();
      touchPressAction = playerInput.actions("TouchPress");
      touchPositionAction = playerInput.actions("TouchPosition");
   }
   
   private void OnEnable()
   {
      touchPressAction.performed += TouchPress;
      //touchPositionAction.performed += OnTouchPosition;
   }
   
   private void OnDisable()
   {
      touchPressAction.performed -= TouchPress;
      //touchPositionAction.performed -= OnTouchPosition;
   }
   private void TouchPress(InputAction.CallbackContext context)
   {
    Vector3 = Camera.main.ScreenToWorldPoint(touchPositionAction.ReadValue<Vector2>());
    position.z = player.transform.position.z;
    player.transform.position = posision;
   }
}
