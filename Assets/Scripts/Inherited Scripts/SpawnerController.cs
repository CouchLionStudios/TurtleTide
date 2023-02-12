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

    [SerializeField] List<GameObject> potentialSpawnables;
    [SerializeField] List<GameObject> activeSpawnables;

    public GameObject myPool;

    public float forwardSpeed = .1f;

    public GameObject SpawnRandom()
    { 
        GameObject newSpawn = Instantiate(potentialSpawnables[UnityEngine.Random.Range(0, potentialSpawnables.Count)], transform);
        activeSpawnables.Add(newSpawn);

        return newSpawn;
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
