using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public float acceleration;
    public Rigidbody player;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            player.AddForce(Vector3.right * acceleration, ForceMode.Acceleration);
            Destroy(gameObject);
        }
    }
}
