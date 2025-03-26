using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject defaultPanel;
    [SerializeField] private float fadeOutDuration;
    [SerializeField] private float fadeInDuration;

    void Start()
    {
        // Find and hide all panels with tag UIPanel
        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("UIPanel"))
            {
                CanvasGroup canvasGroup = child.GetComponent<CanvasGroup>();
                if (canvasGroup != null)
                {
                    canvasGroup.alpha = 0;
                    child.gameObject.SetActive(false);
                }
            }
        }

        // Show the default panel if assigned
        if (defaultPanel != null)
        {
            ShowPanel(defaultPanel);
        }
    }
    
    // Public method to show a specific panel with fade
    public void ShowPanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(true);
            CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                StartCoroutine(FadeIn(canvasGroup));
            }
        }
        else
        {
            Debug.LogWarning("Panel reference is null!");
        }
    }
    
    // Public method to hide all panels
    public void HideAllPanels()
    {
        CanvasGroup[] canvasGroups = GetComponentsInChildren<CanvasGroup>();
        foreach (var canvasGroup in canvasGroups)
        {
            StartCoroutine(FadeOut(canvasGroup));
        }
    }
    
    // Using Coroutine in other methods, fade in specific panel using CanvasGroup
    private IEnumerator FadeIn(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;
        float startTime = Time.time;

        while (Time.time < startTime + fadeInDuration)
        {
            float progress = (Time.time - startTime) / fadeInDuration;
            canvasGroup.alpha = progress;
            yield return null;
        }

        canvasGroup.alpha = 1;
    }

    // Using Coroutine in other methods, fade out specific panel using CanvasGroup
    private IEnumerator FadeOut(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1;
        float startTime = Time.time;

        while (Time.time < startTime + fadeOutDuration)
        {
            float progress = 1f - ((Time.time - startTime) / fadeOutDuration);
            canvasGroup.alpha = progress;
            yield return null;
        }

        canvasGroup.alpha = 0;
        canvasGroup.gameObject.SetActive(false);
    }
}
