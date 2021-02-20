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
        transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"Apes rescued\n{PlayerPrefs.GetInt("Rescued")}";
        transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = $"Apes fallen\n{PlayerPrefs.GetInt("Sacrificed")}";
        transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = $"Apes abandoned\n{PlayerPrefs.GetInt("Monkeys")}";
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
