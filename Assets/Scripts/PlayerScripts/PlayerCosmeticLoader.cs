using UnityEngine;

public class PlayerCosmeticLoader : MonoBehaviour
{
    public Transform headAttachPoint;
    public Transform faceAttachPoint;
    
    [System.Serializable]
    public class CosmeticPrefab
    {
        public string name;
        public GameObject prefab;
        public string attachPoint; // "Head" or "Face"
    }
    
    public CosmeticPrefab[] availableCosmetics;
    
    private void Start()
    {
        LoadSelectedCosmetic();
    }
    
    private void LoadSelectedCosmetic()
    {
        string savedCosmetic = GameManager.Instance.playerData.selectedCosmetic;
        
        if (!string.IsNullOrEmpty(savedCosmetic))
        {
            foreach (var item in availableCosmetics)
            {
                if (item.name == savedCosmetic)
                {
                    Transform attachPoint = (item.attachPoint == "Head") ? headAttachPoint : faceAttachPoint;
                    
                    // Clear existing children
                    foreach (Transform child in attachPoint)
                    {
                        Destroy(child.gameObject);
                    }
                    
                    // Instantiate cosmetic
                    GameObject cosmetic = Instantiate(item.prefab, attachPoint);
                    cosmetic.transform.localPosition = Vector3.zero;
                    cosmetic.transform.localRotation = Quaternion.identity;
                    break;
                }
            }
        }
    }
}