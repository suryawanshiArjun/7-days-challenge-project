using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public enum LightState
    {
        Red,
        Yellow,
        Green
    }

    [Header("Current State")]
    public LightState currentState = LightState.Red;

    [Header("Signal Objects (Optional)")]
    public GameObject redLight;
    public GameObject yellowLight;
    public GameObject greenLight;

    void Start()
    {
        UpdateLights();
    }

    public void SetRed()
    {
        currentState = LightState.Red;
        UpdateLights();
    }

    public void SetYellow()
    {
        currentState = LightState.Yellow;
        UpdateLights();
    }

    public void SetGreen()
    {
        currentState = LightState.Green;
        UpdateLights();
    }

    // ==========================
    // REQUIRED BY VehicleAI
    // ==========================

    public bool IsRed()
    {
        return currentState == LightState.Red;
    }

    public bool IsYellow()
    {
        return currentState == LightState.Yellow;
    }

    public bool IsGreen()
    {
        return currentState == LightState.Green;
    }

    // ==========================

    void UpdateLights()
    {
        if (redLight != null)
            redLight.SetActive(currentState == LightState.Red);

        if (yellowLight != null)
            yellowLight.SetActive(currentState == LightState.Yellow);

        if (greenLight != null)
            greenLight.SetActive(currentState == LightState.Green);
    }
}