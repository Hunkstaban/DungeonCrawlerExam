using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour

{
    public System.Action<Enemy> OnDeath;
    // abstract keyword forces derived classes to implement these properties
    public abstract int health { get; set; }
    public abstract float attackRange { get; set; }
    public abstract float attackCooldown { get; set; }

    [SerializeField] protected GameObject coinPrefab;
    
    
    // virtual keyword allows for overriding in derived classes
    // but defaults to the base class implementation
    public virtual float speed { get; set; } 
    protected virtual Animator animator { get; set; }
    
    protected Transform player { get; set; } 
    protected NavMeshAgent agent;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        
        agent = GetComponent<NavMeshAgent>();
        speed = agent.speed;
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // abstract keyword forces derived classes to implement these methods
    protected abstract void Update();

    protected abstract IEnumerator Attack();

    public virtual void TakeDamage(int damge)
    {
        health -= damge;

        if (health <= 0 )
        {
            Die();
            
        }

    }
    
    private void Die()
    {
        Debug.Log(name + " is invoking OnDeath!");
        OnDeath?.Invoke(this);
        
        if (agent != null)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }

        // Disable collisions if needed
        GetComponent<Collider>().enabled = false;

        
        this.enabled = false; 
    
        // Play death animation
        animator.SetFloat("Speed", 0);
        animator.SetBool("Dead", true);

        // Drop loot (if needed)
        for (int i = 0; i < 5; i++)
        {
            Vector3 spawnPosition = transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * 0.5f;
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
        
        // Destroy after animation
        StartCoroutine(DestroyAfterDelay(3f)); // Adjust time based on animation length
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
