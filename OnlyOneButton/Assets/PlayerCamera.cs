using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Initialize the public variables
    public float sensitivityX;
    public float sensitivityY;
    public Transform playerTransform;

    // Initialize the private variables
    float rotationX;
    float rotationY;

    // Run this code every single frame
    void Update()
    {
        rotationX += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationY += Input.GetAxis("Mouse X") * sensitivityX;

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
        transform.position = playerTransform.position;
    }
}
