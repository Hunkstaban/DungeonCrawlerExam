using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour

{
    // abstract keyword forces derived classes to implement these properties
    public abstract int health { get; set; }
    public abstract float attackRange { get; set; }
    public abstract float attackCooldown { get; set; }
    
    // virtual keyword allows for overriding in derived classes
    // but defaults to the base class implementation
    public virtual float speed { get; set; } 
    protected virtual Animator animator { get; set; }
    
    protected Transform player { get; set; } 
    protected NavMeshAgent agent;

    protected void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        speed = agent.speed;
        animator = GetComponent<Animator>();
        
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
            Destroy(gameObject);
        }

    }
}
