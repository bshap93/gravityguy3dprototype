using UnityEngine;

public abstract class ShieldAttachment : Attachment
{
    public float shieldStrength;
    public float rechargeRate;

    protected float currentShieldStrength;

    public override void Activate()
    {
        if (CanActivate())
        {
            isActive = true;
            lastActivationTime = Time.time;
            ActivateShield();
        }
    }

    public override void Deactivate()
    {
        isActive = false;
        DeactivateShield();
    }

    protected abstract void ActivateShield();
    protected abstract void DeactivateShield();

    protected virtual void RechargeShield()
    {
        if (isActive && currentShieldStrength < shieldStrength)
        {
            currentShieldStrength = Mathf.Min(currentShieldStrength + rechargeRate * Time.deltaTime, shieldStrength);
        }
    }
}
