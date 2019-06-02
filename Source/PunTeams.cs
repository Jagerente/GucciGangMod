using System;
using System.Collections.Generic;
using UnityEngine;

public class PunTeams : MonoBehaviour
{
    public static Dictionary<Team, List<PhotonPlayer>> PlayersPerTeam;
    public const string TeamPlayerProp = "team";

    public void OnJoinedRoom()
    {
        UpdateTeams();
    }

    public void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
    {
        UpdateTeams();
    }

    public void Start()
    {
        PlayersPerTeam = new Dictionary<Team, List<PhotonPlayer>>();
        var values = Enum.GetValues(typeof(Team));
        foreach (var current in values)
        {
            PlayersPerTeam[(Team)(byte)current] = new List<PhotonPlayer>();
        }
    }


    public void UpdateTeams()
    {
        var values = Enum.GetValues(typeof(Team));
        foreach (var current in values)
        {
            PlayersPerTeam[(Team)(byte)current].Clear();
        }
        for (var i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            var photonPlayer = PhotonNetwork.playerList[i];
            var team = photonPlayer.GetTeam();
            PlayersPerTeam[team].Add(photonPlayer);
        }
    }


    public enum Team : byte
    {
        blue = 2,
        none = 0,
        red = 1
    }
}

