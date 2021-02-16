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

        if (lastEndPosition.x >= 1000)
        {
            MoveBack();
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
