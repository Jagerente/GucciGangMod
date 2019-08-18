using System;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;
using GGM;
using GGM.Config;
using UnityEngine;

public class PhotonPlayer
{
    public Dictionary<string, long> RPCList = new Dictionary<string, long>();
    public Dictionary<byte, long> EventList = new Dictionary<byte, long>();
    private int actorID;
    public readonly bool isLocal;
    private string nameField;
    public object TagObject;
    public bool CelestialDeath { get; set; }
    public bool CyanMod { get; set; }
    public bool DI { get; set; }
    public bool DeadInside { get; set; }
    public bool DeadInsideVer { get; set; }
    public bool DeathMod { get; set; }
    public bool GucciLab { get; set; }
    public bool RC83 { get; set; }
    public bool RS { get; set; }
    public bool SukaMod { get; set; }
    public bool SukaModOld { get; set; }
    public bool Universe { get; set; }
    public bool VENICE { get; set; }

    public bool GucciGangMod;

    public static PhotonPlayer[] GetGGMUsers()
    {
        var tmp = new List<PhotonPlayer>();
        foreach (var player in PhotonNetwork.playerList)
        {
            if (player.GucciGangMod)
            {
                tmp.Add(player);
            }
        }
        return tmp.ToArray();
    }

    protected internal PhotonPlayer(bool isLocal, int actorID, Hashtable properties)
    {
        this.actorID = -1;
        nameField = String.Empty;
        customProperties = new Hashtable();
        this.isLocal = isLocal;
        this.actorID = actorID;
        InternalCacheProperties(properties);
        if (isLocal)
            GucciGangMod = true;
    }

    public PhotonPlayer(bool isLocal, int actorID, string name)
    {
        this.actorID = -1;
        nameField = String.Empty;
        customProperties = new Hashtable();
        this.isLocal = isLocal;
        this.actorID = actorID;
        nameField = name;
        if (isLocal)
            GucciGangMod = true;
    }

    public void AddToEvent(byte code)
    {
        if (!EventList.ContainsKey(code))
        {
            EventList.Add(code, 1);
        }
        else
        {
            EventList[code]++;
        }
    }

    public void AddToRPC(string rpc)
    {
        if (!RPCList.ContainsKey(rpc))
        {
            RPCList.Add(rpc, 1);
        }
        else
        {
            RPCList[rpc]++;
        }
    }

    public override bool Equals(object p)
    {
        var player = p as PhotonPlayer;
        return player != null && GetHashCode() == player.GetHashCode();
    }

    public string Events
    {
        get
        {
            string str = String.Empty;
            foreach (byte code in this.EventList.Keys)
            {
                str += code + ": " + EventList[code] + "\n";
            }
            return str;
        }
    }

    public static PhotonPlayer Find(int ID)
    {
        for (var i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            var player = PhotonNetwork.playerList[i];
            if (player.ID == ID)
            {
                return player;
            }
        }

        return null;
    }

    public PhotonPlayer Get(int id)
    {
        return Find(id);
    }

    public override int GetHashCode()
    {
        return ID;
    }

    public PhotonPlayer GetNext()
    {
        return GetNextFor(ID);
    }

    public PhotonPlayer GetNextFor(PhotonPlayer currentPlayer)
    {
        if (currentPlayer == null)
        {
            return null;
        }

        return GetNextFor(currentPlayer.ID);
    }

    public PhotonPlayer GetNextFor(int currentPlayerId)
    {
        if (PhotonNetwork.networkingPeer == null || PhotonNetwork.networkingPeer.mActors == null || PhotonNetwork.networkingPeer.mActors.Count < 2)
        {
            return null;
        }

        var mActors = PhotonNetwork.networkingPeer.mActors;
        var num = 2147483647;
        var num2 = currentPlayerId;
        foreach (var num3 in mActors.Keys)
        {
            if (num3 < num2)
            {
                num2 = num3;
            }
            else if (num3 > currentPlayerId && num3 < num)
            {
                num = num3;
            }
        }

        return num == 2147483647 ? mActors[num2] : mActors[num];
    }

