using UnityEngine;

namespace CannonGame {

    /*  AmmoLoader Class
     * 
     *  Contains the different types of projectiles the Player can use.
     */
    public class AmmoLoader : MonoBehaviour, IPoolUser {

        // The different projectiles.
        public Projectile[] projectileTypes;

        // Pooler Manager reference to reuse the projectiles.
        private IPoolManager pooler;

        // The current projectile being used.
        private Projectile currentType;

        private void Start()
        {

        }

        // Instantiate a projectile to be launched.
        public Projectile UseType(Vector3 position, Quaternion rotation)
        {
            if (projectileTypes.Length == 0)
            {
                return null;
            }

            return pooler.UseObject(currentType, position, rotation) as Projectile;
        }

        // Set projectile type by index.
        public void SetType(int index)
        {
            if (index < 0)
            {
                index = 0;
            }
            else if (index >= projectileTypes.Length)
            {
                index = projectileTypes.Length - 1;
            }

            currentType = projectileTypes[index];
        }

        // Look at the next projectile of current type.
        public Projectile GetCurrentType()
        {
            return pooler.Peek(currentType) as Projectile;
        }

        // Create pools of the projectiles that we have in our container.
        public void InitializeWithPooler(IPoolManager pooler, int poolSize)
        {
            this.pooler = pooler;

            if (poolSize <= 0 || projectileTypes == null || projectileTypes.Length == 0)
            {
                return;
            }

            for (int i = 0; i < projectileTypes.Length; i++)
            {
                PooledObject[] poolContainer = pooler.CreatePool(projectileTypes[i], 5);
                foreach (PooledObject poolObj in poolContainer)
                {
                    IPoolUser poolUser = poolObj.GetComponent<IPoolUser>();
                    if (poolUser != null)
                    {
                        poolUser.InitializeWithPooler(pooler, 5);
                    }
                }
            }

            SetType(0);
        }

    }
}
