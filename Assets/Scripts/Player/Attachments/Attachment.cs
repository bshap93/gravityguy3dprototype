using UnityEngine;

public abstract class Attachment : MonoBehaviour
{
    public string attachmentName;
    public string description;
    public int powerCost;
    public float cooldownTime;

    protected bool isActive = false;
    protected float lastActivationTime = -Mathf.Infinity;

    public abstract void Activate();
    public abstract void Deactivate();

    protected bool CanActivate()
    {
        return !isActive && Time.time - lastActivationTime >= cooldownTime;
    }
}
