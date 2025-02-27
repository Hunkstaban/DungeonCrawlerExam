using System.Collections;
using UnityEngine;

public class SpeedBoostPowerUp : PowerUp
{
    public float boostAmount = 20f;
    
    public override void ApplyPowerUp(PlayerController player)
    {
        if (player != null)
        {
            StartCoroutine(TemporarySpeedBoost(player));
        }
        else
        {
            Debug.LogError("PlayerController reference is missing!");
        }
    }

    private IEnumerator TemporarySpeedBoost(PlayerController player)
    {
        float originalSpeed = player.speed;
        player.speed += boostAmount;

        Debug.Log($"Speed boosted to {player.speed} for {duration} seconds.");
        yield return new WaitForSeconds(duration);
        
        player.speed = originalSpeed;
        Debug.Log("Speed boost ended, speed restored.");
        
        Destroy(gameObject);
    }
}