using UnityEngine;

public class BTN_Server_ASIA : MonoBehaviour
{
    private void OnClick()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.ConnectToMaster("app-asia.exitgamescloud.com", NetworkingPeer.ProtocolToNameServerPort[PhotonNetwork.networkingPeer.UsedProtocol], FengGameManagerMKII.applicationId, UIMainReferences.ServerKey);
        FengGameManagerMKII.OnPrivateServer = false;
    }
}