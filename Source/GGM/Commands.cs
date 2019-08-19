using ExitGames.Client.Photon;
using GGM.Caching;
using GGM.Config;
using System;
using System.Linq;
using UnityEngine;
using static InRoomChat;

namespace GGM
{
    internal static class Commands
    {
        public static void ASODamage()
        {
            Settings.CustomSizeSetting.Value = true;
            Settings.SizeSettings[0].Value = 100f;
            Settings.SizeSettings[0].Value = 0f;
            Settings.SizeSettings[0].Value = 0f;
            Settings.SizeSettings[0].Value = 0f;
            Settings.SizeSettings[0].Value = 0f;
            Settings.ArmorModeSetting.Value = true;
            Settings.ArmorSetting.Value = 1000;
            Settings.CustomSizeSetting.Value = true;
            Settings.SizeSettings[0].Value = 2.5f;
            Settings.SizeSettings[1].Value = 3f;
            SystemMessageGlobal("ASO Damage enabled.");
        }

        public static void ASOKDR()
        {
            RCSettings.asoPreservekdr = RCSettings.asoPreservekdr == 0 ? 1 : 0;
            SystemMessageGlobal("KDRs will " + (RCSettings.asoPreservekdr == 1 ? string.Empty : "not ") + "be preserved from disconnects.");
        }

        public static void ASORacing()
        {
            RCSettings.racingStatic = RCSettings.racingStatic == 0 ? 1 : 0;
            SystemMessageLocal("Restart required.");
        }

        public static void Ban(string id)
        {
            if (MCRequired()) return;
            foreach (var p in id.Split(','))
            {
                if (Convert.ToInt32(p) == PhotonNetwork.player.ID)
                {
                    SystemMessageLocal(Error(2, "ban"));
                }
                else
                {
                    foreach (var player in PhotonNetwork.playerList)
                    {
                        if (Convert.ToInt32(p) == player.ID)
                        {
                            if (FengGameManagerMKII.OnPrivateServer)
                            {
                                FengGameManagerMKII.FGM.kickPlayerRC(player, true, "");
                            }
                            else if (PhotonNetwork.isMasterClient)
                            {
                                FengGameManagerMKII.FGM.kickPlayerRC(player, true, "");
                                SystemMessageGlobal(player, "has been banned.");
                            }
                        }
                    }

                    if (PhotonPlayer.Find(Convert.ToInt32(p)) == null)
                    {
                        SystemMessageLocal(Error(1));
                    }
                }
            }
        }

        public static void ClearChat(bool local = true)
        {
            Chat.Clear();
            if (!local)
            {
                for (var i = 0; i < 15; i++)
                {
                    FengGameManagerMKII.FGM.photonView.RPC("Chat", PhotonTargets.Others, string.Empty, string.Empty);
                }
            }
        }

        public static void GetPosition()
        {
            string[] msg = { "Your position:\n", "\nX", " - ", $"{GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().main_object.transform.position.x.ToString()}" + "\nY", " - ", $"{GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().main_object.transform.position.y.ToString()}" + "\nZ", " - ", $"{GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().main_object.transform.position.z.ToString()}" };
            SystemMessageLocal(msg);
        }

        public static void Ignore(string str)
        {
            var ignoreplayer = PhotonPlayer.Find(Convert.ToInt32(str));
            if (ignoreplayer != null && !ignoreplayer.isLocal && !ignoreplayer.GucciLab)
            {
                FengGameManagerMKII.ignoreList.Add(ignoreplayer.ID);
                SystemMessageLocal(ignoreplayer, " in ignore list now.");
            }
            else if (ignoreplayer.isLocal)
            {
                SystemMessageLocal(Error(2, "ignore"));
            }
            else
            {
                SystemMessageLocal(Error(1));
            }

        }

