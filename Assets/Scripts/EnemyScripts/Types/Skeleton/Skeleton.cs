using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Skeleton : Enemy
{
    
    public override int health { get; set; } = 100;
    public override float attackRange { get; set; } = 2f;
    public override float attackCooldown { get; set; } = 1.5f;
    

    [SerializeField] private Collider swordCollider;

    public float walkThreshold = 0.3f;
    
    private bool isAttacking = false;
    private bool hasDealtDamage = false;
    
    private bool isChasing = false;

    public Collider wanderZone;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
    }

    protected override void Update()
    {
        if (!isChasing)
        {
            if (wanderZone)
            {
                Wander();
            }
        }
        // float speed = agent.velocity.magnitude;
        // animator.SetFloat("Speed", speed);
        
        // animator.SetBool("IsWalking", speed > walkThreshold);
        animator.SetFloat("Speed", agent.velocity.magnitude);
        HandleMovementAndAttack();
        
    }
    protected override IEnumerator Attack()
    {
        isAttacking = true;
        hasDealtDamage = false; // Reset for this attack

        swordCollider.enabled = true; // Enable sword hitbox
        animator.SetLayerWeight(1, 1);
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.5f); // Adjust timing to match animation impact frame

        swordCollider.enabled = false; // Disable after attack hit frame
        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
    }
    
    
    void HandleMovementAndAttack()
    {
        if (player == null) return;
        
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance < attackRange)
        {
            if (!isAttacking)
            {
                StartCoroutine(Attack());
            }
        }
        else if (distance < 15)
        {
            agent.speed = 8f;
            agent.SetDestination(player.position);
        }
    }

    // a method to randomize the wandering of the enemy
    void Wander()
    {
        if (!agent.hasPath || agent.remainingDistance < 0.5f)
        {
            agent.speed = 1.5f;
            Vector3 randomPoint = GetRandomPointInWanderZone();

            // Set destination directly (no NavMeshHit needed)
            agent.SetDestination(randomPoint);
        }
    }
    
    private Vector3 GetRandomPointInWanderZone()
    {
        Vector3 center = wanderZone.bounds.center;
        Vector3 size = wanderZone.bounds.size;

        float randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float randomZ = Random.Range(center.z - size.z / 2, center.z + size.z / 2);

        return new Vector3(randomX, transform.position.y, randomZ);
    }

    
    private void OnTriggerEnter(Collider collider)
    {
        
        if (collider.gameObject.CompareTag("Player"))
        {
            PlayerController player = collider.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                player.TakeDamage(10);
            }
        }
    }
    
}
