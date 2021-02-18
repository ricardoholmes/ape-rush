using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Tsunami : MonoBehaviour
{
    public static Transform tsunami;
    public Transform player;

    public Animator fadeAnimator;
    public static bool playerDead;

    public float acceleration;
    private float maxSpeed;
    private float initialMaxSpeed;

    public float delay = 1f;
    private float currentSpeed = 0;

    public TextMeshProUGUI distanceText;

    private float startTime;
    private void Awake()
    {
        playerDead = false;
        startTime = Time.time + delay;
    }

    private void Start()
    {
        tsunami = transform;
        initialMaxSpeed = player.GetComponent<PlayerMovement>().maxSpeed * 1.1f;
        acceleration = player.GetComponent<PlayerMovement>().acceleration * 1.1f;
    }

    void Update()
    {
        if (!playerDead)
        {
            distanceText.text = $"{Mathf.RoundToInt((player.position.x - transform.position.x) / 10)}m";
        }
    }

    void FixedUpdate()
    {
        if (!playerDead && player.position.x - transform.position.x <= 10f)
        {
            maxSpeed = player.GetComponent<PlayerMovement>().maxSpeed * 0.9f;
        }
        else
        {
            maxSpeed = initialMaxSpeed;
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
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            CameraMovement.stop = true;
            playerDead = true;
            transform.GetChild(0).gameObject.SetActive(true);
            Destroy(player.gameObject);
            StartCoroutine(FadeOut());
        }
    }
}
