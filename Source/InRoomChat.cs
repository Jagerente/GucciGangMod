using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using GGM;
using MonoBehaviour = Photon.MonoBehaviour;
using GGM.Config;
using System.Text;

public class InRoomChat : MonoBehaviour
{
    private readonly bool AlignBottom = true;
    internal static InRoomChat Chat;
    public static readonly string ChatRPC = "Chat";
    public static Rect GuiRect = new Rect(0f, 100f, 300f, 470f);
    public static Rect GuiRect2 = new Rect(30f, 575f, 300f, 25f);
    private string inputLine = string.Empty;
    public static List<string> Messages = new List<string>();
    private Vector2 scrollPos = Vector2.zero;

    public static void AddLine(string newLine = "")
    {
        Messages.Add(newLine);
    }
    
    /// <param name="type">
    /// 0 - Not MC
    /// 1 - Existance
    /// 2 - Self cast
    /// </param>
    /// <param name="text">Cast</param>
    public static string Error(int type, string text = "")
    {
        switch (type)
        {
            case 0:
                return "You are not MasterClient.";
            case 1:
                return "No such Player.";
            case 2:
                return string.Concat("You can't ", text, " yourself.");
            default:
                return "Unknown error.";
        }
    }

    public static string ChatFormatting(string text, string color, bool bold, bool italic, string size = "")
    {
        return "<color=#" + color + ">" + 
            (size != string.Empty ? "<size=" + size + ">" : string.Empty) + 
            (bold ? "<b>" : string.Empty) + 
            (italic ? "<i>" : string.Empty) + 
            text + 
            (italic ? "</i>" : string.Empty) + 
            (bold ? "</b>" : string.Empty) + 
            (size != "" ? "</size>" : string.Empty) + 
            "</color>";
    }

    public static void SystemMessageLocal(string str, bool major = true)
    {
        Messages.Add(
            ChatFormatting(
                str,
                major ? Settings.ChatMajorColor : Settings.ChatMinorColor,
                major ? Settings.ChatMajorBold : Settings.ChatMinorBold,
                major ? Settings.ChatMajorItalic : Settings.ChatMinorItalic,
                Settings.ChatSize));
    }

    public static void SystemMessageLocal(string[] str, bool parity = true)
    {
        StringBuilder msg = new StringBuilder();
        for (int i = 0; i < str.Length; i++)
        {
            if (i % 2 == 0 || i == 0)
            {
                msg.Append(ChatFormatting(
                str[i],
                parity ? Settings.ChatMajorColor : Settings.ChatMinorColor,
                parity ? Settings.ChatMajorBold : Settings.ChatMinorBold,
                parity ? Settings.ChatMajorItalic : Settings.ChatMinorItalic,
                Settings.ChatSize));
            }
            else
            {
                msg.Append(ChatFormatting(
                str[i],
                parity ? Settings.ChatMinorColor : Settings.ChatMajorColor,
                parity ? Settings.ChatMinorBold : Settings.ChatMajorBold,
                parity ? Settings.ChatMinorItalic: Settings.ChatMajorItalic,
                Settings.ChatSize));
            }
        }

        Messages.Add(msg.ToString());
    }

    public static void SystemMessageLocal(string str, PhotonPlayer player)
    {
        Messages.Add(
            ChatFormatting(
                str, 
                Settings.ChatMajorColor, 
                Settings.ChatMajorBold, 
                Settings.ChatMajorItalic, 
                Settings.ChatSize) +
            ChatFormatting(
                $" [{player.ID}] {player.Name.hexColor()}", 
                Settings.ChatMinorColor, 
                Settings.ChatMinorBold, 
                Settings.ChatMinorItalic, 
                Settings.ChatSize) +
            ChatFormatting(
                ".", 
                Settings.ChatMajorColor, 
                Settings.ChatMajorBold, 
                Settings.ChatMajorItalic, 
                Settings.ChatSize));
    }

