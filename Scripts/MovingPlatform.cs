using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject minPosition, maxPosition;
    public float speed = 1.0f;
    public bool autoStart;
    public float seconds = 1.0f;

    private Vector3 targetPosition;
    private bool isMoving;

    private void Start()
    {
        if (autoStart)
        {
            StartMoving();
        }
    }

    public void StartMoving()
    {
        targetPosition = maxPosition.transform.position;
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            MovePlatform();
        }
    }

    private void MovePlatform()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            if (targetPosition == maxPosition.transform.position)
            {
                targetPosition = minPosition.transform.position;
            }
            else
            {
                targetPosition = maxPosition.transform.position;
            }
        }
    }
}
