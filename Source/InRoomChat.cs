using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using MonoBehaviour = Photon.MonoBehaviour;

public class InRoomChat : MonoBehaviour
{
    private readonly bool AlignBottom = true;
    internal static InRoomChat Chat;
    public static readonly string ChatRPC = "Chat";
    public static Rect GuiRect = new Rect(0f, 100f, 300f, 470f);
    public static Rect GuiRect2 = new Rect(30f, 575f, 300f, 25f);
    private string inputLine = string.Empty;
    public bool IsVisible = true;
    public static List<string> messages = new List<string>();
    private Vector2 scrollPos = Vector2.zero;

    public void AddLine(string newLine)
    {
        messages.Add(newLine);
    }

    public void AddLineRC(string newline)
    {
        messages.Add(RCLine(newline));
    }

    public static void Message(string str)
    {
        messages.Add(str);
    }

    private void Awake()
    {
        Chat = this;
    }

    private void commandSwitch(string[] args)
    {
        switch (args[0])
        {
            case "pos":
                FengGameManagerMKII.ShowPos();
                break;
            case "tp":
                var obj3 = new FengGameManagerMKII();
                obj3.send_tp();
                break;
            case "gas":
                var obj2 = new FengGameManagerMKII();
                obj2.send_gas();
                break;
            case "ban":
                {
                    var num8 = Convert.ToInt32(args[1]);
                    if (num8 == PhotonNetwork.player.ID)
                    {
                        AddLine("Error:can't kick yourself.");
                    }
                    else if (!(FengGameManagerMKII.OnPrivateServer || PhotonNetwork.isMasterClient))
                    {
                        FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, "/kick #" + Convert.ToString(num8), LoginFengKAI.player.name);
                    }
                    else
                    {
                        foreach (var player3 in PhotonNetwork.playerList)
                        {
                            if (num8 == player3.ID)
                            {
                                if (FengGameManagerMKII.OnPrivateServer)
                                {
                                    FengGameManagerMKII.instance.kickPlayerRC(player3, true, "");
                                }
                                else if (PhotonNetwork.isMasterClient)
                                {
                                    FengGameManagerMKII.instance.kickPlayerRC(player3, true, "");
                                    FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, RCLine(RCextensions.returnStringFromObject(player3.customProperties[PhotonPlayerProperty.name]) + " has been banned from the server!"), string.Empty);
                                }
                            }
                        }
                        if (PhotonPlayer.Find(num8)==null)
                        {
                            AddLine("error:no such player.");
                        }
                    }
                }
                return;

            case "cloth":
                AddLine(ClothFactory.GetDebugInfo());
                return;

            case "aso":
                if (!PhotonNetwork.isMasterClient)
                {
                    AddLineRC("Error: not masterclient");
                    return;
                }
                switch (args[1])
                {
                    case "kdr":
                        RCSettings.asoPreservekdr = RCSettings.asoPreservekdr == 0 ? 1 : 0;
                        AddLineRC("KDRs will " + (RCSettings.asoPreservekdr == 1 ? " " : "not ") +"be preserved from disconnects.");
                        break;
                    case "racing":
                        RCSettings.racingStatic= RCSettings.racingStatic == 0 ? 1 : 0;
                        AddLineRC("Racing will " + (RCSettings.asoPreservekdr == 1 ? " " : "not ") + "end on finish.");
                        break;
                }
                return;

            case "pause":
            case "unpause":
                if (!PhotonNetwork.isMasterClient)
                {
                    AddLineRC("Error: not masterclient");
                    return;
                }
                FengGameManagerMKII.instance.SetPause();
                return;

            case "checklevel":
                foreach (var player in PhotonNetwork.playerList)
                {
                    AddLine(RCextensions.returnStringFromObject(player.customProperties[PhotonPlayerProperty.currentLevel]));
                }
                return;
            case "isrc":
                AddLineRC((FengGameManagerMKII.masterRC ? "is" : "not") + " RC");
                return;

            case "ignorelist":
                foreach (var id in FengGameManagerMKII.ignoreList)
                {
                    AddLine(id.ToString());
                }
                return;

