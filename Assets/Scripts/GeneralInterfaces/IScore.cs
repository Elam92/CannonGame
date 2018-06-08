namespace CannonGame
{
    public interface IScore
    {
        // Initialize the ScoreManager class that handles the current score.
        void Initialize(IScoreManager scoreManager);

        // Add score to the current score in the game.
        void AddScore();
    }
}
