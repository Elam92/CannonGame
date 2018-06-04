using UnityEngine;

namespace CannonGame
{
    /*  CircleProjectile Class
     * 
     *  A projectile that explodes on hit.
     */
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    public class CircleProjectile : Projectile, IExplodable, IPoolUser
    {
        // The explosion aftereffect when it hits something.
        public PooledObject explodePrefab;

        // The Pool Manager that handles object reuse.
        private IPoolManager pooler;

        private Rigidbody rb;

        protected override void Awake()
        {
            base.Awake();
            rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Ignore Player Tag/Player.
            if (collision.gameObject.CompareTag("Player"))
            {
                return;
            }

            // If we collided with something that's damagable.
            IDamageable hittable = collision.gameObject.GetComponent<IDamageable>();
            if (hittable != null)
            {
                hittable.TakeHit(damage);
                Death();
            }

            // If we end up hitting the ground.
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                Death();
            }

        }

        // Reset projectile back to its initial state at a specified position and rotation.
        public override void Reset(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            gameObject.SetActive(true);
        }

        // Turn off the projectile for future use. But now also explode!
        public override void Death()
        {
            Explode(transform.position, transform.rotation);
            Deactivate();
        }

        // Send the projectile in a direction with gravity enabled.
        public override void Launch(Vector3 velocity)
        {
            rb.useGravity = true;
            rb.velocity = velocity;
        }

        // Instantiate an aftereffect when it hits something.
        public void Explode(Vector3 position, Quaternion rotation)
        {
           if(pooler != null)
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
