using System.Collections;
using UnityEngine;

namespace Scripts
{
    public class DestroyBehavour : MonoBehaviour
    {
        public float seconds = 1;

        private WaitForSeconds wfsObj;
        private IEnumerator Start()
        {
            wfsObj = new WaitForSeconds(seconds); 
            yield return wfsObj;
            Destroy(gameObject); //destroying itself
        }
    
    

    }
}
