﻿using UnityEngine;

namespace CannonGame
{
    /*  StandardProjectile Class
     *  
     *  The basic projectile in the game.
     */
    [RequireComponent(typeof(Rigidbody))]
    public class StandardProjectile : Projectile
    {
        protected Rigidbody rb;

        protected override void Awake()
        {
            base.Awake();
            rb = GetComponent<Rigidbody>();
        }

        protected void OnCollisionEnter(Collision collision)
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

        // Turn off the projectile for future use.
        public override void Death()
        {
            Deactivate();
        }

        // Send the projectile in a direction with gravity enabled.
        public override void Launch(Vector3 velocity)
        {
            rb.useGravity = true;
            rb.velocity = velocity;
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


    }
}
