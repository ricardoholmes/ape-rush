using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tsunami : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float acceleration = PlayerMovement.acceleration * 1.05f;

    public float maxSpeed = 42f;
    public float delay = 1.5f;
    private float currentSpeed = 0;

    public TextMeshProUGUI distanceText;

    new Rigidbody rigidbody;

    private float startTime;
    private void Awake()
    {
        startTime = Time.time + delay;
    }

    void Update()
    {
        distanceText.text = $"{Mathf.RoundToInt(player.position.x - transform.position.x)} m";
    }

    void FixedUpdate()
    {
        if (Time.time > startTime)
        {
            transform.position += Vector3.right * currentSpeed;
            currentSpeed = Mathf.Clamp(currentSpeed + acceleration * Time.fixedDeltaTime, 0, maxSpeed);
        }
        // GetComponent<Rigidbody>().AddForce(Vector3.right * acceleration, ForceMode.Acceleration);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("DEATH");
            //collision.gameObject.Die();
        }
    }
}
