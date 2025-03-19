using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class GunTurretOneBarrel : Enemy
{
    public override int health { get; set; } = 300;
    public override float attackRange { get; set; } = 5f;
    public override float attackCooldown { get; set; } = 2f;


    [Header("Bullet")] [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePointMiddle;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float bulletSize = 1f;
    [SerializeField] private int bulletDamage = 10;
    [SerializeField]private BulletPool bulletPool;
    
    [Header("Player Tracking")]
    [SerializeField] private float rotationAndTurnSpeed = 5f;

    private float lastTimeAttacked; // a variable for seconds for last time the turret attacked 

    private Transform player; // reference to the player. so we can shoot with the tag "Player" is 


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

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
        
        RotateTowardsPlayer();
        
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
        if (firePointMiddle != null)
        {
        }

        GameObject bulletMiddle = bulletPool.GetBullet(firePointMiddle.position, firePointMiddle.rotation);
        Debug.Log("Instantiate the middle bullet");
     
        
        Vector3 direction = firePointMiddle.forward.normalized; // .normalized makes the direction from the turret to the player become vector 1. no matter the distance between them. more easy to use in the furture 
        
        Rigidbody rigidBodyBulletMiddle = bulletMiddle.GetComponent<Rigidbody>();
        

        if ( rigidBodyBulletMiddle != null)
        {
            rigidBodyBulletMiddle.AddForce(direction * bulletSpeed, ForceMode.Impulse);
        }

        yield return null;
    }


    private void RotateTowardsPlayer()
    {
        // the directions from the turret to the player. (Where should the turret look for the player. )
       Vector3 directionOfThePlayer = player.position - transform.position; // transform. refere to this.object (GunTurretOneBarrel)
       
       //make the rotation point towards the player
       Quaternion lookRotation = Quaternion.LookRotation(directionOfThePlayer);
       
       // Rotate the turret smoothly towards the player. .Slept = makes a smooth rotation. 
       // Time.deltatime = the time that has passed in seconds since last frame
       transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationAndTurnSpeed); // Adjust speed as needed
       
    }
}