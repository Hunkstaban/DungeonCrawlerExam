using System;
using System.Collections.Generic;
using UnityEngine;

public class firstPack : MonoBehaviour
{
    
    public List<Enemy> enemies = new List<Enemy>();
    public Animator doorAnimator;
    public Collider wanderZone;
    private int currentEnemies = 0;
    private void Awake()
    {
        currentEnemies = enemies.Count;
        foreach (Skeleton enemy in enemies)
        {
            Skeleton newEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
            newEnemy.wanderZone = wanderZone;
            newEnemy.OnDeath += HandleDeath(newEnemy);
        }
    }

    
    private Action<Enemy> HandleDeath(Enemy skeleton)
    {
        
        return (enemy) =>
        {
            enemies.Remove(skeleton);
            
            currentEnemies--;
        
            // If there are no enemies left, open the door
            if (currentEnemies == 0)
            {
                doorAnimator.SetTrigger("Open");
            }
        };
        
    }
}
