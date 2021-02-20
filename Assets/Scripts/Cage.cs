using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    public static int monkeyCount;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Player.monkeyCount++;
            monkeyCount++;

            GetComponent<AudioSource>().Play();
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
