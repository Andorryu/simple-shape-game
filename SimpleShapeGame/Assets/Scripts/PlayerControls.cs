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

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Move
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        rb.AddForce(inputVector * moveForce);

        if (rb.velocity.magnitude > moveSpeed)
        {
            rb.velocity = rb.velocity.normalized * moveSpeed;
        }
    }
    private void Update()
    {
        // Aim
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 worldPos = Camera.current.ScreenToWorldPoint(mousePos);
        Debug.Log(worldPos);
    }
}
