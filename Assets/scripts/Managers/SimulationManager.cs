using UnityEngine;
using UnityEngine.SceneManagement;

public class SimulationManager : MonoBehaviour
{
    public static SimulationManager Instance;

    [Header("Managers")]
    public TrafficManager trafficManager;

    [Header("Simulation State")]
    public bool simulationRunning = false;
    public bool simulationPaused = false;

    [Header("Simulation Speed")]
    [Range(0.5f,3f)]
    public float simulationSpeed = 1f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        StartSimulation();
    }

    void Update()
    {
        Time.timeScale = simulationSpeed;
    }

    //-----------------------------
    // Simulation Controls
    //-----------------------------

    public void StartSimulation()
    {
        simulationRunning = true;
        simulationPaused = false;
        Time.timeScale = simulationSpeed;
    }

    public void PauseSimulation()
    {
        simulationPaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeSimulation()
    {
        simulationPaused = false;
        Time.timeScale = simulationSpeed;
    }

    public void TogglePause()
    {
        if (simulationPaused)
            ResumeSimulation();
        else
            PauseSimulation();
    }

    //-----------------------------
    // Restart
    //-----------------------------

    public void RestartSimulation()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //-----------------------------
    // Traffic Density
    //-----------------------------

    public void IncreaseTraffic()
    {
        if (trafficManager != null)
            trafficManager.IncreaseTraffic();
    }

    public void DecreaseTraffic()
    {
        if (trafficManager != null)
            trafficManager.DecreaseTraffic();
    }

    //-----------------------------
    // Signal Timing
    //-----------------------------

    public void SetGreenTime(float value)
    {
        foreach (Intersection intersection in trafficManager.intersections)
        {
            if (intersection != null &&
                intersection.signalController != null)
            {
                intersection.signalController.greenTime = value;
            }
        }
    }

    public void SetYellowTime(float value)
    {
        foreach (Intersection intersection in trafficManager.intersections)
        {
            if (intersection != null &&
                intersection.signalController != null)
            {
                intersection.signalController.yellowTime = value;
            }
        }
    }

    public void SetRedTime(float value)
    {
        foreach (Intersection intersection in trafficManager.intersections)
        {
            if (intersection != null &&
                intersection.signalController != null)
            {
                intersection.signalController.redTime = value;
            }
        }
    }

    //-----------------------------
    // Simulation Speed
    //-----------------------------

    public void IncreaseSimulationSpeed()
    {
        simulationSpeed += 0.25f;

        if (simulationSpeed > 3f)
            simulationSpeed = 3f;
    }

    public void DecreaseSimulationSpeed()
    {
        simulationSpeed -= 0.25f;

        if (simulationSpeed < 0.5f)
            simulationSpeed = 0.5f;
    }
}