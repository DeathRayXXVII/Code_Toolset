using UnityEngine;

public class ControlRotation : MonoBehaviour
{
    public float rotationSpeed = 4f;
    public bool rotateX, rotateY, rotateZ;
    private bool x, y, z;
    public Transform obj;
    
    public void Start()
    {
        obj = GetComponent<Transform>();
        /*if (rotateX)
        {
            x = true;
        }
        if (rotateY)
        {
            y = true;
        }
        if (rotateZ)
        {
            z = true;
        }*/
    }
    void Update()
    {
        if (rotateX)
        {
            obj.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        }
        if (rotateY)
        {
            obj.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        if (rotateZ)
        {
            obj.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
}
