using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    private void Start()
    {
        Time.timeScale = 0;
    }

    // Method used to go to Menu
    public void NavigateToMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Method used to resume game.
    public void Resume()
    {
        this.pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            this.Resume();
        
    }
}
