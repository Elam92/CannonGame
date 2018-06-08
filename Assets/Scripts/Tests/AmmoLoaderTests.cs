using UnityEngine;
using NUnit.Framework;
using CannonGame;
using NSubstitute;

public class AmmoLoaderTests
{

    public AmmoLoader ammoLoader;
    public IPoolManager poolManager;
    

    [SetUp]
    public void InitializeAmmoLoader()
    {
        var go = new GameObject();
        go.transform.position = Vector3.zero;
        ammoLoader = go.AddComponent<AmmoLoader>();

        Projectile[] types = ammoLoader.projectileTypes = new Projectile[2];

        var projectileGO1 = new GameObject();
        Projectile projectile1 = projectileGO1.AddComponent<StandardProjectile>();

        var projectileGO2 = new GameObject();
        Projectile projectile2 = projectileGO2.AddComponent<StandardProjectileExplodable>();

        types[0] = projectile1;
        types[1] = projectile2;

        poolManager = Substitute.For<IPoolManager>();
        //poolManager.CreatePool(types[0], 1).Returns(new PooledObject[2] { types[0], types[1] });
        ammoLoader.InitializeWithPooler(poolManager, 1);
    }

    [Test]
    public void AmmoLoader_SetProjectile_ReturnType()
    {
        int index = 0;
        Projectile result = ammoLoader.projectileTypes[index];
        poolManager.Peek(result).Returns(result);

        ammoLoader.SetType(index);

        Assert.AreEqual(result, ammoLoader.GetCurrentType());
    }

    [Test]
    public void AmmoLoader_SetProjectileWithNegativeIndex_ReturnFirstType()
    {
        int index = -1;
        Projectile result = ammoLoader.projectileTypes[0];
        poolManager.Peek(result).Returns(result);

        ammoLoader.SetType(index);

        Assert.AreEqual(result, ammoLoader.GetCurrentType());
    }

    [Test]
    public void AmmoLoader_SetProjectileWithIndexGreaterThanArray_ReturnLastType()
    {
        int index = 5;
        Projectile result = ammoLoader.projectileTypes[ammoLoader.projectileTypes.Length-1];
        poolManager.Peek(result).Returns(result);

        ammoLoader.SetType(index);

        Assert.AreEqual(result, ammoLoader.GetCurrentType());
    }

    [Test]
    public void AmmoLoader_SetProjectileWithNullArray_ReturnNull()
    {
        int index = 1;
        ammoLoader.projectileTypes = null;
        Projectile result = null;
        poolManager.Peek(result).Returns(result);

        ammoLoader.SetType(index);

        Assert.AreEqual(result, ammoLoader.GetCurrentType());
    }

    [Test]
    public void AmmoLoader_SetProjectileWithEmptyArray_ReturnNull()
    {
        int index = 1;
        ammoLoader.projectileTypes = new Projectile[0];
        Projectile result = null;
        poolManager.Peek(result).Returns(result);

        ammoLoader.SetType(index);

        Assert.AreEqual(result, ammoLoader.GetCurrentType());
    }
}
