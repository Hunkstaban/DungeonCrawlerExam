using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    
    public PlayerData playerData;
    
    private int roomsCleared = 0;
    private int coinsCollected = 0;
    private PlayerController player;
    
    // Initializing GameManager instance and loading data from save file if any exists
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
        DontDestroyOnLoad(gameObject);
        player = FindFirstObjectByType<PlayerController>();
        
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
    
    // Adding and saving coins to player's balance, and update the collected coins for the current game
    public void AddCoins(int amount)
    {
        if (player == null) player = FindFirstObjectByType<PlayerController>();
        coinsCollected++;
        player.SetCoinsCollectedText(coinsCollected);
        
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
    
    // Updating and saving player's room record if current rooms cleared is higher, and update the rooms cleared for the current game
    public void UpdateRoomRecord()
    {
        if (player == null) player = FindFirstObjectByType<PlayerController>();

        roomsCleared++;
        player.SetRoomsClearedText(roomsCleared);
        
        if (roomsCleared > playerData.roomRecord)
        {
            playerData.roomRecord = roomsCleared;
            SaveData();
        }
    }
}