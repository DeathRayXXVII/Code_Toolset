using Scripts.UnityActions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryUIButtonBehaviour : MonoBehaviour
{
    public Button ButtonObj { get; private set; }
    public GameAction gameActionSpriteObj, gameActionMaterialObj;
    public UnityEvent spritRaiseEvent, materialRiseEvent;
    public TextMeshProUGUI Label { get; private set; }
    [SerializeField] private Sprite backgroundImage;
    [SerializeField] private Material backgroundMaterial;
    
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
                ButtonObj.image.sprite = InventoryItemObj.PreviewArt;
                gameActionSpriteObj.ExecuteAction(ButtonObj.image.sprite);
                spritRaiseEvent.Invoke();
            }

            if (InventoryItemObj.PreviewMaterial != null)
            {
                ButtonObj.image.material = InventoryItemObj.PreviewMaterial;
                gameActionMaterialObj.ExecuteAction(ButtonObj.image.material);
                materialRiseEvent.Invoke();
            }
        }
        //if (InventoryItemObj == null) return;
        //InventoryItemObj.UsedOrPurchase = false;
        //ButtonObj.interactable = false
    }
}