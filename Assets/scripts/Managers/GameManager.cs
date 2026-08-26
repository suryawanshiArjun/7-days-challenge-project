using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Managers")]
    public SimulationManager simulationManager;
    public TrafficManager trafficManager;
    public VehicleSpawner vehicleSpawner;
    public HUDController hudController;

    [Header("Game State")]
    public bool gameStarted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeGame();
    }

    void Update()
    {
        if (!gameStarted)
            return;

        if (hudController != null)
        {
            hudController.UpdateHUD(
                trafficManager.GetVehicleCount(),
                trafficManager.GetWaitingVehicleCount(),
                trafficManager.GetTrafficDensity()
            );
        }
    }

    //-------------------------------------------------
    // Initialize
    //-------------------------------------------------

    public void InitializeGame()
    {
        gameStarted = true;

        if (simulationManager != null)
            simulationManager.StartSimulation();
    }

    //-------------------------------------------------
    // Simulation Controls
    //-------------------------------------------------

    public void StartSimulation()
    {
        simulationManager.StartSimulation();
    }

    public void PauseSimulation()
    {
        simulationManager.PauseSimulation();
    }

    public void ResumeSimulation()
    {
        simulationManager.ResumeSimulation();
    }

    public void RestartSimulation()
    {
        simulationManager.RestartSimulation();
    }

    //-------------------------------------------------
    // Traffic Density
    //-------------------------------------------------

    public void IncreaseTraffic()
    {
        trafficManager.IncreaseTraffic();
    }

    public void DecreaseTraffic()
    {
        trafficManager.DecreaseTraffic();
    }

    //-------------------------------------------------
    // Signal Timing
    //-------------------------------------------------

    public void SetGreenTime(float value)
    {
        simulationManager.SetGreenTime(value);
    }

    public void SetYellowTime(float value)
    {
        simulationManager.SetYellowTime(value);
    }

    public void SetRedTime(float value)
    {
        simulationManager.SetRedTime(value);
    }

    //-------------------------------------------------
    // Simulation Speed
    //-------------------------------------------------

    public void IncreaseSpeed()
    {
        simulationManager.IncreaseSimulationSpeed();
    }

    public void DecreaseSpeed()
    {
        simulationManager.DecreaseSimulationSpeed();
    }
}