using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] List<GameObject> potentialObstacles;
    public float MyCooldown;
    float laneDistance = 3;  // distance between 2 lanes
    public float forwardSpeed = .1f;
    // Debug.Log((int) Time.timeSinceLevelLoad); use this + time.timescale maybe?  both at once for juice
    int currentLane;
    int pastLane = -1;
    //int pastPastLane = 1; for if we have more than 3 lanes

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0, MyCooldown);
    }

    void Spawn()
    { 
        // TODO: this is all bad and should be more sensible

        GameObject newFella = Instantiate(potentialObstacles[UnityEngine.Random.Range(0, potentialObstacles.Count)], transform);
        BasicEnemy enemyScript = newFella.GetComponent<BasicEnemy>();
        enemyScript.GetAllSpawnables();

        // Get a random viable lane, but dont repeat it more than twice
        //do
        //{
            currentLane = UnityEngine.Random.Range(0, enemyScript.acceptableLanes.Count);
            currentLane = enemyScript.acceptableLanes.ElementAt(currentLane).Key;
        //}
        //while (currentLane == pastLane); //  && currentLane == pastPastLane

        newFella.transform.position = new Vector3(transform.position.x + (currentLane * laneDistance), transform.position.y, transform.position.z);
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
    // Testing only
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        Spawn();
    //    }
    //}
}
