using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUpsAndBuffs/SpeedBoost")]
public class SuperSpeed : TimedPowerUp
{
    public float boostAmount = 20f;
    
    public override void ApplyPowerUp(PlayerController player)
    {
        if (player != null)
        {
            player.speed += boostAmount;
            Debug.Log($"Speed boosted to {player.speed} for {durationInSeconds} seconds.");
        }
        else
        {
            Debug.LogError("PlayerController reference is missing!");
        }
    }

    public override void DeactivatePowerup(PlayerController player)
    {
        player.speed -= boostAmount;
        Debug.Log("Speed boost ended, speed restored.");
    }
}