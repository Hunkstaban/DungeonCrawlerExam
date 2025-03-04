using System;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int Damage { get; set; } = 10;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();

                if (enemy != null)
                {
                    enemy.TakeDamage(Damage);
                }
            }

            Destroy(gameObject);
        }
    }
}