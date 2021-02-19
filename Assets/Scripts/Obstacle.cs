using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int mass = 1;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            //Player.monkeyCount Mathf.Clamp(Mathf.FloorToInt(monkeyCount * 3 / 4), 1, int.MaxValue);
            Player.monkeyCount = Mathf.Clamp(Player.monkeyCount - mass, 0, int.MaxValue);
            Destroy(gameObject);
        }
    }
}
