using ExitGames.Client.Photon;
using System.Collections;
using UnityEngine;
using MonoBehaviour = Photon.MonoBehaviour;

internal class PhotonHandler : MonoBehaviour, IPhotonPeerListener
{
    public static bool AppQuits;
    internal static CloudRegionCode BestRegionCodeCurrently = CloudRegionCode.none;
    private int nextSendTickCount;
    private int nextSendTickCountOnSerialize;
    private const string PlayerPrefsKey = "PUNCloudBestRegion";
    private static bool sendThreadShouldRun;
    public static PhotonHandler SP;
    public int updateInterval;
    public int updateIntervalOnSerialize;

    protected void Awake()
    {
        if (SP != null && SP != this && SP.gameObject != null)
        {
            DestroyImmediate(SP.gameObject);
        }
        SP = this;
        DontDestroyOnLoad(gameObject);
        updateInterval = 1000 / PhotonNetwork.sendRate;
        updateIntervalOnSerialize = 1000 / PhotonNetwork.sendRateOnSerialize;
        StartFallbackSendAckThread();
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        if (level == DebugLevel.ERROR)
        {
            Debug.LogError(message);
        }
        else if (level == DebugLevel.WARNING)
        {
            Debug.LogWarning(message);
        }
        else if (level == DebugLevel.INFO && PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
        {
            Debug.Log(message);
        }
        else if (level == DebugLevel.ALL && PhotonNetwork.logLevel == PhotonLogLevel.Full)
        {
            Debug.Log(message);
        }
    }

    public static bool FallbackSendAckThread()
    {
        if (sendThreadShouldRun && PhotonNetwork.networkingPeer != null)
        {
            PhotonNetwork.networkingPeer.SendAcksOnly();
        }
        return sendThreadShouldRun;
    }

    protected void OnApplicationQuit()
    {
        AppQuits = true;
        StopFallbackSendAckThread();
        PhotonNetwork.Disconnect();
    }

    protected void OnCreatedRoom()
    {
        PhotonNetwork.networkingPeer.SetLevelInPropsIfSynced(Application.loadedLevelName);
    }

    public void OnEvent(EventData photonEvent)
    {
    }

    protected void OnJoinedRoom()
    {
        PhotonNetwork.networkingPeer.LoadLevelIfSynced();
    }

    protected void OnLevelWasLoaded(int level)
    {
        PhotonNetwork.networkingPeer.NewSceneLoaded();
        PhotonNetwork.networkingPeer.SetLevelInPropsIfSynced(Application.loadedLevelName);
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
    }

    protected internal static void PingAvailableRegionsAndConnectToBest()
    {
        SP.StartCoroutine(SP.PingAvailableRegionsCoroutine(true));
    }

    internal IEnumerator PingAvailableRegionsCoroutine(bool connectToBest)
    {
        yield break;
    }

    public static void StartFallbackSendAckThread()
    {
        if (!sendThreadShouldRun)
        {
            sendThreadShouldRun = true;
            SupportClass.StartBackgroundCalls(FallbackSendAckThread);
        }
    }

    public static void StopFallbackSendAckThread()
    {
        sendThreadShouldRun = false;
    }

    protected void Update()
    {
        if (PhotonNetwork.networkingPeer == null)
        {
            Debug.LogError("NetworkPeer broke!");
        }
        else if (PhotonNetwork.connectionStateDetailed != PeerStates.PeerCreated && PhotonNetwork.connectionStateDetailed != PeerStates.Disconnected && !PhotonNetwork.offlineMode && PhotonNetwork.isMessageQueueRunning)
        {
            for (var flag = true; PhotonNetwork.isMessageQueueRunning && flag; flag = PhotonNetwork.networkingPeer.DispatchIncomingCommands())
            {
            }
            var num = (int)(Time.realtimeSinceStartup * 1000f);
            if (PhotonNetwork.isMessageQueueRunning && num > nextSendTickCountOnSerialize)
            {
                PhotonNetwork.networkingPeer.RunViewUpdate();
                nextSendTickCountOnSerialize = num + updateIntervalOnSerialize;
                nextSendTickCount = 0;
            }
            num = (int)(Time.realtimeSinceStartup * 1000f);
            if (num > nextSendTickCount)
            {
                for (var flag2 = true; PhotonNetwork.isMessageQueueRunning && flag2; flag2 = PhotonNetwork.networkingPeer.SendOutgoingCommands())
                {
                }
                nextSendTickCount = num + updateInterval;
            }
        }
    }

    internal static CloudRegionCode BestRegionCodeInPreferences
    {
        get
        {
            var str = PlayerPrefs.GetString("PUNCloudBestRegion", string.Empty);
            if (!string.IsNullOrEmpty(str))
            {
                return Region.Parse(str);
            }
            return CloudRegionCode.none;
        }
        set
        {
            if (value == CloudRegionCode.none)
            {
                PlayerPrefs.DeleteKey("PUNCloudBestRegion");
            }
            else
            {
                PlayerPrefs.SetString("PUNCloudBestRegion", value.ToString());
            }
        }
    }
}