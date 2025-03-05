using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform shoulderL; // Left Shoulder
    public Transform shoulderR; // Right Shoulder
    public Transform cameraTransform; // Camera Transform
    public float rotationSpeed = 50f; // Rotation speed for arm tracking
    public float shoulderRotationSpeed = 200f; // Speed for additional Z-axis rotation

    private float shoulderLRotationZ = 0f; // Track left shoulder Z rotation
    private float shoulderRRotationZ = 0f; // Track right shoulder Z rotation

    void Update()
    {
        if (shoulderL == null || shoulderR == null || cameraTransform == null)
        {
            Debug.LogWarning("Something is not assigned!");
            return;
        }

        // Get opposite direction of the camera
        Vector3 oppositeDirection = -cameraTransform.forward;

        // Calculate base rotation (up/down tracking from camera)
        Quaternion leftTargetRotation = Quaternion.LookRotation(oppositeDirection) * Quaternion.Euler(-90, 0, 0);
        Quaternion rightTargetRotation = Quaternion.LookRotation(oppositeDirection) * Quaternion.Euler(-90, 0, 0);

        // Get mouse horizontal movement (sideways)
        float mouseDeltaX = Input.GetAxis("Mouse X");

        // Rotate left shoulder when left mouse button is held
        if (Input.GetKey(KeyCode.Mouse0))
        {
            shoulderLRotationZ += mouseDeltaX * shoulderRotationSpeed * Time.deltaTime; // Apply Z-axis rotation
            Quaternion zRotation = Quaternion.Euler(0, 0, shoulderLRotationZ);
            shoulderL.rotation = Quaternion.Slerp(shoulderL.rotation, leftTargetRotation * zRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Reset Z rotation slowly when the mouse button is released
            shoulderLRotationZ = Mathf.Lerp(shoulderLRotationZ, 0, Time.deltaTime * rotationSpeed);
            shoulderL.rotation = Quaternion.Slerp(shoulderL.rotation, leftTargetRotation, rotationSpeed * Time.deltaTime);
        }

        // Rotate right shoulder when right mouse button is held
        if (Input.GetKey(KeyCode.Mouse1))
        {
            shoulderRRotationZ -= mouseDeltaX * shoulderRotationSpeed * Time.deltaTime; // Inverted for right arm
            Quaternion zRotation = Quaternion.Euler(0, 0, shoulderRRotationZ);
            shoulderR.rotation = Quaternion.Slerp(shoulderR.rotation, rightTargetRotation * zRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Reset Z rotation slowly when the mouse button is released
            shoulderRRotationZ = Mathf.Lerp(shoulderRRotationZ, 0, Time.deltaTime * rotationSpeed);
            shoulderR.rotation = Quaternion.Slerp(shoulderR.rotation, rightTargetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
