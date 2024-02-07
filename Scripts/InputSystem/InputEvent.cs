using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputEvent : MonoBehaviour
{
    public InputActionReference input;
    public UnityEvent firstEvent, secondEvent;
    private bool isPressed = true;

    public void Update()
    {
        if (input.action.triggered)
        {
            if (isPressed)
            {
                firstEvent.Invoke();
                isPressed = false;
            }
            else
            {
                secondEvent.Invoke();
                isPressed = true;
            }
        }
    }

    private void OnEnable()
    {
        input.action.Enable();
    }
    
    private void OnDisable()
    {
        input.action.Disable();
    }
}
