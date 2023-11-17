using UnityEngine;
using UnityEngine.Events;

namespace Scripts.UnityActions
{
    [CreateAssetMenu]
    public class GameAction : ScriptableObject
    {
        //public UnityAction raise;
        public UnityAction<object> raise;
        public UnityAction raiseNoArgs;
    
    
        public void Raise()
        {
            raiseNoArgs?.Invoke();
        }
        public void Raise(object obj)
        {
            raise?.Invoke(obj);
        }

        public void Raise(float obj)
        {
            raise?.Invoke(obj);
        }
    
        public void Raise(int obj)
        {
            raise?.Invoke(obj);
        }

        public void Raise(Transform obj)
        {
            raise?.Invoke(obj);
        }
    
        public void Raise(Coroutine obj)
        {
            raise?.Invoke(obj);
        }
        //public void RaiseAction()
        //{
        //raise.Invoke();
        //}
    }
}
