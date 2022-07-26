using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HazardSpawnInfo
{
    public GameObject prefab;
    public Vector2 position;
    public float spawnTime;
    public float existTime;
}
