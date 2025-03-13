using System;
using UnityEngine;

public class ForceSheildFollow : MonoBehaviour
{
    private PlayerController targetPlayer;
    private Collider sheildCollider;
    private Collider playerCollider;

    public void Initialize(PlayerController player)
    {
        targetPlayer = player;
        
        // Get the colliders
        sheildCollider = GetComponent<Collider>();
        playerCollider = targetPlayer.GetComponent<Collider>();

        // Ignore collision between shield and player
        Physics.IgnoreCollision(sheildCollider, playerCollider);
    }

    void Update()
    {
        transform.position = new Vector3(targetPlayer.transform.position.x, targetPlayer.transform.position.y, targetPlayer.transform.position.z);
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            return;
        }
        
        if (collider.CompareTag("Enemy"))
        {
            //push back 
        }
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            return;
        }
        
        if (collider.CompareTag("Enemy"))
        {
            //push back 
        }
    }

}