using UnityEngine;

namespace CannonGame
{
    /*  GameController Class
     * 
     *  Sets up game objects, controls spawning enemies,
     *  and watches for when game ends. Controls game flow.
     */
    public class GameController : MonoBehaviour
    {
        // The Player.
        public Player player;

        // Spawns enemies in the level.
        public EnemySpawner enemySpawner;

        // Game Over Screen.
        public GameOverUI gameOverUI;

        // Tracks overall score earned in gameplay.
        public ScoreManager scoreManager;

        // Takes care of object pooling.
        public PoolManager poolManager;

        public int poolUserInstances = 5;

        // The maximum time it takes to spawn an enemy.
        public float maxSpawnTime = 5f;

        // Time that went by since spawning the last enemy.
        private float timeElapsed = 0f;

        // The time to reach to spawn the next enemy.
        private float timeToSpawn = 2f;

        // Unity Wrapper for getting a time value.
        private IUnityTime unityTime;

        // Use this for initialization
        void Start()
        {
            unityTime = new UnityDeltaTime();

            // Turn on GameOver Screen when Player is dead.
            player.GetHealth().OnDeath += GameOver;

            // Look through all Pool Users inside Player object.
            IPoolUser[] poolUsers = player.GetComponentsInChildren<IPoolUser>();
            foreach(IPoolUser pUser in poolUsers)
            {
                pUser.InitializeWithPooler(poolManager, poolUserInstances);
            }

            // Initialize enemy spawner.
            enemySpawner.Initialize(player, poolManager, scoreManager);
            
        }

        // Update is called once per frame
        void Update()
        {
            timeElapsed += unityTime.GetTime();

            if (timeElapsed > timeToSpawn)
            {
                enemySpawner.Spawn();

                // Get ready for the next spawn.
                timeElapsed = 0;
                timeToSpawn = Random.Range(0, maxSpawnTime);
            }
        }
        
        // Unsubscribe from events.
        private void OnDestroy()
        {
            Health playerHealth = player.GetHealth();

            if(playerHealth != null)
                playerHealth.OnDeath -= GameOver;
        }

        // Show Game Over Screen when player dies.
        private void GameOver()
        {
            Time.timeScale = 0;
            gameOverUI.Display();
        }
    }
}
