using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");  // Load Gameplay scene
    }

    public void OpenInstructions()
    {
        SceneManager.LoadScene("Instructions");  // Load Instructions scene
    }
}
