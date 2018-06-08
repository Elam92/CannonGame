using UnityEngine;
using UnityEngine.UI;

namespace CannonGame
{
    /*  ScoreManager Class
     * 
     *  Keeps track of how many points the Player has gotten from killing enemies
     *  and displays it on the UI.
     */
    public class ScoreManager : MonoBehaviour, IScoreManager
    {

        // The UI Component to show the current score.
        private Text scoreText;

        // The Player's current score.
        private int currentScore = 0;

        // Maxmium score Player can reach.
        public static readonly int MAX_SCORE = 9999;

        // Minimum score Player can fall to.
        public static readonly int MIN_SCORE = 0;

        private void Awake()
        {
            currentScore = 0;
            scoreText = GetComponentInChildren<Text>();
            UpdateScore(currentScore);
        }

        // Updates the current score.
        public void UpdateScore(int score)
        {
            currentScore += score;

            if(currentScore > MAX_SCORE)
            {
                currentScore = MAX_SCORE;
            }
            if(currentScore < MIN_SCORE)
            {
                currentScore = MIN_SCORE;
            }

            scoreText.text = "Score: " + currentScore;
        }

        // Get the current score.
        public int GetScore()
        {
            return currentScore;
        }
    }
}
