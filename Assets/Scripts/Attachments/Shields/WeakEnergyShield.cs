using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakEnergyShield : ShieldAttachment
{
    protected override void ActivateShield()
    {
        currentShieldStrength = shieldStrength;
        Debug.Log($"Energy shield activated with {shieldStrength} strength");
    }

    protected override void DeactivateShield()
    {
        Debug.Log("Energy shield deactivated");
    }

    private void Update()
    {
        if (isActive)
        {
            RechargeShield();
        }
    }
}
