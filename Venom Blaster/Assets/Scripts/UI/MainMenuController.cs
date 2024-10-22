using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject playButton;         // Play button reference
    [SerializeField] private GameObject instructionsButton; // Instructions button reference
    [SerializeField] private AudioClip buttonSoundClip;     // Button sound clip reference
    private AudioSource buttonSound;                        // Button sound reference

    void Start()
    {
        Time.timeScale = 1;  // Ensure the game is not paused
        buttonSound = GetComponent<AudioSource>();  // Get the AudioSource component

        if (buttonSound == null)
        {
            buttonSound = gameObject.AddComponent<AudioSource>();  // Add AudioSource component if not found
        }

        buttonSound.clip = buttonSoundClip;          // Assign the button sound clip
    }

    public void PlayGame()
    {
        Debug.Log("Play button clicked!");  // Log the button press
        buttonSound.Play();                   // Play the button sound
        SceneManager.LoadScene("Gameplay");
    }

    public void OpenInstructions()
    {
        buttonSound.Play();                   // Play the button sound
        SceneManager.LoadScene("Instructions");  // Load the scene synchronously
    }
}
