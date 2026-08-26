using UnityEngine;

public class TrafficManager : MonoBehaviour
{
    public static TrafficManager Instance;

    [Header("Vehicle Spawner")]
    public VehicleSpawner vehicleSpawner;

    [Header("Intersections")]
    public Intersection[] intersections;

    [Header("Roundabouts")]
    public Roundabout[] roundabouts;

    [Header("Traffic Density (1-10)")]
    [Range(1,10)]
    public int trafficDensity = 5;

    [Header("Statistics")]
    public int activeVehicles;
    public int waitingVehicles;

    private bool simulationPaused = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        ApplyTrafficDensity();
    }

    void Update()
    {
        if (vehicleSpawner != null)
        {
            activeVehicles = GameObject.FindGameObjectsWithTag("Car").Length;
        }
    }

    //------------------------
    // Traffic Density
    //------------------------

    public void IncreaseTraffic()
    {
        if (trafficDensity < 10)
        {
            trafficDensity++;
            ApplyTrafficDensity();
        }
    }

    public void DecreaseTraffic()
    {
        if (trafficDensity > 1)
        {
            trafficDensity--;
            ApplyTrafficDensity();
        }
    }

    void ApplyTrafficDensity()
    {
        if (vehicleSpawner != null)
        {
            vehicleSpawner.trafficDensity = trafficDensity;
        }
    }

    //------------------------
    // Simulation
    //------------------------

    public void PauseSimulation()
    {
        simulationPaused = true;
        Time.timeScale = 0;
    }

    public void ResumeSimulation()
    {
        simulationPaused = false;
        Time.timeScale = 1;
    }

    public void TogglePause()
    {
        if (simulationPaused)
            ResumeSimulation();
        else
            PauseSimulation();
    }

    //------------------------
    // Vehicle Statistics
    //------------------------

    public void VehicleStartedWaiting()
    {
        waitingVehicles++;
    }

    public void VehicleStoppedWaiting()
    {
        waitingVehicles--;

        if (waitingVehicles < 0)
            waitingVehicles = 0;
    }

    public int GetTrafficDensity()
    {
        return trafficDensity;
    }

    public int GetVehicleCount()
    {
        return activeVehicles;
    }

    public int GetWaitingVehicleCount()
    {
        return waitingVehicles;
    }
}