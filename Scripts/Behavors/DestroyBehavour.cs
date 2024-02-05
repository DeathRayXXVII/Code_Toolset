using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts
{
    public class DestroyBehavour : MonoBehaviour
    {
        public float seconds = 1;
        public bool destroyOnStart;
        private WaitForSeconds wfsObj;
        private void Start()
        {
            if (destroyOnStart == true)
            {
                StartCoroutine(DestroyTimer());
            }
        }
        
        public void DestroyTimerStart()
        {
            StartCoroutine(DestroyTimer());
        }

        private IEnumerator DestroyTimer()
        {
            wfsObj = new WaitForSeconds(seconds); 
            yield return wfsObj;
            Destroy(gameObject); //destroying itself
        }
        public void Destroy()
        {
            Destroy(gameObject);
        }
    

    }
}
