using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InventoryConfigBehaviour : MonoBehaviour
{
    public UnityEvent buttonEvent;
    public InventoryData inventoryDataObj;
    public InventoryUIButtonBehaviour inventoryUIPrefab;
    public StoreUIButtonBehaviour storeUIPrefab;

    private void Start()
    {
        buttonEvent.Invoke();
    }

    public void AddAllInventoryItemsToUI()
    {
        foreach (var item in inventoryDataObj.inventoryDataObjList)
        {
            if (item is not { UsedOrPurchase: true }) continue;
            var element = Instantiate(inventoryUIPrefab.gameObject, transform);
            var elementData = element.GetComponent<InventoryUIButtonBehaviour>();
            elementData.ConfigButton(item);
        }
    }
    
    public void AddAllStoreInventoryItemsToUI()
    {
        foreach (var item in inventoryDataObj.storeDataObjList)
        {
            if (item is not { UsedOrPurchase: false }) continue;
            var element = Instantiate(inventoryUIPrefab.gameObject, transform);
            var elementData = element.GetComponent<StoreUIButtonBehaviour>();
            if (elementData != null)
            {
                elementData.inventoryConfigBehaviour = this;
                elementData.inventoryDataObj = inventoryDataObj;
            } 
            elementData.ConfigButton(item);
        }
    }
    
    
    /*private void AddItemsToUI<T>(List<T> items)
    {
        
        foreach (var item in items)
        {
            GameObject element = null;
            
            if (item is IInventoryItem { UsedOrPurchase: true })
            {
                element = Instantiate(inventoryUIPrefab.gameObject, transform);
            }
            
            if (item is IStoreItem { UsedOrPurchase: false } )
            {
                element = Instantiate(storeUIPrefab.gameObject, transform);
                var storeButton = element.GetComponent<StoreUIButtonBehaviour>();
                if (storeButton != null)
                {
                    storeButton.inventoryConfigBehaviour = this;
                    storeButton.inventoryDataObj = inventoryDataObj;
                }   
            }

            ConfigureElement(element, item);
            
            //var storeButton = element.GetComponent<StoreUIButtonBehaviour>();
            //if (item is not IStoreItem)
            //{
                //storeButton.purchaseEvent.AddListener(UpdateIventoryUI);
            //}
        }
    }

    private void ConfigureElement<T>(GameObject element, T item)
    {
        Vector3 toggelScaleFactor = Vector3.one * 4;
        Vector3 lableMoveFactor = new Vector3(0, -1.56f, -.1f);
        Vector3 toggelMoveFactor = new Vector3(2, .5f, 0);
        Vector3 buttonMoveFactor = new Vector3(0, 0, -.1f);

        if (item is IInventoryItem inventoryItem)
        {
            var elementData = element.GetComponent<InventoryUIButtonBehaviour>();
            if (elementData == null) return;
            elementData.ButtonObj.image.sprite = inventoryItem.PreviewArt;
            elementData.ButtonObj.image.material = inventoryItem.PreviewMaterial;
            elementData.Label.text = inventoryItem.ThisName;
            elementData.ButtonObj.interactable = inventoryItem.UsedOrPurchase;
            elementData.InventoryItemObj = inventoryItem as InventoryItem;
            if(inventoryItem.GameActionObj != null)
                elementData.ButtonObj.onClick.AddListener(inventoryItem.Raise);
            else
            {
                elementData.ButtonObj.interactable = true;
            }
            
            elementData.Label.transform.position += lableMoveFactor;
            elementData.ButtonObj.transform.position += buttonMoveFactor;
        }

        if (item is not IStoreItem storeItem) return;
        {
            var elementData = element.GetComponent<StoreUIButtonBehaviour>();
            if (elementData == null) return;
            elementData.ButtonObj.image.sprite = storeItem.PreviewArt;
            elementData.Label.text = storeItem.ThisName;
            elementData.ButtonObj.interactable = !storeItem.UsedOrPurchase;
            elementData.StoreItemObj = storeItem;
            elementData.ToggleObj.isOn = storeItem.UsedOrPurchase;
            elementData.PriceLabel.text = $"${storeItem.Price}";
            elementData.cash = inventoryDataObj.cash;
            
            elementData.ToggleObj.transform.localScale = toggelScaleFactor;
            elementData.ToggleObj.transform.position += toggelMoveFactor;
            elementData.Label.transform.position += lableMoveFactor;
            elementData.ButtonObj.transform.position += buttonMoveFactor;
        }
    }

    public void AddAllInventoryItemsToUI()
    {
        foreach (var item in inventoryDataObj.inventoryDataObjList)
        {
            if (item is not { UsedOrPurchase: true }) continue;
            var element = Instantiate(inventoryUIPrefab.gameObject, transform);
            var elementData = element.GetComponent<InventoryUIButtonBehaviour>();
            elementData.ConfigButton(item);
        }
    }

    public void AddAllStoreInventoryItemsToUI()
    {
        AddItemsToUI(inventoryDataObj.storeDataObjList);
    }
    
    
    private int ConfigureGameObject(IInventoryItem item, int i)
    {
        if (item.GameActionObj == null || item.GameArt == null) return i;

        var element = Instantiate(item.GameArt, transform);
        var elementData = element.GetComponent<InventoryPrefabItemBehaviour>();
        if (elementData == null) return i;
        elementData.inventoryItemObj = item as InventoryItem;
        elementData.gameActionObj = item.GameActionObj;
        elementData.gameObject.transform.position = Vector3.left * ++i * -3;
        elementData.gameObject.transform.localScale = Vector3.one / 2;
        return i;
    }

 

    public void AddAllInventoryItemsPrefabsToScene()
    {
        var i = inventoryDataObj.inventoryDataObjList.Aggregate(0, (current, item) => ConfigureGameObject(item, current));
    }

    

    public void AddPurchasedInventoryItemsPrefabsToScene()
    {
        var i = 0;
        foreach (var item in inventoryDataObj.storeDataObjList)
        {
            if (!item.UsedOrPurchase || item is not IInventoryItem storeItem ) continue;
            i = ConfigureGameObject(storeItem, i);
        }
    }
    
    public void UpdateIventoryUI()
    {
        //AddAllInventoryItemsToUI();
    }*/
}