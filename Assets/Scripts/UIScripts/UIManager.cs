using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Dictionary<string, GameObject> panels = new Dictionary<string, GameObject>();
    public string defaultPanel;

    void Start()
    {
        // Automatically find all panels in the scene and add them to the dictionary
        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("UIPanel")) // Use a tag to identify panels
            {
                panels.Add(child.gameObject.name, child.gameObject);
            }
        }
        
        ShowPanel(defaultPanel);
    }

    // Method to show a specific panel by name
    public void ShowPanel(string panelName)
    {
        foreach (var panel in panels.Values)
        {
            panel.SetActive(false); // Hide all panels
        }

        if (panels.ContainsKey(panelName))
        {
            panels[panelName].SetActive(true); // Show the requested panel
        }
        else
        {
            Debug.LogWarning($"Panel '{panelName}' not found!");
        }
    }
}