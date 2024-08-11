using UnityEngine;

public abstract class LaserAttachment : Attachment
{
    public float damage;
    public float range;

    public override void Activate()
    {
        if (CanActivate())
        {
            isActive = true;
            lastActivationTime = Time.time;
            FireLaser();
        }
    }

    public override void Deactivate()
    {
        isActive = false;
    }

    protected abstract void FireLaser();
}
