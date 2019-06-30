using ExitGames.Client.Photon;
using UnityEngine;

public class PhotonLagSimulationGui : MonoBehaviour
{
    public bool Visible = true;
    public int WindowId = 101;
    public Rect WindowRect = new Rect(0f, 100f, 120f, 100f);

    private void NetSimHasNoPeerWindow(int windowId)
    {
        GUILayout.Label("No peer to communicate with. ");
    }

    private void NetSimWindow(int windowId)
    {
        GUILayout.Label(string.Format("Rtt:{0,4} +/-{1,3}", Peer.RoundTripTime, Peer.RoundTripTimeVariance));
        var isSimulationEnabled = Peer.IsSimulationEnabled;
        var flag2 = GUILayout.Toggle(isSimulationEnabled, "Simulate");
        if (flag2 != isSimulationEnabled)
        {
            Peer.IsSimulationEnabled = flag2;
        }
        float incomingLag = Peer.NetworkSimulationSettings.IncomingLag;
        GUILayout.Label("Lag " + incomingLag);
        incomingLag = GUILayout.HorizontalSlider(incomingLag, 0f, 500f);
        Peer.NetworkSimulationSettings.IncomingLag = (int) incomingLag;
        Peer.NetworkSimulationSettings.OutgoingLag = (int) incomingLag;
        float incomingJitter = Peer.NetworkSimulationSettings.IncomingJitter;
        GUILayout.Label("Jit " + incomingJitter);
        incomingJitter = GUILayout.HorizontalSlider(incomingJitter, 0f, 100f);
        Peer.NetworkSimulationSettings.IncomingJitter = (int) incomingJitter;
        Peer.NetworkSimulationSettings.OutgoingJitter = (int) incomingJitter;
        float incomingLossPercentage = Peer.NetworkSimulationSettings.IncomingLossPercentage;
        GUILayout.Label("Loss " + incomingLossPercentage);
        incomingLossPercentage = GUILayout.HorizontalSlider(incomingLossPercentage, 0f, 10f);
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
                WindowRect = GUILayout.Window(WindowId, WindowRect, NetSimHasNoPeerWindow, "Netw. Sim.");
            }
            else
            {
                WindowRect = GUILayout.Window(WindowId, WindowRect, NetSimWindow, "Netw. Sim.");
            }
        }
    }

    public void Start()
    {
        Peer = PhotonNetwork.networkingPeer;
    }

    public PhotonPeer Peer { get; set; }
}