        public static void Kick(string id)
        {
            if (MCRequired()) return;

            foreach (var p in id.Split(','))
            {
                if (Convert.ToInt32(p) == PhotonNetwork.player.ID)
                {
                    SystemMessageLocal(Error(2, "kick"));
                }
                else
                {
                    foreach (var player in PhotonNetwork.playerList)
                    {
                        if (Convert.ToInt32(p) == player.ID)
                        {
                            if (FengGameManagerMKII.OnPrivateServer)
                            {
                                FengGameManagerMKII.FGM.kickPlayerRC(player, false, "");
                            }
                            else if (PhotonNetwork.isMasterClient)
                            {
                                FengGameManagerMKII.FGM.kickPlayerRC(player, false, "");
                                SystemMessageLocal(player, "has been kicked.");
                            }
                        }
                    }

                    if (PhotonPlayer.Find(Convert.ToInt32(p)) == null)
                    {
                        SystemMessageLocal(Error(1));
                    }
                }
            }
        }

        public static bool MCRequired()
        {
            if (!PhotonNetwork.isMasterClient)
            {
                SystemMessageLocal(Error(0));
            }

            return !PhotonNetwork.isMasterClient;
        }

        public static void Mute(PhotonPlayer player)
        {
            var name = player.Name.StripHEX();
            if (!Settings.MutedPlayers.Contains(name))
            {
                Settings.MutedPlayers.Add(name);
                SystemMessageLocal(player, "has been muted");
            }
            else
            {
                SystemMessageLocal(player, "already muted.");
            }
        }

        public static void MuteList()
        {
            SystemMessageLocal("Muted Players:");
            foreach (var player in Settings.MutedPlayers)
            {
                SystemMessageLocal(player);
            }
        }

        public static void Reconnect()
        {
            FengGameManagerMKII.NeedRejoin = true;
            PhotonNetwork.Disconnect();
        }

        public static void ResetKD(string id = "", bool self = true, bool global = false)
        {
            if (self)
            {
                PhotonNetwork.player.SetCustomProperties(new Hashtable { { "kills", 0 }, { "deaths", 0 }, { "max_dmg", 0 }, { "total_dmg", 0 } });
                SystemMessageGlobal("Your stats have been reset.");
            }

            if (!global && !self)
            {
                if (Convert.ToInt32(id) != PhotonNetwork.player.ID)
                    if (MCRequired())
                        return;
                foreach (var p in id.Split(','))
                {
                    var player = PhotonPlayer.Find(Convert.ToInt32(p));
                    player.SetCustomProperties(new Hashtable { { "kills", 0 }, { "deaths", 0 }, { "max_dmg", 0 }, { "total_dmg", 0 } });
                    SystemMessageGlobal(player, "stats have been reset.");
                }
            }
            else
            {
                if (MCRequired()) return;

                var hash = new Hashtable { { "kills", 0 }, { "deaths", 0 }, { "max_dmg", 0 }, { "total_dmg", 0 } };
                foreach (var player in PhotonNetwork.playerList)
                {
                    player.SetCustomProperties(hash);
                }

                SystemMessageGlobal("All stats have been reset.");
            }
        }

        public static void Restart()
        {
            if (MCRequired()) return;

            FengGameManagerMKII.FGM.restartGame(false);
            string[] msg = { "MasterClient ", "has restarted the game." };
            SystemMessageLocal(msg, false);
        }

        public static void Revive(string id = "", bool all = false)
        {
            if (MCRequired()) return;
            if (!all)
            {
                foreach (var p in id.Split(','))
                {
                    var player = PhotonPlayer.Find(Convert.ToInt32(p));
                    Antis.RemoveFromAntiRevive(player.ID);
                    FengGameManagerMKII.FGM.photonView.RPC("respawnHeroInNewRound", player);
                    if (PhotonNetwork.isMasterClient) SystemMessageGlobal(player, "has been revived.");
                    else SystemMessageLocal(player, "has been revived.");
                }
            }
            else
            {
                Antis.ClearAntiRevive();
                FengGameManagerMKII.FGM.photonView.RPC("respawnHeroInNewRound", PhotonTargets.All);
                SystemMessageGlobal("All players have been revived.");
            }
        }

