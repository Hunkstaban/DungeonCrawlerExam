using System.Collections;
using UnityEngine;

public abstract class TimedPowerUp : PowerUp
{
    public float durationInSeconds = 5f;

    public IEnumerator StartPowerupCountdown(GameObject target)
    {
        yield return new WaitForSeconds(durationInSeconds);
    }
}