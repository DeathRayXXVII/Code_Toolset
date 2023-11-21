using Scripts.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StoreUIButtonBehaviour : InventoryUIButtonBehaviour
{
    public UnityEvent purchaseEvent, noPurchaseEvent;
    public IntData cash;
    public Text PriceLabel { get; private set; }
    public Toggle ToggleObj { get; private set; }
    public IStoreItem StoreItemObj { get; set; }
    public InventoryData inventoryDataObj;
    public InventoryConfigBehaviour inventoryConfigBehaviour;
    
    protected override void Awake()
    {
        base.Awake();
        ToggleObj = GetComponentInChildren<Toggle>();
        PriceLabel = ToggleObj.GetComponentInChildren<Text>();
        ButtonObj.onClick.AddListener(AttemptPurchase);
    }

    private void AttemptPurchase()
    {
        if (StoreItemObj.Price <= cash.value)
        {
            StoreItemObj.UsedOrPurchase = true;
            ToggleObj.isOn = true;
            cash.UpdateValue(-StoreItemObj.Price);
            ButtonObj.interactable = false;
            //GameState.Instance.CurrentAction = "Purchase";
            var purchaseItem = StoreItemObj as InventoryItem;
            purchaseItem.OnPurchase(inventoryDataObj);
            purchaseEvent?.Invoke();
        }
        else
        {
            noPurchaseEvent?.Invoke();
        }
    }
}