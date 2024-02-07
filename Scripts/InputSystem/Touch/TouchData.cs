using UnityEngine;

namespace Scripts.InputSystem
{
    [CreateAssetMenu]
    public class TouchData : ScriptableObject
    {
        [HideInInspector] public float timeStart, timeEnd, force;
        [HideInInspector] public Vector2 positionStart, positionEnd, direction;
    }
}