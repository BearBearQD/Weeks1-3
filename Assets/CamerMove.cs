using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerMove : MonoBehaviour
{
    public float moveSpeed = 0.1f;   // Speed at which the camera moves based on the mouse position
    public Vector2 offsetRange = new Vector2(1f, 1f);  // Maximum range of the camera's movement

    private Vector3 targetPosition;

    void Update()
    {
        // Get the mouse position in screen space
        Vector3 mousePosition = Input.mousePosition;

        // Normalize mouse position (so it ranges from 0 to 1 across the screen width and height)
        float normalizedX = mousePosition.x / Screen.width;
        float normalizedY = mousePosition.y / Screen.height;

        // Calculate the offset based on the mouse's position, scaled by the range
        float offsetX = (normalizedX - 0.5f) * offsetRange.x * 2f; // Range -1 to 1
        float offsetY = (normalizedY - 0.5f) * offsetRange.y * 2f; // Range -1 to 1

        // Set the target position with the offset
        targetPosition = new Vector3(offsetX, offsetY, transform.position.z);

        // Smoothly move the camera towards the target position with Lerp
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed);
    }
}
