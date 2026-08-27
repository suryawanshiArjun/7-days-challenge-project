using UnityEngine;

public class VehicleSensor : MonoBehaviour
{
    [Header("Sensor Settings")]
    public float sensorDistance = 2f;

    public LayerMask vehicleLayer;

    private bool vehicleAhead;
    private bool ambulanceAhead;

    public bool IsVehicleAhead()
    {
        return vehicleAhead;
    }

    public bool IsAmbulanceAhead()
    {
        return ambulanceAhead;
    }

    void Update()
    {
        CheckVehicleAhead();
    }

    void CheckVehicleAhead()
    {
        vehicleAhead = false;
        ambulanceAhead = false;

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            transform.up,
            sensorDistance,
            vehicleLayer);

        Debug.DrawRay(
            transform.position,
            transform.up * sensorDistance,
            Color.green);

        if (hit.collider != null)
        {
            // Check if the object ahead is an ambulance
            if (hit.collider.GetComponent<EmergencyVehicle>() != null)
            {
                ambulanceAhead = true;

                Debug.DrawRay(
                    transform.position,
                    transform.up * sensorDistance,
                    Color.cyan);

                return;
            }

            // Check if the object ahead is a normal vehicle
            CarController car = hit.collider.GetComponent<CarController>();

            if (car != null && car.gameObject != gameObject)
            {
                vehicleAhead = true;

                Debug.DrawRay(
                    transform.position,
                    transform.up * sensorDistance,
                    Color.red);
            }
        }
    }
}