using System;
using UnityEngine;

public class ThrownRock : MonoBehaviour
{
    public int damage = 45; // Damage the rock deals

    private Collider collider;
    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        // else if (collision.gameObject.CompareTag("Ground"))
        // {
        //     collider.enabled = false;
        // }
        
    }
}