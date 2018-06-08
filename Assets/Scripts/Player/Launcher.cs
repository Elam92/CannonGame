using UnityEngine;

namespace CannonGame
{
    /*  Launcher Class
     * 
     *  Launches different projectiles given to it in an arc.
     */
    [RequireComponent(typeof(AmmoLoader))]
    [RequireComponent(typeof(ArcRenderer))]
    public class Launcher : MonoBehaviour
    {
        // The origin point of where the projectiles start from.
        public Transform launchPoint;

        // The height of the arc we want.
        public float h = 25f;

        // The gravity that will affect the projectile's acceleration.
        public float gravity = -18f;

        // The Player's ammo selection.
        private AmmoLoader ammoLoader;

        // Draws a visible arc in the game.
        private ArcRenderer arcRenderer;

        private void Awake()
        {
            ammoLoader = GetComponent<AmmoLoader>();
            arcRenderer = GetComponent<ArcRenderer>();
        }

        // Use this for initialization
        void Start()
        {
            // Set physics up with our gravity so that projectiles fall faster.
            Physics.gravity = Vector3.up * gravity;
        }

        // Show the location of where our projectiles will go to.
        public void ShowDestination(Vector3 position)
        {
            LaunchData trajectoryInfo = CalculateLaunchData(launchPoint.position, position);
            arcRenderer.DrawArc(launchPoint.position, position, trajectoryInfo.initialVelocity, trajectoryInfo.timeToTarget, gravity);
        }

        // Launches the given projectile at a target position.
        public void Launch(Vector3 targetPosition)
        {
            // Get the active projectile type and set its starting position.
            Projectile projectile = ammoLoader.UseType(launchPoint.position, Quaternion.identity);

            // Calculate and pass the direction speed to move the projectile.
            Vector3 velocity = CalculateLaunchData(launchPoint.position, targetPosition).initialVelocity;
            projectile.Launch(velocity);
        }

        // Calculates the initial speed and the time to reach it from an origin point to the target point.
        private LaunchData CalculateLaunchData(Vector3 from, Vector3 to)
        {

            float displaceY = to.y - from.y;
            Vector3 displacementXZ = new Vector3(to.x - from.x, 0, to.z - from.z);

            // Calculate the time to get up.
            float timeUp = Mathf.Sqrt(-2 * h / gravity);
            // Calculate the time to get down.
            float timeDown = Mathf.Sqrt(2 * (displaceY - h) / gravity);
            float time = timeUp + timeDown;

            // Initial velocity on Y-Axis to get to end position.
            Vector3 initialVelocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
            // Initial velocity on X-Axis to get to end position.
            Vector3 initialVelocityXZ = displacementXZ / time;

            return new LaunchData(initialVelocityXZ + initialVelocityY * -Mathf.Sign(gravity), time);
        }

        // Data that holds the initial speed and the time it takes to get to a position.
        struct LaunchData
        {
            public readonly Vector3 initialVelocity;
            public readonly float timeToTarget;

            public LaunchData(Vector3 initialVelocity, float timeToTarget)
            {
                this.initialVelocity = initialVelocity;
                this.timeToTarget = timeToTarget;
            }
        }
    }
}
