using System;
using UnityEngine;

public class BulletPowerUp : MonoBehaviour
{
   public BulletEffect bulletEffect;

   private void OnTriggerEnter(Collider collider)
   {
      //Destroy after pickup
      Destroy(gameObject);
      // we want to apply that we collide with the gameObject. 
      bulletEffect.Apply(collider.gameObject);
   }
}
