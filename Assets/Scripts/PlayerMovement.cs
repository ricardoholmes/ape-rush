using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;

    public float startSpeed = 1f;
    public float maxSpeed = 10f;
    float currentSpeed;
    public float acceleration = 2f;
    public float deceleration = 1f;
    private float distanceTravelled;

    public static bool isBoosting = false;
    public static float boostAmount;
    public static float stopBoostingTime;

    public float horizontalSpeed = 10f;

    public TextMeshProUGUI speedText;
    public TextMeshProUGUI distanceTravelledText;

    new Rigidbody rigidbody;

    [Range(0.5f, 10f)]
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
        transform.position += new Vector3(0, 0, -Input.GetAxisRaw("Horizontal") * horizontalSpeed * Time.deltaTime);

        distanceTravelledText.text = $"{Mathf.RoundToInt(distanceTravelled / 10)}m";
        animator.speed = Mathf.Clamp(currentSpeed, 1f, float.PositiveInfinity);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float previousX = transform.position.x;
        //rigidbody.AddForce(Vector3.right * currentSpeed);
        transform.position += Vector3.right * currentSpeed * Time.fixedDeltaTime;
        distanceTravelled += currentSpeed * Time.fixedDeltaTime;

        if (isBoosting && Time.time > stopBoostingTime)
        {
            isBoosting = false;
        }

        if (!isBoosting && currentSpeed > maxSpeed)
        {
            // if is slowing down
            currentSpeed = Mathf.Clamp(currentSpeed - deceleration * Time.fixedDeltaTime, maxSpeed, float.PositiveInfinity);
        }
        else if (!isBoosting)
        {
            // if isnt boosting
            currentSpeed = Mathf.Clamp(currentSpeed + acceleration * Time.fixedDeltaTime, 0, maxSpeed);
            //currentSpeed = currentSpeed + acceleration * Time.fixedDeltaTime;
        }
        else
        {
            // if is boosting
            currentSpeed = Mathf.Clamp(currentSpeed + acceleration * boostAmount * Time.fixedDeltaTime, 0, maxSpeed * boostAmount);
        }

        speedText.text = $"{Mathf.RoundToInt(3.6f * (transform.position.x - previousX) / (Time.deltaTime))} kph";
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Obstacle"))
        {
            // +1 to account for player
            int monkeyCount = Player.monkeyCount + 1;

            float mass = collider.GetComponent<Obstacle>().mass;
            float difference = monkeyCount - mass;

            if (difference >= 0) { }

            else
            {
                monkeyCount = Mathf.Clamp(Mathf.FloorToInt((3 * monkeyCount) / 4), 1, int.MaxValue);
            }

            Destroy(collider.gameObject);
            currentSpeed = Mathf.Clamp(currentSpeed - obstacleSlowCoefficient * mass / monkeyCount, 0, maxSpeed);
        }
    }

    public void Die()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
