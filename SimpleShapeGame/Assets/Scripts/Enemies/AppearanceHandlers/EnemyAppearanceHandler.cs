using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAppearanceHandler : MonoBehaviour
{
    public GameObject[] damagedParts;
    private EnemyBehavior behavior;
    public float damagedTime;
    private float damagedTimer;
    private bool damaged;

    void Awake()
    {
        behavior = GetComponent<EnemyBehavior>();
        damagedTimer = damagedTime;
        damaged = false;
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        behavior.AppearDamaged += StartDamaged;
    }

    void OnDisable()
    {
        behavior.AppearDamaged -= StartDamaged;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (damaged)
        {
            damagedTimer -= Time.deltaTime;
            if (damagedTimer <= 0)
            {
                damagedTimer = damagedTime;
                StopDamaged();
            }
        }
    }

    void StartDamaged()
    {
        damaged = true;
        foreach (GameObject damagedPart in damagedParts)
        {
            damagedPart.SetActive(true);
        }

    }

    void StopDamaged()
    {
        damaged = false;
        foreach (GameObject damagedPart in damagedParts)
        {
            damagedPart.SetActive(false);
        }
    }
}
