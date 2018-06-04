using UnityEngine;

namespace CannonGame
{
    public interface IPoolManager
    {
        // Creates a group of objects to be reused in the game.
        PooledObject[] CreatePool(PooledObject prefab, int poolSize);

        // Use an existing pool of objects.
        PooledObject UseObject(PooledObject prefab, Vector3 position, Quaternion rotation);

        // Look at the next pooled object from its pool before it's used.
        PooledObject Peek(PooledObject prefab);

        // Check if an object is already pooled.
        bool HasObject(int poolKey);
    }
}
