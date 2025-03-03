using UnityEngine;

public class CollectiblePowerUp : MonoBehaviour
{
    public PowerUp powerUpScript;
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            PowerUpManager powerUpManager = collider.GetComponent<PowerUpManager>();
            if (powerUpManager != null)
            {
                powerUpManager.CollectItem(powerUpScript);
                Destroy(gameObject);
            }
        }
    }
}