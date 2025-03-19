using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GunPickup : MonoBehaviour
{
    public Gun weapon;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            PlayerController player = collider.gameObject.GetComponent<PlayerController>();
            if (player.equippedWeapon != null)
            {
                Destroy(((MonoBehaviour)player.equippedWeapon).gameObject);
                player.equippedWeapon = null;
            }

            player.equippedWeapon = Instantiate(weapon, player.GetWeaponPos);
            Destroy(gameObject);
        }
    }
}
