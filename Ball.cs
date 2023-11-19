using Scripts.Data;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Ball: MonoBehaviour
{
    public Rigidbody rb;
    private Vector3 direction;
    public FloatData speed ;
    public FloatData maxSpeed;
    public vector3Data vector3DataObj;
    private bool goingLeft;
    private bool goingDown;
    public UnityEvent ballEvent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ResetBall()
    {
        speed.value = maxSpeed.value;
        gameObject.SetActive(true);
        rb.velocity = Vector3.zero;
        transform.position = new Vector3(0, -4, 0);

        Invoke(nameof(SetRandomTrajectory), 1f);
    }

    private void SetRandomTrajectory()
    {
        Vector3 force = new Vector3();
        force.x = Random.Range(-1f, 1f);
        force.y = -1f;

        rb.AddForce(force.normalized * speed.value, ForceMode.Impulse);
    }

    public void OnCollisionEnter(Collision other)
    {
        ballEvent.Invoke();
    }

    private void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * speed.value;
    }
}
