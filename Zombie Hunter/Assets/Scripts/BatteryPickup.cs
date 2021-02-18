using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] float restoreAngle = 45.0f;
    [SerializeField] float addIntensity = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        other.gameObject.GetComponentInChildren<FlashLightSystem>().RestoreLightAngle(restoreAngle);
        other.gameObject.GetComponentInChildren<FlashLightSystem>().AddLightIntensity(addIntensity);

        Destroy(gameObject);
    }
}
