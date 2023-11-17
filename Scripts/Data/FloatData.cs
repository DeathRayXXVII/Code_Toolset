using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Data
{
    //[ExecuteInEditMode]
    [CreateAssetMenu(menuName = "Single Variables/FloatData")]
    public class FloatData : ScriptableObject
    {
        public float value;
        public UnityEvent minValueEvent, maxValueEvent, updateValueEvent;
    
        public void SetValue (float amount)
        {
            value = amount;
            updateValueEvent.Invoke();
        }

        public void UpdateValue(float amount)
        {
            value += amount;
            updateValueEvent.Invoke();
        }
    

        public void IncrementValue()
        {
            value ++;
        }
        public void CompareValue(FloatData obj)
        {
            if (value >= obj.value)
            {
            
            }
            else
            {
                value = obj.value;
            }
        }
    
        public void UpdateValue(FloatData data)
        {
            if (data != null) value += data.value;
        }

        public void SetValue(FloatData data)
        {
            if (data != null) value = data.value;
        }
    
        public void CheckMinValue(float minValue)
        {
            if (!(value < minValue)) return;
            minValueEvent.Invoke();
            value = minValue;
        }

        public void CheckMaxValue(float maxValue)
        {
            if (!(value >= maxValue)) return;
            maxValueEvent.Invoke();
            value = maxValue;
        }
    }
}