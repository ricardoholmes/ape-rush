using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Tsunami : MonoBehaviour
{
    public static Transform tsunami;
    public Transform player;

    private AudioSource audioSource;

    public float maxHearingDistance = 100f;

    public Animator fadeAnimator;
    public static bool playerDead;

    public static bool kill;

    readonly float closeKillDelay = 7f;
    float stopSlow;

    public float maxSpeedAcceleration;
    private float acceleration;
    private float initialAcceleration;

    private float maxSpeed;
    private float initialMaxSpeed;

    private bool firstHit = true;

    public float delay = 1f;
    private float currentSpeed = 0;

    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI distanceTravelled;

    private float startTime;

    float previousDistance;

    private void Awake()
    {
        kill = false;
        firstHit = true;
        playerDead = false;
        startTime = Time.time + delay;

        Cage.monkeyCount = 0;
        Obstacle.sacrificedCount = 0;
        NextPlatform.abandonded = 0;
    }

    private void Start()
    {
        tsunami = transform;
        initialMaxSpeed = player.GetComponent<PlayerMovement>().maxSpeed * 1.3f;
        initialAcceleration = player.GetComponent<PlayerMovement>().acceleration * 1.1f;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!playerDead)
        {
            audioSource.volume = 0.1f * Mathf.Clamp(1 - ((player.position.x - transform.position.x) / maxHearingDistance), 0.1f, 1);
            distanceText.text = $"{Mathf.RoundToInt((player.position.x - transform.position.x) / 10)}m";
        }
    }

    void FixedUpdate()
    {
        if (!playerDead)
        {
            float distance = (player.position.x - transform.position.x) / 10;
            float playerAcceleration = player.GetComponent<PlayerMovement>().acceleration;
            float playerSpeed = player.GetComponent<PlayerMovement>().currentSpeed;

            if (distance < 0)
                KillPlayer();

            if (!firstHit && Time.time >= stopSlow)
                kill = true;

            if (previousDistance < distance)
                kill = false;

            if (distance <= 1f && firstHit && !kill)
            {
                maxSpeed = PlayerMovement.currentMaxSpeed;
                currentSpeed = playerSpeed;
                acceleration = playerAcceleration;
                firstHit = false;

                stopSlow = Time.time + closeKillDelay;
            }
            else if (distance <= 2f && firstHit && !kill)
            {
                maxSpeed = playerSpeed * 1.2f;
                acceleration = playerAcceleration * 1.1f;
            }
            else if (distance > 2f || kill)
            {
                firstHit = true;

                if (distance > 5f)
                    maxSpeed += maxSpeedAcceleration * Time.fixedDeltaTime * distance;
                else
                    maxSpeed = initialMaxSpeed;

                acceleration = initialAcceleration;
            }

            previousDistance = distance;
        }

        if (Time.time > startTime)
        {
            transform.position += Vector3.right * currentSpeed * Time.fixedDeltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed + acceleration * Time.fixedDeltaTime, 0, maxSpeed);
        }
    }

    IEnumerator FadeOut()
    {
        fadeAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GameOver");
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            KillPlayer();
        }
    }

    void KillPlayer()
    {
        CameraMovement.stop = true;
        playerDead = true;
        transform.GetChild(0).GetComponent<AudioSource>().Play();
        string distance = distanceTravelled.text;
        PlayerPrefs.SetString("Score", distance);
        if (int.Parse(PlayerPrefs.GetString("HighestScore", "0m").Split('m')[0]) < int.Parse(distance.Split('m')[0]))
            PlayerPrefs.SetString("HighestScore", distance);

        PlayerPrefs.SetInt("Rescued", Cage.monkeyCount);
        PlayerPrefs.SetInt("Fallen", Obstacle.sacrificedCount);
        PlayerPrefs.SetInt("Abandoned", NextPlatform.abandonded);

        PlayerPrefs.Save();
        Destroy(player.gameObject);
        StartCoroutine(FadeOut());
    }
}
