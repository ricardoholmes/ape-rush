using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    static bool isPaused;
    public GameObject pauseMenu;

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            PauseGame(!isPaused);
        }
    }

    public void Resume()
    {
        PauseGame(false);
    }

    public void ReturnToMainMenu()
    {
        PauseGame(false);
        SceneManager.LoadScene("MainMenu");
    }

    private void PauseGame(bool pause)
    {
        isPaused = pause;
        pauseMenu.SetActive(pause);
        Time.timeScale = pause ? 0 : 1;
    }
}
