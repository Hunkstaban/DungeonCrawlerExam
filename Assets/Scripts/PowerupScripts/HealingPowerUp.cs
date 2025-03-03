using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Powerups/Healing")]
public class HealingPowerUp : PowerUp
{
    public int healAmount = 30; // Adjust as needed
    public override void ApplyPowerUp(PlayerController player)
    {
        if (player != null)
        {
            player.Heal(healAmount);
            Debug.Log($"Player healed by {healAmount}. Current health: {player.CurrentHealth}");
        }
        else
        {
            Debug.LogError("PlayerController is null!");
        }
    }
}