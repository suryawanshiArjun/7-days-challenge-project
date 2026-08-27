using UnityEngine;

public class EmergencyTrafficController : MonoBehaviour
{
    public TrafficLight trafficLight;
    public float detectionRadius = 3f;

    void Update()
    {
        bool ambulanceNearby = false;

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            detectionRadius);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("EmergencyVehicle"))
            {
                ambulanceNearby = true;
                break;
            }
        }

        if (ambulanceNearby)
        {
            trafficLight.SetGreen();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}