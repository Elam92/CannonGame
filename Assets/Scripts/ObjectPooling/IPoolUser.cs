namespace CannonGame
{
    public interface IPoolUser
    {
        // Initialize and use an object pooler to create a new pool of objects.
        void InitializeWithPooler(IPoolManager pooler, int poolSize);
    }
}