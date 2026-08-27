using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CarController : MonoBehaviour
{
    [Header("Movement")]
    [Range(0.05f, 2f)]
    public float moveSpeed = 0.2f;          // Slow speed for demo

    public float rotationSpeed = 5f;
    public float waypointReachDistance = 0.2f;

    [Header("Vehicle Detection")]
    public float checkDistance = 1.2f;
    public LayerMask vehicleLayer;

    [Header("Emergency Vehicle")]
    public bool isEmergencyVehicle = false;

    [Header("Current Waypoint")]
    public Waypoint currentWaypoint;

    private Waypoint targetWaypoint;

    private bool stoppedBySignal = false;
    private bool stoppedByVehicle = false;

    //------------------------------------------------------------

    public void SetWaypoint(Waypoint wp)
    {
        currentWaypoint = wp;
        targetWaypoint = wp;
    }

    //------------------------------------------------------------

    void Update()
    {
        if (targetWaypoint == null)
            return;

        CheckVehicle();
        CheckTrafficSignal();

        if (stoppedByVehicle)
        {
            Debug.Log(gameObject.name + " stopped by vehicle");
            return;
        }

        if (stoppedBySignal)
        {
            Debug.Log(gameObject.name + " stopped by signal");
            return;
        }

        MoveVehicle();
    }

    //------------------------------------------------------------

    void CheckVehicle()
    {
        stoppedByVehicle = false;

        // Ambulance ignores all vehicles
        if (isEmergencyVehicle)
            return;

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            transform.up,
            checkDistance,
            vehicleLayer);

        if (hit.collider == null)
            return;

        if (hit.collider.gameObject == gameObject)
            return;

        stoppedByVehicle = true;
    }

    //------------------------------------------------------------

    void CheckTrafficSignal()
    {
        stoppedBySignal = false;

        // Ambulance ignores all traffic lights
        if (isEmergencyVehicle)
            return;

        if (!targetWaypoint.isStopPoint)
            return;

        if (targetWaypoint.trafficLight == null)
            return;

        TrafficLight light = targetWaypoint.trafficLight;

        if (light.IsRed())
        {
            stoppedBySignal = true;
        }
        else if (light.IsYellow())
        {
            float d = Vector2.Distance(
                transform.position,
                targetWaypoint.transform.position);

            if (d > 0.5f)
                stoppedBySignal = true;
        }
    }

    //------------------------------------------------------------

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

    //------------------------------------------------------------

    void ChooseNextWaypoint()
    {
        if (targetWaypoint.nextWaypoints == null ||
            targetWaypoint.nextWaypoints.Count == 0)
        {
            Destroy(gameObject);
            return;
        }

        targetWaypoint =
            targetWaypoint.nextWaypoints[
                Random.Range(0, targetWaypoint.nextWaypoints.Count)];

        currentWaypoint = targetWaypoint;
    }

    //------------------------------------------------------------

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(
            transform.position,
            transform.position + transform.up * checkDistance);
    }
}