using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextPlatform : MonoBehaviour
{
    void Update()
    {
        if (Camera.main.transform.position.x - 10 > transform.position.x)
        {
            Destroy(gameObject);
        }
    }
}
