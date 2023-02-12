using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BasicGround.cs is attached to each object that is used as a ground tile.
/// This is different than a platform or obstacle
/// </summary>
public class BasicGround : MovingObject
{
    public GameObject endPoint; // where this tile ends, we move the spawner here every time
}
