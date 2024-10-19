using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Gameplay");  // Restart the game
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");  // Return to main menu
    }
}
