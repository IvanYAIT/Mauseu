using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Button graphicsTabButton;
    public Button audioTabButton;
    public Button controlsTabButton;
    public Button languageTabButton;
    public Button interfaceTabButton;

    public GameObject graphicsContentPanel;
    public GameObject audioContentPanel;
    public GameObject controlsContentPanel;
    public GameObject languageContentPanel;
    public GameObject interfaceContentPanel;

    void Start()
    {
        graphicsTabButton.onClick.AddListener(() => {SwitchToTab(graphicsContentPanel); graphicsTabButton.interactable = false; });
        audioTabButton.onClick.AddListener(() => {SwitchToTab(audioContentPanel); audioTabButton.interactable = false; });
        controlsTabButton.onClick.AddListener(() => {SwitchToTab(controlsContentPanel); controlsTabButton.interactable = false; });
        languageTabButton.onClick.AddListener(() => { SwitchToTab(languageContentPanel); languageTabButton.interactable = false; });
        interfaceTabButton.onClick.AddListener(() => { SwitchToTab(interfaceContentPanel); interfaceTabButton.interactable = false;});
    }

    private void OnDestroy()
    {
        graphicsTabButton.onClick.RemoveAllListeners();
        audioTabButton.onClick.RemoveAllListeners();
        controlsTabButton.onClick.RemoveAllListeners();
        languageTabButton.onClick.RemoveAllListeners();
        interfaceTabButton.onClick.RemoveAllListeners();
    }

    void SwitchToTab(GameObject contentPanel)
    {
        graphicsContentPanel.SetActive(false);
        audioContentPanel.SetActive(false);
        controlsContentPanel.SetActive(false);
        languageContentPanel.SetActive(false);
        interfaceContentPanel.SetActive(false);

        graphicsTabButton.interactable = true;
        audioTabButton.interactable = true;
        controlsTabButton.interactable = true;
        languageTabButton.interactable = true;
        interfaceTabButton.interactable = true;

        contentPanel.SetActive(true);
    }
}
