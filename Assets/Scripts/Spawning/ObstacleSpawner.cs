using System.Linq;
using UnityEngine;

/// <summary>
/// ObstacleSpawner.cs is attached to the one Spawner.  There should not be multiple of this script in the scene.
/// It spawns an obstacle, then waits a bit before repeating
/// </summary>
public class ObstacleSpawner : SpawnerController
{
    public float MyCooldown; // set in Inspector

    float laneDistance = 4;  // distance between 2 lanes TODO: set this to ScriptableObject

    int currentLane;
    int pastLane = -1;

    //int pastPastLane = 1; for if we have more than 3 lanes

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, MyCooldown);
    }

    void SpawnEnemy()
    {
        GameObject newSpawnable = base.SpawnRandom();
        BasicEnemy enemyScript = newSpawnable.GetComponent<BasicEnemy>();

        enemyScript.GetAllSpawnables();

        // Get a random viable lane, but dont repeat it more than twice
        //do
        //{
        currentLane = UnityEngine.Random.Range(0, enemyScript.acceptableLanes.Count);
        currentLane = enemyScript.acceptableLanes.ElementAt(currentLane).Key;
        //}
        //while (currentLane == pastLane); //  && currentLane == pastPastLane

        // set up our variables and send it off
        newSpawnable.transform.position = new Vector3(transform.position.x + (currentLane * laneDistance), transform.position.y, transform.position.z);

        enemyScript.forwardSpeed = base.forwardSpeed;

        //update our random number checkers
        //pastPastLane = pastLane;
        pastLane = currentLane;
    }

}
