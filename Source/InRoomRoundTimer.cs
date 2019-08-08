using ExitGames.Client.Photon;
using UnityEngine;

public class InRoomRoundTimer : MonoBehaviour
{
    public int SecondsPerTurn = 5;
    private bool startRoundWhenTimeIsSynced;
    public double StartTime;
    private const string StartTimeKey = "st";
    public Rect TextPos = new Rect(0f, 80f, 150f, 300f);

    public void OnGUI()
    {
        var num = PhotonNetwork.time - StartTime;
        var num2 = SecondsPerTurn - num % SecondsPerTurn;
        var num3 = (int)(num / SecondsPerTurn);
        GUILayout.BeginArea(TextPos);
        GUILayout.Label(string.Format("elapsed: {0:0.000}", num));
        GUILayout.Label(string.Format("remaining: {0:0.000}", num2));
        GUILayout.Label(string.Format("turn: {0:0}", num3));
        if (GUILayout.Button("new round"))
        {
            StartRoundNow();
        }
        GUILayout.EndArea();
    }

    public void OnJoinedRoom()
    {
        if (PhotonNetwork.isMasterClient)
        {
            StartRoundNow();
        }
        else
        {
            Debug.Log("StartTime already set: " + PhotonNetwork.room.customProperties.ContainsKey("st"));
        }
    }

    public void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
        if (!PhotonNetwork.room.customProperties.ContainsKey("st"))
        {
            Debug.Log("The new master starts a new round, cause we didn't start yet.");
            StartRoundNow();
        }
    }

    public void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey("st"))
        {
            StartTime = (double)propertiesThatChanged["st"];
        }
    }

    private void StartRoundNow()
    {
        if (PhotonNetwork.time < 9.9999997473787516E-05)
        {
            startRoundWhenTimeIsSynced = true;
        }
        else
        {
            startRoundWhenTimeIsSynced = false;
            var propertiesToSet = new Hashtable();
            propertiesToSet["st"] = PhotonNetwork.time;
            PhotonNetwork.room.SetCustomProperties(propertiesToSet);
        }
    }

    private void Update()
    {
        if (startRoundWhenTimeIsSynced)
        {
            StartRoundNow();
        }
    }
}