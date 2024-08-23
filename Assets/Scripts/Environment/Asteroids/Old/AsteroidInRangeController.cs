using UnityEngine;

namespace Environment.Asteroids.Old
{
    public class AsteroidInRangeController : MonoBehaviour
    {
        public GameObject asteroid;
        public readonly GameObjectEvent TargetInRange = new();
        public readonly GameObjectEvent TargetOutOfRange = new();
        public bool isInView;
        public GameObject mainCamera;

        // Start is called before the first frame update
        void Start()

        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            asteroid = gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            CheckIfThisObjectIsInView();
        }

        void CheckIfThisObjectIsInView()
        {
            Vector3 screenPoint = mainCamera.GetComponent<UnityEngine.Camera>()
                .WorldToViewportPoint(asteroid.transform.position);

            isInView = screenPoint is { z: > 0, x: > 0 and < 1, y: > 0 and < 1 };
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                TargetInRange.Invoke(asteroid);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                TargetOutOfRange.Invoke(asteroid);
            }
        }
    }
}
