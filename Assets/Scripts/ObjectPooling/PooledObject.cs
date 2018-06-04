using UnityEngine;

namespace CannonGame
{
    /*  PooledObject Class
     * 
     *  Enables objects to be reused by the Object Pool Manager.
     */
    public abstract class PooledObject : MonoBehaviour
    {

        // Check if the object is already active in the scene.
        public bool CheckActive()
        {
            return gameObject.activeSelf;
        }

        // Disable the pooled object.
        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        // Reset objects to their initial state.
        public abstract void Reset(Vector3 position, Quaternion rotation);
    }
}