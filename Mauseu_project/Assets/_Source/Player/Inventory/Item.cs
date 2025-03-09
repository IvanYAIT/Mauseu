using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerInventory
{
    public class Item : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void DisableItem() =>
            GetComponent<PhotonView>().RPC("Disable", RpcTarget.AllBuffered);

        [PunRPC]
        private void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}