using System;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public float speed = 3.0f;
    public float maxAngle = 45.0f;
    public float minAngle = -45.0f;
    private float rotation = 0.0f;
    private float direction = 1.0f;

    private void Update()
    {
        float angleDifference = Mathf.Min(Mathf.Abs(rotation - maxAngle), Mathf.Abs(rotation - minAngle));
        float speedModifier = Mathf.Clamp(angleDifference / 25.0f, 0.1f, 1.0f); 

        rotation += speed * direction * speedModifier * Time.deltaTime;
        if (rotation > maxAngle)
        {
            rotation = maxAngle;
            direction *= -1;
        }
        else if (rotation < minAngle)
        {
            rotation = minAngle;
            direction *= -1;
        }
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
}
