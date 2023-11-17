using UnityEngine;

namespace Scripts.Data
{
    [CreateAssetMenu(fileName = "BallMovementData", menuName = "ScriptableObjects/BallMovementData")]
    public class BallMovementData : ScriptableObject
    {
        public float initialSpeed = 10f;
        public float maxSpeed = 15f;
        public float speedIncreaseRate = 0.1f;
        public Vector2 startingDirection = Vector2.up;
    }
}
