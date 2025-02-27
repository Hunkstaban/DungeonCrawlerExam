using System.Collections;
using UnityEngine;

public class SpiderAI : Enemy
{
    [Header("Spider Settings")]
    public float detectionRange = 15f;
    
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;
    
    
    [Header("Projectile Settings")]
    public GameObject acidBulletPrefab;
    public Transform muzzle;
    public float projectileSpeed = 10f;

    private Transform player;
    private float attackTimer = 0f;

    void Start()
    {
        // Find the player in the scene (assuming the player has the "Player" tag)
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    
    public override int health { get; set; } = 100;
    public override float attackRange { get; set; } = 15f;
    public override float attackCooldown { get; set; } = 2f;

    protected override void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            // Rotate towards the player
            RotateTowardsPlayer();

            if (distanceToPlayer > attackRange)
            {
                // Move towards the player
                MoveTowardsPlayer();
            }
            else
            {
                // Attack the player
                AttackPlayer();
            }
        }

        // Update the attack timer
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    protected override IEnumerator Attack()
    {
        throw new System.NotImplementedException();
    }
    
    
    void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void AttackPlayer()
    {
        if (attackTimer <= 0)
        {
            // Instantiate the acid bullet
            GameObject acidBullet = Instantiate(acidBulletPrefab, muzzle.position, muzzle.rotation);

            // Set the bullet's velocity
            Rigidbody rb = acidBullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = muzzle.forward * projectileSpeed;
            }

            // Reset the attack timer
            attackTimer = attackCooldown;
        }
    }
    
    
}
