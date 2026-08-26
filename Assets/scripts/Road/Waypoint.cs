using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint[] nextWaypoints;

    public Waypoint GetNextWaypoint()
    {
        if (nextWaypoints.Length == 0)
            return null;

        return nextWaypoints[Random.Range(0, nextWaypoints.Length)];
    }
}