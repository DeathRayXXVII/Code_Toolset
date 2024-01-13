using UnityEngine;
using UnityEngine.Events;

namespace Scripts
{
  public class IDContanerBehavour : MonoBehaviour
  {
    public ID idObj;
    public UnityEvent startEvent;

    public void Start()
    {
      startEvent.Invoke();
    }
  }
}