        public static void RoomClose(bool state)
        {
            if (MCRequired())
            {
                return;
            }
            PhotonNetwork.room.open = !state;
            if (state)
            {
                FengGameManagerMKII.FGM.photonView.RPC("showResult", PhotonTargets.Others, new object[] {new string[6].Select(x => "[000000]Closed").ToArray()});
            }
            SystemMessageLocal(new[]{"Room is ", state ? "Closed" : "Opened", " now."});
        }
        public static void RoomHide(bool state)
        {
            if (MCRequired())
            {
                return;
            }
            PhotonNetwork.room.visible = !state;
            SystemMessageLocal(new []{"Room is ", (!state ? "Visible" : "Hidden"), " now."});
        }


        public static void Rules()
        {
            if (Settings.LegacyChatSetting)
            {
                AddLineRC("Currently activated gamemodes:");
                if (RCSettings.bombMode > 0)
                {
                    AddLineRC("Bomb mode is on.");
                }

                if (RCSettings.teamMode > 0)
                {
                    if (RCSettings.teamMode == 1)
                    {
                        AddLineRC("Team mode is on (no sort).");
                    }
                    else if (RCSettings.teamMode == 2)
                    {
                        AddLineRC("Team mode is on (sort by size).");
                    }
                    else if (RCSettings.teamMode == 3)
                    {
                        AddLineRC("Team mode is on (sort by skill).");
                    }
                }

                if (RCSettings.pointMode > 0)
                {
                    AddLineRC("Point mode is on (" + Convert.ToString(RCSettings.pointMode) + ").");
                }

                if (RCSettings.disableRock > 0)
                {
                    AddLineRC("Punk Rock-Throwing is disabled.");
                }

                if (RCSettings.spawnMode > 0)
                {
                    AddLineRC("Custom spawn rate is on (" + RCSettings.nRate.ToString("F2") + "% Normal, " + RCSettings.aRate.ToString("F2") + "% Abnormal, " + RCSettings.jRate.ToString("F2") + "% Jumper, " + RCSettings.cRate.ToString("F2") + "% Crawler, " + RCSettings.pRate.ToString("F2") + "% Punk");
                }

                if (RCSettings.explodeMode > 0)
                {
                    AddLineRC("Titan explode mode is on (" + Convert.ToString(RCSettings.explodeMode) + ").");
                }

                if (RCSettings.healthMode > 0)
                {
                    AddLineRC("Titan health mode is on (" + Convert.ToString(RCSettings.healthLower) + "-" + Convert.ToString(RCSettings.healthUpper) + ").");
                }

                if (RCSettings.infectionMode > 0)
                {
                    AddLineRC("Infection mode is on (" + Convert.ToString(RCSettings.infectionMode) + ").");
                }

                if (RCSettings.damageMode > 0)
                {
                    AddLineRC("Minimum nape damage is on (" + Convert.ToString(RCSettings.damageMode) + ").");
                }

                if (RCSettings.moreTitans > 0)
                {
                    AddLineRC("Custom titan # is on (" + Convert.ToString(RCSettings.moreTitans) + ").");
                }

                if (RCSettings.sizeMode > 0)
                {
                    AddLineRC("Custom titan size is on (" + RCSettings.sizeLower.ToString("F2") + "," + RCSettings.sizeUpper.ToString("F2") + ").");
                }

                if (RCSettings.banEren > 0)
                {
                    AddLineRC("Anti-Eren is on. Using Titan eren will get you kicked.");
                }

                if (RCSettings.waveModeOn == 1)
                {
                    AddLineRC("Custom wave mode is on (" + Convert.ToString(RCSettings.waveModeNum) + ").");
                }

                if (RCSettings.friendlyMode > 0)
                {
                    AddLineRC("Friendly-Fire disabled. PVP is prohibited.");
                }

                if (RCSettings.pvpMode > 0)
                {
                    if (RCSettings.pvpMode == 1)
                    {
                        AddLineRC("AHSS/Blade PVP is on (team-based).");
                    }
                    else if (RCSettings.pvpMode == 2)
                    {
                        AddLineRC("AHSS/Blade PVP is on (FFA).");
                    }
                }

                if (RCSettings.maxWave > 0)
                {
                    AddLineRC("Max Wave set to " + RCSettings.maxWave.ToString());
                }

                if (RCSettings.horseMode > 0)
                {
                    AddLineRC("Horses are enabled.");
                }

                if (RCSettings.ahssReload > 0)
                {
                    AddLineRC("AHSS Air-Reload disabled.");
                }

                if (RCSettings.punkWaves > 0)
                {
                    AddLineRC("Punk override every 5 waves enabled.");
                }

                if (RCSettings.endlessMode > 0)
                {
                    AddLineRC("Endless Respawn is enabled (" + RCSettings.endlessMode.ToString() + " seconds).");
                }

                if (RCSettings.globalDisableMinimap > 0)
                {
                    AddLineRC("Minimap are disabled.");
                }

                if (RCSettings.motd != string.Empty)
                {
                    AddLineRC("MOTD:" + RCSettings.motd);
                }

                if (RCSettings.deadlyCannons > 0)
                {
                    AddLineRC("Cannons will kill humans.");
                }
            }
            else
            {
                if (RCSettings.bombMode > 0)
                {
                    string[] msg = { "Bomb mode is enabled." };
                    SystemMessageLocal(msg, true);
                }

                if (RCSettings.teamMode > 0)
                {
                    var sort = "Unsorted";
                    if (RCSettings.teamMode == 2)
                    {
                        sort = "Sorted by size";
                    }
                    else if (RCSettings.teamMode == 3)
                    {
                        sort = "Sorted by skill";
                    }

                    string[] msg = { "Team mode is enabled. ", sort, "." };
                    SystemMessageLocal(msg, true);
                }

                if (RCSettings.pointMode > 0)
                {
                    string[] msg = { "Points limit is ", $"[{Convert.ToString(RCSettings.pointMode)}]", "." };
                    SystemMessageLocal(msg, true);
                }

                if (RCSettings.disableRock > 0)
                {
                    string[] msg = { "Punks Rock-Throwing is disabled." };
                    SystemMessageLocal(msg, true);
                }

                if (RCSettings.spawnMode > 0)
                {
                    string[] msg = { "Custom Spawn Rate is:", $"\n[{RCSettings.nRate.ToString("F2")}% Normal]" + $"\n[{RCSettings.aRate.ToString("F2")}% Abnormal]" + $"\n[{RCSettings.jRate.ToString("F2")}% Jumper]" + $"\n[{RCSettings.cRate.ToString("F2")}% Crawler]" + $"\n[{RCSettings.pRate.ToString("F2")}% Punk]" };
                    SystemMessageLocal(msg, true);
                }

                if (RCSettings.explodeMode > 0)
                {
                    string[] msg = { "Explode radius is ", $"[{Convert.ToString(RCSettings.explodeMode)}]", "." };
                    SystemMessageLocal(msg, true);
                }

                if (RCSettings.healthMode > 0)
                {
                    var mode = "Static ";
                    if (RCSettings.healthMode == 2)
                    {
                        mode = "Scaled ";
                    }

                    string[] msg = { mode + "Health amount is ", $"[{Convert.ToString(RCSettings.healthLower)} - {Convert.ToString(RCSettings.healthUpper)}]", "." };
                    SystemMessageLocal(msg, true);
                }

                if (RCSettings.infectionMode > 0)
                {
                    string[] msg = { "Infection mode with ", $"[{Convert.ToString(RCSettings.infectionMode)}]", " infected on start." };
                    SystemMessageLocal(msg, true);
                }

                if (RCSettings.damageMode > 0)
                {
                    string[] msg = { "Minimum Nape Damage is ", $"[{Convert.ToString(RCSettings.damageMode)}]", "." };
                    SystemMessageLocal(msg, true);
                }

                if (RCSettings.moreTitans > 0)
                {
                    string[] msg = { "Custom Titans Amount is ", $"[{Convert.ToString(RCSettings.moreTitans)}]", "." };
                    SystemMessageLocal(msg, true);
                }

                if (RCSettings.sizeMode > 0)
                {
                    string[] msg = { "Custom Titans Size is ", $"[{RCSettings.sizeLower.ToString("F2")} - {RCSettings.sizeUpper.ToString("F2")}]", "." };
                    SystemMessageLocal(msg, true);
                }

                if (RCSettings.banEren > 0)
                {
                    string[] msg = { "Anti-Eren mode is enabled." };
                    SystemMessageLocal(msg, true);
                }

                if (RCSettings.waveModeOn == 1)
                {
                    string[] msg = { "Custom Titans/Wave amount is ", $"[{Convert.ToString(RCSettings.waveModeNum)}]", "." };
                    SystemMessageLocal(msg, true);
                }

                if (RCSettings.friendlyMode > 0)
                {
                    string[] msg = { "Friendly mode is enabled." };
                    SystemMessageLocal(msg, true);
                }

                if (RCSettings.pvpMode > 0)
                {
                    var mode = "";
                    if (RCSettings.pvpMode == 1)
                    {
                        mode = "Team ";
                    }
                    else if (RCSettings.pvpMode == 2)
                    {
                        mode = "FFA ";
                    }

                    string[] msg = { mode + "PVP mode is enabled." };
                    SystemMessageLocal(msg, true);
                }

                if (RCSettings.maxWave > 0)
                {
                    string[] msg = { "Custom Maximum Wave is ", $"[{RCSettings.maxWave.ToString()}]", "." };
                    SystemMessageLocal(msg, true);
                }

                if (RCSettings.horseMode > 0)
                {
                    string[] msg = { "Horses are enabled." };
                    SystemMessageLocal(msg, true);
                }

                if (RCSettings.ahssReload > 0)
                {
                    string[] msg = { "AHSS Air-Reloading is disabled." };
                    SystemMessageLocal(msg, true);
                }

                if (RCSettings.punkWaves > 0)
                {
                    string[] msg = { "Punk Waves Override is enabled." };
                    SystemMessageLocal(msg, true);
                }

                if (RCSettings.endlessMode > 0)
                {
                    string[] msg = { "Endless Respawn is ", $"[{RCSettings.endlessMode.ToString()}]", " seconds." };
                    SystemMessageLocal(msg, true);
                }

                if (RCSettings.globalDisableMinimap > 0)
                {
                    string[] msg = { "Minimaps are disabled." };
                    SystemMessageLocal(msg, true);
                }

                if (RCSettings.deadlyCannons > 0)
                {
                    string[] msg = { "Deadly Cannons mode is enabled." };
                    SystemMessageLocal(msg, true);
                }

                if (RCSettings.motd != string.Empty)
                {
                    string[] msg = { "MOTD:\n", RCSettings.motd };
                    SystemMessageLocal(msg, false);
                }
            }
        }

