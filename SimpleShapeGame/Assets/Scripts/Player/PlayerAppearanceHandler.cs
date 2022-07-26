using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAppearanceHandler : MonoBehaviour
{
    private PlayerController playerController;
    public GameObject damagedBase;
    public GameObject damagedFront;
    public float damagedTime;
    private float damagedTimer;
    private bool damaged;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        damagedTimer = damagedTime;
    }

    private void OnEnable()
    {
        playerController.AppearDamaged += StartDamaged;
    }

    private void OnDisable()
    {
        playerController.AppearDamaged -= StartDamaged;
    }

    private void Update()
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
        damagedBase.SetActive(true);
        damagedFront.SetActive(true);
    }

    void StopDamaged()
    {
        damagedTimer = damagedTime;
        damaged = false;
        damagedBase.SetActive(false);
        damagedFront.SetActive(false);
    }

    // PLAYER COLORS
    // Default:
    // Base: #05146A --- Front: #062E89
    // Damaged:
    // Base: #451449 --- Front: #562E55
}
