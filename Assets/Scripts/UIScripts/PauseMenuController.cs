using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuCanvas;
    private bool isPaused = false;

    void Update()
    {
        // Toggle pause menu when Escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenuCanvas.SetActive(true); 
        Time.timeScale = 0f; 
        
        // Unlock the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuCanvas.SetActive(false); 
        Time.timeScale = 1f; 
        
        // Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ReturnToMainMenu()
    {
        // Save the game before returning to the main menu
        GameManager.Instance.SaveData();

        // Reset time scale 
        Time.timeScale = 1f;

        // Load the main menu scene (index 0 in your build settings)
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        // Save the game before quitting
        GameManager.Instance.SaveData();
        Application.Quit();

        // If running in the Unity Editor, stop play mode
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}