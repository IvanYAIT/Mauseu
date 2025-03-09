using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    private const string HUB_SCENE_NAME = "Hub";

    [SerializeField] private Button playBtn;
    [SerializeField] private GameObject connectingPanel;
    [SerializeField] private TMP_InputField nicknameInput;

    void Start()
    {
        playBtn.onClick.AddListener(Connect);
        nicknameInput.onEndEdit.AddListener(delegate { SetNickname(); });
    }

    private void Connect()
    {
        connectingPanel.SetActive(true);
        PhotonNetwork.ConnectUsingSettings();
    }

    private void SetNickname()
    {
        string newNickname = nicknameInput.text;
        if (newNickname.Length > 1)
            PhotonNetwork.NickName = newNickname;
        else
            PhotonNetwork.NickName = "Player" + Random.Range(100, 999);

        Debug.Log($"Nickname set to {PhotonNetwork.NickName}");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon!");
        SetNickname();
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No rooms found, creating a new one...");
        PhotonNetwork.CreateRoom(null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room!");
        //if(PhotonNetwork.IsMasterClient)
        PhotonNetwork.LoadLevel(HUB_SCENE_NAME);
    }
}