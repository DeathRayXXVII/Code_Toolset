using UnityEngine;

public class Ball: MonoBehaviour
{
    public Rigidbody rb;
    private Vector3 direction;
    private float speed = 500f;
    public vector3Data vector3DataObj;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        ResetBall();
    }

    public void ResetBall()
    {
        transform.position = vector3DataObj.value;
        rb.velocity = Vector3.zero;
        Invoke(nameof(SetRandomTrajectory), 1f);
    }
    
    private void SetRandomTrajectory()
    {
        Vector3 force = Vector3.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1f;
        rb.AddForce(force.normalized * speed);
    }
}
