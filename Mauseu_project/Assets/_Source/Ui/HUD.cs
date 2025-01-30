using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenuUI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private string _nextSceneName;
        
        public void Play()
        {
            SceneManager.LoadScene(_nextSceneName);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}