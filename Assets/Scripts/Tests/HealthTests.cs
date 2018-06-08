using NUnit.Framework;
using CannonGame;
using UnityEngine;

// Doing testing in PlayMode so that Awake is called to initialize Health component.
public class HealthTests
{
    [Test]
    public void Health_Initialize_IsPositive()
    {
        var go = new GameObject();
        var health = go.AddComponent<Health>();

        Assert.Positive(health.GetHP());
    }

    [Test]
    public void Health_TakeDamage_LoseHealth()
    {
        var go = new GameObject();
        var health = go.AddComponent<Health>();
        float hpValue = health.GetHP();
        float damage = 10;
        float result = hpValue - damage;

        health.TakeHit(damage);

        Assert.AreEqual(result, health.GetHP());
    }

    [Test]
    public void Health_TakeNegativeDamage_BeSameHealth()
    {
        var go = new GameObject();
        var health = go.AddComponent<Health>();
        float hpValue = health.GetHP();
        float damage = -10f;

        health.TakeHit(damage);

        Assert.AreEqual(hpValue, health.GetHP());
    }

    [Test]
    public void Health_Reset_BackToFull()
    {
        var go = new GameObject();
        var health = go.AddComponent<Health>();
        float hpValue = health.GetHP();
        float damage = 10;

        health.TakeHit(damage);
        health.Reset();

        Assert.AreEqual(hpValue, health.GetHP());
    }

    [Test]
    public void Health_TakeDamage_CallOnHealthChange()
    {
        var go = new GameObject();
        var health = go.AddComponent<Health>();
        float oldHPValue = health.GetHP();
        float damage = 10f;

        float healthChanged = 0;
        health.OnHealthChange += (fraction) =>
        {
            healthChanged = fraction;
        };

        health.TakeHit(damage);

        Assert.AreEqual(healthChanged, health.GetHP()/oldHPValue, 0.5f);
    }

    [Test]
    public void Health_KillObject_CallOnDeath()
    {
        var go = new GameObject();
        var health = go.AddComponent<Health>();
        float damage = health.GetHP();

        bool isDead = false;
        health.OnDeath += () =>
        {
            isDead = true;
        };

        health.TakeHit(damage);

        Assert.IsTrue(isDead);
    }
}
