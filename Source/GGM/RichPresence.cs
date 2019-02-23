using UnityEngine;

namespace GGM
{
    class RichPresence : MonoBehaviour
    {
        private const string _clientID = "548511470443560960";

        private static string _largeImageKey;

        private static DiscordAPI.RichPresence _presence;

        void Awake()
        {
            var handlers = new DiscordAPI.EventHandlers();
            handlers.readyCallback += () => { };
            handlers.disconnectedCallback += (a, b) => { };
            handlers.errorCallback += (a, b) => { Debug.Log(b); };
            handlers.joinCallback += (a) => { };
            handlers.requestCallback += (ref DiscordAPI.JoinRequest a) => { };
            handlers.spectateCallback += (a) => { };

            DiscordAPI.Initialize(_clientID, ref handlers, true, null);
            _presence = new DiscordAPI.RichPresence
            {
                details = "Main Menu",
                state = "Version v4.2.23",
                largeImageKey = GetImage(),
                largeImageText = "Art by https://vk.com/bishoptyan",
                smallImageKey = "logo_small",
                smallImageText = "github.com/JustlPain/GucciGangMod",
                partySize = 0,
                partyMax = 0
            };
            DiscordAPI.UpdatePresence(_presence);
        }

        public static void Update()
        {
            DiscordAPI.RunCallbacks();
        }

        public static void UpdateStatus()
        {
            if (!PhotonNetwork.inRoom)
            {
                if (PhotonNetwork.insideLobby)
                {
                    _presence.details = "Lobby";
                    _presence.state = Extensions.GetLobbyName();
                    _presence.partySize = 0;
                    _presence.partyMax = 0;
                }
                else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                {
                    _presence.details = "Singleplayer";
                    _presence.largeImageKey = GetImage();
                    _presence.largeImageText = $"{FengGameManagerMKII.level}/{Extensions.GetDifficulty()}/{Extensions.GetDayLight()}";
                    _presence.state = $"{FengGameManagerMKII.single_kills}/{FengGameManagerMKII.single_maxDamage}/{FengGameManagerMKII.single_totalDamage}";
                    _presence.partySize = 0;
                    _presence.partyMax = 0;
                }
                else
                {
                    _presence.details = "Main Menu";
                    _presence.state = "Version v4.2.23";
                    _presence.largeImageKey = GetImage();
                    _presence.largeImageText = "Art by https://vk.com/bishoptyan";
                    _presence.partySize = 0;
                    _presence.partyMax = 0;
                }
            }
            else
            {
                _presence.details = "Multiplayer";
                _presence.state = (Extensions.GetRoomName().Length > 14) ? (Extensions.GetRoomName().Remove(12) + "...") : Extensions.GetRoomName();
                _presence.largeImageKey = GetImage();
                _presence.largeImageText = $"{FengGameManagerMKII.level}/{Extensions.GetDifficulty()}/{Extensions.GetDayLight()}";
                _presence.partySize = PhotonNetwork.room.playerCount;
                _presence.partyMax = PhotonNetwork.room.maxPlayers;
            }
            DiscordAPI.UpdatePresence(_presence);
        }

        private static string GetImage()
        {
            _largeImageKey = "logo_large";
            for (int i = 0; i < Extensions.Locations.Length; i++)
            {
                if (FengGameManagerMKII.level.ToLower().Contains(Extensions.Locations[i]))
                {
                    _largeImageKey = Extensions.Locations[i];
                    break;
                }
            }
            if ((FengGameManagerMKII.level.ToLower().Contains("forest") || FengGameManagerMKII.level.ToLower().Contains("city")) && _largeImageKey != "logo_large")
            {
                return $"{_largeImageKey}_{Extensions.GetDayLight().ToLower()}";
            }
            else
            return _largeImageKey;
        }
    }
}