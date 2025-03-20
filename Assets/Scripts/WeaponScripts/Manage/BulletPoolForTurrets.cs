using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolForTurrets : MonoBehaviour
{
    public GameObject bulletPrefab; // Bullet prefab to be pooled
    public int initialPoolSize = 10; // Initial pool size
    private Queue<GameObject> bulletPool; // Queue to hold bullets

    private void Awake()
    {
        if (bulletPool == null)
        {
            Debug.LogError("Bullet Pool is empty!");
        }

        bulletPool = new Queue<GameObject>();

        // Fill the pool with bullets
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false); // Deactivate initially
            bullet.GetComponent<TurretBullet>().SetBulletPool(this);
            bulletPool.Enqueue(bullet);
        }
    }

    // Get a bullet from the pool
    public GameObject GetBullet(Vector3 position, Quaternion rotation)
    {
        if (bulletPool.Count == 0)
        {
            // Optionally, expand the pool if needed
            GameObject bullet = Instantiate(bulletPrefab, position, rotation);
            bullet.SetActive(false);
            bullet.GetComponent<TurretBullet>().SetBulletPool(this); // Set pool reference
            bulletPool.Enqueue(bullet);
        }

        if (bulletPool.Count > 0)
        {
            GameObject bulletToUse = bulletPool.Dequeue();
            bulletToUse.transform.position = position; // Set the bullet's position
            bulletToUse.transform.rotation = rotation; // Set the bullet's rotation
            bulletToUse.SetActive(true); // Activate the bullet
            return bulletToUse;
        }
        else
        {
            Debug.LogError("no bullet pool available!");
            return null;
        }
    }

    // Return a bullet to the pool
    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false); // Deactivate the bullet and return it to the pool
        bulletPool.Enqueue(bullet);
    }
}