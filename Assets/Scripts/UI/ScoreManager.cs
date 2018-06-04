using UnityEngine;
using UnityEngine.UI;

namespace CannonGame
{
    /*  ScoreManager Class
     * 
     *  Keeps track of how many points the Player has gotten from killing enemies
     *  and displays it on the UI.
     */
    public class ScoreManager : MonoBehaviour
    {

        // The UI Component to show the current score.
        private Text scoreText;

        // The Player's current score.
        private int currentScore = 0;

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

            scoreText.text = "Score: " + currentScore;
        }

        // Get the current score.
        public int GetScore()
        {
            return currentScore;
        }
    }
}
