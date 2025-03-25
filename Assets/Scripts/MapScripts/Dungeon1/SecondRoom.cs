using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondRoom : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public Animator summoningCircleAnimator;
    public RockGolem rockGolem; // Reference to the pre-placed Golem
    public GameObject summoningCircle;
    
    public Animator exitDoorAnimator;

    [SerializeField] private GameObject leftLight;
    [SerializeField] private GameObject rightLight;

    private int currentEnemies = 0;

    private void Awake()
    {
        Debug.Log("SecondRoom Awake: Initial enemies count = " + enemies.Count);

        foreach (Enemy enemy in enemies)
        {
            if (enemy == null)
            {
                Debug.LogError("Enemy reference is null in enemies list!");
                continue;
            }

            Debug.Log("Subscribing to enemy: " + enemy.name);
            enemy.OnDeath += HandleDeath;
        }
    
        if (rockGolem == null)
        {
            Debug.LogError("RockGolem reference is missing!");
        }
        else
        {
            rockGolem.OnDeath += HandleDeath;
        }

        currentEnemies = enemies.Count;
        Debug.Log("Enemies count after Awake: " + currentEnemies);
    }

    private void HandleDeath(Enemy deadEnemy)
    {
        if (!enemies.Contains(deadEnemy) && deadEnemy != rockGolem) return; // Prevent duplicate calls

        enemies.Remove(deadEnemy);
        currentEnemies--;

        Debug.Log($"Enemy {deadEnemy.name} died. Remaining: {currentEnemies}");

        // Check if the boss (rockGolem) died first
        if (deadEnemy == rockGolem) 
        {
            Debug.Log("Boss defeated, opening exit.");
            exitDoorAnimator.SetTrigger("Open");
            leftLight.SetActive(true);
            rightLight.SetActive(true);
            return; // Exit early so we don't check summoning logic
        }

        // Check if all normal enemies are dead, then summon the boss
        if (currentEnemies <= 0) 
        {
            Debug.Log("All enemies defeated, spawning the boss.");
            summoningCircle.SetActive(true);
            summoningCircleAnimator.SetTrigger("Summon");
            rockGolem.gameObject.SetActive(true);
            rockGolem.StartSpawning();
        }
    }


    
    // private IEnumerator SlideExitDoor()
    // {
    //     Vector3 startPosition = exitDoor.transform.position;
    //     Vector3 targetPosition = startPosition + new Vector3(0, 0, 5); // Adjust the Z-axis movement
    //
    //     float duration = 2f; 
    //     float elapsedTime = 0f;
    //
    //     while (elapsedTime < duration)
    //     {
    //         float t = elapsedTime / duration;
    //         exitDoor.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
    //         elapsedTime += Time.deltaTime;
    //         yield return null;
    //     }
    //
    //     exitDoor.transform.position = targetPosition; 
    //     Debug.Log("Door has moved to: " + exitDoor.transform.position);
    // }
}