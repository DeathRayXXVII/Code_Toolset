using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueData dialogueData;
    public bool requireinput;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerController playerController))
        {   
            playerController.Interactable = this;
            if (!requireinput)
            {
                playerController.DialogueUI.ShowDialogue(dialogueData);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerController playerController))
        {
            if (playerController.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                playerController.Interactable = null;
                if (!requireinput)
                {
                    playerController.DialogueUI.typewriterEffect.Stop();
                    playerController.DialogueUI.CloseDialogueBox();
                    
                }
            }
        }
    }

    public void Interact(PlayerController player)
    {
        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            if (responseEvents.DialogueData == dialogueData)
            {
                player.DialogueUI.AddResponseEvents(responseEvents.Events);
                break;
            }
        }

        player.DialogueUI.ShowDialogue(dialogueData);
    }
    public void Interact1(PlayerController playerController)
    {
    }
}
