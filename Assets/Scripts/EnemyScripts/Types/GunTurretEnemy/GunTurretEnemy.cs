using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class GunTurretEnemy : Enemy
{
    public override int health { get; set; } = 300;
    public override float attackRange { get; set; } = 5f;
    public override float attackCooldown { get; set; } = 2f;
    
    

    [Header("Bullet")] [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePointRight;
    [SerializeField] private Transform firePointLeft;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float bulletSize = 1f;
    [SerializeField] private int bulletDamage = 10;

    private float lastTimeAttacked; // a variable for seconds for last time the turret attacked 



    private void SetNewBulletDmg(GameObject bullet)
    {
        // Get the TurretBullet component on the bullet and set its damage
        TurretBullet turretBullet = bullet.GetComponent<TurretBullet>();
        if (turretBullet != null)
        {
            turretBullet.SetDamage(bulletDamage);
        }
        else
        {
            Debug.LogError("No TurretBullet component found on the instantiated bullet.");
        }
    }

    private void SetNewBulletTransform()
    {
        bulletPrefab.gameObject.transform.localScale = transform.localScale * bulletSize;
    }
    protected override void Update()
    {
        
        // set the new size on the Bullet 
        SetNewBulletTransform();
        SetNewBulletDmg(bulletPrefab);
        // Time.time: This is a Unity function that returns the time (in seconds)
        // Time.time is all the seconds that have passed since the game started. 
        if (Time.time - lastTimeAttacked >= attackCooldown)
        {
            // Coroutines in Unity allow asynchronous execution of code over time without blocking the main thread
            StartCoroutine(Attack());
            Debug.Log("StartCouroutine is calling Attack");
        }
    }


    // in Coroutines we are using IEnumerator as the return type and yield to pause execution.
    protected override IEnumerator Attack()
    {
        lastTimeAttacked = Time.time; // Record the time of attack to handle cooldown

        // instantiate the bulletPrefab where the firepointPrefab is
        GameObject bulletRight = Instantiate(bulletPrefab, firePointRight.position, firePointRight.rotation);
        Debug.Log("Instantiate the right bullet");
        GameObject bulletLeft = Instantiate(bulletPrefab, firePointLeft.position, firePointLeft.rotation);
        Debug.Log("Instantiate the right bullet");
        
        Vector3 direction = firePointRight.forward;
        
        Rigidbody rigidBodyBulletRight = bulletRight.GetComponent<Rigidbody>();
        Rigidbody rigidBodyBulletLeft = bulletLeft.GetComponent<Rigidbody>();

        if (rigidBodyBulletLeft && rigidBodyBulletRight != null)
        {
            rigidBodyBulletLeft.AddForce(direction * bulletSpeed, ForceMode.Impulse);
            rigidBodyBulletRight.AddForce(direction * bulletSpeed, ForceMode.Impulse);
        }

        yield return null;
    }
}