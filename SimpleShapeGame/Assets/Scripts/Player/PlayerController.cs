using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject bullet;
    public GameObject gun;
    public float health;
    private GameObject bulletCollector;
    private PlayerInputActions playerInputActions;
    private Rigidbody2D rb;
    public float moveForce;
    public float moveSpeed;
    private Vector2 lastMousePos;
    private bool holdingShootButton;
    public float shootDelay;
    private float shootTimer;
    public float invincibilityDuration;
    private float invincibilityTimer;
    private bool invincible;

    public delegate void OnDamaged();
    public event OnDamaged AppearDamaged;

    // Start is called before the first frame update
    void Awake()
    {
        holdingShootButton = false;
        bulletCollector = GameObject.FindGameObjectWithTag("BulletHolder");
        rb = GetComponent<Rigidbody2D>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        lastMousePos = Vector2.zero;
        playerInputActions.Player.Shoot.performed += StartShooting;
        playerInputActions.Player.Shoot.canceled += StopShooting;
        shootTimer = 0;
        invincibilityTimer = invincibilityDuration;
        invincible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Aim();
        Shooting();

        if (invincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0)
            {
                StopInvincibility();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            AttackBehavior attackBehavior = collision.GetComponent<AttackBehavior>();
            if (attackBehavior.senderID == "Hazard")
            {
                if (!invincible)
                {
                    health -= attackBehavior.damage;
                    AppearDamaged();
                    StartInvincibility();
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Hazard"))
        {
            if (!invincible)
            {
                health -= collision.GetComponent<DamagerBehavior>().damage;
                AppearDamaged();
                StartInvincibility();
            }
        }
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

    void Shooting()
    {
        if (shootTimer == 0)
        {
            if (holdingShootButton)
            {
                GameObject bulletClone = Instantiate(bullet, gun.transform.position, transform.rotation, bulletCollector.transform); // create bullet clone
                bulletClone.GetComponent<BulletBehavior>().sender = gameObject;
                bulletClone.GetComponent<BulletBehavior>().senderID = gameObject.tag;

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

    void StartInvincibility()
    {
        invincible = true;
    }

    void StopInvincibility()
    {
        invincibilityTimer = invincibilityDuration;
        invincible = false;
    }
}
