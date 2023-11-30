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
    
    public void ConfigButton(IStoreItem storeItem)
    {
        Vector3 toggelScaleFactor = new Vector3(2.5f, 4, 1);
        Vector3 buttonScale = new Vector3(2,1,1);
        Vector3 lableScale = new Vector3(.5f, 1, 1);
        Vector3 lableMoveFactor = new Vector3(0, -1.56f, -.1f);
        Vector3 toggelMoveFactor = new Vector3(4, .75f, 0);
        
        if (storeItem == null) return;
        ButtonObj.image.sprite = storeItem.PreviewArt;
        //ButtonObj.image.material = storeItem.PreviewMaterial;
        Label.text = storeItem.ThisName;
        ButtonObj.interactable = !storeItem.UsedOrPurchase;
        StoreItemObj = storeItem;
        ToggleObj.isOn = storeItem.UsedOrPurchase;
        PriceLabel.text = $"{storeItem.Price}";
        
        ToggleObj.transform.localScale = toggelScaleFactor;
        ButtonObj.transform.localScale = buttonScale; 
        Label.transform.localScale = lableScale;
        ToggleObj.transform.position += toggelMoveFactor;
        Label.transform.position += lableMoveFactor;
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

            purchaseEvent?.Invoke();
        }
        else
        {
            noPurchaseEvent?.Invoke();
        }
    }
}