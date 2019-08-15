using ExitGames.Client.Photon;
using GGM.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GGM;
using GGM.GUI;
using GGM.GUI.Pages;
using UnityEngine;
using MonoBehaviour = Photon.MonoBehaviour;
using Settings = GGM.Config.Settings;

public class InRoomChat : MonoBehaviour
{
    public static List<string> Chat;
    public static List<string> ChatFeed = new List<string>();
    private static Texture2D chatBackground;
    private static Rect chatFeedRect;
    private static Vector2 chatFeedScroll;
    private static float chatHeight;
    private static Rect chatInputRect;
    private static float chatOpacity;
    private static Rect chatRect;
    private static Vector2 chatScroll;
    private static float chatWidth;
    private string inputLine = string.Empty;

    public static void AddLineChat(string newLine)
    {
        Chat.Add(newLine);
        chatScroll = new Vector2(9999f, 9999f);
    }

    public static void AddLineChatFeed(string newLine)
    {
        ChatFeed.Add(newLine);
        chatFeedScroll = new Vector2(9999f, 9999f);
    }

    public static void AddLineRC(params string[] newLine)
    {
        var str = string.Empty;
        foreach (var line in newLine)
        {
            str += line;
        }

        AddLineChat(RCLine(str));
    }

    public static string ChatFormatting(string text, string color, bool bold, bool italic, string size = "")
    {
        return "<color=#" + color + ">" + (size != string.Empty ? "<size=" + size + ">" : string.Empty) + (bold ? "<b>" : string.Empty) + (italic ? "<i>" : string.Empty) + text + (italic ? "</i>" : string.Empty) + (bold ? "</b>" : string.Empty) + (size != "" ? "</size>" : string.Empty) + "</color>";
    }

    /// <param name="type">
    /// 0 - Not MC
    /// 1 - Existence
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

    public static string RCLine(string line)
    {
        return "<color=#FFC000>" + line + "</color>";
    }

    public static void SendLineRC(params string[] newLine)
    {
        var str = string.Empty;
        foreach (var line in newLine)
        {
            str += line;
        }

        FengGameManagerMKII.FGM.photonView.RPC("Chat", PhotonTargets.All, RCLine(str), string.Empty);
    }

    public static void SystemMessageGlobal(string str, bool major = true)
    {
        if (Settings.LegacyChatSetting)
        {
            SendLineRC(str);
        }
        else
        {
            SystemMessageLocal(str, major);
            FengGameManagerMKII.FGM.photonView.RPC("Chat", PhotonTargets.Others, ChatFormatting(str, major ? Settings.ChatMajorColorSetting : Settings.ChatMinorColorSetting, major ? Settings.ChatMajorFormatSettings[0] : Settings.ChatMinorFormatSettings[0], major ? Settings.ChatMajorFormatSettings[1] : Settings.ChatMinorFormatSettings[1]), string.Empty);
        }
    }

    public static void SystemMessageGlobal(string[] str, bool parity = true)
    {
        if (Settings.LegacyChatSetting)
        {
            SendLineRC(str);
        }
        else
        {
            var msg = new StringBuilder();
            for (var i = 0; i < str.Length; i++)
            {
                if (i % 2 == 0 || i == 0)
                {
                    msg.Append(ChatFormatting(str[i], parity ? Settings.ChatMajorColorSetting : Settings.ChatMinorColorSetting, parity ? Settings.ChatMajorFormatSettings[0] : Settings.ChatMinorFormatSettings[0], parity ? Settings.ChatMajorFormatSettings[1] : Settings.ChatMinorFormatSettings[1]));
                }
                else
                {
                    msg.Append(ChatFormatting(str[i], parity ? Settings.ChatMinorColorSetting : Settings.ChatMajorColorSetting, parity ? Settings.ChatMinorFormatSettings[0] : Settings.ChatMajorFormatSettings[0], parity ? Settings.ChatMinorFormatSettings[1] : Settings.ChatMajorFormatSettings[1]));
                }
            }

            SystemMessageLocal(msg.ToString(), parity);
            FengGameManagerMKII.FGM.photonView.RPC("Chat", PhotonTargets.Others, msg.ToString(), string.Empty);
        }
    }

