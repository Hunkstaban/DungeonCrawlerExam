using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 5f;
    public float jumpForce = 5f;
    public Transform camContainer; // Follows camera's horizontal rotation

    public GameObject targetPoint;
    
    private Vector2 movementInput;
    private bool isGrounded;

    public IWeapon equippedWeapon;
    
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    public int CurrentHealth
    {
        get => currentHealth;
        private set => currentHealth = Mathf.Clamp(value, 0, maxHealth);
    }
    
    void Start()
    {
        currentHealth = 50;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Debug.Log(CurrentHealth);
    }

    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    void OnJump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    
    void OnAttack()
    {
        equippedWeapon?.Attack();
    }


    void FixedUpdate()
    {
        equippedWeapon?.AimAtTarget(targetPoint.transform.position);
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        Vector3 moveDirection = (camContainer.forward * movementInput.y + camContainer.right * movementInput.x).normalized;
        moveDirection.y = 0;

        // Rotate player only on the Y-axis (Horizontal rotation)
        Quaternion targetRotation = Quaternion.Euler(0, camContainer.eulerAngles.y, 0);
        transform.rotation = targetRotation;

        rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);
    }
    
    public void Heal(int amount)
    {
        CurrentHealth += amount;
        Debug.Log(CurrentHealth);
    }
}
