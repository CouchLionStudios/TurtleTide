using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MovingObject.cs is used for inheritance by any other object that needs to move towards the player.
/// Examples: the ground, enemies, obstacles, etc.
/// </summary>
public class MovingObject : MonoBehaviour
{

    public float forwardSpeed = .05f;

    // Update is called once per frame
    void Update()
    {
        MoveSelf();
    }

    void MoveSelf()
    {
        transform.position += new Vector3(0, 0, -forwardSpeed);

        // returns self to pool if we are past the player and out of sight
        if (transform.position.z < -80)
        {
            //Debug.Log("BasicEnemy.cs - Update()");

            //gameObject.SetActive(false); eventually we should use for objectPooling
        }
    }
}
