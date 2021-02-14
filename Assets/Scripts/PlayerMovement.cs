using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static int monkeCount = 1;

    public float startSpeed = 20f;
    float currentSpeed;
    public static float acceleration = 10f;

    public float horizontalSpeed = 10f;

    new Rigidbody rigidbody;

    private void Awake()
    {
        currentSpeed = startSpeed;
    }

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
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            float difference = monkeCount - collision.transform.GetComponent<Obstacle>().mass;
            if (difference > 0)
            {
                Destroy(collision.gameObject);
            }

            else if (difference == 0)
            {
                if (monkeCount == 1)
                {
                    Destroy(collision.gameObject);
                }
            }

            else
            {

            }
        }
    }
}