    internal void InternalCacheProperties(Hashtable properties)
    {
        if (properties != null && properties.Count != 0 && !customProperties.Equals(properties))
        {
            if (properties.ContainsKey((byte)255))
            {
                nameField = (string)properties[(byte)255];
            }

            customProperties.MergeStringKeys(properties);
            customProperties.StripKeysWithNullValues();
        }
    }

    internal void InternalChangeLocalID(int newID)
    {
        if (!isLocal)
        {
            Debug.LogError("ERROR You should never change PhotonPlayer IDs!");
        }
        else
        {
            actorID = newID;
        }
    }

    public void SetCustomProperties(Hashtable propertiesToSet)
    {
        if (propertiesToSet != null)
        {
            customProperties.MergeStringKeys(propertiesToSet);
            customProperties.StripKeysWithNullValues();
            var actorProperties = propertiesToSet.StripToStringKeys();
            if (actorID > 0 && !PhotonNetwork.offlineMode)
            {
                PhotonNetwork.networkingPeer.OpSetCustomPropertiesOfActor(actorID, actorProperties, true, 0);
            }

            object[] parameters = { this, propertiesToSet };
            NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerPropertiesChanged, parameters);
        }
    }

    public override string ToString()
    {
        if (String.IsNullOrEmpty(name))
        {
            return String.Format("#{0:00}{1}", ID, !isMasterClient ? String.Empty : "(master)");
        }

        return String.Format("'{0}'{1}", name, !isMasterClient ? String.Empty : "(master)");
    }

    public string ToStringFull()
    {
        return String.Format("#{0:00} '{1}' {2}", ID, name, customProperties.ToStringFull());
    }

    public string GetProperty(string key)
    {
        customProperties.TryGetValue(key, out var property);
        return property != null ? property.ToString() : String.Empty;
    }

    public static string[] AllProps;

    public Hashtable allProperties
    {
        get
        {
            var target = new Hashtable();
            target.Merge(customProperties);
            target[(byte)255] = name;
            return target;
        }
    }

    public string Props
    {
        get
        {
            string str = "";
            foreach (object prop in this.allProperties.Keys)
            {
                str += prop + ": " + this.allProperties[prop] + "\n";
            }
            return str;
        }
    }

    public Hashtable customProperties { get; private set; }

    public int ID
    {
        get { return actorID; }
    }

    public bool isGuest
    {
        get { return Name.StripHEX().Contains("GUEST"); }
    }

    public bool isAbusive
    {
        get { return Name.StripHEX().StartsWith("Tokyo Ghoul") || Name.StripHEX().Contains("Violent") || Name.StripHEX().StartsWith("Vivid") || Name.StripHEX().Contains("Hyper-MegaCannon") || Name.StripHEX().Contains("MegaCannon") || Name.StripHEX().Contains("G_U_E_S_T") || Name.StripHEX().Contains("Tokyo Ghoul X [") || Name.StripHEX().Contains("Tokyo Ghoul") || Name.StripHEX().Contains("MULTI-WEAPON") || Name.StripHEX().Contains("Saif"); }
    }

    public bool isMuted
    {
        get { return Settings.MutedPlayers.Contains(Name.StripHEX()); }
    }

    public bool isMasterClient
    {
        get { return PhotonNetwork.networkingPeer.mMasterClient == this; }
    }

    protected internal string Name
    {
        get
        {
            string str = "Unknown";
            if (customProperties["name"] is string && customProperties["name"] != null)
            {
                str = (string)customProperties["name"];
            }
            return str;
        }
        set
        {
            var propertiesToSet = new Hashtable { { "name", value } };
            SetCustomProperties(propertiesToSet);
        }
    }

    public string name
    {
        get { return nameField; }
        set
        {
            if (!isLocal)
            {
                Debug.LogError("Error: Cannot change the name of a remote player!");
            }
            else
            {
                nameField = value;
            }
        }
    }

    public string RPCs
    {
        get
        {
            string str = String.Empty;
            foreach (string key in RPCList.Keys)
            {
                str += key + ": " + RPCList[key] + "\n";
            }
            return str;
        }
    }
}