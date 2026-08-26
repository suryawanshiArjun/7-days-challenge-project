using UnityEngine;
using System.Collections;

public class TrafficSignalController : MonoBehaviour
{
    [Header("Traffic Lights")]
    public TrafficLight northLight;
    public TrafficLight southLight;
    public TrafficLight eastLight;
    public TrafficLight westLight;

    [Header("Signal Timing")]
    public float greenTime = 10f;
    public float yellowTime = 3f;
    public float redTime = 1f;

    private void Start()
{
    Debug.Log("Traffic Controller Started : " + gameObject.name);
    StartCoroutine(SignalRoutine());
}

    IEnumerator SignalRoutine()
    {
        while (true)
        {
            //-----------------------------
            // North + South Green
            //-----------------------------
            SetGreen(northLight);
            SetGreen(southLight);

            SetRed(eastLight);
            SetRed(westLight);

            yield return new WaitForSeconds(greenTime);

            SetYellow(northLight);
            SetYellow(southLight);

            yield return new WaitForSeconds(yellowTime);

            SetRed(northLight);
            SetRed(southLight);
            SetRed(eastLight);
            SetRed(westLight);

            yield return new WaitForSeconds(redTime);

            //-----------------------------
            // East + West Green
            //-----------------------------
            SetGreen(eastLight);
            SetGreen(westLight);

            SetRed(northLight);
            SetRed(southLight);

            yield return new WaitForSeconds(greenTime);

            SetYellow(eastLight);
            SetYellow(westLight);

            yield return new WaitForSeconds(yellowTime);

            SetRed(northLight);
            SetRed(southLight);
            SetRed(eastLight);
            SetRed(westLight);

            yield return new WaitForSeconds(redTime);
        }
    }

    void SetGreen(TrafficLight light)
    {
        if (light != null)
            light.SetGreen();
    }

    void SetYellow(TrafficLight light)
    {
        if (light != null)
            light.SetYellow();
    }

    void SetRed(TrafficLight light)
    {
        if (light != null)
            light.SetRed();
    }

    public void SetGreenTime(float value)
    {
        greenTime = value;
    }

    public void SetYellowTime(float value)
    {
        yellowTime = value;
    }

    public void SetRedTime(float value)
    {
        redTime = value;
    }
}