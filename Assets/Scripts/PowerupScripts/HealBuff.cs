using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "PowerUpsAndBuffs/Healing")]
public class HealBuff : PowerUp
{
    [SerializeField] private int healAmount = 30; // Adjust as needed
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