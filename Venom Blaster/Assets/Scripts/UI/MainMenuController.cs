using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject playButton;         // Play button reference
    [SerializeField] private GameObject instructionsButton; // Instructions button reference

    void Start()
    {
        Time.timeScale = 1;  // Ensure the game is not paused
    }

    public void PlayGame()
    {
        Debug.Log("Play button clicked!");  // Log the button press
        SceneManager.LoadScene("Gameplay");
    }


    public void OpenInstructions()
    {
        SceneManager.LoadScene("Instructions");  // Load the scene synchronously
    }


}
