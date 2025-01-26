using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PupilEyemovement : MonoBehaviour
{
    public Transform leftEye;           // Reference to the left eye (center)
    public Transform rightEye;          // Reference to the right eye (center)
    public Transform leftPupil;         // Reference to the left pupil (object to move)
    public Transform rightPupil;        // Reference to the right pupil (object to move)
    public float eyeRadius = 0.5f;      // The maximum distance the pupil can move from the center

    private Vector3 mousePos;
    void Update()
    {
        // Get the mouse position in world coordinates
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Move the left pupil towards the mouse position
        MovePupil(leftEye, leftPupil);

        // Move the right pupil towards the mouse position
        MovePupil(rightEye, rightPupil);
    }

    // Moves the pupil towards the mouse position with a limit (eyeRadius)
    void MovePupil(Transform eye, Transform pupil)
    {
        // Calculate the direction from the eye to the mouse
        Vector3 direction = (mousePos - eye.position).normalized;

        // Calculate the target position within the eye radius
        Vector3 targetPos = eye.position + direction * Mathf.Min(Vector3.Distance(mousePos, eye.position), eyeRadius);

        // Move the pupil to the target position
        pupil.position = new Vector3(targetPos.x, targetPos.y, pupil.position.z);
    }
}
