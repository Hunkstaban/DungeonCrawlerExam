using System;
using UnityEngine;

public class ForceSheildFollow : MonoBehaviour
{
    private ForceSheild forceSheild;
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

    // OnTriggerStay is every frame
    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            return;
        }
        
        if (collider.CompareTag("Enemy"))
        {
            Rigidbody enemyRb = collider.GetComponent<Rigidbody>();
            if (enemyRb != null)
            {
                Vector3 pushDirection = collider.transform.position - transform.position; // Direction away from shield
                enemyRb.AddForce(pushDirection.normalized * forceSheild.sheildPushForce, ForceMode.Impulse); // Adjust strength as needed
            }
        }
    }
    
    // OnTriggerEnter is just the frame some1 enters
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            return;
        }
        
        if (collider.CompareTag("Enemy"))
        {
            Rigidbody enemyRb = collider.GetComponent<Rigidbody>();
            if (enemyRb != null)
            {
                Vector3 pushDirection = collider.transform.position - transform.position; // Direction away from shield
                enemyRb.AddForce(pushDirection.normalized * forceSheild.sheildPushForce, ForceMode.Impulse); // Adjust strength as needed
            }
        }
    }
    

}