using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  // Import TextMeshPro namespace

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;  // Reference to the Game Over UI panel
    public TextMeshProUGUI gameOverText;  // Reference to the Game Over TextMeshProUGUI component
    public string mainMenuSceneName = "MainMenu";  // Name of the Main Menu scene to load

    private bool gameOverTriggered = false;

    void Start()
    {
        // Initially hide the Game Over UI
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        // Ensure the game is not paused at the start
        Time.timeScale = 1f;
    }

    // Method to trigger the Game Over sequence
    public void TriggerGameOver()
    {
        gameOverTriggered = true;

        // Show the Game Over UI and text
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        if (gameOverText != null)
        {
            gameOverText.text = "Game Over! Tap to return to the Main Menu";
        }

        // Pause the game
        Time.timeScale = 0f;  // This will pause all time-related activities in the game
    }

    void Update()
    {
        // If Game Over has been triggered and the player presses anywhere on the screen, load the Main Menu
        if (gameOverTriggered && (Input.GetMouseButtonDown(0) || Input.touchCount > 0))
        {
            LoadMainMenu();
        }
    }

    // Method to load the Main Menu scene
    public void LoadMainMenu()
    {
        // Reset the time scale before loading the main menu
        Time.timeScale = 1f;  // Make sure the game is not paused in the main menu
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
