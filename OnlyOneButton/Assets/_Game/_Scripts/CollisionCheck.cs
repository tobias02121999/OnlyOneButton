using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    public bool isColliding;

    private void OnTriggerEnter(Collider other)
    {
        isColliding = true;
        Debug.Log(other.name);
    }

    private void OnTriggerExit(Collider other)
    {
        isColliding = false;
        Debug.Log(other.name);
    }
}
