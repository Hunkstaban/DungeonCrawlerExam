using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    
    public PlayerData playerData;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
        DontDestroyOnLoad(gameObject);
        
        // Load data immediately when the GameManager initializes
        LoadData();
    }
    
    public void SaveData()
    {
        SaveSystem.SaveData(playerData);
    }
    
    public void LoadData()
    {
        playerData = SaveSystem.LoadData();
    }
    
    // Add coins
    public void AddCoins(int amount)
    {
        playerData.coinBalance += amount;
        SaveData();
    }
    
    // Spend coins and return whether purchase was successful
    public bool SpendCoins(int amount)
    {
        if (playerData.coinBalance >= amount)
        {
            playerData.coinBalance -= amount;
            SaveData();
            return true;
        }
        return false;
    }
    
    // Update room record if current is higher
    public void UpdateRoomRecord(int roomsCleared)
    {
        if (roomsCleared > playerData.roomRecord)
        {
            playerData.roomRecord = roomsCleared;
            SaveData();
        }
    }
}