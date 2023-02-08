using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] SpawnerController spawner;

    public float MyCooldown;

    int groundSpace = 3;
    int currentGroundHeight;

    // Start is called before the first frame update
    void Start()
    {
        currentGroundHeight = 0;
        InvokeRepeating("ChangeHeight", 0, MyCooldown);
    }

    void ChangeHeight()
    {
        currentGroundHeight = UnityEngine.Random.Range(-1, 2);
        Debug.Log("current   " + currentGroundHeight);
        int newYHeight = groundSpace * currentGroundHeight;
        transform.position = new Vector3(transform.position.x, transform.position.y + newYHeight, transform.position.z);
        spawner.transform.position = new Vector3(spawner.transform.position.x, spawner.transform.position.y + newYHeight, spawner.transform.position.z);
    }
}
