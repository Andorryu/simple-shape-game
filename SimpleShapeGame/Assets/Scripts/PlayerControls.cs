using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerControls : MonoBehaviour
{
    public GameObject bullet;
    public GameObject gun;
    private GameObject bulletCollector;
    private PlayerInputActions playerInputActions;
    private Rigidbody2D rb;
    public float moveForce;
    public float moveSpeed;
    private Vector2 lastMousePos;
    private bool holdingShootButton;
    private float shootTimer;
    public float shootDelay;

    // Start is called before the first frame update
    void Awake()
    {
        holdingShootButton = false;
        bulletCollector = GameObject.Find("BulletCollector");
        rb = GetComponent<Rigidbody2D>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        lastMousePos = Vector2.zero;
        playerInputActions.Player.Shoot.performed += StartShooting;
        playerInputActions.Player.Shoot.canceled += StopShooting;
        shootTimer = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Aim();
        Shooting();
    }

    void Aim()
    {
        // detecting mouse
        Vector2 mousePos;
        if (Camera.main != null)
            mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        else
            mousePos = lastMousePos; // when mouse position is not found, set it to what it was last frame

        lastMousePos = mousePos;

        // setting angle of player
        Vector2 distance = mousePos - (Vector2)transform.position; // get position vector from player to mouse
        float angle = (Mathf.Atan2(distance.y, distance.x) * 180 / Mathf.PI); // get desired angle
        Quaternion rotation = transform.rotation; // create new rotation quaternion
        rotation.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, angle); // set rotation's euler angles
        transform.rotation = rotation; // put it back into transform

    }

    void Move()
    {
        // Move
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        rb.AddForce(inputVector * moveForce);

        if (rb.velocity.magnitude > moveSpeed)
        {
            rb.velocity = rb.velocity.normalized * moveSpeed;
        }
    }

    void Shooting()
    {
        if (shootTimer == 0)
        {
            if (holdingShootButton)
            {
                GameObject bulletClone = Instantiate(bullet, gun.transform.position, transform.rotation, bulletCollector.transform); // create bullet clone
                bulletClone.GetComponent<BulletBehavior>().shooter = gameObject; // set the shooter object in bulletClone's bulletBehavior to this one

                shootTimer = shootDelay;
            }
        }
        else
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer < 0) shootTimer = 0;
        }
    }

    void StartShooting(InputAction.CallbackContext phase)
    {
        holdingShootButton = true;
    }

    void StopShooting(InputAction.CallbackContext phase)
    {
        holdingShootButton = false;
    }
}
