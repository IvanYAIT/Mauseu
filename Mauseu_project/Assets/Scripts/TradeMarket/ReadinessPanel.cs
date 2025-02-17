using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;

public class ReadinessPanel : MonoBehaviour
{
    [SerializeField] private GameObject playerIconPrefab;
    [SerializeField] private Transform iconsGrid;
    [SerializeField] private TextMeshProUGUI timerText;

    private float _timer;
    private bool isStart;
    private int _readyCount;
    private string _sceneName;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (isStart)
        {
            _timer -= Time.deltaTime;
            if ( _timer > 0)
            {
                int minutes = Mathf.FloorToInt(_timer / 60);
                int seconds = Mathf.FloorToInt(_timer - (minutes * 60));
                timerText.text = $"{minutes}:{seconds:D2}";
            }
            else
            {
                PhotonNetwork.LoadLevel(_sceneName);
            }
            if (_readyCount >= PhotonNetwork.CurrentRoom.PlayerCount)
            {
                isStart = false;
                PhotonNetwork.LoadLevel(_sceneName);
            }
        }
        
    }

    public void Init(string sceneName)
    {
        GetComponent<PhotonView>().RPC("ShowPanel", RpcTarget.AllBuffered, sceneName);
    }

    public void Ready()
    {
        GetComponent<PhotonView>().RPC("ReadyRPC", RpcTarget.All);
    }

    [PunRPC]
    private void ReadyRPC()
    {
        Color color = iconsGrid.GetChild(_readyCount).GetComponent<Image>().color;
        color.a = 1f;
        iconsGrid.GetChild(_readyCount).GetComponent<Image>().color = color;
        _readyCount++;
    }

    public void Unready()
    {
        GetComponent<PhotonView>().RPC("UnreadyRPC", RpcTarget.All);
    }

    [PunRPC]
    private void UnreadyRPC()
    {
        _readyCount--;
        Color color = iconsGrid.GetChild(_readyCount).GetComponent<Image>().color;
        color.a = 0.6f;
        iconsGrid.GetChild(_readyCount).GetComponent<Image>().color = color;
    }

    public void UpdatePanel()
    {
        DestroyIcons();
        CreateIcons();
    }

    private void CreateIcons()
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            GameObject icon = Instantiate(playerIconPrefab, iconsGrid);
            Color iconColor = icon.GetComponent<Image>().color;
            iconColor.a = 0.6f;
            icon.GetComponent<Image>().color = iconColor;
        }
    }

    private void DestroyIcons()
    {
        for (int i = 0; i < iconsGrid.childCount; i++)
        {
            Destroy(iconsGrid.GetChild(i).gameObject);
        }
    }

    [PunRPC]
    public void ShowPanel(string sceneName)
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
        isStart = gameObject.activeInHierarchy;
        _sceneName = sceneName;
        _timer = 300f;
        UpdatePanel();
    }
}
