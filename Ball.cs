using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Ball: MonoBehaviour
{
    public Rigidbody rb;
    private Vector3 direction;
    public float speed = 10f;
    public float maxSpeed;
    public vector3Data vector3DataObj;
    private bool goingLeft;
    private bool goingDown;
    public UnityEvent ballEvent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        ResetBall();
    }

    public void ResetBall()
    {
        rb.velocity = Vector3.zero;
        transform.position = Vector3.zero;

        Invoke(nameof(SetRandomTrajectory), 1f);
    }

    private void SetRandomTrajectory()
    {
        Vector3 force = new Vector3();
        force.x = Random.Range(-1f, 1f);
        force.y = -1f;

        rb.AddForce(force.normalized * speed, ForceMode.Impulse);
    }

    public void OnCollisionEnter(Collision other)
    {
        ballEvent.Invoke();
    }

    private void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * speed;
    }
}