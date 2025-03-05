using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform shoulderL;
    public float rotationSpeed = 50f;

    void Update() {
        if (shoulderL == null) {
            Debug.LogWarning("No bone assigned");
            return;
        }

        float rotateInput = Input.GetAxis("Horizontal");
        shoulderL.Rotate(Vector3.forward * rotateInput * rotationSpeed * Time.deltaTime);
    }
}
