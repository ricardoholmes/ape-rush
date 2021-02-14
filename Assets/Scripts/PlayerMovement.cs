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
        rigidbody.maxAngularVelocity = 25;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, LayerMask.NameToLayer("Terrain")))
            {
                Debug.Log(hit.point);
            }
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rigidbody.AddForce(Vector3.forward * acceleration, ForceMode.Acceleration);
        //startSpeed += acceleration;
    }
}
