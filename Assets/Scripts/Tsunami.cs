using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tsunami : MonoBehaviour
{
    public static Transform tsunami;
    public Transform player;

    private float acceleration;
    private float maxSpeed;

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
            transform.position += Vector3.right * currentSpeed * Time.fixedDeltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed + acceleration * Time.fixedDeltaTime, 0, maxSpeed);
        }
    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            //Player.Die();
            Debug.Log("DIE");
        }
    }
}
