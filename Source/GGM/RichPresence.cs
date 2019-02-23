using System.Text.RegularExpressions;
using DiscordRPC;
using UnityEngine;

namespace GGM
{
    public class RichPresence : MonoBehaviour
    {
        private static DiscordRpcClient _client;
        private const string _clientID = "548511470443560960";
        private static DiscordRPC.RichPresence _presence;

        public static void Start()
        {
            _client = new DiscordRpcClient(_clientID, null, false, -1);

            _presence = new DiscordRPC.RichPresence()
            {
                Details = "Main Menu",
                State = "Idle",
                Assets = new Assets()
                {
                    LargeImageKey = "image_large",
                    LargeImageText = "github.com/JustlPain/GucciGangMod",
                },
                Party = new Party()
                {
                    Size = 0,
                    Max = 0
                },
            };

            _client.Initialize();

            _client.SetPresence(_presence);
        }

        public void Update()
        {
            if (_client != null)
                _client.Invoke();
            else
                Debug.Log("CLIENT IS NULL\nTI OPYAT POSOSAL");

            UpdateStatus();
        }

        void OnDisable()
        {
            _client.Dispose();
        }

        public static void UpdateStatus()
        {
            if (!PhotonNetwork.inRoom)
            {
                if (PhotonNetwork.insideLobby)
                {
                    _presence.Details = "Lobby";
                    _presence.State = Extensions.GetLobbyName();
                    _presence.Party.Size = 0;
                    _presence.Party.Max = 0;                    
                }
                else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                {
                    _presence.Details = "Singleplayer";
                    _presence.State = $"{FengGameManagerMKII.level.ToUpper()}/{Extensions.GetDifficulty()}/{Extensions.GetDayLight()}";
                    _presence.Party.Size = FengGameManagerMKII.single_kills;
                    _presence.Party.Max = FengGameManagerMKII.single_totalDamage;
                }
                else
                {
                    _presence.Details = "Main Menu";
                    _presence.State = "Idle";
                    _presence.Party.Size = 0;
                    _presence.Party.Max = 0;
                }
            }
            else
            {
                _presence.Details = "Multiplayer";
                _presence.State = (Extensions.GetRoomName().Length > 14) ? (Extensions.GetRoomName().Remove(12) + "...") : Extensions.GetRoomName();
                _presence.Party.Size = PhotonNetwork.room.playerCount;
                _presence.Party.Max = PhotonNetwork.room.maxPlayers;
            }
            _client.SetPresence(_presence);
        }
    }
}