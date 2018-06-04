using UnityEngine;

namespace CannonGame
{
    /*  UnityDeltaTime Class
     * 
     *  Wrapper for Unity Time API to get delta time.
     */ 
    public class UnityDeltaTime : IUnityTime
    {
        // Get the time it took to complete the last frame.
        public float GetTime()
        {
            return Time.deltaTime;
        }
    }
}
