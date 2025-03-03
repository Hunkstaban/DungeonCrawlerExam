using System;
using System.Collections;
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
    public Image[] powerUpCountdownBg;
    public TextMeshProUGUI[] powerUpCountdownText;

    public TextMeshProUGUI powerUpOverlayText;
    public TextMeshProUGUI powerUpSelected;
    
    private PowerUp[] powerUpInventory = new PowerUp[4];
    private int[] powerUpCounts = new int[4];
    
    private List<PowerUp> activePowerups = new();
    
    private PlayerController player;
    private int selectedIndex = 0;

    private void Start()
    {
        UpdateUI();
        player = GetComponent<PlayerController>();
        if (player == null) Debug.LogError("PlayerController reference missing on the player!");
        powerUpOverlayText.SetText("Collected: ");
        powerUpSelected.SetText("Selected: " + selectedIndex);

        for (int i = 0; i < powerUpCountdownText.Length; i++)
        {
            powerUpCountdownBg[i].gameObject.SetActive(false);
            powerUpCountdownText[i].gameObject.SetActive(false);
        }
    }

    public void CollectItem(PowerUp powerUpScript)
    {
        for (int i = 0; i < powerUpInventory.Length; i++)
        {
            if (powerUpInventory[i] == null || powerUpInventory[i] == powerUpScript)
            {
                powerUpInventory[i] = powerUpScript;
                powerUpCounts[i]++;
                UpdateUI();
                Debug.Log($"Player collected {powerUpScript}!");
                return;
            }
        }
        Debug.Log("Inventory Full!");
    }

    public void UseSelectedItem()
    {
        if (selectedIndex < 0 || selectedIndex >= powerUpInventory.Length) return;
        if (powerUpInventory[selectedIndex] == null) return;
        
        PowerUp selectedPowerup = powerUpInventory[selectedIndex];
        if (activePowerups.Contains(selectedPowerup))
        {
            Debug.Log($"Powerup {selectedPowerup} is already active!");
            return;
        }

        activePowerups.Add(selectedPowerup);
        Debug.Log($"Player used {selectedPowerup}!");

        selectedPowerup.ApplyPowerUp(player);
        TimedPowerUp selectedTimedPowerUp = selectedPowerup as TimedPowerUp;
        
        if (selectedTimedPowerUp != null)
        {
            StartCoroutine(RemovePowerupAfterDuration(selectedTimedPowerUp, selectedIndex));
        }
        else
        {
            RemovePowerupFromInventory(selectedIndex);
        }
    }

    private IEnumerator RemovePowerupAfterDuration(TimedPowerUp timedPowerUp, int index)
    {
        float remainingTime = timedPowerUp.durationInSeconds;
        
        powerUpCountdownBg[index].gameObject.SetActive(true);
        powerUpCountdownText[index].gameObject.SetActive(true);
        
        StartCoroutine(timedPowerUp.StartPowerupCountdown(player));

        while (remainingTime > 0)
        {
            powerUpCountdownText[index].SetText($"{Mathf.Ceil(remainingTime)}");
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
        }
        
        powerUpCountdownBg[index].gameObject.SetActive(false);
        powerUpCountdownText[index].gameObject.SetActive(false);
        
        RemovePowerupFromInventory(index);
    }

    private void RemovePowerupFromInventory(int index)
    {
        if (powerUpInventory[index] != null)
        {
            powerUpCounts[index]--;
            if (powerUpCounts[index] <= 0)
            {
                powerUpInventory[index] = null;
                powerUpCounts[index] = 0;
            }
        }
        
        activePowerups.Remove(powerUpInventory[index]);
        
        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < powerUpSlots.Length; i++)
        {
            powerUpSlots[i].color = (i == selectedIndex) ? Color.white : new Color(0.8f, 0.8f, 0.8f);
            if (powerUpInventory[i] != null)
            {
                PowerUp currentPowerUp = powerUpInventory[i];
                if (!currentPowerUp.powerUpIcon) Debug.LogError($"No Powerup Icon Added to {currentPowerUp}");
                
                powerUpSlots[i].sprite = currentPowerUp.powerUpIcon;
                powerUpAmountOverlay[i].text = powerUpCounts[i].ToString();
                // powerUpAmountOverlay[i].gameObject.SetActive(true);
            }
            else
            {
                powerUpSlots[i].sprite = null;
                powerUpAmountOverlay[i].text = "0";
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