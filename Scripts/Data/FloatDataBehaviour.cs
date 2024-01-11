using Scripts.Data;
using UnityEngine;

namespace Scripts
{
   public class FloatDataBehaviour : MonoBehaviour
   {
      public float value;
      public FloatData floatDataObj;

      public void UpdateValue(FloatData obj)
      {
         value += obj.value;
      }
      public void UpdateFloatData(float number)
      {
         floatDataObj.UpdateValue(number);
      }

      private void OnTriggerEnter(Collider other)
      {
         var newObj = other.GetComponent<FloatDataContainer>().DataObj;
         UpdateValue(newObj);
      }
   }
}
