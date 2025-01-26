using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform player;          // Reference to the player (or the pivot point of the sword)
    void Update()
    {
        RotateSword();
    }
    void RotateSword()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Ensure the sword stays in its current position
        mousePosition.z = transform.position.z;  // Keep the same Z position as the sword

        // Calculate the direction from the sword to the mouse position
        Vector3 direction = mousePosition - transform.position;

        // Calculate the angle in radians and convert to degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to the sword (Z-axis rotation)
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }
}
