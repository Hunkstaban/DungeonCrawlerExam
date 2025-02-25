using UnityEngine;

public class AcidBullet : MonoBehaviour
{
    public float lifetime = 8f;
    public int damage = 10;

    void Start()
    {
        // Destroy the bullet after a certain time
        Debug.Log("AcidBullet spawned: " + gameObject.name);
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("AcidBullet collided with: " + collision.gameObject.name);
        
        /*if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }*/

        // Destroy the bullet on collision
        Destroy(gameObject);
    }
}