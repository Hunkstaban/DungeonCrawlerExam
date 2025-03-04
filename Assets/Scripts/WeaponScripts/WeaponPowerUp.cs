using System;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponPowerUp : MonoBehaviour
{
  
   public PickUpEffect pickUpEffect;

   private void OnTriggerEnter(Collider collider)
   {
      
      if (pickUpEffect == null)
      {
         Debug.LogError("PickUpEffect is not assigned in the PowerUp script!");
         return; // Exit early if pickUpEffect is null
      }
      
      //Destroy after pickup
      Destroy(gameObject);
      // we want to apply that we collide with the gameObject. 
      pickUpEffect.Apply(collider.gameObject);
   }
}
