using UnityEngine;

public class BTN_Server_JPN : MonoBehaviour
{
    private void OnClick()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.ConnectToMaster("app-jp.exitgamescloud.com", 0x13bf, FengGameManagerMKII.applicationId, UIMainReferences.ServerKey);
        FengGameManagerMKII.OnPrivateServer = false;
    }
}

