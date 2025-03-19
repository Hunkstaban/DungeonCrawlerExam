using System;
using UnityEngine;

public class ThrownStar : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerController player = collider.gameObject.GetComponent<PlayerController>();
            if (player)
            {
                player.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
