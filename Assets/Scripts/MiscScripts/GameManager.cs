using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public PlayerData playerData;
    
    // Upgrade costs and multipliers
    public int baseHealthUpgradeCost = 100;
    public int baseSpeedUpgradeCost = 100;
    public float upgradeCostMultiplier = 1.5f;
    
    // Dictionary of weapon costs
    public WeaponInfo[] availableWeapons;
    
    [System.Serializable]
    public class WeaponInfo
    {
        public string weaponName;
        public int cost;
        public GameObject weaponPrefab;
    }
    
    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void LoadGame()
    {
        playerData = SaveSystem.LoadData();
    }
    
    public void SaveGame()
    {
        SaveSystem.SaveData(playerData);
    }
    
    public int GetHealthUpgradeCost()
    {
        return Mathf.RoundToInt(baseHealthUpgradeCost * 
            Mathf.Pow(upgradeCostMultiplier, playerData.healthLevel));
    }
    
    public int GetSpeedUpgradeCost()
    {
        return Mathf.RoundToInt(baseSpeedUpgradeCost * 
            Mathf.Pow(upgradeCostMultiplier, playerData.speedLevel));
    }
    
    public void ApplyUpgradesToPlayer(PlayerController player)
    {
        if (player == null) return;
    
        // Apply health multiplier (level 0 = 1.0x, level 1 = 1.1x, etc.)
        float healthMultiplier = 1f + (playerData.healthLevel * 0.1f);
        player.SetMaxHealth(Mathf.RoundToInt(100 * healthMultiplier));
    
        // Apply speed multiplier (level 0 = 1.0x, level 1 = 1.1x, etc.)
        float speedMultiplier = 1f + (playerData.speedLevel * 0.1f);
        player.speed = 5f * speedMultiplier;
    }
    
    public void AddCoins(int amount)
    {
        playerData.coins += amount;
        SaveGame();
    }
    
    public bool SpendCoins(int amount)
    {
        if (playerData.coins >= amount)
        {
            playerData.coins -= amount;
            SaveGame();
            return true;
        }
        return false;
    }
    
    public void UpdateMaxRoomsCleared(int roomsCleared)
    {
        if (roomsCleared > playerData.maxRoomsCleared)
        {
            playerData.maxRoomsCleared = roomsCleared;
            SaveGame();
        }
    }
}
