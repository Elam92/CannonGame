using UnityEngine;

namespace CannonGame
{
    /*  StandardExplosiveProjectile Class
     * 
     *  A projectile that explodes on hit.
     */
    [RequireComponent(typeof(Rigidbody))]
    public class StandardProjectileExplodable : StandardProjectile, IExplodable, IPoolUser
    {
        // The explosion aftereffect when it hits something.
        public PooledObject explodePrefab;

        // The Pool Manager that handles object reuse.
        private IPoolManager pooler;

        // Turn off the projectile for future use. But now also explode!
        public override void Death()
        {
            Explode(transform.position, transform.rotation);
            Deactivate();
        }

        // Instantiate an aftereffect when it hits something.
        public void Explode(Vector3 position, Quaternion rotation)
        {
            if (pooler != null)
            {
                pooler.UseObject(explodePrefab, position, rotation);

            }
        }

        // Initialize the Pool Manager instance so that we can reuse the explosion prefab.
        public void InitializeWithPooler(IPoolManager pooler, int poolSize)
        {
            this.pooler = pooler;

            if (poolSize <= 0 || explodePrefab == null)
            {
                return;
            }

            pooler.CreatePool(explodePrefab, poolSize);
        }


    }
}
