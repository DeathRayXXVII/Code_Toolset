using Scripts.Data;
using Scripts.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchInputMovement : MonoBehaviour
{
    public FloatData speed;
    public GameInputsSO controls;
    private Vector2 touchStartPosition;
    private bool isTouching;

    void Update()
    {
        if (isTouching)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            float touchDelta = touchPosition.x - touchStartPosition.x;
            transform.position += new Vector3(touchDelta, 0f, 0f) * (speed.value * Time.deltaTime);
        }
    }

    public void OnTouchStart(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            touchStartPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            isTouching = true;
        }
    }

    public void OnTouchEnd(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            isTouching = false;
        }
    }
}
