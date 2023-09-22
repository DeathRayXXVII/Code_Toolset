using UnityEngine;
using UnityEngine.Events;

public class CollisionEventBehaviour : MonoBehaviour
{
    public UnityEvent collisionEvent;
    
    private void OnCollisionEnter(Collision other)
    {
        collisionEvent.Invoke();
    }
}
