using UnityEngine;

public class fireBullet1 : MonoBehaviour
{
    public int damage = 30;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        Destroy(gameObject, 3);
    }
}
