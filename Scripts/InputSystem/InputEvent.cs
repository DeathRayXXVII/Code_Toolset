using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputEvent : MonoBehaviour
{
    [Header("Custom Input Action Reference Event")]
    public InputActionReference input;
    public UnityEvent firstEvent, secondEvent;
    private bool isPressed = true;
    [Header("First Selected Button")]
    public GameObject firstSelectedButton;

    public void Update()
    {
        if (input == null)
        {
            return;
        }
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
    
    public void MenuStart()
    {
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }
    public void MenuClose()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnEnable()
    {
        if (input == null)
        {
            return;
        }
        input.action.Enable();
    }
    
    private void OnDisable()
    {
        if (input == null)
        {
            return;
        }
        input.action.Disable();
    }
}
