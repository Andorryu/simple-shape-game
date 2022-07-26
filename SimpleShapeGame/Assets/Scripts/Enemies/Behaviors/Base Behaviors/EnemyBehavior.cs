using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : DamagerBehavior
{
    public float health;

    public delegate void OnDamaged();
    public event OnDamaged AppearDamaged;

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
}
