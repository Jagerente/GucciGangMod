//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using ExitGames.Client.Photon;
using UnityEngine;

public class PhotonLagSimulationGui : MonoBehaviour
{
    public bool Visible = true;
    public int WindowId = 0x65;
    public Rect WindowRect = new Rect(0f, 100f, 120f, 100f);

    private void NetSimHasNoPeerWindow(int windowId)
    {
        GUILayout.Label("No peer to communicate with. ", new GUILayoutOption[0]);
    }

    private void NetSimWindow(int windowId)
    {
        GUILayout.Label(string.Format("Rtt:{0,4} +/-{1,3}", Peer.RoundTripTime, Peer.RoundTripTimeVariance), new GUILayoutOption[0]);
        var isSimulationEnabled = Peer.IsSimulationEnabled;
        var flag2 = GUILayout.Toggle(isSimulationEnabled, "Simulate", new GUILayoutOption[0]);
        if (flag2 != isSimulationEnabled)
        {
            Peer.IsSimulationEnabled = flag2;
        }
        float incomingLag = Peer.NetworkSimulationSettings.IncomingLag;
        GUILayout.Label("Lag " + incomingLag, new GUILayoutOption[0]);
        incomingLag = GUILayout.HorizontalSlider(incomingLag, 0f, 500f, new GUILayoutOption[0]);
        Peer.NetworkSimulationSettings.IncomingLag = (int) incomingLag;
        Peer.NetworkSimulationSettings.OutgoingLag = (int) incomingLag;
        float incomingJitter = Peer.NetworkSimulationSettings.IncomingJitter;
        GUILayout.Label("Jit " + incomingJitter, new GUILayoutOption[0]);
        incomingJitter = GUILayout.HorizontalSlider(incomingJitter, 0f, 100f, new GUILayoutOption[0]);
        Peer.NetworkSimulationSettings.IncomingJitter = (int) incomingJitter;
        Peer.NetworkSimulationSettings.OutgoingJitter = (int) incomingJitter;
        float incomingLossPercentage = Peer.NetworkSimulationSettings.IncomingLossPercentage;
        GUILayout.Label("Loss " + incomingLossPercentage, new GUILayoutOption[0]);
        incomingLossPercentage = GUILayout.HorizontalSlider(incomingLossPercentage, 0f, 10f, new GUILayoutOption[0]);
        Peer.NetworkSimulationSettings.IncomingLossPercentage = (int) incomingLossPercentage;
        Peer.NetworkSimulationSettings.OutgoingLossPercentage = (int) incomingLossPercentage;
        if (GUI.changed)
        {
            WindowRect.height = 100f;
        }
        GUI.DragWindow();
    }

    public void OnGUI()
    {
        if (Visible)
        {
            if (Peer == null)
            {
                WindowRect = GUILayout.Window(WindowId, WindowRect, new GUI.WindowFunction(NetSimHasNoPeerWindow), "Netw. Sim.", new GUILayoutOption[0]);
            }
            else
            {
                WindowRect = GUILayout.Window(WindowId, WindowRect, new GUI.WindowFunction(NetSimWindow), "Netw. Sim.", new GUILayoutOption[0]);
            }
        }
    }

    public void Start()
    {
        Peer = PhotonNetwork.networkingPeer;
    }

    public PhotonPeer Peer { get; set; }
}

