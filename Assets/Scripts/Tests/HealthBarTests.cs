using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using NUnit.Framework;
using CannonGame;

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

    [TestCase(2f, 1f)]
    [TestCase(1f, 1f)]
    [TestCase(0.5f, 0.5f)]
    [TestCase(0f, 0f)]
    [TestCase(-1f, 0f)]
    public void HealthBar_UpdateHealth_ToAmount(float amount, float result)
    {
        healthBar.UpdateHealthBar(amount);

        Assert.AreEqual(result, hpBarUI.fillAmount, 0.005f);
    }
}
