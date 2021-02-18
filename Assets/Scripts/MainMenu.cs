using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioMixer volumeMixer;

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ChangeVolume(string type, float volume)
    {
        volumeMixer.SetFloat(type, Mathf.Log10(volume) * 20);
    }

    public void ChangeMasterVolume(float vol) { ChangeVolume("MasterVol", vol); }
    public void ChangeMusicVolume(float vol) { ChangeVolume("MusicVol", vol); }
    public void ChangeSfxVolume(float vol) { ChangeVolume("SfxVol", vol); }
}
