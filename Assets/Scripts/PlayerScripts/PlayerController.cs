using TMPro;
using System;
using System.Collections;
using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    
    private Rigidbody rb;
    public float speed = 7f;
    public float jumpForce = 5f;
    public Transform camContainer; // Follows camera's horizontal rotation
    
    public GameObject targetPoint;
    
    private Vector2 movementInput;
    private bool isGrounded;

    public IWeapon equippedWeapon;
    public string selectedCosmetic;
    [SerializeField] private Transform weaponPos;
    public Transform GetWeaponPos => weaponPos;

    [SerializeField] private Volume volume;

    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI healthHUDNumber;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private TextMeshProUGUI roomsCleared;
    private float currentHealth;
    
    public float CurrentHealth
    {
        get => currentHealth;
        private set
        {
            currentHealth = Mathf.Clamp(value, 0, maxHealth);
            slider.value = currentHealth / maxHealth;
            healthHUDNumber.SetText(CurrentHealth.ToString());
        }
    }
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        if (volume) volume.weight = 0;
    }

    // comment
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

        if (CurrentHealth <= 30)
        {
            Vignette vignette;
            float pulse = Mathf.Sin(Time.time * 4f) * 0.05f;
            float volumeValue = (1 - (CurrentHealth / 20)) + pulse;
            float vignetteValue = Mathf.Lerp(0.25f, 1f, CurrentHealth / 20);
            
            volume.profile.TryGet(out vignette);
            vignette.intensity.value = vignetteValue;
            volume.weight = volumeValue;
        }
    }
    
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (currentHealth <= 0) Destroy(gameObject);
        
        // Vignette vignette;
        // volume.profile.TryGet(out vignette);
        //
        // vignette.intensity.value = 0.75f;
        // volume.weight = 1f;
        // vignette.intensity.value = 0.75f;
        // volume.weight = 1f;
        // StartCoroutine(ResetVignette(vignette));
    }
    
    // private IEnumerator ResetVignette(Vignette vignette)
    // {
    //     yield return new WaitForSeconds(0.2f); // Adjust delay as needed
    //     vignette.intensity.value = 0.25f;
    //     volume.weight = 0f;
    // }
    
    public void Heal(int amount)
    {
        CurrentHealth += amount;
        Debug.Log(CurrentHealth);
    }
    
    public void SetMaxHealth(float newMaxHealth) => maxHealth = newMaxHealth;
    
    public void SetCurrentHealth(float health) => CurrentHealth = health;

    public void SetRoomsClearedText(int level)
    {
        roomsCleared.SetText(level.ToString());
    }
}
