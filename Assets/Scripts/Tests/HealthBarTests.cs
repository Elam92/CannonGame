using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using CannonGame;

namespace CannonGameTests
{
    public class HealthBarTests
    {
        public Image hpBarUI;

        public HealthBar healthBar;

        [SetUp]
        public void HealthBar_Create()
        {
            var go = new GameObject();
            hpBarUI = go.AddComponent<Image>();
            healthBar = go.AddComponent<HealthBar>();
        }

        // Add over the fill amount.
        [TestCase(2f, 1f)]
        // Fill to 100%.
        [TestCase(1f, 1f)]
        // Fill half way.
        [TestCase(0.5f, 0.5f)]
        // Zero health.
        [TestCase(0f, 0f)]
        // Give a negative health value.
        [TestCase(-1f, 0f)]
        public void HealthBar_UpdateHealth_ToAmount(float amount, float result)
        {
            healthBar.UpdateHealthBar(amount);

            Assert.AreEqual(result, hpBarUI.fillAmount, 0.005f);
        }

        [TearDown]
        public void CleanUp()
        {
            GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();
            foreach (GameObject go in allObjects)
            {
                if (go.activeInHierarchy)
                {
                    Object.Destroy(go);
                }
            }
        }
    }
}
