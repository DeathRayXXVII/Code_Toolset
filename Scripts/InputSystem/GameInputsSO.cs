using UnityEngine;

namespace Scripts.InputSystem
{
    [CreateAssetMenu]
    public class GameInputsSO : ScriptableObject
    {
        public GameInputs gameInputsObj;

        private void OnEnable()
        {
            gameInputsObj = new GameInputs();
        }
    }
}