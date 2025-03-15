using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[Serializable]
public class PlayerData
{
    // Character cosmetics
    public string selectedCosmetic = "";
    
    // Upgrades
    public int healthLevel = 0;
    public int speedLevel = 0;
    
    // Weapons
    public string equippedWeapon = "Sword"; // Default weapon
    public List<string> purchasedWeapons = new List<string>() { "Sword" }; // Start with one basic weapon
    
    // Game progress
    public int coins = 0;
    public int maxRoomsCleared = 0;
}

public static class SaveSystem
{
    private static readonly string SAVE_PATH = Application.persistentDataPath + "/gamedata.json";

    public static void SaveData(PlayerData data)
    {
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(SAVE_PATH, jsonData);
        Debug.Log("Game data saved to: " + SAVE_PATH);
    }

    public static PlayerData LoadData()
    {
        if (File.Exists(SAVE_PATH))
        {
            string jsonData = File.ReadAllText(SAVE_PATH);
            return JsonUtility.FromJson<PlayerData>(jsonData);
        }
        else
        {
            // If no save file exists, create a new one with default values
            PlayerData newData = new PlayerData();
            SaveData(newData);
            return newData;
        }
    }
}