using Photon.Pun;
using Player;
using PlayerInventory;
using Services.Input;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] private Services.Character.CharacterController controller;
    [SerializeField] private Nickname nickname;
    [SerializeField] private PickUpSystem pickUpSystem;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private GameObject nicknameText;
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Gun gun;

    public void Init(IInputService inputService)
    {
        controller.Init(inputService);
        controller.IsOwner = true;
        pickUpSystem.IsOwner = true;
        healthSystem.IsOwner = true;
        gun.IsOwner = true;
        nickname.SetNickname(PhotonNetwork.NickName);
        camera.SetActive(true);
        canvas.SetActive(true);
        nicknameText.SetActive(false);
    }
}
