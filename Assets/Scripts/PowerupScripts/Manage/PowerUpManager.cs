using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    public Image[] powerUpSlots;
    public TextMeshProUGUI[] powerUpAmountOverlay;

    public TextMeshProUGUI powerUpOverlayText;
    public TextMeshProUGUI powerUpSelected;
    
    private Dictionary<PowerUp, int> powerUpInventory = new();
    
    private PlayerController player;
    private int selectedIndex = 0;

    private void Start()
    {
        UpdateUI();
        player = GetComponent<PlayerController>();
        if (player == null) Debug.LogError("PlayerController reference missing in PowerUpManager!");
        powerUpOverlayText.SetText("Collected: ");
        powerUpSelected.SetText("Selected: " + selectedIndex);
    }

    public void CollectItem(PowerUp powerUpScript)
    {
        if (powerUpInventory.ContainsKey(powerUpScript)) powerUpInventory[powerUpScript]++;
        else powerUpInventory[powerUpScript] = 1;
        
        Debug.Log($"Player collected {powerUpScript}! Total: {powerUpInventory[powerUpScript]}");
        UpdateUI();
    }

    public void UseSelectedItem()
    {
        if (powerUpInventory.Count == 0) return;
        if (selectedIndex+1 > powerUpInventory.Count) return;
        
        PowerUp selectedPowerup = powerUpInventory.Keys.ElementAt(selectedIndex);
        powerUpInventory[selectedPowerup]--;
        if (powerUpInventory[selectedPowerup] <= 0) powerUpInventory.Remove(selectedPowerup);

        Debug.Log($"Player used {selectedPowerup}!");

        selectedPowerup.ApplyPowerUp(player);
        TimedPowerUp selectedTimedPowerUp = selectedPowerup as TimedPowerUp;
        if (selectedTimedPowerUp != null)
        {
            StartCoroutine(selectedTimedPowerUp.StartPowerupCountdown(player));
        }
        
        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < powerUpSlots.Length; i++)
        {
            powerUpSlots[i].color = (i == selectedIndex) ? Color.white : new Color(0.8f, 0.8f, 0.8f);
            if (i < powerUpInventory.Count)
            {
                PowerUp currentPowerUp = powerUpInventory.Keys.ElementAt(i);
                if (!currentPowerUp.powerUpIcon) Debug.LogError($"No Powerup Icon Added to {currentPowerUp}");
                powerUpSlots[i].sprite = currentPowerUp.powerUpIcon;

                powerUpAmountOverlay[i].text = powerUpInventory[currentPowerUp].ToString();
                // powerUpAmountOverlay[i].gameObject.SetActive(true);
            }
            else
            {
                powerUpSlots[i].sprite = null;
                // powerUpAmountOverlay[i].gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        HandleSelectedInput();
        if (Input.GetKeyDown(KeyCode.G)) UseSelectedItem();
        powerUpSelected.SetText("Selected: " + selectedIndex);
    }

    private void HandleSelectedInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectPowerUp(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectPowerUp(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectPowerUp(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectPowerUp(3);
    }
    
    private void SelectPowerUp(int index)
    {
        selectedIndex = index;
        UpdateUI();
        // Debug.Log($"Selected Power-Up: {powerUpInventory.Keys.ElementAt(index)}");
    }
}