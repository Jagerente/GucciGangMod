//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class Btn_CreateLanGame : MonoBehaviour
{
    private void OnClick()
    {
        PhotonNetwork.Disconnect();
        print("IP:" + Network.player.ipAddress + Network.player.externalIP);
        PhotonNetwork.ConnectToMaster(Network.player.ipAddress, 5055, FengGameManagerMKII.applicationId, UIMainReferences.version);
    }

    private void Start()
    {
        Destroy(gameObject);
    }
}

