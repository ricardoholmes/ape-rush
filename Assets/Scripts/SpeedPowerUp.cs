using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public float acceleration = 0.5f;
    public static Transform player;

    void Start()
    {
        player = transform;
    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            player.transform.position += Vector3.right * acceleration;


            Destroy(gameObject);
        }
    }
}
