using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGunnerBehavior : TurretBehavior
{
    public GameObject tread;
    private float treadRotation;
    public float moveSpeed;
    public float moveTime;
    private float moveTimer;

    new void Awake()
    {
        treadRotation = Random.Range(0, 360);
        moveTimer = moveTime;
        
    }

    new void FixedUpdate()
    {

    }
}
