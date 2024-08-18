using System.Collections.Generic;
using UnityEngine;

namespace Player.Scanning
{
    public class ScanDatabank : MonoBehaviour
    {
        private Dictionary<string, ScanResult> collectedData = new Dictionary<string, ScanResult>();

        public void AddScanResult(ScanResult result)
        {
            collectedData[result.objectName] = result;
            Debug.Log($"Data for {result.objectName} added to DataBank");
        }

        public ScanResult GetScanResult(string objectName)
        {
            if (collectedData.TryGetValue(objectName, out ScanResult result))
            {
                return result;
            }

            return null;
        }

        public void DisplayAllData()
        {
            foreach (var entry in collectedData)
            {
                Debug.Log($"Object: {entry.Key}, Type: {entry.Value.objectType}");
                foreach (var prop in entry.Value.properties)
                {
                    Debug.Log($"  {prop.Key}: {prop.Value}");
                }
            }
        }
    }
}
