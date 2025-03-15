using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponsMenuController : MonoBehaviour
{
    [System.Serializable]
    public class WeaponUIItem
    {
        public string weaponName;
        public Button purchaseButton;
        public Button selectButton;
        public TextMeshProUGUI costText;
        public GameObject purchasedIndicator;
    }
    
    public WeaponUIItem[] weaponUIItems;
    public Button backButton;
    public GameObject upgradePanel;
    public TextMeshProUGUI coinsText;
    
    private void Start()
    {
        InitializeButtons();
        UpdateUI();
    }
    
    private void InitializeButtons()
    {
        foreach (var item in weaponUIItems)
        {
            var localItem = item;
            
            // Purchase button
            item.purchaseButton.onClick.AddListener(() => {
                PurchaseWeapon(localItem);
            });
            
            // Select button
            item.selectButton.onClick.AddListener(() => {
                SelectWeapon(localItem);
            });
        }
        
        backButton.onClick.AddListener(() => {
            upgradePanel.SetActive(true);
            gameObject.SetActive(false);
        });
    }
    
    private void UpdateUI()
    {
        coinsText.text = $"Coins: {GameManager.Instance.playerData.coins}";
        
        foreach (var item in weaponUIItems)
        {
            bool isPurchased = GameManager.Instance.playerData.purchasedWeapons.Contains(item.weaponName);
            bool isEquipped = GameManager.Instance.playerData.equippedWeapon == item.weaponName;
            
            // Find the weapon info to get the cost
            int weaponCost = 0;
            foreach (var weaponInfo in GameManager.Instance.availableWeapons)
            {
                if (weaponInfo.weaponName == item.weaponName)
                {
                    weaponCost = weaponInfo.cost;
                    break;
                }
            }
            
            // Update UI elements
            item.costText.text = $"{weaponCost} coins";
            item.purchasedIndicator.SetActive(isPurchased);
            item.purchaseButton.gameObject.SetActive(!isPurchased);
            item.selectButton.gameObject.SetActive(isPurchased);
            item.selectButton.interactable = !isEquipped;
            
            // Update purchase button interactability based on coins
            item.purchaseButton.interactable = GameManager.Instance.playerData.coins >= weaponCost;
        }
    }
    
    private void PurchaseWeapon(WeaponUIItem item)
    {
        // Find weapon cost
        int weaponCost = 0;
        foreach (var weaponInfo in GameManager.Instance.availableWeapons)
        {
            if (weaponInfo.weaponName == item.weaponName)
            {
                weaponCost = weaponInfo.cost;
                break;
            }
        }
        
        if (GameManager.Instance.SpendCoins(weaponCost))
        {
            // Add to purchased weapons list
            GameManager.Instance.playerData.purchasedWeapons.Add(item.weaponName);
            
            // Auto-equip the first weapon purchased
            if (GameManager.Instance.playerData.purchasedWeapons.Count == 1)
            {
                GameManager.Instance.playerData.equippedWeapon = item.weaponName;
            }
            
            GameManager.Instance.SaveGame();
            UpdateUI();
        }
    }
    
    private void SelectWeapon(WeaponUIItem item)
    {
        GameManager.Instance.playerData.equippedWeapon = item.weaponName;
        GameManager.Instance.SaveGame();
        UpdateUI();
    }
}
