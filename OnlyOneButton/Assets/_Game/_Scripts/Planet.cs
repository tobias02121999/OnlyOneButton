using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    // Initialize the public variables
    public GameObject playerObject;
    public Transform rangeTransform;
    public float gravity;

    // Initialize the private variables
    float range;
    Transform playerTransform;
    Player playerScript;

    // Run this code once at the start
    void Start()
    {
        // Get the range
        range = rangeTransform.localScale.x / 2f;

        // Get the player components
        playerTransform = playerObject.transform;
        playerScript = playerObject.GetComponent<Player>();
    }

    // Run this code every single frame
    void FixedUpdate()
    {
        if (CheckInRange())
            ApplyGravity();
    }

    // Check if the player is in range
    bool CheckInRange()
    {
        float dist = Vector3.Distance(transform.position, playerTransform.position);
        if (dist <= range)
            return true;
        else
            return false;
    }

    // Pull the player in towards the planet
    void ApplyGravity()
    {
        Vector3 relativePos = transform.position - playerTransform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

        Vector3 targetForward = rotation * Vector3.forward;

        float dist = Vector3.Distance(transform.position, playerTransform.position);
        float force = (range - dist) * gravity;

        playerScript.rb.velocity += targetForward * force;
    }
}
