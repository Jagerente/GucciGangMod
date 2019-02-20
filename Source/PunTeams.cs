//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System;
using System.Collections;
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
        var enumerator = Enum.GetValues(typeof(Team)).GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                PlayersPerTeam[(Team) ((byte) current)] = new List<PhotonPlayer>();
            }
        }
        finally
        {
            var disposable = enumerator as IDisposable;
            if (disposable != null)
            	disposable.Dispose();
        }
    }

    public void UpdateTeams()
    {
        var enumerator = Enum.GetValues(typeof(Team)).GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                PlayersPerTeam[(Team) ((byte) current)].Clear();
            }
        }
        finally
        {
            var disposable = enumerator as IDisposable;
            if (disposable != null)
            	disposable.Dispose();
        }
        for (var i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            var player = PhotonNetwork.playerList[i];
            var team = player.GetTeam();
            PlayersPerTeam[team].Add(player);
        }
    }

    public enum Team : byte
    {
        blue = 2,
        none = 0,
        red = 1
    }
}

