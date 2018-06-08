using UnityEngine;

namespace CannonGame
{
    /*  StandardEnemyExplodable Class
     * 
     *  A standard Enemy that explodes on death.
     */
    public class StandardEnemyExplodable : StandardEnemy, IExplodable, IPoolUser
    {
        // The explosion that spawns after it dies.
        public PooledObject explodePrefab;

        // Pooler Manager reference to reuse its explosion prefab.
        private IPoolManager pooler;

        // When the object dies.
        public override void Death()
        {
            Explode(transform.position, transform.rotation);
            base.Death();
        }

        // Instantiate an explosion object where it died.
        public void Explode(Vector3 position, Quaternion rotation)
        {
            if (pooler != null)
            {
                pooler.UseObject(explodePrefab, position, rotation);
            }
        }

        // Create a pool of the explosion prefab that we have.
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
