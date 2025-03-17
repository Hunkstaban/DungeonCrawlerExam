using System;
using UnityEngine;

public class FireBullet : MonoBehaviour, IProjectile
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private int damage = 20;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();

                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
                
            }

            if (collision.gameObject.CompareTag("Destroyable"))
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            
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