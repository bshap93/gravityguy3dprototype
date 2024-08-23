using System.Collections;
using Player.PlayerController;
using Player.PlayerController.Components;
using UnityEngine;

namespace Environment.Area
{
    public class AreaManager : MonoBehaviour
    {
        public GameObject[] asteroidPrefabs;
        public GameObject[] stationPrefabs;
        public float generationRadius = 5000f;
        public PlayerController playerController;
        public ShipMovement shipMovement;

        public void TravelToNewArea()
        {
            StartCoroutine(TransitionToNewArea());
        }

        private IEnumerator TransitionToNewArea()
        {
            // Disable player controls
            playerController.enabled = false;
            shipMovement.enabled = false;

            // Fade out
            yield return StartCoroutine(FadeEffect(1f));

            // Generate new area and move player
            var newAreaCenter = Random.insideUnitSphere * 10000f;
            GenerateNewArea(newAreaCenter);
            playerController.transform.position = newAreaCenter;
            shipMovement.ResetVelocityAfterTravel();

            // Fade in
            yield return StartCoroutine(FadeEffect(0f));

            // Re-enable player controls
            playerController.enabled = true;
            shipMovement.enabled = true;
        }

        private void GenerateNewArea(Vector3 centerPoint)
        {
            // Clear existing generated objects if necessary

            // Generate new asteroid field
            for (int i = 0; i < 50; i++)
            {
                Vector3 randomPos = centerPoint + Random.insideUnitSphere * generationRadius;
                Instantiate(asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)], randomPos, Random.rotation);
            }

            // Generate a station
            Vector3 stationPos = centerPoint + Random.insideUnitSphere * (generationRadius * 0.5f);
            Instantiate(stationPrefabs[Random.Range(0, stationPrefabs.Length)], stationPos, Random.rotation);
        }

        private IEnumerator FadeEffect(float targetAlpha)
        {
            // Implement fade effect here
            yield return null;
        }
    }
}
