using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPlatform : MonoBehaviour
{
    void Update()
    {
        if (Camera.main.transform.position.x - 100 > transform.position.x)
        {
            Destroy(gameObject);
        }
    }
}
