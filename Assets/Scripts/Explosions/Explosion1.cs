using UnityEngine;

namespace CannonGame
{
    /*  Explosion1 Class
     * 
     *  A type of explosion.
     */
    [RequireComponent(typeof(ParticleSystem))]
    public class Explosion1 : PooledObject
    {
        // Its life time.
        public float startLifeTime = 1f;

        // Its current life time before deactivating.
        private float lifeTime;

        // Unity Wrapper for getting time.
        private IUnityTime unityTime;

        // The explosion particles.
        private ParticleSystem particles;

        // Reset its values and be placed in a new location.
        public override void Reset(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
            gameObject.SetActive(true);
            lifeTime = startLifeTime;
            particles.Clear();
            particles.Play();
        }

        // Use this for initialization
        void Awake()
        {
            unityTime = new UnityDeltaTime();
            particles = GetComponent<ParticleSystem>();
        }

        // Deactivate the explosion when its reached its lifetime.
        void Update()
        {
            lifeTime -= unityTime.GetTime();

            if (lifeTime < 0)
            {
                Deactivate();
            }
        }
    }
}
