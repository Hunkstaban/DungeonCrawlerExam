using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeMenuController : MonoBehaviour
{
    [Header("Health Upgrade")]
    public Button healthUpgradeButton;
    public TextMeshProUGUI healthLevelText;
    public TextMeshProUGUI healthCostText;
    
    [Header("Speed Upgrade")]
    public Button speedUpgradeButton;
    public TextMeshProUGUI speedLevelText;
    public TextMeshProUGUI speedCostText;
    
    [Header("Currency Display")]
    public TextMeshProUGUI coinsText;
    
    [Header("Navigation")]
    public Button weaponsMenuButton;
    public GameObject weaponsPanel;
    
    private void Start()
    {
        InitializeButtons();
        UpdateUI();
    }
    
    private void InitializeButtons()
    {
        healthUpgradeButton.onClick.AddListener(PurchaseHealthUpgrade);
        speedUpgradeButton.onClick.AddListener(PurchaseSpeedUpgrade);
        weaponsMenuButton.onClick.AddListener(OpenWeaponsMenu);
    }
    
    private void UpdateUI()
    {
        // Update level displays
        healthLevelText.text = $"Level: {GameManager.Instance.playerData.healthLevel}";
        speedLevelText.text = $"Level: {GameManager.Instance.playerData.speedLevel}";
        
        // Update cost displays
        healthCostText.text = $"{GameManager.Instance.GetHealthUpgradeCost()} coins";
        speedCostText.text = $"{GameManager.Instance.GetSpeedUpgradeCost()} coins";
        
        // Update coins display
        coinsText.text = $"Coins: {GameManager.Instance.playerData.coins}";
        
        // Disable buttons if not enough coins
        healthUpgradeButton.interactable = 
            GameManager.Instance.playerData.coins >= GameManager.Instance.GetHealthUpgradeCost();
        speedUpgradeButton.interactable = 
            GameManager.Instance.playerData.coins >= GameManager.Instance.GetSpeedUpgradeCost();
    }
    
    private void PurchaseHealthUpgrade()
    {
        int cost = GameManager.Instance.GetHealthUpgradeCost();
        
        if (GameManager.Instance.SpendCoins(cost))
        {
            GameManager.Instance.playerData.healthLevel++;
            GameManager.Instance.SaveGame();
            UpdateUI();
        }
    }
    
    private void PurchaseSpeedUpgrade()
    {
        int cost = GameManager.Instance.GetSpeedUpgradeCost();
        
        if (GameManager.Instance.SpendCoins(cost))
        {
            GameManager.Instance.playerData.speedLevel++;
            GameManager.Instance.SaveGame();
            UpdateUI();
        }
    }
    
    private void OpenWeaponsMenu()
    {
        weaponsPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