    public static void SystemMessageGlobal(string str, PhotonPlayer player)
    {
        if (Settings.LegacyChatSetting)
        {
            SendLineRC(str, $" [{player.ID}] {player.Name.hexColor()}.");
        }
        else
        {
            SystemMessageLocal(str, player);
            FengGameManagerMKII.FGM.photonView.RPC("Chat", PhotonTargets.Others, ChatFormatting(str, Settings.ChatMajorColorSetting, Settings.ChatMajorFormatSettings[0], Settings.ChatMajorFormatSettings[1]) + ChatFormatting($" [{player.ID}] {player.Name.hexColor()}", Settings.ChatMinorColorSetting, Settings.ChatMinorFormatSettings[0], Settings.ChatMinorFormatSettings[1]) + ChatFormatting(".", Settings.ChatMajorColorSetting, Settings.ChatMajorFormatSettings[0], Settings.ChatMajorFormatSettings[1]), string.Empty);
        }
    }

    public static void SystemMessageGlobal(PhotonPlayer player, string str)
    {
        if (Settings.LegacyChatSetting)
        {
            SendLineRC($"[{player.ID}] {player.Name.hexColor()} ", str);
        }
        else
        {
            SystemMessageLocal(player, str);
            FengGameManagerMKII.FGM.photonView.RPC("Chat", PhotonTargets.Others, ChatFormatting($"[{player.ID}] {player.Name.hexColor()} ", Settings.ChatMinorColorSetting, Settings.ChatMinorFormatSettings[0], Settings.ChatMinorFormatSettings[1]) + ChatFormatting(str, Settings.ChatMajorColorSetting, Settings.ChatMajorFormatSettings[0], Settings.ChatMajorFormatSettings[1]), string.Empty);
        }
    }

    public static void SystemMessageGlobal(string str, PhotonPlayer player, string str2)
    {
        if (Settings.LegacyChatSetting)
        {
            SendLineRC(str, $" [{player.ID}] {player.Name.hexColor()} ", str2);
        }
        else
        {
            SystemMessageLocal(str, player, str2);
            FengGameManagerMKII.FGM.photonView.RPC("Chat", PhotonTargets.Others, ChatFormatting(str, Settings.ChatMajorColorSetting, Settings.ChatMajorFormatSettings[0], Settings.ChatMajorFormatSettings[1]) + ChatFormatting($" [{player.ID}] {player.Name.hexColor()} ", Settings.ChatMinorColorSetting, Settings.ChatMinorFormatSettings[0], Settings.ChatMinorFormatSettings[1]) + ChatFormatting(str2, Settings.ChatMajorColorSetting, Settings.ChatMajorFormatSettings[0], Settings.ChatMajorFormatSettings[1]), string.Empty);
        }
    }

    public static void SystemMessageLocal(string str, bool major = true)
    {
        if (Settings.LegacyChatSetting)
        {
            AddLineRC(str);
        }
        else
        {
            AddLineChat(ChatFormatting(str, major ? Settings.ChatMajorColorSetting : Settings.ChatMinorColorSetting, major ? Settings.ChatMajorFormatSettings[0] : Settings.ChatMinorFormatSettings[0], major ? Settings.ChatMajorFormatSettings[1] : Settings.ChatMinorFormatSettings[1], Settings.ChatSizeSetting.ToString()));
        }
    }

    public static void SystemMessageLocal(string[] str, bool parity = true, bool chatFeed = false)
    {
        if (Settings.LegacyChatSetting)
        {
            AddLineRC(str);
        }
        else
        {
            var msg = new StringBuilder();
            if (str.Length != 1)
            {
                for (var i = 0; i < str.Length; i++)
                {
                    if (i % 2 == 0 || i == 0)
                    {
                        msg.Append(ChatFormatting(str[i], parity ? Settings.ChatMajorColorSetting : Settings.ChatMinorColorSetting, parity ? Settings.ChatMajorFormatSettings[0] : Settings.ChatMinorFormatSettings[0], parity ? Settings.ChatMajorFormatSettings[1] : Settings.ChatMinorFormatSettings[1], Settings.ChatSizeSetting.ToString()));
                    }
                    else
                    {
                        msg.Append(ChatFormatting(str[i], parity ? Settings.ChatMinorColorSetting : Settings.ChatMajorColorSetting, parity ? Settings.ChatMinorFormatSettings[0] : Settings.ChatMajorFormatSettings[0], parity ? Settings.ChatMinorFormatSettings[1] : Settings.ChatMajorFormatSettings[1], Settings.ChatSizeSetting.ToString()));
                    }
                }
            }
            else
            {
                msg.Append(ChatFormatting(str[0], parity ? Settings.ChatMajorColorSetting : Settings.ChatMinorColorSetting, parity ? Settings.ChatMajorFormatSettings[0] : Settings.ChatMinorFormatSettings[0], parity ? Settings.ChatMajorFormatSettings[1] : Settings.ChatMinorFormatSettings[1], Settings.ChatSizeSetting.ToString()));
            }
            if (!chatFeed)
                AddLineChat(msg.ToString());
            else
                AddLineChatFeed(msg.ToString());
        }
    }

