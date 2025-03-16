using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenuController : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI totalCoinsText;
    public TextMeshProUGUI roomRecordText; // Displays the player's total coins
    public TextMeshProUGUI healthCurrentLevelText; // Displays the current health level
    public TextMeshProUGUI speedCurrentLevelText; // Displays the current speed level
    public TextMeshProUGUI healthUpgradeCostText; // Displays the cost of the next health upgrade
    public TextMeshProUGUI speedUpgradeCostText; // Displays the cost of the next speed upgrade

    [Header("Upgrade Buttons")]
    public Button healthUpgradeButton; // Button to purchase health upgrades
    public Button speedUpgradeButton; // Button to purchase speed upgrades

    [Header("Upgrade Settings")]
    public int baseUpgradeCost = 50; // Base cost for upgrades
    public float costMultiplier = 1.5f; // Multiplier for upgrade costs 
    
    private Color defaultHealthCostColor; // Stores the initial color of the health cost text
    private Color defaultSpeedCostColor; // Stores the initial color of the speed cost text

    private void Start()
    {
        defaultHealthCostColor = healthUpgradeCostText.color;
        defaultSpeedCostColor = speedUpgradeCostText.color;
        
        // Initialize the UI with the player's current data
        UpdateUI();

        // Add listeners to the buttons
        healthUpgradeButton.onClick.AddListener(() => PurchaseUpgrade("Health"));
        speedUpgradeButton.onClick.AddListener(() => PurchaseUpgrade("Speed"));
    }

    private void UpdateUI()
    {
        // Update the total coins display
        totalCoinsText.text = $"{GameManager.Instance.playerData.coinBalance}";

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
}
