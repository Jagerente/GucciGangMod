using UnityEngine;

public class BTN_ServerUS : MonoBehaviour
{
    private void OnClick()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.ConnectToMaster("app-us.exitgamescloud.com", NetworkingPeer.ProtocolToNameServerPort[PhotonNetwork.networkingPeer.UsedProtocol], FengGameManagerMKII.applicationId, UIMainReferences.ServerKey);
        FengGameManagerMKII.OnPrivateServer = false;
    }
}