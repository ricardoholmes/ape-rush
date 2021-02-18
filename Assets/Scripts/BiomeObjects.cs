using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BiomeObjects
{
    [SerializeField]
    private string name;

    [SerializeField]
    public Material floorMaterial;

    [SerializeField]
    public GameObject[] obstacles;
}
