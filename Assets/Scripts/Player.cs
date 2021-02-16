using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static int monkeyCount = 0;
    public static Transform player;

    private void Start()
    {
        player = transform;
    }

    private void Update()
    {
        if (transform.childCount < monkeyCount)
        {
            for (int i = 0; i < monkeyCount - transform.childCount; i++)
                SpawnMonkey();
        }
    }
    public static void Die()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void SpawnMonkey()
    {
        // spawns monkeys
        return;
    }
}
