using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform firstLevel;
    [SerializeField] private Transform nextLevel;
    [SerializeField] private Transform player;

    public List<Material> biomePlaneMaterials;
    int currentBiome;
    int biomeLengthRemaining;
    public int minBiomeLength;
    public int maxBiomeLength;

    private readonly float triggerDistance = 200f;
    private Vector3 lastEndPosition;

    private void Awake()
    {
        currentBiome = 0;
        biomeLengthRemaining = Random.Range(minBiomeLength, maxBiomeLength);
    }

    void Start()
    {
        lastEndPosition = firstLevel.Find("EndPosition").position;
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
        Transform lastLevelPartTransform = SpawnLevel(lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }

    private Transform SpawnLevel(Vector3 spawnPosition)
    {
        Transform newLevelTransform = Instantiate(nextLevel, spawnPosition, Quaternion.identity);
        newLevelTransform.GetComponent<Renderer>().material = biomePlaneMaterials[currentBiome];
        biomeLengthRemaining--;
        if (biomeLengthRemaining == 0)
        {
            List<int> possibleBiomes = new List<int>();
            for (int i = 0; i < biomePlaneMaterials.Count; i++)
                if (i != currentBiome)
                    possibleBiomes.Add(i);

            currentBiome = possibleBiomes[Random.Range(0, possibleBiomes.Count)];
            biomeLengthRemaining = Random.Range(minBiomeLength, maxBiomeLength);
        }
        return newLevelTransform;
    }
}
