using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private List<Transform> spawnPositions = new List<Transform>();
    public GameObject monkey;
    private int index;

    public static int monkeyCount;
    public static Transform player;

    private float nextApeSound;
    private AudioSource audioSource;

    private void Awake()
    {
        index = 0;
        monkeyCount = 0;
        nextApeSound = Time.time + Random.Range(10, 20);
        for (int i = 0; i < transform.childCount; i++)
            if (transform.GetChild(i).name.Contains("Pos"))
                spawnPositions.Add(transform.GetChild(i));
    }

    private void Start()
    {
        player = transform;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            monkeyCount++;
        }

        if (Time.time > nextApeSound)
        {
            audioSource.Play();
            nextApeSound = Time.time + Random.Range(10, 20);
        }

        if (MonkeyChildCount() < monkeyCount)
        {
            for (int i = 0; i < monkeyCount - MonkeyChildCount(); i++)
                SpawnMonkey();
        }
        else if (MonkeyChildCount() > monkeyCount)
        {
            for (int i = 0; i < MonkeyChildCount() - monkeyCount; i++)
                Destroy(transform.GetChild(transform.childCount - 1).gameObject);
        }
    }

    public static void Die()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public int MonkeyChildCount()
    {
        int count = 0;
        for (int i = 0; i < transform.childCount; i++)
            if (transform.GetChild(i).CompareTag("Monkey"))
                count++;

        return count;
    }

    private void SpawnMonkey()
    {
        // spawns a monkey
        Transform pos = spawnPositions[index];
        Instantiate(monkey, pos.position, Quaternion.identity, transform);

        Vector3 newPos = pos.position;
        newPos.x -= 1.5f;

        index = (index + 1) % spawnPositions.Count;
        pos.position = newPos;
    }
}
