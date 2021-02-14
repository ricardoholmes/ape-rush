using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform firstLevel;
    [SerializeField] private Transform nextLevel;
    [SerializeField] private Transform player;

    private float triggerDistance = 200f;
    private Vector3 lastEndPosition;
    void Start()
    {
        lastEndPosition = firstLevel.Find("EndPosition").position;
        
        for (int i = 0; i < 5; i++)
        {
            SpawnLevel();
        }
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
        return newLevelTransform;
    }
}
