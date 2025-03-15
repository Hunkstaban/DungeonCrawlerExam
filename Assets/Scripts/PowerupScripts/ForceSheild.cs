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
    
    
    [SerializeField]private AudioClip forceSheildActivateSound;
    private AudioSource audioSource;
    
    
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
        
       
       //---------------Set the Collider--------------------------- 
       
       SphereCollider collider = forceSheildInstantiate.GetComponent<SphereCollider>();
       
       if (collider == null)
       {
           collider = forceSheildInstantiate.AddComponent<SphereCollider>();
       }
       
       // set the collider to the same radius as the forceSheild. 
       collider.radius = forceSheildSize;
       
       // instantiate and call the "Initialize" method thats on the ForceSheildFollow class. so the sheild follows the player
       forceSheildInstantiate.AddComponent<ForceSheildFollow>().Initialize(player);

       if (forceSheildActivateSound != null)
       {
           audioSource = player.gameObject.AddComponent<AudioSource>(); // add the sound to the player 
           audioSource.PlayOneShot(forceSheildActivateSound); // plays the sound in the audioSource (PlayOneShot = only plays once and doesnt interrup others sound that may be playing.)
       }

    }

  


    public override void DeactivatePowerup(PlayerController player)
    {
        if (forceSheildInstantiate != null)
        {
            Destroy(forceSheildInstantiate); // Destroy the shield when deactivating the power-up
            Debug.Log("Force Sheild Deactivated");
        }
    }
}