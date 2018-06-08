using UnityEngine;

namespace CannonGame
{
    /*  Score Class
     * 
     *  Gives objects a score value that the Player can earn.
     */ 
    public class Score : MonoBehaviour, IScore
    {
        // The score value of an object.
        public int scorePoints = 10;

        // Tracks all the scores earned.
        private IScoreManager scoreManager;

        // Add object's score value to the total score earned.
        public void AddScore()
        {
            if(scoreManager != null)
            {
                scoreManager.UpdateScore(scorePoints);
            }
        }

        // Initialize Score component with object that tracks scores.
        public void Initialize(IScoreManager scoreManager)
        {
            this.scoreManager = scoreManager;
        }
    }
}
