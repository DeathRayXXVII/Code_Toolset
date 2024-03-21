using System.Collections;
using UnityEngine;

namespace Scripts
{
    public class DestroyBehavour : MonoBehaviour
    {
        public float seconds = 1;
        public bool destroyOnStart;
        private WaitForSeconds wfsObj;
        
        [Header("If Its A Particles")]
        private ParticleSystem thisPatricleSystem;
        public bool destroyParticle;
        private void Start()
        {
            if (destroyOnStart == true)
            {
                StartCoroutine(DestroyTimer());
            }

            if (destroyParticle)
            {
                thisPatricleSystem = GetComponent<ParticleSystem>();
            }
        }
        
        private void Update()
        {
            if (thisPatricleSystem.isPlaying)
                return;

            Destroy(gameObject);
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
