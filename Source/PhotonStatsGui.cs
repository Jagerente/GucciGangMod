using UnityEngine;

public class PhotonStatsGui : MonoBehaviour
{
    public bool buttonsOn;
    public bool healthStatsVisible;
    public bool statsOn = true;
    public Rect statsRect = new Rect(0f, 100f, 200f, 50f);
    public bool statsWindowOn = true;
    public bool trafficStatsOn;
    public int WindowId = 100;

    public void OnGUI()
    {
        if (PhotonNetwork.networkingPeer.TrafficStatsEnabled != statsOn)
        {
            PhotonNetwork.networkingPeer.TrafficStatsEnabled = statsOn;
        }
        if (statsWindowOn)
        {
            statsRect = GUILayout.Window(WindowId, statsRect, TrafficStatsWindow, "Messages (shift+tab)");
        }
    }

    public void Start()
    {
        statsRect.x = Screen.width - statsRect.width;
    }

    public void TrafficStatsWindow(int windowID)
    {
        var flag = false;
        var trafficStatsGameLevel = PhotonNetwork.networkingPeer.TrafficStatsGameLevel;
        var num = PhotonNetwork.networkingPeer.TrafficStatsElapsedMs / 0x3e8L;
        if (num == 0)
        {
            num = 1L;
        }
        GUILayout.BeginHorizontal();
        buttonsOn = GUILayout.Toggle(buttonsOn, "buttons");
        healthStatsVisible = GUILayout.Toggle(healthStatsVisible, "health");
        trafficStatsOn = GUILayout.Toggle(trafficStatsOn, "traffic");
        GUILayout.EndHorizontal();
        var text = string.Format("Out|In|Sum:\t{0,4} | {1,4} | {2,4}", trafficStatsGameLevel.TotalOutgoingMessageCount, trafficStatsGameLevel.TotalIncomingMessageCount, trafficStatsGameLevel.TotalMessageCount);
        var str2 = string.Format("{0}sec average:", num);
        var str3 = string.Format("Out|In|Sum:\t{0,4} | {1,4} | {2,4}", trafficStatsGameLevel.TotalOutgoingMessageCount / num, trafficStatsGameLevel.TotalIncomingMessageCount / num, trafficStatsGameLevel.TotalMessageCount / num);
        GUILayout.Label(text);
        GUILayout.Label(str2);
        GUILayout.Label(str3);
        if (buttonsOn)
        {
            GUILayout.BeginHorizontal();
            statsOn = GUILayout.Toggle(statsOn, "stats on");
            if (GUILayout.Button("Reset"))
            {
                PhotonNetwork.networkingPeer.TrafficStatsReset();
                PhotonNetwork.networkingPeer.TrafficStatsEnabled = true;
            }
            flag = GUILayout.Button("To Log");
            GUILayout.EndHorizontal();
        }
        var str4 = string.Empty;
        var str5 = string.Empty;
        if (trafficStatsOn)
        {
            str4 = "Incoming: " + PhotonNetwork.networkingPeer.TrafficStatsIncoming;
            str5 = "Outgoing: " + PhotonNetwork.networkingPeer.TrafficStatsOutgoing;
            GUILayout.Label(str4);
            GUILayout.Label(str5);
        }
        var str6 = string.Empty;
        if (healthStatsVisible)
        {
            object[] args = { trafficStatsGameLevel.LongestDeltaBetweenSending, trafficStatsGameLevel.LongestDeltaBetweenDispatching, trafficStatsGameLevel.LongestEventCallback, trafficStatsGameLevel.LongestEventCallbackCode, trafficStatsGameLevel.LongestOpResponseCallback, trafficStatsGameLevel.LongestOpResponseCallbackOpCode, PhotonNetwork.networkingPeer.RoundTripTime, PhotonNetwork.networkingPeer.RoundTripTimeVariance };
            str6 = string.Format("ping: {6}[+/-{7}]ms\nlongest delta between\nsend: {0,4}ms disp: {1,4}ms\nlongest time for:\nev({3}):{2,3}ms op({5}):{4,3}ms", args);
            GUILayout.Label(str6);
        }
        if (flag)
        {
            object[] objArray2 = { text, str2, str3, str4, str5, str6 };
            Debug.Log(string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}", objArray2));
        }
        if (GUI.changed)
        {
            statsRect.height = 100f;
        }
        GUI.DragWindow();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            statsWindowOn = !statsWindowOn;
            statsOn = true;
        }
    }
}

