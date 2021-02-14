using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float startSpeed = 2f;
    public float acceleration = 1f;

    new Rigidbody rigidbody;

    // Start is called before the first frame update
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 pos = transform.position;
            pos.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            transform.position = pos;

            Debug.Log(pos.x);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rigidbody.AddForce(Vector3.forward * acceleration, ForceMode.Acceleration);
        //startSpeed += acceleration;
    }
}
