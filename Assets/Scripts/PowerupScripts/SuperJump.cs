using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUpsAndBuffs/JumpBoost")]
public class SuperJump : TimedPowerUp
{
    [SerializeField] private float jumpMultiplier = 2f;

    public override void ApplyPowerUp(PlayerController player)
    {
        if (player != null)
        {
            player.jumpForce *= jumpMultiplier; // Increase jump force
            Debug.Log($"Jump boost activated! New jump force: {player.jumpForce}");
        }
        else
        {
            Debug.LogError("PlayerController is null!");
        }
    }

    public override void DeactivatePowerup(PlayerController player)
    {
        player.jumpForce /= jumpMultiplier; // Reset to original
        Debug.Log("Jump boost ended, jump force restored.");
    }
}