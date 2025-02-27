using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public Sprite powerUpIcon;
    public abstract void ApplyPowerUp(PlayerController player);
}