using UnityEngine;

namespace CannonGame
{
    /*  Quit Class
     * 
     *  Gives the functionality to quit the game.
     */ 
    public class Quit : MonoBehaviour
    {
        // Exit the game.
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
