using Scripts;
using Scripts.UnityActions;
using UnityEngine;

public class InventoryPrefabItemBehaviour : ColliderBehaviour
{
    public GameAction gameActionObj;
    public InventoryItem inventoryItemObj;
    
    protected override void Start()
    {
        base.Start();
        inventoryItemObj.GameActionObj = gameActionObj;
        triggerEnterEvent.AddListener(UseItem);
    }

    private void UseItem()
    {
        if (inventoryItemObj == null) return;
        inventoryItemObj.UsedOrPurchase = false;
        gameObject.SetActive(false);
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        gameActionObj.Raise();
    }
}