using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(2);  // Restart the game
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadSceneAsync(1);  // Return to main menu
    }
}
