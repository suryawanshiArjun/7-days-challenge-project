using UnityEngine;

public class VehicleLife : MonoBehaviour
{
    [HideInInspector]
    public SpawnPoint spawnPoint;

    [HideInInspector]
    public VehicleSpawner spawner;

    public void DestroyVehicle()
    {
        if (spawner != null)
            spawner.VehicleDestroyed(spawnPoint);

        Destroy(gameObject);
    }
}