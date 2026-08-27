using UnityEngine;

public class EmergencySpawner : MonoBehaviour
{
    public GameObject ambulancePrefab;
    public SpawnPoint spawnPoint;

    void Start()
    {
        if (ambulancePrefab == null || spawnPoint == null)
        {
            Debug.LogError("Missing ambulancePrefab or spawnPoint");
            return;
        }

        GameObject ambulance = Instantiate(
            ambulancePrefab,
            spawnPoint.transform.position,
            spawnPoint.transform.rotation);

        Debug.Log("Ambulance Spawned");

        AmbulanceController controller =
            ambulance.GetComponent<AmbulanceController>();

        if (controller != null)
        {
            Debug.Log("AmbulanceController Found");
            controller.SetWaypoint(spawnPoint.firstWaypoint);
        }
        else
        {
            Debug.LogError("AmbulanceController NOT FOUND");
        }
    }
}