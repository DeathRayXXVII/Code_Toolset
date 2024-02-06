using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject startPosition, endPosition;
    public float speed = 1.0f;
    public bool autoStart;
    public bool switchDirection;

    private Vector3 targetPosition;
    private bool isMoving;

    private void Start()
    {
        if (autoStart)
        {
            StartMoving();
        }
    }

    private void StartMoving()
    {
        targetPosition = endPosition.transform.position;
        isMoving = true;
    }
    
    public void ToggleMovePlatform()
    {
        if (!switchDirection)
        {
            switchDirection = true;
        }
        else
        {
            switchDirection = false;
        }
    }

    private void SwitchDirectionOn()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (!switchDirection)
        {
            targetPosition = endPosition.transform.position;
        }
    }

    private void SwitchDirectionOff()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (switchDirection)
        {
            targetPosition = startPosition.transform.position;
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            MovePlatform();
        }

        if (switchDirection)
        {
            SwitchDirectionOff();
        }
        
        if (!switchDirection)
        {
            SwitchDirectionOn();
        }
        
    }

    private void MovePlatform()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            if (targetPosition == endPosition.transform.position)
            {
                targetPosition = startPosition.transform.position;
            }
            else
            {
                targetPosition = endPosition.transform.position;
            }
        }
    }
}
