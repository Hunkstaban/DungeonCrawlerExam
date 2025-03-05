using UnityEngine;

[CreateAssetMenu(menuName = "PowerUpsAndBuffs/BulletBuffVariantsV2")]
public class BulletBuffVariantsV2 : TimedPowerUp
{
    [SerializeField] private int additionalDamageAmount;
    [SerializeField] private int fireRate;
    private float weaponSpeed;
    private int bulletDamage;
    
    public override void ApplyPowerUp(PlayerController player)
    {
        weaponSpeed = player.equippedWeapon.GetSpeed();
        bulletDamage = player.equippedWeapon.GetProjectile().GetDamage();

        player.equippedWeapon.GetProjectile().SetDamage(bulletDamage + additionalDamageAmount);
        player.equippedWeapon.SetSpeed(weaponSpeed + fireRate);
    }

    public override void DeactivatePowerup(PlayerController player)
    {
        player.equippedWeapon.GetProjectile().SetDamage(bulletDamage);
        player.equippedWeapon.SetSpeed(weaponSpeed);
    }
}