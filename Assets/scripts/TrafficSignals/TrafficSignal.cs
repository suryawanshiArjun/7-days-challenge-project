using UnityEngine;

public class TrafficSignal : MonoBehaviour
{
    public GameObject redLight;
    public GameObject yellowLight;
    public GameObject greenLight;

    public float greenTime = 8f;
    public float yellowTime = 2f;
    public float redTime = 8f;

    private enum SignalState
    {
        Green,
        Yellow,
        Red
    }

    private SignalState currentState;
    private float timer;

    void Start()
    {
        currentState = SignalState.Green;
        timer = greenTime;
        UpdateLights();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SwitchState();
        }
    }

    void SwitchState()
    {
        switch (currentState)
        {
            case SignalState.Green:
                currentState = SignalState.Yellow;
                timer = yellowTime;
                break;

            case SignalState.Yellow:
                currentState = SignalState.Red;
                timer = redTime;
                break;

            case SignalState.Red:
                currentState = SignalState.Green;
                timer = greenTime;
                break;
        }

        UpdateLights();
    }

    void UpdateLights()
    {
        redLight.SetActive(currentState == SignalState.Red);
        yellowLight.SetActive(currentState == SignalState.Yellow);
        greenLight.SetActive(currentState == SignalState.Green);
    }

    public bool IsGreen()
    {
        return currentState == SignalState.Green;
    }
}
