using System.Collections.Generic;
using Scripts.Data;
using UnityEngine;
using UnityEngine.Events;

public class TriggerCompareEvent : MonoBehaviour
{
    public List<IntData> iData;
    public List<int> iValue;
    public List<FloatData> fData;
    public List<float> fValue;
    public UnityEvent compareEvent;
    
    public void Compare()
    {
        for (var i = 0; i < iData.Count; i++)
        {
            if (iData[i].value == iValue[i])
            {
                compareEvent.Invoke();
            }
        }
        for (var i = 0; i < fData.Count; i++)
        {
            if (fData[i].value == fValue[i])
            {
                compareEvent.Invoke();
            }
        }
    }
}
