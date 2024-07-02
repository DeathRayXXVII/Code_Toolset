using UnityEngine;
using UnityEngine.Events;

public class InteractableInput : MonoBehaviour, IInteractable
{
    public UnityEvent enterEvent, stayEvent, exitEvent, interactEvent;
    private bool isExiting;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerController playerController))
        {
            playerController.Interactable = this;
            enterEvent.Invoke();
            isExiting = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerController playerController))
        {
            playerController.Interactable = this;
            if (!isExiting)
            {
                stayEvent.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerController playerController))
        {
            playerController.Interactable = null;
            exitEvent.Invoke();
            isExiting = true;
        }
    }

    public void Interact(PlayerController playerController)
    {
        interactEvent.Invoke();
    }
    public void Interact1(PlayerController playerController)
    {
    }
}