using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToSpawn; // Assign the prefab to spawn
    [SerializeField] private string groundTag = "Ground"; // Tag for ground detection
    private Bounds groundBounds;
    private int spawnAmount;
    private List<GameObject> spawnedPowerUps = new List<GameObject>();

    public void Start()
    {
        FindGround();
        
        for (int i = 0; i < spawnAmount; i++)
        {
            SpawnObjects();
        }
    }

    void FindGround()
    {
        GameObject ground = transform.Find(groundTag)?.gameObject;
        if (ground == null)
        {
            Debug.LogError("Ground object not found! Make sure the ground has the correct tag.");
            return;
        }

        Renderer renderer = ground.GetComponent<Renderer>();
        if (renderer == null)
        {
            Debug.LogError("Ground does not have a Renderer component!");
            return;
        }

        groundBounds = renderer.bounds;
        spawnAmount = (int)(Mathf.Ceil(groundBounds.size.x+groundBounds.size.z) / 16);
        // spawnAmount = (int)(groundBounds.size.x+groundBounds.size.z) * 20;
    }

    void SpawnObjects()
    {
        if (groundBounds.size == Vector3.zero) return;

        int maxAttempts = 20;
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 spawnPos = GetRandomPointOnGround();

            // Check if the spawn position is valid (e.g., no obstacles)
            if (IsValidSpawnPosition(spawnPos))
            {
                GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
                Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
                GameObject spawnedObject = Instantiate(objectToSpawn, spawnPos, randomRotation);
                spawnedPowerUps.Add(spawnedObject);

                return; // Exit after successful spawn
            }
        }
    }

    Vector3 GetRandomPointOnGround()
    {
        float randomX = Random.Range(groundBounds.min.x, groundBounds.max.x);
        float randomZ = Random.Range(groundBounds.min.z, groundBounds.max.z);
        float y = groundBounds.max.y + 1f;

        return new Vector3(randomX, y, randomZ);
    }

    bool IsValidSpawnPosition(Vector3 position)
    {
        RaycastHit hit;
        if (Physics.Raycast(position,Vector3.down, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag(groundTag))
            {
                position.y = hit.point.y;
                float checkRadius = 1f;
                
                Collider[] colliders = Physics.OverlapSphere(position, checkRadius);
                foreach (var col in colliders)
                {
                    if (col.GetComponent<CollectiblePowerUp>())
                    {
                        return false;
                    }
                }
                
                return true;
            }
        }
        return false;
    }
    
    public void RemovePowerUps()
    {
        foreach (var powerUp in spawnedPowerUps)
        {
            Destroy(powerUp);
        }
        spawnedPowerUps.Clear(); // Clear the list after destruction
    }
}