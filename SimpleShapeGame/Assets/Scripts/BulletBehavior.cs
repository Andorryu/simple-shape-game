using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private float direction;
    public float speed;
    public float damage;

    void Start()
    {
        // find player
        if (GameObject.Find("Player") != null)
            player = GameObject.Find("Player");
        else
            Debug.LogError("Could not find a gameobject named 'Player'.");

        // reference rigidbody component
        rb = GetComponent<Rigidbody2D>();

        // set direction float to player's direction
        direction = player.transform.rotation.eulerAngles.z;

        // bullet appears on the player and points in same direction
        transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;

        Vector2 directionVector = new Vector2(Mathf.Cos(direction * Mathf.PI / 180), Mathf.Sin(direction * Mathf.PI / 180));

        // apply velocity
        rb.velocity = speed * directionVector;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

}
