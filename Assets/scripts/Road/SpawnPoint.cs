using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [Header("Route")]
    public Waypoint firstWaypoint;

    [Header("Spawn Settings")]
    public bool canSpawn = true;

    public int maxVehicles = 10;

    public float spawnCheckRadius = 2f;

    [HideInInspector]
    public float timer = 0f;

    [HideInInspector]
    public int currentVehicles = 0;

    public bool CanSpawnVehicle()
    {
        if (!canSpawn)
            return false;

        if (firstWaypoint == null)
            return false;

        if (currentVehicles >= maxVehicles)
            return false;

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            spawnCheckRadius);

        foreach (Collider2D hit in hits)
        {
            if (hit.GetComponent<CarController>() != null)
                return false;
        }

        return true;
    }

    public void VehicleSpawned()
    {
        currentVehicles++;
    }

    public void VehicleDestroyed()
    {
        currentVehicles--;

        if (currentVehicles < 0)
            currentVehicles = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spawnCheckRadius);

        if (firstWaypoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, firstWaypoint.transform.position);
            Gizmos.DrawSphere(firstWaypoint.transform.position, 0.2f);
        }
    }
}