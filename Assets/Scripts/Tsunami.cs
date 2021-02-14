using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tsunami : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float acceleration = PlayerMovement.acceleration;

    new Rigidbody rigidbody;

    void Start()
    {
    }

    void Update()
    {
       
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.right * acceleration, ForceMode.Acceleration);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("DEATH");
            //collision.gameObject.Die();
        }
    }
}
