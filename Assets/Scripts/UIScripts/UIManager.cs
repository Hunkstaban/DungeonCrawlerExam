using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Dictionary<string, GameObject> panels = new Dictionary<string, GameObject>();
    public string defaultPanel;
    public float hideFadeSpeed = 0.5f;
    public float showFadeSpeed = 2f;

    void Start()
    {
        // Automatically find all panels in the scene and add them to the dictionary
        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("UIPanel"))
            {
                panels.Add(child.gameObject.name, child.gameObject);
                
                // Ensure each panel has a CanvasGroup
                CanvasGroup canvasGroup = child.gameObject.GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    canvasGroup = child.gameObject.AddComponent<CanvasGroup>();
                }
                
                // Initially hide all panels
                canvasGroup.alpha = 0;
                child.gameObject.SetActive(false);
            }
        }
        
        // Show default panel
        if (panels.ContainsKey(defaultPanel))
        {
            ShowPanel(defaultPanel);
        }
    }
    
    // Public method to show a specific panel with fade
    public void ShowPanel(string panelName)
    {
        if (panels.ContainsKey(panelName))
        {
            GameObject panel = panels[panelName];
            panel.SetActive(true);
            StartCoroutine(FadeIn(panel.GetComponent<CanvasGroup>()));
        }
        else
        {
            Debug.LogWarning($"Panel '{panelName}' not found!");
        }
    }
    
    // Public method to hide all panels
    public void HideAllPanels()
    {
        foreach (var panel in panels.Values)
        {
            StartCoroutine(FadeOut(panel.GetComponent<CanvasGroup>()));
        }
    }
    
    private IEnumerator FadeIn(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;
        float startTime = Time.time;
        float duration = 1f / showFadeSpeed; // Convert speed to duration
    
        while (Time.time < startTime + duration)
        {
            float progress = (Time.time - startTime) / duration;
            canvasGroup.alpha = progress;
            yield return null;
        }
    
        canvasGroup.alpha = 1;
    }

    private IEnumerator FadeOut(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1;
        float startTime = Time.time;
        float duration = 1f / hideFadeSpeed; // Convert speed to duration
    
        while (Time.time < startTime + duration)
        {
            float progress = 1f - ((Time.time - startTime) / duration);
            canvasGroup.alpha = progress;
            yield return null;
        }
    
        canvasGroup.alpha = 0;
        canvasGroup.gameObject.SetActive(false);
    }
}
