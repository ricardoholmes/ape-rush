using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public static int monkeCount = 1;

    public float startSpeed = 1f;
    public float maxSpeed = 35f;
    float currentSpeed;
    public static float acceleration = 0.5f;

    public float horizontalSpeed = 10f;

    public TextMeshProUGUI speedText;
    public TextMeshProUGUI distanceTravelled;

    new Rigidbody rigidbody;

    [Range(0.5f, 10f)]
    public float obstacleSlowCoefficient = 0.5f;

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

        distanceTravelled.text = $"{Mathf.RoundToInt(transform.position.x / 10)} m";
        //speedText.text = $"{Mathf.RoundToInt(currentSpeed * 3.6f)} km/h";

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
        float previousX = transform.position.x;
        //rigidbody.AddForce(Vector3.right * currentSpeed);
        transform.position += Vector3.right * currentSpeed;
        currentSpeed = Mathf.Clamp(currentSpeed + acceleration * Time.fixedDeltaTime, 0, maxSpeed);
        speedText.text = $"{Mathf.RoundToInt(3.6f * (transform.position.x - previousX) / (10 * Time.deltaTime))} km/h";
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Obstacle"))
        {
            float mass = collider.GetComponent<Obstacle>().mass;
            float difference = monkeCount - mass;

            if (difference >= 0) { }

            else
            {
                monkeCount = Mathf.Clamp(Mathf.FloorToInt((3 * monkeCount) / 4), 1, int.MaxValue);
            }

            Destroy(collider.gameObject);
            currentSpeed = Mathf.Clamp(currentSpeed - obstacleSlowCoefficient * mass / monkeCount, 0, maxSpeed);
        }
    }

    public void Die()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
