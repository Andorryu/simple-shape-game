using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : ShooterBehavior
{
    private GameObject player;
    private int rotateDirection;
    public float rotateSpeed;
    public float rotationRange;

    // Start is called before the first frame update
    void Awake()
    {
        // initialize variables
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        bulletHolder = GameObject.FindGameObjectWithTag("BulletHolder");

        // other init stuff
        rotateDirection = 1;
        shootTimer = shootDelay;
        InitializeTransform();
        InitializeExistTimer();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DetermineRotationDirection();
        ApplyRotationSpeed();

        UpdateShootTimer();
        UpdateExistTimer();
    }

    void InitializeTransform()
    {
        // find vector2 from enemy to player
        Vector2 distance = player.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(distance.y, distance.x) * 180 / Mathf.PI;

        Quaternion rotation = transform.rotation;
        rotation.eulerAngles = new Vector3(0, 0, Random.Range(targetAngle - rotationRange, targetAngle + rotationRange));
        transform.rotation = rotation;
    }

    void DetermineRotationDirection()
    {
        // find vector2 from enemy to player
        Vector2 distance = player.transform.position - transform.position;
        float playerAngle = Mod(Mathf.Atan2(distance.y, distance.x) * 180 / Mathf.PI, 360); // find float angle from distance vector

        // find rotation and upper and lower bounds
        float rotation = Mod(transform.rotation.eulerAngles.z, 360);
        float upperBound = Mod(playerAngle + rotationRange, 360);
        float lowerBound = Mod(playerAngle - rotationRange, 360);

        // fix edge case (when upperbound or lowerbound wraps around the origin)
        if (upperBound < playerAngle)
        {
            upperBound += 360;
            if (rotation < 180) rotation += 360;
        }
        if (lowerBound > playerAngle)
        {
            lowerBound -= 360;
            if (rotation > 180) rotation -= 360;
        }

        // if rotation is out of range, change direction
        if (rotation > upperBound)
        {
            rotateDirection = -1;
        }
        else if (rotation < lowerBound)
        {
            rotateDirection = 1;
        }
    }

    void ApplyRotationSpeed()
    {
        // if rotation is in range
        rb.angularVelocity = rotateSpeed * rotateDirection;
    }

    float Mod(float input, float modulus)
    {
        float result = input % modulus;
        if (result < 0)
            result += modulus;
        return result;
    }
}
