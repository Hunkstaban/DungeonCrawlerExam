using UnityEngine;

public class ForceSheildFollow : MonoBehaviour
{
    private PlayerController targetPlayer;

    public void Initialize(PlayerController player)
    {
        targetPlayer = player;
    }

    void Update()
    {
        transform.position = new Vector3(targetPlayer.transform.position.x, targetPlayer.transform.position.y, targetPlayer.transform.position.z);
    }
}