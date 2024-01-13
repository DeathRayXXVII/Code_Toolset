using Scripts.Data;
using Scripts.UnityActions;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteBehaviour : MonoBehaviour
    {
        private SpriteRenderer rendererObj;
        public GameAction gameActionObj;
        public UnityEvent raiseEvent;

        // Start is called before the first frame update
        void Awake()
        {
            rendererObj = GetComponent<SpriteRenderer>();
            gameActionObj.raiseNoArgs += Raise;
        }
        private void Raise()
        {
            raiseEvent.Invoke();
        }

        public void ChangeRendererColor(ColorID obj)
        {
            rendererObj.color = obj.value;
        }

        public void ChangeRendererColor(ColorIDDataList obj)
        {
            rendererObj.color = obj.currentColor.value;
        }
        
        public void ChangeRenererSprite()
        {
            rendererObj.sprite = gameActionObj.spriteObj.sprite;
        }
    }
}

