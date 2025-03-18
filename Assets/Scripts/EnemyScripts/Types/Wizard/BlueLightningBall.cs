using UnityEngine;

public class BlueLightningBall : MonoBehaviour
{
   [SerializeField] private int damage = 15;
   private void OnCollisionEnter(Collision collision)
   {
       if (collision != null)
       {
           if (collision.gameObject.CompareTag("Player"))
           {
               PlayerController player = collision.gameObject.GetComponent<PlayerController>();
               if (player != null)
               {
                   player.TakeDamage(damage);
               }
           }
           Destroy(gameObject);
       }
       Destroy(gameObject, 4);
   }
}
