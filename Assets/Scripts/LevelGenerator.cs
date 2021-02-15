using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform firstLevel;
    [SerializeField] private Transform nextLevel;
    [SerializeField] private Transform player;

    public Transform cliff;

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
        //Instantiate(cliff, new Vector3(3 * cliff.position.x, cliff.position.y, cliff.position.z), cliff.rotation);
        //Instantiate(cliff, new Vector3(5 * cliff.position.x, cliff.position.y, cliff.position.z), cliff.rotation);
        //Instantiate(cliff, new Vector3(7 * cliff.position.x, cliff.position.y, cliff.position.z), cliff.rotation);
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
            // remove start biome and current biome from options
            // so that there arent 2 of the same biome in a row
            // and so that the start biome is unique
            List<int> possibleBiomes = new List<int>();
            for (int i = 1; i < biomePlaneMaterials.Count; i++)
                if (i != currentBiome)
                    possibleBiomes.Add(i);

            // randomly choose biome from allowed biomes and choose length of biome
            currentBiome = possibleBiomes[Random.Range(0, possibleBiomes.Count)];
            biomeLengthRemaining = Random.Range(minBiomeLength, maxBiomeLength);
        }
        return newLevelTransform;
    }
}
