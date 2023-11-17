using Scripts.Data;
using UnityEngine;

namespace Scripts
{
    public class BallMovement : MonoBehaviour
    {
        public BallMovementData ballMovementDataObj;
        public Rigidbody rb;
        private Vector3 direction;
        private float speed = 500f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        void Start()
        {
            Invoke(nameof(SetRandomTrajectory), 1f);
            //direction = ballMovementDataObj.startingDirection.normalized;
            //rb.velocity = ballMovementDataObj.startingDirection.normalized * ballMovementDataObj.initialSpeed;
        }
    
        private void SetRandomTrajectory()
        {
            Vector3 force = Vector3.zero;
            force.x = Random.Range(-1f, 1f);
            force.y = -1f;
            rb.AddForce(force.normalized * speed);
        }
    }
}
