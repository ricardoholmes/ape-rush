using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public float boost = 1.1f;
    public float time = 3f;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") || collider.CompareTag("Monkey"))
        {
            GetComponent<AudioSource>().Play();

            if (!PlayerMovement.isBoosting || boost >= PlayerMovement.boostAmount)
            {
                PlayerMovement.isBoosting = true;
                PlayerMovement.boostAmount = boost;
                PlayerMovement.stopBoostingTime = Time.time + time;
            }
            else
            {
                PlayerMovement.stopBoostingTime = Time.time + (PlayerMovement.stopBoostingTime - Time.time) * boost;
            }

            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
