using UnityEngine;

[CreateAssetMenu(menuName = "PowerUpsAndBuffs/FireBulletPowerUpV2")]
public class FireBulletPowerUpV2 : TimedPowerUp
{
    public GameObject bullet;
    private GameObject baseBullets;

    public override void ApplyPowerUp(PlayerController player)
    {
        baseBullets = player.equippedWeapon.GetProjectile(); // save the original bullets
        player.equippedWeapon.SetProjectile(bullet);
    }

    public override void DeactivatePowerup(PlayerController player)
    {
        player.equippedWeapon.SetProjectile(baseBullets);
    }
}