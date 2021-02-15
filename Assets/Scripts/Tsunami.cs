using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tsunami : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float acceleration = PlayerMovement.acceleration * 1.005f;

    public float maxSpeed;
    public float delay = 1.5f;
    private float currentSpeed = 0;

    public TextMeshProUGUI distanceText;

    private float startTime;
    private void Awake()
    {
        startTime = Time.time + delay;
    }

    private void Start()
    {
        maxSpeed = player.GetComponent<PlayerMovement>().maxSpeed * 1.01f;
    }

    void Update()
    {
        distanceText.text = $"{Mathf.RoundToInt(player.position.x - transform.position.x)}m";
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
