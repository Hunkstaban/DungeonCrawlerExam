using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class PlayerData
{
    // Cosmetics
    public string selectedCosmetic;

    // Upgrades
    public int healthLevel;
    public int speedLevel;

    // Misc
    public string equippedWeapon;

    // Progress
    public int roomRecord;
    public int coinBalance;
    public List<string> unlockedWeapons = new List<string>();

    // Default constructor with default values for a new player
    public PlayerData()
    {
        selectedCosmetic = "None";
        healthLevel = 1;
        speedLevel = 1;
        equippedWeapon = "Sword";
        roomRecord = 0;
        coinBalance = 100;
        
        unlockedWeapons = new List<string> { "Sword" }; 
    }
}

// Public static class designed to save and load game data using the PlayerData class
// Its methods can be called by other scripts to manage player progress
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

