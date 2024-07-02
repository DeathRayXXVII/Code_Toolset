using UnityEngine;

[CreateAssetMenu(menuName = "Items/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public GameObject item;
    public bool collected;
    public float dropChance;
    public float addCoin;
}
