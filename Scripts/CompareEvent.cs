using Scripts.Data;
using UnityEngine;
using UnityEngine.Events;

public class CompareEvent : MonoBehaviour
{
    public IntData intData;
    public int num;
    public UnityEvent compareTrueEvent;
    
    void Start()
    {
        if (intData.value < num)
        {
            Debug.Log("The value is less than the number");
            return;
        }
        if (intData.value >= num)
        {
            compareTrueEvent.Invoke();
        }
    }
    public void CompareValue()
    {
        if (intData.value >= num)
        {
            compareTrueEvent.Invoke();
        }
    }
}