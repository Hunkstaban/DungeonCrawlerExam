using UnityEngine;

public class BombRadiusFollow : MonoBehaviour
{
    private GameObject targetBomb;
    private RaycastHit hit;

    public void Initialize(GameObject bomb)
    {
        targetBomb = bomb;
        Physics.Raycast(bomb.transform.position, Vector3.down, out hit);
    }

    void Update()
    {
        if (targetBomb != null)
        {
            transform.position = new Vector3(
                targetBomb.transform.position.x,
                hit.transform.position.y + 0.1f,
                targetBomb.transform.position.z);
        }
    }
}