using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndRemove : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 previousMousePosition;

    [Header("Sway Settings")]
    public float swayForce = 5f; // Force applied to sway the object
    public float maxSwayVelocity = 10f; // Maximum velocity for swaying

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody2D found on this object!");
        }
    }

    void Update()
    {
        HandleMouseInput();
    }

    void HandleMouseInput()
    {
        // Left mouse button: Pick up and drag objects
        if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
        {
            TrySelectObject();
        }

        if (Input.GetMouseButton(0)) // Left mouse button held down
        {
            DragObject();
        }

        if (Input.GetMouseButtonUp(0)) // Left mouse button released
        {
            ReleaseObject();
        }

        // Right mouse button: Remove objects
        if (Input.GetMouseButtonDown(1)) // Right mouse button clicked
        {
            TryRemoveObject();
        }
    }

    void TrySelectObject()
    {
        // Create a ray from the mouse position
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        // Check if the ray hits this object
        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            isDragging = true;
            offset = transform.position - GetMouseWorldPosition();
            rb.isKinematic = true; // Disable physics while dragging
            previousMousePosition = GetMouseWorldPosition(); // Store initial mouse position
        }
    }

    void DragObject()
    {
        if (isDragging)
        {
            // Move the object to the mouse position with the offset
            Vector3 newPosition = GetMouseWorldPosition() + offset;
            rb.MovePosition(newPosition);

            // Calculate mouse movement direction
            Vector3 mouseDelta = GetMouseWorldPosition() - previousMousePosition;
            previousMousePosition = GetMouseWorldPosition();

            // Apply sway force based on mouse movement
            if (mouseDelta.magnitude > 0)
            {
                Vector2 swayDirection = new Vector2(mouseDelta.x, mouseDelta.y).normalized;
                rb.velocity = Vector2.ClampMagnitude(rb.velocity + swayDirection * swayForce, maxSwayVelocity);
            }
        }
    }

    void ReleaseObject()
    {
        if (isDragging)
        {
            rb.isKinematic = false; // Re-enable physics
            isDragging = false;
        }
    }

    void TryRemoveObject()
    {
        // Create a ray from the mouse position
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        // Check if the ray hits this object
        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            Destroy(gameObject); // Remove the object
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
