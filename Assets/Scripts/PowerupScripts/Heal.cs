using UnityEngine;

[CreateAssetMenu(menuName = "PowerUpsAndBuffs/Heals")]
public class Heal : PickUpEffect
{
    public int healAmount;
    public override void Apply(GameObject target)
    {
     // target.GetComponent<PlayerController>().currentHealth += healAmount;   
    }
}
