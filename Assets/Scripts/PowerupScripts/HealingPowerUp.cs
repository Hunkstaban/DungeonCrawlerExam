using UnityEngine;
using UnityEngine.UI;

public class HealingPowerUp : PowerUp
{
    public int healAmount = 30; // Adjust as needed
    public override void ApplyPowerUp(PlayerController player)
    {
        if (player != null)
        {
            player.Heal(healAmount);
            Debug.Log($"Player healed by {healAmount}. Current health: {player.CurrentHealth}");
            Destroy(gameObject); // Remove power-up after use
        }
        else
        {
            Debug.LogError("PlayerController is null!");
        }
    }
}