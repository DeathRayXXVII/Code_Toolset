using Scripts.UnityActions;
using UnityEngine;

[CreateAssetMenu (fileName = "InventoryItem", menuName = "Store/InventoryItem")]
public class InventoryItem : ScriptableObject, IStoreItem, IInventoryItem
{
    [SerializeField] private string category;
    [SerializeField] private int price;
    //[SerializeField] private bool purchased;
    [SerializeField] private bool own;
    [SerializeField] private Sprite previewArt;
    [SerializeField] private int intLevel;
    [SerializeField] private float floatLevel; // Changed to float
    [SerializeField] private GameObject gameArt;
    [SerializeField] private Material previewMaterial;
    [SerializeField] private bool usedOrPurchase;
    [SerializeField] private GameAction gameActionObj;
    [SerializeField] public float dropChance;

    // IStoreItem and IInventoryItem Implementation
    public string Category { get => category; set => category = value; }
    public int Price { get => price; set => price = value; }
    public bool UsedOrPurchase { get => usedOrPurchase; set => usedOrPurchase = value; }
    public int IntLevel { get => intLevel; set => intLevel = value; }
    public float FloatLevel { get => floatLevel; set => floatLevel = value; } // Corrected type
    public Sprite PreviewArt { get => previewArt; set => previewArt = value; }
    public GameObject GameArt { get => gameArt; set => gameArt = value; }
    public Material PreviewMaterial { get => previewMaterial; set => previewMaterial = value; }
    public float DropChance { get => dropChance; set => dropChance = value; }
    public string ThisName
    {
        get => name; // Directly return the scriptable object's name
        set => name = value;
    }

    public GameAction GameActionObj
    {
        get => gameActionObj;
        set => gameActionObj = value;
    }

    public void Raise()
    {
        if (gameActionObj != null) gameActionObj.Raise();
    }
    
    // public void OnPurchase(InventoryData inventoryDataObj)
    // {
    //     int currentPosition = inventoryDataObj.inventoryDataObjList.IndexOf(this);
    //     
    //     inventoryDataObj.inventory.Remove(this);
    //     
    //     int newPosition = 0;
    //     
    //     inventoryDataObj.inventory.Insert(newPosition, this);
    // }
}