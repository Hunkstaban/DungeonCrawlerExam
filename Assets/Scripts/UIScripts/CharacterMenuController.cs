using System.Collections.Generic;
using UnityEngine;

public class CharacterMenuController : MonoBehaviour
{
    [System.Serializable]
    public class Cosmetic
    {
        public string name; // Name of the cosmetic (e.g., "Cowboy", "Crown")
        public GameObject prefab; // The prefab to attach to the player
        public Transform attachPoint; // The point on the player where the cosmetic will be attached
    }

    public List<Cosmetic> cosmetics; // List of all available cosmetics
    private string selectedCosmetic; // The currently selected cosmetic

    private void Start()
    {
        // Load the selected cosmetic from PlayerData
        selectedCosmetic = GameManager.Instance.playerData.selectedCosmetic;

        // Apply the saved cosmetic to the player
        ApplyCosmetic(selectedCosmetic);
    }

    // Called when a cosmetic is selected from the UI
    public void SelectCosmetic(string cosmeticName)
    {
        // Save the selected cosmetic
        selectedCosmetic = cosmeticName;
        GameManager.Instance.playerData.selectedCosmetic = selectedCosmetic;
        GameManager.Instance.SaveData();

        // Apply the cosmetic to the player
        ApplyCosmetic(selectedCosmetic);
    }

    private void ApplyCosmetic(string cosmeticName)
    {
        // Deactivate all cosmetics
        foreach (var cosmetic in cosmetics)
        {
            if (cosmetic.prefab != null)
            {
                cosmetic.prefab.SetActive(false);
            }
        }

        // Find and activate the selected cosmetic
        foreach (var cosmetic in cosmetics)
        {
            if (cosmetic.name == cosmeticName)
            {
                if (cosmetic.prefab != null)
                {
                    cosmetic.prefab.SetActive(true);
                }
                return;
            }
        }

        Debug.LogWarning($"Cosmetic '{cosmeticName}' not found!");
    }
}