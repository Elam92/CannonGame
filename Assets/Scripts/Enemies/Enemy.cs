using UnityEngine;

namespace CannonGame
{
    /*  Enemy Class
     * 
     *  The base enemy that all enemies should derive from.
     */
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Score))]
    public abstract class Enemy : PooledObject
    {
        // How much damage it should deal out.
        public float damage = 10f;

        // How much the enemy is worth in points.
        public int scoreValue = 10;

        // How fast should it move.
        public float moveSpeed = 5f;

        // Its health.
        protected Health health;

        // To add its score to the game when it dies.
        protected Score scorer;

        // The target the enemy is focusing on.
        protected Transform target;

        // How the enemy should be controlled.
        protected IMoveInput inputMove;

        // How the enemy should move around.
        protected IMoveable movement;

        protected virtual void Awake()
        {
            health = GetComponent<Health>();
            scorer = GetComponent<Score>();

            gameObject.SetActive(false);
        }

        // Set the target for the enemy to focus on.
        public abstract void SetTarget(Transform newTarget);

        // Reset itself to initial state in a new location.
        public override void Reset(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
            health.Reset();

            gameObject.SetActive(true);
        }

        // When it dies, add score to the game.
        public virtual void Death()
        {
            Deactivate();
            scorer.AddScore();
        }

    }
}
