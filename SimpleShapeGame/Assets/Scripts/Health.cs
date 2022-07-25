using UnityEngine;

public class Health : MonoBehaviour
{
    // editor variables
    public int health;
    public bool makeInvincibleOnHit;
    public float invincibilityDuration;

    // internal invincibility control
    public bool invincible;
    private bool stopInvincibility; // this variable ensures that the StopInvincibility event occurs only once
    private float invincibilityTimer;

    // events
    public delegate void Invincibility();
    public event Invincibility StartInvincibility;
    public event Invincibility StopInvincibility;

    void Start()
    {
        invincibilityTimer = invincibilityDuration;
        stopInvincibility = false;
    }


    void Update()
    {
        if (invincible)
        {
            // do invincibility timer
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0)
            {
                invincible = false;
                stopInvincibility = true;
                invincibilityTimer = invincibilityDuration;
            }

        }
        else if (stopInvincibility)
        {
            StopInvincibility(); // STOP INVINCIBILITY EVENT
            stopInvincibility = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            if (collision.GetComponent<BulletBehavior>().shooter.tag != gameObject.tag) // so nothing can shoot itself
            {
                OnHit(collision);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            OnHit(collision);
        }
    }

    void OnHit(Collider2D collision)
    {
        if (!invincible)
        {
            health -= collision.GetComponent<Damager>().damage;

            if (makeInvincibleOnHit)
            {
                invincible = true;
                StartInvincibility(); // START INVINCIBILITY EVENT
            }
        }
    }
}
