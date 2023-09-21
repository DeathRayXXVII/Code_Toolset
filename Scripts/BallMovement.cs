using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public BallMovementData ballMovementDataObj;
    public Rigidbody rb;
    private Vector3 direction;
    private float speed;

    //public bool isWall;
    //public LayerMask whatIsWall;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //direction = ballMovementDataObj.startingDirection.normalized;
        rb.velocity = ballMovementDataObj.startingDirection.normalized * ballMovementDataObj.initialSpeed;
    }

    private void Update()
    {
        
    }
    
}
