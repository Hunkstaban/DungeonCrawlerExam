using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    public int damage = 50;

    public Animator animator;
    
    private Collider collider;
        
    private bool canAttack = true;
    
    private void Start()
    {
        collider = GetComponent<Collider>();
        collider.enabled = false;
    }
    private void OnTriggerEnter(Collider collider)
    {
        canAttack = false;
        
        if (collider.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
        // this.collider.enabled = false;
        canAttack = true;
    }

    public void Attack()
    {
        if (canAttack)
        { 
            collider.enabled = true;
            animator.SetTrigger("Attack");
        }
        
    }

    public void AimAtTarget(Vector3 target)
    {
        // transform.LookAt(target);
    }

    public void SetProjectile(GameObject projectile)
    {
        throw new System.NotImplementedException();
    }

    public GameObject GetProjectileObject()
    {
        throw new System.NotImplementedException();
    }

    IProjectile IWeapon.GetProjectile()
    {
        throw new System.NotImplementedException();
    }

    public void SetSpeed(float speed)
    {
        throw new System.NotImplementedException();
    }

    public float GetSpeed()
    {
        throw new System.NotImplementedException();
    }

    public GameObject GetProjectile()
    {
        throw new System.NotImplementedException();
    }

    public float SetSpeed(GameObject speed)
    {
        throw new System.NotImplementedException();
    }

    public Transform GetMuzzle()
    {
        throw new System.NotImplementedException();
    }

    public void Remove()
    {
        Destroy(gameObject);
    }

    public void EnableMultiShot(bool enable)
    {
        throw new System.NotImplementedException();
    }
}
