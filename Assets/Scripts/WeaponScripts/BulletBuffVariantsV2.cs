using UnityEngine;

[CreateAssetMenu(menuName = "PowerUpsAndBuffs/BulletBuffVariantsV2")]
public class BulletBuffVariantsV2 : TimedPowerUp
{
    public int damageAmount;
    public int fireRate;
    private float weaponSpeed;
    private int bulletDamage;
    
    public override void ApplyPowerUp(PlayerController player)
    {
        weaponSpeed = player.equippedWeapon.GetSpeed();
        bulletDamage = player.equippedWeapon.GetProjectile().Damage;

        player.equippedWeapon.GetProjectile().Damage = bulletDamage + damageAmount;
        player.equippedWeapon.SetSpeed(weaponSpeed + fireRate);
    }

    public override void DeactivatePowerup(PlayerController player)
    {
        player.equippedWeapon.GetProjectile().Damage = bulletDamage - damageAmount;
        player.equippedWeapon.SetSpeed(weaponSpeed - fireRate);
    }
}