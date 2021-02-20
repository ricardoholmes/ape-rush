using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;

    public float startSpeed = 1f;
    public float maxSpeed = 10f;
    public float currentSpeed;
    public float acceleration = 2f;
    public float deceleration = 1f;
    private float distanceTravelled;

    public float rotationAngle = 45f;
    public float rotationSpeed = 10f;

    public static bool isBoosting = false;
    public static float boostAmount;
    public static float stopBoostingTime;

    //public float horizontalSpeed = 10f;
    public float horizontalForce = 10f;

    public GameObject obstacleDestroyParticles;

    public TextMeshProUGUI speedText;
    public TextMeshProUGUI distanceTravelledText;

    new Rigidbody rigidbody;

    public float obstacleSlowCoefficient = 0.5f;

    private void Awake()
    {
        distanceTravelled = 0;
        currentSpeed = startSpeed;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.maxAngularVelocity = 25;
    }

    private void Update()
    {
        
        Quaternion target = Quaternion.Euler(0, -90 + Input.GetAxisRaw("Horizontal") * rotationAngle, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * rotationSpeed);

        if (Mathf.Abs(transform.position.z) >= 17f)
            transform.position = new Vector3(transform.position.x, transform.position.y, 11f);

        distanceTravelledText.text = $"{Mathf.RoundToInt(distanceTravelled / 10)}m";
        animator.speed = Mathf.Clamp(currentSpeed / 50, 1f, float.PositiveInfinity);
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = Input.GetAxisRaw("Horizontal") * horizontalForce * Vector3.back;

        float previousX = transform.position.x;
        //rigidbody.AddForce(Vector3.right * currentSpeed);
        transform.position += Vector3.right * currentSpeed * Time.fixedDeltaTime;
        distanceTravelled += currentSpeed * Time.fixedDeltaTime;

        float currentMaxSpeed = maxSpeed + 1f * Player.monkeyCount;

        if (isBoosting && Time.time > stopBoostingTime)
        {
            isBoosting = false;
        }

        if (!isBoosting && currentSpeed > maxSpeed)
        {
            // if is slowing down
            currentSpeed = Mathf.Clamp(currentSpeed - deceleration * Time.fixedDeltaTime, currentMaxSpeed, float.PositiveInfinity);
        }
        else if (!isBoosting)
        {
            // if isnt boosting
            currentSpeed = Mathf.Clamp(currentSpeed + acceleration * Time.fixedDeltaTime, 0, currentMaxSpeed);
            //currentSpeed = currentSpeed + acceleration * Time.fixedDeltaTime;
        }
        else
        {
            // if is boosting
            currentSpeed = Mathf.Clamp(currentSpeed + acceleration * boostAmount * Time.fixedDeltaTime, 0, maxSpeed * boostAmount + 1f * Player.monkeyCount);
        }

        if (currentSpeed > currentMaxSpeed)
            speedText.color = new Color(255, 0, 0);

        else if (currentSpeed == currentMaxSpeed)
            speedText.color = new Color(255, 132, 0);

        else
            speedText.color = new Color(0, 115, 255);

        speedText.text = $"{Mathf.RoundToInt(3.6f * (transform.position.x - previousX) / (Time.fixedDeltaTime))}kmph";
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Obstacle") || collider.CompareTag("Cage"))
        {
            transform.GetChild(0).GetComponent<AudioSource>().Play();
            if (collider.CompareTag("Obstacle"))
            {
                int monkeyCount = Player.monkeyCount + 1;

                float mass = collider.GetComponent<Obstacle>().mass;

                currentSpeed = Mathf.Clamp(currentSpeed - obstacleSlowCoefficient * mass / monkeyCount, 0, maxSpeed);

                Instantiate(obstacleDestroyParticles, collider.transform.position, obstacleDestroyParticles.transform.rotation, collider.transform.parent);
            }
        }
    }
}
