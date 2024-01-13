using UnityEngine;
using UnityEngine.Events;

namespace Scripts
{
    public class ColliderBehaviour : MonoBehaviour
    {
        private Collider colliderObj;
        public UnityEvent startEvent, triggerEnterEvent;
        protected virtual void Start()
        {
            colliderObj = GetComponent<Collider>();
            colliderObj.isTrigger = true;
            startEvent.Invoke();
        }
    
        private void OnTriggerEnter(Collider other)
        {
            triggerEnterEvent.Invoke();
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            throw new System.NotImplementedException();
        }
    }
}
