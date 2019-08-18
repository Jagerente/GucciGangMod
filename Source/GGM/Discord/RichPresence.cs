using System;
using System.IO;
using System.Net;
using UnityEngine;

namespace GGM.Discord
{
    internal class RichPresence : MonoBehaviour
    {
        private static readonly string[] Locations = { "akina", "annie", "cave", "city", "colossal", "forest", "house", "outside", "tutorial" };

        private const string ClientId = "598429802692870145";

        private static string largeImageKey;

        private static DiscordAPI.RichPresence presence;

        private void Awake()
        {
            var handlers = new DiscordAPI.EventHandlers();
            handlers.readyCallback += () => { Debug.Log("[RichPresence] Ready."); };
            handlers.disconnectedCallback += (a, b) => { Debug.Log("[RichPresence] Disconnected."); };
            handlers.errorCallback += (a, b) => { Debug.Log("[RichPresence] Error " + a + ": "+ b); };
            handlers.joinCallback += (a) => { };
            handlers.requestCallback += (ref DiscordAPI.JoinRequest a) => { };
            handlers.spectateCallback += (a) => { };

            DiscordAPI.Initialize(ClientId, ref handlers, true, null);
            presence = new DiscordAPI.RichPresence
            {
                details = "Main Menu",
                state = $"Version {UIMainReferences.Version}",
                largeImageKey = GetImage(),
                largeImageText = "Art by https://vk.com/bishoptyan",
                smallImageKey = "logo_small",
                smallImageText = "github.com/Jagerente/GucciGangMod",
                startTimestamp = DateTime.Now.Ticks,
                partySize = 0,
                partyMax = 0
            };
            DiscordAPI.UpdatePresence(presence);
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
                    presence.details = "Lobby";
                    presence.state = PhotonNetwork.GetLobbyName();
                    presence.partySize = 0;
                    presence.partyMax = 0;
                }
                else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                {
                    presence.details = "Singleplayer";
                    presence.largeImageKey = GetImage();
                    presence.largeImageText = $"{FengGameManagerMKII.level}/{IN_GAME_MAIN_CAMERA.GetDifficulty()}/{IN_GAME_MAIN_CAMERA.GetDayLight()}";
                    presence.state = $"{FengGameManagerMKII.single_kills}/{FengGameManagerMKII.single_maxDamage}/{FengGameManagerMKII.single_totalDamage}";
                    presence.partySize = 0;
                    presence.partyMax = 0;
                }
                else
                {
                    presence.details = "Main Menu";
                    presence.state = $"Version {UIMainReferences.Version}";
                    presence.largeImageKey = "logo_large";
                    presence.largeImageText = "Art by https://vk.com/bishoptyan";
                    presence.partySize = 0;
                    presence.partyMax = 0;
                }
            }
            else
            {
                presence.details = "Multiplayer";
                presence.state = PhotonNetwork.GetRoomName().Length > 14 ? PhotonNetwork.GetRoomName().Remove(12) + "..." : PhotonNetwork.GetRoomName();
                presence.largeImageKey = GetImage();
                presence.largeImageText = $"{FengGameManagerMKII.level}/{IN_GAME_MAIN_CAMERA.GetDifficulty()}/{IN_GAME_MAIN_CAMERA.GetDayLight()}";
                presence.partySize = PhotonNetwork.room.playerCount;
                presence.partyMax = PhotonNetwork.room.maxPlayers;
            }

            DiscordAPI.UpdatePresence(presence);
        }

        private static string GetImage()
        {
            largeImageKey = "logo_large";
            foreach (var location in Locations)
            {
                if (FengGameManagerMKII.level.ToLower().Contains(location))
                {
                    largeImageKey = location;
                    break;
                }
            }

            if ((FengGameManagerMKII.level.ToLower().Contains("forest") || FengGameManagerMKII.level.ToLower().Contains("city")) && largeImageKey != "logo_large")
            {
                return $"{largeImageKey}_{IN_GAME_MAIN_CAMERA.GetDayLight().ToLower()}";
            }

            return largeImageKey;
        }
    }
}