using Photon.Pun;
using UnityEngine;
using Services.Input;
using Dependencies.ChaserLib.ServiceLocator;
using UnityEngine.XR;

public class PlayerSpawnController : MonoBehaviourPunCallbacks
{
    private const string PLAYER_PREFAB_NAME = "FirstPersonController";
    private static ServiceLocator Locator => ServiceLocator.Instance;

    [SerializeField] private Transform[] spawnPoints;

    private void Awake()
    {
        GameObject spawnedPlayer = PhotonNetwork.Instantiate(PLAYER_PREFAB_NAME, GetNextSpawnpoint(), Quaternion.identity);
        IInputService inputService = null;
        try
        {
            inputService = Locator.Get<IInputService>();
        }
        catch
        {
            inputService = new InputService();
            Locator.Add<IInputService>(inputService);
        }

        spawnedPlayer.GetComponent<PhotonView>().RPC("SetNickname", RpcTarget.AllBuffered, PhotonNetwork.NickName);
        spawnedPlayer.GetComponent<PlayerSetup>().Init(inputService);
    }

    private Vector3 GetNextSpawnpoint()
    {
        Vector3 spawnPoint;
        if (PhotonNetwork.CurrentRoom.PlayerCount < spawnPoints.Length)
            spawnPoint = spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount].position;
        else
            spawnPoint = spawnPoints[0].position;

        return spawnPoint;
    }
}
