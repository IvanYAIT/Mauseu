using Photon.Pun;
using TradeMarket.Characrer;
using UnityEngine;

public class PlayersReadyCheck : InteractableItemComponentBase
{
    [SerializeField] private GameObject readinessPanel;
    [SerializeField] private string sceneName;

    private ReadinessPanel _readiness;
    private int interacts;
    private ExitGames.Client.Photon.Hashtable properties;

    void Start()
    {
        _readiness = readinessPanel.GetComponent<ReadinessPanel>();
        properties = new ExitGames.Client.Photon.Hashtable();
        properties["Readies"] = 0; 
        PhotonNetwork.CurrentRoom.SetCustomProperties(properties);
    }

    public override void Interract()
    {
    }

    public void Interract(Ready ready)
    {
        interacts = (int)PhotonNetwork.CurrentRoom.CustomProperties["Readies"];

        if (!ready.value)
        {
            if (interacts == 0)
            {
                _readiness.Init(sceneName);
            }
            _readiness.Ready();
            ready.value = true;
            interacts++;
            properties["Readies"] = interacts;
            PhotonNetwork.CurrentRoom.SetCustomProperties(properties);
        }
        else
        {
            _readiness.Unready();
            ready.value = false;
            interacts--;
            properties["Readies"] = interacts;
            PhotonNetwork.CurrentRoom.SetCustomProperties(properties);
            if (interacts == 0)
            {
                _readiness.Init(sceneName);
            }
        }
    }
}
