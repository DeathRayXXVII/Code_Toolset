using Scripts.UnityActions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryUIButtonBehaviour : MonoBehaviour
{
    public Button ButtonObj { get; private set; }
    public GameAction gameActionSpriteObj, gameActionMaterialObj, gameActionItemDropObj;
    public UnityEvent spriteRaiseEvent, materialRiseEvent, itemDropRaiseEvent;
    
    public TextMeshProUGUI Label { get; private set; }
    public IInventoryItem InventoryItemObj { get; set; }

    protected virtual void Awake()
    {
        ButtonObj = GetComponent<Button>();
        Label = ButtonObj.GetComponentInChildren<TextMeshProUGUI>();
     
        if (ButtonObj != null)
        {
            ButtonObj.onClick.AddListener(HandleButtonClick);
        }
    }

    private void HandleButtonClick()
    {
        if (InventoryItemObj != null && InventoryItemObj.UsedOrPurchase)
        {
            if (InventoryItemObj.PreviewArt != null)
            {
                Debug.Log("Background is not null");
                ButtonObj.image.sprite = InventoryItemObj.PreviewArt;
                gameActionSpriteObj.Raise(ButtonObj.image.sprite);
                spriteRaiseEvent.Invoke();
            }

            if (InventoryItemObj.PreviewMaterial != null)
            {
                Debug.Log("Material is not null");
                ButtonObj.image.material = InventoryItemObj.PreviewMaterial;
                gameActionMaterialObj.Raise(ButtonObj.image.material);
                materialRiseEvent.Invoke();
            }

            if (InventoryItemObj.GameArt != null)
            {  
                Debug.Log("GameArt is not null");
                IInventoryItem item = InventoryItemObj;
                gameActionItemDropObj.Raise(item);
                ButtonObj.interactable = false;
                Debug.Log("Art Worked");
            }
        }
        //if (InventoryItemObj == null) return;
        //InventoryItemObj.UsedOrPurchase = false;
        //ButtonObj.interactable = false
    }
}