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
        if (authValues != null && authValues.Secret != null)
        {
            customOpParameters[221] = authValues.Secret;
            return SendOperation(230, customOpParameters, SendOptions.SendReliable);
        }
        customOpParameters[220] = appVersion;
        customOpParameters[224] = appId;
        if (!string.IsNullOrEmpty(regionCode))
        {
            customOpParameters[210] = regionCode;
        }
        if (!string.IsNullOrEmpty(userId))
        {
            customOpParameters[225] = userId;
        }
        if (authValues != null && authValues.AuthType != CustomAuthenticationType.None)
        {
            if (!IsEncryptionAvailable)
            {
                Listener.DebugReturn(DebugLevel.ERROR, "OpAuthenticate() failed. When you want Custom Authentication encryption is mandatory.");
                return false;
            }
            customOpParameters[217] = (byte)authValues.AuthType;
            if (!string.IsNullOrEmpty(authValues.Secret))
            {
                customOpParameters[221] = authValues.Secret;
            }
            if (!string.IsNullOrEmpty(authValues.AuthParameters))
            {
                customOpParameters[216] = authValues.AuthParameters;
            }
            if (authValues.AuthPostData != null)
            {
                customOpParameters[214] = authValues.AuthPostData;
            }
        }
        var flag = SendOperation(230, customOpParameters, new SendOptions() { Channel = 0, DeliveryMode = DeliveryMode.Reliable, Encrypt = IsEncryptionAvailable });
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
            customOpParameters[239] = groupsToRemove;
        }
        if (groupsToAdd != null)
        {
            customOpParameters[238] = groupsToAdd;
        }
        return SendOperation(248, customOpParameters, SendOptions.SendReliable);
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
            customOpParameters[255] = roomName;
        }
        if (lobby != null)
        {
            customOpParameters[213] = lobby.Name;
            customOpParameters[212] = (byte)lobby.Type;
        }
        if (onGameServer)
        {
            if (playerProperties != null && playerProperties.Count > 0)
            {
                customOpParameters[249] = playerProperties;
                customOpParameters[250] = true;
            }
            if (roomOptions == null)
            {
                roomOptions = new RoomOptions();
            }
            var target = new Hashtable();
            customOpParameters[248] = target;
            target.MergeStringKeys(roomOptions.customRoomProperties);
            target[(byte)253] = roomOptions.isOpen;
            target[(byte)254] = roomOptions.isVisible;
            target[(byte)250] = roomOptions.customRoomPropertiesForLobby;
            if (roomOptions.maxPlayers > 0)
            {
                target[(byte)255] = roomOptions.maxPlayers;
            }
            if (roomOptions.cleanupCacheOnLeave)
            {
                customOpParameters[241] = true;
                target[(byte)249] = true;
            }
        }
        return SendOperation(227, customOpParameters, SendOptions.SendReliable);
    }

    public virtual bool OpFindFriends(string[] friendsToFind)
    {
        var customOpParameters = new Dictionary<byte, object>();
        if (friendsToFind != null && friendsToFind.Length > 0)
        {
            customOpParameters[1] = friendsToFind;
        }
        return SendOperation(222, customOpParameters, SendOptions.SendReliable);
    }

    public virtual bool OpGetRegions(string appId)
    {
        var customOpParameters = new Dictionary<byte, object>();
        customOpParameters[224] = appId;
        return SendOperation(220, customOpParameters, new SendOptions() { Encrypt = true, DeliveryMode = DeliveryMode.Reliable });
    }

    public virtual bool OpJoinLobby(TypedLobby lobby)
    {
        if (DebugOut >= DebugLevel.INFO)
        {
            Listener.DebugReturn(DebugLevel.INFO, "OpJoinLobby()");
        }
        Dictionary<byte, object> customOpParameters = null;
        if (lobby != null && !lobby.IsDefault)
        {
            customOpParameters = new Dictionary<byte, object>();
            customOpParameters[213] = lobby.Name;
            customOpParameters[212] = (byte)lobby.Type;
        }
        return SendOperation(229, customOpParameters, SendOptions.SendReliable);
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
            target[(byte)255] = expectedMaxPlayers;
        }
        var customOpParameters = new Dictionary<byte, object>();
        if (target.Count > 0)
        {
            customOpParameters[248] = target;
        }
        if (playerProperties != null && playerProperties.Count > 0)
        {
            customOpParameters[249] = playerProperties;
        }
        if (matchingType != MatchmakingMode.FillRoom)
        {
            customOpParameters[223] = (byte)matchingType;
        }
        if (typedLobby != null)
        {
            customOpParameters[213] = typedLobby.Name;
            customOpParameters[212] = (byte)typedLobby.Type;
        }
        if (!string.IsNullOrEmpty(sqlLobbyFilter))
        {
            customOpParameters[245] = sqlLobbyFilter;
        }
        return SendOperation(225, customOpParameters, SendOptions.SendReliable);
    }

    public virtual bool OpJoinRoom(string roomName, RoomOptions roomOptions, TypedLobby lobby, bool createIfNotExists, Hashtable playerProperties, bool onGameServer)
    {
        var customOpParameters = new Dictionary<byte, object>();
        if (!string.IsNullOrEmpty(roomName))
        {
            customOpParameters[255] = roomName;
        }
        if (createIfNotExists)
        {
            customOpParameters[215] = true;
            if (lobby != null)
            {
                customOpParameters[213] = lobby.Name;
                customOpParameters[212] = (byte)lobby.Type;
            }
        }
        if (onGameServer)
        {
            if (playerProperties != null && playerProperties.Count > 0)
            {
                customOpParameters[249] = playerProperties;
                customOpParameters[250] = true;
            }
            if (createIfNotExists)
            {
                if (roomOptions == null)
                {
                    roomOptions = new RoomOptions();
                }
                var target = new Hashtable();
                customOpParameters[248] = target;
                target.MergeStringKeys(roomOptions.customRoomProperties);
                target[(byte)253] = roomOptions.isOpen;
                target[(byte)254] = roomOptions.isVisible;
                target[(byte)250] = roomOptions.customRoomPropertiesForLobby;
                if (roomOptions.maxPlayers > 0)
                {
                    target[(byte)255] = roomOptions.maxPlayers;
                }
                if (roomOptions.cleanupCacheOnLeave)
                {
                    customOpParameters[241] = true;
                    target[(byte)249] = true;
                }
            }
        }
        return SendOperation(226, customOpParameters, SendOptions.SendReliable);
    }

    public virtual bool OpLeaveLobby()
    {
        if (DebugOut >= DebugLevel.INFO)
        {
            Listener.DebugReturn(DebugLevel.INFO, "OpLeaveLobby()");
        }
        return SendOperation(228, null, SendOptions.SendReliable);
    }

    public virtual bool OpRaiseEvent(byte eventCode, object customEventContent, bool sendReliable, RaiseEventOptions raiseEventOptions)
    {
        var customOpParameters = new Dictionary<byte, object>();
        customOpParameters[244] = eventCode;
        if (customEventContent != null)
        {
            customOpParameters[245] = customEventContent;
        }
        if (raiseEventOptions == null)
        {
            raiseEventOptions = RaiseEventOptions.Default;
        }
        else
        {
            if (raiseEventOptions.CachingOption != EventCaching.DoNotCache)
            {
                customOpParameters[247] = (byte)raiseEventOptions.CachingOption;
            }
            if (raiseEventOptions.Receivers != ReceiverGroup.Others)
            {
                customOpParameters[246] = (byte)raiseEventOptions.Receivers;
            }
            if (raiseEventOptions.InterestGroup != 0)
            {
                customOpParameters[240] = raiseEventOptions.InterestGroup;
            }
            if (raiseEventOptions.TargetActors != null)
            {
                customOpParameters[252] = raiseEventOptions.TargetActors;
            }
            if (raiseEventOptions.ForwardToWebhook)
            {
                customOpParameters[234] = true;
            }
        }
        return SendOperation(253, customOpParameters, new SendOptions() { DeliveryMode = sendReliable ? DeliveryMode.Reliable : DeliveryMode.Unreliable, Channel = raiseEventOptions.SequenceChannel });
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
        if (actorNr <= 0 || actorProperties == null)
        {
            if (DebugOut >= DebugLevel.INFO)
            {
                Listener.DebugReturn(DebugLevel.INFO, "OpSetPropertiesOfActor not sent. ActorNr must be > 0 and actorProperties != null.");
            }
            return false;
        }
        var customOpParameters = new Dictionary<byte, object>();
        customOpParameters.Add(251, actorProperties);
        customOpParameters.Add(254, actorNr);
        if (broadcast)
        {
            customOpParameters.Add(250, broadcast);
        }
        return SendOperation(252, customOpParameters, new SendOptions() { Reliability = broadcast, Channel = channelId });
    }

    public bool OpSetPropertiesOfRoom(Hashtable gameProperties, bool broadcast, byte channelId)
    {
        if (DebugOut >= DebugLevel.INFO)
        {
            Listener.DebugReturn(DebugLevel.INFO, "OpSetPropertiesOfRoom()");
        }
        var customOpParameters = new Dictionary<byte, object>();
        customOpParameters.Add(251, gameProperties);
        if (broadcast)
        {
            customOpParameters.Add(250, true);
        }
        return SendOperation(252, customOpParameters, new SendOptions() { Reliability = broadcast, Channel = channelId });
    }

    protected void OpSetPropertyOfRoom(byte propCode, object value)
    {
        var gameProperties = new Hashtable();
        gameProperties[propCode] = value;
        OpSetPropertiesOfRoom(gameProperties, true, 0);
    }
}