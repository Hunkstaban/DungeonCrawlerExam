using System;
using System.Collections;
using UnityEngine;

public class RockGolem : Enemy
{
   
    public override int health { get; set; } = 300;
    public override float attackRange { get; set; } = 20f;
    public override float attackCooldown { get; set; }

    [SerializeField] private GameObject rock;
    public Transform throwPoint;
    public float throwForce = 20f;
    
    public Collider fistCollider;
    
    private bool canAttack = true;
    private bool isAttacking = false;
    protected override void Update()
    {
        HandleMovementAndAttack();
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    protected override IEnumerator Attack()
    {
        if (!canAttack) yield break; // Prevent multiple attacks

        canAttack = false;

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= 4f) // Melee attack
        {
            animator.SetTrigger("Attack2");
        }
        else // Ranged attack (stop movement)
        {
            isAttacking = true;
            agent.ResetPath(); // STOP moving before throwing
            animator.SetTrigger("Attack1");
        }

        yield return new WaitForSeconds(1.5f); // Attack cooldown
        canAttack = true;
        isAttacking = false; // Allow movement again
    }

    void HandleMovementAndAttack()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        float stopDistance = attackRange - 1f;
        
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z); // Keep the same Y-axis

        // if (isAttacking) return; // Prevent movement during ranged attack

        if (distance <= 10f) // Player is close, move toward them
        {
            transform.LookAt(targetPosition);
            agent.SetDestination(player.position);
            
            if (distance <= 4f && canAttack) // Melee attack while moving
            {
                StartCoroutine(Attack());
            }
        }
        else if (distance <= attackRange) // Ranged attack (stand still)
        {
            transform.LookAt(targetPosition);
            agent.SetDestination(transform.position);
            
            StartCoroutine(Attack());
        }
        else if (distance <= attackRange + stopDistance) // Move closer but stop at attack range with offset
        {
            transform.LookAt(targetPosition);
            // Move towards the player, but stop slightly before the target
            Vector3 direction = (player.position - transform.position).normalized;
            Vector3 offsetPosition = player.position - direction * stopDistance;
            agent.SetDestination(offsetPosition);
        }
    }

    private void ThrowRock()
    {
        GameObject rockInstance = Instantiate(rock, throwPoint.position + throwPoint.forward, Quaternion.identity);
        rockInstance.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
        Destroy(rockInstance, 2);
    }
    
    public void ActivatePunchHitbox()
    {
        fistCollider.enabled = true; 
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.TakeDamage(20);
            fistCollider.enabled = false;
        }
    }

    

    
}
