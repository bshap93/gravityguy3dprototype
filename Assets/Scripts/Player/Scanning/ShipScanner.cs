using UnityEngine;
using System.Collections;
using SpaceGraphicsToolkit;
using SpaceGraphicsToolkit.Ring;
using SpaceGraphicsToolkit.Starfield;

namespace Player.Scanning
{
    public class ShipScanner : MonoBehaviour
    {
        public float scanRange = 10f;
        public float scanDuration = 2f;
        public LayerMask scanLayers;

        private SgtRing scanRing;
        private SgtStarfieldBox scanParticles;
        private bool isScanning = false;

        void Start()
        {
            InitializeScanRing();
            InitializeScanParticles();
        }

        void InitializeScanRing()
        {
            GameObject ringObject = new GameObject("ScanRing");
            ringObject.transform.SetParent(transform);
            scanRing = ringObject.AddComponent<SgtRing>();

            scanRing.RadiusInner = 0f;
            scanRing.RadiusOuter = 0.1f;
            scanRing.Brightness = 2f;
            scanRing.Color = Color.cyan;

            Material ringMaterial = new Material(Shader.Find("Space Graphics Toolkit/Ring"));
            ringMaterial.SetColor("_Color", Color.cyan);
            scanRing.SourceMaterial = ringMaterial;

            scanRing.gameObject.SetActive(false);
        }

        void InitializeScanParticles()
        {
            GameObject particlesObject = new GameObject("ScanParticles");
            particlesObject.transform.SetParent(transform);
            scanParticles = particlesObject.AddComponent<SgtStarfieldBox>();

            scanParticles.Color = Color.cyan;
            scanParticles.Brightness = 1f;
            scanParticles.StarCount = 1000;
            scanParticles.StarRadiusMin = 0.01f;
            scanParticles.StarRadiusMax = 0.05f;
            scanParticles.Extents = Vector3.one * 0.1f; // Start with a small box

            Material particlesMaterial = new Material(Shader.Find("Space Graphics Toolkit/Starfield"));
            particlesMaterial.SetColor("_Color", Color.cyan);
            scanParticles.SourceMaterial = particlesMaterial;

            scanParticles.gameObject.SetActive(false);
        }

        public IEnumerator ScanCoroutine(GameObject target)
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= scanRange && !isScanning)
            {
                isScanning = true;
                scanRing.gameObject.SetActive(true);
                scanParticles.gameObject.SetActive(true);

                float elapsedTime = 0f;
                while (elapsedTime < scanDuration)
                {
                    float progress = elapsedTime / scanDuration;
                    scanRing.RadiusOuter = scanRange * progress;
                    scanParticles.Extents = Vector3.one * (scanRange * progress);

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                scanRing.gameObject.SetActive(false);
                scanParticles.gameObject.SetActive(false);

                ScanResult result = new ScanResult(target.name, target.tag);
                result.AddProperty("Size", Random.Range(1f, 10f));
                result.AddProperty("Density", Random.Range(0.5f, 5f));

                ProcessScanResult(result);

                isScanning = false;
            }
            else
            {
                Debug.Log("Target out of range or scan in progress");
            }
        }

        private void ProcessScanResult(ScanResult result)
        {
            Debug.Log($"Processed scan result for {result.objectName}");
            // Here you can send the result to your DataBank or trigger other events
        }

        public void StartScan(GameObject target)
        {
            StartCoroutine(ScanCoroutine(target));
        }
    }
}