        public static void SetSlots(int count)
        {
            if (MCRequired()) return;

            PhotonNetwork.room.maxPlayers = count;
            string[] msg = { "Max players changed to ", count.ToString(), "." };
            SystemMessageGlobal(msg);
        }

        public static void SetTime(int t)
        {
            if (MCRequired()) return;

            var time = (FengGameManagerMKII.FGM.time - (int)FengGameManagerMKII.FGM.timeTotalServer - t) * -1f;
            FengGameManagerMKII.FGM.addTime(time);
            string[] msg = { "Time set to ", time.ToString(), "." };
            SystemMessageGlobal(msg);
        }

        public static void SpectatorMode()
        {
            if ((int)FengGameManagerMKII.settings[245] == 0)
            {
                FengGameManagerMKII.settings[245] = 1;
                FengGameManagerMKII.FGM.EnterSpecMode(true);
                string[] msg = { "You have entered ", "Spectator ", "mode." };
                SystemMessageLocal(msg);
            }
            else
            {
                FengGameManagerMKII.settings[245] = 0;
                FengGameManagerMKII.FGM.EnterSpecMode(false);
                string[] msg = { "You have exited ", "Spectator ", "mode." };
                SystemMessageLocal(msg);
            }
        }

        public static void SwitchTeam(string str)
        {
            if (RCSettings.teamMode != 1)
            {
                string[] msg = { "Teams ", "are locked or disabled." };
                SystemMessageLocal(msg, false);
                return;
            }

            var teamValue = 0;
            var newTeamName = "Individuals";
            switch (str)
            {
                case "0":
                case "individual":
                    break;

                case "1":
                case "cyan":
                    teamValue = 1;
                    newTeamName = "Cyan";
                    break;

                case "2":
                case "magenta":
                    teamValue = 2;
                    newTeamName = "Magenta";
                    break;

                default:
                    string[] err = { "Invalid team code/name. Possibles:\n" + "Team Individuals - ", "0", "/", "individuals.\n", "Team Cyan - ", "1", "/", "cyan.\n", "Team Magenta - ", "2", "/", "magenta." };
                    SystemMessageLocal(err);
                    return;
            }

            FengGameManagerMKII.FGM.photonView.RPC("setTeamRPC", PhotonNetwork.player, teamValue);
            string[] msg2 = { "You have joined ", "Team " + newTeamName, "." };
            SystemMessageLocal(msg2);
            foreach (var obj in FengGameManagerMKII.FGM.getPlayers())
            {
                var her = (HERO)obj;
                if (her.photonView.isMine)
                {
                    her.markDie();
                    her.photonView.RPC("netDie2", PhotonTargets.All, -1, "Team Switch");
                    break;
                }
            }
        }

