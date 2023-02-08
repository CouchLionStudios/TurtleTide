using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public float forwardSpeed = .05f;

    [SerializeField] bool isLeftSpawnable;
    [SerializeField] bool isCenterSpawnable;
    [SerializeField] bool isRightSpawnable;
    [System.NonSerialized] public Dictionary<int, bool> acceptableLanes = new Dictionary<int, bool>();

    public void GetAllSpawnables()
    {
        // this is the list we use for coding
        AddIfTrue(-1, isLeftSpawnable);
        AddIfTrue(0, isCenterSpawnable);
        AddIfTrue(1, isRightSpawnable);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, -forwardSpeed);
        if(transform.position.z < -80)
        {
            Debug.Log("Pew");
            Destroy(gameObject);
            //gameObject.SetActive(false); eventually use for objectPooling
        }
    }

    void AddIfTrue(int laneNum, bool potentialLane)
    {

        if (potentialLane) { acceptableLanes.Add(laneNum, potentialLane); Debug.Log("Adding lane: " + laneNum); }
    }
}
