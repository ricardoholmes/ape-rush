using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    static bool isPaused;
    public GameObject pauseMenu;

    public AudioMixer audioMixer;

    private void Awake()
    {
        audioMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
        audioMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
        audioMixer.SetFloat("SfxVol", PlayerPrefs.GetFloat("SfxVol"));
    }

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