        public static void Teleport(int id)
        {
            var player = PhotonPlayer.Find(id);
            var obj = new GameObject();
            var obj2 = new GameObject();
            var players = GameObject.FindGameObjectsWithTag("Player");
            foreach (var obj3 in players)
            {
                if (obj3.GetPhotonView().owner == player)
                {
                    obj = obj3;
                }

                if (obj3.GetPhotonView().owner == PhotonNetwork.player)
                {
                    obj2 = obj3;
                }
            }

            SystemMessageLocal("Teleported to ", player, ".");
            obj2.transform.position = obj.transform.position;
        }

        public static void Unban(string id)
        {
            if (MCRequired())
            {
                return;
            }

            if (FengGameManagerMKII.OnPrivateServer)
            {
                FengGameManagerMKII.ServerRequestUnban(id);
            }
            else if (PhotonNetwork.isMasterClient)
            {
                var unbanplayer = Convert.ToInt32(id);
                if (FengGameManagerMKII.banHash.ContainsKey(unbanplayer))
                {
                    SystemMessageGlobal(PhotonPlayer.Find(unbanplayer), " has been unbanned from the server.");
                    FengGameManagerMKII.banHash.Remove(unbanplayer);
                }
                else
                {
                    SystemMessageLocal(Error(1));
                }
            }
        }

        public static void Unmute(PhotonPlayer player)
        {
            var name = player.Name.StripHEX();
            if (Settings.MutedPlayers.Contains(name))
            {
                Settings.MutedPlayers.Remove(name);
                SystemMessageLocal(player, "has been unmuted");
            }
            else
            {
                SystemMessageLocal(player, "not muted.");
            }
        }
    }
}