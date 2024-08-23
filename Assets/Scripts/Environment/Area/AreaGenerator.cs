using UnityEngine;

namespace Environment.Area
{
    public class AreaGenerator : MonoBehaviour
    {
        public GameObject[] asteroidPrefabs;
        public GameObject[] stationPrefabs;
        public float generationRadius = 5000f;

        public void GenerateNewArea(Vector3 centerPoint)
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
    }
}
