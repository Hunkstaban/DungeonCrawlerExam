using UnityEngine;

// this will make its nice in the menu out in Unity when creating Script
[CreateAssetMenu(menuName = "PowerUpsAndBuffs/SuperSpeed")]
public class SuperSpeed : PickUpEffect
{
    
    public float amountSpeed;
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerController>().speed += amountSpeed;
    }
}