    public static void SystemMessageLocal(string str, PhotonPlayer player)
    {
        if (Settings.LegacyChatSetting)
        {
            AddLineRC(str, $" [{player.ID}] {player.Name.hexColor()}.");
        }
        else
        {
            AddLineChat(ChatFormatting(str, Settings.ChatMajorColorSetting, Settings.ChatMajorFormatSettings[0], Settings.ChatMajorFormatSettings[1], Settings.ChatSizeSetting.ToString()) + ChatFormatting($" [{player.ID}] {player.Name.hexColor()}", Settings.ChatMinorColorSetting, Settings.ChatMinorFormatSettings[0], Settings.ChatMinorFormatSettings[1], Settings.ChatSizeSetting.ToString()) + ChatFormatting(".", Settings.ChatMajorColorSetting, Settings.ChatMajorFormatSettings[0], Settings.ChatMajorFormatSettings[1], Settings.ChatSizeSetting.ToString()));
        }
    }

    public static void SystemMessageLocal(PhotonPlayer player, string str)
    {
        if (Settings.LegacyChatSetting)
        {
            AddLineRC($"[{player.ID}] {player.Name.hexColor()} ", str);
        }
        else
        {
            AddLineChat(ChatFormatting($"[{player.ID}] {player.Name.hexColor()} ", Settings.ChatMinorColorSetting, Settings.ChatMinorFormatSettings[0], Settings.ChatMinorFormatSettings[1], Settings.ChatSizeSetting.ToString()) + ChatFormatting(str, Settings.ChatMajorColorSetting, Settings.ChatMajorFormatSettings[0], Settings.ChatMajorFormatSettings[1], Settings.ChatSizeSetting.ToString()));
        }
    }

    public static void SystemMessageLocal(string str, PhotonPlayer player, string str2)
    {
        if (Settings.LegacyChatSetting)
        {
            AddLineRC(str, $" [{player.ID}] {player.Name.hexColor()} ", str2);
        }
        else
        {
            AddLineChat(ChatFormatting(str, Settings.ChatMajorColorSetting, Settings.ChatMajorFormatSettings[0], Settings.ChatMajorFormatSettings[1], Settings.ChatSizeSetting.ToString()) + ChatFormatting($" [{player.ID}] {player.Name.hexColor()} ", Settings.ChatMinorColorSetting, Settings.ChatMinorFormatSettings[0], Settings.ChatMinorFormatSettings[1], Settings.ChatSizeSetting.ToString()) + ChatFormatting(str2, Settings.ChatMajorColorSetting, Settings.ChatMajorFormatSettings[0], Settings.ChatMajorFormatSettings[1], Settings.ChatSizeSetting.ToString()));
        }
    }

    public void OnGUI()
    {
        if (!Settings.ChatUISetting || PhotonNetwork.connectionStateDetailed != PeerStates.Joined)
        {
            return;
        }

        if (Event.current.type == EventType.KeyDown)
        {
            if ((Event.current.keyCode == KeyCode.Tab || Event.current.character == '\t') && FengGameManagerMKII.inputRC.humanKeys[InputCodeRC.chat] != KeyCode.Tab && GUI.GetNameOfFocusedControl() != "WelcomeMessage" && GUI.GetNameOfFocusedControl() != "LevelScript" && GUI.GetNameOfFocusedControl() != "LogicScript")
            {
                Event.current.Use();
                goto Label_219C;
            }
        }
        else if (Event.current.type == EventType.KeyUp && Event.current.keyCode != KeyCode.None && Event.current.keyCode == FengGameManagerMKII.inputRC.humanKeys[InputCodeRC.chat] && GUI.GetNameOfFocusedControl() != "ChatInput" && GUI.GetNameOfFocusedControl() != "WelcomeMessage" && GUI.GetNameOfFocusedControl() != "LevelScript" && GUI.GetNameOfFocusedControl() != "LogicScript")
        {
            inputLine = string.Empty;
            GUI.FocusControl("ChatInput");
            goto Label_219C;
        }

        if (Event.current.type == EventType.KeyDown && (Event.current.keyCode == KeyCode.KeypadEnter || Event.current.keyCode == KeyCode.Return) && GUI.GetNameOfFocusedControl() != "WelcomeMessage" && GUI.GetNameOfFocusedControl() != "LevelScript" && GUI.GetNameOfFocusedControl() != "LogicScript")
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
                    var key = (string)FengGameManagerMKII.RCVariableNames["OnChatInput"];
                    if (FengGameManagerMKII.stringVariables.ContainsKey(key))
                    {
                        FengGameManagerMKII.stringVariables[key] = inputLine;
                    }
                    else
                    {
                        FengGameManagerMKII.stringVariables.Add(key, inputLine);
                    }

                    ((RCEvent)FengGameManagerMKII.RCEvents["OnChatInput"]).checkEvent();
                }

