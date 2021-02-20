using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public AudioMixer audioMixer;

    private void Awake()
    {
        audioMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
        audioMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
        audioMixer.SetFloat("SfxVol", PlayerPrefs.GetFloat("SfxVol"));
    }

    private void Start()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Score\n{PlayerPrefs.GetString("Score")}";
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Highest Score\n{PlayerPrefs.GetString("HighestScore")}";
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
