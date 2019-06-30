using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;

[Serializable]
public class ServerSettings : ScriptableObject
{
    public string AppID = string.Empty;
    [HideInInspector]
    public bool DisableAutoOpenWizard;
    public HostingOption HostType;
    public bool PingCloudServersOnAwake;
    public CloudRegionCode PreferredRegion;
    public ConnectionProtocol Protocol;
    public List<string> RpcList = new List<string>();
    public string ServerAddress = string.Empty;
    public int ServerPort = 5055;

    public override string ToString()
    {
        object[] objArray1 = { "ServerSettings: ", HostType, " ", ServerAddress };
        return string.Concat(objArray1);
    }

    public void UseCloud(string cloudAppid)
    {
        HostType = HostingOption.PhotonCloud;
        AppID = cloudAppid;
    }

    public void UseCloud(string cloudAppid, CloudRegionCode code)
    {
        HostType = HostingOption.PhotonCloud;
        AppID = cloudAppid;
        PreferredRegion = code;
    }

    public void UseCloudBestResion(string cloudAppid)
    {
        HostType = HostingOption.BestRegion;
        AppID = cloudAppid;
    }

    public void UseMyServer(string serverAddress, int serverPort, string application)
    {
        HostType = HostingOption.SelfHosted;
        AppID = application == null ? "master" : application;
        ServerAddress = serverAddress;
        ServerPort = serverPort;
    }

    public enum HostingOption
    {
        NotSet,
        PhotonCloud,
        SelfHosted,
        OfflineMode,
        BestRegion
    }
}

