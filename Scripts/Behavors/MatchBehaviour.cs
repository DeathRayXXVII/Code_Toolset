using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts
{
    public class MatchBehaviour : IDContanerBehavour
    {
        public UnityEvent matchEvent, matchEventDelayed, noMatchEvent, noMatchDelayed;
        public bool isMatched;
        private IEnumerator OnTriggerEnter(Collider other)
        {
            //fecting the otherID from a diffrent script
            var tempObj = other.GetComponent<IDContanerBehavour>();
            //if this is not null get the ID
            if (tempObj == null)
                yield break;
        
            var otherID = tempObj.idObj;
            //checking if the ID's are the same
            if (otherID == idObj)
            {
                matchEvent.Invoke();
                isMatched = true;
                yield return new WaitForSeconds(0.5f);
                matchEventDelayed.Invoke();
            }
            else
            {
                noMatchEvent.Invoke();
                isMatched = false;
                yield return new WaitForSeconds(0.5f);
                noMatchDelayed.Invoke();
            }
        }

        public void OnDisable()
        {
            isMatched = false;
        }
    }
}
