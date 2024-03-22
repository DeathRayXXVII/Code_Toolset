using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputPickup : MonoBehaviour
{
    public InputActionReference input;
    public bool isPressed;
    public Transform player;
    public Transform hand;
    
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
                isPressed = false;
            }
            else
            {
                isPressed = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isPressed)
            {
                Pickup();
            }

            if (isPressed == false)
            {
                Drop();
            }
        }
    }
    public void Pickup()
    {
        transform.SetParent(player.transform);
        transform.position = hand.position;
    }
    public void Drop()
    {
        transform.SetParent(null);
    }
}
