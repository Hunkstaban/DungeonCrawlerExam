using System.Collections.Generic;
using UnityEngine;

public class SecondRoom : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public Animator summoningCircleAnimator;
    public RockGolem rockGolem; // Reference to the pre-placed Golem
    public GameObject summoningCircle;

    private int currentEnemies = 0;

    private void Awake()
    {
        Debug.Log("SecondRoom Awake: enemies count = " + enemies.Count);

        foreach (Enemy enemy in enemies)
        {
            if (enemy == null)
            {
                Debug.LogError("Enemy reference is null!");
                continue;
            }

            Debug.Log("Subscribing to enemy: " + enemy.name);
            enemy.OnDeath += HandleDeath;
        }

        currentEnemies = enemies.Count;
    }

    private void HandleDeath(Enemy deadEnemy)
    {
        
        enemies.Remove(deadEnemy);
        currentEnemies--;

        if (currentEnemies == 0)
        {
            summoningCircle.SetActive(true);
            summoningCircleAnimator.SetTrigger("Summon");
            rockGolem.gameObject.SetActive(true); // Activate the pre-placed Golem
            rockGolem.StartSpawning(); // Play spawn animation
        }
    }
}