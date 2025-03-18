using System;
using Unity.VisualScripting;
using UnityEngine;

public class TurretBullet : MonoBehaviour, IProjectile
{

    [SerializeField] private int damage = 10;
 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            // // ---- Only needed if the turrent need Friendly fire on other Enemies ----
            // if (collision.gameObject.CompareTag("Enemy"))
            // {
            //     Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            //
            //     if (enemy != null)
            //     {
            //         enemy.TakeDamage(damage);
            //     }
            //     
            // }
            
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
        }
    }
    
  

    public int GetDamage()
    {
        return damage;
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
}