//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using ExitGames.Client.Photon;
using ExitGames.Client.Photon.Lite;
using System.Collections.Generic;

internal class LoadbalancingPeer : PhotonPeer
{
    public LoadbalancingPeer(IPhotonPeerListener listener, ConnectionProtocol protocolType) : base(listener, protocolType)
    {
    }

    public virtual bool OpAuthenticate(string appId, string appVersion, string userId, AuthenticationValues authValues, string regionCode)
    {
        if (DebugOut >= DebugLevel.INFO)
        {
            Listener.DebugReturn(DebugLevel.INFO, "OpAuthenticate()");
        }
        var customOpParameters = new Dictionary<byte, object>();
        if ((authValues != null) && (authValues.Secret != null))
        {
            customOpParameters[0xdd] = authValues.Secret;
            return OpCustom(230, customOpParameters, true, 0, false);
        }
        customOpParameters[220] = appVersion;
        customOpParameters[0xe0] = appId;
        if (!string.IsNullOrEmpty(regionCode))
        {
            customOpParameters[210] = regionCode;
        }
        if (!string.IsNullOrEmpty(userId))
        {
            customOpParameters[0xe1] = userId;
        }
        if ((authValues != null) && (authValues.AuthType != CustomAuthenticationType.None))
        {
            if (!IsEncryptionAvailable)
            {
                Listener.DebugReturn(DebugLevel.ERROR, "OpAuthenticate() failed. When you want Custom Authentication encryption is mandatory.");
                return false;
            }
            customOpParameters[0xd9] = (byte) authValues.AuthType;
            if (!string.IsNullOrEmpty(authValues.Secret))
            {
                customOpParameters[0xdd] = authValues.Secret;
            }
            if (!string.IsNullOrEmpty(authValues.AuthParameters))
            {
                customOpParameters[0xd8] = authValues.AuthParameters;
            }
            if (authValues.AuthPostData != null)
            {
                customOpParameters[0xd6] = authValues.AuthPostData;
            }
        }
        var flag = OpCustom(230, customOpParameters, true, 0, IsEncryptionAvailable);
        if (!flag)
        {
            Listener.DebugReturn(DebugLevel.ERROR, "Error calling OpAuthenticate! Did not work. Check log output, CustomAuthenticationValues and if you're connected.");
        }
        return flag;
    }

    public virtual bool OpChangeGroups(byte[] groupsToRemove, byte[] groupsToAdd)
    {
        if (DebugOut >= DebugLevel.ALL)
        {
            Listener.DebugReturn(DebugLevel.ALL, "OpChangeGroups()");
        }
        var customOpParameters = new Dictionary<byte, object>();
        if (groupsToRemove != null)
        {
            customOpParameters[0xef] = groupsToRemove;
        }
        if (groupsToAdd != null)
        {
            customOpParameters[0xee] = groupsToAdd;
        }
        return OpCustom(0xf8, customOpParameters, true, 0);
    }

    public virtual bool OpCreateRoom(string roomName, RoomOptions roomOptions, TypedLobby lobby, Hashtable playerProperties, bool onGameServer)
    {
        if (DebugOut >= DebugLevel.INFO)
        {
            Listener.DebugReturn(DebugLevel.INFO, "OpCreateRoom()");
        }
        var customOpParameters = new Dictionary<byte, object>();
        if (!string.IsNullOrEmpty(roomName))
        {
            customOpParameters[0xff] = roomName;
        }
        if (lobby != null)
        {
            customOpParameters[0xd5] = lobby.Name;
            customOpParameters[0xd4] = (byte) lobby.Type;
        }
        if (onGameServer)
        {
            if ((playerProperties != null) && (playerProperties.Count > 0))
            {
                customOpParameters[0xf9] = playerProperties;
                customOpParameters[250] = true;
            }
            if (roomOptions == null)
            {
                roomOptions = new RoomOptions();
            }
            var target = new Hashtable();
            customOpParameters[0xf8] = target;
            target.MergeStringKeys(roomOptions.customRoomProperties);
            target[(byte) 0xfd] = roomOptions.isOpen;
            target[(byte) 0xfe] = roomOptions.isVisible;
            target[(byte) 250] = roomOptions.customRoomPropertiesForLobby;
            if (roomOptions.maxPlayers > 0)
            {
                target[(byte) 0xff] = roomOptions.maxPlayers;
            }
            if (roomOptions.cleanupCacheOnLeave)
            {
                customOpParameters[0xf1] = true;
                target[(byte) 0xf9] = true;
            }
        }
        return OpCustom(0xe3, customOpParameters, true);
    }

