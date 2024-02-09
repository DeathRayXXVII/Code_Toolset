using System;
using UnityEngine;

public class MovingPlatformTest : MonoBehaviour
{
    [SerializeField]
    private WaypointPath waypointPath;
    [SerializeField]
    private float speed;
    private int targetWaypointIndex;
    private Transform targetWaypoint;
    private Transform currentWaypoint;
    private float timeToWaypoint;
    private float elapsedTime;

    private void Start()
    {
        TargetNextWaypoint();
    }
    
    private void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;
        float elapsedPercentage = elapsedTime / timeToWaypoint;
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
        transform.position = Vector3.Lerp(currentWaypoint.position, targetWaypoint.position, elapsedPercentage);
        transform.rotation = Quaternion.Lerp(currentWaypoint.rotation, targetWaypoint.rotation, elapsedPercentage);
        if (elapsedTime >= timeToWaypoint)
        {
            TargetNextWaypoint();
        }
    }

    private void TargetNextWaypoint()
    {
        currentWaypoint = waypointPath.GetWaypoint(targetWaypointIndex);
        targetWaypointIndex = waypointPath.GetNextWaypointIndex(targetWaypointIndex);
        targetWaypoint = waypointPath.GetWaypoint(targetWaypointIndex);
        
        elapsedTime = 0;
        //float distanceToWaypoint = Vector3.Distance(currentWaypoint.position, targetWaypoint.position);
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
