using UnityEngine;

// this will make its nice in the menu out in Unity when creating Script
[CreateAssetMenu(menuName = "BulletVariants")]
public class BulletVariants : BulletEffect
{
    
    public int amount;
    
    // in C# we use override when wanted to implement the inherited method (in this case from the abscract class "BulletEffect")
    public override void Apply(GameObject bullet)
    {
        bullet.GetComponent<Bullet>().damage += amount;
    }
}
