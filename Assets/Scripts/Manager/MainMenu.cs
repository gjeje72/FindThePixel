using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Method used to start a new game.
    public void PlayGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Method used to quit game.
    public void QuitGame()
    {
        Application.Quit();
    }

    // Method used to load Levels Scene.
    public void Levels()
    {
        SceneManager.LoadScene(11);
    }
}
