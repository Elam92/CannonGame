using UnityEngine;

namespace CannonGame {
    /*  TargetMarker Class
     * 
     *  The cursor of where the projectile will land.
     */ 
    public class TargetMarker : MonoBehaviour {

        // How fast the cursor moves.
        public float speed = 50f;

        // The movement input controls.
        private IMoveInput inputMove;
        // The component that moves the cursor itself.
        private IMoveable movement;

        // Use this for initialization
        void Start() {
            inputMove = new PlayerMoveInput();
        }

        // Update is called once per frame
        void Update() {
            // We need the movement set up first before reading in inputs and moving the cursor.
            if (movement != null) {
                inputMove.ReadInput();
                movement.Move(inputMove.HorizontalInput, inputMove.VerticalInput);
            }
        }

        // Set the origin of where the player is and its movement range limit.
        public void SetOrigin(Vector3 newOrigin, float newRangeLimit)
        {
            movement = new TargetMovement(transform, newOrigin, speed, newRangeLimit);
        }
    }
}
