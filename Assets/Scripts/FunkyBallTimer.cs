using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Include the TextMeshPro namespace

public class FunkyBallTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float minTimerDuration = 5f; // Minimum duration of the timer in seconds
    public float maxTimerDuration = 15f; // Maximum duration of the timer in seconds
    public TMP_Text timerText; // TextMeshPro UI Text to display the timer

    [Header("Funky Ball Settings")]
    public GameObject funkyBallPrefab; // Prefab for the funky ball
    public float minBallLifetime = 3f; // Minimum lifetime of the funky ball
    public float maxBallLifetime = 8f; // Maximum lifetime of the funky ball

    private float timeRemaining; // Time left on the timer
    private bool timerIsRunning = false; // Whether the timer is active

    void Start()
    {
        // Ensure the TextMeshPro Text and prefab are assigned
        if (timerText == null)
        {
            Debug.LogError("Timer Text (TextMeshPro) is not assigned!");
            return;
        }

        if (funkyBallPrefab == null)
        {
            Debug.LogError("Funky Ball Prefab is not assigned!");
            return;
        }

        // Initialize the timer
        ResetTimer();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            // Update the timer
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                // Timer has finished
                timeRemaining = 0;
                timerIsRunning = false;
                OnTimerFinished();
            }
        }
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        // Format the time as minutes and seconds
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Update the TextMeshPro Text
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OnTimerFinished()
    {
        // Spawn the funky ball
        SpawnFunkyBall();

        // Reset the timer
        ResetTimer();
    }

    void SpawnFunkyBall()
    {
        // Spawn the funky ball at a default position (e.g., world origin)
        GameObject funkyBall = Instantiate(funkyBallPrefab, Vector3.zero, Quaternion.identity);

        // Make the ball funky (e.g., add a random color and bouncy physics)
        MakeBallFunky(funkyBall);

        // Set a random lifetime for the funky ball
        float ballLifetime = Random.Range(minBallLifetime, maxBallLifetime);
        Destroy(funkyBall, ballLifetime);
    }

    void MakeBallFunky(GameObject ball)
    {
        // Add a random color
        Renderer ballRenderer = ball.GetComponent<Renderer>();
        if (ballRenderer != null)
        {
            ballRenderer.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f); // Random bright color
        }

        // Add bouncy physics (Rigidbody2D)
        Rigidbody2D rb2D = ball.GetComponent<Rigidbody2D>();
        if (rb2D != null)
        {
            rb2D.velocity = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f)); // Random initial velocity
        }
    }

    void ResetTimer()
    {
        // Set a random duration for the timer
        timeRemaining = Random.Range(minTimerDuration, maxTimerDuration);
        timerIsRunning = true;

        // Update the timer display immediately
        UpdateTimerDisplay(timeRemaining);
    }
}
