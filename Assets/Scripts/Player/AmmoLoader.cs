using UnityEngine;

namespace CannonGame {

    public class AmmoLoader : MonoBehaviour, IPoolUser {

        public Projectile[] projectileTypes;

        private IPoolManager pooler;
        private Projectile currentType;

        private void Start()
        {

        }

        public Projectile UseType(Vector3 position, Quaternion rotation)
        {
            if (projectileTypes.Length == 0)
            {
                return null;
            }

            return pooler.UseObject(currentType, position, rotation) as Projectile;
        }

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

        public Projectile GetCurrentType()
        {
            return pooler.Peek(currentType) as Projectile;
        }

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
