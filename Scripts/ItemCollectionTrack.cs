using Scripts.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ItemCollectionTracker : MonoBehaviour
{
    public ItemData itemData;
    public UnityEvent allItemsCollectedEvent, alreadyCollectedEvent, activateEvent;
    public bool itemCollected;

    public IntData activatedObjectsCount;
    public IntData activationThreshold;
    
    private void Start()
    {
        if (itemData.collected == false)
        {
            itemCollected = false;
        }
    }

    public void CollectItem()
    {
        if (itemData.collected == false)
        {
            itemData.collected = true;
        }
        else
        {
            alreadyCollectedEvent.Invoke();
        }
    }

    public void ActivateObject()
    {
        if (itemData.collected && !itemCollected)
        {
            if (activatedObjectsCount.value >= activationThreshold.value)
            {
                return;
            }
            activatedObjectsCount.value++;
            itemCollected = true;
            activateEvent.Invoke();
        }
        if (activatedObjectsCount.value >= activationThreshold.value)
        {
            allItemsCollectedEvent.Invoke();
        }
    }
}