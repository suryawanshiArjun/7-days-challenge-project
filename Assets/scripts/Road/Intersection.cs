using UnityEngine;

public class Intersection : MonoBehaviour
{
    [Header("Traffic Signal")]
    public TrafficSignalController signalController;

    [Header("Entry Waypoints")]
    public Waypoint northEntry;
    public Waypoint southEntry;
    public Waypoint eastEntry;
    public Waypoint westEntry;

    [Header("Exit Waypoints")]
    public Waypoint northExit;
    public Waypoint southExit;
    public Waypoint eastExit;
    public Waypoint westExit;

    [Header("Intersection Settings")]
    public bool allowRandomRouting = true;

    public TrafficLight GetTrafficLight(Waypoint entry)
    {
        if (signalController == null)
            return null;

        if (entry == northEntry)
            return signalController.northLight;

        if (entry == southEntry)
            return signalController.southLight;

        if (entry == eastEntry)
            return signalController.eastLight;

        if (entry == westEntry)
            return signalController.westLight;

        return null;
    }

    public Waypoint GetExit(Waypoint entry)
    {
        if (entry == northEntry)
            return northExit;

        if (entry == southEntry)
            return southExit;

        if (entry == eastEntry)
            return eastExit;

        if (entry == westEntry)
            return westExit;

        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, new Vector3(3f, 3f, 0f));

        DrawConnection(northEntry, northExit);
        DrawConnection(southEntry, southExit);
        DrawConnection(eastEntry, eastExit);
        DrawConnection(westEntry, westExit);
    }

    void DrawConnection(Waypoint from, Waypoint to)
    {
        if (from == null || to == null)
            return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(from.transform.position, to.transform.position);
    }
}