using UnityEngine;

namespace CannonGame
{
    /*  PlayerMoveInput Class
     * 
     *  Reads in Unity's Input API to control the Player's movement.
     */
    public class PlayerMoveInput : IMoveInput
    {
        // Read from Unity's Input system.
        public void ReadInput()
        {
            HorizontalInput = Input.GetAxisRaw("Horizontal");
            VerticalInput = Input.GetAxisRaw("Vertical");
        }

        // The horizontal input value.
        public float HorizontalInput { get; private set; }

        // The vertical input value.
        public float VerticalInput { get; private set; }


    }
}
