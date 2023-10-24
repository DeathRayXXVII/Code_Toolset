using UnityEngine;
using UnityEngine.Events;

public class CollisionEventBehaviour : MonoBehaviour
{
    public UnityEvent collisionEvent, playerCollisionEvent;
    
    public LayerMask player;
    
    private void OnCollisionEnter(Collision other)
    {
        
        
        if (player == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("You have entered");
        }
        
        collisionEvent.Invoke();
    }
}
