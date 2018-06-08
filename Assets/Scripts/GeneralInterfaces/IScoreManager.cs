namespace CannonGame
{
    public interface IScoreManager
    {
        // Update the current score.
        void UpdateScore(int score);

        // Return the current score.
        int GetScore();
    }
}