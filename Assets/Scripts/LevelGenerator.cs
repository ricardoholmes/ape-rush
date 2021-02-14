using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform firstLevel;
    [SerializeField] private Transform nextLevel;
    [SerializeField] private Transform player;

    private float triggerDistance = 50f;

    //public Transform player;
    private Vector3 lastEndPosition;
    // Start is called before the first frame update
    void Start()
    {
        lastEndPosition = firstLevel.Find("EndPosition").position;
        SpawnLevel();
        SpawnLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, lastEndPosition) < triggerDistance)
        {

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
