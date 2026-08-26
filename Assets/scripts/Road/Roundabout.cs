using System.Collections.Generic;
using UnityEngine;

public class Roundabout : MonoBehaviour
{
    [Header("Roundabout Waypoints")]
    public List<Waypoint> roundaboutWaypoints = new List<Waypoint>();

    [Header("Entry Points")]
    public Waypoint northEntry;
    public Waypoint southEntry;
    public Waypoint eastEntry;
    public Waypoint westEntry;

    [Header("Exit Points")]
    public Waypoint northExit;
    public Waypoint southExit;
    public Waypoint eastExit;
    public Waypoint westExit;

    [Header("Settings")]
    public int maxVehiclesInside = 6;

    private List<GameObject> vehiclesInside = new List<GameObject>();

    public bool CanEnter()
    {
        return vehiclesInside.Count < maxVehiclesInside;
    }

    public void VehicleEntered(GameObject vehicle)
    {
        if (!vehiclesInside.Contains(vehicle))
        {
            vehiclesInside.Add(vehicle);
        }
    }

    public void VehicleExited(GameObject vehicle)
    {
        if (vehiclesInside.Contains(vehicle))
        {
            vehiclesInside.Remove(vehicle);
        }
    }

    public int VehicleCount()
    {
        return vehiclesInside.Count;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        for (int i = 0; i < roundaboutWaypoints.Count; i++)
        {
            if (roundaboutWaypoints[i] == null)
                continue;

            Gizmos.DrawWireSphere(
                roundaboutWaypoints[i].transform.position,
                0.25f);

            if (i < roundaboutWaypoints.Count - 1 &&
                roundaboutWaypoints[i + 1] != null)
            {
                Gizmos.DrawLine(
                    roundaboutWaypoints[i].transform.position,
                    roundaboutWaypoints[i + 1].transform.position);
            }
        }

        DrawArrow(northEntry, northExit);
        DrawArrow(southEntry, southExit);
        DrawArrow(eastEntry, eastExit);
        DrawArrow(westEntry, westExit);
    }

    void DrawArrow(Waypoint entry, Waypoint exit)
    {
        if (entry == null || exit == null)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(entry.transform.position, exit.transform.position);
    }
}