using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Initialize the public enums
    public enum states { DEFAULT };

    // Initialize the public variables
    public states playerState;
    public float thrustForce;
    public ParticleSystem smokeParticleSystem;
    public float rotationSpeed;
    public Transform modelTransform;
    public Transform cameraTransform;
    public float lookRotationSpeed;

    [HideInInspector]
    public Rigidbody rb;

    // Initialize the private variables
    float rotationX;

    // Run this code once at the start
    void Start()
    {
        // Get the rigidbody component
        rb = GetComponent<Rigidbody>();

        // Set the cursor settings
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Run this code every single frame
    void FixedUpdate()
    {
        RunState(playerState); // Run the current player state
    }

    // Run the current player state
    void RunState(states state)
    {
        // Switch through the different player states
        switch (state)
        {
            // The default player state
            case states.DEFAULT:
                Thrust(thrustForce); // Thrust the player forwards
                Rotate(rotationSpeed); // Rotate the player around its own axis
                break;
        }
    }

    // Thrust the player forwards
    void Thrust(float force)
    {
        // Add forward velocity to the player
        if (Input.GetButton("Thrust"))
        {
            rb.velocity += (transform.forward * thrustForce);

            if (!smokeParticleSystem.isEmitting)
                smokeParticleSystem.Play();
        }
        else
            smokeParticleSystem.Stop();
    }

    // Rotate the player around its own axis
    void Rotate(float speed)
    {
        modelTransform.Rotate(0f, speed, 0f);

        rotationX += Input.GetAxis("Mouse X") * lookRotationSpeed;
        transform.rotation = Quaternion.Euler(rotationX, 90f, 90f);
    }
}
