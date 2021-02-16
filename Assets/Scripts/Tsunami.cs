using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tsunami : MonoBehaviour
{
    public static Transform tsunami;

    private Transform player;
    private float acceleration;

    public float maxSpeed;
    public float delay = 1f;
    private float currentSpeed = 0;

    public TextMeshProUGUI distanceText;

    private float startTime;
    private void Awake()
    {
        startTime = Time.time + delay;
    }

    private void Start()
    {
        tsunami = transform;
        player = Player.player;
        maxSpeed = player.GetComponent<PlayerMovement>().maxSpeed * 1.1f;
        acceleration = player.GetComponent<PlayerMovement>().acceleration * 1.1f;
    }

    void Update()
    {
        distanceText.text = $"{Mathf.RoundToInt((player.position.x - transform.position.x) / 10)}m";
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

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<PlayerMovement>().Die();
        }
    }
}
