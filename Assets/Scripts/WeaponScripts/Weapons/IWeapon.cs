using UnityEngine;

public interface IWeapon
{
    void Attack();
    
    void AimAtTarget(Vector3 target);
    
    void SetProjectile(GameObject projectile);
    GameObject GetProjectile();
    
    float SetSpeed(GameObject speed);

    void EnableMultiShot(bool enable);

    void Remove();
}