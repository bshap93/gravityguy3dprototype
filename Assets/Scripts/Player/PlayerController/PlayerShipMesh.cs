using UnityEngine;

namespace Player.PlayerController
{
    public class PlayerShipMesh : MonoBehaviour
    {
        private AudioSource audioSource;
        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
        }

        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Asteroid"))
            {
                // Play other sfx
            }

            if (other.gameObject.CompareTag("CapitalShipPart"))
            {
                Debug.Log("Collided with capital ship part");
            }
        }
    }
}
