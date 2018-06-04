using UnityEngine;

namespace CannonGame
{
    /*  AIMoveInput Class
     * 
     *  AI Inputs to move itself. Requires a target to move
     *  towards.
     */
    public class AIMoveInput : IMoveInput
    {
        // Its Transform component to find direction.
        private Transform self;

        // The target to move towards.
        private Transform target;

        public AIMoveInput(Transform newMover, Transform newTarget)
        {
            self = newMover;
            target = newTarget;
        }

        // Find direction and its unit vector to move towards the target.
        public void ReadInput()
        {
            if (target != null)
            {
                Vector3 direction = (target.position - self.position).normalized;
                HorizontalInput = direction.x;
                VerticalInput = direction.z;
            }
        }

        // The X-value to move towards the target.
        public float HorizontalInput { get; private set; }

        // The Z-value to move towards the target.
        public float VerticalInput { get; private set; }

    }
}
