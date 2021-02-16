using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cages : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerMovement.monkeCount++;
            Destroy(gameObject);
        }
    }
}
