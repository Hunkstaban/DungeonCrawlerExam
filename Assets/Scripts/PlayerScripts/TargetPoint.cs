using UnityEngine;

public class TargetPoint : MonoBehaviour
{
    public float gizmoSize = 0.5f;
    public Color gizmoColor = Color.red;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, gizmoSize);
    }
}