    public virtual bool OpFindFriends(string[] friendsToFind)
    {
        var customOpParameters = new Dictionary<byte, object>();
        if ((friendsToFind != null) && (friendsToFind.Length > 0))
        {
            customOpParameters[1] = friendsToFind;
        }
        return OpCustom(0xde, customOpParameters, true);
    }

    public virtual bool OpGetRegions(string appId)
    {
        var customOpParameters = new Dictionary<byte, object>();
        customOpParameters[0xe0] = appId;
        return OpCustom(220, customOpParameters, true, 0, true);
    }

    public virtual bool OpJoinLobby(TypedLobby lobby)
    {
        if (DebugOut >= DebugLevel.INFO)
        {
            Listener.DebugReturn(DebugLevel.INFO, "OpJoinLobby()");
        }
        Dictionary<byte, object> customOpParameters = null;
        if ((lobby != null) && !lobby.IsDefault)
        {
            customOpParameters = new Dictionary<byte, object>();
            customOpParameters[0xd5] = lobby.Name;
            customOpParameters[0xd4] = (byte) lobby.Type;
        }
        return OpCustom(0xe5, customOpParameters, true);
    }

    public virtual bool OpJoinRandomRoom(Hashtable expectedCustomRoomProperties, byte expectedMaxPlayers, Hashtable playerProperties, MatchmakingMode matchingType, TypedLobby typedLobby, string sqlLobbyFilter)
    {
        if (DebugOut >= DebugLevel.INFO)
        {
            Listener.DebugReturn(DebugLevel.INFO, "OpJoinRandomRoom()");
        }
        var target = new Hashtable();
        target.MergeStringKeys(expectedCustomRoomProperties);
        if (expectedMaxPlayers > 0)
        {
            target[(byte) 0xff] = expectedMaxPlayers;
        }
        var customOpParameters = new Dictionary<byte, object>();
        if (target.Count > 0)
        {
            customOpParameters[0xf8] = target;
        }
        if ((playerProperties != null) && (playerProperties.Count > 0))
        {
            customOpParameters[0xf9] = playerProperties;
        }
        if (matchingType != MatchmakingMode.FillRoom)
        {
            customOpParameters[0xdf] = (byte) matchingType;
        }
        if (typedLobby != null)
        {
            customOpParameters[0xd5] = typedLobby.Name;
            customOpParameters[0xd4] = (byte) typedLobby.Type;
        }
        if (!string.IsNullOrEmpty(sqlLobbyFilter))
        {
            customOpParameters[0xf5] = sqlLobbyFilter;
        }
        return OpCustom(0xe1, customOpParameters, true);
    }

    public virtual bool OpJoinRoom(string roomName, RoomOptions roomOptions, TypedLobby lobby, bool createIfNotExists, Hashtable playerProperties, bool onGameServer)
    {
        var customOpParameters = new Dictionary<byte, object>();
        if (!string.IsNullOrEmpty(roomName))
        {
            customOpParameters[0xff] = roomName;
        }
        if (createIfNotExists)
        {
            customOpParameters[0xd7] = true;
            if (lobby != null)
            {
                customOpParameters[0xd5] = lobby.Name;
                customOpParameters[0xd4] = (byte) lobby.Type;
            }
        }
        if (onGameServer)
        {
            if ((playerProperties != null) && (playerProperties.Count > 0))
            {
                customOpParameters[0xf9] = playerProperties;
                customOpParameters[250] = true;
            }
            if (createIfNotExists)
            {
                if (roomOptions == null)
                {
                    roomOptions = new RoomOptions();
                }
                var target = new Hashtable();
                customOpParameters[0xf8] = target;
                target.MergeStringKeys(roomOptions.customRoomProperties);
                target[(byte) 0xfd] = roomOptions.isOpen;
                target[(byte) 0xfe] = roomOptions.isVisible;
                target[(byte) 250] = roomOptions.customRoomPropertiesForLobby;
                if (roomOptions.maxPlayers > 0)
                {
                    target[(byte) 0xff] = roomOptions.maxPlayers;
                }
                if (roomOptions.cleanupCacheOnLeave)
                {
                    customOpParameters[0xf1] = true;
                    target[(byte) 0xf9] = true;
                }
            }
        }
        return OpCustom(0xe2, customOpParameters, true);
    }

