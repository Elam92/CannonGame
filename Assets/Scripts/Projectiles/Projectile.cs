using UnityEngine;

namespace CannonGame
{
    /*  Projectile Class
     * 
     *  Used as a base class for bullets in the game.
     */
    public abstract class Projectile : PooledObject, ILaunchable
    {
        // How much damage the projectile does.
        public float damage = 10f;

        // Immediately turn off the projectile when instantiated.
        protected virtual void Awake()
        {
            gameObject.SetActive(false);
        }

        // Remove the projectile.
        public abstract void Death();

        // Send the projectile in a direction.
        public abstract void Launch(Vector3 velocity);

    }
}
