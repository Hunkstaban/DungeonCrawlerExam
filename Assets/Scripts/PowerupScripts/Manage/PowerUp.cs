using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public Sprite powerUpIcon;
    public float duration = 5f;
    public abstract void ApplyPowerUp(PlayerController player);
}