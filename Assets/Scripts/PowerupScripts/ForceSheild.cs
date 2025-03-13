using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "PowerUpsAndBuffs/ForceSheild")]
public class ForceSheild : TimedPowerUp
{
    [SerializeField] private GameObject forceSheildPrefab;
    private GameObject forceSheildInstantiate;
    public float forceSheildSize = 1.5f;
    public float sheildPushForce = 10f;

    // The distance the shield should be from the player
    public Vector3 shieldOffset = new Vector3(0, 0, 0); //Spawn slightly behind the player
    
    public override void ApplyPowerUp(PlayerController player)
    {
        // so we only instancitiate one forceSheild at a time
        if (forceSheildInstantiate != null)
        {
            return;
        } 

        // --------- so the forcefield spawns in the same rotation as the player --------------
        Quaternion spawnRotation = player.transform.rotation;

        forceSheildInstantiate = Instantiate(forceSheildPrefab, player.transform.position + shieldOffset, spawnRotation);

        //--------------Set the size---------------------------
        forceSheildInstantiate.transform.localScale =
            Vector3.one * forceSheildSize; // Vector.One means all "sides" x,y,z


       //  //----------------Set the Rigidbody---------------------------
       //  Rigidbody rb = forceSheildInstantiate.GetComponent<Rigidbody>();
       //  
       // if (rb == null) // if the sheild doesnt allready have a RB
       //  {
       //      rb = forceSheildInstantiate.AddComponent<Rigidbody>();
       //      rb.isKinematic = true; // to make the sphere interact with other colinders but it not effected by physics
       //  }
       // 
       
       //---------------Set the Collider--------------------------- 
       
       SphereCollider collider = forceSheildInstantiate.GetComponent<SphereCollider>();
       
       if (collider == null)
       {
           collider = forceSheildInstantiate.AddComponent<SphereCollider>();
       }
       
       // set the collider to the same radius as the forceSheild. 
       collider.radius = forceSheildSize;
        
       
       
       
       forceSheildInstantiate.AddComponent<ForceSheildFollow>().Initialize(player);
       
    }

  
    // public void UpdateShieldPosition(PlayerController player)
    // {
    //     if (forceSheildInstantiate != null)
    //     {
    //         // Follow the player's position (you can add an offset if you need)
    //         forceSheildInstantiate.transform.position = player.transform.position + shieldOffset ;
    //
    //         // Make the shield rotate with the player
    //         forceSheildInstantiate.transform.rotation = Quaternion.Euler(0, player.transform.rotation.eulerAngles.y, 0);
    //     }
    // }

    public override void DeactivatePowerup(PlayerController player)
    {
        if (forceSheildInstantiate != null)
        {
            Destroy(forceSheildInstantiate); // Destroy the shield when deactivating the power-up
            Debug.Log("Force Sheild Deactivated");
        }
    }
}