using System;
using Scripts;
using Scripts.Data;
using Scripts.UnityActions;
using UnityEngine;
using UnityEngine.Events;

public class Materialbehaviour : MonoBehaviour
{
    private MeshRenderer rendererObj;
    public GameAction gameActionObj;
    public UnityEvent raiseEvent;

    private void Awake()
    {
        rendererObj = GetComponent<MeshRenderer>();
        if (gameActionObj == null)
        {
            return;
        }
        gameActionObj.raiseNoArgs += Raise;
    }
    
    private void Raise()
    {
        raiseEvent.Invoke();
    }

    public void ChangeRendererColor(ColorID obj)
    {
        rendererObj.material = obj.material;
    }

    public void ChangeRendererColor(ColorIDDataList obj)
    {
        rendererObj.sharedMaterial = obj.currentColor.material;
    }
    
    public void ChangeMeshRenderer()
    {
        //rendererObj.sprite = gameActionObj.spriteObj.sprite;
        rendererObj.sharedMaterial = gameActionObj.meshObj.sharedMaterial;
    }
}
