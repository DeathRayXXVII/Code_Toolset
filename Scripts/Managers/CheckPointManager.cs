using Scripts.Data;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public vector3Data respawnPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("You have entered the checkpoint");
            respawnPosition.value = transform.position;
        }
    }
}