                if (!inputLine.StartsWith("/"))
                {
                    var str2 = RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]).hexColor();
                    if (str2 == string.Empty)
                    {
                        str2 = RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]);
                        if (PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam] != null)
                        {
                            if (RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam]) == 1)
                            {
                                str2 = "<color=#00FFFF>" + str2 + "</color>";
                            }
                            else if (RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam]) == 2)
                            {
                                str2 = "<color=#FF00FF>" + str2 + "</color>";
                            }
                        }
                    }

                    object[] parameters = { inputLine, str2 };
                    FengGameManagerMKII.FGM.photonView.RPC("Chat", PhotonTargets.All, parameters);
                }
                else
                {
                    CommandSwitch(inputLine.Remove(0, 1).Split(' '));
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

        if (chatWidth != Settings.ChatWidthSetting || chatHeight != Settings.ChatHeightSetting)
        {
            SetPosition();
        }

        if (Settings.ChatBackground)
        {
            if (chatBackground == null)
            {
                chatBackground = new Texture2D(1, 1);
                chatOpacity = 0f;
                chatBackground.SetPixel(0, 0, new Color(0f, 0f, 0f, chatOpacity));
                chatBackground.Apply();
            }
            if (chatRect.Contains(GUIHelpers.mousePos) || GUI.GetNameOfFocusedControl() == "ChatInput")
            {
                chatOpacity = Mathf.Lerp(chatOpacity, Settings.ChatOpacitySetting, Time.timeScale < 1f ? 0.01f : Time.deltaTime * 1.5f);
                chatBackground.SetPixel(0, 0, new Color(0f, 0f, 0f, chatOpacity));
                chatBackground.Apply();
            }
            else if (chatOpacity != 0f)
            {
                chatOpacity = Mathf.Lerp(chatOpacity, 0f, Time.timeScale < 1f ? 0.01f : Time.deltaTime * 1.5f);
                chatBackground.SetPixel(0, 0, new Color(0f, 0f, 0f, chatOpacity));
                chatBackground.Apply();
            }

            GUI.DrawTexture(chatRect, chatBackground, ScaleMode.StretchToFill);
            if (Settings.ChatFeedSeparateSetting && Settings.ChatFeedSetting)
                GUI.DrawTexture(chatFeedRect, chatBackground, ScaleMode.StretchToFill);
        }


        GUILayout.BeginArea(chatRect);
        {
            GUILayout.FlexibleSpace();
            var text = string.Empty;
            text = Chat.Aggregate(text, (current, t) => current + t + "\n");
            if (Chat.Count > Settings.MessagesCache)
            {
                Chat.RemoveAt(0);
            }

            chatScroll = GUILayout.BeginScrollView(chatScroll);
            {
                GUILayout.Label(text);
            }
            GUILayout.EndScrollView();
        }
        GUILayout.EndArea();

        GUILayout.BeginArea(chatInputRect);
        {
            GUILayout.BeginHorizontal();
            {
                GUI.SetNextControlName("ChatInput");
                inputLine = GUILayout.TextField(inputLine);
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndArea();

        if (Settings.ChatFeedSeparateSetting)
        {
            if (!Settings.ChatFeedSetting && ChatFeed.Count > 0)
            {
                ChatFeed.Clear();
            }

            GUILayout.BeginArea(chatFeedRect);
            {
                chatFeedScroll = GUILayout.BeginScrollView(chatFeedScroll);
                {
                    GUILayout.FlexibleSpace();
                    var text = string.Empty;
                    text = ChatFeed.Aggregate(text, (current, t) => current + t + "\n");
                    if (ChatFeed.Count > Settings.MessagesCache)
                    {
                        Chat.RemoveAt(0);
                    }
                    GUILayout.Label(text);
                }
                GUILayout.EndScrollView();
            }
            GUILayout.EndArea();
        }
    }

    public void SetPosition()
    {
        chatWidth = Settings.ChatWidthSetting;
        chatHeight = Settings.ChatHeightSetting;
        chatInputRect = new Rect(30f, Screen.height - 300 + 275, chatWidth - 25f, 25f);
        chatRect = GUIHelpers.AlignRect(chatWidth, chatHeight - 15f, GUIHelpers.Alignment.BOTTOMLEFT, 5f, -30f);
        chatFeedRect = GUIHelpers.AlignRect(chatWidth, chatHeight - 15f, GUIHelpers.Alignment.BOTTOMRIGHT, -5f, -30f);
    }

    public void Start()
    {
        SetPosition();
    }

    private static void CommandSwitch(string[] args)
    {
        switch (args[0])
        {
            case "pos":
                Commands.GetPosition();
                break;

            case "ban":
                Commands.Ban(args[1]);
                break;

            case "aso":
                switch (args[1])
                {
                    case "damage":
                        Commands.ASODamage();
                        break;

                    case "kdr":
                        Commands.ASOKDR();
                        break;

                    case "racing":
                        Commands.ASORacing();
                        break;

                    default:
                        string[] err = { "Invalid command. Possibles:", "\n/aso kdr", " - preserves players KDR's from disconnects.", "\n/aso racing", " - racing will not restart on finish.", "\n/aso damage", " - sets ASO Damage settings." };
                        SystemMessageLocal(err);
                        break;
                }
                break;

            case "clear":
                Commands.ClearChat();
                break;

            case "/clean":
            case "/clear":
                Commands.ClearChat(false);
                break;

            case "pause":
            case "unpause":
                FengGameManagerMKII.FGM.SetPause();
                break;

            case "ignorelist":
                Commands.IngoreList();
                break;

            case "slots":
                Commands.SetSlots(Convert.ToInt32(args[1]));
                break;

            case "time":
                Commands.SetTime(Convert.ToInt32(args[1]));
                break;

            case "tp":
                Commands.Teleport(Convert.ToInt32(args[1]));
                break;

            case "reconnect":
                Commands.Reconnect();
                break;

            case "resetkd":
                Commands.ResetKD();
                break;

            case "/resetkd":
                Commands.ResetKD(args[1]);
                break;

            case "resetkdall":
                Commands.ResetKD(global: true);
                break;

            case "revive":
                Commands.Revive(args[1]);
                break;

            case "reviveall":
                Commands.Revive(all: true);
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
                    string sendName;
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

                    FengGameManagerMKII.FGM.photonView.RPC("ChatPM", player, sendName, msg);
                    AddLineChat(ChatFormatting("PM to", Settings.ChatMajorColorSetting, Settings.ChatMajorFormatSettings[0], Settings.ChatMajorFormatSettings[1], Settings.ChatSizeSetting.ToString()) + ChatFormatting($" [{player.ID}] {player.Name.hexColor()}", Settings.ChatMinorColorSetting, Settings.ChatMinorFormatSettings[0], Settings.ChatMinorFormatSettings[1], Settings.ChatSizeSetting.ToString()) + ChatFormatting($": {msg}", Settings.ChatMajorColorSetting, Settings.ChatMajorFormatSettings[0], Settings.ChatMajorFormatSettings[1], Settings.ChatSizeSetting.ToString()));
                }
                break;

            case "team":
                {
                    Commands.SwitchTeam(args[1]);
                }
                break;

            case "kick":
                Commands.Kick(args[1]);
                return;

            case "restart":
                Commands.Restart();
                return;

            case "specmode":
                Commands.SpectatorMode();
                return;

            case "spectate":
                Commands.Spectate(Convert.ToInt32(args[1]));
                return;

            case "mute":
                Commands.Mute(PhotonPlayer.Find(Convert.ToInt32(args[1])));
                return;

            case "unmute":
                Commands.Unmute(PhotonPlayer.Find(Convert.ToInt32(args[1])));
                return;

            case "mutelist":
                Commands.MuteList();
                return;

            case "rules":
                Commands.Rules();
                break;

            default:
                SystemMessageLocal("Unknown command.");
                break;
        }
    }
}