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
    public Sprite defaultSlotSprite;

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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("PowerUp"))
        {
            PowerUp colliderPowerUp = collider.GetComponent<PowerUp>();
            if (colliderPowerUp != null)
            {
                CollectItem(colliderPowerUp);
                // collider.GetComponent<MeshRenderer>().enabled = false;
                // collider.GetComponent<Collider>().enabled = false;
                // colliderPowerUp.ApplyPowerUp(player);
                Destroy(collider.gameObject);
                // collider.gameObject.SetActive(false);
            }
        }
    }

    public void CollectItem(PowerUp powerUpScript)
    {
        if (powerUpInventory.ContainsKey(powerUpScript)) powerUpInventory[powerUpScript]++;
        else powerUpInventory[powerUpScript] = 1;
        
        // Debug.Log($"Player collected {powerUpScript}! Total: {powerUpInventory[powerUpScript]}");
        UpdateUI();
    }

    public void UseSelectedItem()
    {
        if (powerUpInventory.Count == 0) return;
        if (selectedIndex+1 > powerUpInventory.Count) return;
        PowerUp selectedItem = powerUpInventory.Keys.ElementAt(selectedIndex);
        powerUpInventory[selectedItem]--;

        if (powerUpInventory[selectedItem] <= 0) powerUpInventory.Remove(selectedItem);

        // Debug.Log($"Player used {selectedItem}!");

        // ApplyPowerUp(selectedItem);
        selectedItem.ApplyPowerUp(player);
        UpdateUI();
    }

    private void ApplyPowerUp(PowerUp itemType)
    {
        // Debug.Log($"Applying {itemType}");
        // switch (itemType)
        // {
        //     case ItemType.SpeedBoost:
        //         ApplyPowerUp<SpeedBoostPowerUp>();
        //         break;
        //     case ItemType.JumpBoost:
        //         ApplyPowerUp<JumpBoostPowerUp>();
        //         break;
        //     case ItemType.Healing:
        //         ApplyPowerUp<HealingPowerUp>();
        //         break;
        // }
    }

    // private void ApplyPowerUp<T>() where T : PowerUp
    // {
    //     GameObject powerUpObject = new GameObject(typeof(T).Name);
    //     T powerUp = powerUpObject.AddComponent<T>();
    //     powerUp.ApplyPowerUp(player);
    //
    //     float powerUpDuration = powerUp.duration > 0 ? powerUp.duration : 5f;
    //     Destroy(powerUpObject, powerUpDuration + 0.5f);
    // }

    private void UpdateUI()
    {
        for (int i = 0; i < powerUpSlots.Length; i++)
        {
            powerUpSlots[i].color = (i == selectedIndex) ? Color.white : new Color(0.8f, 0.8f, 0.8f);
            if (i < powerUpInventory.Count)
            {
                PowerUp currentPowerUp = powerUpInventory.Keys.ElementAt(i);
                powerUpSlots[i].sprite = currentPowerUp.powerUpIcon;

                powerUpAmountOverlay[i].text = powerUpInventory[currentPowerUp].ToString();
                // powerUpAmountOverlay[i].gameObject.SetActive(true);
            }
            // else
            // {
            //     powerUpSlots[i].sprite = defaultSlotSprite;
            //     powerUpAmountOverlay[i].gameObject.SetActive(false);
            // }
        }
    }

    // private Sprite GetPowerUpSprite(PowerUp itemType)
    // {
    //     return Resources.Load<Sprite>($"PowerUps/{itemType}");
    // }

    private void Update()
    {
        HandleSelectedInput();
        HandleUseInput();
        powerUpSelected.SetText("Selected: " + selectedIndex);
    }

    private void HandleSelectedInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectPowerUp(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectPowerUp(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectPowerUp(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectPowerUp(3);
    }

    private void HandleUseInput()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            UseSelectedItem();
        }
    }

    private void SelectPowerUp(int index)
    {
        selectedIndex = index;
        UpdateUI();
        // Debug.Log($"Selected Power-Up: {powerUps.Keys.ElementAt(index)}");
    }
}