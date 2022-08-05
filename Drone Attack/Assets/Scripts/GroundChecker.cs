using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private int ColCount = 0;

    private float DisableTimer;

    private void OnEnable()
    {
        ColCount = 0;
    }

    public bool State()
    {
        if (DisableTimer > 0)
            return false;
        return ColCount > 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ColCount++;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        ColCount--;
    }

    void Update()
    {
        DisableTimer -= Time.deltaTime;
    }

    public void Disable(float duration)
    {
        DisableTimer = duration;
    }
}
