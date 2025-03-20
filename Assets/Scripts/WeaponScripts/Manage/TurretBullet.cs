using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class TurretBullet : MonoBehaviour, IProjectile
{

    [SerializeField] private int damage = 10;
    
    private BulletPoolForTurrets _bulletPoolForTurrets;
  
 

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
                    
                    _bulletPoolForTurrets.ReturnBullet(gameObject); // instead of destroy. we return the bullet to the pool
                }
            }
        }
    }


    public void SetBulletPool(BulletPoolForTurrets poolForTurrets)
    {
        _bulletPoolForTurrets = poolForTurrets;
    }

    private void Deactivate()
    {
        _bulletPoolForTurrets.ReturnBullet(gameObject); // returns the bullet to the pool
    }

    // Coroutine that waits for 5 seconds and then returns the bullet to the pool
    private IEnumerator ReturnToPoolAfterDelay()
    {
        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // After 5 seconds, return the bullet to the pool
        ReturnBulletToPool();
    }

    // return the bullet to the pool
    private void ReturnBulletToPool()
    {
        if (_bulletPoolForTurrets != null)
        {
            _bulletPoolForTurrets.ReturnBullet(gameObject); // Deactivate and return to the pool
        }
    }

    private void OnEnable()
    {
        // Start the coroutine when the bullet is enabled
        StartCoroutine(ReturnToPoolAfterDelay());
    }

    private void OnDisable()
    {
        // Stop the coroutine if the bullet is disabled early (such as if it hits something)
        StopCoroutine(ReturnToPoolAfterDelay());
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