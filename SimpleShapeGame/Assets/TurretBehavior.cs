using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : ShooterBehavior
{
    private GameObject player;
    public GameObject rotatingObject;
    public float rotateSpeed;
    public float rotationRange;

    // Start is called before the first frame update
    void Awake()
    {
        // initialize variables
        rb = rotatingObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        bulletHolder = GameObject.FindGameObjectWithTag("BulletHolder");

        // other init stuff
        shootTimer = shootDelay;
        InitializeTransform();
        InitializeExistTimer();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DetermineRotationDirection();

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
        float rotation = Mod(rotatingObject.transform.rotation.eulerAngles.z, 360);
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

        Debug.Log("Rotation: " + rotation + "   Upper bound: " + upperBound + "   Lower bound: " + lowerBound);

        // if rotation is out of range, change direction
        if (!(rotation < upperBound && rotation > lowerBound))
        {
            float upperDistance, lowerDistance;

            upperDistance = Mathf.Min(Mathf.Abs(rotation - upperBound), Mathf.Abs(rotation - upperBound + 360));
            lowerDistance = Mathf.Min(Mathf.Abs(rotation - lowerBound), Mathf.Abs(rotation - lowerBound - 360));

            if (upperDistance < lowerDistance)
            {
                rb.angularVelocity = -rotateSpeed;
            }
            else
            {
                rb.angularVelocity = rotateSpeed;
            }
        }
    }
    float Mod(float input, float modulus)
    {
        float result = input % modulus;
        if (result < 0)
            result += modulus;
        return result;
    }
}
