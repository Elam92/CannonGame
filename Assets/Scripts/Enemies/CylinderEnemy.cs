using UnityEngine;

namespace CannonGame
{
    /*  CylinderEnemy Class
     * 
     *  A Cylindral Enemy that explodes!
     */
    public class CylinderEnemy : Enemy, IExplodable, IPoolUser
    {
        // The explosion that spawns after it dies.
        public PooledObject explodePrefab;

        // Pooler Manager reference to reuse its explosion prefab.
        private IPoolManager pooler;

        // Its UI health bar.
        private HealthBar hpBar;

        protected override void Awake()
        {
            base.Awake();

            // Using basic movement.
            movement = new Movement(transform, moveSpeed);

            hpBar = GetComponentInChildren<HealthBar>();
        }

        protected void Start()
        {
            // HP Bar listen to its health to update accordingly.
            health.OnHealthChange += hpBar.UpdateHealthBar;

            // Listen for when it dies.
            health.OnDeath += Death;
        }

        private void Update()
        {
            // Before reading inputs and moving, we need a target for its movement input.
            if (inputMove != null)
            {
                inputMove.ReadInput();
                movement.Move(inputMove.HorizontalInput, inputMove.VerticalInput);
            }
        }

        // Unsubscribe from events.
        private void OnDestroy()
        {
            health.OnHealthChange -= hpBar.UpdateHealthBar;
            health.OnDeath -= Death;
        }

        private void OnTriggerEnter(Collider collider)
        {
            // Only want to do damage to the Player.
            if(collider.gameObject.CompareTag("Player"))
            {
                IDamageable doDamage = collider.gameObject.GetComponent<IDamageable>();
                if (doDamage != null)
                {
                    doDamage.TakeHit(damage);
                }
                Deactivate();
            }
        }

        // Target to move towards.
        public override void SetTarget(Transform newTarget)
        {
            inputMove = new AIMoveInput(transform, newTarget);
        }

        // Reset itself back to initial state in a new location.
        public override void Reset(Vector3 position, Quaternion rotation)
        {
            base.Reset(position, rotation);
            hpBar.UpdateHealthBar(health.GetHP());
        }

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
