using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject projectile;

    [SerializeField] private Transform muzzle;

    [SerializeField] private float speed = 20f;

    [SerializeField] private bool multiShotEnable = false;
    [SerializeField] private bool shotgun = false;

    private bool shotgunAllowedToShot = true;
    
    
    public void Attack()
    {
        if (multiShotEnable)
        {
            multiShotFire();
        }
        else if (shotgun)
        {
            StartCoroutine(ShotgunFire());
        }
        else
        {
            singleShotFire();
        }
    }

    public void Remove()
    {
        throw new System.NotImplementedException();
    }

    public void singleShotFire()
    {
        GameObject bulletInstance = Instantiate(projectile, muzzle.position, muzzle.rotation);
        bulletInstance.GetComponent<Rigidbody>().AddForce(muzzle.forward * speed, ForceMode.Impulse);
        
        Destroy(bulletInstance, 3);
    }

    private IEnumerator ShotgunFire()
    {
        if (shotgunAllowedToShot)
        {
            shotgunAllowedToShot = false;
            
            Animator animator = GetComponent<Animator>();
            animator?.SetTrigger("Shoot");
            
            Vector3[] bulletOffsets = new Vector3[]
            {
                muzzle.position + transform.forward + transform.right * 1.0f, // Right
                muzzle.position + transform.forward - transform.right * 1.0f, // Left
                muzzle.position + transform.forward + transform.up * 0.5f, // Up
                muzzle.position + transform.forward - transform.up * 0.5f, // Down
                muzzle.position + transform.forward * 2.0f // Center
            };

            foreach (Vector3 offset in bulletOffsets)
            {
                GameObject bulletInstance = Instantiate(projectile, offset, muzzle.rotation);
                bulletInstance.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
                Destroy(bulletInstance, 5);
            }

            ShotgunCooldown shotgunCooldown = GetComponent<ShotgunCooldown>();
            shotgunCooldown?.StartCoroutine(shotgunCooldown.CoolDownDisplay(3));
            
            yield return new WaitForSeconds(3f);
            shotgunAllowedToShot = true;
        }
    }
    
    public void multiShotFire()
    {
        Vector3[] bulletOffsets = new Vector3[]
        {
            muzzle.position + transform.forward + transform.right * 1.0f, // Right
            muzzle.position + transform.forward - transform.right * 1.0f, // Left
            muzzle.position + transform.forward + transform.up * 0.5f,    // Up
            muzzle.position + transform.forward - transform.up * 0.5f,    // Down
            muzzle.position + transform.forward * 2.0f                   // Center
        };
        foreach (Vector3 offset in bulletOffsets)
        {
            GameObject bulletInstance = Instantiate(projectile, offset, muzzle.rotation);
            bulletInstance.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
            Destroy(bulletInstance, 5);
        }
    }
    
    public void AimAtTarget(Vector3 target)
    {
        transform.LookAt(target);
    }

    public void SetProjectile(GameObject projectile)
    {
        this.projectile = projectile;
    }

    public GameObject GetProjectileObject()
    {
        return projectile;
    }
    
    public IProjectile GetProjectile()
    {
        return projectile?.GetComponent<IProjectile>();
    }
    
    public Transform GetMuzzle()
    {
        return muzzle;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    
    public float GetSpeed()
    {
        return speed;
    }

    public void EnableMultiShot(bool enable)
    {
        multiShotEnable = enable;
    }
}