using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterMenuController : MonoBehaviour
{
    [System.Serializable]
    public class CosmeticItem
    {
        public string name;
        public Button selectButton;
        public GameObject prefab;
        public string attachPoint; // "Head" or "Face"
    }
    
    public CosmeticItem[] cosmeticItems;
    public GameObject playerPreview; // Reference to player model in menu
    
    private GameObject currentHeadCosmetic;
    private GameObject currentFaceCosmetic;
    
    private void Start()
    {
        InitializeButtons();
        LoadSelectedCosmetic();
    }
    
    private void InitializeButtons()
    {
        foreach (var item in cosmeticItems)
        {
            // Create a local copy of the item for the closure
            var localItem = item;
            
            item.selectButton.onClick.AddListener(() => {
                SelectCosmetic(localItem);
            });
        }
    }
    
    private void LoadSelectedCosmetic()
    {
        string savedCosmetic = GameManager.Instance.playerData.selectedCosmetic;
        
        if (!string.IsNullOrEmpty(savedCosmetic))
        {
            foreach (var item in cosmeticItems)
            {
                if (item.name == savedCosmetic)
                {
                    ApplyCosmetic(item);
                    break;
                }
            }
        }
    }
    
    private void SelectCosmetic(CosmeticItem item)
    {
        // Apply to preview
        ApplyCosmetic(item);
        
        // Save selection
        GameManager.Instance.playerData.selectedCosmetic = item.name;
        GameManager.Instance.SaveGame();
    }
    
    private void ApplyCosmetic(CosmeticItem item)
    {
        // Find attach point in player preview
        Transform attachPoint = playerPreview.transform.Find(item.attachPoint);
        if (attachPoint == null)
        {
            Debug.LogError($"Attach point {item.attachPoint} not found on player preview");
            return;
        }
        
        // Remove existing cosmetics from this attach point
        if (item.attachPoint == "Head")
        {
            if (currentHeadCosmetic != null)
                Destroy(currentHeadCosmetic);
                
            // Instantiate new cosmetic
            currentHeadCosmetic = Instantiate(item.prefab, attachPoint);
            currentHeadCosmetic.transform.localPosition = Vector3.zero;
            currentHeadCosmetic.transform.localRotation = Quaternion.identity;
        }
        else if (item.attachPoint == "Face")
        {
            if (currentFaceCosmetic != null)
                Destroy(currentFaceCosmetic);
                
            // Instantiate new cosmetic
            currentFaceCosmetic = Instantiate(item.prefab, attachPoint);
            currentFaceCosmetic.transform.localPosition = Vector3.zero;
            currentFaceCosmetic.transform.localRotation = Quaternion.identity;
        }
    }
}
