using Photon.Pun;
using Services.Input;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] private Services.Character.CharacterController controller;
    [SerializeField] private Nickname nickname;
    [SerializeField] private GameObject nicknameText;
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject canvas;

    public void Init(IInputService inputService)
    {
        controller.Init(inputService);
        controller.IsOwner = true;
        nickname.SetNickname(PhotonNetwork.NickName);
        camera.SetActive(true);
        canvas.SetActive(true);
        nicknameText.SetActive(false);
    }
}
