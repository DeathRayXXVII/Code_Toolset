using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject startPosition, endPosition;
    public float speed = 1.0f;
    public bool autoStart;
    public bool switchDirection;
    public float delay = 2.5f;

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

    private IEnumerator SwitchDirectionOn()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (!switchDirection)
        {
            yield return new WaitForSeconds(delay);
            targetPosition = endPosition.transform.position;
        }
    }

    private IEnumerator SwitchDirectionOff()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (switchDirection)
        {
            yield return new WaitForSeconds(delay);
            targetPosition = startPosition.transform.position;
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            StartCoroutine(MovePlatform());
        }

        if (switchDirection)
        {
            StartCoroutine(SwitchDirectionOff());
        }
        
        if (!switchDirection)
        {
            StartCoroutine(SwitchDirectionOn());
        }
        
    }

    private IEnumerator MovePlatform()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            if (targetPosition == endPosition.transform.position)
            {
                yield return new WaitForSeconds(delay);
                targetPosition = startPosition.transform.position;
            }
            else
            {
                yield return new WaitForSeconds(delay);
                targetPosition = endPosition.transform.position;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != null && other.gameObject.CompareTag("Player"))
        {
            if (other.transform != null)
            {
                other.transform.parent = transform;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != null && other.gameObject.CompareTag("Player"))
        {
            if (other.transform != null)
            {
                other.transform.parent = null;
            }
        }
    }
}