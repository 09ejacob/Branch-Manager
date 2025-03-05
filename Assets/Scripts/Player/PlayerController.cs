using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform shoulderL;
    public Transform shoulderR;
    public Transform cameraTransform;
    public float rotationSpeed = 5f;

    void Update()
    {
        if (shoulderL == null || shoulderR == null || cameraTransform == null)
        {
            Debug.LogWarning("Something is not assigned");
            return;
        }

        Vector3 oppositeDirection = -cameraTransform.forward;

        Quaternion leftTargetRotation = Quaternion.LookRotation(oppositeDirection) * Quaternion.Euler(-90, 0, 0); 
        Quaternion rightTargetRotation = Quaternion.LookRotation(oppositeDirection) * Quaternion.Euler(-90, 0, 0);  

        if (Input.GetKey(KeyCode.Mouse0))
        {
            shoulderL.rotation = Quaternion.Slerp(shoulderL.rotation, leftTargetRotation, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            shoulderR.rotation = Quaternion.Slerp(shoulderR.rotation, rightTargetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
