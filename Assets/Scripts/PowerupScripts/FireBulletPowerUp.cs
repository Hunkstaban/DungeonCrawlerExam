using System.Collections;
using TMPro;
using UnityEngine;

public class FireBulletPowerUp : MonoBehaviour


{
   [SerializeField] public float countDownPowerUps = 7.0f;

    public bool fireballActive = false;

    public GameObject bullet;

    public TMP_Text powerUpText;

    void Start()
    {
        TextOnTop();
    }

    public void TextOnTop()
    {
        powerUpText.text = "Fireball";
    }


    void OnTriggerEnter(Collider onCollideWithSphere)
    {
        if (onCollideWithSphere.CompareTag("Player"))
        {
            PlayerController player = onCollideWithSphere.GetComponent<PlayerController>();
            if (player != null) // if capsulePlayer is NOT null
            {
              ActivateFireball();
              
            }
        }
    }

    // -------------------------- FireBall power up --------------------

    public void ActivateFireball()
    {
      

        PlayerController player = FindObjectOfType<PlayerController>();
        if (player == null)
        {
            Debug.Log("No player found");
            return;
        }

        StartCoroutine(Fireball(player));
    }

    public IEnumerator Fireball(PlayerController player)
    {
        if (bullet == null)
        {
            Debug.Log("bullet prefab is prob missing");
            yield break;
        }

        GetComponent<Renderer>().enabled = false; // to hide the Object before the destroy 
        GetComponent<Collider>().enabled = false; // to hide the Object before the destroy 
        
        GameObject baseBullets = player.equippedWeapon.GetProjectile(); // save the original bullets 


        player.equippedWeapon.SetProjectile(bullet);

        // create position
        Vector3 fireBallTransformPosition = transform.position + transform.forward * 2.0f;

        // instantiate the game object
        GameObject fireballInstantiate = Instantiate(bullet, fireBallTransformPosition, transform.rotation);
        if (fireballInstantiate != null)
        {
            fireballInstantiate.GetComponent<Rigidbody>().AddForce(transform.forward * 40.0f, ForceMode.Impulse);
        }

        Destroy(fireballInstantiate, 10);


        Debug.Log("still waiting on the countdown !!!");
        yield return new WaitForSeconds(countDownPowerUps);
        Debug.Log("FireBullet no more active. back to regular bullets!!!!");
        player.equippedWeapon.SetProjectile(baseBullets); // back to the original bullets after the seconds are over.
        Destroy(gameObject);
      
    }
}