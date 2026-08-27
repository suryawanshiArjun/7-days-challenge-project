using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AmbulanceController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 1.2f;
    public float rotationSpeed = 8f;
    public float waypointReachDistance = 0.25f;

    [Header("Current Waypoint")]
    public Waypoint currentWaypoint;

    private Waypoint targetWaypoint;

    //--------------------------------------------------

    void Start()
    {
        Debug.Log("AmbulanceController Started");
    }

    //--------------------------------------------------

    public void SetWaypoint(Waypoint wp)
    {
        if (wp == null)
        {
            Debug.LogError("Waypoint is NULL!");
            return;
        }

        currentWaypoint = wp;
        targetWaypoint = wp;

        Debug.Log("Waypoint Assigned : " + wp.name);
    }

    //--------------------------------------------------

    void Update()
    {
        Debug.Log("Ambulance Update Running");

        if (targetWaypoint == null)
        {
            Debug.LogWarning("Target Waypoint is NULL");
            return;
        }

        MoveVehicle();
    }

    //--------------------------------------------------

    void MoveVehicle()
    {
        Vector3 targetPos = targetWaypoint.transform.position;
        targetPos.z = transform.position.z;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            moveSpeed * Time.deltaTime);

        Vector3 direction = targetPos - transform.position;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(Vector3.forward, direction);

            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, targetPos) <= waypointReachDistance)
        {
            ChooseNextWaypoint();
        }
    }

    //--------------------------------------------------

    void ChooseNextWaypoint()
    {
        if (targetWaypoint == null)
            return;

        if (targetWaypoint.nextWaypoints == null ||
            targetWaypoint.nextWaypoints.Count == 0)
        {
            Debug.Log("No more waypoints. Destroying ambulance.");
            Destroy(gameObject);
            return;
        }

        targetWaypoint = targetWaypoint.nextWaypoints[
            Random.Range(0, targetWaypoint.nextWaypoints.Count)];

        currentWaypoint = targetWaypoint;

        Debug.Log("Next Waypoint : " + targetWaypoint.name);
    }
}