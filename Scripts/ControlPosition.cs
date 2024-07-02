using UnityEngine;

public class ControlPosition : MonoBehaviour
{
    public Transform target;
    
    public void NewPosition()
    {
        transform.position = target.position;
    }
}
