using Unity.VisualScripting;
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
        public SpriteRenderer spriteObj;
        public MeshRenderer meshObj;
        public MeshRenderer meshObj1;
        public UnityEvent <IInventoryItem> response;
    
    
        public void Raise()
        {
            raiseNoArgs?.Invoke();
        }
        public void Raise(IInventoryItem item)
        {
            response.Invoke(item);
        }

        public void Raise(Sprite sprite)
        {
            spriteObj.sprite = sprite;
        }
        
        public void Raise(Material texture)
        {
            meshObj.material = texture;
        }
        public void CallMaterial(Material texture)
        {
            meshObj1.material = texture;
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
