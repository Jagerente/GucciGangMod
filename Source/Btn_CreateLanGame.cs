﻿using UnityEngine;

public class Btn_CreateLanGame : MonoBehaviour
{
    private void OnClick()
    {
        PhotonNetwork.Disconnect();
        print("IP:" + Network.player.ipAddress + Network.player.externalIP);
        PhotonNetwork.ConnectToMaster(Network.player.ipAddress, 0x13bf, FengGameManagerMKII.applicationId, UIMainReferences.version);
    }

    private void Start()
    {
        Destroy(gameObject);
    }
}