            case "room":
                if (!PhotonNetwork.isMasterClient)
                {
                    AddLineRC("Error: not masterclient");
                    return;
                }
                var roomValue = Convert.ToInt32(args[2]);
                switch (args[1])
                {
                    case "max":
                        PhotonNetwork.room.maxPlayers = roomValue;
                        FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, RCLine("Max players changed to " + roomValue + "!"), "");
                        break;
                    case "time":
                        FengGameManagerMKII.instance.addTime(roomValue);
                        FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, RCLine(roomValue + " seconds added to the clock."), "");
                        break;
                }
                return;

            case "resetkd":
                PhotonNetwork.player.SetCustomProperties(new Hashtable { { "kills", 0 }, { "deaths", 0 }, { "max_dmg", 0 }, { "total_dmg", 0 } });
                AddLineRC("Your stats have been reset.");
                return;

            case "resetkdall":
                {
                    if (!PhotonNetwork.isMasterClient)
                    {
                        AddLine("Error: not masterclient");
                        return;
                    }
                    var hash = new Hashtable { { "kills", 0 }, { "deaths", 0 }, { "max_dmg", 0 }, { "total_dmg", 0 } };
                    foreach (var player in PhotonNetwork.playerList)
                    {
                        player.SetCustomProperties(hash);
                    }
                    FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, RCLine("All stats have been reset"), "");
                }
                return;

            case "revive":
                {
                    var player = PhotonPlayer.Find(Convert.ToInt32(args[1]));
                    FengGameManagerMKII.instance.photonView.RPC("respawnHeroInNewRound", player);
                    AddLineRC("Player [" + player.ID + "] has been revived");
                }
                return;

            case "reviveall":
                FengGameManagerMKII.instance.photonView.RPC("respawnHeroInNewRound", PhotonTargets.All);
                FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, RCLine("All player have been revived"), "");
                return;

            case "pm":
                {
                    var player = PhotonPlayer.Find(Convert.ToInt32(args[1]));
                    var msg = "";
                    for(var i = 2; i < args.Length; i++)
                    {
                        msg += args[i] + (i == args.Length-1 ? "" : " ");
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
                    AddLine(RCLine("Sent PM [" + player.ID + "] " + msg));
                }
                return;
                    
            case "team":
                if(RCSettings.teamMode != 1)
                {
                    AddLineRC("Error: teams are locked or disabled");
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
                        AddLineRC("Error: invalid team code/name.(use 0, 1, 2)");
                        return;
                }
                FengGameManagerMKII.instance.photonView.RPC("setTeamRPC", PhotonNetwork.player, teamValue);
                AddLineRC("You have joined to team " + newTeamName);
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
                return;

            case "kick":
                { 
                    var num8 = Convert.ToInt32(args[1]);
                    if (num8 == PhotonNetwork.player.ID)
                    {
                        AddLine("error:can't kick yourself.");
                    }
                    else if (!(FengGameManagerMKII.OnPrivateServer || PhotonNetwork.isMasterClient))
                    {
                        FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, "/kick #" + Convert.ToString(num8), LoginFengKAI.player.name);
                    }
                    else
                    {
                        foreach (var player3 in PhotonNetwork.playerList)
                        {
                            if (num8 == player3.ID)
                            {
                                if (FengGameManagerMKII.OnPrivateServer)
                                {
                                    FengGameManagerMKII.instance.kickPlayerRC(player3, false, "");
                                }
                                else if (PhotonNetwork.isMasterClient)
                                {
                                    FengGameManagerMKII.instance.kickPlayerRC(player3, false, "");
                                    FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, RCLine(RCextensions.returnStringFromObject(player3.customProperties[PhotonPlayerProperty.name]) + " has been kicked from the server!"), string.Empty);
                                }
                            }
                        }
                        if (PhotonPlayer.Find(num8) == null)
                        {
                            AddLine("error:no such player.");
                        }
                    }
                }
                return;

            case "restart":
                if (!PhotonNetwork.isMasterClient)
                {
                    AddLineRC("Error: not masterclient");
                    return;
                }
                FengGameManagerMKII.instance.restartGame(false);
                FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, RCLine("MasterClient has restarted the game."), "");
                return;

            case "specmode":
                if ((int)FengGameManagerMKII.settings[0xf5] == 0)
                {
                    FengGameManagerMKII.settings[0xf5] = 1;
                    FengGameManagerMKII.instance.EnterSpecMode(true);
                    AddLineRC("You have entered spectator mode.");
                }
                else
                {
                    FengGameManagerMKII.settings[0xf5] = 0;
                    FengGameManagerMKII.instance.EnterSpecMode(false);
                    AddLineRC("You have exited spectator mode.");
                }
                return;

            case "fov":
                var num6 = Convert.ToInt32(args[1]);
                Camera.main.fieldOfView = num6;
                AddLineRC("Field of vision set to " + num6 + ".");
                return;

            case "colliders":
                var num7 = 0;
                foreach (TITAN titan in FengGameManagerMKII.instance.getTitans())
                {
                    if (titan.myTitanTrigger.isCollide)
                    {
                        num7++;
                    }
                }
                AddLine(num7.ToString());
                return;

            case "spectate":
                {
                    var num8 = Convert.ToInt32(args[1]);
                    foreach (var obj5 in GameObject.FindGameObjectsWithTag("Player"))
                    {
                        if (obj5.GetPhotonView().owner.ID == num8)
                        {
                            Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().setMainObject(obj5);
                            Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().setSpectorMode(false);
                        }
                    }
                }
                return;

            case "rules":
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
                        AddLineRC("Max Wave set to " + RCSettings.maxWave);
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
                        AddLineRC("Endless Respawn is enabled (" + RCSettings.endlessMode + " seconds).");
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
                return;

            default:
                return;
        }
    }

    public void OnGUI()
    {
        if (!IsVisible || PhotonNetwork.connectionStateDetailed != PeerStates.Joined)
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
        if (messages.Count < 15)
        {
            for (var msg = 0; msg < messages.Count; msg++)
            {
                text = text + messages[msg] + "\n";
            }
        }
        else
        {
            for (var i = messages.Count - 15; i < messages.Count; i++)
            {
                text = text + messages[i] + "\n";
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

    public static string RCLine(string line)
    {
        return "<color=#FFC000>" + line + "</color>";
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

