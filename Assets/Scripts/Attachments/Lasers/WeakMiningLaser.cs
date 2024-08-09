using UnityEngine;

public class WeakMiningLaser : Attachment
{
    public ParticleSystem laserBeam;
    public override void Activate()
    {
        // Create a new instance of the laser beam
        laserBeam.Play();
    }
    public override void Deactivate()
    {
        // Stop the laser beam
        laserBeam.Stop();
    }
}
