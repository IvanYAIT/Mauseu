using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button quitBtn;
    [SerializeField] private GameObject pausePanel;

    void Start()
    {
        continueBtn.onClick.AddListener(Continue);
        settingsBtn.onClick.AddListener(ShowSettings);
        quitBtn.onClick.AddListener(QuitToMainMnu);
    }

    private void Update()
    {
        //if(Input.GetKey(KeyCode.Escape))
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (!pausePanel.activeInHierarchy)
            {
                Time.timeScale = 0;
                pausePanel.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            } else
            {
                Time.timeScale = 1;
                pausePanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            
        }
    }

    private void OnDestroy()
    {
        continueBtn.onClick.RemoveListener(Continue);
        settingsBtn.onClick.RemoveListener(ShowSettings);
        quitBtn.onClick.RemoveListener(QuitToMainMnu);
    }

    private void Continue()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void ShowSettings() { }

    private void QuitToMainMnu()
    {
        SceneManager.LoadScene(0);
    }
}
