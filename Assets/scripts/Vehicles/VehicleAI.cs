using UnityEngine;

[RequireComponent(typeof(VehicleSensor))]
public class VehicleAI : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2f;              // slower
    public float rotationSpeed = 5f;
    public float reachDistance = 0.15f;

    [Header("Route")]
    public Waypoint currentWaypoint;

    private VehicleSensor sensor;

    void Start()
    {
        sensor = GetComponent<VehicleSensor>();
    }

    void Update()
    {
        if (currentWaypoint == null)
            return;

        // Stop if another vehicle is ahead
        if (sensor != null && sensor.IsVehicleAhead())
            return;

        // Check traffic signal BEFORE entering stop point
        if (currentWaypoint.isStopPoint &&
            currentWaypoint.trafficLight != null)
        {
            TrafficLight light = currentWaypoint.trafficLight;

            if (light.IsRed())
                return;

            if (light.IsYellow())
            {
                float distance = Vector2.Distance(
                    transform.position,
                    currentWaypoint.transform.position);

                if (distance > 0.4f)
                    return;
            }
        }

        MoveVehicle();
    }

    void MoveVehicle()
    {
        Vector3 target = currentWaypoint.transform.position;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime);

        Vector3 direction = target - transform.position;

        if (direction != Vector3.zero)
        {
            float angle =
                Mathf.Atan2(direction.y, direction.x) *
                Mathf.Rad2Deg - 90f;

            Quaternion targetRotation =
                Quaternion.Euler(0, 0, angle);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, target) <= reachDistance)
        {
            Waypoint next = currentWaypoint.GetNextWaypoint();

            if (next == null)
            {
                Destroy(gameObject);
                return;
            }

            currentWaypoint = next;
        }
    }

    public void SetWaypoint(Waypoint wp)
    {
        currentWaypoint = wp;
    }
}