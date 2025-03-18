using System;
using UnityEngine;

public class Bullet : MonoBehaviour, IProjectile
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private int damage = 10;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            // Debug.Log(collision.gameObject.name);
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();

                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }

            Destroy(gameObject);
        }
        Destroy(gameObject, 5);
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