using UnityEngine;
using TMPro;
using Photon.Pun;

public class Nickname : MonoBehaviour
{
    [SerializeField] private TextMeshPro nickText;

    [PunRPC]
    public void SetNickname(string name)
    {
        nickText.text = name;
    }
}
