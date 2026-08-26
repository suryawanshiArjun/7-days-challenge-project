using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Header("Connected Waypoints")]
    public List<Waypoint> nextWaypoints = new List<Waypoint>();

    [Header("Waypoint Type")]
    public bool isStopPoint = false;
    public bool isIntersection = false;
    public bool isRoundabout = false;
    public bool isEntryPoint = false;
    public bool isExitPoint = false;

    [Header("Traffic Light")]
    public TrafficLight trafficLight;

    [Header("Debug")]
    public bool drawConnections = true;

    //------------------------------------------------------------

    public Waypoint GetNextWaypoint()
    {
        if (nextWaypoints == null || nextWaypoints.Count == 0)
            return null;

        if (nextWaypoints.Count == 1)
            return nextWaypoints[0];

        int randomIndex = Random.Range(0, nextWaypoints.Count);

        return nextWaypoints[randomIndex];
    }

    //------------------------------------------------------------

    public bool HasTrafficLight()
    {
        return trafficLight != null;
    }

    //------------------------------------------------------------

    public bool CanVehiclePass()
    {
        if (!isStopPoint)
            return true;

        if (trafficLight == null)
            return true;

        return trafficLight.currentState == TrafficLight.LightState.Green;
    }

    //------------------------------------------------------------

    private void OnDrawGizmos()
    {
        if (!drawConnections)
            return;

        if (nextWaypoints == null)
            return;

        Gizmos.color = Color.cyan;

        foreach (Waypoint wp in nextWaypoints)
        {
            if (wp == null)
                continue;

            Gizmos.DrawLine(transform.position, wp.transform.position);
        }

        if (isStopPoint)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.12f);
        }

        if (isIntersection)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, 0.10f);
        }

        if (isRoundabout)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 0.10f);
        }

        if (isEntryPoint)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(transform.position, Vector3.one * 0.12f);
        }

        if (isExitPoint)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawCube(transform.position, Vector3.one * 0.12f);
        }
    }
}