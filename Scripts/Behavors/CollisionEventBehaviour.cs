using UnityEngine;
using UnityEngine.Events;

namespace Scripts
{
    public class CollisionEventBehaviour : MonoBehaviour
    {
        public UnityEvent collisionEvent, playerCollisionEvent;
    
        public LayerMask player;
    
        private void OnCollisionEnter(Collision other)
        {
        
        
            if (other.gameObject.layer == player.value)
            {
                Debug.Log("Player has collided");
                playerCollisionEvent.Invoke();
            }
        
            collisionEvent.Invoke();
        }
    }
}
