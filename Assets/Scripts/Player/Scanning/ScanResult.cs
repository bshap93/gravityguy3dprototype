using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Scanning
{
    // Represents the result of a scan
    public class ScanResult
    {
        public string objectName;
        public string objectType;
        public Dictionary<string, float> properties = new Dictionary<string, float>();

        public ScanResult(string name, string type)
        {
            objectName = name;
            objectType = type;
        }

        public void AddProperty(string key, float value)
        {
            properties[key] = value;
        }
    }

// Performs scanning operations
    public class Scanner : MonoBehaviour
    {
        public float scanRange = 10f;
        public float scanDuration = 2f;

        public ScanResult Scan(GameObject target)
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= scanRange)
            {
                // Simulate scan duration
                StartCoroutine(SimulateScanDuration(scanDuration));

                // Create scan result
                ScanResult result = new ScanResult(target.name, target.tag);

                // Add some example properties
                result.AddProperty("Size", Random.Range(1f, 10f));
                result.AddProperty("Density", Random.Range(0.5f, 5f));

                return result;
            }
            else
            {
                Debug.Log("Target out of range");
                return null;
            }
        }

        private IEnumerator SimulateScanDuration(float duration)
        {
            Debug.Log("Scanning...");
            yield return new WaitForSeconds(duration);
            Debug.Log("Scan complete!");
        }
    }

// Stores and manages collected data
    public class DataBank : MonoBehaviour
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
