using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunkyBall : MonoBehaviour
{
    [Header("Bounce Settings")]
    public float minForce = 5f; // Minimum force applied to the ball
    public float maxForce = 10f; // Maximum force applied to the ball
    public float changeDirectionInterval = 2f; // Time interval to change direction

    private Rigidbody2D rb2D; // Rigidbody2D component
    private float timer;

    void Start()
    {
        // Get the Rigidbody2D component
        rb2D = GetComponent<Rigidbody2D>();

        if (rb2D == null)
        {
            Debug.LogError("No Rigidbody2D found on this object!");
            return;
        }

        // Apply an initial random force
        ApplyRandomForce();
    }

    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Change direction at regular intervals
        if (timer >= changeDirectionInterval)
        {
            ApplyRandomForce();
            timer = 0f; // Reset the timer
        }
    }

    void ApplyRandomForce()
    {
        // Generate a random direction
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        // Generate a random force magnitude
        float forceMagnitude = Random.Range(minForce, maxForce);

        // Apply the force to the ball
        rb2D.AddForce(randomDirection * forceMagnitude, ForceMode2D.Impulse);
    }
}