    public static void SystemMessageLocal(PhotonPlayer player, string str)
    {
        Messages.Add(
            ChatFormatting(
                $"[{player.ID}] {player.Name.hexColor()} ", 
                Settings.ChatMinorColor, 
                Settings.ChatMinorBold, 
                Settings.ChatMinorItalic, 
                Settings.ChatSize) +
            ChatFormatting(
                str, 
                Settings.ChatMajorColor, 
                Settings.ChatMajorBold, 
                Settings.ChatMajorItalic, 
                Settings.ChatSize));

    }

    public static void SystemMessageLocal(string str, PhotonPlayer player, string str2)
    {
        Messages.Add(
            ChatFormatting(
                str, 
                Settings.ChatMajorColor, 
                Settings.ChatMajorBold, 
                Settings.ChatMajorItalic, 
                Settings.ChatSize) +
            ChatFormatting(
                $" [{player.ID}] {player.Name.hexColor()} ", 
                Settings.ChatMinorColor, Settings.ChatMinorBold, 
                Settings.ChatMinorItalic, 
                Settings.ChatSize) +
            ChatFormatting(
                str2, 
                Settings.ChatMajorColor, 
                Settings.ChatMajorBold, 
                Settings.ChatMajorItalic, 
                Settings.ChatSize));
    }

