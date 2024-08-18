using UnityEngine;
using System.Collections;
using CW.BuildAndDestroy;

public class CwThrusterController : MonoBehaviour
{
    public CwLoader spaceshipLoader;
    private ParticleSystem thrusterParticleSystem;

    void Start()
    {
        StartCoroutine(SetupThrusterWhenLoaded());
    }

    private IEnumerator SetupThrusterWhenLoaded()
    {
        // Wait until the spaceship is loaded
        while (spaceshipLoader.LoadedParts == null || spaceshipLoader.LoadedParts.Count == 0)
        {
            yield return null;
        }

        // Find the thruster particle system
        

    }

    public void ToggleThruster(bool on)
    {
        if (thrusterParticleSystem != null)
        {
            if (on)
                thrusterParticleSystem.Play();
            else
                thrusterParticleSystem.Stop();
        }
        else
        {
            Debug.LogWarning("Thruster ParticleSystem not set up. Make sure the spaceship is loaded.");
        }
    }
}
