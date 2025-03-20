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
        // Sets the position of the current object to match the target player's position.
        transform.position = new Vector3(targetPlayer.transform.position.x, targetPlayer.transform.position.y, targetPlayer.transform.position.z);
    }

    // using OnCollisionStay instead of OnTriggerStay ( both works in every frame) because OnCollosionStay works on non-trigger colliders
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            return;
        }
        
        if (collision.collider.CompareTag("Enemy"))
        {
            Rigidbody enemyRb = collision.collider.GetComponent<Rigidbody>();
            if (enemyRb != null)
            {
               
                Vector3 pushDirection = collision.transform.position - transform.position; // Direction away from shield
                enemyRb.AddForce(pushDirection.normalized * forceSheild.sheildPushForce, ForceMode.Impulse); // Adjust strength as needed
            }
        }
    }
    
    // OnTriggerEnter is just the frame some1 enters
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            return;
        }
        
        if (collision.collider.CompareTag("Enemy"))
        {
            Rigidbody enemyRb = collision.collider.GetComponent<Rigidbody>();
            if (enemyRb != null)
            {
                Vector3 pushDirection = collision.transform.position - transform.position; // Direction away from shield
                enemyRb.AddForce(pushDirection.normalized * forceSheild.sheildPushForce, ForceMode.Impulse); // Adjust strength as needed
            }
        }
    }
    

}