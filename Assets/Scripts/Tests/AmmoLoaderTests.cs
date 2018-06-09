using UnityEngine;
using NUnit.Framework;
using CannonGame;
using NSubstitute;

namespace CannonGameTests
{
    public class AmmoLoaderTests
    {

        public AmmoLoader ammoLoader;
        public IPoolManager poolManager;


        [SetUp]
        public void Initialize_AmmoLoader()
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
            ammoLoader.InitializeWithPooler(poolManager, 1);
        }

        [Test]
        public void AmmoLoader_InitializedWithPooler_PoolerReceivedCreatePoolMethod()
        {
            ammoLoader.InitializeWithPooler(poolManager, 1);

            poolManager.ReceivedWithAnyArgs().CreatePool(null, 1);
        }

        [Test]
        public void AmmoLoader_GetCurrentType_PoolerReceivedPeekMethod()
        {
            ammoLoader.GetCurrentType();

            poolManager.ReceivedWithAnyArgs().Peek(null);
        }

        [Test]
        public void AmmoLoader_UseType_PoolerReceivedUseObjectMethod()
        {
            int index = 0;
            ammoLoader.SetType(index);

            ammoLoader.UseType(Vector3.one, Quaternion.identity);

            poolManager.ReceivedWithAnyArgs().UseObject(null, Vector3.one, Quaternion.identity);
        }

        [Test]
        public void AmmoLoader_SetProjectile_ReturnType()
        {
            int index = 0;
            Projectile expectedResult = ammoLoader.projectileTypes[index];
            poolManager.Peek(expectedResult).Returns(expectedResult);

            ammoLoader.SetType(index);

            Assert.AreEqual(expectedResult, ammoLoader.GetCurrentType());
        }

        [Test]
        public void AmmoLoader_SetProjectileWithNegativeIndex_ReturnFirstType()
        {
            int index = -1;
            Projectile expectedResult = ammoLoader.projectileTypes[0];
            poolManager.Peek(expectedResult).Returns(expectedResult);

            ammoLoader.SetType(index);

            Assert.AreEqual(expectedResult, ammoLoader.GetCurrentType());
        }

        [Test]
        public void AmmoLoader_SetProjectileWithIndexGreaterThanArray_ReturnLastType()
        {
            int index = 5;
            Projectile expectedResult = ammoLoader.projectileTypes[ammoLoader.projectileTypes.Length - 1];
            poolManager.Peek(expectedResult).Returns(expectedResult);

            ammoLoader.SetType(index);

            Assert.AreEqual(expectedResult, ammoLoader.GetCurrentType());
        }

        [Test]
        public void AmmoLoader_SetProjectileWithNullArray_ReturnNull()
        {
            int index = 1;
            ammoLoader.projectileTypes = null;
            Projectile expectedResult = null;
            poolManager.Peek(expectedResult).Returns(expectedResult);

            ammoLoader.SetType(index);

            Assert.AreEqual(expectedResult, ammoLoader.GetCurrentType());
        }

        [Test]
        public void AmmoLoader_SetProjectileWithEmptyArray_ReturnNull()
        {
            int index = 1;
            ammoLoader.projectileTypes = new Projectile[0];
            Projectile expectedResult = null;
            poolManager.Peek(expectedResult).Returns(expectedResult);

            ammoLoader.SetType(index);

            Assert.AreEqual(expectedResult, ammoLoader.GetCurrentType());
        }

        [Test]
        public void AmmoLoader_UseTypeWithCurrentType_ReturnCurrentType()
        {
            int index = 0;
            Projectile expectedResult = ammoLoader.projectileTypes[index];
            poolManager.UseObject(expectedResult, Vector3.one, Quaternion.identity).Returns(expectedResult);
            ammoLoader.SetType(index);

            Projectile result = ammoLoader.UseType(Vector3.one, Quaternion.identity);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void AmmoLoader_UseTypeWithCurrentTypeAsNull_ReturnNull()
        {
            int index = 0;
            Projectile expectedResult = null;
            poolManager.UseObject(expectedResult, Vector3.one, Quaternion.identity).Returns(expectedResult);
            ammoLoader.projectileTypes = new Projectile[0];
            ammoLoader.SetType(index);

            Projectile result = ammoLoader.UseType(Vector3.one, Quaternion.identity);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
