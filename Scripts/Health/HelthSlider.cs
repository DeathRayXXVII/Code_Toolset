using Scripts.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Scripts
{
    public class HelthSlider : MonoBehaviour
    {
        public UnityEvent StartEvent;
        public Slider slider;

        private void Start()
        {
            slider = GetComponent<Slider>();
            StartEvent.Invoke();
        }

        public void UpdateSlider(FloatData obj)
        {
            slider.value = obj.value;
        }
    }
}
