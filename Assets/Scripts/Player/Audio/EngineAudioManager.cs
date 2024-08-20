using UnityEngine;

namespace Player.Audio
{
    public class EngineAudioManager : MonoBehaviour
    {
        [Header("Audio Sources")] [SerializeField]
        public AudioSource mainEngineAudio;
        [SerializeField] public AudioSource idleEngineAudio;
        [SerializeField] public AudioSource afterburnerAudio;

        [Header("Audio Clips")] [SerializeField]
        public AudioClip engineStartClip;
        [SerializeField] public AudioClip engineShutdownClip;

        [Header("Engine Sound Parameters")] [SerializeField]
        private float minEnginePitch = 0.5f;
        [SerializeField] private float maxEnginePitch = 1.5f;
        [SerializeField] private float enginePitchFactor = 0.1f;

        private float currentSpeed;
        private float normalizedSpeed;

        private bool isEngineRunning;
        private bool isAfterburnerActive;

        private void Start()
        {
            SetupEngineSounds();
        }

        private void SetupEngineSounds()
        {
            mainEngineAudio.loop = true;
            idleEngineAudio.loop = true;
            afterburnerAudio.loop = true;

            idleEngineAudio.Play();
        }


        public void UpdateEngineSounds(float speed, float maxSpeed, bool isThrusting, bool hasActiveInput, bool hasFuel)
        {
            currentSpeed = speed;
            normalizedSpeed = Mathf.Clamp01(currentSpeed / maxSpeed);

            UpdateMainEngine(isThrusting && hasFuel);
            UpdateIdleEngine();
            UpdateAfterburner(hasActiveInput && hasFuel);
        }

        private void UpdateMainEngine(bool shouldBeRunning)
        {
            if (shouldBeRunning && !isEngineRunning)
            {
                StartEngine();
            }
            else if (!shouldBeRunning && isEngineRunning)
            {
                StopEngine();
            }

            if (isEngineRunning)
            {
                float pitch = Mathf.Lerp(minEnginePitch, maxEnginePitch, normalizedSpeed);
                mainEngineAudio.pitch = pitch;
                mainEngineAudio.volume = Mathf.Lerp(0.5f, 1f, normalizedSpeed);
            }
        }

        private void StartEngine()
        {
            mainEngineAudio.PlayOneShot(engineStartClip);
            mainEngineAudio.Play();
            isEngineRunning = true;
        }

        private void StopEngine()
        {
            mainEngineAudio.Stop();
            mainEngineAudio.PlayOneShot(engineShutdownClip);
            isEngineRunning = false;
        }

        private void UpdateIdleEngine()
        {
            idleEngineAudio.volume = Mathf.Lerp(0.8f, 0.2f, normalizedSpeed);
        }

        private void UpdateAfterburner(bool shouldBeActive)
        {
            if (shouldBeActive && !isAfterburnerActive)
            {
                afterburnerAudio.Play();
                isAfterburnerActive = true;
            }

            if (isAfterburnerActive)
            {
                afterburnerAudio.volume = shouldBeActive
                    ? Mathf.Lerp(0f, 1f, normalizedSpeed)
                    : Mathf.Lerp(afterburnerAudio.volume, 0f, Time.deltaTime * 2f);

                if (afterburnerAudio.volume < 0.01f)
                {
                    afterburnerAudio.Stop();
                    isAfterburnerActive = false;
                }
            }
        }
    }
}
