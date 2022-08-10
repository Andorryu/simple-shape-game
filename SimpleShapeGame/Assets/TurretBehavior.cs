using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : ShooterBehavior
{
    protected GameObject player;
    public GameObject rotatingObject;
    public float rotateSpeed;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        // initialize variables
        rb = rotatingObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        bulletHolder = GameObject.FindGameObjectWithTag("BulletHolder");

        // other init stuff
        shootTimer = shootDelay;
        InitializeTransform();
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        RotateToTarget();
    }
    void InitializeTransform()
    {
        // find vector2 from enemy to player
        Vector2 distance = player.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(distance.y, distance.x) * 180 / Mathf.PI;

        Quaternion rotation = rb.transform.rotation;
        rb.transform.rotation = rotation;
    }
    void RotateToTarget()
    {
        Vector3 distance = player.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, distance);
        rotation = Quaternion.Euler(0, 0, rotation.eulerAngles.z + 90);

        rotatingObject.transform.rotation = Quaternion.RotateTowards(rotatingObject.transform.rotation, rotation, rotateSpeed * Time.deltaTime);
    }
}
