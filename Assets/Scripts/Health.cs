using System;
using UnityEngine;

namespace CannonGame
{
    /*  Health Class
     * 
     * Give a health attribute to the game object. Can be damaged.
     */
    public class Health : MonoBehaviour, IDamageable
    {
        // Event for when health is changed.
        public Action<float> OnHealthChange = delegate { };

        // Event for when health goes to zero or below.
        public Action OnDeath = delegate { };

        // The starting health of the object.
        public float startingHealth = 100f;

        // The current health of the object.
        private float health;

        // The saved initial Health.
        private float oldHealth;

        private void Awake()
        {
            health = startingHealth;
            oldHealth = startingHealth;
        }

        // Reset the health back to its initial value.
        public void Reset()
        {
            health = oldHealth;
            OnHealthChange(health);
        }

        // Can be hurt and loses health.
        public void TakeHit(float damage)
        {
            health -= damage;
            float fraction = health / oldHealth;

            // Alert listeners to health change. 
            OnHealthChange(fraction);

            // Object has died.
            if (health <= 0)
            {
                OnDeath();
            }
        }

        // Get the current health value.
        public float GetHP()
        {
            return health;
        }

    }
}
