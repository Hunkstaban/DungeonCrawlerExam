using UnityEngine;

public class CollectiblePowerUp : MonoBehaviour
{
    public PowerUp itemType;
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            PowerUpManager powerUpManager = collider.GetComponent<PowerUpManager>();
            if (powerUpManager != null)
            {
                powerUpManager.CollectItem(itemType);
                // gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}