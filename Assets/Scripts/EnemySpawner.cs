using UnityEngine;

namespace CannonGame
{
    /*  EnemySpawner Class
     * 
     *  Spawns the different enemies in the level with a set distance
     *  away from the player's range radius.
     */
    public class EnemySpawner : MonoBehaviour, IPoolUser
    {
        // The different types of enemies in the level.
        public Enemy[] enemyPrefabs;

        // The distance away from the player to spawn an enemy.
        public float additionalDistFromPlayer = 20f;
        
        // The ground layer.
        private int groundLayer = 10;

        // The Player object.
        private Player target;
        // The Score Keeper for when enemies die.
        private ScoreManager scoreManager;
        // The Pool Manager that handles object reuse.
        private IPoolManager pooler;

        // Layermask to target for Raycasts.
        private int layerMask = 1;

        // Flag for checking if we have initialized first.
        private bool initialized = false;

        // Use this for initialization
        public void Initialize(Player player, IPoolManager newPooler, ScoreManager newScoreManager)
        {
            target = player;
            scoreManager = newScoreManager;

            // Get the ground layer.
            layerMask = 1 << groundLayer;

            // Create pools of our enemies.
            InitializeWithPooler(newPooler, 5);

            initialized = true;
        }

        // Finds a position around the player before spawning an enemy.
        public void Spawn()
        {
            if (initialized)
            {
                Vector3 targetPos = target.transform.position;

                // Get a random direction around the Player.
                float angle = Random.Range(0.0f, Mathf.PI * 2);
                Vector3 directionPosition = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

                // Ensure that it's spawned outside the Player's Range.
                directionPosition *= target.radius + additionalDistFromPlayer;

                // Get location and spawn it above the ground.
                Vector3 spawnPoint = targetPos - directionPosition;
                SpawnOnGround(spawnPoint);
            }
        }

        // Spawn enemies at the location at ground level.
        private void SpawnOnGround(Vector3 spawnLocation)
        {
            // Raycast from above down towards the ground.
            spawnLocation.y = 100f;
            RaycastHit hit;
            if (Physics.Raycast(spawnLocation, Vector3.down, out hit, 1000f, layerMask))
            {
                // Find the y-offset needed to adjust its position to be above the ground.
                Enemy enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
                float yOffset = enemy.GetComponent<MeshRenderer>().bounds.extents.y;

                // Instantiate the enemy and set its target.
                Vector3 spawnPosition = new Vector3(hit.point.x, hit.point.y + yOffset, hit.point.z);
                Enemy obj = (Enemy)pooler.UseObject(enemy, spawnPosition, Quaternion.identity);
                obj.SetTarget(target.transform);
            }
        }

        // Create pools of the enemies that we have in our container.
        public void InitializeWithPooler(IPoolManager pooler, int poolSize)
        {
            this.pooler = pooler;

            // Instantiate the enemies and set up their properties.
            for (int i = 0; i < enemyPrefabs.Length; i++)
            {
                PooledObject[] poolContainer = pooler.CreatePool(enemyPrefabs[i], poolSize);
                foreach (PooledObject poolObj in poolContainer)
                {
                    // Initialize features of the enemies.
                    IScore scorer = poolObj.GetComponent<IScore>();
                    if (scorer != null)
                    {
                        scorer.Initialize(scoreManager);
                    }
                    IPoolUser poolUser = poolObj.GetComponent<IPoolUser>();
                    if (poolUser != null)
                    {
                        poolUser.InitializeWithPooler(pooler, poolSize);
                    }
                }
            }
        }
    }
}
