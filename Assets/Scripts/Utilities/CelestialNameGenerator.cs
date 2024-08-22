using UnityEngine;

namespace Utilities
{
    public class CelestialNameGenerator : MonoBehaviour
    {
        // Prefixes for stars
        private static readonly string[] StarPrefixes =
            { "Alpha", "Beta", "Gamma", "Delta", "Epsilon", "Zeta", "Eta", "Theta", "Iota", "Kappa" };

        // Suffixes for stars
        private static readonly string[] StarSuffixes =
            { "Centauri", "Cygni", "Draconis", "Eridani", "Leonis", "Orionis", "Persei", "Tauri", "Ursae", "Velorum" };

        // Prefixes for planets
        private static readonly string[] PlanetPrefixes =
            { "New", "Old", "Great", "Lesser", "Upper", "Lower", "Inner", "Outer" };

        // Suffixes for planets
        private static readonly string[] PlanetSuffixes = { "world", "terra", "globe", "sphere", };

        // Root words for planet names
        private static readonly string[] PlanetRoots =
        {
            "Aether", "Chrom", "Dyn", "Eos", "Ferra", "Gaia", "Helio", "Io", "Jovia", "Krypto", "Luna", "Meso", "Nova",
            "Ocea", "Plu", "Quantum", "Rego", "Sola", "Terra", "Umbra", "Vortex", "Xeno", "Yggdra", "Zephyr"
        };

        private static readonly string[] RomanNumeral =
        {
            "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X"
        };

        public static string GenerateStarName()
        {
            string prefix = StarPrefixes[Random.Range(0, StarPrefixes.Length)];
            string suffix = StarSuffixes[Random.Range(0, StarSuffixes.Length)];
            return $"{prefix} {suffix}";
        }

        public static string GeneratePlanetName()
        {
            // 50% chance of using a prefix
            bool usePrefix = Random.value > 0.5f;
            // 30% chance of using a suffix
            bool useSuffix = Random.value > 0.7f;
            // 20% chance of using a Roman numeral
            bool useRomanNumeral = Random.value > 0.8f;

            string name = PlanetRoots[Random.Range(0, PlanetRoots.Length)];

            if (usePrefix)
            {
                string prefix = PlanetPrefixes[Random.Range(0, PlanetPrefixes.Length)];
                name = $"{prefix} {name}";
            }

            if (useSuffix)
            {
                string suffix = PlanetSuffixes[Random.Range(0, PlanetSuffixes.Length)];
                name = $"{name}{suffix}";
            }

            if (useRomanNumeral)
            {
                string numeral = RomanNumeral[Random.Range(0, RomanNumeral.Length)];
                name = $"{name} {numeral}";
            }

            return name;
        }

        public static string GenerateRandomName()
        {
            // Generate a completely random name
            int syllables = Random.Range(2, 5);
            string name = "";
            string vowels = "aeiou";
            string consonants = "bcdfghjklmnpqrstvwxyz";

            for (int i = 0; i < syllables; i++)
            {
                name += consonants[Random.Range(0, consonants.Length)];
                name += vowels[Random.Range(0, vowels.Length)];
            }

            return char.ToUpper(name[0]) + name.Substring(1);
        }
    }
}
