using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    [Header("Vehicle Prefabs")]
    public GameObject[] vehiclePrefabs;

    [Header("Spawn Points")]
    public SpawnPoint[] spawnPoints;

    [Header("Traffic Settings")]
    [Range(1, 10)]
    public int trafficDensity = 5;

    public int maxVehicles = 50;

    private int activeVehicles = 0;

    void Update()
    {
        if (vehiclePrefabs.Length == 0 || spawnPoints.Length == 0)
            return;

        foreach (SpawnPoint spawn in spawnPoints)
        {
            if (spawn == null)
                continue;

            spawn.timer += Time.deltaTime;

            float interval = GetSpawnInterval();

            if (spawn.timer < interval)
                continue;

            spawn.timer = 0;

            if (activeVehicles >= maxVehicles)
                continue;

            if (!spawn.CanSpawnVehicle())
                continue;

            SpawnVehicle(spawn);
        }
    }

    float GetSpawnInterval()
    {
        return Mathf.Lerp(6f, 0.5f, (trafficDensity - 1) / 9f);
    }

    void SpawnVehicle(SpawnPoint spawn)
    {
        if (spawn.firstWaypoint == null)
        {
            Debug.LogWarning(spawn.name + " has no First Waypoint assigned!");
            return;
        }

        GameObject prefab =
            vehiclePrefabs[Random.Range(0, vehiclePrefabs.Length)];

        GameObject vehicle = Instantiate(
            prefab,
            spawn.transform.position,
            spawn.transform.rotation);

        // Assign first waypoint to CarController
        CarController controller = vehicle.GetComponent<CarController>();

        if (controller != null)
        {
            controller.SetWaypoint(spawn.firstWaypoint);
        }

        VehicleLife life = vehicle.GetComponent<VehicleLife>();

        if (life != null)
        {
            life.spawner = this;
            life.spawnPoint = spawn;
        }

        spawn.VehicleSpawned();
        activeVehicles++;

        Debug.Log("Spawned Car at " + spawn.name);
    }

    public void VehicleDestroyed(SpawnPoint spawn)
    {
        if (spawn != null)
            spawn.VehicleDestroyed();

        activeVehicles--;

        if (activeVehicles < 0)
            activeVehicles = 0;
    }

    public void IncreaseTraffic()
    {
        trafficDensity = Mathf.Clamp(trafficDensity + 1, 1, 10);
    }

    public void DecreaseTraffic()
    {
        trafficDensity = Mathf.Clamp(trafficDensity - 1, 1, 10);
    }

    public void SetTrafficDensity(int value)
    {
        trafficDensity = Mathf.Clamp(value, 1, 10);
    }

    public int GetActiveVehicles()
    {
        return activeVehicles;
    }
}