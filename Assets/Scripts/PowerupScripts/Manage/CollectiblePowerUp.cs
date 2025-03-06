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
                bool pickedUp = powerUpManager.CollectItem(powerUpScript);
                if (pickedUp) Destroy(gameObject);
            }
        }
    }
}