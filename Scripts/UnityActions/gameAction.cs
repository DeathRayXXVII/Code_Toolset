using UnityEngine;

namespace Scripts.UnityActions
{
    [CreateAssetMenu]
    public class GameAction : ScriptableObject
    {
        public delegate void GameActionEvent(GameAction action);
        public delegate void GameObjectEvent(object obj);
        public event GameActionEvent RaiseEvent;
        public event GameObjectEvent RaiseEventObj;
        public void RaiseAction() => RaiseEvent?.Invoke(this);
        public void RaiseAction(object obj) => RaiseEventObj?.Invoke(obj);
    }
}
