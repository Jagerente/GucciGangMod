﻿using UnityEngine;

public class Btn_CreateLanGame : MonoBehaviour
{
    private void OnClick()
    {
        PhotonNetwork.Disconnect();
        print("IP:" + Network.player.ipAddress + Network.player.externalIP);
        PhotonNetwork.ConnectToMaster(Network.player.ipAddress, 5055, FengGameManagerMKII.applicationId, UIMainReferences.ServerKey);
    }

    private void Start()
    {
        Destroy(gameObject);
    }
}

