using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitializer : MonoBehaviour
{
    public GameObject playerPrefab; // Reference to the player prefab
    private string selectedCosmetic;
    
    // Weapon prefabs
    public GameObject swordPrefab;
    public GameObject gunPrefab;
    public GameObject shotgunPrefab;
    public GameObject arPrefab;
    
    private Dictionary<string, GameObject> weaponPrefabs;
    
    void Start()
    {
        // Initialize weap dict.
        weaponPrefabs = new Dictionary<string, GameObject>
        {
            { "Sword", swordPrefab },
            { "Gun", gunPrefab },
            { "Shotgun", shotgunPrefab },
            { "AR", arPrefab }
        };
        
        // Check if we're in the game scene (not the menu scene)
        // This assumes your menu scene has a different name than your gameplay scenes
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            InitializePlayer();
        }
    }
    
    void InitializePlayer()
    {
        // Find the player in the scene
        PlayerController player = FindFirstObjectByType<PlayerController>();
        
        if (player == null)
        {
            Debug.LogError("Player not found in scene!");
            return;
        }
        
        // Apply saved health level
        int healthLevel = GameManager.Instance.playerData.healthLevel;
        float maxHealth = 100f;
        if (healthLevel > 1)
        { 
            maxHealth = 80 + (healthLevel * 20); // Assuming 20 health per level
        }
        player.SetMaxHealth(maxHealth);
        
        // Set CurrentHealth to new max Health
        Debug.Log("Max health is" + maxHealth);
        player.SetCurrentHealth(maxHealth);
        
        // Apply saved speed level
        int speedLevel = GameManager.Instance.playerData.speedLevel;
        if (speedLevel > 1)
        {
            player.speed += (speedLevel * 0.5f); // Assuming 0.5 speed per level
        }
        Debug.Log("Speed is " + player.speed);
        
        // Apply saved weapon
        string equippedWeapon = GameManager.Instance.playerData.equippedWeapon;
        ApplyWeaponToPlayer(player, equippedWeapon);

        // Apply the selected cosmetic
        selectedCosmetic = GameManager.Instance.playerData.selectedCosmetic;
        ApplyCosmeticToPlayer(playerPrefab, selectedCosmetic);
    }
    
    void ApplyWeaponToPlayer(PlayerController player, string weaponName)
    {
        // Check if the weapon exists in the dictionary
        if (weaponPrefabs.TryGetValue(weaponName, out GameObject weaponPrefab))
        {
            // Instantiate the weapon prefab
            GameObject weaponInstance = Instantiate(weaponPrefab, player.GetWeaponPos);

            // Optionally, set the equipped weapon in the PlayerController
            player.equippedWeapon = weaponInstance.GetComponent<IWeapon>();

            Debug.Log($"Equipped weapon: {weaponName}");
        }
        else
        {
            Debug.LogWarning($"Weapon '{weaponName}' not found in weapon dictionary!");
        }
    }
    
    void ApplyCosmeticToPlayer(GameObject player, string cosmeticName)
    {
        // Find the "Head" and "Face" objects in the player hierarchy
        Transform head = player.transform.Find("Player/Head");
        Transform face = player.transform.Find("Player/Face");

        if (head == null || face == null)
        {
            Debug.LogError("Head or Face object not found in player hierarchy!");
            return;
        }

        // Deactivate all cosmetics under Head and Face
        foreach (Transform child in head)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in face)
        {
            child.gameObject.SetActive(false);
        }

        // Activate the selected cosmetic
        Transform selectedCosmeticTransform = head.Find(cosmeticName) ?? face.Find(cosmeticName);
        if (selectedCosmeticTransform != null)
        {
            selectedCosmeticTransform.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Cosmetic '{cosmeticName}' not found under Head or Face!");
        }
    }
}