using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    public static HUDController Instance;

    [Header("Statistics")]
    public TMP_Text activeVehicleText;
    public TMP_Text waitingVehicleText;
    public TMP_Text trafficDensityText;
    public TMP_Text simulationStateText;

    [Header("Signal Timing")]
    public TMP_Text greenTimeText;
    public TMP_Text yellowTimeText;
    public TMP_Text redTimeText;

    [Header("Buttons")]
    public Button startButton;
    public Button pauseButton;
    public Button restartButton;
    public Button increaseTrafficButton;
    public Button decreaseTrafficButton;

    [Header("Sliders")]
    public Slider greenSlider;
    public Slider yellowSlider;
    public Slider redSlider;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (greenSlider != null)
            greenSlider.onValueChanged.AddListener(ChangeGreenTime);

        if (yellowSlider != null)
            yellowSlider.onValueChanged.AddListener(ChangeYellowTime);

        if (redSlider != null)
            redSlider.onValueChanged.AddListener(ChangeRedTime);

        if (startButton != null)
            startButton.onClick.AddListener(StartSimulation);

        if (pauseButton != null)
            pauseButton.onClick.AddListener(PauseSimulation);

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartSimulation);

        if (increaseTrafficButton != null)
            increaseTrafficButton.onClick.AddListener(IncreaseTraffic);

        if (decreaseTrafficButton != null)
            decreaseTrafficButton.onClick.AddListener(DecreaseTraffic);
    }

    //--------------------------------------------------
    // Update UI
    //--------------------------------------------------

    public void UpdateHUD(int active, int waiting, int density)
    {
        if (activeVehicleText != null)
            activeVehicleText.text = "Active Vehicles : " + active;

        if (waitingVehicleText != null)
            waitingVehicleText.text = "Waiting Vehicles : " + waiting;

        if (trafficDensityText != null)
            trafficDensityText.text = "Traffic Density : " + density;
    }

    public void SetSimulationState(string state)
    {
        if (simulationStateText != null)
            simulationStateText.text = "Simulation : " + state;
    }

    //--------------------------------------------------
    // Buttons
    //--------------------------------------------------

    void StartSimulation()
    {
        GameManager.Instance.StartSimulation();
        SetSimulationState("Running");
    }

    void PauseSimulation()
    {
        GameManager.Instance.PauseSimulation();
        SetSimulationState("Paused");
    }

    void RestartSimulation()
    {
        GameManager.Instance.RestartSimulation();
    }

    void IncreaseTraffic()
    {
        GameManager.Instance.IncreaseTraffic();
    }

    void DecreaseTraffic()
    {
        GameManager.Instance.DecreaseTraffic();
    }

    //--------------------------------------------------
    // Sliders
    //--------------------------------------------------

    void ChangeGreenTime(float value)
    {
        if (greenTimeText != null)
            greenTimeText.text = value.ToString("0");

        GameManager.Instance.SetGreenTime(value);
    }

    void ChangeYellowTime(float value)
    {
        if (yellowTimeText != null)
            yellowTimeText.text = value.ToString("0");

        GameManager.Instance.SetYellowTime(value);
    }

    void ChangeRedTime(float value)
    {
        if (redTimeText != null)
            redTimeText.text = value.ToString("0");

        GameManager.Instance.SetRedTime(value);
    }
}