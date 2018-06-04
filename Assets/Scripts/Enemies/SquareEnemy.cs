using UnityEngine;

namespace CannonGame
{
    /*  SquareEnemy Class
     * 
     *  A Square Enemy! Just moves towards the Player.
     */
    public class SquareEnemy : Enemy
    {
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
            if (collider.gameObject.CompareTag("Player"))
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
    }
}