    public static void SystemMessageGlobal(string str, bool major = true)
    {
        SystemMessageLocal(str, major);

        FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.Others, 
            ChatFormatting(
                str,
                major ? Settings.ChatMajorColor : Settings.ChatMinorColor,
                major ? Settings.ChatMajorBold : Settings.ChatMinorBold,
                major ? Settings.ChatMajorItalic : Settings.ChatMinorItalic),
            string.Empty);
    }

    public static void SystemMessageGlobal(string[] str, bool parity = true)
    {
        StringBuilder msg = new StringBuilder();
        for (int i = 0; i < str.Length; i++)
        {
            if (i % 2 == 0 || i == 0)
            {
                msg.Append(ChatFormatting(
                str[i],
                parity ? Settings.ChatMajorColor : Settings.ChatMinorColor,
                parity ? Settings.ChatMajorBold : Settings.ChatMinorBold,
                parity ? Settings.ChatMajorItalic : Settings.ChatMinorItalic));
            }
            else
            {
                msg.Append(ChatFormatting(
                str[i],
                parity ? Settings.ChatMinorColor : Settings.ChatMajorColor,
                parity ? Settings.ChatMinorBold : Settings.ChatMajorBold,
                parity ? Settings.ChatMinorItalic : Settings.ChatMajorItalic));
            }
        }

        SystemMessageLocal(msg.ToString(), parity);
        FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.Others, msg.ToString(), string.Empty);
    }

    public static void SystemMessageGlobal(string str, PhotonPlayer player)
    {
        SystemMessageLocal(str, player);

        FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.Others,
            ChatFormatting(
                str, 
                Settings.ChatMajorColor, 
                Settings.ChatMajorBold, 
                Settings.ChatMajorItalic
                ) +
                ChatFormatting(
                    $" [{player.ID}] {player.Name.hexColor()}", 
                    Settings.ChatMinorColor, 
                    Settings.ChatMinorBold, 
                    Settings.ChatMinorItalic) +
                ChatFormatting(
                    ".",
                    Settings.ChatMajorColor,
                    Settings.ChatMajorBold, 
                    Settings.ChatMajorItalic), 
            string.Empty);
    }

    public static void SystemMessageGlobal(PhotonPlayer player, string str)
    {
        SystemMessageLocal(player, str);

        FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.Others,
            ChatFormatting(
                $"[{player.ID}] {player.Name.hexColor()} ",
                Settings.ChatMinorColor,
                Settings.ChatMinorBold,
                Settings.ChatMinorItalic) +
            ChatFormatting(
                str,
                Settings.ChatMajorColor,
                Settings.ChatMajorBold,
                Settings.ChatMajorItalic), 
            string.Empty);
    }

    public static void SystemMessageGlobal(string str, PhotonPlayer player, string str2)
    {
        SystemMessageLocal(str, player, str2);

        FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.Others,
            ChatFormatting(
                str,
                Settings.ChatMajorColor,
                Settings.ChatMajorBold,
                Settings.ChatMajorItalic) +
            ChatFormatting(
                $" [{player.ID}] {player.Name.hexColor()} ",
                Settings.ChatMinorColor, Settings.ChatMinorBold,
                Settings.ChatMinorItalic) +
            ChatFormatting(
                str2,
                Settings.ChatMajorColor,
                Settings.ChatMajorBold,
                Settings.ChatMajorItalic), 
            string.Empty);
    }

    public static void MCRequired()
    {
        if (!PhotonNetwork.isMasterClient)
        {
            SystemMessageLocal(Error(0));
            return;
        }
    }

    private void Awake()
    {
        Chat = this;
    }

    private void commandSwitch(string[] args)
    {
        switch (args[0])
        {
            case "test":
                {
                    string[] test = { "1", "2", "3" };
                    SystemMessageLocal(test, true);
                    SystemMessageLocal(test, false);
                }
                break;
            case "pos":
                {
                    string[] msg = { "Your position:\n",
                        $"\nX", " - ", $"{GGM.Caching.GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().main_object.transform.position.x.ToString()}" +
                        $"\nY", " - ", $"{GGM.Caching.GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().main_object.transform.position.y.ToString()}" +
                        $"\nZ", " - ", $"{GGM.Caching.GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().main_object.transform.position.z.ToString()}" };

                    SystemMessageLocal(msg);
                }
                break;

            case "ban":
                {
                    MCRequired();

                    var id = Convert.ToInt32(args[1]);
                    
                    if (id == PhotonNetwork.player.ID)
                    {
                        SystemMessageLocal(Error(2, "ban"));
                    }
                    else if (!(FengGameManagerMKII.OnPrivateServer || PhotonNetwork.isMasterClient))
                    {
                        FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, "/kick #" + Convert.ToString(id), LoginFengKAI.player.name);
                    }
                    else
                    {
                        foreach (var player in PhotonNetwork.playerList)
                        {
                            if (id == player.ID)
                            {
                                if (FengGameManagerMKII.OnPrivateServer)
                                {
                                    FengGameManagerMKII.instance.kickPlayerRC(player, true, "");
                                }
                                else if (PhotonNetwork.isMasterClient)
                                {
                                    FengGameManagerMKII.instance.kickPlayerRC(player, true, "");
                                    SystemMessageGlobal(player, "has been banned.");
                                }
                            }
                        }
                        if (PhotonPlayer.Find(id) == null)
                        {
                            SystemMessageLocal(Error(1));
                        }
                    }
                }
                break;

            case "aso":
                switch (args[1])
                {
                    case "kdr":
                        RCSettings.asoPreservekdr = RCSettings.asoPreservekdr == 0 ? 1 : 0;
                        SystemMessageGlobal("KDRs will " + (RCSettings.asoPreservekdr == 1 ? string.Empty : "not ") + "be preserved from disconnects.");
                        break;
                    case "racing":
                        RCSettings.racingStatic= RCSettings.racingStatic == 0 ? 1 : 0;
                        SystemMessageLocal("Restart required.");
                        break;
                    default:
                        string[] err = {
                            "Invalid command. Possibles:",
                            "\n/aso kdr", " - peserves players KDR's from disconnects.",
                            "\n/aso racing", " - racing will not restart on finish.",
                            "\n/aso damage", " - sets ASO Damage settings." };
                        SystemMessageLocal(err);
                        break;
                }
                break;

            case "pause":
            case "unpause":
                {
                    MCRequired();

                    FengGameManagerMKII.instance.SetPause();
                }
                break;

            case "ignorelist":
                foreach (var id in FengGameManagerMKII.ignoreList)
                {
                    SystemMessageLocal(id.ToString());
                }
                break;

            case "slots":
                {
                    MCRequired();

                    var slots = Convert.ToInt32(args[1]);
                    PhotonNetwork.room.maxPlayers = slots;
                    string[] msg = { "Max players changed to", slots.ToString(), "."};
                    SystemMessageGlobal(msg);
                }
                break;

            case "time":
                {
                    MCRequired();

                    var time = (FengGameManagerMKII.instance.time - (int)FengGameManagerMKII.instance.timeTotalServer - Convert.ToInt32(args[1])) * -1;
                    FengGameManagerMKII.instance.addTime(time);
                    string[] msg = { "Time set to", time.ToString(), "." };
                    SystemMessageGlobal(msg);
                }
                break;

            case "resetkd":
                {
                    PhotonNetwork.player.SetCustomProperties(new Hashtable {
                        { "kills", 0 },
                        { "deaths", 0 },
                        { "max_dmg", 0 },
                        { "total_dmg", 0 }
                    });
                    SystemMessageLocal("Your stats have been reset.");
                }
                break;

            case "resetkdall":
                {
                    MCRequired();

                    var hash = new Hashtable { { "kills", 0 }, { "deaths", 0 }, { "max_dmg", 0 }, { "total_dmg", 0 } };
                    foreach (var player in PhotonNetwork.playerList)
                    {
                        player.SetCustomProperties(hash);
                    }
                    SystemMessageGlobal("All stats have been reset.");
                }
                break;

            case "revive":
                {
                    MCRequired();

                    var player = PhotonPlayer.Find(Convert.ToInt32(args[1]));
                    FengGameManagerMKII.instance.photonView.RPC("respawnHeroInNewRound", player);
                    SystemMessageGlobal(player, "has been revived.");
                }
                break;

            case "reviveall":
                {
                    MCRequired();

                    FengGameManagerMKII.instance.photonView.RPC("respawnHeroInNewRound", PhotonTargets.All);
                    SystemMessageGlobal("All players have been revived.");
                }
                break;

            case "pm":
                {
                    var player = PhotonPlayer.Find(Convert.ToInt32(args[1]));
                    var msg = "";
                    for (var i = 2; i < args.Length; i++)
                    {
                        msg += args[i] + (i == args.Length - 1 ? "" : " ");
                    }
                    var myName = RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties["name"]).hexColor();
                    var sendName = "";
                    switch (RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties["RCteam"]))
                    {
                        case 1:
                            sendName = "<color=cyan>" + myName + "</color>";
                            break;
                        case 2:
                            sendName = "<color=magenta>" + myName + "</color>";
                            break;
                        default:
                            sendName = myName;
                            break;
                    }
                    FengGameManagerMKII.instance.photonView.RPC("ChatPM", player, sendName, msg);
                    Messages.Add(
            ChatFormatting(
                "PM to",
                Settings.ChatMajorColor,
                Settings.ChatMajorBold,
                Settings.ChatMajorItalic,
                Settings.ChatSize) +
            ChatFormatting(
                $" [{player.ID}] {player.Name.hexColor()}",
                Settings.ChatMinorColor, Settings.ChatMinorBold,
                Settings.ChatMinorItalic,
                Settings.ChatSize) +
            ChatFormatting(
                $": {msg}",
                Settings.ChatMajorColor,
                Settings.ChatMajorBold,
                Settings.ChatMajorItalic,
                Settings.ChatSize));
                }
                break;
                    
            case "team":
                {
                    if (RCSettings.teamMode != 1)
                    {
                        string[] msg = { "Teams ", "are locked or disabled." };
                        SystemMessageLocal(msg, false);
                        return;
                    }
                    var teamValue = 0;
                    var newTeamName = "Individuals";
                    switch (args[1])
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
                            string[] err = { "Invalid team code/name. Possibles:\n" +
                                    "Team Individuals - ", "0", "/", "individuals.\n",
                                "Team Cyan - ", "1", "/", "cyan.\n", 
                                "Team Magenta - ", "2", "/", "magenta." };
                            SystemMessageLocal(err);
                            return;
                    }
                    FengGameManagerMKII.instance.photonView.RPC("setTeamRPC", PhotonNetwork.player, teamValue);
                    string[] msg2 = { "You have joined ", "Team " + newTeamName, "." };
                    SystemMessageLocal(msg2);
                    foreach (var obj in FengGameManagerMKII.instance.getPlayers())
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
                break;

            case "kick":
                {
                    MCRequired();

                    var num8 = Convert.ToInt32(args[1]);
                    if (num8 == PhotonNetwork.player.ID)
                    {
                        SystemMessageLocal(Error(2, "kick"));
                    }
                    else if (!(FengGameManagerMKII.OnPrivateServer || PhotonNetwork.isMasterClient))
                    {
                        FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, "/kick #" + Convert.ToString(num8), LoginFengKAI.player.name);
                    }
                    else
                    {
                        foreach (var player in PhotonNetwork.playerList)
                        {
                            if (num8 == player.ID)
                            {
                                if (FengGameManagerMKII.OnPrivateServer)
                                {
                                    FengGameManagerMKII.instance.kickPlayerRC(player, false, "");
                                }
                                else if (PhotonNetwork.isMasterClient)
                                {
                                    FengGameManagerMKII.instance.kickPlayerRC(player, false, "");
                                    SystemMessageGlobal(player, "has been kicked.");
                                }
                            }
                        }
                        if (PhotonPlayer.Find(num8) == null)
                        {
                            SystemMessageLocal(Error(1));
                        }
                    }
                }
                return;

            case "restart":
                {
                    MCRequired();

                    FengGameManagerMKII.instance.restartGame(false);
                    string[] msg = { "MasterClient ", "has restarted the game." };
                    SystemMessageLocal(msg, false);
                }
                return;

            case "specmode":
                if ((int)FengGameManagerMKII.settings[0xf5] == 0)
                {
                    FengGameManagerMKII.settings[0xf5] = 1;
                    FengGameManagerMKII.instance.EnterSpecMode(true);
                    string[] msg = { "You have entered ", "Spectator ", "mode." };
                    SystemMessageLocal(msg);
                }
                else
                {
                    FengGameManagerMKII.settings[0xf5] = 0;
                    FengGameManagerMKII.instance.EnterSpecMode(false);
                    string[] msg = { "You have exited ", "Spectator ", "mode." };
                    SystemMessageLocal(msg);
                }
                return;

            case "fov":
                {
                    var fov = Convert.ToInt32(args[1]);
                    Camera.main.fieldOfView = fov;
                    string[] msg = { "Field of Vision", "set to", fov.ToString(), "." };
                    SystemMessageLocal(msg, false);
                }
                return;

            case "spectate":
                {
                    var playerid = Convert.ToInt32(args[1]);
                    foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
                    {
                        if (player.GetPhotonView().owner.ID == playerid)
                        {
                            Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().setMainObject(player);
                            Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().setSpectorMode(false);
                            SystemMessageLocal("You are now spectate", player.GetPhotonView().owner);
                        }
                    }
                }
                return;

            case "rules":
                {
                    if (RCSettings.bombMode > 0)
                    {
                        string[] msg = { "Bomb ", "mode is enabled."};
                        SystemMessageLocal(msg, false);
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

                        string[] msg = { "Team ", "mode is enabled. ", sort, "." };
                        SystemMessageLocal(msg, false);
                    }
                    if (RCSettings.pointMode > 0)
                    {
                        string[] msg = { "Points ", "limit is ", $"[{Convert.ToString(RCSettings.pointMode)}]", "." };
                        SystemMessageLocal(msg, false);
                    }
                    if (RCSettings.disableRock > 0)
                    {
                        string[] msg = { "Punks Rock-Throwing ", "is disabled." };
                        SystemMessageLocal(msg, false);
                    }
                    if (RCSettings.spawnMode > 0)
                    {
                        string[] msg = { "Custom Spawn Rate ", "is:",
                            $"\n[{RCSettings.nRate.ToString("F2")}% Normal]" +
                            $"\n[{RCSettings.aRate.ToString("F2")}% Abnormal]" +
                            $"\n[{RCSettings.jRate.ToString("F2")}% Jumper]" +
                            $"\n[{RCSettings.cRate.ToString("F2")}% Crawler]" +
                            $"\n[{RCSettings.pRate.ToString("F2")}% Punk]"};
                        SystemMessageLocal(msg, false);
                    }
                    if (RCSettings.explodeMode > 0)
                    {
                        string[] msg = { "Explode ", "radius is ", $"[{Convert.ToString(RCSettings.explodeMode)}]", "." };
                        SystemMessageLocal(msg, false);
                    }
                    if (RCSettings.healthMode > 0)
                    {
                        var mode = "Static ";
                        if (RCSettings.healthMode == 2)
                        {
                            mode = "Scaled ";
                        }

                        string[] msg = { mode + "Health ", "amount is ", $"[{Convert.ToString(RCSettings.healthLower)} - {Convert.ToString(RCSettings.healthUpper)}]", "." };
                        SystemMessageLocal(msg, false);
                    }
                    if (RCSettings.infectionMode > 0)
                    {
                        string[] msg = { "Infection ", "mode with ", $"[{Convert.ToString(RCSettings.infectionMode)}]", " infected on start." };
                        InRoomChat.SystemMessageLocal(msg, false);
                    }
                    if (RCSettings.damageMode > 0)
                    {
                        string[] msg = { "Minimum Nape Damage ", "is ", $"[{Convert.ToString(RCSettings.damageMode)}]", "." };
                        InRoomChat.SystemMessageLocal(msg, false);
                    }
                    if (RCSettings.moreTitans > 0)
                    {
                        string[] msg = { "Custom Titans Amount ", "is ", $"[{Convert.ToString(RCSettings.moreTitans)}]", "." };
                        InRoomChat.SystemMessageLocal(msg, false);
                    }
                    if (RCSettings.sizeMode > 0)
                    {
                        string[] msg = { "Custom Titans Size ", "is ", $"[{RCSettings.sizeLower.ToString("F2")} - {RCSettings.sizeUpper.ToString("F2")}]", "." };
                        InRoomChat.SystemMessageLocal(msg, false);
                    }
                    if (RCSettings.banEren > 0)
                    {
                        string[] msg = { "Anti-Eren ", "mode is enabled." };
                        SystemMessageLocal(msg, false);
                    }
                    if (RCSettings.waveModeOn == 1)
                    {
                        string[] msg = { "Custom Titans/Wave ", "amount is ", $"[{Convert.ToString(RCSettings.waveModeNum)}]", "." };
                        InRoomChat.SystemMessageLocal(msg, false);
                    }
                    if (RCSettings.friendlyMode > 0)
                    {
                        string[] msg = { "Friendly ", "mode is enabled." };
                        SystemMessageLocal(msg, false);
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

                        string[] msg = { mode + "PVP ", "mode is enabled." };
                        SystemMessageLocal(msg, false);
                    }
                    if (RCSettings.maxWave > 0)
                    {
                        string[] msg = { "Custom Maximum Wave ", "is ", $"[{RCSettings.maxWave.ToString()}]", "." };
                        InRoomChat.SystemMessageLocal(msg, false);
                    }
                    if (RCSettings.horseMode > 0)
                    {
                        string[] msg = { "Horses ", "are enabled." };
                        SystemMessageLocal(msg, false);
                    }
                    if (RCSettings.ahssReload > 0)
                    {
                        string[] msg = { "AHSS Air-Reloading ", "is disabled." };
                        SystemMessageLocal(msg, false);
                    }
                    if (RCSettings.punkWaves > 0)
                    {
                        string[] msg = { "Punk Waves Override ", "is enabled." };
                        SystemMessageLocal(msg, false);
                    }
                    if (RCSettings.endlessMode > 0)
                    {
                        string[] msg = { "Endless Respawn ", "is ", $"[{RCSettings.endlessMode.ToString()}]", " seconds." };
                        InRoomChat.SystemMessageLocal(msg, false);
                    }
                    if (RCSettings.globalDisableMinimap > 0)
                    {
                        string[] msg = { "Minimaps ", "are disabled." };
                        SystemMessageLocal(msg, false);
                    }
                    if (RCSettings.deadlyCannons > 0)
                    {
                        string[] msg = { "Deadly Cannons ", "mode is enabled." };
                        SystemMessageLocal(msg, false);
                    }
                    if (RCSettings.motd != string.Empty)
                    {
                        string[] msg = { "MOTD:\n", RCSettings.motd };
                        SystemMessageLocal(msg, false);
                    }
                }
                break;

            default:
                SystemMessageLocal("Unknown command.");
                break;
        }
    }

    public void OnGUI()
    {
        if (!Settings.Chat || PhotonNetwork.connectionStateDetailed != PeerStates.Joined)
        {
            return;
        }

        if (Event.current.type == EventType.KeyDown)
        {
            if ((Event.current.keyCode == KeyCode.Tab || Event.current.character == '\t') &&
                !IN_GAME_MAIN_CAMERA.isPausing &&
                FengGameManagerMKII.inputRC.humanKeys[InputCodeRC.chat] != KeyCode.Tab)
            {
                Event.current.Use();
                goto Label_219C;
            }
        }
        else if (Event.current.type == EventType.KeyUp && Event.current.keyCode != KeyCode.None &&
                 Event.current.keyCode == FengGameManagerMKII.inputRC.humanKeys[InputCodeRC.chat] &&
                 GUI.GetNameOfFocusedControl() != "ChatInput")
        {
            inputLine = string.Empty;
            GUI.FocusControl("ChatInput");
            goto Label_219C;
        }

        if (Event.current.type == EventType.KeyDown &&
            (Event.current.keyCode == KeyCode.KeypadEnter || Event.current.keyCode == KeyCode.Return))
        {
            if (!string.IsNullOrEmpty(inputLine))
            {
                if (inputLine == "\t")
                {
                    inputLine = string.Empty;
                    GUI.FocusControl(string.Empty);
                    return;
                }

                if (FengGameManagerMKII.RCEvents.ContainsKey("OnChatInput"))
                {
                    var key = (string) FengGameManagerMKII.RCVariableNames["OnChatInput"];
                    if (FengGameManagerMKII.stringVariables.ContainsKey(key))
                    {
                        FengGameManagerMKII.stringVariables[key] = inputLine;
                    }
                    else
                    {
                        FengGameManagerMKII.stringVariables.Add(key, inputLine);
                    }

                    ((RCEvent) FengGameManagerMKII.RCEvents["OnChatInput"]).checkEvent();
                }

                if (!inputLine.StartsWith("/"))
                {
                    string str2 = RCextensions
                        .returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name])
                        .hexColor();
                    if (str2 == string.Empty)
                    {
                        str2 = RCextensions.returnStringFromObject(
                            PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]);
                        if (PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam] != null)
                        {
                            if (RCextensions.returnIntFromObject(
                                    PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam]) == 1)
                            {
                                str2 = "<color=#00FFFF>" + str2 + "</color>";
                            }
                            else if (RCextensions.returnIntFromObject(
                                         PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam]) == 2)
                            {
                                str2 = "<color=#FF00FF>" + str2 + "</color>";
                            }
                        }
                    }

                    object[] parameters = {inputLine, str2};
                    FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, parameters);
                }
                else
                {
                    commandSwitch(inputLine.Remove(0, 1).Split(' '));
                }

                inputLine = string.Empty;
                GUI.FocusControl(string.Empty);
                return;
            }

            inputLine = "\t";
            GUI.FocusControl("ChatInput");
        }

        Label_219C:
        GUI.SetNextControlName(string.Empty);
        GUILayout.BeginArea(GuiRect);
        GUILayout.FlexibleSpace();
        var text = string.Empty;
        if (Messages.Count < 15)
        {
            for (var msg = 0; msg < Messages.Count; msg++)
            {
                text = text + Messages[msg] + "\n";
            }
        }
        else
        {
            for (var i = Messages.Count - 15; i < Messages.Count; i++)
            {
                text = text + Messages[i] + "\n";
            }
        }

        GUILayout.Label(text);
        GUILayout.EndArea();
        GUILayout.BeginArea(GuiRect2);
        GUILayout.BeginHorizontal();
        GUI.SetNextControlName("ChatInput");
        inputLine = GUILayout.TextField(inputLine);
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    public void SetPosition()
    {
        if (AlignBottom)
        {
            GuiRect = new Rect(0f, Screen.height - 500, 300f, 470f);
            GuiRect2 = new Rect(30f, Screen.height - 300 + 0x113, 300f, 25f);
        }
    }

    public void Start()
    {
        SetPosition();
    }
}

