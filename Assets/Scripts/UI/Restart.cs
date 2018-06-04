using UnityEngine;
using UnityEngine.SceneManagement;

namespace CannonGame
{
    /* Restart Class
     * 
     * Gives the functionality to restart the level.
     */
    public class Restart : MonoBehaviour
    {
        // Reloads the current level.
        public void RestartLevel()
        {
            // Make sure we reset the time.
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
