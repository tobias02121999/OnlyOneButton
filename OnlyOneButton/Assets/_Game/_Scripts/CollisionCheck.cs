using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    private int collisionCount = 0;

    public bool isColliding
    {
        get { return collisionCount != 0; }
    }

    void OnTriggerEnter(Collider other)
    {
        collisionCount++;
    }

    void OnTriggerExit(Collider other)
    {
        collisionCount--;
    }
}
