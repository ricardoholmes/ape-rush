using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Tsunami : MonoBehaviour
{
    public static Transform tsunami;
    private Transform player;

    private Animator animator;
    private bool isDead;

    private float acceleration;
    private float maxSpeed;

    public float delay = 1f;
    private float currentSpeed = 0;

    public TextMeshProUGUI distanceText;

    private float startTime;
    private void Awake()
    {
        isDead = false;
        player = Player.player;
        startTime = Time.time + delay;
    }

    private void Start()
    {
        tsunami = transform;
        animator = GetComponent<Animator>();
        maxSpeed = player.GetComponent<PlayerMovement>().maxSpeed * 1.1f;
        acceleration = player.GetComponent<PlayerMovement>().acceleration * 1.1f;
    }

    void Update()
    {
        if (!isDead)
        {
            distanceText.text = $"{Mathf.RoundToInt((player.position.x - transform.position.x) / 10)}m";
        }
    }

    void FixedUpdate()
    {
        if (Time.time > startTime)
        {
            transform.position += Vector3.right * currentSpeed * Time.fixedDeltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed + acceleration * Time.fixedDeltaTime, 0, maxSpeed);
        }
    }

    IEnumerator FloatAway()
    {
        animator.SetTrigger("die");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            CameraMovement.stop = true;
            isDead = true;
            Destroy(player.gameObject);
            StartCoroutine(FloatAway());
        }
    }
}
