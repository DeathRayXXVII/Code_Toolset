using UnityEngine;

[CreateAssetMenu(menuName = "Single Cariables/GameObjectData")]
public class GameObjectData : ScriptableObject
{
    public GameObject obj;
    public Renderer rend;
    public Vector3 value;
    
    public void SetObj(GameObject setObj)
    {
        this.obj = setObj;
        rend = setObj.GetComponent<Renderer>();
    }
}
