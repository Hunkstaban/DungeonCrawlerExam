using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private Animator menuAnimator;
    private CustomCameraController cameraController;
    private UIManager uiManager;
    
    // Inner class and an array to use in connection with UIManager and CustomCameraController
    [System.Serializable]
    public class MenuDestination
    {
        public string menuState;
        public Transform cameraTarget;
        public string panelName;
    }
    
    public MenuDestination[] menuDestinations;
    
    private void Awake()
    {
        menuAnimator = GetComponent<Animator>();
        cameraController = GetComponent<CustomCameraController>();
        uiManager = GetComponent<UIManager>();
        
        if (cameraController == null)
            cameraController = Camera.main.GetComponent<CustomCameraController>();
    }
    
    // This will be called by button clicks
    public void TransitionToMenu(string menuStateName)
    {
        // Find the menu destination
        foreach (var destination in menuDestinations)
        {
            if (destination.menuState == menuStateName)
            {
                // Trigger animation state
                menuAnimator.SetTrigger(menuStateName);
                return;
            }
        }
        
        Debug.LogWarning($"Menu state '{menuStateName}' not found!");
    }
    
    // Called by animation events
    public void MoveCamera(string menuStateName)
    {
        foreach (var destination in menuDestinations)
        {
            if (destination.menuState == menuStateName && destination.cameraTarget != null)
            {
                cameraController.MoveToTarget(destination.cameraTarget);
                return;
            }
        }
    }
    
    // Called by animation events
    public void AnimEventShowPanel(string menuStateName)
    {
        foreach (var destination in menuDestinations)
        {
            if (destination.menuState == menuStateName)
            {
                uiManager.ShowPanel(destination.panelName);
                return;
            }
        }
    }
    
    // Called by animation events
    public void AnimEventHideAllPanels()
    {
        uiManager.HideAllPanels();
    }
    
    public void StartGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
