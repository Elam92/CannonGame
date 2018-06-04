using UnityEngine;
using UnityEngine.UI;

namespace CannonGame
{
    /*  GameOverUI Class
     * 
     *  Displays at the end of the game/when Player dies and shows the Player's
     *  total score.
     */
    public class GameOverUI : MonoBehaviour
    {

        // The gameplay UI.
        public GameObject gameplayUI;

        // UI Component to show total score.
        public Text scoreFinalDisplay;

        // The object that keeps track of all the scores earned.
        public ScoreManager scoreManager;


        // Turn on GameOverUI and show Player's total score.
        public void Display()
        {
            gameplayUI.SetActive(false);

            scoreFinalDisplay.text = "You got a score of: " + scoreManager.GetScore().ToString() + "!";

            gameObject.SetActive(true);
        }
    }
}
