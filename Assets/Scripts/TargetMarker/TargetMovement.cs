using UnityEngine;

namespace CannonGame
{
    /*  TargetMovement Class
     * 
     *  Moves the Player's target marker around the Player.
     */
    public class TargetMovement : IMoveable
    {
        // How fast the cursor/target marker moves.
        private float speed;

        // The maximum range from the Player that the cursor can move.
        private float rangeLimit;

        // Its Transfrom component.
        private Transform self;

        // The Player's position.
        private Vector3 origin;

        // Unity Wrapper for its Time API.
        private IUnityTime time;

        // Used for checking if the player reached the maximum range or not from the Player position.
        private float rangeCheck;

        public TargetMovement(Transform mover, Vector3 newOrigin, float newSpeed, float newRangeLimit, IUnityTime timeInformation)
        {
            self = mover;
            origin = newOrigin;
            speed = newSpeed;
            rangeLimit = newRangeLimit;

            time = timeInformation;

            // To avoid square rooting when checking for distance.
            rangeCheck = newRangeLimit * newRangeLimit;

        }

        // Moves the target marker around the Player, restricted by a maximum range.
        public void Move(float horizontal, float vertical)
        {
            float x = horizontal * speed * time.GetTime();
            float z = vertical * speed * time.GetTime();

            Vector3 newTargetLocation = self.position + new Vector3(x, 0, z);

            // Get distance from center to target.
            Vector3 toTarget = newTargetLocation - origin;

            float distance = toTarget.sqrMagnitude;
            // If distance is greater than the range limit, then keep it within bounds.
            if (distance > rangeCheck)
            {
                newTargetLocation = origin + (toTarget.normalized * rangeLimit);
            }

            self.position = newTargetLocation;
        }
    }
}
