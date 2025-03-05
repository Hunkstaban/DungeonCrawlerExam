using UnityEngine;

// this will make its nice in the menu out in Unity when creating Script
[CreateAssetMenu(menuName = "PowerUpsAndBuffs/BulletVariants")]
public class BulletBuffVariants : PickUpEffect
{
    public int amount;
    public int fireRate;

    
    

    // in C# we use override when wanted to implement the inherited method (in this case from the abscract class "BulletEffect")
    public override void Apply(GameObject target)
    {
        if (target == null)
        {
            Debug.LogError("Target GameObject is null!");
            return; // Exit early if target is null
        }

        //target.GetComponent<Bullet>().Damage += amount;
        target.GetComponent<Gun>().speed += fireRate;
    }
}