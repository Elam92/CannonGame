using UnityEngine;

namespace CannonGame
{
    public interface IExplodable
    {
        // Enables objects to explode!
        void Explode(Vector3 position, Quaternion rotation);
    }
}