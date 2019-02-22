using System.Text.RegularExpressions;
using UnityEngine;

namespace GGM
{
    public class RichPresence : MonoBehaviour
    {
        public static string Details = string.Empty;
        public static string State = string.Empty;

        public static bool IsRunning = false;

        static float _time = 0f;

        private static string _clientID = "546067288093097994";
        public static DiscordRpc.RichPresence _presence;
        public static DiscordRpc.EventHandlers _handlers;

        public static void Start()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                _presence = new DiscordRpc.RichPresence
                {
                    details = "GucciGangMod",
                    largeImageKey = "image_large",
                    largeImageText = "github.com/JustlPain/GucciGangMod",
                    smallImageKey = "image_large",
                    smallImageText = Settings.Version
                };
                _handlers = default(DiscordRpc.EventHandlers);
                DiscordRpc.Initialize(_clientID, ref _handlers, true, null);
                DiscordRpc.UpdatePresence(_presence);
                UpdateStatus();
            }
        }

        public void Update()
        {
            _time += Time.deltaTime;
            if (_time > 1f)
            {
                DiscordRpc.UpdatePresence(_presence);
                _time = 0f;
            }
        }

        public static void UpdateStatus()
        {
            if (!PhotonNetwork.inRoom)
            {
                if (PhotonNetwork.insideLobby)
                {
                    _presence.details = "Lobby";
                    _presence.state = Regex.Replace(PhotonNetwork.ServerAddress, "app\\-|\\.exitgamescloud\\.com|\\:\\d+", "").ToUpper();
                    _presence.partySize = 0;
                    _presence.partyMax = 0;
                }
                else if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.STOP)
                {
                    _presence.details = "Singleplayer";
                    _presence.state = $"{FengGameManagerMKII.level}/{Extensions.GetDifficulty()}/{Extensions.GetDayLight()}";
                    _presence.partySize = FengGameManagerMKII.single_kills;
                    _presence.partyMax = FengGameManagerMKII.single_totalDamage;
                }
                else
                {
                    _presence.details = "GucciGangMod";
                    _presence.state = "Main Menu";
                    _presence.partySize = 0;
                    _presence.partyMax = 0;
                }
            }
            else
            {
                var text = PhotonNetwork.room.name.Split(new char[]{'`'})[0].Trim();
                _presence.details = (text.Length > 20) ? (text.Remove(17) + "...") : text;
                _presence.state = $"{FengGameManagerMKII.level}/{Extensions.GetDifficulty()}/{Extensions.GetDayLight()}";
                _presence.partySize = PhotonNetwork.room.playerCount;
                _presence.partyMax = PhotonNetwork.room.maxPlayers;
            }
            DiscordRpc.UpdatePresence(_presence);
        }
    }
}