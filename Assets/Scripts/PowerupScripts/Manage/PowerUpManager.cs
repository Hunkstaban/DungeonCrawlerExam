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
    [SerializeField] private Outline[] powerUpSlots;
    [SerializeField] private Image[] powerUpIcons;
    [SerializeField] private TextMeshProUGUI[] powerUpAmountOverlay;
    [SerializeField] private Image[] powerUpCountdownBg;
    [SerializeField] private TextMeshProUGUI[] powerUpCountdownText;
    
    private static int inventorySize = 4;
    private PowerUp[] powerUpInventory = new PowerUp[inventorySize];
    private int[] powerUpCounts = new int[inventorySize];
    
    private List<PowerUp> activePowerups = new();
    
    private PlayerController player;
    private int selectedIndex = 0;

    private void Start()
    {
        UpdateUI();
        player = GetComponent<PlayerController>();
        if (player == null) Debug.LogError("PlayerController reference missing on the player!");

        for (int i = 0; i < powerUpCountdownText.Length; i++)
        {
            powerUpCountdownBg[i].gameObject.SetActive(false);
            powerUpCountdownText[i].gameObject.SetActive(false);
        }
    }

    public bool CollectItem(PowerUp powerUpScript)
    {
        int powerupIndex = Array.IndexOf(powerUpInventory, powerUpScript);
        int itemLimit = 4;
        
        if (powerUpInventory.Contains(powerUpScript))
        {
            if (powerUpCounts[powerupIndex] < itemLimit)
            {
                powerUpInventory[powerupIndex] = powerUpScript;
                powerUpCounts[powerupIndex]++;
                UpdateUI();
                Debug.Log($"Player collected {powerUpScript}!");
                return true;
            }
            return false;
        }
        
        for (int i = 0; i < powerUpInventory.Length; i++)
        {
            if (powerUpInventory[i] == null)
            {
                powerUpInventory[i] = powerUpScript;
                powerUpCounts[i]++;
                UpdateUI();
                Debug.Log($"Player collected {powerUpScript}!");
                // Array.ForEach(powerUpInventory, i => Debug.Log(i));
                // Debug.Log(string.Join(", ", powerUpCounts));
                return true;
            }
        }
        Debug.Log("Inventory Full!");
        return false;
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
            activePowerups.Remove(powerUpInventory[index]);
            powerUpCounts[index]--;
            if (powerUpCounts[index] <= 0)
            {
                powerUpInventory[index] = null;
                powerUpCounts[index] = 0;
            }
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < powerUpIcons.Length; i++)
        {
            Color inactive = new Color(0.8f, 0.8f, 0.8f);
            Color active = new Color(1f, 1f, 1f);
            powerUpIcons[i].color = (i == selectedIndex) ? active : inactive;
            powerUpSlots[i].enabled = i == selectedIndex;
            if (powerUpInventory[i] != null)
            {
                PowerUp currentPowerUp = powerUpInventory[i];
                if (!currentPowerUp.powerUpIcon) Debug.LogError($"No Powerup Icon Added to {currentPowerUp}");
                
                powerUpIcons[i].sprite = currentPowerUp.powerUpIcon;
                powerUpAmountOverlay[i].text = powerUpCounts[i].ToString();
                // powerUpAmountOverlay[i].gameObject.SetActive(true);
            }
            else
            {
                powerUpIcons[i].sprite = null;
                powerUpAmountOverlay[i].text = "0";
                // powerUpAmountOverlay[i].gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        HandleSelectedInput();
        if (Input.GetKeyDown(KeyCode.G)) UseSelectedItem();
    }

    private void HandleSelectedInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectPowerUp(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectPowerUp(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectPowerUp(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectPowerUp(3);
        if(Input.GetAxis("Mouse ScrollWheel") < 0f) SelectPowerUp((selectedIndex + 1) % inventorySize);
        if(Input.GetAxis("Mouse ScrollWheel") > 0f) SelectPowerUp((selectedIndex + (inventorySize - 1)) % inventorySize);
    }
    
    private void SelectPowerUp(int index)
    {
        selectedIndex = index;
        UpdateUI();
        // Debug.Log($"Selected Power-Up: {powerUpInventory.Keys.ElementAt(index)}");
    }
}