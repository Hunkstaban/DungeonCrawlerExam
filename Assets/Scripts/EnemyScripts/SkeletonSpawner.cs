using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSpawner : Enemy
{
    
   [SerializeField] private Skeleton enemyPrefab;
    private int maxEnemies = 5;
    private int currentEnemies = 0;
    private float spawnRate = 3f;
    private bool isSpawning = false;
    
    private List<Skeleton> enemies = new List<Skeleton>();

    public Collider wanderZone;

    public override int health { get; set; } = 200;
    public override float attackRange { get; set; }
    public override float attackCooldown { get; set; }

    protected override void Awake()
    {
        
    }
    protected override void  Update()
    {
       

        if (enemies.Count < maxEnemies && !isSpawning)
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    protected override IEnumerator Attack()
    {
        throw new NotImplementedException();
    }

    private IEnumerator SpawnEnemy()
    {
            isSpawning = true;
            
            yield return new WaitForSeconds(spawnRate);
            
            Skeleton enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemy.wanderZone = wanderZone;

            enemy.OnDeath += HandleDeath(enemy);
            enemies.Add(enemy);
        
            
            
            
            isSpawning = false;
    }

    private Action<Enemy> HandleDeath(Skeleton skeleton)
    {
        return (enemy) =>
        {
            enemies.Remove(skeleton);
        };
    }
    
    public override void TakeDamage(int damge)
    {
        
        health -= damge;

        if (health <= 0 )
        {
            OnDeath?.Invoke(this);
            Destroy(gameObject);
            
        }
    }
    
   
}
