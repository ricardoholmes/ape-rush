using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    static bool isPaused;
    public GameObject pauseMenu;

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            isPaused = !isPaused;
            pauseMenu.SetActive(isPaused);
            Time.timeScale = isPaused ? 0 : 1;
        }
    }
}
