using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // TextMesh Pro namespace

public class MainMenu : MonoBehaviour
{
    // TextMesh Pro Button references
    public Button playButton;
    public Button optionsButton;
    public Button quitButton;

    void Start()
    {
        // Ensure all buttons are linked in the Inspector
        playButton.onClick.AddListener(PlayGame);
        optionsButton.onClick.AddListener(OpenOptions);
        quitButton.onClick.AddListener(QuitGame);
    }

    // Function to start the game
    void PlayGame()
    {
        SceneManager.LoadScene("Play"); // Replace with your game scene name
    }

    // Function to open the options menu
    void OpenOptions()
    {
        Debug.Log("Options Button Clicked");
    }

    // Function to quit the game
    void QuitGame()
    {
        Debug.Log("Quit Button Clicked. Quitting the Game.");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in the editor
#endif
    }
}
