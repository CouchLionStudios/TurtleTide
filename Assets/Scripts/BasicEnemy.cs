using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BasicEnemy.cs is attached to each object that harms the player on contact.
/// This is different than a platform which can harm the player if they get stuck on it, but not on contact
/// </summary>

public class BasicEnemy : MonoBehaviour
{
    public float forwardSpeed = .05f;


    [Header("Potential Lanes")]
    // Use in inspector; these will determine where the user can spawn our given prefab
    [SerializeField] bool isLeftSpawnable;
    [SerializeField] bool isCenterSpawnable;
    [SerializeField] bool isRightSpawnable;

    // This just makes it easier to pull the results of the above bools in code
    [System.NonSerialized] public Dictionary<int, bool> acceptableLanes = new Dictionary<int, bool>();

    // Update is called once per frame
    void Update()
    {
        MoveSelf();
    }

    /// <summary>
    /// Adds each bool "is___Spawnable" to the dictionary, but only if the bool in question is true.
    /// </summary>
    public void GetAllSpawnables()
    {
        // this is the list we use for coding
        AddIfTrue(-1, isLeftSpawnable);
        AddIfTrue(0, isCenterSpawnable);
        AddIfTrue(1, isRightSpawnable);
    }

    void MoveSelf()
    {
        transform.Translate(0, 0, -forwardSpeed);

        // Destroys self if we are past the player and out of sight
        if (transform.position.z < -80)
        {
            Debug.Log("BasicEnemy.cs - Update()");
            Destroy(gameObject);
            //gameObject.SetActive(false); eventually we should use for objectPooling
        }
    }

    /// <summary>
    /// If potentialLane == true, then we add it to the dictionary which SpawnController.cs uses to determine spawn locations
    /// </summary>
    /// <param name="laneNum">Currently, -1 is leftmost, 0 is center, and 1 is rightmost lane</param>
    void AddIfTrue(int laneNum, bool isUseableLane)
    {
        if (isUseableLane) { acceptableLanes.Add(laneNum, isUseableLane); Debug.Log("BasicEnemy.cs - AddIfTrue lane: " + laneNum); }
    }
}
