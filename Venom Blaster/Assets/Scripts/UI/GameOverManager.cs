using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  // Import TextMeshPro namespace

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;  // Reference to the Game Over UI panel
    public GameObject winUI;       // Reference to the Win UI panel
    public TextMeshProUGUI gameOverText;  // Reference to the Game Over TextMeshProUGUI component
    public TextMeshProUGUI winText;       // Reference to the Win TextMeshProUGUI component
    public string mainMenuSceneName = "MainMenu";  // Name of the Main Menu scene to load
    public AudioClip gameOverSound;  // Sound to play when the game over sequence starts
    public AudioClip winSound;       // Sound to play when the win condition is triggered

    private bool gameOverTriggered = false;
    private bool winTriggered = false;
    private AudioSource audioSource;  // Reference to the AudioSource component

    void Start()
    {
        // Initially hide the Game Over and Win UI
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
        if (winUI != null)
        {
            winUI.SetActive(false);
        }

        // Ensure the game is not paused at the start
        Time.timeScale = 1f;

        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
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

        // Play the game over sound
        if (audioSource != null && gameOverSound != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }

        // Pause the game
        Time.timeScale = 0f;  // This will pause all time-related activities in the game
    }

    // Method to trigger the win condition
    public void TriggerWinCondition()
    {
        winTriggered = true;

        // Show the Win UI and text
        if (winUI != null)
        {
            winUI.SetActive(true);
        }

        if (winText != null)
        {
            winText.text = "You Win! Tap to return to the Main Menu";
        }

        // Play the win sound
        if (audioSource != null && winSound != null)
        {
            audioSource.PlayOneShot(winSound);
        }

        // Pause the game
        Time.timeScale = 0f;
    }

    void Update()
    {
        // If Game Over or Win condition has been triggered and the player presses anywhere on the screen, load the Main Menu
        if ((gameOverTriggered || winTriggered) && (Input.GetMouseButtonDown(0) || Input.touchCount > 0))
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
