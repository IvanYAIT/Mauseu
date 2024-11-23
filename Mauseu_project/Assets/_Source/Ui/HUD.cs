using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenuUI
{
    public class HUD : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void OpenInventory()
        {
            
        }
        
        public void OpenTradeMarket()
        {
            
        }
        
        private void Exit()
        {
            Application.Quit();
        }
    }
}