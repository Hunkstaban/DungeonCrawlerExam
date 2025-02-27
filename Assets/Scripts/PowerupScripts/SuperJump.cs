using System.Collections;
using TMPro;
using UnityEngine;

public class SuperJump : MonoBehaviour
{
    
    [SerializeField] public float countDownPowerUps = 7.0f;
    [SerializeField] public float multiplierSuperJump = 20.0f;
    public TMP_Text powerUpText;

    void Start()
    {
        TextOnTop();
    }

    public void TextOnTop()
    {
        powerUpText.text = "SuperJump";
    }


    void OnTriggerEnter(Collider onCollideWithSphere)
    {
        if (onCollideWithSphere.CompareTag("Player"))
        {
            PlayerController player = onCollideWithSphere.GetComponent<PlayerController>();
            if (player != null) // if PlayerMoment is NOT null
            {
                ActivateSuperJump();
                Debug.Log("PowerUpSuperJump activated ");
            }
        }
    }

    // ---------------- Super Jump Power Up -----------------------------

    // IEnumerator → Defines a coroutine (enables pausing executions without. pausing the actual game. (must return a yield)).
    // StartCoroutine → Starts the coroutine.

    public void ActivateSuperJump()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        StartCoroutine(SuperJumpPower(player));
    }

    public IEnumerator SuperJumpPower(PlayerController player)
    {
    
      
        if (player != null)
        {
            
            GetComponent<Renderer>().enabled = false; // to hide the Object before the destroy 
            GetComponent<Collider>().enabled = false; // to hide the Object before the destroy 
            
            player.jumpForce *= multiplierSuperJump;
            Debug.Log("Jump Power activated for" + countDownPowerUps + " seconds");
            yield return new WaitForSeconds(countDownPowerUps);
            player.jumpForce /= multiplierSuperJump; //  devides - resets the jumpjumpHeight 
            Debug.Log("Jump Power disabled ");
            Destroy(gameObject);
        }
    }
}
