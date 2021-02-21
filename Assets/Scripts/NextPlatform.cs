using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPlatform : MonoBehaviour
{
    public static int abandonded;

    void Update()
    {
        if (Camera.main.transform.position.x - 100 > transform.position.x)
        {
            abandonded += GetComponentsInChildren<Cage>().Length;
            Destroy(gameObject);
        }
    }
}
