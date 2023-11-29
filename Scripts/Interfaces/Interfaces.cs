using System.Collections.Generic;
using Scripts.UnityActions;
using UnityEngine;

public interface ICollectList<T> where T : Object
{
    int Index { get; set; }
    List<T> CollectionList { get; set; }
    void ConfigureInstance(GameObject instance);
}


public interface IStoreItem
{
    int Price { get; set; }
    bool UsedOrPurchase { get; set; }
    Sprite PreviewArt { get; set; }
    GameObject GameArt { get; set; }
    Material PreviewMaterial { get; set; }
    string ThisName { get; set; }
}


public interface IInventoryItem
{
    bool UsedOrPurchase { get; set; }
    int IntLevel { get; set; }
    float FloatLevel { get; set; }
    Sprite PreviewArt { get; set; }
    GameObject GameArt { get; set; }
    Material PreviewMaterial { get; set; }
    float DropChance { get; set; }
    string ThisName { get; set; }
    string Category { get; set; }
    
    public GameAction GameActionObj { get; set; }
    void Raise();
}

public interface IGameAction<T>
{
    public GameAction GameActionObj { get; set; }
    void Raise(T obj);
}
