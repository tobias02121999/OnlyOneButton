using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Initialize the public variables
    public float rotationSpeedMin;
    public float rotationSpeedMax;

    // Initialize the private variables
    float speed;

    // Run this code once at the start
    void Start()
    {
        speed = Random.Range(rotationSpeedMin, rotationSpeedMax);
    }

    // Run this code every single frame
    void Update()
    {
        transform.Rotate(0f, 0f, speed);
    }
}
