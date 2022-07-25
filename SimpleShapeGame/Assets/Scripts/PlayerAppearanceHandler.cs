using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAppearanceHandler : MonoBehaviour
{
    public GameObject _base;
    public GameObject _front;
    private SpriteRenderer _baseSP;
    private SpriteRenderer _frontSP;

    private Health health;

    // PLAYER COLORS
    // Default:
    // Base: #05146A --- Front: #062E89
    // Invincible:
    // Base: #451449 --- Front: #562E55

    public Color _defaultBaseColor;
    public Color _invincibleBaseColor;
    public Color _defaultFrontColor;
    public Color _invincibleFrontColor;

    // Start is called before the first frame update
    void Awake()
    {
        // references
        _baseSP = _base.GetComponent<SpriteRenderer>();
        _frontSP = _front.GetComponent<SpriteRenderer>();
        health = GetComponent<Health>();

        // events
        health.StartInvincibility += StartInvincibility;
        health.StopInvincibility += StopInvincibility;
    }

    void OnDisable()
    {
        health.StartInvincibility -= StartInvincibility;
        health.StopInvincibility -= StopInvincibility;
    }

    void StartInvincibility()
    {
        _baseSP.color = _invincibleBaseColor;
        _frontSP.color = _invincibleFrontColor;
    }

    void StopInvincibility()
    {
        _baseSP.color = _defaultBaseColor;
        _frontSP.color = _defaultFrontColor;
    }
}
