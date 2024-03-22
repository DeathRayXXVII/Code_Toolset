using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DelayAction : MonoBehaviour
{
    public float delay = 2.5f;
    public UnityEvent firstDelayEvent, secondDelayEvent;
    private WaitForSeconds waitObj;
    
    // private void Start()
    // {
    //     waitObj = new WaitForSeconds(delay);
    // }
    
    public void Delay()
    {
        StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay()
    {
        firstDelayEvent.Invoke();
        yield return new WaitForSeconds(delay);
        secondDelayEvent.Invoke();
    }
}
