using UnityEngine;

/// <summary>
/// GroundSpawner.cs is attached to the one Spawner.  There should not be multiple of this script in the scene.
/// It spawns a tile, then moves to the end of that tile, and repeats
/// </summary>
public class GroundSpawner : SpawnerController
{

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnGround", 0, 1.0f);
    }

    void SpawnGround()
    {
        GameObject newSpawnable = base.SpawnRandom();
        BasicGround groundScript = newSpawnable.GetComponent<BasicGround>();

        // connect our new tile to the old one, and add it to the pool
        newSpawnable.transform.position = this.transform.position;
        newSpawnable.transform.parent = base.myPool.transform;

        //  Now, set our Spawner to stick to the new tile's end point
        this.transform.parent = groundScript.endPoint.transform;
        this.transform.localPosition = Vector3.zero;

        // send it
        groundScript.forwardSpeed = base.forwardSpeed;
    }
}
