using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FunkyBallTimer : MonoBehaviour
{
    public float timerDuration = 10f; // Duration of the timer in seconds
    public Text timerText; // UI Text to display the timer
    public GameObject funkyBallPrefab; // Prefab for the funky ball

    private float timeRemaining; // Time left on the timer
    private bool timerIsRunning = false; // Whether the timer is active

    void Start()
    {
        // Ensure the UI Text and prefab are assigned
        if (timerText == null)
        {
            Debug.LogError("Timer Text is not assigned!");
            return;
        }

        if (funkyBallPrefab == null)
        {
            Debug.LogError("Funky Ball Prefab is not assigned!");
            return;
        }

        // Initialize the timer
        timeRemaining = timerDuration;
        timerIsRunning = true;
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

        // Update the UI Text
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OnTimerFinished()
    {
        // Spawn the funky ball
        SpawnFunkyBall();

        // Optionally, reset the timer
        // ResetTimer();
    }

    void SpawnFunkyBall()
    {
        // Spawn the funky ball at a default position (e.g., world origin)
        GameObject funkyBall = Instantiate(funkyBallPrefab, Vector3.zero, Quaternion.identity);

        // Make the ball funky (e.g., add a random color and bouncy physics)
        MakeBallFunky(funkyBall);
    }

    void MakeBallFunky(GameObject ball)
    {
        // Add a random color
        Renderer ballRenderer = ball.GetComponent<Renderer>();
        if (ballRenderer != null)
        {
            ballRenderer.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f); // Random bright color
        }

        // Add bouncy physics (if using Rigidbody2D or Rigidbody)
        Rigidbody2D rb2D = ball.GetComponent<Rigidbody2D>();
        if (rb2D != null)
        {
            rb2D.velocity = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f)); // Random initial velocity
        }
        else
        {
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0); // Random initial velocity
            }
        }
    }

    void ResetTimer()
    {
        // Reset the timer to its initial duration
        timeRemaining = timerDuration;
        timerIsRunning = true;
    }
}
