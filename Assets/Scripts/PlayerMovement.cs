using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static int monkeCount;

    public float startSpeed = 2f;
    public float acceleration = 1f;

    public float horizontalSpeed = 10f;

    new Rigidbody rigidbody;

    // Start is called before the first frame update
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.maxAngularVelocity = 25;
    }

    private void Update()
    {
        transform.position += new Vector3(0, 0, -Input.GetAxisRaw("Horizontal") * horizontalSpeed * Time.deltaTime);

        //if (Input.GetMouseButton(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit, 1000, LayerMask.NameToLayer("Floor")))
        //    {
        //        Debug.Log(hit.point);
        //    }
        //}
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rigidbody.AddForce(Vector3.right * acceleration, ForceMode.Acceleration);
    }

    void OnCollisionEnter(Collision collision)
    {
    }
}
