//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using ExitGames.Client.Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

internal class PhotonHandler : Photon.MonoBehaviour, IPhotonPeerListener
{
    public static bool AppQuits;
    internal static CloudRegionCode BestRegionCodeCurrently = CloudRegionCode.none;
    private int nextSendTickCount;
    private int nextSendTickCountOnSerialize;
    public static Type PingImplementation;
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
            UnityEngine.Debug.LogError(message);
        }
        else if (level == DebugLevel.WARNING)
        {
            UnityEngine.Debug.LogWarning(message);
        }
        else if (level == DebugLevel.INFO && PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
        {
            UnityEngine.Debug.Log(message);
        }
        else if (level == DebugLevel.ALL && PhotonNetwork.logLevel == PhotonLogLevel.Full)
        {
            UnityEngine.Debug.Log(message);
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

    [DebuggerHidden]
    internal IEnumerator PingAvailableRegionsCoroutine(bool connectToBest)
    {
        return new PingAvailableRegionsCoroutinecIteratorA { connectToBest = connectToBest, SconnectToBest = connectToBest };
    }

    public static void StartFallbackSendAckThread()
    {
        if (!sendThreadShouldRun)
        {
            sendThreadShouldRun = true;
            SupportClass.CallInBackground(new Func<bool>(FallbackSendAckThread));
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
            UnityEngine.Debug.LogError("NetworkPeer broke!");
        }
        else if (PhotonNetwork.connectionStateDetailed != PeerStates.PeerCreated && PhotonNetwork.connectionStateDetailed != PeerStates.Disconnected && !PhotonNetwork.offlineMode && PhotonNetwork.isMessageQueueRunning)
        {
            for (var flag = true; PhotonNetwork.isMessageQueueRunning && flag; flag = PhotonNetwork.networkingPeer.DispatchIncomingCommands())
            {
            }
            var num = (int) (Time.realtimeSinceStartup * 1000f);
            if (PhotonNetwork.isMessageQueueRunning && num > nextSendTickCountOnSerialize)
            {
                PhotonNetwork.networkingPeer.RunViewUpdate();
                nextSendTickCountOnSerialize = num + updateIntervalOnSerialize;
                nextSendTickCount = 0;
            }
            num = (int) (Time.realtimeSinceStartup * 1000f);
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

    [CompilerGenerated]
    private sealed class PingAvailableRegionsCoroutinecIteratorA : IEnumerator, IDisposable, IEnumerator<object>
    {
        internal object Scurrent;
        internal int SPC;
        internal bool SconnectToBest;
        internal List<Region>.Enumerator Ss_891;
        internal Region best3;
        internal PhotonPingManager pingManager0;
        internal Region region2;
        internal bool connectToBest;

        [DebuggerHidden]
        public void Dispose()
        {
            SPC = -1;
        }

        public bool MoveNext()
        {
            var num = (uint) SPC;
            SPC = -1;
            switch (num)
            {
                case 0:
                    BestRegionCodeCurrently = CloudRegionCode.none;
                    break;

                case 1:
                    break;

                case 2:
                    goto Label_01A4;

                default:
                    goto Label_0266;
            }
            if (PhotonNetwork.networkingPeer.AvailableRegions == null)
            {
                if (PhotonNetwork.connectionStateDetailed != PeerStates.ConnectingToNameServer && PhotonNetwork.connectionStateDetailed != PeerStates.ConnectedToNameServer)
                {
                    UnityEngine.Debug.LogError("Call ConnectToNameServer to ping available regions.");
                    goto Label_0266;
                }
                UnityEngine.Debug.Log(string.Concat("Waiting for AvailableRegions. State: ", PhotonNetwork.connectionStateDetailed, " Server: ", PhotonNetwork.Server, " PhotonNetwork.networkingPeer.AvailableRegions ", PhotonNetwork.networkingPeer.AvailableRegions != null));
                Scurrent = new WaitForSeconds(0.25f);
                SPC = 1;
                //goto Label_0268;
            }
            if (PhotonNetwork.networkingPeer.AvailableRegions == null || PhotonNetwork.networkingPeer.AvailableRegions.Count == 0)
            {
                UnityEngine.Debug.LogError("No regions available. Are you sure your appid is valid and setup?");
                goto Label_0266;
            }
            pingManager0 = new PhotonPingManager();
            Ss_891 = PhotonNetwork.networkingPeer.AvailableRegions.GetEnumerator();
            try
            {
                while (Ss_891.MoveNext())
                {
                    region2 = Ss_891.Current;
                    SP.StartCoroutine(pingManager0.PingSocket(region2));
                }
            }
            finally
            {
                Ss_891.Dispose();
            }
        Label_01A4:
            while (!pingManager0.Done)
            {
                Scurrent = new WaitForSeconds(0.1f);
                SPC = 2;
                //goto Label_0268;
            }
            best3 = pingManager0.BestRegion;
            BestRegionCodeCurrently = best3.Code;
            BestRegionCodeInPreferences = best3.Code;
            UnityEngine.Debug.Log(string.Concat("Found best region: ", best3.Code, " ping: ", best3.Ping, ". Calling ConnectToRegionMaster() is: ", connectToBest));
            if (connectToBest)
            {
                PhotonNetwork.networkingPeer.ConnectToRegionMaster(best3.Code);
            }
            SPC = -1;
        Label_0266:
            return false;
        Label_0268:
            return true;
        }

        [DebuggerHidden]
        public void Reset()
        {
            throw new NotSupportedException();
        }

        object IEnumerator<object>.Current
        {
            [DebuggerHidden]
            get
            {
                return Scurrent;
            }
        }

        object IEnumerator.Current
        {
            [DebuggerHidden]
            get
            {
                return Scurrent;
            }
        }
    }
}

