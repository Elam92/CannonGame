﻿using UnityEngine;
using NUnit.Framework;
using CannonGame;
using NSubstitute;

namespace CannonGameTests
{
    public class MovementTests
    {

        public Transform objectTransform;
        public Movement basicMovement;

        [SetUp]
        public void Initialize_Movement()
        {
            var go = new GameObject();
            objectTransform = go.transform;
            objectTransform.position = Vector3.zero;

            var speed = 1f;

            IUnityTime timeInformation = Substitute.For<IUnityTime>();
            timeInformation.GetTime().Returns(1f);

            basicMovement = new Movement(objectTransform, speed, timeInformation);
        }

        [Test]
        public void Movement_Move_RightForward()
        {
            float horizontal = 1f;
            float vertical = 1f;
            Vector3 result = new Vector3(horizontal, 0f, vertical);

            basicMovement.Move(horizontal, vertical);
            Assert.AreEqual(result, objectTransform.position);
        }

        [Test]
        public void Movement_Move_LeftBackward()
        {
            float horizontal = -1f;
            float vertical = -1f;
            Vector3 result = new Vector3(horizontal, 0f, vertical);

            basicMovement.Move(horizontal, vertical);

            Assert.AreEqual(result, objectTransform.position);
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
