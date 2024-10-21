using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstructionsController : MonoBehaviour
{
    [SerializeField] private GameObject backButton;  // Back button reference
    [SerializeField] private AudioSource audioSource;  // AudioSource reference
    [SerializeField] private AudioClip backButtonClip;  // AudioClip reference

    public void BackToMenu()
    {
        audioSource.PlayOneShot(backButtonClip);  // Play the sound effect
        SceneManager.LoadScene("MainMenu");
    }

    private void Start()
    {
        // Add a listener to the backButton's onClick event
        backButton.GetComponent<Button>().onClick.AddListener(BackToMenu);
    }
}
