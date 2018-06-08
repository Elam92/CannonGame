using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using CannonGame;

public class ScoreManagerTests {

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


    [TestCase(10, 10)]
    [TestCase(-10, 0)]
    public void ScoreManager_UpdateScore_ScoreChanged(int scoreChange, int result)
    {
        scoreManager.UpdateScore(scoreChange);

        Assert.AreEqual(result, scoreManager.GetScore());
    }

    [TestCase(100000)]
    [TestCase(5000)]
    public void ScoreManager_ReachMaxScore_ScoreUnderOrAtMax(int scoreChange)
    {
        scoreManager.UpdateScore(scoreChange);

        Assert.GreaterOrEqual(ScoreManager.MAX_SCORE, scoreManager.GetScore());
    }

    [TestCase(100, -1000)]
    [TestCase(10, -5)]
    public void ScoreManager_ReachMinScore_ScoreAboveOrAtMin(int initialScore, int scoreChange)
    {
        scoreManager.UpdateScore(scoreChange);

        Assert.LessOrEqual(ScoreManager.MIN_SCORE, scoreManager.GetScore());
    }
}
