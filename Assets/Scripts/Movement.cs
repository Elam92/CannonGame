using UnityEngine;

namespace CannonGame {
    /*  Movement Class
     * 
     *  Basic movement behaviour. Moves along the X-Z axis.
     */
    public class Movement : IMoveable {

        // How fast the object should be moving.
        private readonly float speed;

        // Its Transform component to move itself.
        private Transform self;

        // Unity Wrapper to get time.
        private IUnityTime time;

        public Movement(Transform mover, float newSpeed)
        {
            self = mover;
            speed = newSpeed;
            time = new UnityDeltaTime();
        }

        // Moves the object on the X and Z axis.
        public void Move(float horizontal, float vertical)
        {
            float x = horizontal * speed * time.GetTime();
            float z = vertical * speed * time.GetTime();

            Vector3 newPosition = new Vector3(x, 0, z);
            self.position += newPosition;
        }
    }
}
