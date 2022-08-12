using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAppearancehandler : MonoBehaviour
{
    public GameObject[] damagedParts;
    private PlayerController controller;
    public float damagedTime;
    private float damagedTimer;
    private bool damaged;

    void Awake()
    {
        controller = GetComponent<PlayerController>();
        damagedTimer = damagedTime;
        damaged = false;
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        controller.AppearDamaged += StartDamaged;
    }

    void OnDisable()
    {
        controller.AppearDamaged -= StartDamaged;
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