    public virtual bool OpLeaveLobby()
    {
        if (DebugOut >= DebugLevel.INFO)
        {
            Listener.DebugReturn(DebugLevel.INFO, "OpLeaveLobby()");
        }
        return OpCustom(0xe4, null, true);
    }

    public virtual bool OpRaiseEvent(byte eventCode, object customEventContent, bool sendReliable, RaiseEventOptions raiseEventOptions)
    {
        var customOpParameters = new Dictionary<byte, object>();
        customOpParameters[0xf4] = eventCode;
        if (customEventContent != null)
        {
            customOpParameters[0xf5] = customEventContent;
        }
        if (raiseEventOptions == null)
        {
            raiseEventOptions = RaiseEventOptions.Default;
        }
        else
        {
            if (raiseEventOptions.CachingOption != EventCaching.DoNotCache)
            {
                customOpParameters[0xf7] = (byte) raiseEventOptions.CachingOption;
            }
            if (raiseEventOptions.Receivers != ReceiverGroup.Others)
            {
                customOpParameters[0xf6] = (byte) raiseEventOptions.Receivers;
            }
            if (raiseEventOptions.InterestGroup != 0)
            {
                customOpParameters[240] = raiseEventOptions.InterestGroup;
            }
            if (raiseEventOptions.TargetActors != null)
            {
                customOpParameters[0xfc] = raiseEventOptions.TargetActors;
            }
            if (raiseEventOptions.ForwardToWebhook)
            {
                customOpParameters[0xea] = true;
            }
        }
        return OpCustom(0xfd, customOpParameters, sendReliable, raiseEventOptions.SequenceChannel, false);
    }

    public bool OpSetCustomPropertiesOfActor(int actorNr, Hashtable actorProperties, bool broadcast, byte channelId)
    {
        return OpSetPropertiesOfActor(actorNr, actorProperties.StripToStringKeys(), broadcast, channelId);
    }

    public bool OpSetCustomPropertiesOfRoom(Hashtable gameProperties, bool broadcast, byte channelId)
    {
        return OpSetPropertiesOfRoom(gameProperties.StripToStringKeys(), broadcast, channelId);
    }

    protected bool OpSetPropertiesOfActor(int actorNr, Hashtable actorProperties, bool broadcast, byte channelId)
    {
        if (DebugOut >= DebugLevel.INFO)
        {
            Listener.DebugReturn(DebugLevel.INFO, "OpSetPropertiesOfActor()");
        }
        if ((actorNr <= 0) || (actorProperties == null))
        {
            if (DebugOut >= DebugLevel.INFO)
            {
                Listener.DebugReturn(DebugLevel.INFO, "OpSetPropertiesOfActor not sent. ActorNr must be > 0 and actorProperties != null.");
            }
            return false;
        }
        var customOpParameters = new Dictionary<byte, object>();
        customOpParameters.Add(0xfb, actorProperties);
        customOpParameters.Add(0xfe, actorNr);
        if (broadcast)
        {
            customOpParameters.Add(250, broadcast);
        }
        return OpCustom(0xfc, customOpParameters, broadcast, channelId);
    }

    public bool OpSetPropertiesOfRoom(Hashtable gameProperties, bool broadcast, byte channelId)
    {
        if (DebugOut >= DebugLevel.INFO)
        {
            Listener.DebugReturn(DebugLevel.INFO, "OpSetPropertiesOfRoom()");
        }
        var customOpParameters = new Dictionary<byte, object>();
        customOpParameters.Add(0xfb, gameProperties);
        if (broadcast)
        {
            customOpParameters.Add(250, true);
        }
        return OpCustom(0xfc, customOpParameters, broadcast, channelId);
    }

    protected void OpSetPropertyOfRoom(byte propCode, object value)
    {
        var gameProperties = new Hashtable();
        gameProperties[propCode] = value;
        OpSetPropertiesOfRoom(gameProperties, true, 0);
    }
}

