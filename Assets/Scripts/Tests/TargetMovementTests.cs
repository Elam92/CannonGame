using UnityEngine;
using NUnit.Framework;
using CannonGame;
using NSubstitute;

namespace CannonGameTests
{
    public class TargetMovementTests
    {
        public Transform mover;
        public TargetMovement targetMovement;
        public Vector3 origin;
        public float rangeLimit;

        [SetUp]
        public void InitializeTargetMovement()
        {
            var go = new GameObject();
            mover = go.transform;
            mover.position = Vector3.zero;

            origin = Vector3.zero;
            rangeLimit = 2f;

            var speed = 1f;

            IUnityTime timeInformation = Substitute.For<IUnityTime>();
            timeInformation.GetTime().Returns(1f);

            targetMovement = new TargetMovement(mover, origin, speed, rangeLimit, timeInformation);
        }

        [Test]
        public void TargetMovement_Move_RightForward()
        {
            float horizontal = 1f;
            float vertical = 1f;
            Vector3 result = new Vector3(horizontal, 0f, vertical);

            targetMovement.Move(horizontal, vertical);
            Assert.AreEqual(result, mover.position);
        }

        [Test]
        public void TargetMovement_Move_LeftBackward()
        {
            float horizontal = -1f;
            float vertical = -1f;
            Vector3 result = new Vector3(horizontal, 0f, vertical);

            targetMovement.Move(horizontal, vertical);

            Assert.AreEqual(result, mover.position);
        }

        // Top Right
        [TestCase(200, 200, 1.4f, 1.4f)]
        // Bottom
        [TestCase(0, -100, 0, -2f)]
        // Top
        [TestCase(0, 100, 0, 2f)]
        // Left
        [TestCase(-100, 0, -2f, 0)]
        // Right
        [TestCase(100, 0, 2f, 0)]
        // Bottom Left
        [TestCase(-200, -200, -1.4f, -1.4f)]
        public void TargetMovement_Move_MaxRange(float horizontal, float vertical, float expectedX, float expectedZ)
        {
            targetMovement.Move(horizontal, vertical);

            Assert.AreEqual(expectedX, mover.position.x, 0.05f);
            Assert.AreEqual(expectedZ, mover.position.z, 0.05f);
        }
    }
}
