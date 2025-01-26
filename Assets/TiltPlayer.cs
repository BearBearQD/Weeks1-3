using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltPlayer : MonoBehaviour
{
    public float tiltAmount = 10f;        // Amount of tilt (degrees)
    public float tiltSpeed = 5f;          // Speed of tilting
    private float currentTilt = 0f;       // Current tilt of the player

    private float horizontalInput;        // Horizontal movement input (e.g., A/D or arrow keys)

    void Update()
    {
        // Get horizontal input (use either Input.GetAxis for smooth input or Input.GetKeyDown for discrete movement)
        horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the target tilt based on horizontal input
        float targetTilt = horizontalInput * tiltAmount;

        // Smoothly interpolate the current tilt towards the target tilt
        currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * tiltSpeed);

        // Apply the tilt to the player by rotating around the Z-axis
        transform.rotation = Quaternion.Euler(0f, 0f, currentTilt);
    }
}
