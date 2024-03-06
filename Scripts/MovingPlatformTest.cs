using System;
using System.Collections;
using UnityEngine;

public class MovingPlatformTest : MonoBehaviour
{
    [SerializeField]
    private WaypointPath waypointPath;
    [SerializeField]
    private float speed;
    public bool autoStart;
    public bool switchDirection;
    public bool isMoving;
    public float delay = 2.5f;
    private WaitForSeconds waitObj;
    
    private int targetWaypointIndex;
    private Transform targetWaypoint;
    private Transform currentWaypoint;
    private float timeToWaypoint;
    private float elapsedTime;

    private void Start()
    {
        waitObj = new WaitForSeconds(delay);
        TargetNextWaypoint();
        
        if (autoStart)
        {
            isMoving = true;
        }
    }
    
    private void FixedUpdate()
    {
        if (isMoving)
        {
            elapsedTime += Time.deltaTime;
            float elapsedPercentage = elapsedTime / timeToWaypoint;
            elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
            transform.position = Vector3.Lerp(currentWaypoint.position, targetWaypoint.position, elapsedPercentage);
            transform.rotation = Quaternion.Lerp(currentWaypoint.rotation, targetWaypoint.rotation, elapsedPercentage);
            if (elapsedTime >= timeToWaypoint)
            {
                StartCoroutine(MoveDelay());
            }
        }
        if (switchDirection && !isMoving)
        {
            elapsedTime += Time.deltaTime;
            float elapsedPercentage = elapsedTime / timeToWaypoint;
            elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
            transform.position = Vector3.Lerp(currentWaypoint.position, targetWaypoint.position, elapsedPercentage);
            transform.rotation = Quaternion.Lerp(currentWaypoint.rotation, targetWaypoint.rotation, elapsedPercentage);
            if (elapsedTime >= timeToWaypoint)
            {
                switchDirection = false;
                TargetNextWaypoint();
            }
        }
        
    }
    
    private IEnumerator MoveDelay()
    {
        isMoving = false;
        yield return waitObj;
        isMoving = true;
        TargetNextWaypoint();
    }
    
    public void ToggleMovePlatform()
    {
        if (!switchDirection)
        {
            switchDirection = true;
        }
    }
    
    public void StartMoving()
    {
        isMoving = true;
    }

    private void TargetNextWaypoint()
    {
        currentWaypoint = waypointPath.GetWaypoint(targetWaypointIndex);
        targetWaypointIndex = waypointPath.GetNextWaypointIndex(targetWaypointIndex);
        targetWaypoint = waypointPath.GetWaypoint(targetWaypointIndex);
        
        elapsedTime = 0;
        timeToWaypoint = Vector3.Distance(currentWaypoint.position, targetWaypoint.position) / speed;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
    }
    
    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }
}
