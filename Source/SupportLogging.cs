//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System.Text;
using UnityEngine;

public class SupportLogging : MonoBehaviour
{
    public bool LogTrafficStats;

    private void LogBasics()
    {
        var builder = new StringBuilder();
        builder.AppendFormat("SupportLogger Info: PUN {0}: ", "1.28");
        builder.AppendFormat("AppID: {0}*** GameVersion: {1} ", PhotonNetwork.networkingPeer.mAppId.Substring(0, 8), PhotonNetwork.networkingPeer.mAppVersionPun);
        builder.AppendFormat("Server: {0}. Region: {1} ", PhotonNetwork.ServerAddress, PhotonNetwork.networkingPeer.CloudRegion);
        builder.AppendFormat("HostType: {0} ", PhotonNetwork.PhotonServerSettings.HostType);
        Debug.Log(builder.ToString());
    }

    public void LogStats()
    {
        if (LogTrafficStats)
        {
            Debug.Log("SupportLogger " + PhotonNetwork.NetworkStatisticsToString());
        }
    }

    public void OnApplicationQuit()
    {
        CancelInvoke();
    }

    public void OnConnectedToPhoton()
    {
        Debug.Log("SupportLogger OnConnectedToPhoton().");
        LogBasics();
        if (LogTrafficStats)
        {
            PhotonNetwork.NetworkStatisticsEnabled = true;
        }
    }

    public void OnCreatedRoom()
    {
        Debug.Log(string.Concat("SupportLogger OnCreatedRoom(", PhotonNetwork.room, "). ", PhotonNetwork.lobby));
    }

    public void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.Log("SupportLogger OnFailedToConnectToPhoton(" + cause + ").");
        LogBasics();
    }

    public void OnJoinedLobby()
    {
        Debug.Log("SupportLogger OnJoinedLobby(" + PhotonNetwork.lobby + ").");
    }

    public void OnJoinedRoom()
    {
        Debug.Log(string.Concat("SupportLogger OnJoinedRoom(", PhotonNetwork.room, "). ", PhotonNetwork.lobby));
    }

    public void OnLeftRoom()
    {
        Debug.Log("SupportLogger OnLeftRoom().");
    }

    public void Start()
    {
        if (LogTrafficStats)
        {
            InvokeRepeating("LogStats", 10f, 10f);
        }
    }
}

