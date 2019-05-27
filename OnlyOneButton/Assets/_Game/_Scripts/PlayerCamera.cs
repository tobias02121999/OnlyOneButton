using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Initialize the public variables
    public Transform playerTransform;

    // Run this code every single frame
    void Update()
    {
        transform.position = playerTransform.position;
    }
}
