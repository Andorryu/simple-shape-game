using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGunnerBehavior : TurretBehavior
{
    public GameObject tread;
    public float treadRotation;
    public float moveSpeed;
    public float moveTime;
    private float moveTimer;
    private int moveDirection;
    
    protected override void Awake()
    {
        base.Awake();
        moveTimer = moveTime;
        moveDirection = 1;
        SetRotation();
        SetVelocity();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        moveTimer -= Time.deltaTime;
        if (moveTimer <= 0)
        {
            moveTimer = moveTime;
            moveDirection = -moveDirection;
            SetVelocity();
        }
    }

    void SetVelocity()
    {
        Vector2 moveVector = new Vector2(Mathf.Cos(treadRotation * Mathf.PI / 180), Mathf.Sin(treadRotation * Mathf.PI / 180));
        GetComponent<Rigidbody2D>().velocity = moveVector * moveSpeed * moveDirection;
        rotatingObject.GetComponent<Rigidbody2D>().velocity = moveVector * moveSpeed * moveDirection;
    }

    void SetRotation()
    {
        Quaternion rotation = Quaternion.identity;
        rotation.eulerAngles = new Vector3(0, 0, treadRotation);
        tread.transform.rotation = rotation;
    }
}
