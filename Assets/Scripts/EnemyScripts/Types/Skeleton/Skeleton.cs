using System.Collections;
using UnityEngine;

public class Skeleton : Enemy
{
    
    public override int health { get; set; } = 100;
    public override float attackRange { get; set; } = 2f;
    public override float attackCooldown { get; set; } = 1.5f;
    

    [SerializeField] private Collider swordCollider;

    public float walkThreshold = 0.3f;
    
    private bool isAttacking = false;
    private bool hasDealtDamage = false;
    protected override void Update()
    {
        
        float speed = agent.velocity.magnitude;
        // animator.SetFloat("Speed", speed);
        
        animator.SetBool("IsWalking", speed > walkThreshold);
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
            
            agent.SetDestination(player.position);
        }
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
