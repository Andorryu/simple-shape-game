using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerControls : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private Rigidbody2D rb;
    public float moveForce;
    public float moveSpeed;
    private Vector2 lastMousePos;
    public GameObject shot;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        lastMousePos = Vector2.zero;
        playerInputActions.Player.Shoot.performed += Shoot;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Aim();
    }

    void Aim()
    {
        // detecting mouse
        Vector2 mousePos;
        if (Camera.current != null)
            mousePos = Camera.current.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        else
            mousePos = lastMousePos;
        lastMousePos.x = mousePos.x;

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

    void Shoot(InputAction.CallbackContext phase)
    {
        GameObject.Instantiate(shot, transform.parent);
    }
}
