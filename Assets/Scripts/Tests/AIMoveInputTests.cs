using UnityEngine;
using NUnit.Framework;
using CannonGame;

namespace CannonGameTests
{
    public class AIMoveInputTests
    {

        public Transform objectTransform;
        public Transform targetTransform;
        public AIMoveInput moveInput;

        [SetUp]
        public void Initialize_AIMove()
        {
            var go = new GameObject();
            objectTransform = go.transform;
            objectTransform.position = Vector3.zero;

            var go2 = new GameObject();
            targetTransform = go2.transform;
            targetTransform.position = Vector3.one;
        }

        [Test]
        public void AIMoveInput_ReadInput_GetDirectionToTarget()
        {
            targetTransform.position = new Vector3(5f, 0f, 5f);
            moveInput = new AIMoveInput(objectTransform, targetTransform);
            float expectedResultX = 0.7f;
            float expectedResultY = 0.7f;

            moveInput.ReadInput();

            Assert.AreEqual(expectedResultX, moveInput.HorizontalInput, 0.05f);
            Assert.AreEqual(expectedResultY, moveInput.VerticalInput, 0.05f);
        }

        [Test]
        public void AIMoveInput_ReadInputWithNoTarget_DoNothing()
        {
            moveInput = new AIMoveInput(objectTransform, null);
            float expectedResultX = 0f;
            float expectedResultY = 0f;

            moveInput.ReadInput();

            Assert.AreEqual(expectedResultX, moveInput.HorizontalInput, 0.05f);
            Assert.AreEqual(expectedResultY, moveInput.VerticalInput, 0.05f);
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
