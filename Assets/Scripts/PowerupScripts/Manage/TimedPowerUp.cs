using System.Collections;
using UnityEngine;

public abstract class TimedPowerUp : PowerUp
{
    public float durationInSeconds = 5f;

    public abstract void DeactivatePowerup(PlayerController player);

    public IEnumerator StartPowerupCountdown(PlayerController player)
    {
        yield return new WaitForSeconds(durationInSeconds);
        
        DeactivatePowerup(player);
    }
}