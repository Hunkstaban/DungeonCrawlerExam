using UnityEngine;
using UnityEngine.InputSystem;
public class CinemaCam : MonoBehaviour
{
    public GameObject player;
    public GameObject followTarget;
    public float sensitivity = 2f;
    // public float verticalClamp = 80f; // Limits vertical aim
    public GameObject targetPoint;
    
    private Vector2 lookInput;
    
    private float verticalRotation = 0f;
    public float verticalClamp = 80f; // Limit vertical rotation

    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        
        
        lookInput = Mouse.current.delta.ReadValue() * sensitivity * Time.deltaTime;
       
        player.transform.rotation *= Quaternion.Euler(0, lookInput.x, 0);
        // camContainer.transform.rotation = Quaternion.Euler(0, lookInput.x, 0);
       
        verticalRotation -= lookInput.y;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalClamp, verticalClamp);

        // Apply only the X rotation
        followTarget.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
       
    }

   
}
