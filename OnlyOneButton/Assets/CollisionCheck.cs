using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    // Initialize the public variables

    public bool isColliding;

    // Check if the trigger is colliding
    void OnTriggerStay(Collider other)
    {
        isColliding = true;
    }

    // Check if the trigger is not colliding
    void OnTriggerExit(Collider other)
    {
        isColliding = false;
    }

    // Check if the collider is colliding
    void OnCollisionStay(Collision collision)
    {
        isColliding = true;
    }

    // Check if the collider is not colliding
    void OnCollisionExit(Collision collision)
    {
        isColliding = false;
    }
}
