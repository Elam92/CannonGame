using System.Collections.Generic;
using UnityEngine;

namespace CannonGame
{
    /*  PoolManager Class
     * 
     *  Singleton class that manages Object Pooling.
     *  Creates "pools" of different game objects that can be recycled during
     *  gameplay.
     */
    public class PoolManager : MonoBehaviour, IPoolManager
    {

        // Dictionary of different GameObject types to pool and reuse.
        private Dictionary<int, Queue<PooledObject>> poolDictionary = new Dictionary<int, Queue<PooledObject>>();
        
        // The pool objects' parent transform containers in the scene to keep things organized.
        private Dictionary<int, Transform> poolParents = new Dictionary<int, Transform>();

        // Get Singleton instance of PoolManager.
        public static PoolManager Instance { get; private set; }

        private void Awake()
        {
            // Ensure that there is only 1 copy of PoolManager in the scene.
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        // If we're destroying this, make sure to clear the Instance.
        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }

        // Creates a new pool of a game object with a specified amount of objects to Instantiate.
        public PooledObject[] CreatePool(PooledObject prefab, int poolSize)
        {
            int poolKey = prefab.GetInstanceID();
            PooledObject[] poolContainer = new PooledObject[poolSize];

            if (!HasObject(poolKey))
            {
                poolDictionary.Add(poolKey, new Queue<PooledObject>());

                // Keep things clean and organized in the scene hierarchy.
                Transform poolHolderTrans = new GameObject(prefab.name + " pool").transform;
                poolHolderTrans.parent = transform;
                poolParents.Add(poolKey, poolHolderTrans);

                // Create new instances of this game object to be pooled.
                for (int i = 0; i < poolSize; i++)
                {
                    PooledObject poolObj = InstantiateInstance(poolKey, prefab);
                    poolContainer[i] = poolObj;
                }
            }

            return poolContainer;
        }

        // Use a pooled object that it manages.
        public PooledObject UseObject(PooledObject prefab, Vector3 position, Quaternion rotation)
        {
            int poolKey = prefab.GetInstanceID();

            if (!HasObject(poolKey))
            {
                return null;
            }

            PooledObject useObject = poolDictionary[poolKey].Dequeue();
            poolDictionary[poolKey].Enqueue(useObject);

            // If the object is already being used, time to instantiate a new one and add it to the pool.
            if (useObject.CheckActive())
            {
                useObject = InstantiateInstance(poolKey, prefab);
            }

            // Reset the values.
            useObject.Reset(position, rotation);

            return useObject;
        }

        // Look at the next pooled object to be used from its pool.
        public PooledObject Peek(PooledObject prefab)
        {
            int poolKey = prefab.GetInstanceID();

            if(!HasObject(poolKey) || poolDictionary[poolKey].Count == 0)
            {
                return null;
            }

            return poolDictionary[poolKey].Peek();
        }

        // Check if it contains a game object by its ID.
        public bool HasObject(int poolKey)
        {
            return poolDictionary.ContainsKey(poolKey);
        }

        // Create 1 instance of the prefab and add it into its pool.
        private PooledObject InstantiateInstance(int poolKey, PooledObject prefab)
        {
            PooledObject newObject = Instantiate(prefab) as PooledObject;

            poolDictionary[poolKey].Enqueue(newObject);
            newObject.transform.SetParent(poolParents[poolKey]);

            return newObject;
        }
    }
}