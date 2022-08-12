using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : DamagerBehavior
{
    protected GameObject player;
    protected Vector3 playerVector; // vector from enemy to player
    public float health;

    public delegate void OnDamaged();
    public event OnDamaged AppearDamaged;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindWithTag("Player");
        // find vector2 from enemy to player
        playerVector = player.transform.position - transform.position;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        playerVector = player.transform.position - transform.position; // updated here so it can be used anywhere in child classes
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            AttackBehavior attackBehavior = collision.GetComponent<AttackBehavior>();
            if (attackBehavior.senderID == "Player")
            {
                health -= attackBehavior.damage;
                AppearDamaged();
                if (health <= 0)
                {
                    // play destruction animation
                    Destroy(gameObject);
                }
            }
        }
    }

    protected void LookAtPlayer(float offset)
    {
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, playerVector);
        rotation.eulerAngles = new Vector3(0, 0, rotation.eulerAngles.z + 90 + offset);
    }
    protected void RotateToPlayer()
    {

    }
    protected void LookAtTarget()
    {

    }
    protected void RotateToTarget()
    {

    }
}
