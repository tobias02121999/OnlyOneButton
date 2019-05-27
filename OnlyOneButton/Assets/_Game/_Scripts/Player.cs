using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Initialize the public enums
    public enum states { FLYING, LANDED };

    // Initialize the public variables
    public states playerState;
    public float thrustForce;
    public ParticleSystem smokeParticleSystem;
    public float rotationSpeed;
    public Transform modelTransform;
    public Transform cameraTransform;
    public float lookRotationSpeed;
    public float lookRotationLerp;
    public Slider fuelSlider;
    public float fuelCost;
    public CollisionCheck hitColliderScript;
    public CollisionCheck landingColliderScript;
    public float refuelSpeed;

    [HideInInspector]
    public Rigidbody rb;

    // Initialize the private variables
    float rotationX;
    float fuel = 1;
    bool isStationed;

    // Run this code once at the start
    void Start()
    {
        // Get the rigidbody component
        rb = GetComponent<Rigidbody>();

        // Set the cursor settings
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Start the stationed check coroutine
        StartCoroutine(StationedCheckCycle(1, 2.5f));

        // Set the rotation to equal the current
        rotationX = transform.eulerAngles.x;
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
            // The flying player state
            case states.FLYING:
                Thrust(thrustForce); // Thrust the player forwards
                Rotate(rotationSpeed); // Rotate the player around its own axis
                UpdateUI(); // Update the UI elements
                Collide(); // Collide with the environment
                break;

            // The landed player state
            case states.LANDED:
                Thrust(thrustForce); // Thrust the player forwards
                UpdateUI(); // Update the UI elements
                Collide(); // Collide with the environment
                Refuel(); // Refuel the players fuel
                break;
        }
    }

    // Thrust the player forwards
    void Thrust(float force)
    {
        // Add forward velocity to the player
        if (Input.GetButton("Thrust") && fuel >= fuelCost)
        {
            rb.velocity += (transform.forward * thrustForce);

            if (!smokeParticleSystem.isEmitting)
                smokeParticleSystem.Play();

            fuel -= fuelCost;
        }
        else
            smokeParticleSystem.Stop();
    }

    // Rotate the player around its own axis
    void Rotate(float speed)
    {
        modelTransform.Rotate(0f, speed, 0f);

        rotationX += Input.GetAxis("Turn") * lookRotationSpeed;
        Quaternion targetRotation = Quaternion.Euler(rotationX, 90f, 90f);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookRotationLerp * Time.deltaTime);
    }

    // Update the UI elements
    void UpdateUI()
    {
        // Update the fuel slider
        fuelSlider.value = fuel;
    }

    // Collide with the environment
    void Collide()
    {
        if (hitColliderScript.isColliding)
            SceneManager.LoadScene("Game");

        if (landingColliderScript.isColliding)
            playerState = states.LANDED;
        else
        {
            if (playerState == states.LANDED)
                playerState = states.FLYING;
        }
    }

    // Refuel the players fuel
    void Refuel()
    {
        if (isStationed)
        {
            if (fuel <= 1 - refuelSpeed)
                fuel += refuelSpeed;
            else
                fuel = 1;
        }
    }

    // Check if the player is stationed or not
    IEnumerator StationedCheckCycle(int checkInterval, float rotationThreshold)
    {
        float rotationOld = transform.eulerAngles.x;
        yield return new WaitForSeconds(checkInterval);
        float rotationNew = transform.eulerAngles.x;

        float difference = Mathf.Abs(rotationOld - rotationNew);
        if (difference <= rotationThreshold)
            isStationed = true;
        else
            isStationed = false;

        StartCoroutine(StationedCheckCycle(checkInterval, rotationThreshold)); // Loop the coroutine
    }
}
