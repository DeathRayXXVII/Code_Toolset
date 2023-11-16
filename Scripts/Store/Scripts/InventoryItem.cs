using System;
using UnityEngine;

[CreateAssetMenu]
public class InventoryItem : ScriptableObject, IStoreItem
{
    [SerializeField]private int price;
    [SerializeField]private bool purchased;
    [SerializeField]private Sprite previewArt;
    [SerializeField]private Mesh previewMesh;

    int IStoreItem.Price
    {
        get => price;
        set => price = value;
    }

    bool IStoreItem.Purchased
    {
        get => purchased;
        set => purchased = value;
    }

    Sprite IStoreItem.PreviewArt
    {
        get => previewArt;
        set => previewArt = value;
    }

    public string Name { get; set; }

    private void OnEnable()
    {
        Name = name;
    }

    public Sprite GetSprite()
    {
        return previewArt;
    }

    public Mesh GetMesh()
    {
        return previewMesh;
    }
}
