using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenuUI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button playBtn;
        [SerializeField] private Button exitBtn;

        private void Awake()
        {
            playBtn.onClick.AddListener(Play);
            exitBtn.onClick.AddListener(Exit);
        }

        private void OnDestroy()
        {
            playBtn.onClick.RemoveListener(Play);
            exitBtn.onClick.RemoveListener(Exit);
        }

        public void Play() =>
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        private void Exit()=>
            Application.Quit();
    }
}