using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// SpawnerController.cs is for , such as managing new ground, enmvironment changes, etc.
/// </summary>
public class SpawnerController : MonoBehaviour
{
    // TODO: As we are now spawning ground tiles soon, this should be less focused on enemies only.
    // - We need to accept enemy, obstacles, and ground spawnables.  Perhaps use inheritance to reduce clutter?
    // - Save laneDistance for this and playerController to a ScriptableObject.

    [SerializeField] List<GameObject> potentialObstacles;
    public float MyCooldown; // set in Inspector
    public float forwardSpeed = .1f;

    float laneDistance = 4;  // distance between 2 lanes

    int currentLane;
    int pastLane = -1;
    //int pastPastLane = 1; for if we have more than 3 lanes

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0, MyCooldown);
    }

    // Testing only
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        Spawn();
    //    }
    //}

    void Spawn()
    { 
        // TODO:
        // - Re-implement some kind of non-repeat system for variety
        // - Possibly add way to lower cooldown, for logarithmically faster spawning? (increasing difficulty)

        GameObject newSpawnable = Instantiate(potentialObstacles[UnityEngine.Random.Range(0, potentialObstacles.Count)], transform);
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

        enemyScript.forwardSpeed = forwardSpeed; 

        //update our random number checkers
        //pastPastLane = pastLane;
        pastLane = currentLane;

    }

    public void TurnOff()
    {
        CancelInvoke();
        foreach(var script in this.transform.GetComponentsInChildren<BasicEnemy>())
        {
            script.forwardSpeed = 0f;
        }
    }
}
