using UnityEngine;

namespace Utilities
{
    public class DialogueAudioManager : MonoBehaviour
    {
        static DialogueAudioManager Instance { get; set; }

        private AudioSource _audioSource;

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlayAudio(string audioName)
        {
            // Play audio from Resources/Sounds folder

            AudioClip audioClip = Resources.Load<AudioClip>("Sounds/" + audioName);
            if (audioClip != null)
            {
                _audioSource.PlayOneShot(audioClip);
            }
            else
            {
                Debug.LogError("Audio clip not found in Resources/Sounds folder: " + audioName);
            }
        }
    }
}
