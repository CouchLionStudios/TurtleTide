using UnityEngine;

/// <summary>
/// LevelController.cs is for actual level changes, such as managing new ground, enmvironment changes, etc.
/// </summary>
public class LevelController : MonoBehaviour
{
    [SerializeField] SpawnerController spawner;

    [Space]
    public float MyCooldown;

    int groundSpace = 3;
    int currentGroundHeight;

    // Start is called before the first frame update
    void Start()
    {
        currentGroundHeight = 0;
        InvokeRepeating("ChangeHeight", 0, MyCooldown);
    }

    /// <summary>
    /// Currently moves the ground a set distance (groundSpace) up or down.  There is no limit to how far up or down
    /// </summary>
    void ChangeHeight()
    {
        // TODO:   - this feature needs to move a ground Spawner up or down, rather than the existing ground plane
        //         - From there, we can have angled planes connect up the new to the old ground.
        //         - Add in weight to the random num, so the player generally trends down (towards the beach)

        currentGroundHeight = UnityEngine.Random.Range(-1, 2);
        Debug.Log("LevelController.cs - ChangeHeight() currentGroundHeight: " + currentGroundHeight);

        int newYHeight = groundSpace * currentGroundHeight;
        transform.position = new Vector3(transform.position.x, transform.position.y + newYHeight, transform.position.z);
        spawner.transform.position = new Vector3(spawner.transform.position.x, spawner.transform.position.y + newYHeight, spawner.transform.position.z);
    }
}
