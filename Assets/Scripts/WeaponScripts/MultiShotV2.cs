using UnityEngine;

[CreateAssetMenu(menuName = "PowerUpsAndBuffs/MultiShotV2")]
public class MultiShotV2 : TimedPowerUp
{
    public override void ApplyPowerUp(PlayerController player)
    {
        player.equippedWeapon.EnableMultiShot(true);
        Debug.Log("MultiShot Activated");
    }

    public override void DeactivatePowerup(PlayerController player)
    {
        player.equippedWeapon.EnableMultiShot(false);
        Debug.Log("Times up! MultiShot Deactivated");
    }
}