using UnityEngine;

public abstract class PowerUp : ScriptableObject
{
    public Sprite powerUpIcon;
    public abstract void ApplyPowerUp(PlayerController player);
}