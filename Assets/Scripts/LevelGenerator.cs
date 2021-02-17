using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject[] maps;

    public List<GameObject[]> obstacles;

    public List<Material> biomePlaneMaterials;
    int currentBiome;
    int biomeLengthRemaining;
    public int minBiomeLength;
    public int maxBiomeLength;

    private readonly float triggerDistance = 300f;
    private Vector3 lastEndPosition;
    private void Awake()
    {
        // reset biome and biome length remaining
        currentBiome = 0;
        biomeLengthRemaining = Random.Range(minBiomeLength, maxBiomeLength);
    }

    void Start()
    {
        lastEndPosition = new Vector3(player.position.x, 0) + new Vector3(80, 0, 0);
    }

    void Update()
    {
        if (!Tsunami.playerDead)
        {
            if (Vector3.Distance(player.position, lastEndPosition) < triggerDistance)
            {
                SpawnLevel();
            }

            if (lastEndPosition.x >= 1000)
            {
                MoveBack();
            }
        }
    }

    private void MoveBack()
    {
        Vector3 moveVector = Vector3.left * 2000;
        
        Tsunami.tsunami.position += moveVector;
        Player.player.position += moveVector;
        Camera.main.transform.position += moveVector;

        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).position += moveVector;

        lastEndPosition += moveVector;
    }

    private void SpawnLevel()
    {
        Transform newLevelTransform = Instantiate(maps[Random.Range(0, maps.Length)].transform, lastEndPosition, Quaternion.identity, transform);
        lastEndPosition = newLevelTransform.Find("EndPosition").position + new Vector3(95, 0.001f, 0);

        for (int i = 0; i < newLevelTransform.childCount; i++)
        {
            if (transform.GetChild(i).CompareTag("Obstacle"))
            {
                Transform obstacle = transform.GetChild(i);
                Instantiate(obstacles[currentBiome][Random.Range(0, obstacles[currentBiome].Length)], obstacle);
            }
        }

        newLevelTransform.GetComponent<Renderer>().material = biomePlaneMaterials[currentBiome];
        biomeLengthRemaining--;
        if (biomeLengthRemaining == 0)
        {
            List<int> possibleBiomes = new List<int>();
            for (int i = 1; i < biomePlaneMaterials.Count; i++)
                if (i != currentBiome)
                    possibleBiomes.Add(i);

            currentBiome = possibleBiomes[Random.Range(0, possibleBiomes.Count)];
            biomeLengthRemaining = Random.Range(minBiomeLength, maxBiomeLength);
        }
    }
}
