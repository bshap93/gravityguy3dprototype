using UnityEngine;
using UnityEngine.UI;

namespace Player.Resources
{
    public class FuelSystem : MonoBehaviour
    {
        [SerializeField] private float maxFuel = 100f;
        [SerializeField] private float currentFuel;
        [SerializeField] private float fuelConsumptionRate = 1f; // Fuel consumed per second

        [SerializeField] private Image fuelRemainingImage;
        [SerializeField] private Image fuelCapacityImage;

        private void Start()
        {
            UpdateFuelUI();
        }

        private void Update()
        {
            ConsumeFuel(fuelConsumptionRate * Time.deltaTime);
            UpdateFuelUI();
        }

        public void ConsumeFuel(float amount)
        {
            currentFuel = Mathf.Max(0, currentFuel - amount);
        }

        public void RefuelShip(float amount)
        {
            currentFuel = Mathf.Min(maxFuel, currentFuel + amount);
        }

        private void UpdateFuelUI()
        {
            float fuelPercentage = currentFuel / maxFuel;
            fuelRemainingImage.fillAmount = fuelPercentage;
        }

        public bool HasFuel()
        {
            return currentFuel > 0;
        }

        public float GetCurrentFuel()
        {
            return currentFuel;
        }

        public float GetMaxFuel()
        {
            return maxFuel;
        }
    }
}
