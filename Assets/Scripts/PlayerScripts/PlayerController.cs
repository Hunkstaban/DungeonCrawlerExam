using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 5f;
    public float jumpForce = 5f;
    public Transform camContainer; // Follows camera's horizontal rotation
    
    [SerializeField]
    private int maxHealth = 100;

    public GameObject targetPoint;
    
    private Vector2 movementInput;
    private bool isGrounded;

    public IWeapon equippedWeapon;
    
    private int currentHealth;

    public int CurrentHealth
    {
        get => currentHealth;
        private set => currentHealth = Mathf.Clamp(value, 0, maxHealth);
    }
    
    void Start()
    {
        currentHealth = 100;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Debug.Log("Current Health: " + CurrentHealth);
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
        // Aim at target if applicable.
        equippedWeapon?.AimAtTarget(targetPoint.transform.position);
    
        // Ground check.
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        // Calculate movement direction.
        Vector3 moveDirection = (camContainer.forward * movementInput.y + camContainer.right * movementInput.x).normalized;
        moveDirection.y = 0; // Ensure no vertical movement.

        // Set Rigidbody velocity while preserving the current Y velocity.
        rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);

        // Calculate target rotation based on the camera's Y rotation.
        Quaternion targetRotation = Quaternion.Euler(0, camContainer.eulerAngles.y, 0);

        // Rotate using Rigidbody's MoveRotation for smooth physics-based rotation.
        rb.MoveRotation(targetRotation);
    }
    public int getHealth()
    {
        return currentHealth;
    }
    public void setHealth(int health)
    {
        currentHealth = health;
    }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (currentHealth <= 0) Destroy(gameObject);
    }
    public void Heal(int amount)
    {
        CurrentHealth += amount;
        Debug.Log(CurrentHealth);
    }
}
