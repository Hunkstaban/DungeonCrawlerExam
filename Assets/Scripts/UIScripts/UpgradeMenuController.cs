using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenuController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI totalCoinsText;
    [SerializeField] private TextMeshProUGUI roomRecordText; 
    [SerializeField] private TextMeshProUGUI healthCurrentLevelText; 
    [SerializeField] private TextMeshProUGUI speedCurrentLevelText; 
    [SerializeField] private TextMeshProUGUI healthUpgradeCostText; 
    [SerializeField] private TextMeshProUGUI speedUpgradeCostText; 
    [SerializeField] private TextMeshProUGUI swordText;
    [SerializeField] private TextMeshProUGUI gunText;
    [SerializeField] private TextMeshProUGUI shotgunText;
    
    [Header("Upgrade Buttons")]
    [SerializeField] private Button healthUpgradeButton; 
    [SerializeField] private Button speedUpgradeButton; 

    [Header("Upgrade Settings")]
    [SerializeField] private int baseUpgradeCost = 50; 
    [SerializeField] private float costMultiplier = 1.5f; 
    
    private Color defaultHealthCostColor; // Stores the initial color of the health cost text
    private Color defaultSpeedCostColor; // Stores the initial color of the speed cost text
    private Color defaultColor;

    private string selectedWeapon;

    private void Start()
    {
        defaultHealthCostColor = healthUpgradeCostText.color;
        defaultSpeedCostColor = speedUpgradeCostText.color;
        defaultColor = swordText.color;
        
        // Initialize the UI with the player's current data
        UpdateUI();

        // Add listeners to the buttons
        healthUpgradeButton.onClick.AddListener(() => PurchaseUpgrade("Health"));
        speedUpgradeButton.onClick.AddListener(() => PurchaseUpgrade("Speed"));
    }

    private void UpdateUI()
    {
        // Update the total coins and room record display
        totalCoinsText.text = $"{GameManager.Instance.playerData.coinBalance}";
        roomRecordText.text = $"{GameManager.Instance.playerData.roomRecord}";

        // Update the current levels
        healthCurrentLevelText.text = $"{GameManager.Instance.playerData.healthLevel}";
        speedCurrentLevelText.text = $"{GameManager.Instance.playerData.speedLevel}";
        
        // Calculate cost and update text color if player can't afford upgrade
        int healthCost = CalculateUpgradeCost(GameManager.Instance.playerData.healthLevel);
        int speedCost = CalculateUpgradeCost(GameManager.Instance.playerData.speedLevel);
        healthUpgradeCostText.text = $"{CalculateUpgradeCost(GameManager.Instance.playerData.healthLevel)}";
        speedUpgradeCostText.text = $"{CalculateUpgradeCost(GameManager.Instance.playerData.speedLevel)}";
        
        // Change text color based on affordability
        healthUpgradeCostText.color = GameManager.Instance.playerData.coinBalance >= healthCost ? defaultHealthCostColor : Color.red;
        speedUpgradeCostText.color = GameManager.Instance.playerData.coinBalance >= speedCost ? defaultSpeedCostColor : Color.red;


        // Enable or disable buttons based on affordability
        healthUpgradeButton.interactable = GameManager.Instance.playerData.coinBalance >= CalculateUpgradeCost(GameManager.Instance.playerData.healthLevel);
        speedUpgradeButton.interactable = GameManager.Instance.playerData.coinBalance >= CalculateUpgradeCost(GameManager.Instance.playerData.speedLevel);
        
        // Set the color of the current equipped weapon
        HighlightEquippedWeapon(GameManager.Instance.playerData.equippedWeapon);
    }

    private int CalculateUpgradeCost(int currentLevel)
    {
        if (currentLevel <= 1)
        {
            // Return the base cost for level 1
            return baseUpgradeCost;
        }

        // Apply the multiplier for levels above 1
        float cost = baseUpgradeCost * Mathf.Pow(costMultiplier, currentLevel - 1);
        return Mathf.RoundToInt(cost); // Round to the nearest whole number
    }

    
    public void PurchaseHealthUpgrade()
    {
        PurchaseUpgrade("Health");
    }

    public void PurchaseSpeedUpgrade()
    {
        PurchaseUpgrade("Speed");
    }


    private void PurchaseUpgrade(string upgradeType)
    {
        if (upgradeType == "Health")
        {
            int cost = CalculateUpgradeCost(GameManager.Instance.playerData.healthLevel);
            if (GameManager.Instance.SpendCoins(cost))
            {
                // Increase the health level
                GameManager.Instance.playerData.healthLevel++;
                GameManager.Instance.SaveData();
                UpdateUI();
            }
        }
        else if (upgradeType == "Speed")
        {
            int cost = CalculateUpgradeCost(GameManager.Instance.playerData.speedLevel);
            if (GameManager.Instance.SpendCoins(cost))
            {
                // Increase the speed level
                GameManager.Instance.playerData.speedLevel++;
                GameManager.Instance.SaveData();
                UpdateUI();
            }
        }
    }

    // Called when a weapon is selected from the UI
    public void SelectWeapon(string weaponName)
    {
        // Save the selected weapon
        selectedWeapon = weaponName;
        GameManager.Instance.playerData.equippedWeapon = selectedWeapon;
        GameManager.Instance.SaveData();

        // Highlight the selected weapon
        HighlightEquippedWeapon(weaponName);
    }

    // Highlights the equipped weapon by changing its text color
    private void HighlightEquippedWeapon(string weaponName)
    {
        // Reset all weapon text colors to default
        swordText.color = defaultColor;
        gunText.color = defaultColor;
        shotgunText.color = defaultColor;

        // Change the color of the equipped weapon to green
        switch (weaponName)
        {
            case "Sword":
                swordText.color = Color.green;
                break;
            case "Gun":
                gunText.color = Color.green;
                break;
            case "Shotgun":
                shotgunText.color = Color.green;
                break;
            default:
                Debug.LogWarning("Unknown weapon equipped: " + weaponName);
                break;
        }
    }
}
