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
            for (int i = 0; i < transform.childCount; i++)
                if (transform.GetChild(i).CompareTag("Cage"))
                    abandonded++;
            Destroy(gameObject);
        }
    }
}
