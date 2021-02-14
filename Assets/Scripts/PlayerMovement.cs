using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float startSpeed = 2f;
    public float acceleration = 1f;

    Vector3 directionFacing = Vector3.forward;

    new Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody.AddForce(Vector3.forward * startSpeed);
        startSpeed += acceleration;
    }
}
