using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject[] maps;

    [Range(0, 1)]
    public float cageProbability = 0.05f;
    public GameObject cage;

    public List<BiomeObjects> biomes;

    int currentBiome;
    int biomeLengthRemaining;
    public int minBiomeLength;
    public int maxBiomeLength;

    float nextBiomeStart;
    bool biomeStarted;

    private readonly float triggerDistance = 300f;
    private Vector3 lastEndPosition;

    private void Awake()
    {
        // reset biome and biome length remaining
        nextBiomeStart = 0;
        currentBiome = 0;
        biomeStarted = false;
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
            if (lastEndPosition.x - player.position.x < triggerDistance)
            {
                SpawnLevel();
            }

            if (lastEndPosition.x >= 1000)
            {
                MoveBack();
            }

            if (player.position.x >= nextBiomeStart && !biomeStarted)
            {
                player.GetComponent<AudioSource>().clip = biomes[currentBiome].steppingSound;
                biomeStarted = true;
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
        nextBiomeStart -= moveVector.magnitude;
    }

    private void SpawnLevel()
    {
        Transform newLevelTransform = Instantiate(maps[Random.Range(0, maps.Length)].transform, lastEndPosition, Quaternion.identity, transform);
        lastEndPosition = newLevelTransform.Find("EndPosition").position + new Vector3(95, 0.001f, 0);

        GameObject[] biomeObjects = biomes[currentBiome].obstacles;
        List<Transform> obstaclesList = new List<Transform>();
        for (int i = 0; i < newLevelTransform.childCount; i++)
        {
            if (newLevelTransform.GetChild(i).CompareTag("Obstacle"))
            {
                obstaclesList.Add(newLevelTransform.GetChild(i));
            }
        }

        foreach (Transform i in obstaclesList)
        {
            Transform newObstacle;
            if (Random.value <= cageProbability)
                newObstacle = Instantiate(cage, i.position, Quaternion.identity, newLevelTransform).transform;
            else
                newObstacle = Instantiate(biomeObjects[Random.Range(0, biomeObjects.Length)], i.position, Quaternion.identity, newLevelTransform).transform;

            Vector3 newScale = newObstacle.localScale;
            newScale.x /= newLevelTransform.localScale.x;
            newScale.y /= newLevelTransform.localScale.y;
            newScale.z /= newLevelTransform.localScale.z;
            newObstacle.localScale = newScale;

            Destroy(i.gameObject);
        }

        newLevelTransform.GetComponent<Renderer>().material = biomes[currentBiome].floorMaterial;
        biomeLengthRemaining--;
        if (biomeLengthRemaining == 0)
        {
            nextBiomeStart = newLevelTransform.Find("EndPosition").position.x;
            List<int> possibleBiomes = new List<int>();
            for (int i = 1; i < biomes.Count; i++)
                if (i != currentBiome)
                    possibleBiomes.Add(i);

            currentBiome = possibleBiomes[Random.Range(0, possibleBiomes.Count)];
            biomeLengthRemaining = Random.Range(minBiomeLength, maxBiomeLength);
        }
    }
}
