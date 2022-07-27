using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAppearanceHandler : MonoBehaviour
{
    private TankGunnerBehavior turretBehavior;
    public float damagedTime;
    private float damagedTimer;
    private bool damaged;
    public GameObject damagedBackground;
    public GameObject damagedCenter;
    public GameObject damagedFront;

    // Start is called before the first frame update
    void Awake()
    {
        turretBehavior = GetComponent<TankGunnerBehavior>();
        damagedTimer = damagedTime;
        damaged = false;
    }

    private void OnEnable()
    {
        turretBehavior.AppearDamaged += StartDamaged;
    }

    private void OnDisable()
    {
        turretBehavior.AppearDamaged -= StartDamaged;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (damaged)
        {
            damagedTimer -= Time.deltaTime;
            if (damagedTimer <= 0)
            {
                StopDamaged();
            }
        }
    }

    void StartDamaged()
    {
        damagedTimer = damagedTime;
        damaged = true;
        damagedBackground.SetActive(true);
        damagedCenter.SetActive(true);
        damagedFront.SetActive(true);
    }

    void StopDamaged()
    {
        damagedTimer = damagedTime;
        damaged = false;
        damagedBackground.SetActive(false);
        damagedCenter.SetActive(false);
        damagedFront.SetActive(false);
    }
}
