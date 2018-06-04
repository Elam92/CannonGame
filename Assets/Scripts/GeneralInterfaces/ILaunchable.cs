using UnityEngine;

namespace CannonGame
{
    public interface ILaunchable
    {
        // Launch itself towards a direction.
        void Launch(Vector3 velocity);
    }
}