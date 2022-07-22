using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1AI : MonoBehaviour
{
    private GameObject player;
    public GameObject bullet;
    private GameObject bulletCollector;
    private Rigidbody2D rb;
    public float rotationRange; // rotates to look at player with this much variability
    public float rotatePower;
    public float rotateSpeed;
    private int rotateDirection; // used when enemy is in rotation range

    public float shootDelay;
    private float shootTimer;

    // Start is called before the first frame update
    void Start()
    {
        // initialize variables
        rb = GetComponent<Rigidbody2D>();
        rotateDirection = 1;
        player = GameObject.Find("Player");
        bulletCollector = GameObject.Find("BulletCollector");
        shootTimer = shootDelay;
        InitializeTransform();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyRotationDirection();
        ApplyPower();
        shootTimer -= Time.deltaTime;
        if (shootTimer < 0)
        {
            Instantiate(bullet, bulletCollector.transform);
            shootTimer = shootDelay;
        }
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

    void ApplyRotationDirection()
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

        
        Debug.Log("playerAngle: " + playerAngle);
        Debug.Log("rotation: " + rotation);
    }

    void ApplyPower()
    {
        // if rotation is in range
        rb.AddTorque(rotatePower * rotateDirection);

        // set a maximum rotate speed
        if (rb.angularVelocity > rotateSpeed)
        {
            rb.angularVelocity = rotateSpeed;
        }
        else if (rb.angularVelocity < -rotateSpeed)
        {
            rb.angularVelocity = -rotateSpeed;
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
