using UnityEngine;

public class CustomCameraController : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public float rotationSpeed = 5f; 
    private Vector3 targetPosition; 
    private Quaternion targetRotation; 
    private bool isMoving = false; 

    void Start()
    {
        // Set the initial target position and rotation to the camera's current state
        targetPosition = transform.position;
        targetRotation = transform.rotation;
    }

    void Update()
    {
        if (isMoving)
        {
            // Move position
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Rotate
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Stop moving if the camera is close enough to the target position and rotation
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f &&
                Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.position = targetPosition;
                transform.rotation = targetRotation;
                isMoving = false;
            }
        }
    }

    // Public method to move the camera to a target position and rotation
    public void MoveToTarget(Transform target)
    {
        if (target != null)
        {
            Debug.Log($"Moving to target: {target.name} at position {target.position} and rotation {target.rotation}");
            MoveToPosition(target.position, target.rotation);
        }
        else
        {
            Debug.Log("Target is null!");
        }
    }

    // Method to set the target position and rotation
    public void MoveToPosition(Vector3 newPosition, Quaternion newRotation)
    {
        Debug.Log($"Setting target position to: {newPosition} and target rotation to: {newRotation.eulerAngles}");
        targetPosition = newPosition;
        targetRotation = newRotation;
        isMoving = true;
    }
}
