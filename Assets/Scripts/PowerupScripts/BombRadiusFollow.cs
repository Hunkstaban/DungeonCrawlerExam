using UnityEngine;

public class BombRadiusFollow : MonoBehaviour
{
    private GameObject targetBomb;
    private float fixedYPosition = 0.1f; // Adjust as needed

    public void Initialize(GameObject bomb)
    {
        targetBomb = bomb;
    }

    void Update()
    {
        if (targetBomb != null)
        {
            transform.position = new Vector3(targetBomb.transform.position.x, fixedYPosition, targetBomb.transform.position.z);
        }
    }
}