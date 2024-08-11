using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakReflectiveShield : ShieldAttachment
{
    public float reflectionChance = 0.3f;

    protected override void ActivateShield()
    {
        currentShieldStrength = shieldStrength;
        Debug.Log(
            $"Reflective shield activated with {shieldStrength} strength and {reflectionChance * 100}% reflection chance");
    }

    protected override void DeactivateShield()
    {
        Debug.Log("Reflective shield deactivated");
    }

    private void Update()
    {
        if (isActive)
        {
            RechargeShield();
        }
    }
}
