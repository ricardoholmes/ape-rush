using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int mass = 1;
    public static int sacrificedCount;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            float distance = Player.player.position.x - Tsunami.tsunami.position.x;
            if (distance <= 20)
                Tsunami.kill = true;

            int temp = Player.monkeyCount;
            Player.monkeyCount = Mathf.Clamp(Player.monkeyCount - mass, 0, int.MaxValue);
            sacrificedCount += temp - Player.monkeyCount;
            Destroy(gameObject);
        }
    }
}
