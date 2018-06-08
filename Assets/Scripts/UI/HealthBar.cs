using UnityEngine;
using UnityEngine.UI;

namespace CannonGame
{
    /*  HealthBar Class
     * 
     *  Updates a UI Component that keeps track of an object's health.
     */ 
    public class HealthBar : MonoBehaviour
    {
        // Visual representation of an object's current health.
        private Image healthBar;

        private void Awake()
        {
            healthBar = GetComponent<Image>();
        }

        // Update health bar to show an object's current health.
        public void UpdateHealthBar(float newHp)
        {
            if(newHp < 0f)
            {
                newHp = 0f;
            }
            if(newHp > 1f)
            {
                newHp = 1f;
            }

            healthBar.fillAmount = newHp;
        }
    }
}
