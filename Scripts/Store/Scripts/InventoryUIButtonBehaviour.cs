using System.Diagnostics.SymbolStore;
using Scripts.UnityActions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryUIButtonBehaviour : MonoBehaviour
{
    public Button ButtonObj { get; private set; }
    public string catagory;
    public GameAction gameActionSpriteObj, gameActionMaterialObj, gameActionItemDropObj, ballMaterialObj;
    public UnityEvent spriteRaiseEvent, materialRiseEvent, ballMaterialEvent;
    
    public GameObject ballObj;
    public GameObject playerObj;
    
    public TextMeshProUGUI Label { get; private set; }
    public IInventoryItem InventoryItemObj { get; set; }
    [Header("Scale Factor")] 
    public Vector3 invButtonScale;
    public Vector3 invLableScale;
    public Vector3 invLableMoveFactor;

    protected virtual void Awake()
    {
        ButtonObj = GetComponent<Button>();
        Label = ButtonObj.GetComponentInChildren<TextMeshProUGUI>();
     
        if (ButtonObj != null)
        {
            ButtonObj.onClick.AddListener(HandleButtonClick);
        }
    }
    
    public void ConfigButton(IInventoryItem inventoryItem)
    {
        // Vector3 buttonScale = new Vector3(2,1,1);
        // Vector3 lableScale = new Vector3(.5f, 1, 1);
        // Vector3 lableMoveFactor = new Vector3(0, -1.56f, -.1f);

        // invButtonScale = new Vector3();
        // invLableScale = new Vector3();
        // invLableMoveFactor = new Vector3();

        
        ButtonObj.image.sprite = inventoryItem.PreviewArt;
        //ButtonObj.image.material = inventoryItem.PreviewMaterial;
        Label.text = inventoryItem.ThisName;
        ButtonObj.interactable = inventoryItem.UsedOrPurchase;
        InventoryItemObj = inventoryItem as InventoryItem;
        catagory = inventoryItem.Category;
        if(inventoryItem.GameActionObj != null)
            ButtonObj.onClick.AddListener(inventoryItem.Raise);
        else
        {
            ButtonObj.interactable = true;
        }
        // ButtonObj.transform.localScale = invButtonScale; 
        // Label.transform.localScale = invLableScale;
        // Label.transform.position += invLableMoveFactor;
    }

    private void HandleButtonClick()
    {
        if (InventoryItemObj != null && InventoryItemObj.UsedOrPurchase)
        {
            if (InventoryItemObj.PreviewArt != null)
            {
                if (catagory == "Background")
                {
                    Debug.Log("Background is not null");
                    ButtonObj.image.sprite = InventoryItemObj.PreviewArt;
                    //gameActionSpriteObj.RaiseAction(ButtonObj.image.sprite);
                    spriteRaiseEvent.Invoke();
                }
                
            }

            if (InventoryItemObj.PreviewMaterial != null)
            {
                if (catagory == "Player")
                {
                    Debug.Log("Material is not null");
                    //ButtonObj.image.material = InventoryItemObj.PreviewMaterial;
                    //gameActionMaterialObj.RaiseAction(InventoryItemObj.PreviewMaterial);
                    materialRiseEvent.Invoke();
                }

                if (catagory == "Ball")
                {
                    Debug.Log("Material is not null");
                    //ButtonObj.image.material = InventoryItemObj.PreviewMaterial;
                    //ballMaterialObj.RaiseEvent(InventoryItemObj.PreviewMaterial);
                    ballMaterialEvent.Invoke();
                }

            }

            if (InventoryItemObj.GameArt != null)
            {
                if (catagory == "Coin")
                {
                    Debug.Log("GameArt is not null");
                    IInventoryItem item = InventoryItemObj;
                    //gameActionItemDropObj.RaiseAction(item);
                    ButtonObj.interactable = false;
                    Debug.Log("Art Worked");                    
                }
            }
        }
    }
}