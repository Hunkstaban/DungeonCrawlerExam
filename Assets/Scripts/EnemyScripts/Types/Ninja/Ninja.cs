using System.Collections;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Ninja : Enemy
{
    public override int health { get; set; } = 100;
    public override float attackRange { get; set; } = 15f;
    public override float attackCooldown { get; set; } = 1.4f;

    [SerializeField] private GameObject starPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float throwForce = 10f;

    private bool canAttack = true;

    protected override void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
        HandleMovementAndAttack();
    }

    protected override IEnumerator Attack()
    {
        canAttack = false;
        agent.isStopped = true; //Stop moving when attacking

        // animator.SetLayerWeight(1, 1);
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }

    void HandleMovementAndAttack()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);

        if (distance <= 10) // (Stop and attack)
        {
            agent.isStopped = true;
            transform.LookAt(targetPosition);
            if (canAttack) StartCoroutine(Attack());
            return;
        }

        if (distance <= attackRange) // Player is close enough to move and attack
        {
            agent.isStopped = false;
            transform.LookAt(targetPosition);
            agent.SetDestination(player.position);
            if (canAttack) StartCoroutine(Attack());
            return;
        }

        if (distance <= 20) // Move toward player
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
            return;
        }

        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    private void ThrowNinjaStar()
    {
        GameObject starInstance = Instantiate(starPrefab, transform.position, Quaternion.identity);
        Rigidbody starRb = starInstance.GetComponent<Rigidbody>();
        starRb.useGravity = false;
        starRb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
        Destroy(starInstance, 3f);

        int rand = Random.Range(1, 6);
        if (rand == 1)
        {
            for (int i = 1; i < 6; i++)
            {
                GameObject starInstance2 = Instantiate(starPrefab, transform.position, Quaternion.identity);
                Rigidbody starRb2 = starInstance2.GetComponent<Rigidbody>();
                starRb2.useGravity = false;
                Vector3 randDirection = GetRandomDirection();
                float thowForceAdjusted = throwForce * Random.Range(0.5f, 1.5f);
                starRb2.AddForce(randDirection * thowForceAdjusted, ForceMode.Impulse);
                Destroy(starInstance2, 3f);
            }
        }
    }

    Vector3 GetRandomDirection()
    {
        float randomAngle = Random.Range(-45f, 45f);
        Quaternion rotation = Quaternion.Euler(0, randomAngle, 0);
        return rotation * transform.forward;
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player")) 
    //     {
    //         PlayerController player = other.GetComponent<PlayerController>();
    //         player.TakeDamage(20);
    //     }
    // }
}