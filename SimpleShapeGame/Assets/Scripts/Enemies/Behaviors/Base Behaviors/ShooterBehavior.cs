using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBehavior : EnemyBehavior
{
    protected GameObject bulletHolder;
    public float shootDelay;
    protected float shootTimer;
    public GameObject bullet;

    protected void UpdateShootTimer()
    {
        // shoot timer
        shootTimer -= Time.deltaTime;
        if (shootTimer < 0)
        {
            GameObject bulletClone = Instantiate(bullet, transform.position, transform.rotation, bulletHolder.transform);
            bulletClone.GetComponent<BulletBehavior>().sender = gameObject;
            bulletClone.GetComponent<BulletBehavior>().senderID = gameObject.tag;

            shootTimer = shootDelay;
        }
    }
}
