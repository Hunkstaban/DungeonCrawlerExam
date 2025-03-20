using System.Collections;
using UnityEngine;

public class Wizard : Enemy
{
    public override int health { get; set; } = 100;
    public override float attackRange { get; set; } = 25f;
    public override float attackCooldown { get; set; } = 2f;

    [SerializeField] private GameObject wizardBall;

    public Transform staffPoint;
    protected override void Update()
    {
        if (player == null) return;
        
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= attackRange)
        {
            Vector3 lookPos = player.position - transform.position;
            lookPos.y = 0; // Ignore vertical rotation

            // Rotate towards the player
            transform.rotation = Quaternion.LookRotation(lookPos);
            
            StartCoroutine(Attack());
        }
        
    }

    protected override IEnumerator Attack()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackCooldown);
    }
    
    private void CastSpell()
    {
        GameObject ball = Instantiate(wizardBall, staffPoint.position, Quaternion.identity);
    
        // Calculate direction from staff to player
        Vector3 direction = (player.position - staffPoint.position).normalized;

        // Apply force in that direction
        ball.GetComponent<Rigidbody>().AddForce(direction * 20f, ForceMode.Impulse);

        // Destroy the spell after 2 seconds
        Destroy(ball, 2f);
    }

}
