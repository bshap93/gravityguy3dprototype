using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;

public class CameraFader : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    private Vignette vignette;

    void Start()
    {
        // Ensure the PostProcessVolume has a Vignette effect
        if (postProcessVolume.profile.TryGetSettings(out Vignette vig))
        {
            vignette = vig;
        }
        else
        {
            vignette = postProcessVolume.profile.AddSettings<Vignette>();
        }
    }

    public IEnumerator FadeToBlack(float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            vignette.intensity.value = Mathf.Lerp(0f, 1f, t);
            vignette.smoothness.value = Mathf.Lerp(0.01f, 1f, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        vignette.intensity.value = 1f;
        vignette.smoothness.value = 1f;
    }

    public IEnumerator FadeFromBlack(float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            vignette.intensity.value = Mathf.Lerp(1f, 0f, t);
            vignette.smoothness.value = Mathf.Lerp(1f, 0.01f, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        vignette.intensity.value = 0f;
        vignette.smoothness.value = 0.01f;
    }
}
