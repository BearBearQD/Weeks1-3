using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallowMouse : MonoBehaviour
{
    public float followSpeed = 5f;    // Speed at which the object follows the mouse
    public float rotationSpeed = 10f; // Speed at which the object rotates towards the mouse

    private Vector3 targetPosition;

    void Start()
    {
        // Set the initial target position as the current position of the object
        targetPosition = transform.position;
    }

    void Update()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Lock the Z axis to the current object's Z position
        mousePosition.z = transform.position.z;

        // Move towards the mouse position with a delay
        targetPosition = Vector3.Lerp(transform.position, mousePosition, followSpeed * Time.deltaTime);

        // Move the object to the target position
        transform.position = targetPosition;

        // Rotate the object towards the mouse position
        RotateTowardsMouse(mousePosition);
    }

    void RotateTowardsMouse(Vector3 mousePosition)
    {
        // Calculate the direction from the object to the mouse position
        Vector3 direction = mousePosition - transform.position;

        // Calculate the angle in radians and convert it to degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Smoothly rotate the object towards the mouse position using Lerp
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 90));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
