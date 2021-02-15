using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public float acceleration = 50f;
    public Transform player;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log(PlayerMovement.player.transform.position);
            PlayerMovement.player.transform.position += Vector3.right * acceleration;
            Debug.Log(PlayerMovement.player.transform.position);

            Destroy(gameObject);
        }
    }
}
