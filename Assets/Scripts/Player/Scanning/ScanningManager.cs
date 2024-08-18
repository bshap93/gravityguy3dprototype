using UnityEngine;

namespace Player.Scanning
{
    public class ScanningManager : MonoBehaviour
    {
        public Scanner scanner;
        public DataBank dataBank;

        private void Start()
        {
            scanner = GetComponent<Scanner>();
            dataBank = GetComponent<DataBank>();
        }

        public void ScanObject(GameObject target)
        {
            ScanResult result = scanner.Scan(target);
            if (result != null)
            {
                dataBank.AddScanResult(result);
            }
        }

        public void DisplayCollectedData()
        {
            dataBank.DisplayAllData();
        }
    }
}
