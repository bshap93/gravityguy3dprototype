using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player.Resources
{
    public class FuelSystem : MonoBehaviour
    {
        [SerializeField] private float maxFuelInGrams = 1000f; // 1 kg of fuel
        [SerializeField] private float currentFuelInGrams;
        [SerializeField] private float fuelConsumptionRateInGramsPerSecond = 0.1f; // 0.1 grams per second

        [SerializeField] private Image fuelRemainingImage;
        [SerializeField] private TMP_Text fuelTextDisplay;

        // Energy density of D-T fusion is about 3.38e14 J/kg
        private const float EnergyDensityJoulesPerKg = 3.38e14f;
        // Let's assume 40% efficiency in converting fusion energy to thrust
        private const float EfficiencyFactor = 0.40f;

        private void Start()
        {
            currentFuelInGrams = maxFuelInGrams;
            UpdateFuelUI();
        }

        private void Update()
        {
            UpdateFuelUI();
        }

        public void ConsumeFuel(float amountInGrams)
        {
            currentFuelInGrams = Mathf.Max(0, currentFuelInGrams - amountInGrams);
        }

        public void RefuelShip(float amountInGrams)
        {
            currentFuelInGrams = Mathf.Min(maxFuelInGrams, currentFuelInGrams + amountInGrams);
        }

        private void UpdateFuelUI()
        {
            float fuelPercentage = currentFuelInGrams / maxFuelInGrams;
            fuelRemainingImage.fillAmount = fuelPercentage;
            fuelTextDisplay.text = $"Fuel: {currentFuelInGrams:F2} g ";
        }

        public bool HasFuel()
        {
            return currentFuelInGrams > 0;
        }

        public float GetAvailableEnergyInJoules()
        {
            return currentFuelInGrams / 1000f * EnergyDensityJoulesPerKg * EfficiencyFactor;
        }

        public float GetCurrentFuelInGrams()
        {
            return currentFuelInGrams;
        }

        public float GetMaxFuelInGrams()
        {
            return maxFuelInGrams;
        }
    }
}
