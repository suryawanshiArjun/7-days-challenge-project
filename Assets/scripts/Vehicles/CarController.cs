using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 3f;
    public float rotationSpeed = 6f;

    [Header("Starting Waypoint")]
    public Waypoint currentWaypoint;

    private Waypoint targetWaypoint;

    void Start()
    {
        Debug.Log("===== CAR START =====");

        if (currentWaypoint == null)
        {
            Debug.LogError("Current Waypoint is NULL!");
            return;
        }

        Debug.Log("Current Waypoint : " + currentWaypoint.name);

        transform.position = currentWaypoint.transform.position;

        if (currentWaypoint.nextWaypoints.Length == 0)
        {
            Debug.LogError(currentWaypoint.name + " has NO Next Waypoints!");
            return;
        }

        targetWaypoint = currentWaypoint.nextWaypoints[0];

        Debug.Log("Target Waypoint : " + targetWaypoint.name);
    }

    void Update()
    {
        if (targetWaypoint == null)
            return;

        MoveToWaypoint();
    }

    void MoveToWaypoint()
    {
        Vector3 direction = targetWaypoint.transform.position - transform.position;
        direction.z = 0;

        // Rotate towards target
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // Move towards target
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetWaypoint.transform.position,
            speed * Time.deltaTime
        );

        // Reached target
        if (Vector3.Distance(transform.position, targetWaypoint.transform.position) < 0.05f)
        {
            Debug.Log("Reached : " + targetWaypoint.name);

            currentWaypoint = targetWaypoint;

            // ===== DEBUG START =====
            Debug.Log("Current Waypoint = " + currentWaypoint.name);
            Debug.Log("Array Length = " + currentWaypoint.nextWaypoints.Length);

            for (int i = 0; i < currentWaypoint.nextWaypoints.Length; i++)
            {
                if (currentWaypoint.nextWaypoints[i] != null)
                    Debug.Log("Waypoint[" + i + "] = " + currentWaypoint.nextWaypoints[i].name);
                else
                    Debug.Log("Waypoint[" + i + "] = NULL");
            }
            // ===== DEBUG END =====

            if (currentWaypoint.nextWaypoints.Length > 0)
            {
                targetWaypoint = currentWaypoint.nextWaypoints[0];
                Debug.Log("Next Target = " + targetWaypoint.name);
            }
            else
            {
                Debug.LogError("No next waypoint!");
                targetWaypoint = null;
            }
        }
    }
}