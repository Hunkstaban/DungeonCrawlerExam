using UnityEngine;

public class SwordPickup : MonoBehaviour
{
    public Gun weapon;

    private void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Player")
        {
            PlayerController player = collider.gameObject.GetComponent<PlayerController>();
            if (player.equippedWeapon != null)
            {
                player.equippedWeapon.Remove();
            }
            player.equippedWeapon = Instantiate(weapon, player.transform);
            Destroy(gameObject);
            
        }
        
    }
    
}

