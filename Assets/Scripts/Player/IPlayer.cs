using UnityEngine;

namespace CannonGame
{
    public interface IPlayer
    {
        // Initialize the object.
        void Initialize();

        // The method called per frame.
        void Tick();

        // Clean up the implementing object when it's no longer in use.
        void CleanUp();

        // Get the object's Transform component.
        Transform GetTransform();

        // Get the shootable range of the Player.
        float GetRadius();
    }
}