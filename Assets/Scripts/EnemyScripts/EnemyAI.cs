using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Animator animator;
    public Transform player;
    private NavMeshAgent agent;

    public float walkThreshold = 0.3f;
    public float health = 50f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f; // Adjust based on animation length
    private bool isAttacking = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float speed = agent.velocity.magnitude;
        animator.SetBool("IsWalking", speed > walkThreshold);
        HandleMovementAndAttack();
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
        else if (distance < 10)
        {
            agent.SetDestination(player.position);
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetLayerWeight(1, 1); // Activate Attack Layer
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.1f); // Short delay before checking distance

        if (Vector3.Distance(player.position, transform.position) > attackRange)
        {
            isAttacking = false; // Cancel attack if player moved away
            yield break; // Stop coroutine
        }

        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}