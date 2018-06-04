using UnityEngine;

namespace CannonGame
{
    /*  PlayerButtonDownInput Class
     * 
     *  Wrapper class for Unity's Input System.
     */ 
    public class PlayerButtonDownInput : IButtonDownInput
    {
        // Find if a button was pressed or not.
        public bool GetButtonDown(string buttonName)
        {
            return Input.GetButtonDown(buttonName);
        }
    }
}
