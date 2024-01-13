using System;
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
        gameActionObj.raiseNoArgs += Raise;
    }
    
    private void Raise()
    {
        raiseEvent.Invoke();
    }
    
    public void ChangeMeshRenderer()
    {
        //rendererObj.sprite = gameActionObj.spriteObj.sprite;
        rendererObj.sharedMaterial = gameActionObj.meshObj.sharedMaterial;
    }
}
