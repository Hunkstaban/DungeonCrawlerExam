using System.Collections;
using UnityEngine;

public class JumpBoostPowerUp : PowerUp
{
    public float jumpMultiplier = 2f;

    public override void ApplyPowerUp(PlayerController player)
    {
        if (player != null)
        {
            StartCoroutine(TemporaryJumpBoost(player));
        }
        else
        {
            Debug.LogError("PlayerController is null!");
        }
    }

    private IEnumerator TemporaryJumpBoost(PlayerController player)
    {
        float originalJumpForce = player.jumpForce;
        player.jumpForce *= jumpMultiplier; // Increase jump force
        Debug.Log($"Jump boost activated! New jump force: {player.jumpForce}");

        yield return new WaitForSeconds(duration); // Wait for duration

        player.jumpForce = originalJumpForce; // Reset to original
        Debug.Log("Jump boost ended, jump force restored.");
        
        Destroy(gameObject); // Remove power-up object
    }
}