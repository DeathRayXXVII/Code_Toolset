using System.Collections.Generic;
using Scripts.UnityActions;
using UnityEngine;

public class IteamDrop : MonoBehaviour
{
    public List <InventoryItem> itemPrefabs;
    public InventoryData itemPrefabsObj;
    public GameAction gameAction;
    public float skipChance = .98f;
    public Brick brickObj;
    
    
    private void Awake()
    {
        gameAction.response.AddListener(AddToList);
    }
   private void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Ball") &&  brickObj.unbreakable != true)
    {
        if (Random.value < skipChance)
        {
            return;
        }
        // Create a cumulative list of drop chances
        List<float> cumulative = new List<float>();
        float total = 0;
        if (itemPrefabsObj != null && itemPrefabsObj.inventory != null)
        { 
            foreach (var item in itemPrefabsObj.inventory)
            {
                if (item is IInventoryItem inventoryItem)
                {
                    total += inventoryItem.DropChance;
                    cumulative.Add(total);
                }
            }
        }
        // Generate a random number
        float random = Random.value * total;

        // Find the first item where the cumulative drop chance is greater than the random number
        for (int i = 0; i < cumulative.Count; i++)
        {
            if (random < cumulative[i])
            {
                // Instantiate the item's GameObject
                if (itemPrefabsObj.inventory[i] is IInventoryItem inventoryItem)
                {
                    Instantiate(inventoryItem.GameArt, transform.position, Quaternion.identity);
                    i = 0;
                }
                break;
            }
        }
    }
}
   public void AddToList(IInventoryItem item)
{

    if (item is InventoryItem inventoryItem)
    {
        if (!itemPrefabs.Contains(inventoryItem))
        {
            itemPrefabs.Add(inventoryItem);
        }
        
    }
}
   public void RemoveFromList(IInventoryItem item)
   {
       if (item is InventoryItem inventoryItem)
       {
           itemPrefabs.Remove(inventoryItem);
       }
   }
}
