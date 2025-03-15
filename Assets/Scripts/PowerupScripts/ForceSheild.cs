using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "PowerUpsAndBuffs/ForceSheild")]


public class ForceSheild : TimedPowerUp
{
    [SerializeField][Header("Force Shield Prefab")] private GameObject forceSheildPrefab;
    private GameObject forceSheildInstantiate;
    [Header("Force Shield Size")] public float forceSheildSize = 1.3f;
    [Header("Push Strength")]public float sheildPushForce = 10f;

    // The distance the shield should be from the player
   [SerializeField] [Header("Placement")] public Vector3 shieldOffset = new Vector3(0, 0, 0); 
    
    
    [SerializeField] [Header("Sound Clip")]private AudioClip forceSheildActivateSound;
    private AudioSource audioSource;
    
    
    public override void ApplyPowerUp(PlayerController player)
    {
        // so we only instancitiate one forceSheild at a time
        if (forceSheildInstantiate != null)
        {
            return;
        } 

        //so the forcefield spawns in the same rotation as the player 
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
       
       // ----- set the collider to the same radius as the forceSheild. ------
       collider.radius = forceSheildSize;
       
       // instantiate and call the "Initialize" method thats on the ForceSheildFollow class. so the sheild follows the player
       forceSheildInstantiate.AddComponent<ForceSheildFollow>().Initialize(player);

       // ---------------- add the sound effect ---------------
       
       if (forceSheildActivateSound != null)
       {
           audioSource = player.gameObject.AddComponent<AudioSource>(); // add the Audio Source component to the player 
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