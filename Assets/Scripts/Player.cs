using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static int monkeyCount = 0;
    public static Transform player;

    private float nextApeSound;
    private AudioSource audioSource;

    private void Awake()
    {
        nextApeSound = Time.time + Random.Range(10, 20);
    }

    private void Start()
    {
        player = transform;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Time.time > nextApeSound)
        {
            audioSource.Play();
            nextApeSound = Time.time + Random.Range(10, 20);
        }

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
        // spawns a monkey
        return;
    }
}
