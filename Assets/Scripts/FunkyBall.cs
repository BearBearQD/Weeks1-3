using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunkyBall : MonoBehaviour
{
    [Header("Jolt Settings")]
    public float minForce = 5f; // Minimum force applied to the ball
    public float maxForce = 15f; // Maximum force applied to the ball
    public float minInterval = 0.5f; // Minimum time between jolts
    public float maxInterval = 2f; // Maximum time between jolts

    private Rigidbody2D rb2D; // Rigidbody2D component
    private float nextJoltTime; // Time when the next jolt will occur

    void Start()
    {
        // Get the Rigidbody2D component
        rb2D = GetComponent<Rigidbody2D>();

        if (rb2D == null)
        {
            Debug.LogError("No Rigidbody2D found on this object!");
            return;
        }

        // Set the initial time for the next jolt
        SetNextJoltTime();
    }

    void Update()
    {
        // Check if it's time to apply a jolt
        if (Time.time >= nextJoltTime)
        {
            ApplyRandomJolt();
            SetNextJoltTime(); // Schedule the next jolt
        }
    }

    void ApplyRandomJolt()
    {
        // Generate a random direction
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        // Generate a random force magnitude
        float forceMagnitude = Random.Range(minForce, maxForce);

        // Apply the force to the ball
        rb2D.AddForce(randomDirection * forceMagnitude, ForceMode2D.Impulse);
    }

    void SetNextJoltTime()
    {
        // Set a random time for the next jolt
        float interval = Random.Range(minInterval, maxInterval);
        nextJoltTime = Time.time + interval;
    }
}
