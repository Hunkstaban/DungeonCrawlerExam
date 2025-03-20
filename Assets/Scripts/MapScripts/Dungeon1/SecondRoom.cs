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
        rockGolem.OnDeath += HandleDeath;
        currentEnemies = enemies.Count;
    }

    private void HandleDeath(Enemy deadEnemy)
    {
        enemies.Remove(deadEnemy);
        currentEnemies--;

        if (deadEnemy == rockGolem) 
        {
            
            exitDoorAnimator.SetTrigger("Open");
            leftLight.SetActive(true);
            rightLight.SetActive(true);
            return;
        }

        if (currentEnemies == 0)
        {
            summoningCircle.SetActive(true);
            summoningCircleAnimator.SetTrigger("Summon");
            rockGolem.gameObject.SetActive(true); // Activate the pre-placed Golem
            rockGolem.StartSpawning(); // Play spawn animation
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