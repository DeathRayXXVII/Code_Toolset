using Scripts.Data;
using Scripts.UnityActions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts
{
  [RequireComponent(typeof(Image))]
  public class ImageBehaviour : MonoBehaviour
  {
    public GameAction GameActionObj;
    private Image imageObj;
    public GameAction updateAction;
    public UnityEvent startEvent, updateImageEvent;
    
    private void Start()
    {
      imageObj = GetComponent<Image>();
      updateAction.raiseNoArgs += OnUpdate;
      startEvent.Invoke();
    }

    public void OnUpdate()
    {
      updateImageEvent.Invoke();
    }

    public void UpdateWithFloatData(FloatData dataObj)
    {
      imageObj.fillAmount = dataObj.value;
    }

    private void RunStartEvent()
    {
      startEvent.Invoke();
    }

    public void UpdateImage(FloatData obj)
    {
      imageObj.fillAmount = obj.value;
    }
  }
}
