using UnityEngine;

public class VehicleSensor : MonoBehaviour
{
    [Header("Sensor Settings")]
    public float sensorDistance = 2f;

    public LayerMask vehicleLayer;

    private bool vehicleAhead;

    public bool IsVehicleAhead()
    {
        return vehicleAhead;
    }

    void Update()
    {
        CheckVehicleAhead();
    }

    void CheckVehicleAhead()
    {
        vehicleAhead = false;

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