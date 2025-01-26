using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveScript : MonoBehaviour
{
    public float moveSpeed = 5f;        // Movement speed
    public float lerpSpeed = 10f;       // Speed for Lerp smoothing

    private float targetX;              // Target position for Lerp
    private Vector3 currentPos;         // Current position of the player

    void Start()
    {
        // Set initial target to the player's starting X position
        targetX = transform.position.x;
    }

    void Update()
    {
        HandleInput();
        SmoothMove();

        // Handle teleport when "LeftShift" is pressed
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashToCursor();
        }
    }

    // Handle input for moving left and right
    void HandleInput()
    {
        Debug.Log("Move left with a and right with d");
        Debug.Log("Dash with LeftShift");
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // Move left by decreasing the X position
            targetX -= moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            // Move right by increasing the X position
            targetX += moveSpeed * Time.deltaTime;
        }
    }

    // Smoothly move the player using Lerp
    void SmoothMove()
    {
        // Get the current position of the player
        currentPos = transform.position;

        // Interpolate between the current position and the target position
        float newX = Mathf.Lerp(currentPos.x, targetX, lerpSpeed * Time.deltaTime);

        // Update the player's position with the new interpolated X value
        transform.position = new Vector3(newX, currentPos.y, currentPos.z);
    }

    // Teleport to the cursor's X position on the screen
    void DashToCursor()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Only teleport on the X axis, keep the current Y and Z positions
        targetX = mousePosition.x;
    }
}
