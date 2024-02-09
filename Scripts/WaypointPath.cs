using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    public Transform GetWaypoint(int waypoint)
    {
        return transform.GetChild(waypoint);
    }
    
    public int GetNextWaypointIndex(int currentWaypoint)
    {
        int nextWaypoint = currentWaypoint + 1;
        if (nextWaypoint == transform.childCount)
        {
            nextWaypoint = 0;
        }
        return nextWaypoint;
    }
}
