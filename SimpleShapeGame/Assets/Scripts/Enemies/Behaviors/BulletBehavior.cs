using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : AttackBehavior
{
    public float speed;

    protected override void Awake()
    {
        base.Awake();
        // reference rigidbody component
        rb = GetComponent<Rigidbody2D>();


        // apply velocity

        //rb.velocity += (Vector2)shooter.GetComponent<Rigidbody2D>().velocity; // *** UNCOMMENT THIS LINE IF THE BULLET SHOULD HAVE THE VELOCITY THAT ITS SHOOTER HAS AT THE MOMENT IT IS SHOT ***
        rb.velocity += (Vector2)(speed * transform.right);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != senderID && !collision.CompareTag("Attack"))
        {
            // play destruction animation?
            Destroy(gameObject);
        }

    }

}
