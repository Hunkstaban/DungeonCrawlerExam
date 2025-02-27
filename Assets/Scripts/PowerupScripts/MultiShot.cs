using System.Collections;
using TMPro;
using UnityEngine;

public class MultiShot : MonoBehaviour
{
    [SerializeField] public float countDownPowerUps = 7.0f;
    public TMP_Text powerUpText;

    void Start()
    {
        TextOnTop();
    }

    public void TextOnTop()
    {
        powerUpText.text = "MultiShot";
    }

    void OnTriggerEnter(Collider onCollideWithSphere)
    {
        if (onCollideWithSphere.CompareTag("Player"))
        {
            PlayerController player = onCollideWithSphere.GetComponent<PlayerController>();
            if (player != null)
            {
                ActivateMultiShot();
                if (gameObject != null)
                {
                    Debug.Log("MultiShot activated ");
                }
            }
        }
    }

    public void ActivateMultiShot()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player == null)
        {
            Debug.Log("No player found");
            return;
        }

        StartCoroutine(MultiShotPower(player));
    }
    

    public IEnumerator MultiShotPower(PlayerController player)
    {
        if (player == null) yield break;

        GetComponent<Renderer>().enabled = false; // to hide the Object before the destroy 
        GetComponent<Collider>().enabled = false; // to hide the Object before the destroy 
        
        player.equippedWeapon.EnableMultiShot(true);
        Debug.Log("MultiShot Activated");

        yield return new WaitForSeconds(countDownPowerUps);

        player.equippedWeapon.EnableMultiShot(false);
        Debug.Log("Times up! MultiShot Deactivated");
            Destroy(gameObject);
        }
    }
