using System;
using UnityEngine;

public class Coin : MonoBehaviour
{

    private PlayerController player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    
    private void Update()
    {
        coinPickup();
    }

    // a method which makes the coins fly towards the player when they are in range and when they collide they are "picked up"
    private void coinPickup()
    {
        // 3. Calculate the direction from the coin to the player
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        
        // 4. Move the coin towards the player
        transform.position += direction * Time.deltaTime * 5;
        
        // 5. Check if the coin is close enough to the player
        if (Vector3.Distance(transform.position, player.transform.position) < 1)
        {
            // 6. Destroy the coin
            Destroy(gameObject);
        }
        
    }
}
