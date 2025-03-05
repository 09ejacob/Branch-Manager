using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform shoulderL;
    public Transform cameraTransform;

    public float rotationSpeed = 5f;

    void Update() {
        if (shoulderL == null || cameraTransform == null) {
            Debug.LogWarning("Something is not assigned");
            return;
        }

        Vector3 oppositeDirection = -cameraTransform.forward;
        Quaternion targetRotation = Quaternion.LookRotation(oppositeDirection, Vector3.up);
        shoulderL.rotation = Quaternion.Slerp(shoulderL.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
