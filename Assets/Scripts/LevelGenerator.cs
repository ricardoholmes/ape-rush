using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject[] maps;

    public List<Material> biomePlaneMaterials;
    int currentBiome;
    int biomeLengthRemaining;
    public int minBiomeLength;
    public int maxBiomeLength;

    private readonly float triggerDistance = 200f;
    private Vector3 lastEndPosition;

    private void Awake()
    {
        // reset biome and biome length remaining
        currentBiome = 0;
        biomeLengthRemaining = Random.Range(minBiomeLength, maxBiomeLength);
    }

    void Start()
    {
        lastEndPosition = new Vector3(player.position.x, 0);
    }

    void Update()
    {
        if (Vector3.Distance(player.position, lastEndPosition) < triggerDistance)
        {
            SpawnLevel();
        }
    }

    private void SpawnLevel()
    {
        Transform newLevelTransform = Instantiate(maps[Random.Range(0, maps.Length)].transform, lastEndPosition, Quaternion.identity);
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

        lastEndPosition = newLevelTransform.Find("EndPosition").position;
    }
}
