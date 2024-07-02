using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public GameObjectData checkpointPosition;
    public PlayerResponManager playerResponManager;
    public GameObjectData activePoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckPoint(other.gameObject);
        }
    }

    public void CheckPoint(GameObject obj)
    {
        Debug.Log("You have entered the checkpoint");
        checkpointPosition.value = transform.position;
        playerResponManager.startSpawn.value = false;
        
        if (activePoint != null && activePoint != obj)
        {
            activePoint.obj.GetComponent<Renderer>().enabled = false;
        }
        
        activePoint.obj = obj;
        activePoint.obj.GetComponent<Renderer>().enabled = true;
    }
}
