using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputEvent : MonoBehaviour
{
    [Header("Custom Input Action Reference Event")]
    public InputActionReference input;
    public UnityEvent firstEvent, secondEvent;
    [SerializeField] private bool inputEnabled;
    private bool isPressed = true;
    [Header("First Selected Button")]
    public GameObject firstSelectedButton;

    private void Start()
    {
        if (input == null)
        {
            return;
        }
        if (inputEnabled)
        {
            input.action.Enable();
        }
        else
        {
            input.action.Disable();
        }
    }

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

    public void OnEnable()
    {
        if (input == null)
        {
            return;
        }
        input.action.Enable();
        inputEnabled = true;
    }
    
    public void OnDisable()
    {
        if (input == null)
        {
            return;
        }
        input.action.Disable();
        inputEnabled = false;
    }
}
