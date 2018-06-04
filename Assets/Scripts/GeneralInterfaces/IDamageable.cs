namespace CannonGame
{
    public interface IDamageable
    {
        // Enable objects to be able to take damage.
        void TakeHit(float damage);
    }
}