using System.Collections.Generic;
using UnityEngine;
using MonoBehaviour = Photon.MonoBehaviour;

[RequireComponent(typeof(PhotonView))]
public class PickupItemSyncer : MonoBehaviour
{
    public bool IsWaitingForPickupInit;
    private const float TimeDeltaToIgnore = 0.2f;

    public void AskForPickupItemSpawnTimes()
    {
        if (IsWaitingForPickupInit)
        {
            if (PhotonNetwork.playerList.Length < 2)
            {
                Debug.Log("Cant ask anyone else for PickupItem spawn times.");
                IsWaitingForPickupInit = false;
            }
            else
            {
                var next = PhotonNetwork.masterClient.GetNext();
                if (next == null || next.Equals(PhotonNetwork.player))
                {
                    next = PhotonNetwork.player.GetNext();
                }
                if (next != null && !next.Equals(PhotonNetwork.player))
                {
                    photonView.RPC("RequestForPickupTimes", next);
                }
                else
                {
                    Debug.Log("No player left to ask");
                    IsWaitingForPickupInit = false;
                }
            }
        }
    }

    public void OnJoinedRoom()
    {
        Debug.Log(string.Concat("Joined Room. isMasterClient: ", PhotonNetwork.isMasterClient, " id: ", PhotonNetwork.player.ID));
        IsWaitingForPickupInit = !PhotonNetwork.isMasterClient;
        if (PhotonNetwork.playerList.Length >= 2)
        {
            Invoke("AskForPickupItemSpawnTimes", 2f);
        }
    }

    public void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        if (PhotonNetwork.isMasterClient)
        {
            SendPickedUpItems(newPlayer);
        }
    }

    [RPC]
    public void PickupItemInit(double timeBase, float[] inactivePickupsAndTimes)
    {
        IsWaitingForPickupInit = false;
        for (var i = 0; i < inactivePickupsAndTimes.Length / 2; i++)
        {
            var index = i * 2;
            var viewID = (int) inactivePickupsAndTimes[index];
            var num4 = inactivePickupsAndTimes[index + 1];
            var view = PhotonView.Find(viewID);
            var component = view.GetComponent<PickupItem>();
            if (num4 <= 0f)
            {
                component.PickedUp(0f);
            }
            else
            {
                var num5 = num4 + timeBase;
                Debug.Log(string.Concat(view.viewID, " respawn: ", num5, " timeUntilRespawnBasedOnTimeBase:", num4, " SecondsBeforeRespawn: ", component.SecondsBeforeRespawn));
                var num6 = num5 - PhotonNetwork.time;
                if (num4 <= 0f)
                {
                    num6 = 0.0;
                }
                component.PickedUp((float) num6);
            }
        }
    }

    [RPC]
    public void RequestForPickupTimes(PhotonMessageInfo msgInfo)
    {
        if (msgInfo.sender == null)
        {
            Debug.LogError("Unknown player asked for PickupItems");
        }
        else
        {
            SendPickedUpItems(msgInfo.sender);
        }
    }

    private void SendPickedUpItems(PhotonPlayer targtePlayer)
    {
        if (targtePlayer == null)
        {
            Debug.LogWarning("Cant send PickupItem spawn times to unknown targetPlayer.");
        }
        else
        {
            var time = PhotonNetwork.time;
            var num2 = time + 0.20000000298023224;
            var array = new PickupItem[PickupItem.DisabledPickupItems.Count];
            PickupItem.DisabledPickupItems.CopyTo(array);
            var list = new List<float>(array.Length * 2);
            for (var i = 0; i < array.Length; i++)
            {
                var item = array[i];
                if (item.SecondsBeforeRespawn <= 0f)
                {
                    list.Add(item.ViewID);
                    list.Add(0f);
                }
                else
                {
                    var num4 = item.TimeOfRespawn - PhotonNetwork.time;
                    if (item.TimeOfRespawn > num2)
                    {
                        Debug.Log(string.Concat(item.ViewID, " respawn: ", item.TimeOfRespawn, " timeUntilRespawn: ", num4, " (now: ", PhotonNetwork.time, ")"));
                        list.Add(item.ViewID);
                        list.Add((float) num4);
                    }
                }
            }
            Debug.Log(string.Concat("Sent count: ", list.Count, " now: ", time));
            object[] parameters = { PhotonNetwork.time, list.ToArray() };
            photonView.RPC("PickupItemInit", targtePlayer, parameters);
        }
    }
}

