using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
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
