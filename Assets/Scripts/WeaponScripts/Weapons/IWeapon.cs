using UnityEngine;

public interface IWeapon
{
    void Attack();
    
    void AimAtTarget(Vector3 target);
    
    void SetProjectile(GameObject projectile);
    GameObject GetProjectile();
    
    void SetSpeed(float speed);
    
    float GetSpeed();

    void EnableMultiShot(bool enable);

    Transform GetMuzzle();

    void Remove();
}