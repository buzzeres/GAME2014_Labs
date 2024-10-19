using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsController : MonoBehaviour
{
    [SerializeField] private GameObject backButton;  // Back button reference

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
