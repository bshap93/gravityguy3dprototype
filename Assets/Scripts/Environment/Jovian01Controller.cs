using UnityEngine;
using UnityEngine.Serialization;

namespace Environment
{
    public class Jovian01Controller : MonoBehaviour
    {
        private AudioSource _audioSource;
        [FormerlySerializedAs("EventManager")] public EventManager eventManager;
        private string jovianName = "Echo Prime";

        // Start is called before the first frame update
        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        void OnTriggerEnter(Collider other)
        {
            _audioSource.Play();
            eventManager.deathEvent.Invoke(jovianName + " has been destroyed");
        }
    }
}
