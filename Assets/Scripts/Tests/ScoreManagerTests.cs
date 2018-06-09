using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using CannonGame;

namespace CannonGameTests
{
    public class ScoreManagerTests
    {
        public ScoreManager scoreManager;

        [SetUp]
        public void ScoreManager_Initialize()
        {
            var go = new GameObject();
            go.AddComponent<Text>();
            scoreManager = go.AddComponent<ScoreManager>();
        }

        [Test]
        public void ScoreManager_Initialize_ScoreIsZero()
        {
            int result = 0;

            Assert.AreEqual(result, scoreManager.GetScore());
        }

        // Add Score.
        [TestCase(10, 10)]
        // Subtract Score.
        [TestCase(-10, 0)]
        public void ScoreManager_UpdateScore_ScoreChanged(int scoreChange, int result)
        {
            scoreManager.UpdateScore(scoreChange);

            Assert.AreEqual(result, scoreManager.GetScore());
        }

        // Go over Max Score Value.
        [TestCase(100000)]
        // Go within/under Max Score Value.
        [TestCase(5000)]
        public void ScoreManager_ReachMaxScore_ScoreUnderOrAtMax(int scoreChange)
        {
            scoreManager.UpdateScore(scoreChange);

            Assert.GreaterOrEqual(ScoreManager.MAX_SCORE, scoreManager.GetScore());
        }

        // Go below Min Score Value.
        [TestCase(100, -1000)]
        // Go within/above Min Score Value.
        [TestCase(10, -5)]
        public void ScoreManager_ReachMinScore_ScoreAboveOrAtMin(int initialScore, int scoreChange)
        {
            scoreManager.UpdateScore(initialScore);

            scoreManager.UpdateScore(scoreChange);

            Assert.LessOrEqual(ScoreManager.MIN_SCORE, scoreManager.GetScore());
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
