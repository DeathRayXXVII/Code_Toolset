using System.Collections;
using Scripts.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts
{
    public class CoritineBehaviour : MonoBehaviour
    {
        public UnityEvent startEvent, startCountEvent, repeatCountEvent, endCountEvent, repeatUntilFalseEvent;

        public bool canRun;
        public FloatData counterNum;
        public float seconds = 3.0f;
        private WaitForSeconds wfsObj;

        private bool CanRun
        {
            get => canRun;
            set => canRun = value;
        }


        private void Start()
        {
            wfsObj = new WaitForSeconds(seconds);
            startEvent.Invoke();
        }

        public void StartCounting()
        {
        
            StartCoroutine(Counting());
        }
        private IEnumerator Counting()
        {
            startCountEvent.Invoke();
            yield return wfsObj;
            while (counterNum.value > 0)
            {
                repeatCountEvent.Invoke();
                yield return wfsObj;
                counterNum.value--;
            }
            endCountEvent.Invoke();
        }

        public void StartRepeatUntilFalse()
        {
            CanRun = true;
            StartCoroutine(RepeatUntilFalse());
        }
        private IEnumerator RepeatUntilFalse()
        {
            
            while(CanRun)
            {
                yield return wfsObj;
                repeatUntilFalseEvent.Invoke();
            }
        }
        
        
    }
}
