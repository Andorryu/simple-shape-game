using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  
 * This could theoretically inherit from a class called "TimedEntityBehavior", which would hold the existTimer
 * functions. "TimedEntityBehavior" would then inherit from a class called EntityBehavior which PlayerController
 * might inherit from (in that case, maybe PlayerController would be renamed to "PlayerBehavior"?). This isn't
 * the case because there is currently no advantage to making this happen.
*/

public class DamagerBehavior : MonoBehaviour
{
    public float damage;
    protected Rigidbody2D rb;
    public float existTime;
    public float existTimer;

    protected void InitializeExistTimer()
    {
        existTimer = existTime;
    }

    protected void UpdateExistTimer() // called in every final child's Update or FixedUpdate function
    {
        // exist timer
        existTimer -= Time.deltaTime;
        if (existTimer <= 0)
        {
            // play exit animation?
            Destroy(gameObject);
        }
    }
}