using UnityEngine;
using UnityEngine.EventSystems;

namespace CannonGame
{
    /*  Player Class
     * 
     *  Player controls a cannon that can shoot out different projectiles towards a target marker
     *  that the player moves around.
     */ 
    [RequireComponent(typeof(Health))]
    public class Player : MonoBehaviour
    {
        // Radius of the shootable area.
        public float radius = 100f;

        // Its UI Health Bar.
        public HealthBar hpBar;

        // Button Input for firing projectiles.
        public IButtonDownInput inputAction;

        // The target marker that the Player moves.
        public Transform target;

        // Its 3D model to rotate towards the cursor/target marker.
        public Transform model;

        // The Player's health.
        private Health health;

        // The Player's ammo selection.
        private AmmoLoader ammoLoader;

        // The Player's weapon to shoot projectiles from.
        private Launcher launcher;

        void Awake()
        {
            health = GetComponent<Health>();
            ammoLoader = GetComponent<AmmoLoader>();
            launcher = GetComponentInChildren<Launcher>();

            inputAction = new PlayerButtonDownInput();
        }

        // Use this for initialization
        void Start()
        {
            // UI HP Bar will listen to any health changes that happens to the Player.
            health.OnHealthChange += hpBar.UpdateHealthBar;

            // Set up the target.
            target.GetComponent<TargetMarker>().SetOrigin(transform.position, radius);

        }

        // Update is called once per frame
        void Update()
        {
            // Control for launcher.
            if (!EventSystem.current.IsPointerOverGameObject()) {
                if (inputAction.GetButtonDown("Fire1"))
                {
                    // Get the active projectile type and launch it at the cursor!
                    Projectile projectile = ammoLoader.UseType(launcher.launchPoint.position, Quaternion.identity);
                    launcher.Launch(projectile, target.position);
                }
            }

            // Display a visual arc of where the projectile will land.
            launcher.ShowDestination(target.position);


            // Rotate Model to look at new target position.
            model.rotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
        }

        // Unsubscribe from the event.
        private void OnDestroy()
        {
            health.OnHealthChange -= hpBar.UpdateHealthBar;
        }

        // Return Player's current health.
        public Health GetHealth()
        {
            return health;
        }

    }
}
