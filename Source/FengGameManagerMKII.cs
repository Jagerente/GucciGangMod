using GGM;
using GGM.Caching;
using GGM.Config;
using GGM.GUI.Pages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Logger = GGM.Logger;
using MonoBehaviour = Photon.MonoBehaviour;
using Random = UnityEngine.Random;

public class FengGameManagerMKII : MonoBehaviour
{
    public static readonly string applicationId = string.Empty;
    public static Hashtable banHash;
    public static Hashtable boolVariables;
    public static Dictionary<string, GameObject> CachedPrefabs;
    public static string currentLevel;
    public static string currentScript;
    public static string currentScriptLogic;
    public static bool customLevelLoaded;
    public static List<int> DamageFeed = new List<int>();
    public static int DamageFeedCurrent = 0;
    public static int DamageFeedHighest = 0;
    public static List<float> DamageFeedTime = new List<float>();
    public static FengGameManagerMKII FGM;
    public static Hashtable floatVariables;
    public static FPSCounter FPS = new FPSCounter();
    public static Hashtable globalVariables;
    public static bool hasLogged;
    public static Hashtable heroHash;
    public static int highest_feed = 0;
    public static List<int> ignoreList;
    public static Hashtable imatitan;
    public static FengCustomInputs inputManager;
    public static InputManagerRC inputRC;
    public static Hashtable intVariables;
    public static bool isAssetLoaded;
    public static bool LAN;
    public static string level = string.Empty;
    public static Hashtable[] linkHash;
    public static bool logicLoaded;
    public static bool masterRC;
    public static string nameField;
    public static bool NeedRejoin = false;
    public static bool noRestart;
    public static string oldScript;
    public static string oldScriptLogic;
    public static bool OnPrivateServer;
    public static Hashtable playerVariables;
    public static string PrivateServerAuthPass;
    public static string privateServerField;
    public static AssetBundle RCassets;
    public static Hashtable RCEvents;
    public static Hashtable RCRegions;
    public static Hashtable RCRegionTriggers;
    public static Hashtable RCVariableNames;
    public static string RejoinRegion = "";
    public static Room RejoinRoom;
    public static string[] s;
    public static Vector2 scroll;
    public static Vector2 scroll2;
    public static object[] settings;
    public static int single_kills;
    public static int single_maxDamage;
    public static int single_totalDamage;
    public static Material skyMaterial;
    public static Hashtable stringVariables;
    public static Hashtable titanVariables;
    public Dictionary<int, CannonValues> allowedToCannon;
    public Dictionary<string, Texture2D> assetCacheTextures;
    public GameObject cameraObject;
    public GameObject canvasObject;
    public GameObject checkpoint;
    public int cyanKills;
    public int difficulty;
    public bool gameStart;
    public List<GameObject> groundList;
    public bool isFirstLoad;
    public bool isRecompiling;
    public bool isRestarting;
    public bool isSpawning;
    public bool isUnloading;
    public bool justSuicide;
    public List<string[]> levelCache;
    public int magentaKills;
    public int maxPlayers;
    public float mouseSlider;
    public float myRespawnTime;
    public new string name;
    public bool needChooseSide;
    public float pauseWaitTime;
    public string playerList;
    public List<Vector3> playerSpawnsC;
    public List<Vector3> playerSpawnsM;
    public List<PhotonPlayer> playersRPC;
    public Dictionary<string, int[]> PreservedPlayerKDR;
    public int PVPhumanScore;
    public int PVPtitanScore;
    public float qualitySlider;
    public List<GameObject> racingDoors;
    public Vector3 racingSpawnPoint;
    public bool racingSpawnPointSet;
    public List<float> restartCount;
    public bool restartingBomb;
    public bool restartingEren;
    public bool restartingHorse;
    public bool restartingMC;
    public bool restartingTitan;
    public float retryTime;
    public float roundTime;
    public GameObject selectedObj;
    public List<GameObject> spectateSprites;
    public Texture2D textureBackgroundBlack;
    public Texture2D textureBackgroundBlue;
    public int time = 600;
    public float timeTotalServer;
    public List<TitanSpawner> titanSpawners;
    public List<Vector3> titanSpawns;
    public float transparencySlider;
    public float updateTime;
    public int wave = 1;
    private ArrayList chatContent;
    private ArrayList cT;
    private float currentSpeed;
    private bool endRacing;
    private ArrayList eT;
    private ArrayList fT;
    private float gameEndCD;
    private float gameEndTotalCDtime = 9f;
    private bool gameTimesUp;
    private ArrayList heroes;
    private int highestwave = 1;
    private ArrayList hooks;
    private int humanScore;
    private bool isLosing;
    private bool isWinning;
    private ArrayList kicklist;
    private ArrayList killInfoGO = new ArrayList();
    private string localRacingResult;
    private IN_GAME_MAIN_CAMERA mainCamera;
    private float maxSpeed;
    private string myLastHero;
    private string myLastRespawnTag = "playerRespawn";
    private int PVPhumanScoreMax = 200;
    private int PVPtitanScoreMax = 200;
    private ArrayList racingResult;
    private bool startRacing;
    private int[] teamScores;
    private int teamWinner;
    private float timeElapse;
    private ArrayList titans;
    private int titanScore;
    private GameObject ui;

    private enum LoginStates
    {
        notlogged,
        loggingin,
        loginfailed,
        loggedin
    }

    public static GameObject InstantiateCustomAsset(string key)
    {
        key = key.Substring(8);
        return (GameObject)ResourcesCache.RCLoadGO(key);
    }

    public static PeerStates returnPeerState(int peerstate)
    {
        switch (peerstate)
        {
            case 0:
                return PeerStates.Authenticated;

            case 1:
                return PeerStates.ConnectedToMaster;

            case 2:
                return PeerStates.DisconnectingFromMasterserver;

            case 3:
                return PeerStates.DisconnectingFromGameserver;

            case 4:
                return PeerStates.DisconnectingFromNameServer;
        }

        return PeerStates.ConnectingToMasterserver;
    }

    public static void ServerCloseConnection(PhotonPlayer targetPlayer, bool requestIpBan, string inGameName)
    {
        var options = new RaiseEventOptions { TargetActors = new[] { targetPlayer.ID } };
        if (requestIpBan)
        {
            var eventContent = new Hashtable();
            eventContent[(byte)0] = true;
            if (inGameName != null && inGameName.Length > 0)
            {
                eventContent[(byte)1] = inGameName;
            }

            PhotonNetwork.RaiseEvent(203, eventContent, true, options);
        }
        else
        {
            PhotonNetwork.RaiseEvent(203, null, true, options);
        }
    }

    public static void ServerRequestAuthentication(string authPassword)
    {
        if (!string.IsNullOrEmpty(authPassword))
        {
            var eventContent = new Hashtable();
            eventContent[(byte)0] = authPassword;
            PhotonNetwork.RaiseEvent(198, eventContent, true, new RaiseEventOptions());
        }
    }

    public static void ServerRequestUnban(string bannedAddress)
    {
        if (!string.IsNullOrEmpty(bannedAddress))
        {
            var eventContent = new Hashtable();
            eventContent[(byte)0] = bannedAddress;
            PhotonNetwork.RaiseEvent(199, eventContent, true, new RaiseEventOptions());
        }
    }

    public static bool TryRejoin()
    {
        if (RejoinRoom == null)
            return false;
        bool result = PhotonNetwork.ConnectToMaster(RejoinRegion, NetworkingPeer.ProtocolToNameServerPort[PhotonNetwork.networkingPeer.TransportProtocol], applicationId, UIMainReferences.ServerKey);
        return result;
    }

    public static bool TryRejoinRoom()
    {
        if (RejoinRoom == null)
        {
            return false;
        }

        if (RejoinRoom.maxPlayers <= RejoinRoom.playerCount)
            return false;
        return PhotonNetwork.JoinRoom(RejoinRoom.name);
    }

    public void addCamera(IN_GAME_MAIN_CAMERA c)
    {
        mainCamera = c;
    }

    public void addCT(COLOSSAL_TITAN titan)
    {
        cT.Add(titan);
    }

    public void addET(TITAN_EREN hero)
    {
        eT.Add(hero);
    }

    public void addFT(FEMALE_TITAN titan)
    {
        fT.Add(titan);
    }

    public void addHero(HERO hero)
    {
        heroes.Add(hero);
    }

    public void addHook(Bullet h)
    {
        hooks.Add(h);
    }

    public void addTime(float time)
    {
        timeTotalServer -= time;
    }

    public void addTitan(TITAN titan)
    {
        titans.Add(titan);
    }

    public void checkPVPpts()
    {
        if (PVPtitanScore >= PVPtitanScoreMax)
        {
            PVPtitanScore = PVPtitanScoreMax;
            gameLose();
        }
        else if (PVPhumanScore >= PVPhumanScoreMax)
        {
            PVPhumanScore = PVPhumanScoreMax;
            gameWin();
        }
    }

    public void compileScript(string str)
    {
        int num3;
        var strArray2 = str.Replace(" ", string.Empty).Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        var hashtable = new Hashtable();
        var num = 0;
        var num2 = 0;
        var flag = false;
        for (num3 = 0; num3 < strArray2.Length; num3++)
        {
            if (strArray2[num3] == "{")
            {
                num++;
            }
            else if (strArray2[num3] == "}")
            {
                num2++;
            }
            else
            {
                var num4 = 0;
                var num5 = 0;
                var num6 = 0;
                foreach (var ch in strArray2[num3])
                {
                    switch (ch)
                    {
                        case '(':
                            num4++;
                            break;

                        case ')':
                            num5++;
                            break;

                        case '"':
                            num6++;
                            break;
                    }
                }

                if (num4 != num5)
                {
                    var num8 = num3 + 1;
                    string[] msg = { "Script Error:\n", "Parentheses not equal.", "[LINE " + num8 + "]" };
                    InRoomChat.SystemMessageLocal(msg, false);
                    flag = true;
                }

                if (num6 % 2 != 0)
                {
                    string[] msg = { "Script Error:\n", "Quotations not equal.", "[LINE " + (num3 + 1) + "]" };
                    InRoomChat.SystemMessageLocal(msg, false);
                    flag = true;
                }
            }
        }

        if (num != num2)
        {
            string[] msg = { "Script Error:\n", "Bracket count not equivalent." };
            InRoomChat.SystemMessageLocal(msg, false);
            flag = true;
        }

        if (!flag)
        {
            try
            {
                int num10;
                num3 = 0;
                while (num3 < strArray2.Length)
                {
                    if (strArray2[num3].StartsWith("On") && strArray2[num3 + 1] == "{")
                    {
                        var key = num3;
                        num10 = num3 + 2;
                        var num11 = 0;
                        for (var i = num3 + 2; i < strArray2.Length; i++)
                        {
                            if (strArray2[i] == "{")
                            {
                                num11++;
                            }

                            if (strArray2[i] == "}")
                            {
                                if (num11 > 0)
                                {
                                    num11--;
                                }
                                else
                                {
                                    num10 = i - 1;
                                    i = strArray2.Length;
                                }
                            }
                        }

                        hashtable.Add(key, num10);
                        num3 = num10;
                    }

                    num3++;
                }

                foreach (int num9 in hashtable.Keys)
                {
                    int num14;
                    int num15;
                    string str4;
                    string str5;
                    RegionTrigger trigger;
                    var str3 = strArray2[num9];
                    num10 = (int)hashtable[num9];
                    var stringArray = new string[num10 - num9 + 1];
                    var index = 0;
                    for (num3 = num9; num3 <= num10; num3++)
                    {
                        stringArray[index] = strArray2[num3];
                        index++;
                    }

                    var event2 = parseBlock(stringArray, 0, 0, null);
                    if (str3.StartsWith("OnPlayerEnterRegion"))
                    {
                        num14 = str3.IndexOf('[');
                        num15 = str3.IndexOf(']');
                        str4 = str3.Substring(num14 + 2, num15 - num14 - 3);
                        num14 = str3.IndexOf('(');
                        num15 = str3.IndexOf(')');
                        str5 = str3.Substring(num14 + 2, num15 - num14 - 3);
                        if (RCRegionTriggers.ContainsKey(str4))
                        {
                            trigger = (RegionTrigger)RCRegionTriggers[str4];
                            trigger.playerEventEnter = event2;
                            trigger.myName = str4;
                            RCRegionTriggers[str4] = trigger;
                        }
                        else
                        {
                            trigger = new RegionTrigger { playerEventEnter = event2, myName = str4 };
                            RCRegionTriggers.Add(str4, trigger);
                        }

                        RCVariableNames.Add("OnPlayerEnterRegion[" + str4 + "]", str5);
                    }
                    else if (str3.StartsWith("OnPlayerLeaveRegion"))
                    {
                        num14 = str3.IndexOf('[');
                        num15 = str3.IndexOf(']');
                        str4 = str3.Substring(num14 + 2, num15 - num14 - 3);
                        num14 = str3.IndexOf('(');
                        num15 = str3.IndexOf(')');
                        str5 = str3.Substring(num14 + 2, num15 - num14 - 3);
                        if (RCRegionTriggers.ContainsKey(str4))
                        {
                            trigger = (RegionTrigger)RCRegionTriggers[str4];
                            trigger.playerEventExit = event2;
                            trigger.myName = str4;
                            RCRegionTriggers[str4] = trigger;
                        }
                        else
                        {
                            trigger = new RegionTrigger { playerEventExit = event2, myName = str4 };
                            RCRegionTriggers.Add(str4, trigger);
                        }

                        RCVariableNames.Add("OnPlayerExitRegion[" + str4 + "]", str5);
                    }
                    else if (str3.StartsWith("OnTitanEnterRegion"))
                    {
                        num14 = str3.IndexOf('[');
                        num15 = str3.IndexOf(']');
                        str4 = str3.Substring(num14 + 2, num15 - num14 - 3);
                        num14 = str3.IndexOf('(');
                        num15 = str3.IndexOf(')');
                        str5 = str3.Substring(num14 + 2, num15 - num14 - 3);
                        if (RCRegionTriggers.ContainsKey(str4))
                        {
                            trigger = (RegionTrigger)RCRegionTriggers[str4];
                            trigger.titanEventEnter = event2;
                            trigger.myName = str4;
                            RCRegionTriggers[str4] = trigger;
                        }
                        else
                        {
                            trigger = new RegionTrigger { titanEventEnter = event2, myName = str4 };
                            RCRegionTriggers.Add(str4, trigger);
                        }

                        RCVariableNames.Add("OnTitanEnterRegion[" + str4 + "]", str5);
                    }
                    else if (str3.StartsWith("OnTitanLeaveRegion"))
                    {
                        num14 = str3.IndexOf('[');
                        num15 = str3.IndexOf(']');
                        str4 = str3.Substring(num14 + 2, num15 - num14 - 3);
                        num14 = str3.IndexOf('(');
                        num15 = str3.IndexOf(')');
                        str5 = str3.Substring(num14 + 2, num15 - num14 - 3);
                        if (RCRegionTriggers.ContainsKey(str4))
                        {
                            trigger = (RegionTrigger)RCRegionTriggers[str4];
                            trigger.titanEventExit = event2;
                            trigger.myName = str4;
                            RCRegionTriggers[str4] = trigger;
                        }
                        else
                        {
                            trigger = new RegionTrigger { titanEventExit = event2, myName = str4 };
                            RCRegionTriggers.Add(str4, trigger);
                        }

                        RCVariableNames.Add("OnTitanExitRegion[" + str4 + "]", str5);
                    }
                    else if (str3.StartsWith("OnFirstLoad()"))
                    {
                        RCEvents.Add("OnFirstLoad", event2);
                    }
                    else if (str3.StartsWith("OnRoundStart()"))
                    {
                        RCEvents.Add("OnRoundStart", event2);
                    }
                    else if (str3.StartsWith("OnUpdate()"))
                    {
                        RCEvents.Add("OnUpdate", event2);
                    }
                    else
                    {
                        string[] strArray4;
                        if (str3.StartsWith("OnTitanDie"))
                        {
                            num14 = str3.IndexOf('(');
                            num15 = str3.LastIndexOf(')');
                            strArray4 = str3.Substring(num14 + 1, num15 - num14 - 1).Split(',');
                            strArray4[0] = strArray4[0].Substring(1, strArray4[0].Length - 2);
                            strArray4[1] = strArray4[1].Substring(1, strArray4[1].Length - 2);
                            RCVariableNames.Add("OnTitanDie", strArray4);
                            RCEvents.Add("OnTitanDie", event2);
                        }
                        else if (str3.StartsWith("OnPlayerDieByTitan"))
                        {
                            RCEvents.Add("OnPlayerDieByTitan", event2);
                            num14 = str3.IndexOf('(');
                            num15 = str3.LastIndexOf(')');
                            strArray4 = str3.Substring(num14 + 1, num15 - num14 - 1).Split(',');
                            strArray4[0] = strArray4[0].Substring(1, strArray4[0].Length - 2);
                            strArray4[1] = strArray4[1].Substring(1, strArray4[1].Length - 2);
                            RCVariableNames.Add("OnPlayerDieByTitan", strArray4);
                        }
                        else if (str3.StartsWith("OnPlayerDieByPlayer"))
                        {
                            RCEvents.Add("OnPlayerDieByPlayer", event2);
                            num14 = str3.IndexOf('(');
                            num15 = str3.LastIndexOf(')');
                            strArray4 = str3.Substring(num14 + 1, num15 - num14 - 1).Split(',');
                            strArray4[0] = strArray4[0].Substring(1, strArray4[0].Length - 2);
                            strArray4[1] = strArray4[1].Substring(1, strArray4[1].Length - 2);
                            RCVariableNames.Add("OnPlayerDieByPlayer", strArray4);
                        }
                        else if (str3.StartsWith("OnChatInput"))
                        {
                            RCEvents.Add("OnChatInput", event2);
                            num14 = str3.IndexOf('(');
                            num15 = str3.LastIndexOf(')');
                            str5 = str3.Substring(num14 + 1, num15 - num14 - 1);
                            RCVariableNames.Add("OnChatInput", str5.Substring(1, str5.Length - 2));
                        }
                    }
                }
            }
            catch (UnityException exception)
            {
                InRoomChat.SystemMessageLocal(exception.Message, false);
            }
        }
    }

    public int conditionType(string str)
    {
        if (!str.StartsWith("Int"))
        {
            if (str.StartsWith("Bool"))
            {
                return 1;
            }

            if (str.StartsWith("String"))
            {
                return 2;
            }

            if (str.StartsWith("Float"))
            {
                return 3;
            }

            if (str.StartsWith("Titan"))
            {
                return 5;
            }

            if (str.StartsWith("Player"))
            {
                return 4;
            }
        }

        return 0;
    }

    public void DestroyAllExistingCloths()
    {
        var clothArray = FindObjectsOfType<Cloth>();
        if (clothArray.Length > 0)
        {
            for (var i = 0; i < clothArray.Length; i++)
            {
                ClothFactory.DisposeObject(clothArray[i].gameObject);
            }
        }
    }

    public void EnterSpecMode(bool enter)
    {
        SpectatorMode.SpecMode = enter;
        if (enter)
        {
            spectateSprites = new List<GameObject>();
            foreach (GameObject obj2 in FindObjectsOfType(typeof(GameObject)))
            {
                if (obj2.GetComponent<UISprite>() != null && obj2.activeInHierarchy)
                {
                    var name = obj2.name;
                    if (name.Contains("blade") || name.Contains("bullet") || name.Contains("gas") || name.Contains("flare") || name.Contains("skill_cd"))
                    {
                        if (!spectateSprites.Contains(obj2))
                        {
                            spectateSprites.Add(obj2);
                        }

                        obj2.SetActive(false);
                    }
                }
            }

            string[] strArray2 = { "Flare", "LabelInfoBottomRight" };
            foreach (var str2 in strArray2)
            {
                var item = GameObjectCache.Find(str2);
                if (item != null)
                {
                    if (!spectateSprites.Contains(item))
                    {
                        spectateSprites.Add(item);
                    }

                    item.SetActive(false);
                }
            }

            foreach (HERO hero in FGM.getPlayers())
            {
                if (hero.photonView.isMine)
                {
                    PhotonNetwork.Destroy(hero.photonView);
                }
            }

            if (RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.isTitan]) == 2 && !RCextensions.returnBoolFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.dead]))
            {
                foreach (TITAN titan in FGM.getTitans())
                {
                    if (titan.photonView.isMine)
                    {
                        PhotonNetwork.Destroy(titan.photonView);
                    }
                }
            }

            NGUITools.SetActive(GameObjectCache.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[1], false);
            NGUITools.SetActive(GameObjectCache.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[2], false);
            NGUITools.SetActive(GameObjectCache.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[3], false);
            FGM.needChooseSide = false;
            Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().enabled = true;
            if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.ORIGINAL)
            {
                Screen.lockCursor = false;
                Screen.showCursor = false;
            }

            var obj4 = GameObject.FindGameObjectWithTag("Player");
            if (obj4 != null && obj4.GetComponent<HERO>() != null)
            {
                Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().setMainObject(obj4);
            }
            else
            {
                Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().setMainObject(null);
            }

            Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().setSpectorMode(false);
            Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().gameOver = true;
            StartCoroutine(reloadSky());
        }
        else
        {
            if (GameObjectCache.Find("cross1") != null)
            {
                GameObjectCache.Find("cross1").transform.localPosition = Vector3.up * 5000f;
            }

            if (spectateSprites != null)
            {
                foreach (var obj2 in spectateSprites)
                {
                    if (obj2 != null)
                    {
                        obj2.SetActive(true);
                    }
                }
            }

            spectateSprites = new List<GameObject>();
            NGUITools.SetActive(GameObjectCache.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[1], false);
            NGUITools.SetActive(GameObjectCache.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[2], false);
            NGUITools.SetActive(GameObjectCache.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[3], false);
            FGM.needChooseSide = true;
            Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().setMainObject(null);
            Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().setSpectorMode(true);
            Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().gameOver = true;
        }
    }

    public void gameLose()
    {
        if (!isWinning && !isLosing)
        {
            isLosing = true;
            titanScore++;
            gameEndCD = gameEndTotalCDtime;
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
            {
                object[] parameters = { titanScore };
                photonView.RPC("netGameLose", PhotonTargets.Others, parameters);
                if (Settings.ChatFeedSetting)
                {
                    if (Settings.LegacyChatSetting)
                    {
                        if (!Settings.ChatFeedSeparateSetting)
                            InRoomChat.AddLineChat("<color=#FFC000>(" + this.roundTime.ToString("F2") + ")</color> Round ended (game lose).");
                        else
                            InRoomChat.AddLineChatFeed("<color=#FFC000>(" + this.roundTime.ToString("F2") + ")</color> Round ended (game lose).");
                    }
                    else
                    {
                        string[] msg = { $"({roundTime.ToString("F2")}) ", "Round ended. ", "[Game Lose]" };
                        if (!Settings.ChatFeedSeparateSetting)
                            InRoomChat.SystemMessageLocal(msg, false);
                        else
                            InRoomChat.SystemMessageLocal(msg, false, true);
                    }
                }
            }
        }
    }

    public void gameWin()
    {
        if (!isLosing && !isWinning)
        {
            isWinning = true;
            humanScore++;
            if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
            {
                if (RCSettings.racingStatic == 1)
                {
                    gameEndCD = 1000f;
                }
                else
                {
                    gameEndCD = 20f;
                }

                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                {
                    object[] parameters = { 0 };
                    photonView.RPC("netGameWin", PhotonTargets.Others, parameters);
                    if (Settings.ChatFeedSetting)
                    {
                        if (Settings.LegacyChatSetting)
                        {
                            if (!Settings.ChatFeedSeparateSetting)
                                InRoomChat.AddLineChat("<color=#FFC000>(" + this.roundTime.ToString("F2") + ")</color> Round ended (game win).");
                            else
                                InRoomChat.AddLineChatFeed("<color=#FFC000>(" + this.roundTime.ToString("F2") + ")</color> Round ended (game win).");
                        }
                        else
                        {
                            string[] msg = { $"({roundTime.ToString("F2")}) ", "Round ended. ", "[Game Win]" };
                            if (!Settings.ChatFeedSeparateSetting)
                                InRoomChat.SystemMessageLocal(msg, false);
                            else
                                InRoomChat.SystemMessageLocal(msg, false, true);
                        }
                    }
                }
            }
            else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS)
            {
                gameEndCD = gameEndTotalCDtime;
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                {
                    object[] objArray3 = { teamWinner };
                    photonView.RPC("netGameWin", PhotonTargets.Others, objArray3);
                    if (Settings.ChatFeedSetting)
                    {
                        if (Settings.LegacyChatSetting)
                        {
                            if (!Settings.ChatFeedSeparateSetting)
                                InRoomChat.AddLineChat("<color=#FFC000>(" + this.roundTime.ToString("F2") + ")</color> Round ended (game win).");
                            else
                                InRoomChat.AddLineChatFeed("<color=#FFC000>(" + this.roundTime.ToString("F2") + ")</color> Round ended (game win).");
                        }
                        else
                        {
                            string[] msg = { $"({roundTime.ToString("F2")}) ", "Round ended. ", "[Game Win]" };
                            if (!Settings.ChatFeedSeparateSetting)
                                InRoomChat.SystemMessageLocal(msg, false);
                            else
                                InRoomChat.SystemMessageLocal(msg, false, true);
                        }
                    }
                }

                teamScores[teamWinner - 1]++;
            }
            else
            {
                gameEndCD = gameEndTotalCDtime;
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                {
                    object[] objArray4 = { humanScore };
                    photonView.RPC("netGameWin", PhotonTargets.Others, objArray4);
                    if (Settings.ChatFeedSetting)
                    {
                        if (Settings.LegacyChatSetting)
                        {
                            if (!Settings.ChatFeedSeparateSetting)
                                InRoomChat.AddLineChat("<color=#FFC000>(" + this.roundTime.ToString("F2") + ")</color> Round ended (game win).");
                            else
                                InRoomChat.AddLineChatFeed("<color=#FFC000>(" + this.roundTime.ToString("F2") + ")</color> Round ended (game win).");
                        }
                        else
                        {
                            string[] msg = { $"({roundTime.ToString("F2")}) ", "Round ended. ", "[Game Win]" };
                            if (!Settings.ChatFeedSeparateSetting)
                                InRoomChat.SystemMessageLocal(msg, false);
                            else
                                InRoomChat.SystemMessageLocal(msg, false, true);
                        }
                    }
                }
            }
        }
    }

    public ArrayList getPlayers()
    {
        return heroes;
    }

    public ArrayList getTitans()
    {
        return titans;
    }

    public string hairtype(int lol)
    {
        if (lol < 0)
        {
            return "Random";
        }

        return "Male " + lol;
    }

    public bool isPlayerAllDead()
    {
        var num = 0;
        var num2 = 0;
        foreach (var player in PhotonNetwork.playerList)
        {
            if (RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.isTitan]) == 1)
            {
                num++;
                if (RCextensions.returnBoolFromObject(player.customProperties[PhotonPlayerProperty.dead]))
                {
                    num2++;
                }
            }
        }

        return num == num2;
    }

    public bool isTeamAllDead(int team)
    {
        var num = 0;
        var num2 = 0;
        foreach (var player in PhotonNetwork.playerList)
        {
            if (player.customProperties[PhotonPlayerProperty.isTitan] != null && player.customProperties[PhotonPlayerProperty.team] != null && RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.isTitan]) == 1 && RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.team]) == team)
            {
                num++;
                if (RCextensions.returnBoolFromObject(player.customProperties[PhotonPlayerProperty.dead]))
                {
                    num2++;
                }
            }
        }

        return num == num2;
    }

    public void justRecompileThePlayerList()
    {
        int num15;
        string str3;
        int num16;
        int num17;
        int num18;
        int num19;
        var str = string.Empty;
        if (RCSettings.teamMode != 0)
        {
            int num10;
            string str2;
            var num = 0;
            var num2 = 0;
            var num3 = 0;
            var num4 = 0;
            var num5 = 0;
            var num6 = 0;
            var num7 = 0;
            var num8 = 0;
            var dictionary = new Dictionary<int, PhotonPlayer>();
            var dictionary2 = new Dictionary<int, PhotonPlayer>();
            var dictionary3 = new Dictionary<int, PhotonPlayer>();
            foreach (var player in PhotonNetwork.playerList)
            {
                if (player.customProperties[PhotonPlayerProperty.dead] != null && !ignoreList.Contains(player.ID))
                {
                    num10 = RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.RCteam]);
                    switch (num10)
                    {
                        case 0:
                            dictionary3.Add(player.ID, player);
                            break;

                        case 1:
                            dictionary.Add(player.ID, player);
                            num += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.kills]);
                            num3 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.deaths]);
                            num5 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.max_dmg]);
                            num7 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.total_dmg]);
                            break;

                        case 2:
                            dictionary2.Add(player.ID, player);
                            num2 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.kills]);
                            num4 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.deaths]);
                            num6 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.max_dmg]);
                            num8 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.total_dmg]);
                            break;
                    }
                }
            }

            cyanKills = num;
            magentaKills = num2;
            if (PhotonNetwork.isMasterClient)
            {
                if (RCSettings.teamMode == 2)
                {
                    foreach (var player2 in PhotonNetwork.playerList)
                    {
                        var num11 = 0;
                        if (dictionary.Count > dictionary2.Count + 1)
                        {
                            num11 = 2;
                            if (dictionary.ContainsKey(player2.ID))
                            {
                                dictionary.Remove(player2.ID);
                            }

                            if (!dictionary2.ContainsKey(player2.ID))
                            {
                                dictionary2.Add(player2.ID, player2);
                            }
                        }
                        else if (dictionary2.Count > dictionary.Count + 1)
                        {
                            num11 = 1;
                            if (!dictionary.ContainsKey(player2.ID))
                            {
                                dictionary.Add(player2.ID, player2);
                            }

                            if (dictionary2.ContainsKey(player2.ID))
                            {
                                dictionary2.Remove(player2.ID);
                            }
                        }

                        if (num11 > 0)
                        {
                            photonView.RPC("setTeamRPC", player2, num11);
                        }
                    }
                }
                else if (RCSettings.teamMode == 3)
                {
                    foreach (var player3 in PhotonNetwork.playerList)
                    {
                        var num12 = 0;
                        num10 = RCextensions.returnIntFromObject(player3.customProperties[PhotonPlayerProperty.RCteam]);
                        if (num10 > 0)
                        {
                            switch (num10)
                            {
                                case 1:
                                    {
                                        var num13 = 0;
                                        num13 = RCextensions.returnIntFromObject(player3.customProperties[PhotonPlayerProperty.kills]);
                                        if (num2 + num13 + 7 < num - num13)
                                        {
                                            num12 = 2;
                                            num2 += num13;
                                            num -= num13;
                                        }

                                        break;
                                    }

                                case 2:
                                    {
                                        var num14 = 0;
                                        num14 = RCextensions.returnIntFromObject(player3.customProperties[PhotonPlayerProperty.kills]);
                                        if (num + num14 + 7 < num2 - num14)
                                        {
                                            num12 = 1;
                                            num += num14;
                                            num2 -= num14;
                                        }

                                        break;
                                    }
                            }

                            if (num12 > 0)
                            {
                                photonView.RPC("setTeamRPC", player3, num12);
                            }
                        }
                    }
                }
            }

            str = string.Concat(str, "[00FFFF]TEAM CYAN", "[ffffff]:", cyanKills, "/", num3, "/", num5, "/", num7, "\n");
            foreach (var player4 in dictionary.Values)
            {
                num10 = RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.RCteam]);
                if (player4.customProperties[PhotonPlayerProperty.dead] != null && num10 == 1)
                {
                    if (ignoreList.Contains(player4.ID))
                    {
                        str = str + "[FF0000][X] ";
                    }

                    if (player4.isLocal)
                    {
                        str = str + "[00CC00]";
                    }
                    else
                    {
                        str = str + "[FFCC00]";
                    }

                    str = str + "[" + Convert.ToString(player4.ID) + "] ";
                    if (player4.isMasterClient)
                    {
                        str = str + "[ffffff][M] ";
                    }

                    if (RCextensions.returnBoolFromObject(player4.customProperties[PhotonPlayerProperty.dead]))
                    {
                        str = str + "[" + ColorSet.color_red + "] *dead* ";
                    }

                    if (RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.isTitan]) < 2)
                    {
                        num15 = RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.team]);
                        if (num15 < 2)
                        {
                            str = str + "[" + ColorSet.color_human + "] <H> ";
                        }
                        else if (num15 == 2)
                        {
                            str = str + "[" + ColorSet.color_human_1 + "] <A> ";
                        }
                    }
                    else if (RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.isTitan]) == 2)
                    {
                        str = str + "[" + ColorSet.color_titan_player + "] <T> ";
                    }

                    str2 = str;
                    str3 = string.Empty;
                    str3 = RCextensions.returnStringFromObject(player4.customProperties[PhotonPlayerProperty.name]);
                    num16 = 0;
                    num16 = RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.kills]);
                    num17 = 0;
                    num17 = RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.deaths]);
                    num18 = 0;
                    num18 = RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.max_dmg]);
                    num19 = 0;
                    num19 = RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.total_dmg]);
                    str = string.Concat(str2, string.Empty, str3, "[ffffff]:", num16, "/", num17, "/", num18, "/", num19);
                    if (RCextensions.returnBoolFromObject(player4.customProperties[PhotonPlayerProperty.dead]))
                    {
                        str = str + "[-]";
                    }

                    str = str + "\n";
                }
            }

            str = string.Concat(str, " \n", "[FF00FF]TEAM MAGENTA", "[ffffff]:", magentaKills, "/", num4, "/", num6, "/", num8, "\n");
            foreach (var player5 in dictionary2.Values)
            {
                num10 = RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.RCteam]);
                if (player5.customProperties[PhotonPlayerProperty.dead] != null && num10 == 2)
                {
                    if (ignoreList.Contains(player5.ID))
                    {
                        str = str + "[FF0000][X] ";
                    }

                    if (player5.isLocal)
                    {
                        str = str + "[00CC00]";
                    }
                    else
                    {
                        str = str + "[FFCC00]";
                    }

                    str = str + "[" + Convert.ToString(player5.ID) + "] ";
                    if (player5.isMasterClient)
                    {
                        str = str + "[ffffff][M] ";
                    }

                    if (RCextensions.returnBoolFromObject(player5.customProperties[PhotonPlayerProperty.dead]))
                    {
                        str = str + "[" + ColorSet.color_red + "] *dead* ";
                    }

                    if (RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.isTitan]) < 2)
                    {
                        num15 = RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.team]);
                        if (num15 < 2)
                        {
                            str = str + "[" + ColorSet.color_human + "] <H> ";
                        }
                        else if (num15 == 2)
                        {
                            str = str + "[" + ColorSet.color_human_1 + "] <A> ";
                        }
                    }
                    else if (RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.isTitan]) == 2)
                    {
                        str = str + "[" + ColorSet.color_titan_player + "] <T> ";
                    }

                    str2 = str;
                    str3 = string.Empty;
                    str3 = RCextensions.returnStringFromObject(player5.customProperties[PhotonPlayerProperty.name]);
                    num16 = 0;
                    num16 = RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.kills]);
                    num17 = 0;
                    num17 = RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.deaths]);
                    num18 = 0;
                    num18 = RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.max_dmg]);
                    num19 = 0;
                    num19 = RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.total_dmg]);
                    str = string.Concat(str2, string.Empty, str3, "[ffffff]:", num16, "/", num17, "/", num18, "/", num19);
                    if (RCextensions.returnBoolFromObject(player5.customProperties[PhotonPlayerProperty.dead]))
                    {
                        str = str + "[-]";
                    }

                    str = str + "\n";
                }
            }

            str = string.Concat(new object[] { str, " \n", "[00FF00]INDIVIDUAL\n" });
            foreach (var player6 in dictionary3.Values)
            {
                num10 = RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.RCteam]);
                if (player6.customProperties[PhotonPlayerProperty.dead] != null && num10 == 0)
                {
                    if (ignoreList.Contains(player6.ID))
                    {
                        str = str + "[FF0000][X] ";
                    }

                    if (player6.isLocal)
                    {
                        str = str + "[00CC00]";
                    }
                    else
                    {
                        str = str + "[FFCC00]";
                    }

                    str = str + "[" + Convert.ToString(player6.ID) + "] ";
                    if (player6.isMasterClient)
                    {
                        str = str + "[ffffff][M] ";
                    }

                    if (RCextensions.returnBoolFromObject(player6.customProperties[PhotonPlayerProperty.dead]))
                    {
                        str = str + "[" + ColorSet.color_red + "] *dead* ";
                    }

                    if (RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.isTitan]) < 2)
                    {
                        num15 = RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.team]);
                        if (num15 < 2)
                        {
                            str = str + "[" + ColorSet.color_human + "] <H> ";
                        }
                        else if (num15 == 2)
                        {
                            str = str + "[" + ColorSet.color_human_1 + "] <A> ";
                        }
                    }
                    else if (RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.isTitan]) == 2)
                    {
                        str = str + "[" + ColorSet.color_titan_player + "] <T> ";
                    }

                    str2 = str;
                    str3 = string.Empty;
                    str3 = RCextensions.returnStringFromObject(player6.customProperties[PhotonPlayerProperty.name]);
                    num16 = 0;
                    num16 = RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.kills]);
                    num17 = 0;
                    num17 = RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.deaths]);
                    num18 = 0;
                    num18 = RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.max_dmg]);
                    num19 = 0;
                    num19 = RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.total_dmg]);
                    str = string.Concat(str2, string.Empty, str3, "[ffffff]:", num16, "/", num17, "/", num18, "/", num19);
                    if (RCextensions.returnBoolFromObject(player6.customProperties[PhotonPlayerProperty.dead]))
                    {
                        str = str + "[-]";
                    }

                    str = str + "\n";
                }
            }
        }
        else
        {
            foreach (var player7 in PhotonNetwork.playerList)
            {
                if (player7.customProperties[PhotonPlayerProperty.dead] != null)
                {
                    if (ignoreList.Contains(player7.ID))
                    {
                        str = str + "[FF0000][X] ";
                    }

                    if (player7.isLocal)
                    {
                        str = str + "[00CC00]";
                    }
                    else
                    {
                        str = str + "[FFCC00]";
                    }

                    str = str + "[" + Convert.ToString(player7.ID) + "] ";
                    if (player7.isMasterClient)
                    {
                        str = str + "[ffffff][M] ";
                    }

                    if (RCextensions.returnBoolFromObject(player7.customProperties[PhotonPlayerProperty.dead]))
                    {
                        str = str + "[" + ColorSet.color_red + "] *dead* ";
                    }

                    if (RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.isTitan]) < 2)
                    {
                        num15 = RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.team]);
                        if (num15 < 2)
                        {
                            str = str + "[" + ColorSet.color_human + "] <H> ";
                        }
                        else if (num15 == 2)
                        {
                            str = str + "[" + ColorSet.color_human_1 + "] <A> ";
                        }
                    }
                    else if (RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.isTitan]) == 2)
                    {
                        str = str + "[" + ColorSet.color_titan_player + "] <T> ";
                    }

                    var str4 = str;
                    str3 = string.Empty;
                    str3 = RCextensions.returnStringFromObject(player7.customProperties[PhotonPlayerProperty.name]);
                    num16 = 0;
                    num16 = RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.kills]);
                    num17 = 0;
                    num17 = RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.deaths]);
                    num18 = 0;
                    num18 = RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.max_dmg]);
                    num19 = 0;
                    num19 = RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.total_dmg]);
                    str = string.Concat(str4, string.Empty, str3, "[ffffff]:", num16, "/", num17, "/", num18, "/", num19);
                    if (RCextensions.returnBoolFromObject(player7.customProperties[PhotonPlayerProperty.dead]))
                    {
                        str = str + "[-]";
                    }

                    str = str + "\n";
                }
            }
        }

        playerList = str;
        if (PhotonNetwork.isMasterClient && !isWinning && !isLosing && roundTime >= 5f)
        {
            int num21;
            if (RCSettings.infectionMode > 0)
            {
                var num20 = 0;
                for (num21 = 0; num21 < PhotonNetwork.playerList.Length; num21++)
                {
                    var targetPlayer = PhotonNetwork.playerList[num21];
                    if (!ignoreList.Contains(targetPlayer.ID) && targetPlayer.customProperties[PhotonPlayerProperty.dead] != null && targetPlayer.customProperties[PhotonPlayerProperty.isTitan] != null)
                    {
                        if (RCextensions.returnIntFromObject(targetPlayer.customProperties[PhotonPlayerProperty.isTitan]) == 1)
                        {
                            if (RCextensions.returnBoolFromObject(targetPlayer.customProperties[PhotonPlayerProperty.dead]) && RCextensions.returnIntFromObject(targetPlayer.customProperties[PhotonPlayerProperty.deaths]) > 0)
                            {
                                if (!imatitan.ContainsKey(targetPlayer.ID))
                                {
                                    imatitan.Add(targetPlayer.ID, 2);
                                }

                                var propertiesToSet = new Hashtable();
                                propertiesToSet.Add(PhotonPlayerProperty.isTitan, 2);
                                targetPlayer.SetCustomProperties(propertiesToSet);
                                photonView.RPC("spawnTitanRPC", targetPlayer);
                            }
                            else if (imatitan.ContainsKey(targetPlayer.ID))
                            {
                                for (var i = 0; i < heroes.Count; i++)
                                {
                                    var hero = (HERO)heroes[i];
                                    if (hero.photonView.owner == targetPlayer)
                                    {
                                        hero.markDie();
                                        hero.photonView.RPC("netDie2", PhotonTargets.All, -1, "noswitchingfagt");
                                    }
                                }
                            }
                        }
                        else if (!(RCextensions.returnIntFromObject(targetPlayer.customProperties[PhotonPlayerProperty.isTitan]) != 2 || RCextensions.returnBoolFromObject(targetPlayer.customProperties[PhotonPlayerProperty.dead])))
                        {
                            num20++;
                        }
                    }
                }

                if (num20 <= 0 && IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.KILL_TITAN)
                {
                    gameWin();
                }
            }
            else if (RCSettings.pointMode > 0)
            {
                if (RCSettings.teamMode > 0)
                {
                    if (cyanKills >= RCSettings.pointMode)
                    {
                        InRoomChat.SystemMessageGlobal("<color=#00FFFF>Team Cyan wins!</color>");
                        gameWin();
                    }
                    else if (magentaKills >= RCSettings.pointMode)
                    {
                        InRoomChat.SystemMessageGlobal("<color=#00FFFF>Team Magenta wins!</color>");
                        gameWin();
                    }
                }
                else if (RCSettings.teamMode == 0)
                {
                    for (num21 = 0; num21 < PhotonNetwork.playerList.Length; num21++)
                    {
                        var player9 = PhotonNetwork.playerList[num21];
                        if (RCextensions.returnIntFromObject(player9.customProperties[PhotonPlayerProperty.kills]) >= RCSettings.pointMode)
                        {
                            InRoomChat.SystemMessageGlobal(player9, "wins!");
                            gameWin();
                        }
                    }
                }
            }
            else if (RCSettings.pointMode <= 0 && (RCSettings.bombMode == 1 || RCSettings.pvpMode > 0))
            {
                if (RCSettings.teamMode > 0 && PhotonNetwork.playerList.Length > 1)
                {
                    var num23 = 0;
                    var num24 = 0;
                    var num25 = 0;
                    var num26 = 0;
                    for (num21 = 0; num21 < PhotonNetwork.playerList.Length; num21++)
                    {
                        var player10 = PhotonNetwork.playerList[num21];
                        if (!ignoreList.Contains(player10.ID) && player10.customProperties[PhotonPlayerProperty.RCteam] != null && player10.customProperties[PhotonPlayerProperty.dead] != null)
                        {
                            if (RCextensions.returnIntFromObject(player10.customProperties[PhotonPlayerProperty.RCteam]) == 1)
                            {
                                num25++;
                                if (!RCextensions.returnBoolFromObject(player10.customProperties[PhotonPlayerProperty.dead]))
                                {
                                    num23++;
                                }
                            }
                            else if (RCextensions.returnIntFromObject(player10.customProperties[PhotonPlayerProperty.RCteam]) == 2)
                            {
                                num26++;
                                if (!RCextensions.returnBoolFromObject(player10.customProperties[PhotonPlayerProperty.dead]))
                                {
                                    num24++;
                                }
                            }
                        }
                    }

                    if (num25 > 0 && num26 > 0)
                    {
                        if (num23 == 0)
                        {
                            InRoomChat.SystemMessageGlobal("<color=#00FFFF>Team Magenta wins!</color>");
                            gameWin();
                        }
                        else if (num24 == 0)
                        {
                            InRoomChat.SystemMessageGlobal("<color=#00FFFF>Team Cyan wins!</color>");
                            gameWin();
                        }
                    }
                }
                else if (RCSettings.teamMode == 0 && PhotonNetwork.playerList.Length > 1)
                {
                    var num27 = 0;
                    var text = "Nobody";
                    var player11 = PhotonNetwork.playerList[0];
                    PhotonPlayer player = null;
                    for (num21 = 0; num21 < PhotonNetwork.playerList.Length; num21++)
                    {
                        player = PhotonNetwork.playerList[num21];
                        if (!(player.customProperties[PhotonPlayerProperty.dead] == null || RCextensions.returnBoolFromObject(player.customProperties[PhotonPlayerProperty.dead])))
                        {
                            text = RCextensions.returnStringFromObject(player.customProperties[PhotonPlayerProperty.name]).hexColor();
                            player11 = player;
                            num27++;
                        }
                    }

                    if (num27 <= 1)
                    {
                        var str6 = "5 points added.";
                        if (text == "Nobody")
                        {
                            str6 = string.Empty;
                        }
                        else
                        {
                            for (num21 = 0; num21 < 5; num21++)
                            {
                                playerKillInfoUpdate(player11, 0);
                            }
                        }

                        InRoomChat.SystemMessageGlobal($"{text} wins. {str6}");
                        gameWin();
                    }
                }
            }
        }
    }

    public void kickPlayerRC(PhotonPlayer player, bool ban, string reason)
    {
        string str;
        if (OnPrivateServer)
        {
            str = string.Empty;
            str = RCextensions.returnStringFromObject(player.customProperties[PhotonPlayerProperty.name]);
            ServerCloseConnection(player, ban, str);
        }
        else
        {
            PhotonNetwork.DestroyPlayerObjects(player);
            PhotonNetwork.CloseConnection(player);
            photonView.RPC("ignorePlayer", PhotonTargets.Others, player.ID);
            if (!ignoreList.Contains(player.ID))
            {
                ignoreList.Add(player.ID);
                var options = new RaiseEventOptions { TargetActors = new[] { player.ID } };
                PhotonNetwork.RaiseEvent(254, null, true, options);
            }

            if (!(!ban || banHash.ContainsKey(player.ID)))
            {
                str = string.Empty;
                str = RCextensions.returnStringFromObject(player.customProperties[PhotonPlayerProperty.name]);
                banHash.Add(player.ID, str);
            }

            if (reason != string.Empty)
            {
                string[] msg = { $"[{player.ID}] {player.Name.hexColor()} ", "was autobanned.\n", "Reason: ", reason };
                InRoomChat.SystemMessageLocal(msg, false);
            }

            RecompilePlayerList(0.1f);
        }
    }

    public IEnumerator LoadBackground()
    {
        using (var www = new WWW("file:///" + Application.dataPath + "/Background.png"))
        {
            yield return www;
            if (www.texture != null)
            {
                cameraObject = new GameObject();
                var camera = cameraObject.AddComponent<Camera>();
                camera.clearFlags = CameraClearFlags.Color;
                camera.depth = -1f;
                cameraObject.AddComponent<GUILayer>();
                canvasObject = new GameObject();
                var canvas = canvasObject.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                canvas.worldCamera = camera;
                canvas.pixelPerfect = true;
                canvas.sortingOrder = -1;
                var canvasScaler = canvasObject.AddComponent<CanvasScaler>();
                canvasObject.GetComponent<RectTransform>();
                canvasObject.AddComponent<CanvasRenderer>();
                canvasObject.AddComponent<GraphicRaycaster>();
                canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                canvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height);
                canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
                canvasScaler.matchWidthOrHeight = -1f;
                canvasScaler.referencePixelsPerUnit = 100f;
                var image = canvasObject.AddComponent<Image>();
                image.sprite = UnityEngine.Sprite.Create(www.texture, new Rect(0f, 0f, www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f));
                image.color = new Color(255f, 255f, 255f, 255f);
                //image.type = Image.Type.Simple;
                image.preserveAspect = false;
            }
        }
    }

    public void loadconfig()
    {
        int num;
        int num2;
        var objArray = new object[270];
        objArray[1] = PlayerPrefs.GetInt("titan", 1);
        objArray[16] = PlayerPrefs.GetInt("titantype1", -1);
        objArray[17] = PlayerPrefs.GetInt("titantype2", -1);
        objArray[18] = PlayerPrefs.GetInt("titantype3", -1);
        objArray[19] = PlayerPrefs.GetInt("titantype4", -1);
        objArray[20] = PlayerPrefs.GetInt("titantype5", -1);
        objArray[21] = PlayerPrefs.GetString("titanhair1", string.Empty);
        objArray[22] = PlayerPrefs.GetString("titanhair2", string.Empty);
        objArray[23] = PlayerPrefs.GetString("titanhair3", string.Empty);
        objArray[24] = PlayerPrefs.GetString("titanhair4", string.Empty);
        objArray[25] = PlayerPrefs.GetString("titanhair5", string.Empty);
        objArray[26] = PlayerPrefs.GetString("titaneye1", string.Empty);
        objArray[27] = PlayerPrefs.GetString("titaneye2", string.Empty);
        objArray[28] = PlayerPrefs.GetString("titaneye3", string.Empty);
        objArray[29] = PlayerPrefs.GetString("titaneye4", string.Empty);
        objArray[30] = PlayerPrefs.GetString("titaneye5", string.Empty);
        objArray[32] = PlayerPrefs.GetInt("titanR", 0);
        objArray[64] = 0;
        objArray[65] = PlayerPrefs.GetString("eren", string.Empty);
        objArray[66] = PlayerPrefs.GetString("annie", string.Empty);
        objArray[67] = PlayerPrefs.GetString("colossal", string.Empty);
        objArray[82] = PlayerPrefs.GetString("cnumber", "1");
        objArray[86] = PlayerPrefs.GetString("titanbody1", string.Empty);
        objArray[87] = PlayerPrefs.GetString("titanbody2", string.Empty);
        objArray[88] = PlayerPrefs.GetString("titanbody3", string.Empty);
        objArray[89] = PlayerPrefs.GetString("titanbody4", string.Empty);
        objArray[90] = PlayerPrefs.GetString("titanbody5", string.Empty);
        objArray[91] = 0;
        objArray[98] = PlayerPrefs.GetString("reelin", "LeftAlt");
        objArray[99] = PlayerPrefs.GetString("reelout", "Space");
        objArray[100] = 0;
        objArray[101] = PlayerPrefs.GetString("tforward", "W");
        objArray[102] = PlayerPrefs.GetString("tback", "S");
        objArray[103] = PlayerPrefs.GetString("tleft", "A");
        objArray[104] = PlayerPrefs.GetString("tright", "D");
        objArray[105] = PlayerPrefs.GetString("twalk", "LeftShift");
        objArray[106] = PlayerPrefs.GetString("tjump", "Space");
        objArray[107] = PlayerPrefs.GetString("tpunch", "Q");
        objArray[108] = PlayerPrefs.GetString("tslam", "E");
        objArray[109] = PlayerPrefs.GetString("tgrabfront", "Alpha1");
        objArray[110] = PlayerPrefs.GetString("tgrabback", "Alpha3");
        objArray[111] = PlayerPrefs.GetString("tgrabnape", "Mouse1");
        objArray[112] = PlayerPrefs.GetString("tantiae", "Mouse0");
        objArray[113] = PlayerPrefs.GetString("tbite", "Alpha2");
        objArray[114] = PlayerPrefs.GetString("tcover", "Z");
        objArray[115] = PlayerPrefs.GetString("tsit", "X");
        objArray[117] = PlayerPrefs.GetString("lforward", "W");
        objArray[118] = PlayerPrefs.GetString("lback", "S");
        objArray[119] = PlayerPrefs.GetString("lleft", "A");
        objArray[120] = PlayerPrefs.GetString("lright", "D");
        objArray[121] = PlayerPrefs.GetString("lup", "Mouse1");
        objArray[122] = PlayerPrefs.GetString("ldown", "Mouse0");
        objArray[123] = PlayerPrefs.GetString("lcursor", "X");
        objArray[124] = PlayerPrefs.GetString("lplace", "Space");
        objArray[125] = PlayerPrefs.GetString("ldel", "Backspace");
        objArray[126] = PlayerPrefs.GetString("lslow", "LeftShift");
        objArray[127] = PlayerPrefs.GetString("lrforward", "R");
        objArray[128] = PlayerPrefs.GetString("lrback", "F");
        objArray[129] = PlayerPrefs.GetString("lrleft", "Q");
        objArray[130] = PlayerPrefs.GetString("lrright", "E");
        objArray[131] = PlayerPrefs.GetString("lrccw", "Z");
        objArray[132] = PlayerPrefs.GetString("lrcw", "C");
        objArray[133] = PlayerPrefs.GetInt("humangui", 0);
        objArray[161] = PlayerPrefs.GetString("lfast", "LeftControl");
        objArray[163] = PlayerPrefs.GetString("forestskyfront", string.Empty);
        objArray[164] = PlayerPrefs.GetString("forestskyback", string.Empty);
        objArray[165] = PlayerPrefs.GetString("forestskyleft", string.Empty);
        objArray[166] = PlayerPrefs.GetString("forestskyright", string.Empty);
        objArray[167] = PlayerPrefs.GetString("forestskyup", string.Empty);
        objArray[168] = PlayerPrefs.GetString("forestskydown", string.Empty);
        objArray[169] = PlayerPrefs.GetString("cityskyfront", string.Empty);
        objArray[170] = PlayerPrefs.GetString("cityskyback", string.Empty);
        objArray[171] = PlayerPrefs.GetString("cityskyleft", string.Empty);
        objArray[172] = PlayerPrefs.GetString("cityskyright", string.Empty);
        objArray[173] = PlayerPrefs.GetString("cityskyup", string.Empty);
        objArray[174] = PlayerPrefs.GetString("cityskydown", string.Empty);
        objArray[175] = PlayerPrefs.GetString("customskyfront", string.Empty);
        objArray[176] = PlayerPrefs.GetString("customskyback", string.Empty);
        objArray[177] = PlayerPrefs.GetString("customskyleft", string.Empty);
        objArray[178] = PlayerPrefs.GetString("customskyright", string.Empty);
        objArray[179] = PlayerPrefs.GetString("customskyup", string.Empty);
        objArray[180] = PlayerPrefs.GetString("customskydown", string.Empty);
        objArray[182] = PlayerPrefs.GetString("dashkey", "RightControl");
        objArray[232] = PlayerPrefs.GetString("mapMaximize", "Tab");
        objArray[233] = PlayerPrefs.GetString("mapToggle", "M");
        objArray[234] = PlayerPrefs.GetString("mapReset", "K");
        objArray[236] = PlayerPrefs.GetString("chatRebind", "None");
        objArray[237] = PlayerPrefs.GetString("hforward", "W");
        objArray[238] = PlayerPrefs.GetString("hback", "S");
        objArray[239] = PlayerPrefs.GetString("hleft", "A");
        objArray[240] = PlayerPrefs.GetString("hright", "D");
        objArray[241] = PlayerPrefs.GetString("hwalk", "LeftShift");
        objArray[242] = PlayerPrefs.GetString("hjump", "Q");
        objArray[243] = PlayerPrefs.GetString("hmount", "LeftControl");
        objArray[245] = 0;
        objArray[254] = PlayerPrefs.GetString("cannonUp", "UpArrow");
        objArray[255] = PlayerPrefs.GetString("cannonDown", "DownArrow");
        objArray[256] = PlayerPrefs.GetString("cannonLeft", "LeftArrow");
        objArray[257] = PlayerPrefs.GetString("cannonRight", "RightArrow");
        objArray[258] = PlayerPrefs.GetString("cannonFire", "Mouse0");
        objArray[259] = PlayerPrefs.GetString("cannonMount", "LeftControl");
        objArray[260] = PlayerPrefs.GetString("cannonSlow", "LeftShift");
        objArray[262] = PlayerPrefs.GetString("liveCam", "Y");
        objArray[263] = 0;
        inputRC = new InputManagerRC();
        inputRC.setInputHuman(InputCodeRC.reelin, (string)objArray[98]);
        inputRC.setInputHuman(InputCodeRC.reelout, (string)objArray[99]);
        inputRC.setInputHuman(InputCodeRC.dash, (string)objArray[182]);
        inputRC.setInputHuman(InputCodeRC.mapMaximize, (string)objArray[232]);
        inputRC.setInputHuman(InputCodeRC.mapToggle, (string)objArray[233]);
        inputRC.setInputHuman(InputCodeRC.mapReset, (string)objArray[234]);
        inputRC.setInputHuman(InputCodeRC.chat, (string)objArray[236]);
        inputRC.setInputHuman(InputCodeRC.liveCam, (string)objArray[262]);
        if (!Enum.IsDefined(typeof(KeyCode), (string)objArray[232]))
        {
            objArray[232] = "None";
        }

        if (!Enum.IsDefined(typeof(KeyCode), (string)objArray[233]))
        {
            objArray[233] = "None";
        }

        if (!Enum.IsDefined(typeof(KeyCode), (string)objArray[234]))
        {
            objArray[234] = "None";
        }

        for (num = 0; num < 15; num++)
        {
            inputRC.setInputTitan(num, (string)objArray[101 + num]);
        }

        for (num = 0; num < 16; num++)
        {
            inputRC.setInputLevel(num, (string)objArray[117 + num]);
        }

        for (num = 0; num < 7; num++)
        {
            inputRC.setInputHorse(num, (string)objArray[237 + num]);
        }

        for (num = 0; num < 7; num++)
        {
            inputRC.setInputCannon(num, (string)objArray[254 + num]);
        }

        inputRC.setInputLevel(InputCodeRC.levelFast, (string)objArray[161]);

        linkHash = new[] { new Hashtable(), new Hashtable(), new Hashtable(), new Hashtable(), new Hashtable() };
        settings = objArray;
        scroll = Vector2.zero;
        scroll2 = Vector2.zero;
        qualitySlider = PlayerPrefs.GetFloat("GameQuality", 0f);
        transparencySlider = 1f;
    }

    public void multiplayerRacingFinsih()
    {
        var time = roundTime - 20f;
        if (PhotonNetwork.isMasterClient)
        {
            getRacingResult(LoginFengKAI.player.name, time);
        }
        else
        {
            object[] parameters = { LoginFengKAI.player.name, time };
            photonView.RPC("getRacingResult", PhotonTargets.MasterClient, parameters);
        }

        gameWin();
    }

    [RPC]
    public void netShowDamage(int speed)
    {
        GameObjectCache.Find("Stylish").GetComponent<StylishComponent>().Style(speed);
        var target = GameObjectCache.Find("LabelScore");
        if (target != null)
        {
            target.GetComponent<UILabel>().text = speed.ToString();
            target.transform.localScale = Vector3.zero;
            speed = (int)(speed * 0.1f);
            speed = Mathf.Max(40, speed);
            speed = Mathf.Min(150, speed);
            iTween.Stop(target);
            object[] args = { "x", speed, "y", speed, "z", speed, "easetype", iTween.EaseType.easeOutElastic, "time", 1f };
            iTween.ScaleTo(target, iTween.Hash(args));
            object[] objArray2 = { "x", 0, "y", 0, "z", 0, "easetype", iTween.EaseType.easeInBounce, "time", 0.5f, "delay", 2f };
            iTween.ScaleTo(target, iTween.Hash(objArray2));
        }
    }

    public void NOTSpawnNonAITitan(string id)
    {
        myLastHero = id.ToUpper();
        var hashtable = new Hashtable();
        hashtable.Add("dead", true);
        var propertiesToSet = hashtable;
        PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        hashtable = new Hashtable();
        hashtable.Add(PhotonPlayerProperty.isTitan, 2);
        propertiesToSet = hashtable;
        PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS)
        {
            Screen.lockCursor = true;
        }
        else
        {
            Screen.lockCursor = false;
        }

        Screen.showCursor = true;
        ShowHUDInfoCenter("the game has started for 60 seconds.\n please wait for next round.\n Click Right Mouse Key to Enter or Exit the Spectator Mode.");
        GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().enabled = true;
        GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().setMainObject(null);
        GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().setSpectorMode(true);
        GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().gameOver = true;
    }

    public void NOTSpawnNonAITitanRC(string id)
    {
        myLastHero = id.ToUpper();
        var hashtable = new Hashtable();
        hashtable.Add("dead", true);
        var propertiesToSet = hashtable;
        PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        hashtable = new Hashtable();
        hashtable.Add(PhotonPlayerProperty.isTitan, 2);
        propertiesToSet = hashtable;
        PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS)
        {
            Screen.lockCursor = true;
        }
        else
        {
            Screen.lockCursor = false;
        }

        Screen.showCursor = true;
        ShowHUDInfoCenter("Syncing spawn locations...");
        GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().enabled = true;
        GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().setMainObject(null);
        GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().setSpectorMode(true);
        GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().gameOver = true;
    }

    public void NOTSpawnPlayer(string id)
    {
        myLastHero = id.ToUpper();
        var hashtable = new Hashtable();
        hashtable.Add("dead", true);
        var propertiesToSet = hashtable;
        PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        hashtable = new Hashtable();
        hashtable.Add(PhotonPlayerProperty.isTitan, 1);
        propertiesToSet = hashtable;
        PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS)
        {
            Screen.lockCursor = true;
        }
        else
        {
            Screen.lockCursor = false;
        }

        Screen.showCursor = false;
        ShowHUDInfoCenter("the game has started for 60 seconds.\n please wait for next round.\n Click Right Mouse Key to Enter or Exit the Spectator Mode.");
        GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().enabled = true;
        GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().setMainObject(null);
        GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().setSpectorMode(true);
        GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().gameOver = true;
    }

    public void NOTSpawnPlayerRC(string id)
    {
        myLastHero = id.ToUpper();
        var hashtable = new Hashtable();
        hashtable.Add("dead", true);
        var propertiesToSet = hashtable;
        PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        hashtable = new Hashtable();
        hashtable.Add(PhotonPlayerProperty.isTitan, 1);
        propertiesToSet = hashtable;
        PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS)
        {
            Screen.lockCursor = true;
        }
        else
        {
            Screen.lockCursor = false;
        }

        Screen.showCursor = false;
        GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().enabled = true;
        GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().setMainObject(null);
        GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().setSpectorMode(true);
        GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().gameOver = true;
    }

    public void OnConnectedToMaster()
    {
        Print("OnConnectedToMaster");
    }

    public void OnConnectedToPhoton()
    {
        Print("OnConnectedToPhoton");
    }

    public void OnConnectionFail(DisconnectCause cause)
    {
        switch (cause)
        {
            case DisconnectCause.DisconnectByServerTimeout:
            case DisconnectCause.DisconnectByServerLogic:
            case DisconnectCause.DisconnectByClientTimeout:
                NeedRejoin = true;
                break;
        }

        Print("OnConnectionFail : " + cause);
        Screen.lockCursor = false;
        Screen.showCursor = true;
        IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
        gameStart = false;
        NGUITools.SetActive(ui.GetComponent<UIReferArray>().panels[0], false);
        NGUITools.SetActive(ui.GetComponent<UIReferArray>().panels[1], false);
        NGUITools.SetActive(ui.GetComponent<UIReferArray>().panels[2], false);
        NGUITools.SetActive(ui.GetComponent<UIReferArray>().panels[3], false);
        NGUITools.SetActive(ui.GetComponent<UIReferArray>().panels[4], true);
        GameObjectCache.Find("LabelDisconnectInfo").GetComponent<UILabel>().text = "OnConnectionFail : " + cause;
    }

    public void OnCreatedRoom()
    {
        kicklist = new ArrayList();
        racingResult = new ArrayList();
        teamScores = new int[2];
        Print("OnCreatedRoom");
    }

    public void OnCustomAuthenticationFailed()
    {
        Print("OnCustomAuthenticationFailed");
    }

    public void OnDisconnectedFromPhoton()
    {
        Print("OnDisconnectedFromPhoton");
        Screen.lockCursor = false;
        Screen.showCursor = true;
    }

    [RPC]
    public void oneTitanDown(string name1, bool onPlayerLeave)
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || PhotonNetwork.isMasterClient)
        {
            if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
            {
                if (name1 != string.Empty)
                {
                    if (name1 == "Titan")
                    {
                        PVPhumanScore++;
                    }
                    else if (name1 == "Aberrant")
                    {
                        PVPhumanScore += 2;
                    }
                    else if (name1 == "Jumper")
                    {
                        PVPhumanScore += 3;
                    }
                    else if (name1 == "Crawler")
                    {
                        PVPhumanScore += 4;
                    }
                    else if (name1 == "Female Titan")
                    {
                        PVPhumanScore += 10;
                    }
                    else
                    {
                        PVPhumanScore += 3;
                    }
                }

                checkPVPpts();
                object[] parameters = { PVPhumanScore, PVPtitanScore };
                photonView.RPC("refreshPVPStatus", PhotonTargets.Others, parameters);
            }
            else if (IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.CAGE_FIGHT)
            {
                if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.KILL_TITAN)
                {
                    if (checkIsTitanAllDie())
                    {
                        gameWin();
                        Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().gameOver = true;
                    }
                }
                else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
                {
                    if (checkIsTitanAllDie())
                    {
                        wave++;
                        Antis.ClearAntiRevive();
                        if ((LevelInfo.getInfo(level).respawnMode == RespawnMode.NEWROUND || level.StartsWith("Custom") && RCSettings.gameType == 1) && IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                        {
                            foreach (var player in PhotonNetwork.playerList)
                            {
                                if (RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.isTitan]) != 2)
                                {
                                    photonView.RPC("respawnHeroInNewRound", player);
                                }
                            }
                        }

                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                        {
                            if (Settings.LegacyChatSetting)
                            {
                                string msg = "<color=#A8FF24>Wave : " + this.wave + "</color>";
                                photonView.RPC("Chat", PhotonTargets.All, new object[] { msg, string.Empty });
                            }
                            else
                            {
                                string[] msg = { $"[Wave {wave}]" };
                                InRoomChat.SystemMessageGlobal(msg, false);

                            }
                        }

                        if (wave > highestwave)
                        {
                            highestwave = wave;
                        }

                        if (PhotonNetwork.isMasterClient)
                        {
                            RequireStatus();
                        }

                        if (RCSettings.maxWave == 0 && wave > 20 || RCSettings.maxWave > 0 && wave > RCSettings.maxWave)
                        {
                            gameWin();
                        }
                        else
                        {
                            var abnormal = 90;
                            if (difficulty == 1)
                            {
                                abnormal = 70;
                            }

                            if (!LevelInfo.getInfo(level).punk)
                            {
                                spawnTitanCustom("titanRespawn", abnormal, wave + 2, false);
                            }
                            else if (wave == 5)
                            {
                                spawnTitanCustom("titanRespawn", abnormal, 1, true);
                            }
                            else if (wave == 10)
                            {
                                spawnTitanCustom("titanRespawn", abnormal, 2, true);
                            }
                            else if (wave == 15)
                            {
                                spawnTitanCustom("titanRespawn", abnormal, 3, true);
                            }
                            else if (wave == 20)
                            {
                                spawnTitanCustom("titanRespawn", abnormal, 4, true);
                            }
                            else
                            {
                                spawnTitanCustom("titanRespawn", abnormal, wave + 2, false);
                            }
                        }
                    }
                }
                else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.ENDLESS_TITAN)
                {
                    if (!onPlayerLeave)
                    {
                        humanScore++;
                        var num2 = 90;
                        if (difficulty == 1)
                        {
                            num2 = 70;
                        }

                        spawnTitanCustom("titanRespawn", num2, 1, false);
                    }
                }
                else if (LevelInfo.getInfo(level).enemyNumber == -1)
                {
                }
            }
        }
    }

    public void OnFailedToConnectToPhoton()
    {
        Print("OnFailedToConnectToPhoton");
    }

    public void OnJoinedLobby()
    {
        if (NeedRejoin)
        {
            TryRejoinRoom();
            NeedRejoin = false;
        }

        RejoinRegion = PhotonNetwork.networkingPeer.MasterServerAddress.Split(':')[0].Replace("wss://", string.Empty).Replace("ws://", string.Empty);
    }

    private void Print(object msg)
    {
        print($"{DateTime.Now:T} | INFO | {msg}");
    }

    public void OnJoinedRoom()
    {
        HERO.PlayersSkins = new Dictionary<int, string>();
        Logger.LogChat(Logger.ChatLogPath, PhotonNetwork.room);
        maxPlayers = PhotonNetwork.room.maxPlayers;
        playerList = string.Empty;
        char[] separator = { "`"[0] };
        Print(DateTime.Now + "OnJoinedRoom " + PhotonNetwork.room.name + "    >>>>   " + LevelInfo.getInfo(PhotonNetwork.room.name.Split(separator)[1]).mapName);
        RejoinRoom = PhotonNetwork.networkingPeer.mRoomToGetInto;
        gameTimesUp = false;
        char[] chArray3 = { "`"[0] };
        var strArray = PhotonNetwork.room.name.Split(chArray3);
        level = strArray[1];
        if (strArray[2] == "normal")
        {
            difficulty = 0;
        }
        else if (strArray[2] == "hard")
        {
            difficulty = 1;
        }
        else if (strArray[2] == "abnormal")
        {
            difficulty = 2;
        }

        IN_GAME_MAIN_CAMERA.difficulty = difficulty;
        time = int.Parse(strArray[3]);
        time *= 60;
        if (strArray[4] == "day")
        {
            IN_GAME_MAIN_CAMERA.dayLight = DayLight.Day;
        }
        else if (strArray[4] == "dawn")
        {
            IN_GAME_MAIN_CAMERA.dayLight = DayLight.Dawn;
        }
        else if (strArray[4] == "night")
        {
            IN_GAME_MAIN_CAMERA.dayLight = DayLight.Night;
        }

        IN_GAME_MAIN_CAMERA.gamemode = LevelInfo.getInfo(level).type;
        PhotonNetwork.LoadLevel(LevelInfo.getInfo(level).mapName);
        var hashtable = new Hashtable();
        hashtable.Add(PhotonPlayerProperty.name, LoginFengKAI.player.name);
        hashtable.Add(PhotonPlayerProperty.guildName, LoginFengKAI.player.guildname);
        hashtable.Add(PhotonPlayerProperty.kills, 0);
        hashtable.Add(PhotonPlayerProperty.max_dmg, 0);
        hashtable.Add(PhotonPlayerProperty.total_dmg, 0);
        hashtable.Add(PhotonPlayerProperty.deaths, 0);
        hashtable.Add(PhotonPlayerProperty.dead, true);
        hashtable.Add(PhotonPlayerProperty.isTitan, 0);
        hashtable.Add(PhotonPlayerProperty.RCteam, 0);
        hashtable.Add(PhotonPlayerProperty.currentLevel, string.Empty);
        var propertiesToSet = hashtable;
        PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        humanScore = 0;
        titanScore = 0;
        PVPtitanScore = 0;
        PVPhumanScore = 0;
        wave = 1;
        highestwave = 1;
        localRacingResult = string.Empty;
        needChooseSide = true;
        chatContent = new ArrayList();
        killInfoGO = new ArrayList();
        InRoomChat.Chat = new List<string>();

        if (!PhotonNetwork.isMasterClient)
        {
            photonView.RPC("RequireStatus", PhotonTargets.MasterClient);
        }

        assetCacheTextures = new Dictionary<string, Texture2D>();
        isFirstLoad = true;
        name = nameField.Replace("[-]", "");
        LoginFengKAI.player.name = name;

        var hashtable3 = new Hashtable();
        hashtable3.Add(PhotonPlayerProperty.name, name);
        PhotonNetwork.player.SetCustomProperties(hashtable3);
        if (OnPrivateServer)
        {
            ServerRequestAuthentication(PrivateServerAuthPass);
        }
    }

    public void OnLeftLobby()
    {
        Print("OnLeftLobby");
    }

    public void OnLeftRoom()
    {
        if (Application.loadedLevel != 0)
        {
            Time.timeScale = 1f;
            if (PhotonNetwork.connected)
            {
                PhotonNetwork.Disconnect();
            }
            resetSettings(true);
            loadconfig();
            IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
            gameStart = false;
            Screen.lockCursor = false;
            Screen.showCursor = true;
            inputManager.menuOn = false;
            DestroyAllExistingCloths();
            Destroy(GameObjectCache.Find("MultiplayerManager"));
            Application.LoadLevel("menu");
        }
    }

    public void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
        if (!noRestart)
        {
            if (PhotonNetwork.isMasterClient)
            {
                restartingMC = true;
                if (RCSettings.infectionMode > 0)
                {
                    restartingTitan = true;
                }

                if (RCSettings.bombMode > 0)
                {
                    restartingBomb = true;
                }

                if (RCSettings.horseMode > 0)
                {
                    restartingHorse = true;
                }

                if (RCSettings.banEren == 0)
                {
                    restartingEren = true;
                }
            }

            resetSettings(false);
            if (!LevelInfo.getInfo(level).teamTitan)
            {
                var propertiesToSet = new Hashtable();
                propertiesToSet.Add(PhotonPlayerProperty.isTitan, 1);
                PhotonNetwork.player.SetCustomProperties(propertiesToSet);
            }

            if (!(gameTimesUp || !PhotonNetwork.isMasterClient))
            {
                restartGame(true);
                photonView.RPC("setMasterRC", PhotonTargets.All);
            }
        }

        noRestart = false;
    }

    public void OnPhotonCreateRoomFailed()
    {
        Print("OnPhotonCreateRoomFailed");
    }

    public void OnPhotonCustomRoomPropertiesChanged()
    {
        if (PhotonNetwork.isMasterClient)
        {
            if (!PhotonNetwork.room.open)
            {
                PhotonNetwork.room.open = true;
            }

            if (!PhotonNetwork.room.visible)
            {
                PhotonNetwork.room.visible = true;
            }

            if (PhotonNetwork.room.maxPlayers != maxPlayers)
            {
                PhotonNetwork.room.maxPlayers = maxPlayers;
            }
        }
        else
        {
            maxPlayers = PhotonNetwork.room.maxPlayers;
        }
    }

    public void OnPhotonInstantiate()
    {
        Print("OnPhotonInstantiate");
    }

    public void OnPhotonJoinRoomFailed()
    {
        Print("OnPhotonJoinRoomFailed");
    }

    public void OnPhotonMaxCccuReached()
    {
        Print("OnPhotonMaxCccuReached");
    }

    private IEnumerator OnPlayerConnected(PhotonPlayer player)
    {
        yield return new WaitForSeconds(1);

        if (Settings.AnnounceArrivalsSetting) InRoomChat.SystemMessageLocal(player, "has joined.");
        if (Settings.AntiGuestsSetting && player.isGuest && PhotonNetwork.isMasterClient)
        {
            kickPlayerRC(player, true, "Anti Guest enabled.");
        }
        if (Settings.AntiAbusiveModsSetting && player.isAbusive && PhotonNetwork.isMasterClient)
        {
            kickPlayerRC(player, true, "Anti Abusive mods enabled.");
        }
    }

    public void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        StartCoroutine(OnPlayerConnected(player));
        if (PhotonNetwork.isMasterClient)
        {
            var photonView = this.photonView;
            if (banHash.ContainsValue(RCextensions.returnStringFromObject(player.customProperties[PhotonPlayerProperty.name])))
            {
                kickPlayerRC(player, false, "banned.");
            }
            else
            {
                var num = RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.statACL]);
                var num2 = RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.statBLA]);
                var num3 = RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.statGAS]);
                var num4 = RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.statSPD]);
                if (num > 150 || num2 > 125 || num3 > 150 || num4 > 140)
                {
                    kickPlayerRC(player, true, "excessive stats.");
                    return;
                }

                if (RCSettings.asoPreservekdr == 1)
                {
                    StartCoroutine(WaitAndReloadKDR(player));
                }

                if (level.StartsWith("Custom"))
                {
                    StartCoroutine(customlevelE(new List<PhotonPlayer> { player }));
                }

                var hashtable = new Hashtable();
                if (RCSettings.bombMode == 1)
                {
                    hashtable.Add("bomb", 1);
                }

                if (RCSettings.globalDisableMinimap == 1)
                {
                    hashtable.Add("globalDisableMinimap", 1);
                }

                if (RCSettings.teamMode > 0)
                {
                    hashtable.Add("team", RCSettings.teamMode);
                }

                if (RCSettings.pointMode > 0)
                {
                    hashtable.Add("point", RCSettings.pointMode);
                }

                if (RCSettings.disableRock > 0)
                {
                    hashtable.Add("rock", RCSettings.disableRock);
                }

                if (RCSettings.explodeMode > 0)
                {
                    hashtable.Add("explode", RCSettings.explodeMode);
                }

                if (RCSettings.healthMode > 0)
                {
                    hashtable.Add("healthMode", RCSettings.healthMode);
                    hashtable.Add("healthLower", RCSettings.healthLower);
                    hashtable.Add("healthUpper", RCSettings.healthUpper);
                }

                if (RCSettings.infectionMode > 0)
                {
                    hashtable.Add("infection", RCSettings.infectionMode);
                }

                if (RCSettings.banEren == 1)
                {
                    hashtable.Add("eren", RCSettings.banEren);
                }

                if (RCSettings.moreTitans > 0)
                {
                    hashtable.Add("titanc", RCSettings.moreTitans);
                }

                if (RCSettings.damageMode > 0)
                {
                    hashtable.Add("damage", RCSettings.damageMode);
                }

                if (RCSettings.sizeMode > 0)
                {
                    hashtable.Add("sizeMode", RCSettings.sizeMode);
                    hashtable.Add("sizeLower", RCSettings.sizeLower);
                    hashtable.Add("sizeUpper", RCSettings.sizeUpper);
                }

                if (RCSettings.spawnMode > 0)
                {
                    hashtable.Add("spawnMode", RCSettings.spawnMode);
                    hashtable.Add("nRate", RCSettings.nRate);
                    hashtable.Add("aRate", RCSettings.aRate);
                    hashtable.Add("jRate", RCSettings.jRate);
                    hashtable.Add("cRate", RCSettings.cRate);
                    hashtable.Add("pRate", RCSettings.pRate);
                }

                if (RCSettings.waveModeOn > 0)
                {
                    hashtable.Add("waveModeOn", 1);
                    hashtable.Add("waveModeNum", RCSettings.waveModeNum);
                }

                if (RCSettings.friendlyMode > 0)
                {
                    hashtable.Add("friendly", 1);
                }

                if (RCSettings.pvpMode > 0)
                {
                    hashtable.Add("pvp", RCSettings.pvpMode);
                }

                if (RCSettings.maxWave > 0)
                {
                    hashtable.Add("maxwave", RCSettings.maxWave);
                }

                if (RCSettings.endlessMode > 0)
                {
                    hashtable.Add("endless", RCSettings.endlessMode);
                }

                if (RCSettings.motd != string.Empty)
                {
                    hashtable.Add("motd", RCSettings.motd);
                }

                if (RCSettings.horseMode > 0)
                {
                    hashtable.Add("horse", RCSettings.horseMode);
                }

                if (RCSettings.ahssReload > 0)
                {
                    hashtable.Add("ahssReload", RCSettings.ahssReload);
                }

                if (RCSettings.punkWaves > 0)
                {
                    hashtable.Add("punkWaves", RCSettings.punkWaves);
                }

                if (RCSettings.deadlyCannons > 0)
                {
                    hashtable.Add("deadlycannons", RCSettings.deadlyCannons);
                }

                if (RCSettings.racingStatic > 0)
                {
                    hashtable.Add("asoracing", RCSettings.racingStatic);
                }

                if (ignoreList != null && ignoreList.Count > 0)
                {
                    photonView.RPC("ignorePlayerArray", player, ignoreList.ToArray());
                }

                photonView.RPC("settingRPC", player, hashtable);
                photonView.RPC("setMasterRC", player);
                if (Time.timeScale <= 0.1f && pauseWaitTime > 3f)
                {
                    photonView.RPC("pauseRPC", player, true);
                    object[] parameters = { InRoomChat.ChatFormatting("MasterClient ", Settings.ChatMinorColorSetting, Settings.ChatMinorFormatSettings[0], Settings.ChatMinorFormatSettings[1]) + InRoomChat.ChatFormatting("has paused the game.", Settings.ChatMajorColorSetting, Settings.ChatMajorFormatSettings[0], Settings.ChatMajorFormatSettings[1]), "" };
                    photonView.RPC("Chat", player, parameters);
                }
            }
            if (player.GucciGangMod && Settings.LocationSkinsSetting == 2) photonView.RPC("extendedSkinsRPC", player, Settings.ExtendedSkinsToSend);
        }
        RecompilePlayerList(0.1f);
        GGM.Discord.RichPresence.UpdateStatus();
    }

    public void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        if (Settings.AnnounceArrivalsSetting) InRoomChat.SystemMessageLocal(player, "has left.");

        if (!gameTimesUp)
        {
            oneTitanDown(string.Empty, true);
            someOneIsDead(0);
        }

        if (ignoreList.Contains(player.ID))
        {
            ignoreList.Remove(player.ID);
        }

        InstantiateTracker.instance.TryRemovePlayer(player.ID);
        if (PhotonNetwork.isMasterClient)
        {
            photonView.RPC("verifyPlayerHasLeft", PhotonTargets.All, player.ID);
        }

        if (RCSettings.asoPreservekdr == 1)
        {
            var key = RCextensions.returnStringFromObject(player.customProperties[PhotonPlayerProperty.name]);
            if (PreservedPlayerKDR.ContainsKey(key))
            {
                PreservedPlayerKDR.Remove(key);
            }

            int[] numArray2 = { RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.kills]), RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.deaths]), RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.max_dmg]), RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.total_dmg]) };
            PreservedPlayerKDR.Add(key, numArray2);
        }

        RecompilePlayerList(0.1f);
        GGM.Discord.RichPresence.UpdateStatus();
    }

    public void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
    {
        RecompilePlayerList(0.1f);
        if (playerAndUpdatedProps != null && playerAndUpdatedProps.Length >= 2 && (PhotonPlayer)playerAndUpdatedProps[0] == PhotonNetwork.player)
        {
            Hashtable hashtable2;
            var hashtable = (Hashtable)playerAndUpdatedProps[1];
            if (hashtable.ContainsKey("name") && RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]) != name)
            {
                hashtable2 = new Hashtable();
                hashtable2.Add(PhotonPlayerProperty.name, name);
                PhotonNetwork.player.SetCustomProperties(hashtable2);
            }

            if (hashtable.ContainsKey("statACL") || hashtable.ContainsKey("statBLA") || hashtable.ContainsKey("statGAS") || hashtable.ContainsKey("statSPD"))
            {
                var player = PhotonNetwork.player;
                var num = RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.statACL]);
                var num2 = RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.statBLA]);
                var num3 = RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.statGAS]);
                var num4 = RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.statSPD]);
                if (num > 150)
                {
                    hashtable2 = new Hashtable();
                    hashtable2.Add(PhotonPlayerProperty.statACL, 100);
                    PhotonNetwork.player.SetCustomProperties(hashtable2);
                    num = 100;
                }

                if (num2 > 125)
                {
                    hashtable2 = new Hashtable();
                    hashtable2.Add(PhotonPlayerProperty.statBLA, 100);
                    PhotonNetwork.player.SetCustomProperties(hashtable2);
                    num2 = 100;
                }

                if (num3 > 150)
                {
                    hashtable2 = new Hashtable();
                    hashtable2.Add(PhotonPlayerProperty.statGAS, 100);
                    PhotonNetwork.player.SetCustomProperties(hashtable2);
                    num3 = 100;
                }

                if (num4 > 140)
                {
                    hashtable2 = new Hashtable();
                    hashtable2.Add(PhotonPlayerProperty.statSPD, 100);
                    PhotonNetwork.player.SetCustomProperties(hashtable2);
                    num4 = 100;
                }
            }
        }
    }

    public void OnPhotonRandomJoinFailed()
    {
        Print("OnPhotonRandomJoinFailed");
    }

    public void OnPhotonSerializeView()
    {
        Print("OnPhotonSerializeView");
    }

    public void OnReceivedRoomListUpdate()
    {
        GGM.Discord.RichPresence.UpdateStatus();
    }

    public void OnUpdate()
    {
        if (RCEvents.ContainsKey("OnUpdate"))
        {
            if (updateTime > 0f)
            {
                updateTime -= Time.deltaTime;
            }
            else
            {
                ((RCEvent)RCEvents["OnUpdate"]).checkEvent();
                updateTime = 1f;
            }
        }
    }

    public void OnUpdatedFriendList()
    {
        Print("OnUpdatedFriendList");
    }

    public int operantType(string str, int condition)
    {
        switch (condition)
        {
            case 0:
            case 3:
                if (!str.StartsWith("Equals"))
                {
                    if (str.StartsWith("NotEquals"))
                    {
                        return 5;
                    }

                    if (!str.StartsWith("LessThan"))
                    {
                        if (str.StartsWith("LessThanOrEquals"))
                        {
                            return 1;
                        }

                        if (str.StartsWith("GreaterThanOrEquals"))
                        {
                            return 3;
                        }

                        if (str.StartsWith("GreaterThan"))
                        {
                            return 4;
                        }
                    }

                    return 0;
                }

                return 2;

            case 1:
            case 4:
            case 5:
                if (!str.StartsWith("Equals"))
                {
                    if (str.StartsWith("NotEquals"))
                    {
                        return 5;
                    }

                    return 0;
                }

                return 2;

            case 2:
                if (!str.StartsWith("Equals"))
                {
                    if (str.StartsWith("NotEquals"))
                    {
                        return 1;
                    }

                    if (str.StartsWith("Contains"))
                    {
                        return 2;
                    }

                    if (str.StartsWith("NotContains"))
                    {
                        return 3;
                    }

                    if (str.StartsWith("StartsWith"))
                    {
                        return 4;
                    }

                    if (str.StartsWith("NotStartsWith"))
                    {
                        return 5;
                    }

                    if (str.StartsWith("EndsWith"))
                    {
                        return 6;
                    }

                    if (str.StartsWith("NotEndsWith"))
                    {
                        return 7;
                    }

                    return 0;
                }

                return 0;
        }

        return 0;
    }

    public RCEvent parseBlock(string[] stringArray, int eventClass, int eventType, RCCondition condition)
    {
        var sentTrueActions = new List<RCAction>();
        var event2 = new RCEvent(null, null, 0, 0);
        for (var i = 0; i < stringArray.Length; i++)
        {
            int num2;
            int num3;
            int num4;
            int length;
            string[] strArray;
            int num6;
            int num7;
            int index;
            int num9;
            string str;
            int num10;
            int num11;
            int num12;
            string[] strArray2;
            RCCondition condition2;
            RCEvent event3;
            RCAction action;
            if (stringArray[i].StartsWith("If") && stringArray[i + 1] == "{")
            {
                num2 = i + 2;
                num3 = i + 2;
                num4 = 0;
                length = i + 2;
                while (length < stringArray.Length)
                {
                    if (stringArray[length] == "{")
                    {
                        num4++;
                    }

                    if (stringArray[length] == "}")
                    {
                        if (num4 > 0)
                        {
                            num4--;
                        }
                        else
                        {
                            num3 = length - 1;
                            length = stringArray.Length;
                        }
                    }

                    length++;
                }

                strArray = new string[num3 - num2 + 1];
                num6 = 0;
                num7 = num2;
                while (num7 <= num3)
                {
                    strArray[num6] = stringArray[num7];
                    num6++;
                    num7++;
                }

                index = stringArray[i].IndexOf("(");
                num9 = stringArray[i].LastIndexOf(")");
                str = stringArray[i].Substring(index + 1, num9 - index - 1);
                num10 = conditionType(str);
                num11 = str.IndexOf('.');
                str = str.Substring(num11 + 1);
                num12 = operantType(str, num10);
                index = str.IndexOf('(');
                num9 = str.LastIndexOf(")");
                strArray2 = str.Substring(index + 1, num9 - index - 1).Split(',');
                condition2 = new RCCondition(num12, num10, returnHelper(strArray2[0]), returnHelper(strArray2[1]));
                event3 = parseBlock(strArray, 1, 0, condition2);
                action = new RCAction(0, 0, event3, null);
                event2 = event3;
                sentTrueActions.Add(action);
                i = num3;
            }
            else if (stringArray[i].StartsWith("While") && stringArray[i + 1] == "{")
            {
                num2 = i + 2;
                num3 = i + 2;
                num4 = 0;
                length = i + 2;
                while (length < stringArray.Length)
                {
                    if (stringArray[length] == "{")
                    {
                        num4++;
                    }

                    if (stringArray[length] == "}")
                    {
                        if (num4 > 0)
                        {
                            num4--;
                        }
                        else
                        {
                            num3 = length - 1;
                            length = stringArray.Length;
                        }
                    }

                    length++;
                }

                strArray = new string[num3 - num2 + 1];
                num6 = 0;
                num7 = num2;
                while (num7 <= num3)
                {
                    strArray[num6] = stringArray[num7];
                    num6++;
                    num7++;
                }

                index = stringArray[i].IndexOf("(");
                num9 = stringArray[i].LastIndexOf(")");
                str = stringArray[i].Substring(index + 1, num9 - index - 1);
                num10 = conditionType(str);
                num11 = str.IndexOf('.');
                str = str.Substring(num11 + 1);
                num12 = operantType(str, num10);
                index = str.IndexOf('(');
                num9 = str.LastIndexOf(")");
                strArray2 = str.Substring(index + 1, num9 - index - 1).Split(',');
                condition2 = new RCCondition(num12, num10, returnHelper(strArray2[0]), returnHelper(strArray2[1]));
                event3 = parseBlock(strArray, 3, 0, condition2);
                action = new RCAction(0, 0, event3, null);
                sentTrueActions.Add(action);
                i = num3;
            }
            else if (stringArray[i].StartsWith("ForeachTitan") && stringArray[i + 1] == "{")
            {
                num2 = i + 2;
                num3 = i + 2;
                num4 = 0;
                length = i + 2;
                while (length < stringArray.Length)
                {
                    if (stringArray[length] == "{")
                    {
                        num4++;
                    }

                    if (stringArray[length] == "}")
                    {
                        if (num4 > 0)
                        {
                            num4--;
                        }
                        else
                        {
                            num3 = length - 1;
                            length = stringArray.Length;
                        }
                    }

                    length++;
                }

                strArray = new string[num3 - num2 + 1];
                num6 = 0;
                num7 = num2;
                while (num7 <= num3)
                {
                    strArray[num6] = stringArray[num7];
                    num6++;
                    num7++;
                }

                index = stringArray[i].IndexOf("(");
                num9 = stringArray[i].LastIndexOf(")");
                str = stringArray[i].Substring(index + 2, num9 - index - 3);
                num10 = 0;
                event3 = parseBlock(strArray, 2, num10, null);
                event3.foreachVariableName = str;
                action = new RCAction(0, 0, event3, null);
                sentTrueActions.Add(action);
                i = num3;
            }
            else if (stringArray[i].StartsWith("ForeachPlayer") && stringArray[i + 1] == "{")
            {
                num2 = i + 2;
                num3 = i + 2;
                num4 = 0;
                length = i + 2;
                while (length < stringArray.Length)
                {
                    if (stringArray[length] == "{")
                    {
                        num4++;
                    }

                    if (stringArray[length] == "}")
                    {
                        if (num4 > 0)
                        {
                            num4--;
                        }
                        else
                        {
                            num3 = length - 1;
                            length = stringArray.Length;
                        }
                    }

                    length++;
                }

                strArray = new string[num3 - num2 + 1];
                num6 = 0;
                num7 = num2;
                while (num7 <= num3)
                {
                    strArray[num6] = stringArray[num7];
                    num6++;
                    num7++;
                }

                index = stringArray[i].IndexOf("(");
                num9 = stringArray[i].LastIndexOf(")");
                str = stringArray[i].Substring(index + 2, num9 - index - 3);
                num10 = 1;
                event3 = parseBlock(strArray, 2, num10, null);
                event3.foreachVariableName = str;
                action = new RCAction(0, 0, event3, null);
                sentTrueActions.Add(action);
                i = num3;
            }
            else if (stringArray[i].StartsWith("Else") && stringArray[i + 1] == "{")
            {
                num2 = i + 2;
                num3 = i + 2;
                num4 = 0;
                length = i + 2;
                while (length < stringArray.Length)
                {
                    if (stringArray[length] == "{")
                    {
                        num4++;
                    }

                    if (stringArray[length] == "}")
                    {
                        if (num4 > 0)
                        {
                            num4--;
                        }
                        else
                        {
                            num3 = length - 1;
                            length = stringArray.Length;
                        }
                    }

                    length++;
                }

                strArray = new string[num3 - num2 + 1];
                num6 = 0;
                for (num7 = num2; num7 <= num3; num7++)
                {
                    strArray[num6] = stringArray[num7];
                    num6++;
                }

                if (stringArray[i] == "Else")
                {
                    event3 = parseBlock(strArray, 0, 0, null);
                    action = new RCAction(0, 0, event3, null);
                    event2.setElse(action);
                    i = num3;
                }
                else if (stringArray[i].StartsWith("Else If"))
                {
                    index = stringArray[i].IndexOf("(");
                    num9 = stringArray[i].LastIndexOf(")");
                    str = stringArray[i].Substring(index + 1, num9 - index - 1);
                    num10 = conditionType(str);
                    num11 = str.IndexOf('.');
                    str = str.Substring(num11 + 1);
                    num12 = operantType(str, num10);
                    index = str.IndexOf('(');
                    num9 = str.LastIndexOf(")");
                    strArray2 = str.Substring(index + 1, num9 - index - 1).Split(',');
                    condition2 = new RCCondition(num12, num10, returnHelper(strArray2[0]), returnHelper(strArray2[1]));
                    event3 = parseBlock(strArray, 1, 0, condition2);
                    action = new RCAction(0, 0, event3, null);
                    event2.setElse(action);
                    i = num3;
                }
            }
            else
            {
                int num13;
                int num14;
                int num15;
                int num16;
                string str2;
                string[] strArray3;
                RCActionHelper helper;
                RCActionHelper helper2;
                RCActionHelper helper3;
                if (stringArray[i].StartsWith("VariableInt"))
                {
                    num13 = 1;
                    num14 = stringArray[i].IndexOf('.');
                    num15 = stringArray[i].IndexOf('(');
                    num16 = stringArray[i].LastIndexOf(')');
                    str2 = stringArray[i].Substring(num14 + 1, num15 - num14 - 1);
                    strArray3 = stringArray[i].Substring(num15 + 1, num16 - num15 - 1).Split(',');
                    if (str2.StartsWith("SetRandom"))
                    {
                        helper = returnHelper(strArray3[0]);
                        helper2 = returnHelper(strArray3[1]);
                        helper3 = returnHelper(strArray3[2]);
                        action = new RCAction(num13, 12, null, new[] { helper, helper2, helper3 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Set"))
                    {
                        helper = returnHelper(strArray3[0]);
                        helper2 = returnHelper(strArray3[1]);
                        action = new RCAction(num13, 0, null, new[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Add"))
                    {
                        helper = returnHelper(strArray3[0]);
                        helper2 = returnHelper(strArray3[1]);
                        action = new RCAction(num13, 1, null, new[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Subtract"))
                    {
                        helper = returnHelper(strArray3[0]);
                        helper2 = returnHelper(strArray3[1]);
                        action = new RCAction(num13, 2, null, new[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Multiply"))
                    {
                        helper = returnHelper(strArray3[0]);
                        helper2 = returnHelper(strArray3[1]);
                        action = new RCAction(num13, 3, null, new[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Divide"))
                    {
                        helper = returnHelper(strArray3[0]);
                        helper2 = returnHelper(strArray3[1]);
                        action = new RCAction(num13, 4, null, new[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Modulo"))
                    {
                        helper = returnHelper(strArray3[0]);
                        helper2 = returnHelper(strArray3[1]);
                        action = new RCAction(num13, 5, null, new[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Power"))
                    {
                        helper = returnHelper(strArray3[0]);
                        helper2 = returnHelper(strArray3[1]);
                        action = new RCAction(num13, 6, null, new[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                }
                else if (stringArray[i].StartsWith("VariableBool"))
                {
                    num13 = 2;
                    num14 = stringArray[i].IndexOf('.');
                    num15 = stringArray[i].IndexOf('(');
                    num16 = stringArray[i].LastIndexOf(')');
                    str2 = stringArray[i].Substring(num14 + 1, num15 - num14 - 1);
                    strArray3 = stringArray[i].Substring(num15 + 1, num16 - num15 - 1).Split(',');
                    if (str2.StartsWith("SetToOpposite"))
                    {
                        helper = returnHelper(strArray3[0]);
                        action = new RCAction(num13, 11, null, new[] { helper });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("SetRandom"))
                    {
                        helper = returnHelper(strArray3[0]);
                        action = new RCAction(num13, 12, null, new[] { helper });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Set"))
                    {
                        helper = returnHelper(strArray3[0]);
                        helper2 = returnHelper(strArray3[1]);
                        action = new RCAction(num13, 0, null, new[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                }
                else if (stringArray[i].StartsWith("VariableString"))
                {
                    num13 = 3;
                    num14 = stringArray[i].IndexOf('.');
                    num15 = stringArray[i].IndexOf('(');
                    num16 = stringArray[i].LastIndexOf(')');
                    str2 = stringArray[i].Substring(num14 + 1, num15 - num14 - 1);
                    strArray3 = stringArray[i].Substring(num15 + 1, num16 - num15 - 1).Split(',');
                    if (str2.StartsWith("Set"))
                    {
                        helper = returnHelper(strArray3[0]);
                        helper2 = returnHelper(strArray3[1]);
                        action = new RCAction(num13, 0, null, new[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Concat"))
                    {
                        var helpers = new RCActionHelper[strArray3.Length];
                        for (length = 0; length < strArray3.Length; length++)
                        {
                            helpers[length] = returnHelper(strArray3[length]);
                        }

                        action = new RCAction(num13, 7, null, helpers);
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Append"))
                    {
                        helper = returnHelper(strArray3[0]);
                        helper2 = returnHelper(strArray3[1]);
                        action = new RCAction(num13, 8, null, new[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Replace"))
                    {
                        helper = returnHelper(strArray3[0]);
                        helper2 = returnHelper(strArray3[1]);
                        helper3 = returnHelper(strArray3[2]);
                        action = new RCAction(num13, 10, null, new[] { helper, helper2, helper3 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Remove"))
                    {
                        helper = returnHelper(strArray3[0]);
                        helper2 = returnHelper(strArray3[1]);
                        action = new RCAction(num13, 9, null, new[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                }
                else if (stringArray[i].StartsWith("VariableFloat"))
                {
                    num13 = 4;
                    num14 = stringArray[i].IndexOf('.');
                    num15 = stringArray[i].IndexOf('(');
                    num16 = stringArray[i].LastIndexOf(')');
                    str2 = stringArray[i].Substring(num14 + 1, num15 - num14 - 1);
                    strArray3 = stringArray[i].Substring(num15 + 1, num16 - num15 - 1).Split(',');
                    if (str2.StartsWith("SetRandom"))
                    {
                        helper = returnHelper(strArray3[0]);
                        helper2 = returnHelper(strArray3[1]);
                        helper3 = returnHelper(strArray3[2]);
                        action = new RCAction(num13, 12, null, new[] { helper, helper2, helper3 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Set"))
                    {
                        helper = returnHelper(strArray3[0]);
                        helper2 = returnHelper(strArray3[1]);
                        action = new RCAction(num13, 0, null, new[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Add"))
                    {
                        helper = returnHelper(strArray3[0]);
                        helper2 = returnHelper(strArray3[1]);
                        action = new RCAction(num13, 1, null, new[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Subtract"))
                    {
                        helper = returnHelper(strArray3[0]);
                        helper2 = returnHelper(strArray3[1]);
                        action = new RCAction(num13, 2, null, new[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Multiply"))
                    {
                        helper = returnHelper(strArray3[0]);
                        helper2 = returnHelper(strArray3[1]);
                        action = new RCAction(num13, 3, null, new[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Divide"))
                    {
                        helper = returnHelper(strArray3[0]);
                        helper2 = returnHelper(strArray3[1]);
                        action = new RCAction(num13, 4, null, new[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Modulo"))
                    {
                        helper = returnHelper(strArray3[0]);
                        helper2 = returnHelper(strArray3[1]);
                        action = new RCAction(num13, 5, null, new[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Power"))
                    {
                        helper = returnHelper(strArray3[0]);
                        helper2 = returnHelper(strArray3[1]);
                        action = new RCAction(num13, 6, null, new[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                }
                else if (stringArray[i].StartsWith("VariablePlayer"))
                {
                    num13 = 5;
                    num14 = stringArray[i].IndexOf('.');
                    num15 = stringArray[i].IndexOf('(');
                    num16 = stringArray[i].LastIndexOf(')');
                    str2 = stringArray[i].Substring(num14 + 1, num15 - num14 - 1);
                    strArray3 = stringArray[i].Substring(num15 + 1, num16 - num15 - 1).Split(',');
                    if (str2.StartsWith("Set"))
                    {
                        helper = returnHelper(strArray3[0]);
                        helper2 = returnHelper(strArray3[1]);
                        action = new RCAction(num13, 0, null, new[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                }
                else if (stringArray[i].StartsWith("VariableTitan"))
                {
                    num13 = 6;
                    num14 = stringArray[i].IndexOf('.');
                    num15 = stringArray[i].IndexOf('(');
                    num16 = stringArray[i].LastIndexOf(')');
                    str2 = stringArray[i].Substring(num14 + 1, num15 - num14 - 1);
                    strArray3 = stringArray[i].Substring(num15 + 1, num16 - num15 - 1).Split(',');
                    if (str2.StartsWith("Set"))
                    {
                        helper = returnHelper(strArray3[0]);
                        helper2 = returnHelper(strArray3[1]);
                        action = new RCAction(num13, 0, null, new[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                }
                else
                {
                    RCActionHelper helper4;
                    if (stringArray[i].StartsWith("Player"))
                    {
                        num13 = 7;
                        num14 = stringArray[i].IndexOf('.');
                        num15 = stringArray[i].IndexOf('(');
                        num16 = stringArray[i].LastIndexOf(')');
                        str2 = stringArray[i].Substring(num14 + 1, num15 - num14 - 1);
                        strArray3 = stringArray[i].Substring(num15 + 1, num16 - num15 - 1).Split(',');
                        if (str2.StartsWith("KillPlayer"))
                        {
                            helper = returnHelper(strArray3[0]);
                            helper2 = returnHelper(strArray3[1]);
                            action = new RCAction(num13, 0, null, new[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SpawnPlayerAt"))
                        {
                            helper = returnHelper(strArray3[0]);
                            helper2 = returnHelper(strArray3[1]);
                            helper3 = returnHelper(strArray3[2]);
                            helper4 = returnHelper(strArray3[3]);
                            action = new RCAction(num13, 2, null, new[] { helper, helper2, helper3, helper4 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SpawnPlayer"))
                        {
                            helper = returnHelper(strArray3[0]);
                            action = new RCAction(num13, 1, null, new[] { helper });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("MovePlayer"))
                        {
                            helper = returnHelper(strArray3[0]);
                            helper2 = returnHelper(strArray3[1]);
                            helper3 = returnHelper(strArray3[2]);
                            helper4 = returnHelper(strArray3[3]);
                            action = new RCAction(num13, 3, null, new[] { helper, helper2, helper3, helper4 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetKills"))
                        {
                            helper = returnHelper(strArray3[0]);
                            helper2 = returnHelper(strArray3[1]);
                            action = new RCAction(num13, 4, null, new[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetDeaths"))
                        {
                            helper = returnHelper(strArray3[0]);
                            helper2 = returnHelper(strArray3[1]);
                            action = new RCAction(num13, 5, null, new[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetMaxDmg"))
                        {
                            helper = returnHelper(strArray3[0]);
                            helper2 = returnHelper(strArray3[1]);
                            action = new RCAction(num13, 6, null, new[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetTotalDmg"))
                        {
                            helper = returnHelper(strArray3[0]);
                            helper2 = returnHelper(strArray3[1]);
                            action = new RCAction(num13, 7, null, new[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetName"))
                        {
                            helper = returnHelper(strArray3[0]);
                            helper2 = returnHelper(strArray3[1]);
                            action = new RCAction(num13, 8, null, new[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetGuildName"))
                        {
                            helper = returnHelper(strArray3[0]);
                            helper2 = returnHelper(strArray3[1]);
                            action = new RCAction(num13, 9, null, new[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetTeam"))
                        {
                            helper = returnHelper(strArray3[0]);
                            helper2 = returnHelper(strArray3[1]);
                            action = new RCAction(num13, 10, null, new[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetCustomInt"))
                        {
                            helper = returnHelper(strArray3[0]);
                            helper2 = returnHelper(strArray3[1]);
                            action = new RCAction(num13, 11, null, new[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetCustomBool"))
                        {
                            helper = returnHelper(strArray3[0]);
                            helper2 = returnHelper(strArray3[1]);
                            action = new RCAction(num13, 12, null, new[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetCustomString"))
                        {
                            helper = returnHelper(strArray3[0]);
                            helper2 = returnHelper(strArray3[1]);
                            action = new RCAction(num13, 13, null, new[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetCustomFloat"))
                        {
                            helper = returnHelper(strArray3[0]);
                            helper2 = returnHelper(strArray3[1]);
                            action = new RCAction(num13, 14, null, new[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                    }
                    else if (stringArray[i].StartsWith("Titan"))
                    {
                        num13 = 8;
                        num14 = stringArray[i].IndexOf('.');
                        num15 = stringArray[i].IndexOf('(');
                        num16 = stringArray[i].LastIndexOf(')');
                        str2 = stringArray[i].Substring(num14 + 1, num15 - num14 - 1);
                        strArray3 = stringArray[i].Substring(num15 + 1, num16 - num15 - 1).Split(',');
                        if (str2.StartsWith("KillTitan"))
                        {
                            helper = returnHelper(strArray3[0]);
                            helper2 = returnHelper(strArray3[1]);
                            helper3 = returnHelper(strArray3[2]);
                            action = new RCAction(num13, 0, null, new[] { helper, helper2, helper3 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SpawnTitanAt"))
                        {
                            helper = returnHelper(strArray3[0]);
                            helper2 = returnHelper(strArray3[1]);
                            helper3 = returnHelper(strArray3[2]);
                            helper4 = returnHelper(strArray3[3]);
                            var helper5 = returnHelper(strArray3[4]);
                            var helper6 = returnHelper(strArray3[5]);
                            var helper7 = returnHelper(strArray3[6]);
                            action = new RCAction(num13, 2, null, new[] { helper, helper2, helper3, helper4, helper5, helper6, helper7 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SpawnTitan"))
                        {
                            helper = returnHelper(strArray3[0]);
                            helper2 = returnHelper(strArray3[1]);
                            helper3 = returnHelper(strArray3[2]);
                            helper4 = returnHelper(strArray3[3]);
                            action = new RCAction(num13, 1, null, new[] { helper, helper2, helper3, helper4 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetHealth"))
                        {
                            helper = returnHelper(strArray3[0]);
                            helper2 = returnHelper(strArray3[1]);
                            action = new RCAction(num13, 3, null, new[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("MoveTitan"))
                        {
                            helper = returnHelper(strArray3[0]);
                            helper2 = returnHelper(strArray3[1]);
                            helper3 = returnHelper(strArray3[2]);
                            helper4 = returnHelper(strArray3[3]);
                            action = new RCAction(num13, 4, null, new[] { helper, helper2, helper3, helper4 });
                            sentTrueActions.Add(action);
                        }
                    }
                    else if (stringArray[i].StartsWith("Game"))
                    {
                        num13 = 9;
                        num14 = stringArray[i].IndexOf('.');
                        num15 = stringArray[i].IndexOf('(');
                        num16 = stringArray[i].LastIndexOf(')');
                        str2 = stringArray[i].Substring(num14 + 1, num15 - num14 - 1);
                        strArray3 = stringArray[i].Substring(num15 + 1, num16 - num15 - 1).Split(',');
                        if (str2.StartsWith("PrintMessage"))
                        {
                            helper = returnHelper(strArray3[0]);
                            action = new RCAction(num13, 0, null, new[] { helper });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("LoseGame"))
                        {
                            helper = returnHelper(strArray3[0]);
                            action = new RCAction(num13, 2, null, new[] { helper });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("WinGame"))
                        {
                            helper = returnHelper(strArray3[0]);
                            action = new RCAction(num13, 1, null, new[] { helper });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("Restart"))
                        {
                            helper = returnHelper(strArray3[0]);
                            action = new RCAction(num13, 3, null, new[] { helper });
                            sentTrueActions.Add(action);
                        }
                    }
                }
            }
        }

        return new RCEvent(condition, sentTrueActions, eventClass, eventType);
    }

    [RPC]
    public void pauseRPC(bool pause, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            if (pause)
            {
                pauseWaitTime = 100000f;
                Time.timeScale = 1E-06f;
            }
            else
            {
                pauseWaitTime = 3f;
            }
        }
    }

    public void playerKillInfoSingleUpdate(int dmg)
    {
        single_kills++;
        single_maxDamage = Mathf.Max(dmg, single_maxDamage);
        single_totalDamage += dmg;
        GGM.Discord.RichPresence.UpdateStatus();
        UpdateDamageFeedLabel(dmg);
    }

    public void playerKillInfoUpdate(PhotonPlayer player, int dmg)
    {
        var propertiesToSet = new Hashtable();
        propertiesToSet.Add(PhotonPlayerProperty.kills, (int)player.customProperties[PhotonPlayerProperty.kills] + 1);
        player.SetCustomProperties(propertiesToSet);
        propertiesToSet = new Hashtable();
        propertiesToSet.Add(PhotonPlayerProperty.max_dmg, Mathf.Max(dmg, (int)player.customProperties[PhotonPlayerProperty.max_dmg]));
        player.SetCustomProperties(propertiesToSet);
        propertiesToSet = new Hashtable();
        propertiesToSet.Add(PhotonPlayerProperty.total_dmg, (int)player.customProperties[PhotonPlayerProperty.total_dmg] + dmg);
        player.SetCustomProperties(propertiesToSet);
    }

    public GameObject randomSpawnOneTitan(string place, int rate)
    {
        var objArray = GameObject.FindGameObjectsWithTag(place);
        var index = Random.Range(0, objArray.Length);
        var obj2 = objArray[index];
        while (objArray[index] == null)
        {
            index = Random.Range(0, objArray.Length);
            obj2 = objArray[index];
        }

        objArray[index] = null;
        return spawnTitan(rate, obj2.transform.position, obj2.transform.rotation, false);
    }

    public void randomSpawnTitan(string place, int rate, int num, bool punk)
    {
        if (num == -1)
        {
            num = 1;
        }

        var objArray = GameObject.FindGameObjectsWithTag(place);
        if (objArray.Length > 0)
        {
            for (var i = 0; i < num; i++)
            {
                var index = Random.Range(0, objArray.Length);
                var obj2 = objArray[index];
                while (objArray[index] == null)
                {
                    index = Random.Range(0, objArray.Length);
                    obj2 = objArray[index];
                }

                objArray[index] = null;
                spawnTitan(rate, obj2.transform.position, obj2.transform.rotation, punk);
            }
        }
    }

    public Texture2D RCLoadTexture(string tex)
    {
        if (assetCacheTextures == null)
        {
            assetCacheTextures = new Dictionary<string, Texture2D>();
        }

        if (assetCacheTextures.ContainsKey(tex))
        {
            return assetCacheTextures[tex];
        }

        var textured2 = (Texture2D)ResourcesCache.RCLoadT2D(tex);
        assetCacheTextures.Add(tex, textured2);
        return textured2;
    }

    public void RecompilePlayerList(float time)
    {
        if (!isRecompiling)
        {
            isRecompiling = true;
            StartCoroutine(WaitAndRecompilePlayerList(time));
        }
    }

    public IEnumerator reloadSky()
    {
        yield return new WaitForSeconds(0.5f);
        if (skyMaterial != null && Camera.main.GetComponent<Skybox>().material != skyMaterial)
        {
            Camera.main.GetComponent<Skybox>().material = skyMaterial;
        }

        Screen.lockCursor = !Screen.lockCursor;
        Screen.lockCursor = !Screen.lockCursor;
    }

    public void removeCT(COLOSSAL_TITAN titan)
    {
        cT.Remove(titan);
    }

    public void removeET(TITAN_EREN hero)
    {
        eT.Remove(hero);
    }

    public void removeFT(FEMALE_TITAN titan)
    {
        fT.Remove(titan);
    }

    public void removeHero(HERO hero)
    {
        heroes.Remove(hero);
    }

    public void removeHook(Bullet h)
    {
        hooks.Remove(h);
    }

    public void removeTitan(TITAN titan)
    {
        titans.Remove(titan);
    }

    public IEnumerator restartE(float time)
    {
        yield return new WaitForSeconds(time);
        restartGame(false);
    }

    public void restartGame(bool masterclientSwitched)
    {
        if (!gameTimesUp)
        {
            PVPtitanScore = 0;
            PVPhumanScore = 0;
            startRacing = false;
            endRacing = false;
            checkpoint = null;
            timeElapse = 0f;
            roundTime = 0f;
            isWinning = false;
            isLosing = false;
            wave = 1;
            myRespawnTime = 0f;
            kicklist = new ArrayList();
            killInfoGO = new ArrayList();
            racingResult = new ArrayList();
            ShowHUDInfoCenter(string.Empty);
            isRestarting = true;
            DestroyAllExistingCloths();
            PhotonNetwork.DestroyAll();
            var hash = checkGameGUI();
            photonView.RPC("settingRPC", PhotonTargets.Others);
            photonView.RPC("RPCLoadLevel", PhotonTargets.All);
            setGameSettings(hash);
            if (masterclientSwitched)
            {
                var msg = InRoomChat.ChatFormatting("MasterClient ", Settings.ChatMinorColorSetting, Settings.ChatMinorFormatSettings[0], Settings.ChatMinorFormatSettings[1]) + InRoomChat.ChatFormatting("has switched to ", Settings.ChatMajorColorSetting, Settings.ChatMajorFormatSettings[0], Settings.ChatMajorFormatSettings[1]) + InRoomChat.ChatFormatting($"[{PhotonNetwork.player.ID}] ", Settings.ChatMinorColorSetting, Settings.ChatMinorFormatSettings[0], Settings.ChatMinorFormatSettings[1]) + PhotonNetwork.player.Name.hexColor();
                InRoomChat.AddLineChat($"<size={Settings.ChatSizeSetting}>{msg}</size>");
            }
        }
    }

    public void restartGameSingle()
    {
        startRacing = false;
        endRacing = false;
        checkpoint = null;
        single_kills = 0;
        single_maxDamage = 0;
        single_totalDamage = 0;
        timeElapse = 0f;
        roundTime = 0f;
        timeTotalServer = 0f;
        isWinning = false;
        isLosing = false;
        wave = 1;
        myRespawnTime = 0f;
        ShowHUDInfoCenter(string.Empty);
        DestroyAllExistingCloths();
        Application.LoadLevel(Application.loadedLevel);
    }

    public void restartRC()
    {
        intVariables.Clear();
        boolVariables.Clear();
        stringVariables.Clear();
        floatVariables.Clear();
        playerVariables.Clear();
        titanVariables.Clear();
        if (RCSettings.infectionMode > 0)
        {
            endGameInfectionRC();
        }
        else
        {
            endGameRC();
        }
    }

    public RCActionHelper returnHelper(string str)
    {
        float num;
        int num3;
        var strArray = str.Split('.');
        if (float.TryParse(str, out num))
        {
            strArray = new[] { str };
        }

        var list = new List<RCActionHelper>();
        var sentType = 0;
        for (num3 = 0; num3 < strArray.Length; num3++)
        {
            string str2;
            RCActionHelper helper;
            if (list.Count == 0)
            {
                str2 = strArray[num3];
                if (str2.StartsWith("\"") && str2.EndsWith("\""))
                {
                    helper = new RCActionHelper(0, 0, str2.Substring(1, str2.Length - 2));
                    list.Add(helper);
                    sentType = 2;
                }
                else
                {
                    int num4;
                    if (int.TryParse(str2, out num4))
                    {
                        helper = new RCActionHelper(0, 0, num4);
                        list.Add(helper);
                        sentType = 0;
                    }
                    else
                    {
                        float num5;
                        if (float.TryParse(str2, out num5))
                        {
                            helper = new RCActionHelper(0, 0, num5);
                            list.Add(helper);
                            sentType = 3;
                        }
                        else if (str2.ToLower() == "true" || str2.ToLower() == "false")
                        {
                            helper = new RCActionHelper(0, 0, Convert.ToBoolean(str2.ToLower()));
                            list.Add(helper);
                            sentType = 1;
                        }
                        else
                        {
                            int index;
                            int num7;
                            if (str2.StartsWith("Variable"))
                            {
                                index = str2.IndexOf('(');
                                num7 = str2.LastIndexOf(')');
                                if (str2.StartsWith("VariableInt"))
                                {
                                    str2 = str2.Substring(index + 1, num7 - index - 1);
                                    helper = new RCActionHelper(1, 0, returnHelper(str2));
                                    list.Add(helper);
                                    sentType = 0;
                                }
                                else if (str2.StartsWith("VariableBool"))
                                {
                                    str2 = str2.Substring(index + 1, num7 - index - 1);
                                    helper = new RCActionHelper(1, 1, returnHelper(str2));
                                    list.Add(helper);
                                    sentType = 1;
                                }
                                else if (str2.StartsWith("VariableString"))
                                {
                                    str2 = str2.Substring(index + 1, num7 - index - 1);
                                    helper = new RCActionHelper(1, 2, returnHelper(str2));
                                    list.Add(helper);
                                    sentType = 2;
                                }
                                else if (str2.StartsWith("VariableFloat"))
                                {
                                    str2 = str2.Substring(index + 1, num7 - index - 1);
                                    helper = new RCActionHelper(1, 3, returnHelper(str2));
                                    list.Add(helper);
                                    sentType = 3;
                                }
                                else if (str2.StartsWith("VariablePlayer"))
                                {
                                    str2 = str2.Substring(index + 1, num7 - index - 1);
                                    helper = new RCActionHelper(1, 4, returnHelper(str2));
                                    list.Add(helper);
                                    sentType = 4;
                                }
                                else if (str2.StartsWith("VariableTitan"))
                                {
                                    str2 = str2.Substring(index + 1, num7 - index - 1);
                                    helper = new RCActionHelper(1, 5, returnHelper(str2));
                                    list.Add(helper);
                                    sentType = 5;
                                }
                            }
                            else if (str2.StartsWith("Region"))
                            {
                                index = str2.IndexOf('(');
                                num7 = str2.LastIndexOf(')');
                                if (str2.StartsWith("RegionRandomX"))
                                {
                                    str2 = str2.Substring(index + 1, num7 - index - 1);
                                    helper = new RCActionHelper(4, 0, returnHelper(str2));
                                    list.Add(helper);
                                    sentType = 3;
                                }
                                else if (str2.StartsWith("RegionRandomY"))
                                {
                                    str2 = str2.Substring(index + 1, num7 - index - 1);
                                    helper = new RCActionHelper(4, 1, returnHelper(str2));
                                    list.Add(helper);
                                    sentType = 3;
                                }
                                else if (str2.StartsWith("RegionRandomZ"))
                                {
                                    str2 = str2.Substring(index + 1, num7 - index - 1);
                                    helper = new RCActionHelper(4, 2, returnHelper(str2));
                                    list.Add(helper);
                                    sentType = 3;
                                }
                            }
                        }
                    }
                }

                continue;
            }

            if (list.Count <= 0)
            {
                continue;
            }

            str2 = strArray[num3];
            if (list[list.Count - 1].helperClass != 1)
            {
                goto Label_0AF5;
            }

            switch (list[list.Count - 1].helperType)
            {
                case 4:
                    {
                        if (!str2.StartsWith("GetTeam()"))
                        {
                            break;
                        }

                        helper = new RCActionHelper(2, 1, null);
                        list.Add(helper);
                        sentType = 0;
                        continue;
                    }

                case 5:
                    {
                        if (!str2.StartsWith("GetType()"))
                        {
                            goto Label_0918;
                        }

                        helper = new RCActionHelper(3, 0, null);
                        list.Add(helper);
                        sentType = 0;
                        continue;
                    }

                default:
                    goto Label_0A1C;
            }

            if (str2.StartsWith("GetType()"))
            {
                helper = new RCActionHelper(2, 0, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("GetIsAlive()"))
            {
                helper = new RCActionHelper(2, 2, null);
                list.Add(helper);
                sentType = 1;
            }
            else if (str2.StartsWith("GetTitan()"))
            {
                helper = new RCActionHelper(2, 3, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("GetKills()"))
            {
                helper = new RCActionHelper(2, 4, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("GetDeaths()"))
            {
                helper = new RCActionHelper(2, 5, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("GetMaxDmg()"))
            {
                helper = new RCActionHelper(2, 6, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("GetTotalDmg()"))
            {
                helper = new RCActionHelper(2, 7, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("GetCustomInt()"))
            {
                helper = new RCActionHelper(2, 8, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("GetCustomBool()"))
            {
                helper = new RCActionHelper(2, 9, null);
                list.Add(helper);
                sentType = 1;
            }
            else if (str2.StartsWith("GetCustomString()"))
            {
                helper = new RCActionHelper(2, 10, null);
                list.Add(helper);
                sentType = 2;
            }
            else if (str2.StartsWith("GetCustomFloat()"))
            {
                helper = new RCActionHelper(2, 11, null);
                list.Add(helper);
                sentType = 3;
            }
            else if (str2.StartsWith("GetPositionX()"))
            {
                helper = new RCActionHelper(2, 14, null);
                list.Add(helper);
                sentType = 3;
            }
            else if (str2.StartsWith("GetPositionY()"))
            {
                helper = new RCActionHelper(2, 15, null);
                list.Add(helper);
                sentType = 3;
            }
            else if (str2.StartsWith("GetPositionZ()"))
            {
                helper = new RCActionHelper(2, 16, null);
                list.Add(helper);
                sentType = 3;
            }
            else if (str2.StartsWith("GetName()"))
            {
                helper = new RCActionHelper(2, 12, null);
                list.Add(helper);
                sentType = 2;
            }
            else if (str2.StartsWith("GetGuildName()"))
            {
                helper = new RCActionHelper(2, 13, null);
                list.Add(helper);
                sentType = 2;
            }
            else if (str2.StartsWith("GetSpeed()"))
            {
                helper = new RCActionHelper(2, 17, null);
                list.Add(helper);
                sentType = 3;
            }

            continue;
        Label_0918:
            if (str2.StartsWith("GetSize()"))
            {
                helper = new RCActionHelper(3, 1, null);
                list.Add(helper);
                sentType = 3;
            }
            else if (str2.StartsWith("GetHealth()"))
            {
                helper = new RCActionHelper(3, 2, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("GetPositionX()"))
            {
                helper = new RCActionHelper(3, 3, null);
                list.Add(helper);
                sentType = 3;
            }
            else if (str2.StartsWith("GetPositionY()"))
            {
                helper = new RCActionHelper(3, 4, null);
                list.Add(helper);
                sentType = 3;
            }
            else if (str2.StartsWith("GetPositionZ()"))
            {
                helper = new RCActionHelper(3, 5, null);
                list.Add(helper);
                sentType = 3;
            }

            continue;
        Label_0A1C:
            if (str2.StartsWith("ConvertToInt()"))
            {
                helper = new RCActionHelper(5, sentType, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("ConvertToBool()"))
            {
                helper = new RCActionHelper(5, sentType, null);
                list.Add(helper);
                sentType = 1;
            }
            else if (str2.StartsWith("ConvertToString()"))
            {
                helper = new RCActionHelper(5, sentType, null);
                list.Add(helper);
                sentType = 2;
            }
            else if (str2.StartsWith("ConvertToFloat()"))
            {
                helper = new RCActionHelper(5, sentType, null);
                list.Add(helper);
                sentType = 3;
            }

            continue;
        Label_0AF5:
            if (str2.StartsWith("ConvertToInt()"))
            {
                helper = new RCActionHelper(5, sentType, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("ConvertToBool()"))
            {
                helper = new RCActionHelper(5, sentType, null);
                list.Add(helper);
                sentType = 1;
            }
            else if (str2.StartsWith("ConvertToString()"))
            {
                helper = new RCActionHelper(5, sentType, null);
                list.Add(helper);
                sentType = 2;
            }
            else if (str2.StartsWith("ConvertToFloat()"))
            {
                helper = new RCActionHelper(5, sentType, null);
                list.Add(helper);
                sentType = 3;
            }
        }

        for (num3 = list.Count - 1; num3 > 0; num3--)
        {
            list[num3 - 1].setNextHelper(list[num3]);
        }

        return list[0];
    }

    public void sendKillInfo(bool t1, string killer, bool t2, string victim, int dmg)
    {
        object[] parameters = { t1, killer, t2, victim, dmg };
        photonView.RPC("updateKillInfo", PhotonTargets.All, parameters);
    }

    public void SetPause(bool state)
    {
        if (Commands.MCRequired())
        {
            return;
        }
        if (state) Page.GetInstance<Pause>().Enable();
        photonView.RPC("pauseRPC", PhotonTargets.AllBuffered, state);
        string[] msg = { "MasterClient ", "has " + (state ? "paused" : "unpaused") + " the game." };
        InRoomChat.SystemMessageGlobal(msg, false);
    }

    [RPC]
    public void someOneIsDead(int id = -1)
    {
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
        {
            if (id != 0)
            {
                PVPtitanScore += 2;
            }

            checkPVPpts();
            object[] parameters = { PVPhumanScore, PVPtitanScore };
            photonView.RPC("refreshPVPStatus", PhotonTargets.Others, parameters);
        }
        else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.ENDLESS_TITAN)
        {
            titanScore++;
        }
        else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.KILL_TITAN || IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE || IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.BOSS_FIGHT_CT || IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.TROST)
        {
            if (isPlayerAllDead())
            {
                gameLose();
            }
        }
        else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS && RCSettings.pvpMode == 0 && RCSettings.bombMode == 0)
        {
            if (isPlayerAllDead())
            {
                gameLose();
                teamWinner = 0;
            }

            if (isTeamAllDead(1))
            {
                teamWinner = 2;
                gameWin();
            }

            if (isTeamAllDead(2))
            {
                teamWinner = 1;
                gameWin();
            }
        }
    }

    public void SpawnNonAITitan(string id, string tag = "titanRespawn")
    {
        GameObject obj3;
        var objArray = GameObject.FindGameObjectsWithTag(tag);
        var obj2 = objArray[Random.Range(0, objArray.Length)];
        myLastHero = id.ToUpper();
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
        {
            obj3 = PhotonNetwork.Instantiate("TITAN_VER3.1", checkpoint.transform.position + new Vector3(Random.Range(-20, 20), 2f, Random.Range(-20, 20)), checkpoint.transform.rotation, 0);
        }
        else
        {
            obj3 = PhotonNetwork.Instantiate("TITAN_VER3.1", obj2.transform.position, obj2.transform.rotation, 0);
        }

        GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().setMainObjectASTITAN(obj3);
        obj3.GetComponent<TITAN>().nonAI = true;
        obj3.GetComponent<TITAN>().speed = 30f;
        obj3.GetComponent<TITAN_CONTROLLER>().enabled = true;
        if (id == "RANDOM" && Random.Range(0, 100) < 7)
        {
            obj3.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_CRAWLER, true);
        }

        GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().enabled = true;
        GameObjectCache.Find("MainCamera").GetComponent<SpectatorMovement>().disable = true;
        GameObjectCache.Find("MainCamera").GetComponent<MouseLook>().disable = true;
        GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().gameOver = false;
        var hashtable = new Hashtable();
        hashtable.Add("dead", false);
        var propertiesToSet = hashtable;
        PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        hashtable = new Hashtable();
        hashtable.Add(PhotonPlayerProperty.isTitan, 2);
        propertiesToSet = hashtable;
        PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS)
        {
            Screen.lockCursor = true;
        }
        else
        {
            Screen.lockCursor = false;
        }

        Screen.showCursor = true;
        ShowHUDInfoCenter(string.Empty);
    }

    public void SpawnNonAITitan2(string id, string tag = "titanRespawn")
    {
        if (logicLoaded && customLevelLoaded)
        {
            GameObject obj3;
            var objArray = GameObject.FindGameObjectsWithTag(tag);
            var obj2 = objArray[Random.Range(0, objArray.Length)];
            var position = obj2.transform.position;
            if (level.StartsWith("Custom") && titanSpawns.Count > 0)
            {
                position = titanSpawns[Random.Range(0, titanSpawns.Count)];
            }

            myLastHero = id.ToUpper();
            if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
            {
                obj3 = PhotonNetwork.Instantiate("TITAN_VER3.1", checkpoint.transform.position + new Vector3(Random.Range(-20, 20), 2f, Random.Range(-20, 20)), checkpoint.transform.rotation, 0);
            }
            else
            {
                obj3 = PhotonNetwork.Instantiate("TITAN_VER3.1", position, obj2.transform.rotation, 0);
            }

            GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().setMainObjectASTITAN(obj3);
            obj3.GetComponent<TITAN>().nonAI = true;
            obj3.GetComponent<TITAN>().speed = 30f;
            obj3.GetComponent<TITAN_CONTROLLER>().enabled = true;
            if (id == "RANDOM" && Random.Range(0, 100) < 7)
            {
                obj3.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_CRAWLER, true);
            }

            GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().enabled = true;
            GameObjectCache.Find("MainCamera").GetComponent<SpectatorMovement>().disable = true;
            GameObjectCache.Find("MainCamera").GetComponent<MouseLook>().disable = true;
            GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().gameOver = false;
            var hashtable = new Hashtable();
            hashtable.Add("dead", false);
            var propertiesToSet = hashtable;
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
            hashtable = new Hashtable();
            hashtable.Add(PhotonPlayerProperty.isTitan, 2);
            propertiesToSet = hashtable;
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
            if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS)
            {
                Screen.lockCursor = true;
            }
            else
            {
                Screen.lockCursor = false;
            }

            Screen.showCursor = true;
            ShowHUDInfoCenter(string.Empty);
        }
        else
        {
            NOTSpawnNonAITitanRC(id);
        }
    }

    public void SpawnPlayer(string id, string tag = "playerRespawn")
    {
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
        {
            SpawnPlayerAt(id, checkpoint);
        }
        else
        {
            myLastRespawnTag = tag;
            var objArray = GameObject.FindGameObjectsWithTag(tag);
            var pos = objArray[Random.Range(0, objArray.Length)];
            SpawnPlayerAt(id, pos);
        }
    }

    public void SpawnPlayerAt(string id, GameObject pos)
    {
        if (!logicLoaded || !customLevelLoaded)
        {
            NOTSpawnPlayerRC(id);
        }
        else
        {
            var position = pos.transform.position;
            if (racingSpawnPointSet)
            {
                position = racingSpawnPoint;
            }
            else if (level.StartsWith("Custom"))
            {
                if (RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam]) == 0)
                {
                    var list = new List<Vector3>();
                    foreach (var vector2 in playerSpawnsC)
                    {
                        list.Add(vector2);
                    }

                    foreach (var vector2 in playerSpawnsM)
                    {
                        list.Add(vector2);
                    }

                    if (list.Count > 0)
                    {
                        position = list[Random.Range(0, list.Count)];
                    }
                }
                else if (RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam]) == 1)
                {
                    if (playerSpawnsC.Count > 0)
                    {
                        position = playerSpawnsC[Random.Range(0, playerSpawnsC.Count)];
                    }
                }
                else if (RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam]) == 2 && playerSpawnsM.Count > 0)
                {
                    position = playerSpawnsM[Random.Range(0, playerSpawnsM.Count)];
                }
            }

            var component = GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>();
            myLastHero = id.ToUpper();
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                if (IN_GAME_MAIN_CAMERA.singleCharacter == "TITAN_EREN")
                {
                    component.setMainObject((GameObject)Instantiate(Resources.Load("TITAN_EREN"), pos.transform.position, pos.transform.rotation));
                }
                else
                {
                    component.setMainObject((GameObject)Instantiate(Resources.Load("AOTTG_HERO 1"), pos.transform.position, pos.transform.rotation));
                    if (IN_GAME_MAIN_CAMERA.singleCharacter == "SET 1" || IN_GAME_MAIN_CAMERA.singleCharacter == "SET 2" || IN_GAME_MAIN_CAMERA.singleCharacter == "SET 3")
                    {
                        var costume = CostumeConeveter.LocalDataToHeroCostume(IN_GAME_MAIN_CAMERA.singleCharacter);
                        costume.checkstat();
                        CostumeConeveter.HeroCostumeToLocalData(costume, IN_GAME_MAIN_CAMERA.singleCharacter);
                        component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().init();
                        if (costume != null)
                        {
                            component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().myCostume = costume;
                            component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().myCostume.stat = costume.stat;
                        }
                        else
                        {
                            costume = HeroCostume.costumeOption[3];
                            component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().myCostume = costume;
                            component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().myCostume.stat = HeroStat.getInfo(costume.name.ToUpper());
                        }

                        component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().setCharacterComponent();
                        component.main_object.GetComponent<HERO>().setStat();
                        component.main_object.GetComponent<HERO>().setSkillHUDPosition();
                    }
                    else
                    {
                        for (var i = 0; i < HeroCostume.costume.Length; i++)
                        {
                            if (HeroCostume.costume[i].name.ToUpper() == IN_GAME_MAIN_CAMERA.singleCharacter.ToUpper())
                            {
                                var index = HeroCostume.costume[i].id + CheckBoxCostume.costumeSet - 1;
                                if (HeroCostume.costume[index].name != HeroCostume.costume[i].name)
                                {
                                    index = HeroCostume.costume[i].id + 1;
                                }

                                component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().init();
                                component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().myCostume = HeroCostume.costume[index];
                                component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().myCostume.stat = HeroStat.getInfo(HeroCostume.costume[index].name.ToUpper());
                                component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().setCharacterComponent();
                                component.main_object.GetComponent<HERO>().setStat();
                                component.main_object.GetComponent<HERO>().setSkillHUDPosition();
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                component.setMainObject(PhotonNetwork.Instantiate("AOTTG_HERO 1", position, pos.transform.rotation, 0));
                id = id.ToUpper();
                if (id == "SET 1" || id == "SET 2" || id == "SET 3")
                {
                    var costume2 = CostumeConeveter.LocalDataToHeroCostume(id);
                    costume2.checkstat();
                    CostumeConeveter.HeroCostumeToLocalData(costume2, id);
                    component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().init();
                    if (costume2 != null)
                    {
                        component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().myCostume = costume2;
                        component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().myCostume.stat = costume2.stat;
                    }
                    else
                    {
                        costume2 = HeroCostume.costumeOption[3];
                        component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().myCostume = costume2;
                        component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().myCostume.stat = HeroStat.getInfo(costume2.name.ToUpper());
                    }

                    component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().setCharacterComponent();
                    component.main_object.GetComponent<HERO>().setStat();
                    component.main_object.GetComponent<HERO>().setSkillHUDPosition();
                }
                else
                {
                    for (var j = 0; j < HeroCostume.costume.Length; j++)
                    {
                        if (HeroCostume.costume[j].name.ToUpper() == id.ToUpper())
                        {
                            var num4 = HeroCostume.costume[j].id;
                            if (id.ToUpper() != "AHSS")
                            {
                                num4 += CheckBoxCostume.costumeSet - 1;
                            }

                            if (HeroCostume.costume[num4].name != HeroCostume.costume[j].name)
                            {
                                num4 = HeroCostume.costume[j].id + 1;
                            }

                            component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().init();
                            component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().myCostume = HeroCostume.costume[num4];
                            component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().myCostume.stat = HeroStat.getInfo(HeroCostume.costume[num4].name.ToUpper());
                            component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().setCharacterComponent();
                            component.main_object.GetComponent<HERO>().setStat();
                            component.main_object.GetComponent<HERO>().setSkillHUDPosition();
                            break;
                        }
                    }
                }

                CostumeConeveter.HeroCostumeToPhotonData2(component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().myCostume, PhotonNetwork.player);
                if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
                {
                    var transform = component.main_object.transform;
                    transform.position += new Vector3(Random.Range(-20, 20), 2f, Random.Range(-20, 20));
                }

                var hashtable = new Hashtable();
                hashtable.Add("dead", false);
                var propertiesToSet = hashtable;
                PhotonNetwork.player.SetCustomProperties(propertiesToSet);
                hashtable = new Hashtable();
                hashtable.Add(PhotonPlayerProperty.isTitan, 1);
                propertiesToSet = hashtable;
                PhotonNetwork.player.SetCustomProperties(propertiesToSet);
            }

            component.enabled = true;
            GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().setHUDposition();
            GameObjectCache.Find("MainCamera").GetComponent<SpectatorMovement>().disable = true;
            GameObjectCache.Find("MainCamera").GetComponent<MouseLook>().disable = true;
            component.gameOver = false;
            if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS)
            {
                Screen.lockCursor = true;
            }
            else
            {
                Screen.lockCursor = false;
            }

            Screen.showCursor = false;
            isLosing = false;
            ShowHUDInfoCenter(string.Empty);
        }
    }

    [RPC]
    public void spawnPlayerAtRPC(float posX, float posY, float posZ, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient && logicLoaded && customLevelLoaded && !needChooseSide && Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().gameOver)
        {
            var position = new Vector3(posX, posY, posZ);
            var component = Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>();
            component.setMainObject(PhotonNetwork.Instantiate("AOTTG_HERO 1", position, new Quaternion(0f, 0f, 0f, 1f), 0));
            var slot = myLastHero.ToUpper();
            switch (slot)
            {
                case "SET 1":
                case "SET 2":
                case "SET 3":
                    {
                        var costume = CostumeConeveter.LocalDataToHeroCostume(slot);
                        costume.checkstat();
                        CostumeConeveter.HeroCostumeToLocalData(costume, slot);
                        component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().init();
                        if (costume != null)
                        {
                            component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().myCostume = costume;
                            component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().myCostume.stat = costume.stat;
                        }
                        else
                        {
                            costume = HeroCostume.costumeOption[3];
                            component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().myCostume = costume;
                            component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().myCostume.stat = HeroStat.getInfo(costume.name.ToUpper());
                        }

                        component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().setCharacterComponent();
                        component.main_object.GetComponent<HERO>().setStat();
                        component.main_object.GetComponent<HERO>().setSkillHUDPosition();
                        break;
                    }

                default:
                    for (var i = 0; i < HeroCostume.costume.Length; i++)
                    {
                        if (HeroCostume.costume[i].name.ToUpper() == slot.ToUpper())
                        {
                            var id = HeroCostume.costume[i].id;
                            if (slot.ToUpper() != "AHSS")
                            {
                                id += CheckBoxCostume.costumeSet - 1;
                            }

                            if (HeroCostume.costume[id].name != HeroCostume.costume[i].name)
                            {
                                id = HeroCostume.costume[i].id + 1;
                            }

                            component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().init();
                            component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().myCostume = HeroCostume.costume[id];
                            component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().myCostume.stat = HeroStat.getInfo(HeroCostume.costume[id].name.ToUpper());
                            component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().setCharacterComponent();
                            component.main_object.GetComponent<HERO>().setStat();
                            component.main_object.GetComponent<HERO>().setSkillHUDPosition();
                            break;
                        }
                    }

                    break;
            }

            CostumeConeveter.HeroCostumeToPhotonData2(component.main_object.GetComponent<HERO>().GetComponent<HERO_SETUP>().myCostume, PhotonNetwork.player);
            if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
            {
                var transform = component.main_object.transform;
                transform.position += new Vector3(Random.Range(-20, 20), 2f, Random.Range(-20, 20));
            }

            var hashtable = new Hashtable();
            hashtable.Add("dead", false);
            var propertiesToSet = hashtable;
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
            hashtable = new Hashtable();
            hashtable.Add(PhotonPlayerProperty.isTitan, 1);
            propertiesToSet = hashtable;
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
            component.enabled = true;
            GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().setHUDposition();
            GameObjectCache.Find("MainCamera").GetComponent<SpectatorMovement>().disable = true;
            GameObjectCache.Find("MainCamera").GetComponent<MouseLook>().disable = true;
            component.gameOver = false;
            if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS)
            {
                Screen.lockCursor = true;
            }
            else
            {
                Screen.lockCursor = false;
            }

            Screen.showCursor = false;
            isLosing = false;
            ShowHUDInfoCenter(string.Empty);
        }
    }

    public GameObject spawnTitan(int rate, Vector3 position, Quaternion rotation, bool punk)
    {
        GameObject obj3;
        var obj2 = spawnTitanRaw(position, rotation);
        if (punk)
        {
            obj2.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_PUNK, false);
        }
        else if (Random.Range(0, 100) < rate)
        {
            if (IN_GAME_MAIN_CAMERA.difficulty == 2)
            {
                if (Random.Range(0f, 1f) < 0.7f || LevelInfo.getInfo(level).noCrawler)
                {
                    obj2.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_JUMPER, false);
                }
                else
                {
                    obj2.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_CRAWLER, false);
                }
            }
        }
        else if (IN_GAME_MAIN_CAMERA.difficulty == 2)
        {
            if (Random.Range(0f, 1f) < 0.7f || LevelInfo.getInfo(level).noCrawler)
            {
                obj2.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_JUMPER, false);
            }
            else
            {
                obj2.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_CRAWLER, false);
            }
        }
        else if (Random.Range(0, 100) < rate)
        {
            if (Random.Range(0f, 1f) < 0.8f || LevelInfo.getInfo(level).noCrawler)
            {
                obj2.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_I, false);
            }
            else
            {
                obj2.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_CRAWLER, false);
            }
        }
        else if (Random.Range(0f, 1f) < 0.8f || LevelInfo.getInfo(level).noCrawler)
        {
            obj2.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_JUMPER, false);
        }
        else
        {
            obj2.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_CRAWLER, false);
        }

        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            obj3 = (GameObject)Instantiate(Resources.Load("FX/FXtitanSpawn"), obj2.transform.position, Quaternion.Euler(-90f, 0f, 0f));
        }
        else
        {
            obj3 = PhotonNetwork.Instantiate("FX/FXtitanSpawn", obj2.transform.position, Quaternion.Euler(-90f, 0f, 0f), 0);
        }

        obj3.transform.localScale = obj2.transform.localScale;
        return obj2;
    }

    public void spawnTitanAction(int type, float size, int health, int number)
    {
        var position = new Vector3(Random.Range(-400f, 400f), 0f, Random.Range(-400f, 400f));
        var rotation = new Quaternion(0f, 0f, 0f, 1f);
        if (titanSpawns.Count > 0)
        {
            position = titanSpawns[Random.Range(0, titanSpawns.Count)];
        }
        else
        {
            var objArray = GameObject.FindGameObjectsWithTag("titanRespawn");
            if (objArray.Length > 0)
            {
                var index = Random.Range(0, objArray.Length);
                var obj2 = objArray[index];
                while (objArray[index] == null)
                {
                    index = Random.Range(0, objArray.Length);
                    obj2 = objArray[index];
                }

                objArray[index] = null;
                position = obj2.transform.position;
                rotation = obj2.transform.rotation;
            }
        }

        for (var i = 0; i < number; i++)
        {
            var obj3 = spawnTitanRaw(position, rotation);
            obj3.GetComponent<TITAN>().resetLevel(size);
            obj3.GetComponent<TITAN>().hasSetLevel = true;
            if (health > 0f)
            {
                obj3.GetComponent<TITAN>().currentHealth = health;
                obj3.GetComponent<TITAN>().maxHealth = health;
            }

            switch (type)
            {
                case 0:
                    obj3.GetComponent<TITAN>().setAbnormalType(AbnormalType.NORMAL, false);
                    break;

                case 1:
                    obj3.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_I, false);
                    break;

                case 2:
                    obj3.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_JUMPER, false);
                    break;

                case 3:
                    obj3.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_CRAWLER, true);
                    break;

                case 4:
                    obj3.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_PUNK, false);
                    break;
            }
        }
    }

    public void spawnTitanAtAction(int type, float size, int health, int number, float posX, float posY, float posZ)
    {
        var position = new Vector3(posX, posY, posZ);
        var rotation = new Quaternion(0f, 0f, 0f, 1f);
        for (var i = 0; i < number; i++)
        {
            var obj2 = spawnTitanRaw(position, rotation);
            obj2.GetComponent<TITAN>().resetLevel(size);
            obj2.GetComponent<TITAN>().hasSetLevel = true;
            if (health > 0f)
            {
                obj2.GetComponent<TITAN>().currentHealth = health;
                obj2.GetComponent<TITAN>().maxHealth = health;
            }

            switch (type)
            {
                case 0:
                    obj2.GetComponent<TITAN>().setAbnormalType(AbnormalType.NORMAL, false);
                    break;

                case 1:
                    obj2.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_I, false);
                    break;

                case 2:
                    obj2.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_JUMPER, false);
                    break;

                case 3:
                    obj2.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_CRAWLER, true);
                    break;

                case 4:
                    obj2.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_PUNK, false);
                    break;
            }
        }
    }

    public void spawnTitanCustom(string type, int abnormal, int rate, bool punk)
    {
        int num8;
        Vector3 position;
        Quaternion rotation;
        GameObject[] objArray;
        int num9;
        GameObject obj2;
        var moreTitans = rate;
        if (level.StartsWith("Custom"))
        {
            moreTitans = 5;
            if (RCSettings.gameType == 1)
            {
                moreTitans = 3;
            }
            else if (RCSettings.gameType == 2 || RCSettings.gameType == 3)
            {
                moreTitans = 0;
            }
        }

        if (RCSettings.moreTitans > 0 || RCSettings.moreTitans == 0 && level.StartsWith("Custom") && RCSettings.gameType >= 2)
        {
            moreTitans = RCSettings.moreTitans;
        }

        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
        {
            if (punk)
            {
                moreTitans = rate;
            }
            else
            {
                int waveModeNum;
                if (RCSettings.moreTitans == 0)
                {
                    waveModeNum = 1;
                    if (RCSettings.waveModeOn == 1)
                    {
                        waveModeNum = RCSettings.waveModeNum;
                    }

                    moreTitans += (wave - 1) * (waveModeNum - 1);
                }
                else if (RCSettings.moreTitans > 0)
                {
                    waveModeNum = 1;
                    if (RCSettings.waveModeOn == 1)
                    {
                        waveModeNum = RCSettings.waveModeNum;
                    }

                    moreTitans += (wave - 1) * waveModeNum;
                }
            }
        }

        moreTitans = Math.Min(50, moreTitans);
        if (RCSettings.spawnMode == 1)
        {
            var nRate = RCSettings.nRate;
            var aRate = RCSettings.aRate;
            var jRate = RCSettings.jRate;
            var cRate = RCSettings.cRate;
            var pRate = RCSettings.pRate;
            if (punk && RCSettings.punkWaves == 1)
            {
                nRate = 0f;
                aRate = 0f;
                jRate = 0f;
                cRate = 0f;
                pRate = 100f;
                moreTitans = rate;
            }

            for (num8 = 0; num8 < moreTitans; num8++)
            {
                position = new Vector3(Random.Range(-400f, 400f), 0f, Random.Range(-400f, 400f));
                rotation = new Quaternion(0f, 0f, 0f, 1f);
                if (titanSpawns.Count > 0)
                {
                    position = titanSpawns[Random.Range(0, titanSpawns.Count)];
                }
                else
                {
                    objArray = GameObject.FindGameObjectsWithTag("titanRespawn");
                    if (objArray.Length > 0)
                    {
                        num9 = Random.Range(0, objArray.Length);
                        obj2 = objArray[num9];
                        while (objArray[num9] == null)
                        {
                            num9 = Random.Range(0, objArray.Length);
                            obj2 = objArray[num9];
                        }

                        objArray[num9] = null;
                        position = obj2.transform.position;
                        rotation = obj2.transform.rotation;
                    }
                }

                var num10 = Random.Range(0f, 100f);
                if (num10 <= nRate + aRate + jRate + cRate + pRate)
                {
                    var obj3 = spawnTitanRaw(position, rotation);
                    if (num10 < nRate)
                    {
                        obj3.GetComponent<TITAN>().setAbnormalType(AbnormalType.NORMAL, false);
                    }
                    else if (num10 >= nRate && num10 < nRate + aRate)
                    {
                        obj3.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_I, false);
                    }
                    else if (num10 >= nRate + aRate && num10 < nRate + aRate + jRate)
                    {
                        obj3.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_JUMPER, false);
                    }
                    else if (num10 >= nRate + aRate + jRate && num10 < nRate + aRate + jRate + cRate)
                    {
                        obj3.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_CRAWLER, true);
                    }
                    else if (num10 >= nRate + aRate + jRate + cRate && num10 < nRate + aRate + jRate + cRate + pRate)
                    {
                        obj3.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_PUNK, false);
                    }
                    else
                    {
                        obj3.GetComponent<TITAN>().setAbnormalType(AbnormalType.NORMAL, false);
                    }
                }
                else
                {
                    spawnTitan(abnormal, position, rotation, punk);
                }
            }
        }
        else if (level.StartsWith("Custom"))
        {
            for (num8 = 0; num8 < moreTitans; num8++)
            {
                position = new Vector3(Random.Range(-400f, 400f), 0f, Random.Range(-400f, 400f));
                rotation = new Quaternion(0f, 0f, 0f, 1f);
                if (titanSpawns.Count > 0)
                {
                    position = titanSpawns[Random.Range(0, titanSpawns.Count)];
                }
                else
                {
                    objArray = GameObject.FindGameObjectsWithTag("titanRespawn");
                    if (objArray.Length > 0)
                    {
                        num9 = Random.Range(0, objArray.Length);
                        obj2 = objArray[num9];
                        while (objArray[num9] == null)
                        {
                            num9 = Random.Range(0, objArray.Length);
                            obj2 = objArray[num9];
                        }

                        objArray[num9] = null;
                        position = obj2.transform.position;
                        rotation = obj2.transform.rotation;
                    }
                }

                spawnTitan(abnormal, position, rotation, punk);
            }
        }
        else
        {
            randomSpawnTitan("titanRespawn", abnormal, moreTitans, punk);
        }
    }

    [RPC]
    public void titanGetKill(PhotonPlayer player, int Damage, string name)
    {
        Damage = Mathf.Max(10, Damage);
        object[] parameters = { Damage };
        photonView.RPC("netShowDamage", player, parameters);
        object[] objArray2 = { name, false };
        photonView.RPC("oneTitanDown", PhotonTargets.MasterClient, objArray2);
        sendKillInfo(false, (string)player.customProperties[PhotonPlayerProperty.name], true, name, Damage);
        playerKillInfoUpdate(player, Damage);
        if (player.isLocal)
        {
            UpdateDamageFeedLabel(Damage);
        }
    }

    public void titanGetKillbyServer(int Damage, string name)
    {
        Damage = Mathf.Max(10, Damage);
        sendKillInfo(false, LoginFengKAI.player.name, true, name, Damage);
        netShowDamage(Damage);
        oneTitanDown(name, false);
        playerKillInfoUpdate(PhotonNetwork.player, Damage);
    }

    public void unloadAssets()
    {
        if (!isUnloading)
        {
            isUnloading = true;
            StartCoroutine(unloadAssetsE(10f));
        }
    }

    public IEnumerator unloadAssetsE(float time)
    {
        yield return new WaitForSeconds(time);
        Resources.UnloadUnusedAssets();
        isUnloading = false;
    }

    public void unloadAssetsEditor()
    {
        if (!isUnloading)
        {
            isUnloading = true;
            StartCoroutine(unloadAssetsE(30f));
        }
    }

    [RPC]
    public void verifyPlayerHasLeft(int ID, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient && PhotonPlayer.Find(ID) != null)
        {
            var player = PhotonPlayer.Find(ID);
            var str = string.Empty;
            str = RCextensions.returnStringFromObject(player.customProperties[PhotonPlayerProperty.name]);
            banHash.Add(ID, str);
        }
    }

    public IEnumerator WaitAndRecompilePlayerList(float time)
    {
        int num16;
        string str2;
        int num17;
        int num18;
        int num19;
        int num20;
        object[] objArray2;
        yield return new WaitForSeconds(time);
        var iteratorVariable1 = string.Empty;
        if (RCSettings.teamMode == 0)
        {
            foreach (var player7 in PhotonNetwork.playerList)
            {
                if (player7.customProperties[PhotonPlayerProperty.dead] != null)
                {
                    if (ignoreList.Contains(player7.ID))
                    {
                        iteratorVariable1 = iteratorVariable1 + "[FF0000][X] ";
                    }

                    if (player7.isLocal)
                    {
                        iteratorVariable1 = iteratorVariable1 + "[00CC00]";
                    }
                    else
                    {
                        iteratorVariable1 = iteratorVariable1 + "[FFCC00]";
                    }

                    iteratorVariable1 = iteratorVariable1 + "[" + Convert.ToString(player7.ID) + "] ";
                    if (player7.isMasterClient)
                    {
                        iteratorVariable1 = iteratorVariable1 + "[ffffff][M] ";
                    }

                    if (RCextensions.returnBoolFromObject(player7.customProperties[PhotonPlayerProperty.dead]))
                    {
                        iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_red + "] *dead* ";
                    }

                    if (RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.isTitan]) < 2)
                    {
                        num16 = RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.team]);
                        if (num16 < 2)
                        {
                            iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_human + "] <H> ";
                        }
                        else if (num16 == 2)
                        {
                            iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_human_1 + "] <A> ";
                        }
                    }
                    else if (RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.isTitan]) == 2)
                    {
                        iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_titan_player + "] <T> ";
                    }

                    var iteratorVariable0 = iteratorVariable1;
                    str2 = string.Empty;
                    str2 = RCextensions.returnStringFromObject(player7.customProperties[PhotonPlayerProperty.name]);
                    num17 = 0;
                    num17 = RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.kills]);
                    num18 = 0;
                    num18 = RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.deaths]);
                    num19 = 0;
                    num19 = RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.max_dmg]);
                    num20 = 0;
                    num20 = RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.total_dmg]);
                    objArray2 = new object[] { iteratorVariable0, string.Empty, str2, "[ffffff]:", num17, "/", num18, "/", num19, "/", num20 };
                    iteratorVariable1 = string.Concat(objArray2);
                    if (RCextensions.returnBoolFromObject(player7.customProperties[PhotonPlayerProperty.dead]))
                    {
                        iteratorVariable1 = iteratorVariable1 + "[-]";
                    }

                    iteratorVariable1 = iteratorVariable1 + "\n";
                }
            }
        }
        else
        {
            int num11;
            string str;
            var num2 = 0;
            var num3 = 0;
            var num4 = 0;
            var num5 = 0;
            var num6 = 0;
            var num7 = 0;
            var num8 = 0;
            var num9 = 0;
            var dictionary = new Dictionary<int, PhotonPlayer>();
            var dictionary2 = new Dictionary<int, PhotonPlayer>();
            var dictionary3 = new Dictionary<int, PhotonPlayer>();
            foreach (var player in PhotonNetwork.playerList)
            {
                if (player.customProperties[PhotonPlayerProperty.dead] != null && !ignoreList.Contains(player.ID))
                {
                    num11 = RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.RCteam]);
                    switch (num11)
                    {
                        case 0:
                            dictionary3.Add(player.ID, player);
                            break;

                        case 1:
                            dictionary.Add(player.ID, player);
                            num2 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.kills]);
                            num4 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.deaths]);
                            num6 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.max_dmg]);
                            num8 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.total_dmg]);
                            break;

                        case 2:
                            dictionary2.Add(player.ID, player);
                            num3 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.kills]);
                            num5 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.deaths]);
                            num7 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.max_dmg]);
                            num9 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.total_dmg]);
                            break;
                    }
                }
            }

            cyanKills = num2;
            magentaKills = num3;
            if (PhotonNetwork.isMasterClient)
            {
                if (RCSettings.teamMode != 2)
                {
                    if (RCSettings.teamMode == 3)
                    {
                        foreach (var player3 in PhotonNetwork.playerList)
                        {
                            var num13 = 0;
                            num11 = RCextensions.returnIntFromObject(player3.customProperties[PhotonPlayerProperty.RCteam]);
                            if (num11 > 0)
                            {
                                switch (num11)
                                {
                                    case 1:
                                        {
                                            var num14 = 0;
                                            num14 = RCextensions.returnIntFromObject(player3.customProperties[PhotonPlayerProperty.kills]);
                                            if (num3 + num14 + 7 < num2 - num14)
                                            {
                                                num13 = 2;
                                                num3 += num14;
                                                num2 -= num14;
                                            }

                                            break;
                                        }

                                    case 2:
                                        {
                                            var num15 = 0;
                                            num15 = RCextensions.returnIntFromObject(player3.customProperties[PhotonPlayerProperty.kills]);
                                            if (num2 + num15 + 7 < num3 - num15)
                                            {
                                                num13 = 1;
                                                num2 += num15;
                                                num3 -= num15;
                                            }

                                            break;
                                        }
                                }

                                if (num13 > 0)
                                {
                                    photonView.RPC("setTeamRPC", player3, num13);
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (var player2 in PhotonNetwork.playerList)
                    {
                        var num12 = 0;
                        if (dictionary.Count > dictionary2.Count + 1)
                        {
                            num12 = 2;
                            if (dictionary.ContainsKey(player2.ID))
                            {
                                dictionary.Remove(player2.ID);
                            }

                            if (!dictionary2.ContainsKey(player2.ID))
                            {
                                dictionary2.Add(player2.ID, player2);
                            }
                        }
                        else if (dictionary2.Count > dictionary.Count + 1)
                        {
                            num12 = 1;
                            if (!dictionary.ContainsKey(player2.ID))
                            {
                                dictionary.Add(player2.ID, player2);
                            }

                            if (dictionary2.ContainsKey(player2.ID))
                            {
                                dictionary2.Remove(player2.ID);
                            }
                        }

                        if (num12 > 0)
                        {
                            photonView.RPC("setTeamRPC", player2, num12);
                        }
                    }
                }
            }

            iteratorVariable1 = string.Concat(iteratorVariable1, "[00FFFF]TEAM CYAN", "[ffffff]:", cyanKills, "/", num4, "/", num6, "/", num8, "\n");
            foreach (var player4 in dictionary.Values)
            {
                num11 = RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.RCteam]);
                if (player4.customProperties[PhotonPlayerProperty.dead] != null && num11 == 1)
                {
                    if (ignoreList.Contains(player4.ID))
                    {
                        iteratorVariable1 = iteratorVariable1 + "[FF0000][X] ";
                    }

                    if (player4.isLocal)
                    {
                        iteratorVariable1 = iteratorVariable1 + "[00CC00]";
                    }
                    else
                    {
                        iteratorVariable1 = iteratorVariable1 + "[FFCC00]";
                    }

                    iteratorVariable1 = iteratorVariable1 + "[" + Convert.ToString(player4.ID) + "] ";
                    if (player4.isMasterClient)
                    {
                        iteratorVariable1 = iteratorVariable1 + "[ffffff][M] ";
                    }

                    if (RCextensions.returnBoolFromObject(player4.customProperties[PhotonPlayerProperty.dead]))
                    {
                        iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_red + "] *dead* ";
                    }

                    if (RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.isTitan]) < 2)
                    {
                        num16 = RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.team]);
                        if (num16 < 2)
                        {
                            iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_human + "] <H> ";
                        }
                        else if (num16 == 2)
                        {
                            iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_human_1 + "] <A> ";
                        }
                    }
                    else if (RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.isTitan]) == 2)
                    {
                        iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_titan_player + "] <T> ";
                    }

                    str = iteratorVariable1;
                    str2 = string.Empty;
                    str2 = RCextensions.returnStringFromObject(player4.customProperties[PhotonPlayerProperty.name]);
                    num17 = 0;
                    num17 = RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.kills]);
                    num18 = 0;
                    num18 = RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.deaths]);
                    num19 = 0;
                    num19 = RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.max_dmg]);
                    num20 = 0;
                    num20 = RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.total_dmg]);
                    iteratorVariable1 = string.Concat(str, string.Empty, str2, "[ffffff]:", num17, "/", num18, "/", num19, "/", num20);
                    if (RCextensions.returnBoolFromObject(player4.customProperties[PhotonPlayerProperty.dead]))
                    {
                        iteratorVariable1 = iteratorVariable1 + "[-]";
                    }

                    iteratorVariable1 = iteratorVariable1 + "\n";
                }
            }

            iteratorVariable1 = string.Concat(iteratorVariable1, " \n", "[FF00FF]TEAM MAGENTA", "[ffffff]:", magentaKills, "/", num5, "/", num7, "/", num9, "\n");
            foreach (var player5 in dictionary2.Values)
            {
                num11 = RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.RCteam]);
                if (player5.customProperties[PhotonPlayerProperty.dead] != null && num11 == 2)
                {
                    if (ignoreList.Contains(player5.ID))
                    {
                        iteratorVariable1 = iteratorVariable1 + "[FF0000][X] ";
                    }

                    if (player5.isLocal)
                    {
                        iteratorVariable1 = iteratorVariable1 + "[00CC00]";
                    }
                    else
                    {
                        iteratorVariable1 = iteratorVariable1 + "[FFCC00]";
                    }

                    iteratorVariable1 = iteratorVariable1 + "[" + Convert.ToString(player5.ID) + "] ";
                    if (player5.isMasterClient)
                    {
                        iteratorVariable1 = iteratorVariable1 + "[ffffff][M] ";
                    }

                    if (RCextensions.returnBoolFromObject(player5.customProperties[PhotonPlayerProperty.dead]))
                    {
                        iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_red + "] *dead* ";
                    }

                    if (RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.isTitan]) < 2)
                    {
                        num16 = RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.team]);
                        if (num16 < 2)
                        {
                            iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_human + "] <H> ";
                        }
                        else if (num16 == 2)
                        {
                            iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_human_1 + "] <A> ";
                        }
                    }
                    else if (RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.isTitan]) == 2)
                    {
                        iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_titan_player + "] <T> ";
                    }

                    str = iteratorVariable1;
                    str2 = string.Empty;
                    str2 = RCextensions.returnStringFromObject(player5.customProperties[PhotonPlayerProperty.name]);
                    num17 = 0;
                    num17 = RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.kills]);
                    num18 = 0;
                    num18 = RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.deaths]);
                    num19 = 0;
                    num19 = RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.max_dmg]);
                    num20 = 0;
                    num20 = RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.total_dmg]);
                    iteratorVariable1 = string.Concat(str, string.Empty, str2, "[ffffff]:", num17, "/", num18, "/", num19, "/", num20);
                    if (RCextensions.returnBoolFromObject(player5.customProperties[PhotonPlayerProperty.dead]))
                    {
                        iteratorVariable1 = iteratorVariable1 + "[-]";
                    }

                    iteratorVariable1 = iteratorVariable1 + "\n";
                }
            }

            iteratorVariable1 = string.Concat(new object[] { iteratorVariable1, " \n", "[00FF00]INDIVIDUAL\n" });
            foreach (var player6 in dictionary3.Values)
            {
                num11 = RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.RCteam]);
                if (player6.customProperties[PhotonPlayerProperty.dead] != null && num11 == 0)
                {
                    if (ignoreList.Contains(player6.ID))
                    {
                        iteratorVariable1 = iteratorVariable1 + "[FF0000][X] ";
                    }

                    if (player6.isLocal)
                    {
                        iteratorVariable1 = iteratorVariable1 + "[00CC00]";
                    }
                    else
                    {
                        iteratorVariable1 = iteratorVariable1 + "[FFCC00]";
                    }

                    iteratorVariable1 = iteratorVariable1 + "[" + Convert.ToString(player6.ID) + "] ";
                    if (player6.isMasterClient)
                    {
                        iteratorVariable1 = iteratorVariable1 + "[ffffff][M] ";
                    }

                    if (RCextensions.returnBoolFromObject(player6.customProperties[PhotonPlayerProperty.dead]))
                    {
                        iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_red + "] *dead* ";
                    }

                    if (RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.isTitan]) < 2)
                    {
                        num16 = RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.team]);
                        if (num16 < 2)
                        {
                            iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_human + "] <H> ";
                        }
                        else if (num16 == 2)
                        {
                            iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_human_1 + "] <A> ";
                        }
                    }
                    else if (RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.isTitan]) == 2)
                    {
                        iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_titan_player + "] <T> ";
                    }

                    str = iteratorVariable1;
                    str2 = string.Empty;
                    str2 = RCextensions.returnStringFromObject(player6.customProperties[PhotonPlayerProperty.name]);
                    num17 = 0;
                    num17 = RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.kills]);
                    num18 = 0;
                    num18 = RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.deaths]);
                    num19 = 0;
                    num19 = RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.max_dmg]);
                    num20 = 0;
                    num20 = RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.total_dmg]);
                    iteratorVariable1 = string.Concat(str, string.Empty, str2, "[ffffff]:", num17, "/", num18, "/", num19, "/", num20);
                    if (RCextensions.returnBoolFromObject(player6.customProperties[PhotonPlayerProperty.dead]))
                    {
                        iteratorVariable1 = iteratorVariable1 + "[-]";
                    }

                    iteratorVariable1 = iteratorVariable1 + "\n";
                }
            }
        }

        playerList = iteratorVariable1;
        if (PhotonNetwork.isMasterClient && !isWinning && !isLosing && roundTime >= 5f)
        {
            int num22;
            if (RCSettings.infectionMode > 0)
            {
                var num21 = 0;
                for (num22 = 0; num22 < PhotonNetwork.playerList.Length; num22++)
                {
                    var targetPlayer = PhotonNetwork.playerList[num22];
                    if (!ignoreList.Contains(targetPlayer.ID) && targetPlayer.customProperties[PhotonPlayerProperty.dead] != null && targetPlayer.customProperties[PhotonPlayerProperty.isTitan] != null)
                    {
                        if (RCextensions.returnIntFromObject(targetPlayer.customProperties[PhotonPlayerProperty.isTitan]) == 1)
                        {
                            if (RCextensions.returnBoolFromObject(targetPlayer.customProperties[PhotonPlayerProperty.dead]) && RCextensions.returnIntFromObject(targetPlayer.customProperties[PhotonPlayerProperty.deaths]) > 0)
                            {
                                if (!imatitan.ContainsKey(targetPlayer.ID))
                                {
                                    imatitan.Add(targetPlayer.ID, 2);
                                }

                                var propertiesToSet = new Hashtable();
                                propertiesToSet.Add(PhotonPlayerProperty.isTitan, 2);
                                targetPlayer.SetCustomProperties(propertiesToSet);
                                photonView.RPC("spawnTitanRPC", targetPlayer);
                            }
                            else if (imatitan.ContainsKey(targetPlayer.ID))
                            {
                                for (var j = 0; j < heroes.Count; j++)
                                {
                                    var hero = (HERO)heroes[j];
                                    if (hero.photonView.owner == targetPlayer)
                                    {
                                        hero.markDie();
                                        hero.photonView.RPC("netDie2", PhotonTargets.All, -1, "noswitchingfagt");
                                    }
                                }
                            }
                        }
                        else if (!(RCextensions.returnIntFromObject(targetPlayer.customProperties[PhotonPlayerProperty.isTitan]) != 2 || RCextensions.returnBoolFromObject(targetPlayer.customProperties[PhotonPlayerProperty.dead])))
                        {
                            num21++;
                        }
                    }
                }

                if (num21 <= 0 && IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.KILL_TITAN)
                {
                    gameWin();
                }
            }
            else if (RCSettings.pointMode > 0)
            {
                if (RCSettings.teamMode > 0)
                {
                    if (cyanKills >= RCSettings.pointMode)
                    {
                        InRoomChat.SystemMessageGlobal("<color=#00FFFF>Team Cyan wins!</color>");
                        gameWin();
                    }
                    else if (magentaKills >= RCSettings.pointMode)
                    {
                        InRoomChat.SystemMessageGlobal("<color=#00FFFF>Team Magenta wins!</color>");
                        gameWin();
                    }
                }
                else if (RCSettings.teamMode == 0)
                {
                    for (num22 = 0; num22 < PhotonNetwork.playerList.Length; num22++)
                    {
                        var player9 = PhotonNetwork.playerList[num22];
                        if (RCextensions.returnIntFromObject(player9.customProperties[PhotonPlayerProperty.kills]) >= RCSettings.pointMode)
                        {
                            InRoomChat.SystemMessageGlobal(player9, " wins!");
                            gameWin();
                        }
                    }
                }
            }
            else if (RCSettings.pointMode <= 0 && (RCSettings.bombMode == 1 || RCSettings.pvpMode > 0))
            {
                if (RCSettings.teamMode > 0 && PhotonNetwork.playerList.Length > 1)
                {
                    var num24 = 0;
                    var num25 = 0;
                    var num26 = 0;
                    var num27 = 0;
                    for (num22 = 0; num22 < PhotonNetwork.playerList.Length; num22++)
                    {
                        var player10 = PhotonNetwork.playerList[num22];
                        if (!ignoreList.Contains(player10.ID) && player10.customProperties[PhotonPlayerProperty.RCteam] != null && player10.customProperties[PhotonPlayerProperty.dead] != null)
                        {
                            if (RCextensions.returnIntFromObject(player10.customProperties[PhotonPlayerProperty.RCteam]) == 1)
                            {
                                num26++;
                                if (!RCextensions.returnBoolFromObject(player10.customProperties[PhotonPlayerProperty.dead]))
                                {
                                    num24++;
                                }
                            }
                            else if (RCextensions.returnIntFromObject(player10.customProperties[PhotonPlayerProperty.RCteam]) == 2)
                            {
                                num27++;
                                if (!RCextensions.returnBoolFromObject(player10.customProperties[PhotonPlayerProperty.dead]))
                                {
                                    num25++;
                                }
                            }
                        }
                    }

                    if (num26 > 0 && num27 > 0)
                    {
                        if (num24 == 0)
                        {
                            InRoomChat.SystemMessageGlobal("<color=#00FFFF>Team Magenta wins!</color>");
                            gameWin();
                        }
                        else if (num25 == 0)
                        {
                            InRoomChat.SystemMessageGlobal("<color=#00FFFF>Team Magenta wins!</color>");
                            gameWin();
                        }
                    }
                }
                else if (RCSettings.teamMode == 0 && PhotonNetwork.playerList.Length > 1)
                {
                    var num28 = 0;
                    var text = "Nobody";
                    var player11 = PhotonNetwork.playerList[0];
                    for (num22 = 0; num22 < PhotonNetwork.playerList.Length; num22++)
                    {
                        var player12 = PhotonNetwork.playerList[num22];
                        if (!(player12.customProperties[PhotonPlayerProperty.dead] == null || RCextensions.returnBoolFromObject(player12.customProperties[PhotonPlayerProperty.dead])))
                        {
                            text = RCextensions.returnStringFromObject(player12.customProperties[PhotonPlayerProperty.name]).hexColor();
                            player11 = player12;
                            num28++;
                        }
                    }

                    if (num28 <= 1)
                    {
                        var str4 = "5 points added.";
                        if (text == "Nobody")
                        {
                            str4 = string.Empty;
                        }
                        else
                        {
                            for (num22 = 0; num22 < 5; num22++)
                            {
                                playerKillInfoUpdate(player11, 0);
                            }
                        }

                        InRoomChat.SystemMessageGlobal($"{text} wins. {str4}");
                        gameWin();
                    }
                }
            }
        }

        isRecompiling = false;
    }

    public IEnumerator WaitAndReloadKDR(PhotonPlayer player)
    {
        yield return new WaitForSeconds(5f);
        var key = RCextensions.returnStringFromObject(player.customProperties[PhotonPlayerProperty.name]);
        if (PreservedPlayerKDR.ContainsKey(key))
        {
            var numArray = PreservedPlayerKDR[key];
            PreservedPlayerKDR.Remove(key);
            var propertiesToSet = new Hashtable();
            propertiesToSet.Add(PhotonPlayerProperty.kills, numArray[0]);
            propertiesToSet.Add(PhotonPlayerProperty.deaths, numArray[1]);
            propertiesToSet.Add(PhotonPlayerProperty.max_dmg, numArray[2]);
            propertiesToSet.Add(PhotonPlayerProperty.total_dmg, numArray[3]);
            player.SetCustomProperties(propertiesToSet);
        }
    }

    public IEnumerator WaitAndResetRestarts()
    {
        yield return new WaitForSeconds(10f);
        restartingBomb = false;
        restartingEren = false;
        restartingHorse = false;
        restartingMC = false;
        restartingTitan = false;
    }

    public IEnumerator WaitAndRespawn1(float time, string str)
    {
        yield return new WaitForSeconds(time);
        SpawnPlayer(myLastHero, str);
    }

    public IEnumerator WaitAndRespawn2(float time, GameObject pos)
    {
        yield return new WaitForSeconds(time);
        SpawnPlayerAt(myLastHero, pos);
    }

    private void cache()
    {
        ClothFactory.ClearClothCache();
        inputManager = GameObjectCache.Find("InputManagerController").GetComponent<FengCustomInputs>();
        playersRPC.Clear();
        titanSpawners.Clear();
        groundList.Clear();
        PreservedPlayerKDR = new Dictionary<string, int[]>();
        noRestart = false;
        skyMaterial = null;
        isSpawning = false;
        retryTime = 0f;
        logicLoaded = false;
        customLevelLoaded = true;
        isUnloading = false;
        isRecompiling = false;
        Time.timeScale = 1f;
        Camera.main.farClipPlane = 1500f;
        pauseWaitTime = 0f;
        spectateSprites = new List<GameObject>();
        isRestarting = false;
        if (PhotonNetwork.isMasterClient)
        {
            StartCoroutine(WaitAndResetRestarts());
        }

        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
        {
            roundTime = 0f;
            if (level.StartsWith("Custom"))
            {
                customLevelLoaded = false;
            }

            if (PhotonNetwork.isMasterClient)
            {
                if (isFirstLoad)
                {
                    setGameSettings(checkGameGUI());
                }

                if (RCSettings.endlessMode > 0)
                {
                    StartCoroutine(respawnE(RCSettings.endlessMode));
                }
            }

            if (Settings.ChatFeedSetting)
            {
                if (Settings.LegacyChatSetting)
                {
                    if (!Settings.ChatFeedSeparateSetting)
                        InRoomChat.AddLineRC("<color=#FFC000>(" + this.roundTime.ToString("F2") + ")</color> Round Start.");
                    else
                        InRoomChat.AddLineChatFeed("<color=#FFC000>(" + this.roundTime.ToString("F2") + ")</color> Round Start.");
                }
                else
                {
                    string[] msg = { $"({roundTime.ToString("F2")}) ", "Round Start." };
                    if (!Settings.ChatFeedSeparateSetting)
                        InRoomChat.SystemMessageLocal(msg, false);
                    else
                        InRoomChat.SystemMessageLocal(msg, false, true);
                }
            }
        }

        isFirstLoad = false;
        RecompilePlayerList(0.5f);
    }

    [RPC]
    private void Chat(string content, string sender, PhotonMessageInfo info)
    {
        Logger.LogChat(Logger.ChatLogPath, content, info);

        if (sender != string.Empty)
        {
            content = sender + ": " + content.StripHTML();
        }

        if (Settings.LegacyChatSetting)
        {
            content = "<color=#FFC000>[" + Convert.ToString(info.sender.ID) + "]</color> " + content;
            InRoomChat.AddLineChat(content);
        }
        else
        {
            content = InRoomChat.ChatFormatting($"[{Convert.ToString(info.sender.ID)}] ", Settings.ChatMinorColorSetting, Settings.ChatMinorFormatSettings[0], Settings.ChatMinorFormatSettings[1]) + content;
            InRoomChat.AddLineChat($"<size={Settings.ChatSizeSetting}>{content}</size>");
        }
    }

    [RPC]
    private void ChatPM(string sender, string content, PhotonMessageInfo info)
    {
        var request = content.ToUpper();

        Logger.LogChat(Logger.ChatLogPath, content, info);

        content = InRoomChat.ChatFormatting("Message from ", Settings.ChatMajorColorSetting, Settings.ChatMajorFormatSettings[0], Settings.ChatMajorFormatSettings[1]) + InRoomChat.ChatFormatting($"[{Convert.ToString(info.sender.ID)}]", Settings.ChatMinorColorSetting, Settings.ChatMinorFormatSettings[0], Settings.ChatMinorFormatSettings[1]) + info.sender.Name.hexColor() + ": " + content;
        if (Settings.LegacyChatSetting)
        {
            content = "<color=#FFC000>FROM [" + Convert.ToString(info.sender.ID) + "]</color> " + content;
            InRoomChat.AddLineChat(content);
        }
        else
        {
            InRoomChat.AddLineChat($"<size={Settings.ChatSizeSetting}>{content}</size>");
        }

        CheckRequests(info, request);
    }

    private void CheckRequests(PhotonMessageInfo info, string request)
    {
        if (info.sender.WaitForMapScript && (request.Contains("Y") || request.Contains("N")))
        {
            info.sender.WaitForMapScript = false;

            if (!request.StripHTML().ToUpper().Contains("Y"))
            {
                InRoomChat.SystemMessageLocal(info.sender, "denied your request.");
                return;
            }

            InRoomChat.SystemMessageLocal(info.sender, "accepted your request.");
            Settings.CustomMapScriptsList[Settings.CustomMapSkinsCurrentSetSetting] = string.Empty;

            foreach (var line in levelCache)
            {
                foreach (var obj in line)
                {
                    Settings.CustomMapScriptsList[Settings.CustomMapSkinsCurrentSetSetting] += obj;
                    Settings.CustomMapScriptsList[Settings.CustomMapSkinsCurrentSetSetting] += ";\n";
                }
            }
        }

        if (info.sender.WaitForLocationSkin && (request.Contains("Y") || request.Contains("N")))
        {
            info.sender.WaitForLocationSkin = false;

            if (!request.StripHTML().ToUpper().Contains("Y"))
            {
                InRoomChat.SystemMessageLocal(info.sender, "denied your request.");
                return;
            }

            InRoomChat.SystemMessageLocal(info.sender, "accepted your request.");

            if (string.IsNullOrEmpty(LocationSkinToSteal) || IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.MULTIPLAYER || PhotonNetwork.isMasterClient) return;
            if (Application.loadedLevelName.Contains("Forest"))
            {
                Settings.LocationSkinsForestTitlesList.Add(PhotonNetwork.masterClient.Name.StripHEX());
                Settings.LocationSkinsForestList.Add(LocationSkinToSteal.Split(','));
                Settings.LocationSkinsForestCurrentSetSetting.Value = Settings.LocationSkinsForestTitlesList.Count - 1;
                Settings.LocationSkinsForestAmbientList.Add(0);
                Settings.LocationSkinsForestAmbientSettingsList.Add(new float[] { Settings.CustomAmbientColorSetting[0][0], Settings.CustomAmbientColorSetting[0][1], Settings.CustomAmbientColorSetting[0][2] });
                Settings.LocationSkinsForestFogList.Add(0);
                Settings.LocationSkinsForestFogSettingsList.Add(new float[] { 0.066f, 0.066f, 0.066f, 0f, 1000f });
                Settings.LocationSkinsForestLightList.Add(0);
                Settings.LocationSkinsForestLightSettingsList.Add(new[] { 1f, 1f, 1f });
                Settings.LocationSkinsForestParticlesList.Add(0);
                Settings.LocationSkinsForestParticlesSettingsList.Add(new float[] { 1500f, 125f, 60f, 120f, 0.001f, 0f, 1f, 1f, 1f, 1f });
                Settings.LocationSkinsForestCountSetting.Value++;
            }
            else if (Application.loadedLevelName.Contains("City"))
            {
                Settings.LocationSkinsCityTitlesList.Add(PhotonNetwork.masterClient.Name.StripHEX());
                Settings.LocationSkinsCityList.Add(LocationSkinToSteal.Split('`'));
                Settings.LocationSkinsCityCurrentSetSetting.Value = Settings.LocationSkinsCityTitlesList.Count - 1;
                Settings.LocationSkinsCityAmbientList.Add(0);
                Settings.LocationSkinsCityAmbientSettingsList.Add(new float[] { Settings.CustomAmbientColorSetting[0][0], Settings.CustomAmbientColorSetting[0][1], Settings.CustomAmbientColorSetting[0][2] });
                Settings.LocationSkinsCityFogList.Add(0);
                Settings.LocationSkinsCityFogSettingsList.Add(new float[] { 0.066f, 0.066f, 0.066f, 0f, 1000f });
                Settings.LocationSkinsCityLightList.Add(0);
                Settings.LocationSkinsCityLightSettingsList.Add(new[] { 1f, 1f, 1f });
                Settings.LocationSkinsCityParticlesList.Add(0);
                Settings.LocationSkinsCityParticlesSettingsList.Add(new float[] { 1500f, 125f, 60f, 120f, 0.001f, 0f, 1f, 1f, 1f, 1f });
                Settings.LocationSkinsCityCountSetting.Value++;
            }
        }

        if (info.sender.WaitForHumanSkin && (request.Contains("Y") || request.Contains("N")))
        {
            info.sender.WaitForHumanSkin = false;

            if (!request.StripHTML().ToUpper().Contains("Y"))
            {
                InRoomChat.SystemMessageLocal(info.sender, "denied your request.");
                return;
            }

            InRoomChat.SystemMessageLocal(info.sender, "accepted your request.");

            var skin = HERO.PlayersSkins[info.sender.ID];
            Settings.HumanSkinsTitlesList.Add(info.sender.Name.StripHEX());
            Settings.HumanSkinsCurrentSetSetting.Value = Settings.HumanSkinsTitlesList.Count - 1;
            Settings.HumanSkinsList.Add(skin.Split(','));
            Settings.HumanSkinsCountSetting.Value++;
        }
    }

    private Hashtable checkGameGUI()
    {
        int num;
        int num2;
        PhotonPlayer player;
        int num4;
        float num8;
        float num9;
        var hashtable = new Hashtable();
        if (Settings.InfectionModeSetting)
        {
            Settings.TeamModeSetting.Value = false;
            Settings.PointsModeSetting.Value = false;
            Settings.PVPModeSetting.Value = false;
            num = 1;
            if (Settings.InfectedTitansSetting > PhotonNetwork.countOfPlayers || Settings.InfectedTitansSetting < 0)
            {
                Settings.InfectedTitansSetting.Value = 1;
            }

            hashtable.Add("infection", Settings.InfectedTitansSetting.Value);

            if (RCSettings.infectionMode != Settings.InfectedTitansSetting)
            {
                imatitan.Clear();
                for (num2 = 0; num2 < PhotonNetwork.playerList.Length; num2++)
                {
                    player = PhotonNetwork.playerList[num2];
                    var propertiesToSet = new Hashtable();
                    propertiesToSet.Add(PhotonPlayerProperty.isTitan, 1);
                    player.SetCustomProperties(propertiesToSet);
                }

                var length = PhotonNetwork.playerList.Length;
                num4 = num;
                for (num2 = 0; num2 < PhotonNetwork.playerList.Length; num2++)
                {
                    var player2 = PhotonNetwork.playerList[num2];
                    if (length > 0 && Random.Range(0f, 1f) <= num4 / (float)length)
                    {
                        var hashtable3 = new Hashtable();
                        hashtable3.Add(PhotonPlayerProperty.isTitan, 2);
                        player2.SetCustomProperties(hashtable3);
                        imatitan.Add(player2.ID, 2);
                        num4--;
                    }

                    length--;
                }
            }
        }

        if (Settings.BombsModeSetting)
        {
            hashtable.Add("bomb", 1);
        }

        if (Settings.DisableMinimapsSetting)
        {
            hashtable.Add("globalDisableMinimap", 1);
        }

        if (Settings.TeamModeSetting)
        {
            hashtable.Add("team", Settings.TeamSortSetting.Value + 1);
            if (RCSettings.teamMode != Settings.TeamSortSetting + 1)
            {
                num4 = 1;
                for (num2 = 0; num2 < PhotonNetwork.playerList.Length; num2++)
                {
                    player = PhotonNetwork.playerList[num2];
                    switch (num4)
                    {
                        case 1:
                            photonView.RPC("setTeamRPC", player, 1);
                            num4 = 2;
                            break;

                        case 2:
                            photonView.RPC("setTeamRPC", player, 2);
                            num4 = 1;
                            break;
                    }
                }
            }
        }

        if (Settings.PointsModeSetting)
        {
            if (Settings.PointsLimitSetting < 0)
            {
                Settings.PointsLimitSetting.Value = 50;
            }

            hashtable.Add("point", Settings.PointsLimitSetting.Value);
        }

        if (Settings.DisableRockThrowingSetting)
        {
            hashtable.Add("rock", 1);
        }

        if (Settings.ExplodeModeSetting)
        {
            if (Settings.ExplodeRadiusSetting < 0)
            {
                Settings.ExplodeRadiusSetting.Value = 30;
            }

            hashtable.Add("explode", Settings.ExplodeRadiusSetting.Value);
        }

        if (Settings.HealthModeSetting)
        {
            if (Settings.HealthSettings[1] < 0)
            {
                settings[198] = "100";
            }

            if (Settings.HealthSettings[2] < 0)
            {
                settings[199] = "200";
            }

            hashtable.Add("healthMode", Settings.HealthSettings[0].Value + 1);
            hashtable.Add("healthLower", Settings.HealthSettings[1].Value);
            hashtable.Add("healthUpper", Settings.HealthSettings[2].Value);
        }

        if (Settings.AntiTitanErenSetting)
        {
            hashtable.Add("eren", 1);
        }

        if (Settings.CustomStarterTitansSetting)
        {
            if (Settings.StarterAmountSetting < 0)
            {
                Settings.StarterAmountSetting.Value = 1;
            }

            hashtable.Add("titanc", Settings.StarterAmountSetting.Value);
        }

        if (Settings.ArmorModeSetting)
        {
            if (Settings.ArmorSetting < 0)
            {
                Settings.ArmorSetting.Value = 1000;
            }

            hashtable.Add("damage", Settings.ArmorSetting.Value);
        }

        if (Settings.CustomSizeSetting)
        {
            if (Settings.SizeSettings[0] < 0f)
            {
                Settings.SizeSettings[0].Value = 1f;
            }

            if (Settings.SizeSettings[1] < 0f)
            {
                Settings.SizeSettings[1].Value = 3f;
            }

            hashtable.Add("sizeMode", 1);
            hashtable.Add("sizeLower", Settings.SizeSettings[0].Value);
            hashtable.Add("sizeUpper", Settings.SizeSettings[1].Value);
        }

        if (Settings.CustomSpawnRateSetting)
        {
            hashtable.Add("spawnMode", 1);
            hashtable.Add("nRate", Settings.SpawnRateSettings[0].Value);
            hashtable.Add("aRate", Settings.SpawnRateSettings[1].Value);
            hashtable.Add("jRate", Settings.SpawnRateSettings[2].Value);
            hashtable.Add("cRate", Settings.SpawnRateSettings[3].Value);
            hashtable.Add("pRate", Settings.SpawnRateSettings[4].Value);
        }

        if (Settings.HorsesSetting)
        {
            hashtable.Add("horse", 1);
        }

        if (Settings.CustomTitansPerWaveSetting)
        {
            if (Settings.TitansPerWaveSetting > 50)
            {
                Settings.TitansPerWaveSetting.Value = 1;
            }

            hashtable.Add("waveModeOn", 1);
            hashtable.Add("waveModeNum", Settings.TitansPerWaveSetting.Value);
        }

        if (Settings.FriendlyModeSetting)
        {
            hashtable.Add("friendly", 1);
        }

        if (Settings.PVPModeSetting)
        {
            hashtable.Add("pvp", Settings.PVPTypeSetting.Value + 1);
        }

        if (Settings.CustomWavesSetting)
        {
            if (Settings.MaximumWavesSetting < 0)
            {
                Settings.MaximumWavesSetting.Value = 20;
            }

            hashtable.Add("maxwave", Settings.MaximumWavesSetting.Value);
        }

        if (Settings.AutoReviveSetting)
        {
            if (Settings.AutoReviveTimeSetting < 0)
            {
                Settings.AutoReviveTimeSetting.Value = 5;
            }

            hashtable.Add("endless", Settings.AutoReviveTimeSetting.Value);
        }

        if (Settings.WelcomeMessageSetting != string.Empty)
        {
            hashtable.Add("motd", Settings.WelcomeMessageSetting.Value);
        }

        if (Settings.DisableAHSSAirReloadingSetting)
        {
            hashtable.Add("ahssReload", 1);
        }

        if (Settings.PunkWavesSetting)
        {
            hashtable.Add("punkWaves", 1);
        }

        if (Settings.DeadlyCannonsModeSetting)
        {
            hashtable.Add("deadlycannons", 1);
        }

        if (RCSettings.racingStatic > 0)
        {
            hashtable.Add("asoracing", 1);
        }

        return hashtable;
    }

    private bool checkIsTitanAllDie()
    {
        foreach (var obj2 in GameObject.FindGameObjectsWithTag("titan"))
        {
            if (obj2.GetComponent<TITAN>() != null && !obj2.GetComponent<TITAN>().hasDie)
            {
                return false;
            }

            if (obj2.GetComponent<FEMALE_TITAN>() != null)
            {
                return false;
            }
        }

        return true;
    }

    [RPC]
    private void clearlevel(string[] link, int gametype, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            if (gametype == 0)
            {
                IN_GAME_MAIN_CAMERA.gamemode = GAMEMODE.KILL_TITAN;
            }
            else if (gametype == 1)
            {
                IN_GAME_MAIN_CAMERA.gamemode = GAMEMODE.SURVIVE_MODE;
            }
            else if (gametype == 2)
            {
                IN_GAME_MAIN_CAMERA.gamemode = GAMEMODE.PVP_AHSS;
            }
            else if (gametype == 3)
            {
                IN_GAME_MAIN_CAMERA.gamemode = GAMEMODE.RACING;
            }
            else if (gametype == 4)
            {
                IN_GAME_MAIN_CAMERA.gamemode = GAMEMODE.None;
            }

            if (info.sender.isMasterClient && link.Length > 6 && Settings.CustomMapSkinsSetting)
            {
                StartCoroutine(clearlevelE(link));
            }
        }
    }

    private IEnumerator clearlevelE(string[] skybox)
    {
        var key = skybox[6];
        var mipmap = Settings.MipMappingSetting;
        var needUnloadAssests = false;

        if (skybox[0] != string.Empty || skybox[1] != string.Empty || skybox[2] != string.Empty || skybox[3] != string.Empty || skybox[4] != string.Empty || skybox[5] != string.Empty)
        {
            var iteratorVariable3 = string.Join(",", skybox);
            if (!linkHash[1].ContainsKey(iteratorVariable3))
            {
                needUnloadAssests = true;
                var material = Camera.main.GetComponent<Skybox>().material;
                var skyboxFront = skybox[0];
                var skyboxBack = skybox[1];
                var skyboxLeft = skybox[2];
                var skyboxRight = skybox[3];
                var skyboxUp = skybox[4];
                var skyboxDown = skybox[5];

                if (skyboxFront.EndsWith(".jpg") || skyboxFront.EndsWith(".png") || skyboxFront.EndsWith(".jpeg"))
                {
                    var link = new WWW(skyboxFront);
                    yield return link;
                    var texture = RCextensions.loadimage(link, mipmap, 500000);
                    link.Dispose();
                    material.SetTexture("_FrontTex", texture);
                }

                if (skyboxBack.EndsWith(".jpg") || skyboxBack.EndsWith(".png") || skyboxBack.EndsWith(".jpeg"))
                {
                    var iteratorVariable13 = new WWW(skyboxBack);
                    yield return iteratorVariable13;
                    var iteratorVariable14 = RCextensions.loadimage(iteratorVariable13, mipmap, 500000);
                    iteratorVariable13.Dispose();
                    material.SetTexture("_BackTex", iteratorVariable14);
                }

                if (skyboxLeft.EndsWith(".jpg") || skyboxLeft.EndsWith(".png") || skyboxLeft.EndsWith(".jpeg"))
                {
                    var iteratorVariable15 = new WWW(skyboxLeft);
                    yield return iteratorVariable15;
                    var iteratorVariable16 = RCextensions.loadimage(iteratorVariable15, mipmap, 500000);
                    iteratorVariable15.Dispose();
                    material.SetTexture("_LeftTex", iteratorVariable16);
                }

                if (skyboxRight.EndsWith(".jpg") || skyboxRight.EndsWith(".png") || skyboxRight.EndsWith(".jpeg"))
                {
                    var iteratorVariable17 = new WWW(skyboxRight);
                    yield return iteratorVariable17;
                    var iteratorVariable18 = RCextensions.loadimage(iteratorVariable17, mipmap, 500000);
                    iteratorVariable17.Dispose();
                    material.SetTexture("_RightTex", iteratorVariable18);
                }

                if (skyboxUp.EndsWith(".jpg") || skyboxUp.EndsWith(".png") || skyboxUp.EndsWith(".jpeg"))
                {
                    var iteratorVariable19 = new WWW(skyboxUp);
                    yield return iteratorVariable19;
                    var iteratorVariable20 = RCextensions.loadimage(iteratorVariable19, mipmap, 500000);
                    iteratorVariable19.Dispose();
                    material.SetTexture("_UpTex", iteratorVariable20);
                }

                if (skyboxDown.EndsWith(".jpg") || skyboxDown.EndsWith(".png") || skyboxDown.EndsWith(".jpeg"))
                {
                    var iteratorVariable21 = new WWW(skyboxDown);
                    yield return iteratorVariable21;
                    var iteratorVariable22 = RCextensions.loadimage(iteratorVariable21, mipmap, 500000);
                    iteratorVariable21.Dispose();
                    material.SetTexture("_DownTex", iteratorVariable22);
                }

                Camera.main.GetComponent<Skybox>().material = material;
                linkHash[1].Add(iteratorVariable3, material);
                skyMaterial = material;
            }
            else
            {
                Camera.main.GetComponent<Skybox>().material = (Material)linkHash[1][iteratorVariable3];
                skyMaterial = (Material)linkHash[1][iteratorVariable3];
            }
        }

        if (key.EndsWith(".jpg") || key.EndsWith(".png") || key.EndsWith(".jpeg"))
        {
            foreach (var iteratorVariable23 in groundList)
            {
                if (iteratorVariable23 != null && iteratorVariable23.renderer != null)
                {
                    foreach (var iteratorVariable24 in iteratorVariable23.GetComponentsInChildren<Renderer>())
                    {
                        if (!linkHash[0].ContainsKey(key))
                        {
                            var iteratorVariable25 = new WWW(key);
                            yield return iteratorVariable25;
                            var iteratorVariable26 = RCextensions.loadimage(iteratorVariable25, mipmap, 200000);
                            iteratorVariable25.Dispose();
                            if (!linkHash[0].ContainsKey(key))
                            {
                                needUnloadAssests = true;
                                iteratorVariable24.material.mainTexture = iteratorVariable26;
                                linkHash[0].Add(key, iteratorVariable24.material);
                                iteratorVariable24.material = (Material)linkHash[0][key];
                            }
                            else
                            {
                                iteratorVariable24.material = (Material)linkHash[0][key];
                            }
                        }
                        else
                        {
                            iteratorVariable24.material = (Material)linkHash[0][key];
                        }
                    }
                }
            }
        }
        else if (key.ToLower() == "transparent")
        {
            foreach (var obj2 in groundList)
            {
                if (obj2 != null && obj2.renderer != null)
                {
                    foreach (var renderer in obj2.GetComponentsInChildren<Renderer>())
                    {
                        renderer.enabled = false;
                    }
                }
            }
        }

        if (needUnloadAssests)
        {
            unloadAssets();
        }
    }

    private void core()
    {
        if ((int)settings[64] >= 100)
        {
            coreeditor();
        }
        else
        {
            if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE && needChooseSide)
            {
                if (inputManager.isInputDown[InputCode.flare1])
                {
                    if (NGUITools.GetActive(ui.GetComponent<UIReferArray>().panels[3]))
                    {
                        Screen.lockCursor = true;
                        Screen.showCursor = true;
                        NGUITools.SetActive(ui.GetComponent<UIReferArray>().panels[0], true);
                        NGUITools.SetActive(ui.GetComponent<UIReferArray>().panels[1], false);
                        NGUITools.SetActive(ui.GetComponent<UIReferArray>().panels[2], false);
                        NGUITools.SetActive(ui.GetComponent<UIReferArray>().panels[3], false);
                        Camera.main.GetComponent<SpectatorMovement>().disable = false;
                        Camera.main.GetComponent<MouseLook>().disable = false;
                    }
                    else
                    {
                        Screen.lockCursor = false;
                        Screen.showCursor = true;
                        NGUITools.SetActive(ui.GetComponent<UIReferArray>().panels[0], false);
                        NGUITools.SetActive(ui.GetComponent<UIReferArray>().panels[1], false);
                        NGUITools.SetActive(ui.GetComponent<UIReferArray>().panels[2], false);
                        NGUITools.SetActive(ui.GetComponent<UIReferArray>().panels[3], true);
                        Camera.main.GetComponent<SpectatorMovement>().disable = true;
                        Camera.main.GetComponent<MouseLook>().disable = true;
                    }
                }

                if (inputManager.isInputDown[15] && !inputManager.menuOn)
                {
                    Screen.showCursor = true;
                    Screen.lockCursor = false;
                    Camera.main.GetComponent<SpectatorMovement>().disable = true;
                    Camera.main.GetComponent<MouseLook>().disable = true;
                    inputManager.menuOn = true;
                    Page.GetInstance<PauseMenu>().Enable();
                }
            }

            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
            {
                int length;
                float num3;
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                {
                    coreadd();
                    ShowHUDInfoTopLeft(playerList);
                    if (Camera.main != null && IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.RACING && Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().gameOver && !needChooseSide && (int)settings[245] == 0)
                    {
                        ShowHUDInfoCenter("Press [F7D358]" + inputManager.inputString[InputCode.flare1] + "[-] to spectate the next player. \nPress [F7D358]" + inputManager.inputString[InputCode.flare2] + "[-] to spectate the previous player.\nPress [F7D358]" + inputManager.inputString[InputCode.attack1] + "[-] to enter the spectator mode.\n\n\n\n");
                        if (LevelInfo.getInfo(level).respawnMode == RespawnMode.DEATHMATCH || RCSettings.endlessMode > 0 || (RCSettings.bombMode == 1 || RCSettings.pvpMode > 0) && RCSettings.pointMode > 0)
                        {
                            myRespawnTime += Time.deltaTime;
                            var endlessMode = 5;
                            if (RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.isTitan]) == 2)
                            {
                                endlessMode = 10;
                            }

                            if (RCSettings.endlessMode > 0)
                            {
                                endlessMode = RCSettings.endlessMode;
                            }

                            length = endlessMode - (int)myRespawnTime;
                            ShowHUDInfoCenterADD("Respawn in " + length + "s.");
                            if (myRespawnTime > endlessMode)
                            {
                                myRespawnTime = 0f;
                                Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().gameOver = false;
                                if (RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.isTitan]) == 2)
                                {
                                    SpawnNonAITitan2(myLastHero);
                                }
                                else
                                {
                                    StartCoroutine(WaitAndRespawn1(0.1f, myLastRespawnTag));
                                }

                                Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().gameOver = false;
                                ShowHUDInfoCenter(string.Empty);
                            }
                        }
                    }
                }
                else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                {
                    if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
                    {
                        if (!isLosing)
                        {
                            currentSpeed = Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().main_object.rigidbody.velocity.magnitude;
                            maxSpeed = Mathf.Max(maxSpeed, currentSpeed);
                            ShowHUDInfoTopLeft(string.Concat("Current Speed : ", (int)currentSpeed, "\nMax Speed:", maxSpeed));
                        }
                    }
                    else
                    {
                        ShowHUDInfoTopLeft(string.Concat("Kills:", single_kills, "\nMax Damage:", single_maxDamage, "\nTotal Damage:", single_totalDamage));
                    }
                }

                if (isLosing && IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.RACING)
                {
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                    {
                        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
                        {
                            ShowHUDInfoCenter(string.Concat("Survive ", wave, " Waves!\n Press ", inputManager.inputString[InputCode.restart], " to Restart.\n\n\n"));
                        }
                        else
                        {
                            ShowHUDInfoCenter("Humanity Fail!\n Press " + inputManager.inputString[InputCode.restart] + " to Restart.\n\n\n");
                        }
                    }
                    else
                    {
                        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
                        {
                            ShowHUDInfoCenter(string.Concat("Survive ", wave, " Waves!\nGame Restart in ", (int)gameEndCD, "s\n\n"));
                        }
                        else
                        {
                            ShowHUDInfoCenter("Humanity Fail!\nAgain!\nGame Restart in " + (int)gameEndCD + "s\n\n");
                        }

                        if (gameEndCD <= 0f)
                        {
                            gameEndCD = 0f;
                            if (PhotonNetwork.isMasterClient)
                            {
                                restartRC();
                            }

                            ShowHUDInfoCenter(string.Empty);
                        }
                        else
                        {
                            gameEndCD -= Time.deltaTime;
                        }
                    }
                }

                if (isWinning)
                {
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                    {
                        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
                        {
                            num3 = (int)(timeTotalServer * 10f) * 0.1f - 5f;
                            ShowHUDInfoCenter(num3 + "s !\n Press " + inputManager.inputString[InputCode.restart] + " to Restart.\n\n\n");
                        }
                        else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
                        {
                            ShowHUDInfoCenter("Survive All Waves!\n Press " + inputManager.inputString[InputCode.restart] + " to Restart.\n\n\n");
                        }
                        else
                        {
                            ShowHUDInfoCenter("Humanity Win!\n Press " + inputManager.inputString[InputCode.restart] + " to Restart.\n\n\n");
                        }
                    }
                    else
                    {
                        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
                        {
                            ShowHUDInfoCenter(string.Concat(localRacingResult, "\n\nGame Restart in ", (int)gameEndCD, "s"));
                        }
                        else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
                        {
                            ShowHUDInfoCenter("Survive All Waves!\nGame Restart in " + (int)gameEndCD + "s\n\n");
                        }
                        else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS)
                        {
                            if (RCSettings.pvpMode == 0 && RCSettings.bombMode == 0)
                            {
                                ShowHUDInfoCenter(string.Concat("Team ", teamWinner, " Win!\nGame Restart in ", (int)gameEndCD, "s\n\n"));
                            }
                            else
                            {
                                ShowHUDInfoCenter(string.Concat(new object[] { "Round Ended!\nGame Restart in ", (int)gameEndCD, "s\n\n" }));
                            }
                        }
                        else
                        {
                            ShowHUDInfoCenter("Humanity Win!\nGame Restart in " + (int)gameEndCD + "s\n\n");
                        }

                        if (gameEndCD <= 0f)
                        {
                            gameEndCD = 0f;
                            if (PhotonNetwork.isMasterClient)
                            {
                                restartRC();
                            }

                            ShowHUDInfoCenter(string.Empty);
                        }
                        else
                        {
                            gameEndCD -= Time.deltaTime;
                        }
                    }
                }

                timeElapse += Time.deltaTime;
                roundTime += Time.deltaTime;
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                {
                    if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
                    {
                        if (!isWinning)
                        {
                            timeTotalServer += Time.deltaTime;
                        }
                    }
                    else if (!(isLosing || isWinning))
                    {
                        timeTotalServer += Time.deltaTime;
                    }
                }
                else
                {
                    timeTotalServer += Time.deltaTime;
                }

                if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
                {
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                    {
                        if (!isWinning)
                        {
                            ShowHUDInfoTopCenter("Time : " + ((int)(timeTotalServer * 10f) * 0.1f - 5f));
                        }

                        if (timeTotalServer < 5f)
                        {
                            ShowHUDInfoCenter("RACE START IN " + (int)(5f - timeTotalServer));
                        }
                        else if (!startRacing)
                        {
                            ShowHUDInfoCenter(string.Empty);
                            startRacing = true;
                            endRacing = false;
                            GameObjectCache.Find("door").SetActive(false);
                        }
                    }
                    else
                    {
                        ShowHUDInfoTopCenter("Time : " + (roundTime >= 20f ? (num3 = (int)(roundTime * 10f) * 0.1f - 20f).ToString() : "WAITING"));
                        if (roundTime < 20f)
                        {
                            ShowHUDInfoCenter("RACE START IN " + (int)(20f - roundTime) + (!(localRacingResult == string.Empty) ? "\nLast Round\n" + localRacingResult : "\n\n"));
                        }
                        else if (!startRacing)
                        {
                            ShowHUDInfoCenter(string.Empty);
                            startRacing = true;
                            endRacing = false;
                            var obj2 = GameObjectCache.Find("door");
                            if (obj2 != null)
                            {
                                obj2.SetActive(false);
                            }

                            if (racingDoors != null && customLevelLoaded)
                            {
                                foreach (var obj3 in racingDoors)
                                {
                                    obj3.SetActive(false);
                                }

                                racingDoors = null;
                            }
                        }
                        else if (racingDoors != null && customLevelLoaded)
                        {
                            foreach (var obj3 in racingDoors)
                            {
                                obj3.SetActive(false);
                            }

                            racingDoors = null;
                        }
                    }

                    if (Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().gameOver && !needChooseSide && customLevelLoaded)
                    {
                        myRespawnTime += Time.deltaTime;
                        if (myRespawnTime > 1.5f)
                        {
                            myRespawnTime = 0f;
                            Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().gameOver = false;
                            if (checkpoint != null)
                            {
                                StartCoroutine(WaitAndRespawn2(0.1f, checkpoint));
                            }
                            else
                            {
                                StartCoroutine(WaitAndRespawn1(0.1f, myLastRespawnTag));
                            }

                            Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().gameOver = false;
                            ShowHUDInfoCenter(string.Empty);
                        }
                    }
                }

                if (timeElapse > 1f)
                {
                    timeElapse--;
                    var content = string.Empty;
                    if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.ENDLESS_TITAN)
                    {
                        length = time - (int)timeTotalServer;
                        content = content + "Time : " + length;
                    }
                    else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.KILL_TITAN || IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.None)
                    {
                        content = "Titan Left: ";
                        length = GameObject.FindGameObjectsWithTag("titan").Length;
                        content = content + length + "  Time : ";
                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                        {
                            length = (int)timeTotalServer;
                            content = content + length;
                        }
                        else
                        {
                            length = time - (int)timeTotalServer;
                            content = content + length;
                        }
                    }
                    else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
                    {
                        content = "Titan Left: ";
                        var objArray = new object[4];
                        objArray[0] = content;
                        length = GameObject.FindGameObjectsWithTag("titan").Length;
                        objArray[1] = length.ToString();
                        objArray[2] = " Wave : ";
                        objArray[3] = wave;
                        content = string.Concat(objArray);
                    }
                    else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.BOSS_FIGHT_CT)
                    {
                        content = "Time : ";
                        length = time - (int)timeTotalServer;
                        content = content + length + "\nDefeat the Colossal Titan.\nPrevent abnormal titan from running to the north gate";
                    }
                    else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
                    {
                        var str2 = "| ";
                        for (var i = 0; i < PVPcheckPoint.chkPts.Count; i++)
                        {
                            str2 = str2 + (PVPcheckPoint.chkPts[i] as PVPcheckPoint).getStateString() + " ";
                        }

                        str2 = str2 + "|";
                        length = time - (int)timeTotalServer;
                        content = string.Concat(PVPtitanScoreMax - PVPtitanScore, "  ", str2, "  ", PVPhumanScoreMax - PVPhumanScore, "\n") + "Time : " + length;
                    }

                    if (RCSettings.teamMode > 0)
                    {
                        content = content + "\n[00FFFF]Cyan:" + Convert.ToString(cyanKills) + "       [FF00FF]Magenta:" + Convert.ToString(magentaKills) + "[ffffff]";
                    }

                    ShowHUDInfoTopCenter(content);
                    content = string.Empty;
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                    {
                        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
                        {
                            content = "Time : ";
                            length = (int)timeTotalServer;
                            content = content + length;
                        }
                    }
                    else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.ENDLESS_TITAN)
                    {
                        content = string.Concat("Humanity ", humanScore, " : Titan ", titanScore, " ");
                    }
                    else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.KILL_TITAN || IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.BOSS_FIGHT_CT || IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
                    {
                        content = string.Concat("Humanity ", humanScore, " : Titan ", titanScore, " ");
                    }
                    else if (IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.CAGE_FIGHT)
                    {
                        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
                        {
                            content = "Time : ";
                            length = time - (int)timeTotalServer;
                            content = content + length;
                        }
                        else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS)
                        {
                            for (var j = 0; j < teamScores.Length; j++)
                            {
                                var str3 = content;
                                content = string.Concat(str3, j == 0 ? string.Empty : " : ", "Team", j + 1, " ", teamScores[j], string.Empty);
                            }

                            content = content + "\nTime : " + (time - (int)timeTotalServer);
                        }
                    }

                    ShowHUDInfoTopRight(content);
                    var str4 = IN_GAME_MAIN_CAMERA.difficulty >= 0 ? IN_GAME_MAIN_CAMERA.difficulty != 0 ? IN_GAME_MAIN_CAMERA.difficulty != 1 ? "Abnormal" : "Hard" : "Normal" : "Trainning";
                    if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.CAGE_FIGHT)
                    {
                        ShowHUDInfoTopRightMAPNAME(string.Concat((int)roundTime, "s\n", level, " : ", str4));
                    }
                    else
                    {
                        ShowHUDInfoTopRightMAPNAME("\n" + level + " : " + str4);
                    }

                    if (Settings.DamageFeedUISetting && Application.loadedLevelName.Contains("Forest") && !level.StartsWith("Custom"))
                    {
                        ShowHUDInfoTopCenterADD(string.Concat("\nDamage Feed: ", DamageFeedCurrent, "  Highest Feed: ", DamageFeedHighest));
                    }

                    ShowHUDInfoTopRightMAPNAME($"\nFPS [{Settings.ChatMinorColorSetting}]{FPS.FPS}[-]");

                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                    {
                        char[] separator = { "`"[0] };
                        var str5 = PhotonNetwork.room.name.Split(separator)[0];
                        if (str5.Length > 20)
                        {
                            str5 = str5.Remove(19) + "...";
                        }

                        ShowHUDInfoTopRightMAPNAME("\n" + str5 + " [FFC000](" + Convert.ToString(PhotonNetwork.room.playerCount) + "/" + Convert.ToString(PhotonNetwork.room.maxPlayers) + ")");
                        if (needChooseSide)
                        {
                            ShowHUDInfoTopCenterADD("\n\nPRESS 1 TO ENTER GAME");
                        }
                    }
                }

                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && killInfoGO.Count > 0 && killInfoGO[0] == null)
                {
                    killInfoGO.RemoveAt(0);
                }

                if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE && PhotonNetwork.isMasterClient && timeTotalServer > time)
                {
                    string str11;
                    IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
                    gameStart = false;
                    Screen.lockCursor = false;
                    Screen.showCursor = true;
                    var str6 = string.Empty;
                    var str7 = string.Empty;
                    var str8 = string.Empty;
                    var str9 = string.Empty;
                    var str10 = string.Empty;
                    foreach (var player in PhotonNetwork.playerList)
                    {
                        if (player != null)
                        {
                            str6 = str6 + player.customProperties[PhotonPlayerProperty.name] + "\n";
                            str7 = str7 + player.customProperties[PhotonPlayerProperty.kills] + "\n";
                            str8 = str8 + player.customProperties[PhotonPlayerProperty.deaths] + "\n";
                            str9 = str9 + player.customProperties[PhotonPlayerProperty.max_dmg] + "\n";
                            str10 = str10 + player.customProperties[PhotonPlayerProperty.total_dmg] + "\n";
                        }
                    }

                    if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS)
                    {
                        str11 = string.Empty;
                        for (var k = 0; k < teamScores.Length; k++)
                        {
                            str11 = str11 + (k == 0 ? string.Concat("Team", k + 1, " ", teamScores[k], " ") : " : ");
                        }
                    }
                    else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
                    {
                        str11 = "Highest Wave : " + highestwave;
                    }
                    else
                    {
                        str11 = string.Concat("Humanity ", humanScore, " : Titan ", titanScore);
                    }

                    object[] parameters = { str6, str7, str8, str9, str10, str11 };
                    photonView.RPC("showResult", PhotonTargets.AllBuffered, parameters);
                }
            }
        }
    }

    private void coreadd()
    {
        if (PhotonNetwork.isMasterClient)
        {
            OnUpdate();
            if (customLevelLoaded)
            {
                for (var i = 0; i < titanSpawners.Count; i++)
                {
                    var item = titanSpawners[i];
                    item.time -= Time.deltaTime;
                    if (item.time <= 0f && titans.Count + fT.Count < Math.Min(RCSettings.titanCap, 80))
                    {
                        var name = item.name;
                        if (name == "spawnAnnie")
                        {
                            PhotonNetwork.Instantiate("FEMALE_TITAN", item.location, new Quaternion(0f, 0f, 0f, 1f), 0);
                        }
                        else
                        {
                            var obj2 = PhotonNetwork.Instantiate("TITAN_VER3.1", item.location, new Quaternion(0f, 0f, 0f, 1f), 0);
                            if (name == "spawnAbnormal")
                            {
                                obj2.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_I, false);
                            }
                            else if (name == "spawnJumper")
                            {
                                obj2.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_JUMPER, false);
                            }
                            else if (name == "spawnCrawler")
                            {
                                obj2.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_CRAWLER, true);
                            }
                            else if (name == "spawnPunk")
                            {
                                obj2.GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_PUNK, false);
                            }
                        }

                        if (item.endless)
                        {
                            item.time = item.delay;
                        }
                        else
                        {
                            titanSpawners.Remove(item);
                        }
                    }
                }
            }
        }

        if (Time.timeScale <= 0.1f)
        {
            if (pauseWaitTime <= 3f)
            {
                pauseWaitTime -= Time.deltaTime * 1000000f;
                if (pauseWaitTime <= 1f)
                {
                    Camera.main.farClipPlane = 1500f;
                }

                if (pauseWaitTime <= 0f)
                {
                    pauseWaitTime = 0f;
                    Time.timeScale = 1f;
                }
            }

            justRecompileThePlayerList();
        }
    }

    private void coreeditor()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            GUI.FocusControl(null);
        }

        if (selectedObj != null)
        {
            var num = 0.2f;
            if (inputRC.isInputLevel(InputCodeRC.levelSlow))
            {
                num = 0.04f;
            }
            else if (inputRC.isInputLevel(InputCodeRC.levelFast))
            {
                num = 0.6f;
            }

            if (inputRC.isInputLevel(InputCodeRC.levelForward))
            {
                var transform = selectedObj.transform;
                transform.position += num * new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z);
            }
            else if (inputRC.isInputLevel(InputCodeRC.levelBack))
            {
                var transform9 = selectedObj.transform;
                transform9.position -= num * new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z);
            }

            if (inputRC.isInputLevel(InputCodeRC.levelLeft))
            {
                var transform10 = selectedObj.transform;
                transform10.position -= num * new Vector3(Camera.main.transform.right.x, 0f, Camera.main.transform.right.z);
            }
            else if (inputRC.isInputLevel(InputCodeRC.levelRight))
            {
                var transform11 = selectedObj.transform;
                transform11.position += num * new Vector3(Camera.main.transform.right.x, 0f, Camera.main.transform.right.z);
            }

            if (inputRC.isInputLevel(InputCodeRC.levelDown))
            {
                var transform12 = selectedObj.transform;
                transform12.position -= Vector3.up * num;
            }
            else if (inputRC.isInputLevel(InputCodeRC.levelUp))
            {
                var transform13 = selectedObj.transform;
                transform13.position += Vector3.up * num;
            }

            if (!selectedObj.name.StartsWith("misc,region"))
            {
                if (inputRC.isInputLevel(InputCodeRC.levelRRight))
                {
                    selectedObj.transform.Rotate(Vector3.up * num);
                }
                else if (inputRC.isInputLevel(InputCodeRC.levelRLeft))
                {
                    selectedObj.transform.Rotate(Vector3.down * num);
                }

                if (inputRC.isInputLevel(InputCodeRC.levelRCCW))
                {
                    selectedObj.transform.Rotate(Vector3.forward * num);
                }
                else if (inputRC.isInputLevel(InputCodeRC.levelRCW))
                {
                    selectedObj.transform.Rotate(Vector3.back * num);
                }

                if (inputRC.isInputLevel(InputCodeRC.levelRBack))
                {
                    selectedObj.transform.Rotate(Vector3.left * num);
                }
                else if (inputRC.isInputLevel(InputCodeRC.levelRForward))
                {
                    selectedObj.transform.Rotate(Vector3.right * num);
                }
            }

            if (inputRC.isInputLevel(InputCodeRC.levelPlace))
            {
                linkHash[3].Add(selectedObj.GetInstanceID(), selectedObj.name + "," + Convert.ToString(selectedObj.transform.position.x) + "," + Convert.ToString(selectedObj.transform.position.y) + "," + Convert.ToString(selectedObj.transform.position.z) + "," + Convert.ToString(selectedObj.transform.rotation.x) + "," + Convert.ToString(selectedObj.transform.rotation.y) + "," + Convert.ToString(selectedObj.transform.rotation.z) + "," + Convert.ToString(selectedObj.transform.rotation.w));
                selectedObj = null;
                Camera.main.GetComponent<MouseLook>().enabled = true;
                Screen.lockCursor = true;
            }

            if (inputRC.isInputLevel(InputCodeRC.levelDelete))
            {
                Destroy(selectedObj);
                selectedObj = null;
                Camera.main.GetComponent<MouseLook>().enabled = true;
                Screen.lockCursor = true;
                linkHash[3].Remove(selectedObj.GetInstanceID());
            }
        }
        else
        {
            if (Screen.lockCursor)
            {
                var num2 = 100f;
                if (inputRC.isInputLevel(InputCodeRC.levelSlow))
                {
                    num2 = 20f;
                }
                else if (inputRC.isInputLevel(InputCodeRC.levelFast))
                {
                    num2 = 400f;
                }

                var transform7 = Camera.main.transform;
                if (inputRC.isInputLevel(InputCodeRC.levelForward))
                {
                    transform7.position += transform7.forward * num2 * Time.deltaTime;
                }
                else if (inputRC.isInputLevel(InputCodeRC.levelBack))
                {
                    transform7.position -= transform7.forward * num2 * Time.deltaTime;
                }

                if (inputRC.isInputLevel(InputCodeRC.levelLeft))
                {
                    transform7.position -= transform7.right * num2 * Time.deltaTime;
                }
                else if (inputRC.isInputLevel(InputCodeRC.levelRight))
                {
                    transform7.position += transform7.right * num2 * Time.deltaTime;
                }

                if (inputRC.isInputLevel(InputCodeRC.levelUp))
                {
                    transform7.position += transform7.up * num2 * Time.deltaTime;
                }
                else if (inputRC.isInputLevel(InputCodeRC.levelDown))
                {
                    transform7.position -= transform7.up * num2 * Time.deltaTime;
                }
            }

            if (inputRC.isInputLevelDown(InputCodeRC.levelCursor))
            {
                if (Screen.lockCursor)
                {
                    Camera.main.GetComponent<MouseLook>().enabled = false;
                    Screen.lockCursor = false;
                }
                else
                {
                    Camera.main.GetComponent<MouseLook>().enabled = true;
                    Screen.lockCursor = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && !Screen.lockCursor && GUIUtility.hotControl == 0 && (Input.mousePosition.x > 300f && Input.mousePosition.x < Screen.width - 300f || Screen.height - Input.mousePosition.y > 600f))
            {
                var hitInfo = new RaycastHit();
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
                {
                    var transform8 = hitInfo.transform;
                    if (transform8.gameObject.name.StartsWith("custom") || transform8.gameObject.name.StartsWith("base") || transform8.gameObject.name.StartsWith("racing") || transform8.gameObject.name.StartsWith("photon") || transform8.gameObject.name.StartsWith("spawnpoint") || transform8.gameObject.name.StartsWith("misc"))
                    {
                        selectedObj = transform8.gameObject;
                        Camera.main.GetComponent<MouseLook>().enabled = false;
                        Screen.lockCursor = true;
                        linkHash[3].Remove(selectedObj.GetInstanceID());
                    }
                    else if (transform8.parent.gameObject.name.StartsWith("custom") || transform8.parent.gameObject.name.StartsWith("base") || transform8.parent.gameObject.name.StartsWith("racing") || transform8.parent.gameObject.name.StartsWith("photon"))
                    {
                        selectedObj = transform8.parent.gameObject;
                        Camera.main.GetComponent<MouseLook>().enabled = false;
                        Screen.lockCursor = true;
                        linkHash[3].Remove(selectedObj.GetInstanceID());
                    }
                }
            }
        }
    }

    private IEnumerator customlevelcache()
    {
        for (var i = 0; i < levelCache.Count; i++)
        {
            customlevelclientE(levelCache[i], false);
            yield return new WaitForEndOfFrame();
        }
    }

    private void customlevelclientE(string[] content, bool renewHash)
    {
        int num;
        string[] strArray;
        var flag = false;
        var flag2 = false;
        if (content[content.Length - 1].StartsWith("a"))
        {
            flag = true;
        }
        else if (content[content.Length - 1].StartsWith("z"))
        {
            flag2 = true;
            customLevelLoaded = true;
            spawnPlayerCustomMap();
            Minimap.TryRecaptureInstance();
            unloadAssets();
            Camera.main.GetComponent<TiltShift>().enabled = false;
        }

        if (renewHash)
        {
            if (flag)
            {
                currentLevel = string.Empty;
                levelCache.Clear();
                titanSpawns.Clear();
                playerSpawnsC.Clear();
                playerSpawnsM.Clear();
                for (num = 0; num < content.Length; num++)
                {
                    strArray = content[num].Split(',');
                    if (strArray[0] == "titan")
                    {
                        titanSpawns.Add(new Vector3(Convert.ToSingle(strArray[1]), Convert.ToSingle(strArray[2]), Convert.ToSingle(strArray[3])));
                    }
                    else if (strArray[0] == "playerC")
                    {
                        playerSpawnsC.Add(new Vector3(Convert.ToSingle(strArray[1]), Convert.ToSingle(strArray[2]), Convert.ToSingle(strArray[3])));
                    }
                    else if (strArray[0] == "playerM")
                    {
                        playerSpawnsM.Add(new Vector3(Convert.ToSingle(strArray[1]), Convert.ToSingle(strArray[2]), Convert.ToSingle(strArray[3])));
                    }
                }

                spawnPlayerCustomMap();
            }

            currentLevel = currentLevel + content[content.Length - 1];
            levelCache.Add(content);
            var propertiesToSet = new Hashtable();
            propertiesToSet.Add(PhotonPlayerProperty.currentLevel, currentLevel);
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        }

        if (!flag && !flag2)
        {
            for (num = 0; num < content.Length; num++)
            {
                float num2;
                GameObject obj2;
                float num3;
                float num5;
                float num6;
                float num7;
                Color color;
                Mesh mesh;
                Color[] colorArray;
                int num8;
                strArray = content[num].Split(',');
                if (strArray[0].StartsWith("custom"))
                {
                    num2 = 1f;
                    obj2 = null;
                    obj2 = (GameObject)Instantiate((GameObject)ResourcesCache.RCLoadGO(strArray[1]), new Vector3(Convert.ToSingle(strArray[12]), Convert.ToSingle(strArray[13]), Convert.ToSingle(strArray[14])), new Quaternion(Convert.ToSingle(strArray[15]), Convert.ToSingle(strArray[16]), Convert.ToSingle(strArray[17]), Convert.ToSingle(strArray[18])));
                    if (strArray[2] != "default")
                    {
                        if (strArray[2].StartsWith("transparent"))
                        {
                            if (float.TryParse(strArray[2].Substring(11), out num3))
                            {
                                num2 = num3;
                            }

                            foreach (var renderer in obj2.GetComponentsInChildren<Renderer>())
                            {
                                renderer.material = (Material)ResourcesCache.RCLoadM("transparent");
                                if (Convert.ToSingle(strArray[10]) != 1f || Convert.ToSingle(strArray[11]) != 1f)
                                {
                                    renderer.material.mainTextureScale = new Vector2(renderer.material.mainTextureScale.x * Convert.ToSingle(strArray[10]), renderer.material.mainTextureScale.y * Convert.ToSingle(strArray[11]));
                                }
                            }
                        }
                        else
                        {
                            foreach (var renderer in obj2.GetComponentsInChildren<Renderer>())
                            {
                                renderer.material = (Material)ResourcesCache.RCLoadM(strArray[2]);
                                if (Convert.ToSingle(strArray[10]) != 1f || Convert.ToSingle(strArray[11]) != 1f)
                                {
                                    renderer.material.mainTextureScale = new Vector2(renderer.material.mainTextureScale.x * Convert.ToSingle(strArray[10]), renderer.material.mainTextureScale.y * Convert.ToSingle(strArray[11]));
                                }
                            }
                        }
                    }

                    num5 = obj2.transform.localScale.x * Convert.ToSingle(strArray[3]);
                    num5 -= 0.001f;
                    num6 = obj2.transform.localScale.y * Convert.ToSingle(strArray[4]);
                    num7 = obj2.transform.localScale.z * Convert.ToSingle(strArray[5]);
                    obj2.transform.localScale = new Vector3(num5, num6, num7);
                    if (strArray[6] != "0")
                    {
                        color = new Color(Convert.ToSingle(strArray[7]), Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), num2);
                        foreach (var filter in obj2.GetComponentsInChildren<MeshFilter>())
                        {
                            mesh = filter.mesh;
                            colorArray = new Color[mesh.vertexCount];
                            num8 = 0;
                            while (num8 < mesh.vertexCount)
                            {
                                colorArray[num8] = color;
                                num8++;
                            }

                            mesh.colors = colorArray;
                        }
                    }
                }
                else if (strArray[0].StartsWith("base"))
                {
                    if (strArray.Length < 15)
                    {
                        Instantiate(Resources.Load(strArray[1]), new Vector3(Convert.ToSingle(strArray[2]), Convert.ToSingle(strArray[3]), Convert.ToSingle(strArray[4])), new Quaternion(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7]), Convert.ToSingle(strArray[8])));
                    }
                    else
                    {
                        num2 = 1f;
                        obj2 = null;
                        obj2 = (GameObject)Instantiate((GameObject)Resources.Load(strArray[1]), new Vector3(Convert.ToSingle(strArray[12]), Convert.ToSingle(strArray[13]), Convert.ToSingle(strArray[14])), new Quaternion(Convert.ToSingle(strArray[15]), Convert.ToSingle(strArray[16]), Convert.ToSingle(strArray[17]), Convert.ToSingle(strArray[18])));
                        if (strArray[2] != "default")
                        {
                            if (strArray[2].StartsWith("transparent"))
                            {
                                if (float.TryParse(strArray[2].Substring(11), out num3))
                                {
                                    num2 = num3;
                                }

                                foreach (var renderer in obj2.GetComponentsInChildren<Renderer>())
                                {
                                    renderer.material = (Material)ResourcesCache.RCLoadM("transparent");
                                    if (Convert.ToSingle(strArray[10]) != 1f || Convert.ToSingle(strArray[11]) != 1f)
                                    {
                                        renderer.material.mainTextureScale = new Vector2(renderer.material.mainTextureScale.x * Convert.ToSingle(strArray[10]), renderer.material.mainTextureScale.y * Convert.ToSingle(strArray[11]));
                                    }
                                }
                            }
                            else
                            {
                                foreach (var renderer in obj2.GetComponentsInChildren<Renderer>())
                                {
                                    if (!renderer.name.Contains("Particle System") || !obj2.name.Contains("aot_supply"))
                                    {
                                        renderer.material = (Material)ResourcesCache.RCLoadM(strArray[2]);
                                        if (Convert.ToSingle(strArray[10]) != 1f || Convert.ToSingle(strArray[11]) != 1f)
                                        {
                                            renderer.material.mainTextureScale = new Vector2(renderer.material.mainTextureScale.x * Convert.ToSingle(strArray[10]), renderer.material.mainTextureScale.y * Convert.ToSingle(strArray[11]));
                                        }
                                    }
                                }
                            }
                        }

                        num5 = obj2.transform.localScale.x * Convert.ToSingle(strArray[3]);
                        num5 -= 0.001f;
                        num6 = obj2.transform.localScale.y * Convert.ToSingle(strArray[4]);
                        num7 = obj2.transform.localScale.z * Convert.ToSingle(strArray[5]);
                        obj2.transform.localScale = new Vector3(num5, num6, num7);
                        if (strArray[6] != "0")
                        {
                            color = new Color(Convert.ToSingle(strArray[7]), Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), num2);
                            foreach (var filter in obj2.GetComponentsInChildren<MeshFilter>())
                            {
                                mesh = filter.mesh;
                                colorArray = new Color[mesh.vertexCount];
                                for (num8 = 0; num8 < mesh.vertexCount; num8++)
                                {
                                    colorArray[num8] = color;
                                }

                                mesh.colors = colorArray;
                            }
                        }
                    }
                }
                else if (strArray[0].StartsWith("misc"))
                {
                    if (strArray[1].StartsWith("barrier"))
                    {
                        obj2 = null;
                        obj2 = (GameObject)Instantiate((GameObject)ResourcesCache.RCLoadGO(strArray[1]), new Vector3(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7])), new Quaternion(Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), Convert.ToSingle(strArray[10]), Convert.ToSingle(strArray[11])));
                        num5 = obj2.transform.localScale.x * Convert.ToSingle(strArray[2]);
                        num5 -= 0.001f;
                        num6 = obj2.transform.localScale.y * Convert.ToSingle(strArray[3]);
                        num7 = obj2.transform.localScale.z * Convert.ToSingle(strArray[4]);
                        obj2.transform.localScale = new Vector3(num5, num6, num7);
                    }
                    else if (strArray[1].StartsWith("racingStart"))
                    {
                        obj2 = null;
                        obj2 = (GameObject)Instantiate((GameObject)ResourcesCache.RCLoadGO(strArray[1]), new Vector3(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7])), new Quaternion(Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), Convert.ToSingle(strArray[10]), Convert.ToSingle(strArray[11])));
                        num5 = obj2.transform.localScale.x * Convert.ToSingle(strArray[2]);
                        num5 -= 0.001f;
                        num6 = obj2.transform.localScale.y * Convert.ToSingle(strArray[3]);
                        num7 = obj2.transform.localScale.z * Convert.ToSingle(strArray[4]);
                        obj2.transform.localScale = new Vector3(num5, num6, num7);
                        racingDoors?.Add(obj2);
                    }
                    else if (strArray[1].StartsWith("racingEnd"))
                    {
                        obj2 = null;
                        obj2 = (GameObject)Instantiate((GameObject)ResourcesCache.RCLoadGO(strArray[1]), new Vector3(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7])), new Quaternion(Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), Convert.ToSingle(strArray[10]), Convert.ToSingle(strArray[11])));
                        num5 = obj2.transform.localScale.x * Convert.ToSingle(strArray[2]);
                        num5 -= 0.001f;
                        num6 = obj2.transform.localScale.y * Convert.ToSingle(strArray[3]);
                        num7 = obj2.transform.localScale.z * Convert.ToSingle(strArray[4]);
                        obj2.transform.localScale = new Vector3(num5, num6, num7);
                        obj2.AddComponent<LevelTriggerRacingEnd>();
                    }
                    else if (strArray[1].StartsWith("region") && PhotonNetwork.isMasterClient)
                    {
                        var loc = new Vector3(Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7]), Convert.ToSingle(strArray[8]));
                        var region = new RCRegion(loc, Convert.ToSingle(strArray[3]), Convert.ToSingle(strArray[4]), Convert.ToSingle(strArray[5]));
                        var key = strArray[2];
                        if (RCRegionTriggers.ContainsKey(key))
                        {
                            var obj3 = (GameObject)Instantiate((GameObject)ResourcesCache.RCLoadGO("region"));
                            obj3.transform.position = loc;
                            obj3.AddComponent<RegionTrigger>();
                            obj3.GetComponent<RegionTrigger>().CopyTrigger((RegionTrigger)RCRegionTriggers[key]);
                            num5 = obj3.transform.localScale.x * Convert.ToSingle(strArray[3]);
                            num5 -= 0.001f;
                            num6 = obj3.transform.localScale.y * Convert.ToSingle(strArray[4]);
                            num7 = obj3.transform.localScale.z * Convert.ToSingle(strArray[5]);
                            obj3.transform.localScale = new Vector3(num5, num6, num7);
                            region.myBox = obj3;
                        }

                        RCRegions.Add(key, region);
                    }
                }
                else if (strArray[0].StartsWith("racing"))
                {
                    if (strArray[1].StartsWith("start"))
                    {
                        obj2 = null;
                        obj2 = (GameObject)Instantiate((GameObject)ResourcesCache.RCLoadGO(strArray[1]), new Vector3(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7])), new Quaternion(Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), Convert.ToSingle(strArray[10]), Convert.ToSingle(strArray[11])));
                        num5 = obj2.transform.localScale.x * Convert.ToSingle(strArray[2]);
                        num5 -= 0.001f;
                        num6 = obj2.transform.localScale.y * Convert.ToSingle(strArray[3]);
                        num7 = obj2.transform.localScale.z * Convert.ToSingle(strArray[4]);
                        obj2.transform.localScale = new Vector3(num5, num6, num7);
                        racingDoors?.Add(obj2);
                    }
                    else if (strArray[1].StartsWith("end"))
                    {
                        obj2 = null;
                        obj2 = (GameObject)Instantiate((GameObject)ResourcesCache.RCLoadGO(strArray[1]), new Vector3(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7])), new Quaternion(Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), Convert.ToSingle(strArray[10]), Convert.ToSingle(strArray[11])));
                        num5 = obj2.transform.localScale.x * Convert.ToSingle(strArray[2]);
                        num5 -= 0.001f;
                        num6 = obj2.transform.localScale.y * Convert.ToSingle(strArray[3]);
                        num7 = obj2.transform.localScale.z * Convert.ToSingle(strArray[4]);
                        obj2.transform.localScale = new Vector3(num5, num6, num7);
                        obj2.GetComponentInChildren<Collider>().gameObject.AddComponent<LevelTriggerRacingEnd>();
                    }
                    else if (strArray[1].StartsWith("kill"))
                    {
                        obj2 = null;
                        obj2 = (GameObject)Instantiate((GameObject)ResourcesCache.RCLoadGO(strArray[1]), new Vector3(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7])), new Quaternion(Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), Convert.ToSingle(strArray[10]), Convert.ToSingle(strArray[11])));
                        num5 = obj2.transform.localScale.x * Convert.ToSingle(strArray[2]);
                        num5 -= 0.001f;
                        num6 = obj2.transform.localScale.y * Convert.ToSingle(strArray[3]);
                        num7 = obj2.transform.localScale.z * Convert.ToSingle(strArray[4]);
                        obj2.transform.localScale = new Vector3(num5, num6, num7);
                        obj2.GetComponentInChildren<Collider>().gameObject.AddComponent<RacingKillTrigger>();
                    }
                    else if (strArray[1].StartsWith("checkpoint"))
                    {
                        obj2 = null;
                        obj2 = (GameObject)Instantiate((GameObject)ResourcesCache.RCLoadGO(strArray[1]), new Vector3(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7])), new Quaternion(Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), Convert.ToSingle(strArray[10]), Convert.ToSingle(strArray[11])));
                        num5 = obj2.transform.localScale.x * Convert.ToSingle(strArray[2]);
                        num5 -= 0.001f;
                        num6 = obj2.transform.localScale.y * Convert.ToSingle(strArray[3]);
                        num7 = obj2.transform.localScale.z * Convert.ToSingle(strArray[4]);
                        obj2.transform.localScale = new Vector3(num5, num6, num7);
                        obj2.GetComponentInChildren<Collider>().gameObject.AddComponent<RacingCheckpointTrigger>();
                    }
                }
                else if (strArray[0].StartsWith("map"))
                {
                    if (strArray[1].StartsWith("disablebounds"))
                    {
                        Destroy(GameObjectCache.Find("gameobjectOutSide"));
                        Instantiate(ResourcesCache.RCLoadGO("outside"));
                    }
                }
                else if (PhotonNetwork.isMasterClient && strArray[0].StartsWith("photon"))
                {
                    if (strArray[1].StartsWith("Cannon"))
                    {
                        if (strArray.Length > 15)
                        {
                            var go = PhotonNetwork.Instantiate("RCAsset/" + strArray[1] + "Prop", new Vector3(Convert.ToSingle(strArray[12]), Convert.ToSingle(strArray[13]), Convert.ToSingle(strArray[14])), new Quaternion(Convert.ToSingle(strArray[15]), Convert.ToSingle(strArray[16]), Convert.ToSingle(strArray[17]), Convert.ToSingle(strArray[18])), 0);
                            go.GetComponent<CannonPropRegion>().settings = content[num];
                            go.GetPhotonView().RPC("SetSize", PhotonTargets.AllBuffered, content[num]);
                        }
                        else
                        {
                            PhotonNetwork.Instantiate("RCAsset/" + strArray[1] + "Prop", new Vector3(Convert.ToSingle(strArray[2]), Convert.ToSingle(strArray[3]), Convert.ToSingle(strArray[4])), new Quaternion(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7]), Convert.ToSingle(strArray[8])), 0).GetComponent<CannonPropRegion>().settings = content[num];
                        }
                    }
                    else
                    {
                        var item = new TitanSpawner();
                        num5 = 30f;
                        if (float.TryParse(strArray[2], out num3))
                        {
                            num5 = Mathf.Max(Convert.ToSingle(strArray[2]), 1f);
                        }

                        item.time = num5;
                        item.delay = num5;
                        item.name = strArray[1];
                        if (strArray[3] == "1")
                        {
                            item.endless = true;
                        }
                        else
                        {
                            item.endless = false;
                        }

                        item.location = new Vector3(Convert.ToSingle(strArray[4]), Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]));
                        titanSpawners.Add(item);
                    }
                }
            }
        }
    }

    private IEnumerator customlevelE(List<PhotonPlayer> players)
    {
        string[] strArray;
        if (!(currentLevel == string.Empty))
        {
            for (var i = 0; i < levelCache.Count; i++)
            {
                foreach (var player in players)
                {
                    if (player.customProperties[PhotonPlayerProperty.currentLevel] != null && currentLevel != string.Empty && RCextensions.returnStringFromObject(player.customProperties[PhotonPlayerProperty.currentLevel]) == currentLevel)
                    {
                        if (i == 0)
                        {
                            strArray = new[] { "loadcached" };
                            photonView.RPC("customlevelRPC", player, new object[] { strArray });
                        }
                    }
                    else
                    {
                        photonView.RPC("customlevelRPC", player, new object[] { levelCache[i] });
                    }
                }

                if (i > 0)
                {
                    yield return new WaitForSeconds(0.75f);
                }
                else
                {
                    yield return new WaitForSeconds(0.25f);
                }
            }

            yield break;
        }

        strArray = new[] { "loadempty" };
        foreach (var player in players)
        {
            photonView.RPC("customlevelRPC", player, new object[] { strArray });
        }

        customLevelLoaded = true;
    }

    [RPC]
    private void customlevelRPC(string[] content, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            if (content.Length == 1 && content[0] == "loadcached")
            {
                StartCoroutine(customlevelcache());
            }
            else if (content.Length == 1 && content[0] == "loadempty")
            {
                currentLevel = string.Empty;
                levelCache.Clear();
                titanSpawns.Clear();
                playerSpawnsC.Clear();
                playerSpawnsM.Clear();
                var propertiesToSet = new Hashtable();
                propertiesToSet.Add(PhotonPlayerProperty.currentLevel, currentLevel);
                PhotonNetwork.player.SetCustomProperties(propertiesToSet);
                customLevelLoaded = true;
                spawnPlayerCustomMap();
            }
            else
            {
                customlevelclientE(content, true);
            }
        }
    }

    private void endGameInfectionRC()
    {
        int num;
        imatitan.Clear();
        for (num = 0; num < PhotonNetwork.playerList.Length; num++)
        {
            var player = PhotonNetwork.playerList[num];
            var propertiesToSet = new Hashtable();
            propertiesToSet.Add(PhotonPlayerProperty.isTitan, 1);
            player.SetCustomProperties(propertiesToSet);
        }

        var length = PhotonNetwork.playerList.Length;
        var infectionMode = RCSettings.infectionMode;
        for (num = 0; num < PhotonNetwork.playerList.Length; num++)
        {
            var player2 = PhotonNetwork.playerList[num];
            if (length > 0 && Random.Range(0f, 1f) <= infectionMode / (float)length)
            {
                var hashtable2 = new Hashtable();
                hashtable2.Add(PhotonPlayerProperty.isTitan, 2);
                player2.SetCustomProperties(hashtable2);
                imatitan.Add(player2.ID, 2);
                infectionMode--;
            }

            length--;
        }

        gameEndCD = 0f;
        restartGame(false);
    }

    private void endGameRC()
    {
        if (RCSettings.pointMode > 0)
        {
            for (var i = 0; i < PhotonNetwork.playerList.Length; i++)
            {
                var player = PhotonNetwork.playerList[i];
                var propertiesToSet = new Hashtable();
                propertiesToSet.Add(PhotonPlayerProperty.kills, 0);
                propertiesToSet.Add(PhotonPlayerProperty.deaths, 0);
                propertiesToSet.Add(PhotonPlayerProperty.max_dmg, 0);
                propertiesToSet.Add(PhotonPlayerProperty.total_dmg, 0);
                player.SetCustomProperties(propertiesToSet);
            }
        }

        gameEndCD = 0f;
        restartGame(false);
    }

    [RPC]
    private void extendedSkinsRPC(int ambient, float[] ambientColor, int light, float[] lightColor, int fog, float[] fogColor, PhotonMessageInfo info)
    {
        if (Settings.LocationSkinsSetting == 2 && info.sender.isMasterClient)
        {
            Settings.ReceivedLocationSkinsData = new List<object>();
            Settings.ReceivedLocationSkinsData.Add(ambient);
            Settings.ReceivedLocationSkinsData.Add(ambientColor[0]);
            Settings.ReceivedLocationSkinsData.Add(ambientColor[1]);
            Settings.ReceivedLocationSkinsData.Add(ambientColor[2]);
            Settings.ReceivedLocationSkinsData.Add(light);
            Settings.ReceivedLocationSkinsData.Add(lightColor[0]);
            Settings.ReceivedLocationSkinsData.Add(lightColor[1]);
            Settings.ReceivedLocationSkinsData.Add(lightColor[2]);
            Settings.ReceivedLocationSkinsData.Add(fog);
            Settings.ReceivedLocationSkinsData.Add(fogColor[0]);
            Settings.ReceivedLocationSkinsData.Add(fogColor[1]);
            Settings.ReceivedLocationSkinsData.Add(fogColor[2]);
            Settings.ReceivedLocationSkinsData.Add(fogColor[3]);
            Settings.ReceivedLocationSkinsData.Add(fogColor[4]);
        }
    }

    [RPC]
    private void getRacingResult(string player, float time)
    {
        var result = new RacingResult { name = player, time = time };
        racingResult.Add(result);
        refreshRacingResult();
    }

    [RPC]
    private void ignorePlayer(int ID, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            var player = PhotonPlayer.Find(ID);
            if (player != null && !ignoreList.Contains(ID))
            {
                for (var i = 0; i < PhotonNetwork.playerList.Length; i++)
                {
                    if (PhotonNetwork.playerList[i] == player)
                    {
                        ignoreList.Add(ID);
                        var options = new RaiseEventOptions { TargetActors = new[] { ID } };
                        PhotonNetwork.RaiseEvent(254, null, true, options);
                    }
                }
            }
        }

        RecompilePlayerList(0.1f);
    }

    [RPC]
    private void ignorePlayerArray(int[] IDS, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            for (var i = 0; i < IDS.Length; i++)
            {
                var iD = IDS[i];
                var player = PhotonPlayer.Find(iD);
                if (player != null && !ignoreList.Contains(iD))
                {
                    for (var j = 0; j < PhotonNetwork.playerList.Length; j++)
                    {
                        if (PhotonNetwork.playerList[j] == player)
                        {
                            ignoreList.Add(iD);
                            var options = new RaiseEventOptions { TargetActors = new[] { iD } };
                            PhotonNetwork.RaiseEvent(254, null, true, options);
                        }
                    }
                }
            }
        }

        RecompilePlayerList(0.1f);
    }

    private void kickPhotonPlayer(string name)
    {
        Print("KICK " + name + "!!!");
        foreach (var player in PhotonNetwork.playerList)
        {
            if (player.ID.ToString() == name && !player.isMasterClient)
            {
                PhotonNetwork.CloseConnection(player);
                return;
            }
        }
    }

    private void kickPlayer(string kickPlayer, string kicker)
    {
        KickState state;
        var flag = false;
        for (var i = 0; i < kicklist.Count; i++)
        {
            if (((KickState)kicklist[i]).name == kickPlayer)
            {
                state = (KickState)kicklist[i];
                state.addKicker(kicker);
                tryKick(state);
                flag = true;
                break;
            }
        }

        if (!flag)
        {
            state = new KickState();
            state.init(kickPlayer);
            state.addKicker(kicker);
            kicklist.Add(state);
            tryKick(state);
        }
    }

    [RPC]
    private void labelRPC(int setting, PhotonMessageInfo info)
    {
        if (PhotonView.Find(setting) != null)
        {
            var owner = PhotonView.Find(setting).owner;
            if (owner == info.sender)
            {
                var str = RCextensions.returnStringFromObject(owner.customProperties[PhotonPlayerProperty.guildName]);
                var str2 = RCextensions.returnStringFromObject(owner.customProperties[PhotonPlayerProperty.name]);
                var gameObject = PhotonView.Find(setting).gameObject;
                if (gameObject != null)
                {
                    var component = gameObject.GetComponent<HERO>();
                    if (component != null)
                    {
                        if (str != string.Empty)
                        {
                            component.myNetWorkName.GetComponent<UILabel>().text = "[FFFF00]" + str + "\n[FFFFFF]" + str2;
                        }
                        else
                        {
                            component.myNetWorkName.GetComponent<UILabel>().text = str2;
                        }
                    }
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (gameStart)
        {
            foreach (HERO hERO in heroes)
            {
                hERO.lateUpdate();
            }

            foreach (TITAN_EREN tITAN_EREN in eT)
            {
                tITAN_EREN.lateUpdate();
            }

            foreach (TITAN tITAN in titans)
            {
                tITAN.lateUpdate2();
            }

            foreach (FEMALE_TITAN fEMALE_TITAN in fT)
            {
                fEMALE_TITAN.lateUpdate();
            }

            core();
        }
    }

    public void loadskin()
    {
        GameObject[] objArray;
        int num;
        GameObject obj2;
        if ((int)settings[64] >= 100)
        {
            string[] strArray2 = { "Flare", "LabelInfoBottomRight", "LabelNetworkStatus", "skill_cd_bottom", "GasUI" };
            objArray = (GameObject[])FindObjectsOfType(typeof(GameObject));
            for (num = 0; num < objArray.Length; num++)
            {
                obj2 = objArray[num];
                if (obj2.name.Contains("TREE") || obj2.name.Contains("aot_supply") || obj2.name.Contains("gameobjectOutSide"))
                {
                    Destroy(obj2);
                }
            }

            GameObjectCache.Find("Cube_001").renderer.material.mainTexture = ((Material)ResourcesCache.RCLoadM("grass")).mainTexture;
            Instantiate(ResourcesCache.RCLoadGO("spawnPlayer"), new Vector3(-10f, 1f, -10f), new Quaternion(0f, 0f, 0f, 1f));
            for (num = 0; num < strArray2.Length; num++)
            {
                var name = strArray2[num];
                var obj3 = GameObjectCache.Find(name);
                if (obj3 != null)
                {
                    Destroy(obj3);
                }
            }

            Camera.main.GetComponent<SpectatorMovement>().disable = true;
        }
        else
        {
            GameObject obj;
            string[] SkyBoxArray;
            int num2;
            InstantiateTracker.instance.Dispose();
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && PhotonNetwork.isMasterClient)
            {
                updateTime = 1f;
                if (oldScriptLogic != currentScriptLogic)
                {
                    intVariables.Clear();
                    boolVariables.Clear();
                    stringVariables.Clear();
                    floatVariables.Clear();
                    globalVariables.Clear();
                    RCEvents.Clear();
                    RCVariableNames.Clear();
                    playerVariables.Clear();
                    titanVariables.Clear();
                    RCRegionTriggers.Clear();
                    oldScriptLogic = currentScriptLogic;
                    compileScript(currentScriptLogic);
                    if (RCEvents.ContainsKey("OnFirstLoad"))
                    {
                        var event2 = (RCEvent)RCEvents["OnFirstLoad"];
                        event2.checkEvent();
                    }
                }

                if (RCEvents.ContainsKey("OnRoundStart"))
                {
                    ((RCEvent)RCEvents["OnRoundStart"]).checkEvent();
                }

                photonView.RPC("setMasterRC", PhotonTargets.All);
            }

            logicLoaded = true;
            racingSpawnPoint = new Vector3(0f, 0f, 0f);
            racingSpawnPointSet = false;
            racingDoors = new List<GameObject>();
            allowedToCannon = new Dictionary<int, CannonValues>();
            if (!level.StartsWith("Custom") && Settings.LocationSkinsSetting > 0)
            {
                obj = GameObjectCache.Find("aot_supply");
                if (obj != null && Minimap.instance != null)
                {
                    Minimap.instance.TrackGameObjectOnMinimap(obj, Color.white, false, true, Minimap.IconStyle.SUPPLY);
                }

                var url = string.Empty;
                var str3 = string.Empty;
                var n = string.Empty;
                SkyBoxArray = new[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
                if (Application.loadedLevelName.Contains("City"))
                {
                    //Houses
                    for (var k = 3; k < 11; k++)
                    {
                        url += Settings.LocationSkinsCityList[Settings.LocationSkinsCityCurrentSetSetting][k] + (k != 10 ? "," : "");
                    }

                    num2 = 0;
                    while (num2 < 250)
                    {
                        n += Convert.ToString((int)UnityEngine.Random.Range(0f, 8f));
                        num2++;
                    }

                    //Ground Wall Gates
                    for (var i = 0; i < 3; i++)
                    {
                        str3 += Settings.LocationSkinsCityList[Settings.LocationSkinsCityCurrentSetSetting][i] + (i != 2 ? "," : "");
                    }

                    //Skybox
                    var skyboxpart = 0;
                    for (var l = 11; l < 17; l++)
                    {
                        SkyBoxArray[skyboxpart] = Settings.LocationSkinsCityList[Settings.LocationSkinsCityCurrentSetSetting][l];
                        skyboxpart++;
                    }
                }
                else if (Application.loadedLevelName.Contains("Forest"))
                {
                    //Trunks
                    for (var i = 1; i < 9; i++)
                    {
                        url += Settings.LocationSkinsForestList[Settings.LocationSkinsForestCurrentSetSetting][i] + (i != 8 ? "," : "");
                    }

                    //Leaves
                    for (var j = 9; j < 17; j++)
                    {
                        str3 += Settings.LocationSkinsForestList[Settings.LocationSkinsForestCurrentSetSetting][j] + ",";
                    }

                    //Ground
                    str3 += Settings.LocationSkinsForestList[Settings.LocationSkinsForestCurrentSetSetting][0];
                    for (var k = 0; k < 150; k++)
                    {
                        var str5 = Convert.ToString((int)UnityEngine.Random.Range(0f, 8f));
                        n += str5;
                        if (!Settings.LocationSkinsRandomizedPairsSetting)
                        {
                            n += str5;
                        }
                        else
                        {
                            n += Convert.ToString((int)UnityEngine.Random.Range(0f, 8f));
                        }
                    }

                    //Skybox
                    var skyboxpart = 0;
                    for (var l = 17; l < 23; l++)
                    {
                        SkyBoxArray[skyboxpart] = Settings.LocationSkinsForestList[Settings.LocationSkinsForestCurrentSetSetting][l];
                        skyboxpart++;
                    }
                }

                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || PhotonNetwork.isMasterClient && Settings.LocationSkinsSetting == 2 || Settings.LocationSkinsSetting == 1)
                {
                    StartCoroutine(loadskinE(n, url, str3, SkyBoxArray));
                }

                if (PhotonNetwork.isMasterClient && Settings.LocationSkinsSetting == 2)
                {
                    if (Application.loadedLevelName.Contains("Forest"))
                    {
                        Settings.ExtendedSkinsToSend = new object[]
                        {
                            Settings.LocationSkinsForestAmbientList[Settings.LocationSkinsForestCurrentSetSetting],
                            new float[]
                            {
                                Settings.LocationSkinsForestAmbientSettingsList[Settings.LocationSkinsForestCurrentSetSetting][0],
                                Settings.LocationSkinsForestAmbientSettingsList[Settings.LocationSkinsForestCurrentSetSetting][1],
                                Settings.LocationSkinsForestAmbientSettingsList[Settings.LocationSkinsForestCurrentSetSetting][2]
                            },
                            Settings.LocationSkinsForestLightList[Settings.LocationSkinsForestCurrentSetSetting],
                            new float[]
                            {
                                Settings.LocationSkinsForestLightSettingsList[Settings.LocationSkinsForestCurrentSetSetting][0],
                                Settings.LocationSkinsForestLightSettingsList[Settings.LocationSkinsForestCurrentSetSetting][1],
                                Settings.LocationSkinsForestLightSettingsList[Settings.LocationSkinsForestCurrentSetSetting][2]
                            },
                            Settings.LocationSkinsForestFogList[Settings.LocationSkinsForestCurrentSetSetting],
                            new float[]
                            {
                                Settings.LocationSkinsForestFogSettingsList[Settings.LocationSkinsForestCurrentSetSetting][0],
                                Settings.LocationSkinsForestFogSettingsList[Settings.LocationSkinsForestCurrentSetSetting][1],
                                Settings.LocationSkinsForestFogSettingsList[Settings.LocationSkinsForestCurrentSetSetting][2],
                                Settings.LocationSkinsForestFogSettingsList[Settings.LocationSkinsForestCurrentSetSetting][3],
                                Settings.LocationSkinsForestFogSettingsList[Settings.LocationSkinsForestCurrentSetSetting][4]

                            }
                        };
                        photonView.SendToGGMUser("extendedSkinsRPC", Settings.ExtendedSkinsToSend);
                    }

                    if (Application.loadedLevelName.Contains("City"))
                    {
                        Settings.ExtendedSkinsToSend = new object[]
                        {
                            Settings.LocationSkinsCityAmbientList[Settings.LocationSkinsCityCurrentSetSetting],
                            new float[]
                            {
                                Settings.LocationSkinsCityAmbientSettingsList[Settings.LocationSkinsCityCurrentSetSetting][0],
                                Settings.LocationSkinsCityAmbientSettingsList[Settings.LocationSkinsCityCurrentSetSetting][1],
                                Settings.LocationSkinsCityAmbientSettingsList[Settings.LocationSkinsCityCurrentSetSetting][2]
                            },
                            Settings.LocationSkinsCityLightList[Settings.LocationSkinsCityCurrentSetSetting],
                            new float[]
                            {
                                Settings.LocationSkinsCityLightSettingsList[Settings.LocationSkinsCityCurrentSetSetting][0],
                                Settings.LocationSkinsCityLightSettingsList[Settings.LocationSkinsCityCurrentSetSetting][1],
                                Settings.LocationSkinsCityLightSettingsList[Settings.LocationSkinsCityCurrentSetSetting][2],
                            },
                            Settings.LocationSkinsCityFogList[Settings.LocationSkinsCityCurrentSetSetting],
                            new float[]
                            {
                                Settings.LocationSkinsCityFogSettingsList[Settings.LocationSkinsCityCurrentSetSetting][0],
                                Settings.LocationSkinsCityFogSettingsList[Settings.LocationSkinsCityCurrentSetSetting][1],
                                Settings.LocationSkinsCityFogSettingsList[Settings.LocationSkinsCityCurrentSetSetting][2],
                                Settings.LocationSkinsCityFogSettingsList[Settings.LocationSkinsCityCurrentSetSetting][3],
                                Settings.LocationSkinsCityFogSettingsList[Settings.LocationSkinsCityCurrentSetSetting][4]
                            }
                        };
                        photonView.SendToGGMUser("extendedSkinsRPC", Settings.ExtendedSkinsToSend);
                    }
                    photonView.RPC("loadskinRPC", PhotonTargets.AllBuffered, n, url, str3, SkyBoxArray);
                }
            }
            else if (level.StartsWith("Custom") && IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
            {
                var objArray3 = GameObject.FindGameObjectsWithTag("playerRespawn");
                for (num = 0; num < objArray3.Length; num++)
                {
                    obj = objArray3[num];
                    obj.transform.position = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
                }

                objArray = (GameObject[])FindObjectsOfType(typeof(GameObject));
                for (num = 0; num < objArray.Length; num++)
                {
                    obj2 = objArray[num];
                    if (obj2.name.Contains("TREE") || obj2.name.Contains("aot_supply"))
                    {
                        Destroy(obj2);
                    }
                    else if (obj2.name == "Cube_001" && obj2.transform.parent.gameObject.tag != "player" && obj2.renderer != null)
                    {
                        groundList.Add(obj2);
                        obj2.renderer.material.mainTexture = ((Material)ResourcesCache.RCLoadM("grass")).mainTexture;
                    }
                }

                if (PhotonNetwork.isMasterClient)
                {
                    int num6;
                    SkyBoxArray = new[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
                    for (num = 0; num < 6; num++)
                    {
                        SkyBoxArray[num] = Settings.CustomMapSkinsList[Settings.CustomMapSkinsCurrentSetSetting][num + 1];
                    }

                    SkyBoxArray[6] = Settings.CustomMapSkinsList[Settings.CustomMapSkinsCurrentSetSetting][0];
                    RCSettings.titanCap = Settings.TitansSpawnCapSetting;
                    RCSettings.titanCap = Math.Min(50, RCSettings.titanCap);
                    photonView.RPC("clearlevel", PhotonTargets.AllBuffered, SkyBoxArray, RCSettings.gameType);
                    RCRegions.Clear();
                    if (oldScript != currentScript)
                    {
                        Hashtable hashtable;
                        levelCache.Clear();
                        titanSpawns.Clear();
                        playerSpawnsC.Clear();
                        playerSpawnsM.Clear();
                        titanSpawners.Clear();
                        currentLevel = string.Empty;
                        if (currentScript == string.Empty)
                        {
                            hashtable = new Hashtable();
                            hashtable.Add(PhotonPlayerProperty.currentLevel, currentLevel);
                            PhotonNetwork.player.SetCustomProperties(hashtable);
                            oldScript = currentScript;
                        }
                        else
                        {
                            var strArray4 = Regex.Replace(currentScript, @"\s+", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Split(';');
                            for (num = 0; num < Mathf.FloorToInt((strArray4.Length - 1) / 100) + 1; num++)
                            {
                                string[] strArray5;
                                int num7;
                                string[] strArray6;
                                string str6;
                                if (num < Mathf.FloorToInt(strArray4.Length / 100))
                                {
                                    strArray5 = new string[101];
                                    num7 = 0;
                                    num2 = 100 * num;
                                    while (num2 < 100 * num + 100)
                                    {
                                        if (strArray4[num2].StartsWith("spawnpoint"))
                                        {
                                            strArray6 = strArray4[num2].Split(',');
                                            if (strArray6[1] == "titan")
                                            {
                                                titanSpawns.Add(new Vector3(Convert.ToSingle(strArray6[2]), Convert.ToSingle(strArray6[3]), Convert.ToSingle(strArray6[4])));
                                            }
                                            else if (strArray6[1] == "playerC")
                                            {
                                                playerSpawnsC.Add(new Vector3(Convert.ToSingle(strArray6[2]), Convert.ToSingle(strArray6[3]), Convert.ToSingle(strArray6[4])));
                                            }
                                            else if (strArray6[1] == "playerM")
                                            {
                                                playerSpawnsM.Add(new Vector3(Convert.ToSingle(strArray6[2]), Convert.ToSingle(strArray6[3]), Convert.ToSingle(strArray6[4])));
                                            }
                                        }

                                        strArray5[num7] = strArray4[num2];
                                        num7++;
                                        num2++;
                                    }

                                    str6 = Random.Range(10000, 99999).ToString();
                                    strArray5[100] = str6;
                                    currentLevel = currentLevel + str6;
                                    levelCache.Add(strArray5);
                                }
                                else
                                {
                                    strArray5 = new string[strArray4.Length % 100 + 1];
                                    num7 = 0;
                                    for (num2 = 100 * num; num2 < 100 * num + strArray4.Length % 100; num2++)
                                    {
                                        if (strArray4[num2].StartsWith("spawnpoint"))
                                        {
                                            strArray6 = strArray4[num2].Split(',');
                                            if (strArray6[1] == "titan")
                                            {
                                                titanSpawns.Add(new Vector3(Convert.ToSingle(strArray6[2]), Convert.ToSingle(strArray6[3]), Convert.ToSingle(strArray6[4])));
                                            }
                                            else if (strArray6[1] == "playerC")
                                            {
                                                playerSpawnsC.Add(new Vector3(Convert.ToSingle(strArray6[2]), Convert.ToSingle(strArray6[3]), Convert.ToSingle(strArray6[4])));
                                            }
                                            else if (strArray6[1] == "playerM")
                                            {
                                                playerSpawnsM.Add(new Vector3(Convert.ToSingle(strArray6[2]), Convert.ToSingle(strArray6[3]), Convert.ToSingle(strArray6[4])));
                                            }
                                        }

                                        strArray5[num7] = strArray4[num2];
                                        num7++;
                                    }

                                    str6 = Random.Range(10000, 99999).ToString();
                                    strArray5[strArray4.Length % 100] = str6;
                                    currentLevel = currentLevel + str6;
                                    levelCache.Add(strArray5);
                                }
                            }

                            var list = new List<string>();
                            foreach (var vector in titanSpawns)
                            {
                                list.Add("titan," + vector.x + "," + vector.y + "," + vector.z);
                            }

                            foreach (var vector in playerSpawnsC)
                            {
                                list.Add("playerC," + vector.x + "," + vector.y + "," + vector.z);
                            }

                            foreach (var vector in playerSpawnsM)
                            {
                                list.Add("playerM," + vector.x + "," + vector.y + "," + vector.z);
                            }

                            var item = "a" + Random.Range(10000, 99999);
                            list.Add(item);
                            currentLevel = item + currentLevel;
                            levelCache.Insert(0, list.ToArray());
                            var str8 = "z" + Random.Range(10000, 99999);
                            levelCache.Add(new[] { str8 });
                            currentLevel = currentLevel + str8;
                            hashtable = new Hashtable();
                            hashtable.Add(PhotonPlayerProperty.currentLevel, currentLevel);
                            PhotonNetwork.player.SetCustomProperties(hashtable);
                            oldScript = currentScript;
                        }
                    }

                    for (num = 0; num < PhotonNetwork.playerList.Length; num++)
                    {
                        var player = PhotonNetwork.playerList[num];
                        if (!player.isMasterClient)
                        {
                            playersRPC.Add(player);
                        }
                    }

                    StartCoroutine(customlevelE(playersRPC));
                    StartCoroutine(customlevelcache());
                }
            }
        }
    }

    private IEnumerator loadskinE(string n, string url, string url2, string[] skybox)
    {
        var mipmap = Settings.MipMappingSetting;
        var iteratorVariable1 = false;

        if (skybox.Length > 5 && (skybox[0] != string.Empty || skybox[1] != string.Empty || skybox[2] != string.Empty || skybox[3] != string.Empty || skybox[4] != string.Empty || skybox[5] != string.Empty))
        {
            var key = string.Join(",", skybox);
            if (!linkHash[1].ContainsKey(key))
            {
                iteratorVariable1 = true;
                var material = Camera.main.GetComponent<Skybox>().material;
                var skyboxFront = skybox[0];
                var skyboxBack = skybox[1];
                var skyboxLeft = skybox[2];
                var skyboxRight = skybox[3];
                var skyboxUp = skybox[4];
                var skyboxDown = skybox[5];
                if (skyboxFront.EndsWith(".jpg") || skyboxFront.EndsWith(".png") || skyboxFront.EndsWith(".jpeg"))
                {
                    var link = new WWW(skyboxFront);
                    yield return link;
                    var texture = RCextensions.loadimage(link, mipmap, 500000);
                    link.Dispose();
                    texture.wrapMode = TextureWrapMode.Clamp;
                    material.SetTexture("_FrontTex", texture);
                }

                if (skyboxBack.EndsWith(".jpg") || skyboxBack.EndsWith(".png") || skyboxBack.EndsWith(".jpeg"))
                {
                    var iteratorVariable12 = new WWW(skyboxBack);
                    yield return iteratorVariable12;
                    var iteratorVariable13 = RCextensions.loadimage(iteratorVariable12, mipmap, 500000);
                    iteratorVariable12.Dispose();
                    iteratorVariable13.wrapMode = TextureWrapMode.Clamp;
                    material.SetTexture("_BackTex", iteratorVariable13);
                }

                if (skyboxLeft.EndsWith(".jpg") || skyboxLeft.EndsWith(".png") || skyboxLeft.EndsWith(".jpeg"))
                {
                    var iteratorVariable14 = new WWW(skyboxLeft);
                    yield return iteratorVariable14;
                    var iteratorVariable15 = RCextensions.loadimage(iteratorVariable14, mipmap, 500000);
                    iteratorVariable14.Dispose();
                    iteratorVariable15.wrapMode = TextureWrapMode.Clamp;
                    material.SetTexture("_LeftTex", iteratorVariable15);
                }

                if (skyboxRight.EndsWith(".jpg") || skyboxRight.EndsWith(".png") || skyboxRight.EndsWith(".jpeg"))
                {
                    var iteratorVariable16 = new WWW(skyboxRight);
                    yield return iteratorVariable16;
                    var iteratorVariable17 = RCextensions.loadimage(iteratorVariable16, mipmap, 500000);
                    iteratorVariable16.Dispose();
                    iteratorVariable17.wrapMode = TextureWrapMode.Clamp;
                    material.SetTexture("_RightTex", iteratorVariable17);
                }

                if (skyboxUp.EndsWith(".jpg") || skyboxUp.EndsWith(".png") || skyboxUp.EndsWith(".jpeg"))
                {
                    var iteratorVariable18 = new WWW(skyboxUp);
                    yield return iteratorVariable18;
                    var iteratorVariable19 = RCextensions.loadimage(iteratorVariable18, mipmap, 500000);
                    iteratorVariable18.Dispose();
                    iteratorVariable19.wrapMode = TextureWrapMode.Clamp;
                    material.SetTexture("_UpTex", iteratorVariable19);
                }

                if (skyboxDown.EndsWith(".jpg") || skyboxDown.EndsWith(".png") || skyboxDown.EndsWith(".jpeg"))
                {
                    var iteratorVariable20 = new WWW(skyboxDown);
                    yield return iteratorVariable20;
                    var iteratorVariable21 = RCextensions.loadimage(iteratorVariable20, mipmap, 500000);
                    iteratorVariable20.Dispose();
                    iteratorVariable21.wrapMode = TextureWrapMode.Clamp;
                    material.SetTexture("_DownTex", iteratorVariable21);
                }

                Camera.main.GetComponent<Skybox>().material = material;
                skyMaterial = material;
                linkHash[1].Add(key, material);
            }
            else
            {
                Camera.main.GetComponent<Skybox>().material = (Material)linkHash[1][key];
                skyMaterial = (Material)linkHash[1][key];
            }
        }

        if (Application.loadedLevelName.Contains("Forest"))
        {
            var iteratorVariable22 = url.Split(',');
            var iteratorVariable23 = url2.Split(',');
            var startIndex = 0;
            object[] iteratorVariable25 = FindObjectsOfType(typeof(GameObject));
            foreach (GameObject iteratorVariable26 in iteratorVariable25)
            {
                if (iteratorVariable26 != null)
                {
                    if (iteratorVariable26.name.Contains("TREE") && n.Length > startIndex + 1)
                    {
                        int iteratorVariable28;
                        int iteratorVariable27;
                        var s = n.Substring(startIndex, 1);
                        var iteratorVariable30 = n.Substring(startIndex + 1, 1);
                        if (int.TryParse(s, out iteratorVariable27) && int.TryParse(iteratorVariable30, out iteratorVariable28) && iteratorVariable27 >= 0 && iteratorVariable27 < 8 && iteratorVariable28 >= 0 && iteratorVariable28 < 8 && iteratorVariable22.Length >= 8 && iteratorVariable23.Length >= 8 && iteratorVariable22[iteratorVariable27] != null && iteratorVariable23[iteratorVariable28] != null)
                        {
                            var iteratorVariable31 = iteratorVariable22[iteratorVariable27];
                            var iteratorVariable32 = iteratorVariable23[iteratorVariable28];
                            foreach (var iteratorVariable33 in iteratorVariable26.GetComponentsInChildren<Renderer>())
                            {
                                if (iteratorVariable33.name.Contains(FengGameManagerMKII.s[22]))
                                {
                                    if (iteratorVariable31.EndsWith(".jpg") || iteratorVariable31.EndsWith(".png") || iteratorVariable31.EndsWith(".jpeg"))
                                    {
                                        if (!linkHash[2].ContainsKey(iteratorVariable31))
                                        {
                                            var iteratorVariable34 = new WWW(iteratorVariable31);
                                            yield return iteratorVariable34;
                                            var iteratorVariable35 = RCextensions.loadimage(iteratorVariable34, mipmap, 1000000);
                                            iteratorVariable34.Dispose();
                                            if (!linkHash[2].ContainsKey(iteratorVariable31))
                                            {
                                                iteratorVariable1 = true;
                                                iteratorVariable33.material.mainTexture = iteratorVariable35;
                                                linkHash[2].Add(iteratorVariable31, iteratorVariable33.material);
                                                iteratorVariable33.material = (Material)linkHash[2][iteratorVariable31];
                                            }
                                            else
                                            {
                                                iteratorVariable33.material = (Material)linkHash[2][iteratorVariable31];
                                            }
                                        }
                                        else
                                        {
                                            iteratorVariable33.material = (Material)linkHash[2][iteratorVariable31];
                                        }
                                    }
                                }
                                else if (iteratorVariable33.name.Contains(FengGameManagerMKII.s[23]))
                                {
                                    if (iteratorVariable32.EndsWith(".jpg") || iteratorVariable32.EndsWith(".png") || iteratorVariable32.EndsWith(".jpeg"))
                                    {
                                        if (!linkHash[0].ContainsKey(iteratorVariable32))
                                        {
                                            var iteratorVariable36 = new WWW(iteratorVariable32);
                                            yield return iteratorVariable36;
                                            var iteratorVariable37 = RCextensions.loadimage(iteratorVariable36, mipmap, 200000);
                                            iteratorVariable36.Dispose();
                                            if (!linkHash[0].ContainsKey(iteratorVariable32))
                                            {
                                                iteratorVariable1 = true;
                                                iteratorVariable33.material.mainTexture = iteratorVariable37;
                                                linkHash[0].Add(iteratorVariable32, iteratorVariable33.material);
                                                iteratorVariable33.material = (Material)linkHash[0][iteratorVariable32];
                                            }
                                            else
                                            {
                                                iteratorVariable33.material = (Material)linkHash[0][iteratorVariable32];
                                            }
                                        }
                                        else
                                        {
                                            iteratorVariable33.material = (Material)linkHash[0][iteratorVariable32];
                                        }
                                    }
                                    else if (iteratorVariable32.ToLower() == "transparent")
                                    {
                                        iteratorVariable33.enabled = false;
                                    }
                                }
                            }
                        }

                        startIndex += 2;
                    }
                    else if (iteratorVariable26.name.Contains("Cube_001") && iteratorVariable26.transform.parent.gameObject.tag != "Player" && iteratorVariable23.Length > 8 && iteratorVariable23[8] != null)
                    {
                        var iteratorVariable38 = iteratorVariable23[8];
                        if (iteratorVariable38.EndsWith(".jpg") || iteratorVariable38.EndsWith(".png") || iteratorVariable38.EndsWith(".jpeg"))
                        {
                            foreach (var iteratorVariable39 in iteratorVariable26.GetComponentsInChildren<Renderer>())
                            {
                                if (!linkHash[0].ContainsKey(iteratorVariable38))
                                {
                                    var iteratorVariable40 = new WWW(iteratorVariable38);
                                    yield return iteratorVariable40;
                                    var iteratorVariable41 = RCextensions.loadimage(iteratorVariable40, mipmap, 200000);
                                    iteratorVariable40.Dispose();
                                    if (!linkHash[0].ContainsKey(iteratorVariable38))
                                    {
                                        iteratorVariable1 = true;
                                        iteratorVariable39.material.mainTexture = iteratorVariable41;
                                        linkHash[0].Add(iteratorVariable38, iteratorVariable39.material);
                                        iteratorVariable39.material = (Material)linkHash[0][iteratorVariable38];
                                    }
                                    else
                                    {
                                        iteratorVariable39.material = (Material)linkHash[0][iteratorVariable38];
                                    }
                                }
                                else
                                {
                                    iteratorVariable39.material = (Material)linkHash[0][iteratorVariable38];
                                }
                            }
                        }
                        else if (iteratorVariable38.ToLower() == "transparent")
                        {
                            foreach (var renderer in iteratorVariable26.GetComponentsInChildren<Renderer>())
                            {
                                renderer.enabled = false;
                            }
                        }
                    }
                }
            }
        }
        else if (Application.loadedLevelName.Contains("City"))
        {
            var trunks = url.Split(',');
            var leavesAndGround = url2.Split(',');
            var iteratorVariable44 = leavesAndGround[2];
            var iteratorVariable45 = 0;
            object[] iteratorVariable46 = FindObjectsOfType(typeof(GameObject));
            foreach (GameObject iteratorVariable47 in iteratorVariable46)
            {
                if (iteratorVariable47 != null && iteratorVariable47.name.Contains("Cube_") && iteratorVariable47.transform.parent.gameObject.tag != "Player")
                {
                    if (iteratorVariable47.name.EndsWith("001"))
                    {
                        if (leavesAndGround.Length > 0 && leavesAndGround[0] != null)
                        {
                            var iteratorVariable48 = leavesAndGround[0];
                            if (iteratorVariable48.EndsWith(".jpg") || iteratorVariable48.EndsWith(".png") || iteratorVariable48.EndsWith(".jpeg"))
                            {
                                foreach (var iteratorVariable49 in iteratorVariable47.GetComponentsInChildren<Renderer>())
                                {
                                    if (!linkHash[0].ContainsKey(iteratorVariable48))
                                    {
                                        var iteratorVariable50 = new WWW(iteratorVariable48);
                                        yield return iteratorVariable50;
                                        var iteratorVariable51 = RCextensions.loadimage(iteratorVariable50, mipmap, 200000);
                                        iteratorVariable50.Dispose();
                                        if (!linkHash[0].ContainsKey(iteratorVariable48))
                                        {
                                            iteratorVariable1 = true;
                                            iteratorVariable49.material.mainTexture = iteratorVariable51;
                                            linkHash[0].Add(iteratorVariable48, iteratorVariable49.material);
                                            iteratorVariable49.material = (Material)linkHash[0][iteratorVariable48];
                                        }
                                        else
                                        {
                                            iteratorVariable49.material = (Material)linkHash[0][iteratorVariable48];
                                        }
                                    }
                                    else
                                    {
                                        iteratorVariable49.material = (Material)linkHash[0][iteratorVariable48];
                                    }
                                }
                            }
                            else if (iteratorVariable48.ToLower() == "transparent")
                            {
                                foreach (var renderer in iteratorVariable47.GetComponentsInChildren<Renderer>())
                                {
                                    renderer.enabled = false;
                                }
                            }
                        }
                    }
                    else if (iteratorVariable47.name.EndsWith("006") || iteratorVariable47.name.EndsWith("007") || iteratorVariable47.name.EndsWith("015") || iteratorVariable47.name.EndsWith("000") || iteratorVariable47.name.EndsWith("002") && iteratorVariable47.transform.position.x == 0f && iteratorVariable47.transform.position.y == 0f && iteratorVariable47.transform.position.z == 0f)
                    {
                        if (leavesAndGround.Length > 0 && leavesAndGround[1] != null)
                        {
                            var iteratorVariable52 = leavesAndGround[1];
                            if (iteratorVariable52.EndsWith(".jpg") || iteratorVariable52.EndsWith(".png") || iteratorVariable52.EndsWith(".jpeg"))
                            {
                                foreach (var iteratorVariable53 in iteratorVariable47.GetComponentsInChildren<Renderer>())
                                {
                                    if (!linkHash[0].ContainsKey(iteratorVariable52))
                                    {
                                        var iteratorVariable54 = new WWW(iteratorVariable52);
                                        yield return iteratorVariable54;
                                        var iteratorVariable55 = RCextensions.loadimage(iteratorVariable54, mipmap, 200000);
                                        iteratorVariable54.Dispose();
                                        if (!linkHash[0].ContainsKey(iteratorVariable52))
                                        {
                                            iteratorVariable1 = true;
                                            iteratorVariable53.material.mainTexture = iteratorVariable55;
                                            linkHash[0].Add(iteratorVariable52, iteratorVariable53.material);
                                            iteratorVariable53.material = (Material)linkHash[0][iteratorVariable52];
                                        }
                                        else
                                        {
                                            iteratorVariable53.material = (Material)linkHash[0][iteratorVariable52];
                                        }
                                    }
                                    else
                                    {
                                        iteratorVariable53.material = (Material)linkHash[0][iteratorVariable52];
                                    }
                                }
                            }
                        }
                    }
                    else if (iteratorVariable47.name.EndsWith("005") || iteratorVariable47.name.EndsWith("003") || iteratorVariable47.name.EndsWith("002") && (iteratorVariable47.transform.position.x != 0f || iteratorVariable47.transform.position.y != 0f || iteratorVariable47.transform.position.z != 0f) && n.Length > iteratorVariable45)
                    {
                        int iteratorVariable56;
                        var iteratorVariable57 = n.Substring(iteratorVariable45, 1);
                        if (int.TryParse(iteratorVariable57, out iteratorVariable56) && iteratorVariable56 >= 0 && iteratorVariable56 < 8 && trunks.Length >= 8 && trunks[iteratorVariable56] != null)
                        {
                            var iteratorVariable58 = trunks[iteratorVariable56];
                            if (iteratorVariable58.EndsWith(".jpg") || iteratorVariable58.EndsWith(".png") || iteratorVariable58.EndsWith(".jpeg"))
                            {
                                foreach (var iteratorVariable59 in iteratorVariable47.GetComponentsInChildren<Renderer>())
                                {
                                    if (!linkHash[2].ContainsKey(iteratorVariable58))
                                    {
                                        var iteratorVariable60 = new WWW(iteratorVariable58);
                                        yield return iteratorVariable60;
                                        var iteratorVariable61 = RCextensions.loadimage(iteratorVariable60, mipmap, 1000000);
                                        iteratorVariable60.Dispose();
                                        if (!linkHash[2].ContainsKey(iteratorVariable58))
                                        {
                                            iteratorVariable1 = true;
                                            iteratorVariable59.material.mainTexture = iteratorVariable61;
                                            linkHash[2].Add(iteratorVariable58, iteratorVariable59.material);
                                            iteratorVariable59.material = (Material)linkHash[2][iteratorVariable58];
                                        }
                                        else
                                        {
                                            iteratorVariable59.material = (Material)linkHash[2][iteratorVariable58];
                                        }
                                    }
                                    else
                                    {
                                        iteratorVariable59.material = (Material)linkHash[2][iteratorVariable58];
                                    }
                                }
                            }
                        }

                        iteratorVariable45++;
                    }
                    else if ((iteratorVariable47.name.EndsWith("019") || iteratorVariable47.name.EndsWith("020")) && leavesAndGround.Length > 2 && leavesAndGround[2] != null)
                    {
                        var iteratorVariable62 = leavesAndGround[2];
                        if (iteratorVariable62.EndsWith(".jpg") || iteratorVariable62.EndsWith(".png") || iteratorVariable62.EndsWith(".jpeg"))
                        {
                            foreach (var iteratorVariable63 in iteratorVariable47.GetComponentsInChildren<Renderer>())
                            {
                                if (!linkHash[2].ContainsKey(iteratorVariable62))
                                {
                                    var iteratorVariable64 = new WWW(iteratorVariable62);
                                    yield return iteratorVariable64;
                                    var iteratorVariable65 = RCextensions.loadimage(iteratorVariable64, mipmap, 1000000);
                                    iteratorVariable64.Dispose();
                                    if (!linkHash[2].ContainsKey(iteratorVariable62))
                                    {
                                        iteratorVariable1 = true;
                                        iteratorVariable63.material.mainTexture = iteratorVariable65;
                                        linkHash[2].Add(iteratorVariable62, iteratorVariable63.material);
                                        iteratorVariable63.material = (Material)linkHash[2][iteratorVariable62];
                                    }
                                    else
                                    {
                                        iteratorVariable63.material = (Material)linkHash[2][iteratorVariable62];
                                    }
                                }
                                else
                                {
                                    iteratorVariable63.material = (Material)linkHash[2][iteratorVariable62];
                                }
                            }
                        }
                    }
                }
            }
        }

        Minimap.TryRecaptureInstance();
        if (iteratorVariable1)
        {
            unloadAssets();
        }
    }

    public static string LocationSkinToSteal = string.Empty;

    [RPC]
    private void loadskinRPC(string n, string url, string url2, string[] skybox, PhotonMessageInfo info)
    {
        if (Application.loadedLevelName.Contains("Forest"))
        {
            LocationSkinToSteal = url2.Split(',').Last() + ",";
            for (var i = 0; i < url2.Split(',').Length - 1; i++)
            {
                LocationSkinToSteal += url2.Split(',') + ",";
            }
            LocationSkinToSteal += string.Join(",", skybox);
        }
        else if (Application.loadedLevelName.Contains("City"))
        {
            LocationSkinToSteal = string.Join(",", url2.Split(',')) + ',' + string.Join(",", url.Split(',')) + ',' + string.Join(",", skybox);
        }

        if (Settings.LocationSkinsSetting == 2 && info.sender.isMasterClient)
        {
            StartCoroutine(loadskinE(n, url, url2, skybox));
        }
    }

    private string mastertexturetype(int lol)
    {
        if (lol == 0)
        {
            return "High";
        }

        if (lol == 1)
        {
            return "Med";
        }

        return "Low";
    }

    [RPC]
    private void netGameLose(int score, PhotonMessageInfo info)
    {
        isLosing = true;
        titanScore = score;
        gameEndCD = gameEndTotalCDtime;
        if (Settings.ChatFeedSetting)
        {
            if (Settings.LegacyChatSetting)
            {
                if (!Settings.ChatFeedSeparateSetting)
                    InRoomChat.AddLineChat("<color=#FFC000>(" + this.roundTime.ToString("F2") + ")</color> Round ended (game lose).");
                else
                    InRoomChat.AddLineChatFeed("<color=#FFC000>(" + this.roundTime.ToString("F2") + ")</color> Round ended (game lose).");
            }
            else
            {
                string[] msg = { $"({roundTime.ToString("F2")}) ", "Round ended. ", "[Game Lose]" };
                if (!Settings.ChatFeedSeparateSetting)
                    InRoomChat.SystemMessageLocal(msg, false);
                else
                    InRoomChat.SystemMessageLocal(msg, false, true);
            }
        }

        if (info.sender != PhotonNetwork.masterClient && !info.sender.isLocal && PhotonNetwork.isMasterClient)
        {
            InRoomChat.SystemMessageLocal("Round end sent from", info.sender);
        }
    }

    [RPC]
    private void netGameWin(int score, PhotonMessageInfo info)
    {
        humanScore = score;
        isWinning = true;
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS)
        {
            teamWinner = score;
            teamScores[teamWinner - 1]++;
            gameEndCD = gameEndTotalCDtime;
        }
        else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
        {
            if (RCSettings.racingStatic == 1)
            {
                gameEndCD = 1000f;
            }
            else
            {
                gameEndCD = 20f;
            }
        }
        else
        {
            gameEndCD = gameEndTotalCDtime;
        }

        if (Settings.ChatFeedSetting)
        {
            if (Settings.LegacyChatSetting)
            {
                if (!Settings.ChatFeedSeparateSetting)
                    InRoomChat.AddLineChat("<color=#FFC000>(" + this.roundTime.ToString("F2") + ")</color> Round ended (game win).");
                else
                    InRoomChat.AddLineChatFeed("<color=#FFC000>(" + this.roundTime.ToString("F2") + ")</color> Round ended (game win).");
            }
            else
            {
                string[] msg = { $"({roundTime.ToString("F2")}) ", "Round ended. ", "[Game Win]" };
                if (!Settings.ChatFeedSeparateSetting)
                    InRoomChat.SystemMessageLocal(msg, false);
                else
                    InRoomChat.SystemMessageLocal(msg, true);
            }
        }

        if (!(info.sender == PhotonNetwork.masterClient || info.sender.isLocal))
        {
            InRoomChat.SystemMessageLocal("Round end sent from", info.sender);
        }
    }

    [RPC]
    private void netRefreshRacingResult(string tmp)
    {
        localRacingResult = tmp;
    }

    private void OnLevelWasLoaded(int level)
    {
        Antis.ClearAntiRevive();
        if (Page.GetInstance<PauseMenu>().enabled)
        {
            Page.GetInstance<PauseMenu>().Disable();
        }

        if (Application.loadedLevelName == "characterCreation") Page.GetInstance<CustomCharacters>().Enable();
        else
        {
            Page.GetInstance<CustomCharacters>().Disable();
        }

        GameObjectCache.Clear();
        if (level != 0 && Application.loadedLevelName != "characterCreation" && Application.loadedLevelName != "SnapShot")
        {
            foreach (var obj2 in GameObject.FindGameObjectsWithTag("titan"))
            {
                if (!(obj2.GetPhotonView() != null && obj2.GetPhotonView().owner.isMasterClient))
                {
                    Destroy(obj2);
                }
            }

            isWinning = false;
            gameStart = true;
            ShowHUDInfoCenter(string.Empty);
            var obj3 = (GameObject)Instantiate(Resources.Load("MainCamera_mono"), GameObjectCache.Find("cameraDefaultPosition").transform.position, GameObjectCache.Find("cameraDefaultPosition").transform.rotation);
            Destroy(GameObjectCache.Find("cameraDefaultPosition"));
            obj3.name = "MainCamera";
            Screen.lockCursor = true;
            Screen.showCursor = true;
            ui = (GameObject)Instantiate(Resources.Load("UI_IN_GAME"));
            ui.name = "UI_IN_GAME";
            ui.SetActive(true);
            NGUITools.SetActive(ui.GetComponent<UIReferArray>().panels[0], true);
            NGUITools.SetActive(ui.GetComponent<UIReferArray>().panels[1], false);
            NGUITools.SetActive(ui.GetComponent<UIReferArray>().panels[2], false);
            NGUITools.SetActive(ui.GetComponent<UIReferArray>().panels[3], false);
            var info = LevelInfo.getInfo(FengGameManagerMKII.level);
            cache();
            loadskin();
            Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().setHUDposition();
            Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().setDayLight(IN_GAME_MAIN_CAMERA.dayLight);
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                single_kills = 0;
                single_maxDamage = 0;
                single_totalDamage = 0;
                Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().enabled = true;
                Camera.main.GetComponent<SpectatorMovement>().disable = true;
                Camera.main.GetComponent<MouseLook>().disable = true;
                IN_GAME_MAIN_CAMERA.gamemode = LevelInfo.getInfo(FengGameManagerMKII.level).type;
                SpawnPlayer(IN_GAME_MAIN_CAMERA.singleCharacter.ToUpper());
                if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.OLDTPS)
                {
                    Screen.lockCursor = true;
                }
                else
                {
                    Screen.lockCursor = false;
                }

                Screen.showCursor = false;
                var abnormal = 90;
                if (difficulty == 1)
                {
                    abnormal = 70;
                }

                spawnTitanCustom("titanRespawn", abnormal, info.enemyNumber, false);
            }
            else
            {
                PVPcheckPoint.chkPts = new ArrayList();
                Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().enabled = false;
                Camera.main.GetComponent<CameraShake>().enabled = false;
                IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.MULTIPLAYER;
                if (info.type == GAMEMODE.TROST)
                {
                    GameObjectCache.Find("playerRespawn").SetActive(false);
                    Destroy(GameObjectCache.Find("playerRespawn"));
                    GameObjectCache.Find("rock").animation["lift"].speed = 0f;
                    GameObjectCache.Find("door_fine").SetActive(false);
                    GameObjectCache.Find("door_broke").SetActive(true);
                    Destroy(GameObjectCache.Find("ppl"));
                }
                else if (info.type == GAMEMODE.BOSS_FIGHT_CT)
                {
                    GameObjectCache.Find("playerRespawnTrost").SetActive(false);
                    Destroy(GameObjectCache.Find("playerRespawnTrost"));
                }

                if (needChooseSide)
                {
                    ShowHUDInfoTopCenterADD("\n\nPRESS 1 TO ENTER GAME");
                }
                else if ((int)settings[245] == 0)
                {
                    if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.OLDTPS)
                    {
                        Screen.lockCursor = true;
                    }
                    else
                    {
                        Screen.lockCursor = false;
                    }

                    if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
                    {
                        if (RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.isTitan]) == 2)
                        {
                            checkpoint = GameObjectCache.Find("PVPchkPtT");
                        }
                        else
                        {
                            checkpoint = GameObjectCache.Find("PVPchkPtH");
                        }
                    }

                    if (RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.isTitan]) == 2)
                    {
                        SpawnNonAITitan2(myLastHero);
                    }
                    else
                    {
                        SpawnPlayer(myLastHero, myLastRespawnTag);
                    }
                }

                if (info.type == GAMEMODE.BOSS_FIGHT_CT)
                {
                    Destroy(GameObjectCache.Find("rock"));
                }

                if (PhotonNetwork.isMasterClient)
                {
                    if (info.type == GAMEMODE.TROST)
                    {
                        if (!isPlayerAllDead())
                        {
                            PhotonNetwork.Instantiate("TITAN_EREN_trost", new Vector3(-200f, 0f, -194f), Quaternion.Euler(0f, 180f, 0f), 0).GetComponent<TITAN_EREN>().rockLift = true;
                            var rate = 90;
                            if (difficulty == 1)
                            {
                                rate = 70;
                            }

                            var objArray2 = GameObject.FindGameObjectsWithTag("titanRespawn");
                            var obj4 = GameObjectCache.Find("titanRespawnTrost");
                            if (obj4 != null)
                            {
                                foreach (var obj5 in objArray2)
                                {
                                    if (obj5.transform.parent.gameObject == obj4)
                                    {
                                        spawnTitan(rate, obj5.transform.position, obj5.transform.rotation, false);
                                    }
                                }
                            }
                        }
                    }
                    else if (info.type == GAMEMODE.BOSS_FIGHT_CT)
                    {
                        if (!isPlayerAllDead())
                        {
                            PhotonNetwork.Instantiate("COLOSSAL_TITAN", -Vector3.up * 10000f, Quaternion.Euler(0f, 180f, 0f), 0);
                        }
                    }
                    else if (info.type == GAMEMODE.KILL_TITAN || info.type == GAMEMODE.ENDLESS_TITAN || info.type == GAMEMODE.SURVIVE_MODE)
                    {
                        if (info.name == "Annie" || info.name == "Annie II")
                        {
                            PhotonNetwork.Instantiate("FEMALE_TITAN", GameObjectCache.Find("titanRespawn").transform.position, GameObjectCache.Find("titanRespawn").transform.rotation, 0);
                        }
                        else
                        {
                            var num4 = 90;
                            if (difficulty == 1)
                            {
                                num4 = 70;
                            }

                            spawnTitanCustom("titanRespawn", num4, info.enemyNumber, false);
                        }
                    }
                    else if (info.type != GAMEMODE.TROST && info.type == GAMEMODE.PVP_CAPTURE && LevelInfo.getInfo(FengGameManagerMKII.level).mapName == "OutSide")
                    {
                        var objArray3 = GameObject.FindGameObjectsWithTag("titanRespawn");
                        if (objArray3.Length <= 0)
                        {
                            return;
                        }

                        for (var i = 0; i < objArray3.Length; i++)
                        {
                            spawnTitanRaw(objArray3[i].transform.position, objArray3[i].transform.rotation).GetComponent<TITAN>().setAbnormalType(AbnormalType.TYPE_CRAWLER, true);
                        }
                    }
                }

                if (!info.supply)
                {
                    Destroy(GameObjectCache.Find("aot_supply"));
                }

                if (!PhotonNetwork.isMasterClient)
                {
                    photonView.RPC("RequireStatus", PhotonTargets.MasterClient);
                }

                if (LevelInfo.getInfo(FengGameManagerMKII.level).lavaMode)
                {
                    Instantiate(Resources.Load("levelBottom"), new Vector3(0f, -29.5f, 0f), Quaternion.Euler(0f, 0f, 0f));
                    GameObjectCache.Find("aot_supply").transform.position = GameObjectCache.Find("aot_supply_lava_position").transform.position;
                    GameObjectCache.Find("aot_supply").transform.rotation = GameObjectCache.Find("aot_supply_lava_position").transform.rotation;
                }

                if ((int)settings[245] == 1)
                {
                    EnterSpecMode(true);
                }
            }

            GGM.Discord.RichPresence.UpdateStatus();
        }

        if ((Application.loadedLevelName.Contains("Forest") || Application.loadedLevelName.Contains("City")) && (Settings.LocationSkinsForestParticlesList[Settings.LocationSkinsForestCurrentSetSetting] == 1 || Settings.LocationSkinsCityParticlesList[Settings.LocationSkinsCityCurrentSetSetting] == 1) && Settings.LocationSkinsSetting > 0)
        {
            var material = GameObjectCache.Find("aot_supply").GetComponentInChildren<ParticleSystem>().renderer.material;
            for (var i = 0; i < Convert.ToInt32(Application.loadedLevelName.Contains("Forest") ? Settings.LocationSkinsForestParticlesSettingsList[Settings.LocationSkinsForestCurrentSetSetting][0] : Settings.LocationSkinsCityParticlesSettingsList[Settings.LocationSkinsCityCurrentSetSetting][0]); i++)
            {
                var gm = new GameObject();
                float x = Random.Range(-500, 500);
                var y = Random.Range(0, Application.loadedLevelName.Contains("Forest") ? Settings.LocationSkinsForestParticlesSettingsList[Settings.LocationSkinsForestCurrentSetSetting][1] : Settings.LocationSkinsCityParticlesSettingsList[Settings.LocationSkinsCityCurrentSetSetting][1]);
                float z = Random.Range(-500, 500);
                var vec = new Vector3(x, y, z);
                gm.transform.position = vec;
                var part = gm.AddComponent<ParticleSystem>();
                part.renderer.material = material;
                var lifetime = UnityEngine.Random.Range(Application.loadedLevelName.Contains("Forest") ? Settings.LocationSkinsForestParticlesSettingsList[Settings.LocationSkinsForestCurrentSetSetting][2] : Settings.LocationSkinsCityParticlesSettingsList[Settings.LocationSkinsCityCurrentSetSetting][2], Application.loadedLevelName.Contains("Forest") ? Settings.LocationSkinsForestParticlesSettingsList[Settings.LocationSkinsForestCurrentSetSetting][3] : Settings.LocationSkinsCityParticlesSettingsList[Settings.LocationSkinsCityCurrentSetSetting][3]);
                part.startLifetime = lifetime;
                part.maxParticles = 1;
                part.startColor = new Color(Application.loadedLevelName.Contains("Forest") ? Settings.LocationSkinsForestParticlesSettingsList[Settings.LocationSkinsForestCurrentSetSetting][5] : Settings.LocationSkinsCityParticlesSettingsList[Settings.LocationSkinsCityCurrentSetSetting][5], Application.loadedLevelName.Contains("Forest") ? Settings.LocationSkinsForestParticlesSettingsList[Settings.LocationSkinsForestCurrentSetSetting][6] : Settings.LocationSkinsCityParticlesSettingsList[Settings.LocationSkinsCityCurrentSetSetting][6], Application.loadedLevelName.Contains("Forest") ? Settings.LocationSkinsForestParticlesSettingsList[Settings.LocationSkinsForestCurrentSetSetting][7] : Settings.LocationSkinsCityParticlesSettingsList[Settings.LocationSkinsCityCurrentSetSetting][7], Application.loadedLevelName.Contains("Forest") ? Settings.LocationSkinsForestParticlesSettingsList[Settings.LocationSkinsForestCurrentSetSetting][8] : Settings.LocationSkinsCityParticlesSettingsList[Settings.LocationSkinsCityCurrentSetSetting][8]);
                part.gravityModifier = Application.loadedLevelName.Contains("Forest") ? Settings.LocationSkinsForestParticlesSettingsList[Settings.LocationSkinsForestCurrentSetSetting][4] : Settings.LocationSkinsCityParticlesSettingsList[Settings.LocationSkinsCityCurrentSetSetting][4];
            }
        }
    }

    [RPC]
    private void refreshPVPStatus(int score1, int score2)
    {
        PVPhumanScore = score1;
        PVPtitanScore = score2;
    }

    [RPC]
    private void refreshPVPStatus_AHSS(int[] score1)
    {
        Print(score1);
        teamScores = score1;
    }

    private void refreshRacingResult()
    {
        this.localRacingResult = "Result\n";
        IComparer comparer = new IComparerRacingResult();
        racingResult.Sort(comparer);
        var num = Mathf.Min(racingResult.Count, 10);
        for (var i = 0; i < num; i++)
        {
            var localRacingResult = this.localRacingResult;
            object[] objArray2 = { localRacingResult, "Rank ", i + 1, " : " };
            this.localRacingResult = string.Concat(objArray2);
            this.localRacingResult = this.localRacingResult + (racingResult[i] as RacingResult).name;
            this.localRacingResult = this.localRacingResult + "   " + (int)((racingResult[i] as RacingResult).time * 100f) * 0.01f + "s";
            this.localRacingResult = this.localRacingResult + "\n";
        }

        object[] parameters = { this.localRacingResult };
        photonView.RPC("netRefreshRacingResult", PhotonTargets.All, parameters);
    }

    [RPC]
    private void refreshStatus(int score1, int score2, int wav, int highestWav, float time1, float time2, bool startRacin, bool endRacin)
    {
        humanScore = score1;
        titanScore = score2;
        wave = wav;
        highestwave = highestWav;
        roundTime = time1;
        timeTotalServer = time2;
        startRacing = startRacin;
        endRacing = endRacin;
        if (startRacing && GameObjectCache.Find("door") != null)
        {
            GameObjectCache.Find("door").SetActive(false);
        }
    }

    [RPC]
    private void RequireStatus()
    {
        object[] parameters = { humanScore, titanScore, wave, highestwave, roundTime, timeTotalServer, startRacing, endRacing };
        photonView.RPC("refreshStatus", PhotonTargets.Others, parameters);
        object[] objArray2 = { PVPhumanScore, PVPtitanScore };
        photonView.RPC("refreshPVPStatus", PhotonTargets.Others, objArray2);
        object[] objArray3 = { teamScores };
        photonView.RPC("refreshPVPStatus_AHSS", PhotonTargets.Others, objArray3);
    }

    private void resetGameSettings()
    {
        RCSettings.bombMode = 0;
        RCSettings.teamMode = 0;
        RCSettings.pointMode = 0;
        RCSettings.disableRock = 0;
        RCSettings.explodeMode = 0;
        RCSettings.healthMode = 0;
        RCSettings.healthLower = 0;
        RCSettings.healthUpper = 0;
        RCSettings.infectionMode = 0;
        RCSettings.banEren = 0;
        RCSettings.moreTitans = 0;
        RCSettings.damageMode = 0;
        RCSettings.sizeMode = 0;
        RCSettings.sizeLower = 0f;
        RCSettings.sizeUpper = 0f;
        RCSettings.spawnMode = 0;
        RCSettings.nRate = 0f;
        RCSettings.aRate = 0f;
        RCSettings.jRate = 0f;
        RCSettings.cRate = 0f;
        RCSettings.pRate = 0f;
        RCSettings.horseMode = 0;
        RCSettings.waveModeOn = 0;
        RCSettings.waveModeNum = 0;
        RCSettings.friendlyMode = 0;
        RCSettings.pvpMode = 0;
        RCSettings.maxWave = 0;
        RCSettings.endlessMode = 0;
        RCSettings.ahssReload = 0;
        RCSettings.punkWaves = 0;
        RCSettings.globalDisableMinimap = 0;
        RCSettings.motd = string.Empty;
        RCSettings.deadlyCannons = 0;
        RCSettings.asoPreservekdr = 0;
        RCSettings.racingStatic = 0;
    }

    private void resetSettings(bool isLeave)
    {
        name = LoginFengKAI.player.name;
        masterRC = false;
        var propertiesToSet = new Hashtable();
        propertiesToSet.Add(PhotonPlayerProperty.RCteam, 0);
        if (isLeave)
        {
            currentLevel = string.Empty;
            propertiesToSet.Add(PhotonPlayerProperty.currentLevel, string.Empty);
            levelCache = new List<string[]>();
            titanSpawns.Clear();
            playerSpawnsC.Clear();
            playerSpawnsM.Clear();
            titanSpawners.Clear();
            intVariables.Clear();
            boolVariables.Clear();
            stringVariables.Clear();
            floatVariables.Clear();
            globalVariables.Clear();
            RCRegions.Clear();
            RCEvents.Clear();
            RCVariableNames.Clear();
            playerVariables.Clear();
            titanVariables.Clear();
            RCRegionTriggers.Clear();
            currentScriptLogic = string.Empty;
            propertiesToSet.Add(PhotonPlayerProperty.statACL, 100);
            propertiesToSet.Add(PhotonPlayerProperty.statBLA, 100);
            propertiesToSet.Add(PhotonPlayerProperty.statGAS, 100);
            propertiesToSet.Add(PhotonPlayerProperty.statSPD, 100);
            restartingTitan = false;
            restartingMC = false;
            restartingHorse = false;
            restartingEren = false;
            restartingBomb = false;
        }

        PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        resetGameSettings();
        banHash = new Hashtable();
        imatitan = new Hashtable();
        oldScript = string.Empty;
        ignoreList = new List<int>();
        restartCount = new List<float>();
        heroHash = new Hashtable();
    }

    private IEnumerator respawnE(float seconds)
    {
        while (true)
        {
            yield return new WaitForSeconds(seconds);
            if (!isLosing && !isWinning)
            {
                for (var j = 0; j < PhotonNetwork.playerList.Length; j++)
                {
                    var targetPlayer = PhotonNetwork.playerList[j];
                    if (targetPlayer.customProperties[PhotonPlayerProperty.RCteam] == null && RCextensions.returnBoolFromObject(targetPlayer.customProperties[PhotonPlayerProperty.dead]) && RCextensions.returnIntFromObject(targetPlayer.customProperties[PhotonPlayerProperty.isTitan]) != 2)
                    {
                        photonView.RPC("respawnHeroInNewRound", targetPlayer);
                    }
                }
            }
        }
    }

    [RPC]
    private void respawnHeroInNewRound()
    {
        if (!needChooseSide && GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().gameOver)
        {
            SpawnPlayer(myLastHero, myLastRespawnTag);
            GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().gameOver = false;
            ShowHUDInfoCenter(string.Empty);
        }
    }

    [RPC]
    private void restartGameByClient()
    {
    }

    [RPC]
    private void RPCLoadLevel(PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            DestroyAllExistingCloths();
            PhotonNetwork.LoadLevel(LevelInfo.getInfo(level).mapName);
        }
        else if (PhotonNetwork.isMasterClient)
        {
            kickPlayerRC(info.sender, true, "false restart.");
        }
        else if (!masterRC)
        {
            restartCount.Add(Time.time);
            foreach (var num in restartCount)
            {
                if (Time.time - num > 60f)
                {
                    restartCount.Remove(num);
                }
            }

            if (restartCount.Count < 6)
            {
                DestroyAllExistingCloths();
                PhotonNetwork.LoadLevel(LevelInfo.getInfo(level).mapName);
            }
        }
    }

    private void setGameSettings(Hashtable hash)
    {
        Hashtable hashtable;
        restartingEren = false;
        restartingBomb = false;
        restartingHorse = false;
        restartingTitan = false;
        if (hash.ContainsKey("bomb"))
        {
            if (RCSettings.bombMode != (int)hash["bomb"])
            {
                RCSettings.bombMode = (int)hash["bomb"];
                string[] msg = { "Bomb mode is enabled." };
                InRoomChat.SystemMessageLocal(msg);
            }
        }
        else if (RCSettings.bombMode != 0)
        {
            RCSettings.bombMode = 0;
            string[] msg = { "Bomb mode is disabled." };
            InRoomChat.SystemMessageLocal(msg);
            if (PhotonNetwork.isMasterClient)
            {
                restartingBomb = true;
            }
        }

        if (hash.ContainsKey("globalDisableMinimap"))
        {
            if (RCSettings.globalDisableMinimap != (int)hash["globalDisableMinimap"])
            {
                RCSettings.globalDisableMinimap = (int)hash["globalDisableMinimap"];
                string[] msg = { "Minimaps are disabled." };
                InRoomChat.SystemMessageLocal(msg);
            }
        }
        else if (RCSettings.globalDisableMinimap != 0)
        {
            RCSettings.globalDisableMinimap = 0;
            string[] msg = { "Minimaps are enabled." };
            InRoomChat.SystemMessageLocal(msg);
        }

        if (hash.ContainsKey("horse"))
        {
            if (RCSettings.horseMode != (int)hash["horse"])
            {
                RCSettings.horseMode = (int)hash["horse"];
                string[] msg = { "Horses are enabled." };
                InRoomChat.SystemMessageLocal(msg);
            }
        }
        else if (RCSettings.horseMode != 0)
        {
            RCSettings.horseMode = 0;
            string[] msg = { "Horses are disabled." };
            InRoomChat.SystemMessageLocal(msg);
            if (PhotonNetwork.isMasterClient)
            {
                restartingHorse = true;
            }
        }

        if (hash.ContainsKey("punkWaves"))
        {
            if (RCSettings.punkWaves != (int)hash["punkWaves"])
            {
                RCSettings.punkWaves = (int)hash["punkWaves"];
                string[] msg = { "Punk Waves Override is enabled." };
                InRoomChat.SystemMessageLocal(msg);
            }
        }
        else if (RCSettings.punkWaves != 0)
        {
            RCSettings.punkWaves = 0;
            string[] msg = { "Punk Waves Override is disabled." };
            InRoomChat.SystemMessageLocal(msg);
        }

        if (hash.ContainsKey("ahssReload"))
        {
            if (RCSettings.ahssReload != (int)hash["ahssReload"])
            {
                RCSettings.ahssReload = (int)hash["ahssReload"];
                string[] msg = { "AHSS Air-Reloading is disabled." };
                InRoomChat.SystemMessageLocal(msg);
            }
        }
        else if (RCSettings.ahssReload != 0)
        {
            RCSettings.ahssReload = 0;
            string[] msg = { "AHSS Air-Reloading is enabled." };
            InRoomChat.SystemMessageLocal(msg);
        }

        if (hash.ContainsKey("team"))
        {
            if (RCSettings.teamMode != (int)hash["team"])
            {
                RCSettings.teamMode = (int)hash["team"];
                var sort = string.Empty;
                if (RCSettings.teamMode == 1)
                {
                    sort = "Unsorted";
                }
                else if (RCSettings.teamMode == 2)
                {
                    sort = "Sorted by Size";
                }
                else if (RCSettings.teamMode == 3)
                {
                    sort = "Sorted by Skill";
                }

                string[] msg = { "Team mode is enabled. ", sort, "." };
                InRoomChat.SystemMessageLocal(msg);

                if (RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam]) == 0)
                {
                    setTeam(3);
                }
            }
        }
        else if (RCSettings.teamMode != 0)
        {
            RCSettings.teamMode = 0;
            setTeam(0);
            string[] msg = { "Team mode is disabled." };
            InRoomChat.SystemMessageLocal(msg);
        }

        if (hash.ContainsKey("point"))
        {
            if (RCSettings.pointMode != (int)hash["point"])
            {
                RCSettings.pointMode = (int)hash["point"];
                string[] msg = { "Points limit is ", $"[{Convert.ToString(RCSettings.pointMode)}]", "." };
                InRoomChat.SystemMessageLocal(msg);
            }
        }
        else if (RCSettings.pointMode != 0)
        {
            RCSettings.pointMode = 0;
            string[] msg = { "Points mode is disabled." };
            InRoomChat.SystemMessageLocal(msg);
        }

        if (hash.ContainsKey("rock"))
        {
            if (RCSettings.disableRock != (int)hash["rock"])
            {
                RCSettings.disableRock = (int)hash["rock"];
                string[] msg = { "Punks Rock-Throwing is disabled." };
                InRoomChat.SystemMessageLocal(msg);
            }
        }
        else if (RCSettings.disableRock != 0)
        {
            RCSettings.disableRock = 0;
            string[] msg = { "Punks Rock-Throwing is enabled." };
            InRoomChat.SystemMessageLocal(msg);
        }

        if (hash.ContainsKey("explode"))
        {
            if (RCSettings.explodeMode != (int)hash["explode"])
            {
                RCSettings.explodeMode = (int)hash["explode"];
                string[] msg = { "Explode radius is ", $"[{Convert.ToString(RCSettings.explodeMode)}]", "." };
                InRoomChat.SystemMessageLocal(msg);
            }
        }
        else if (RCSettings.explodeMode != 0)
        {
            RCSettings.explodeMode = 0;
            string[] msg = { "Explode mode is disabled." };
            InRoomChat.SystemMessageLocal(msg);
        }

        if (hash.ContainsKey("healthMode") && hash.ContainsKey("healthLower") && hash.ContainsKey("healthUpper"))
        {
            if (RCSettings.healthMode != (int)hash["healthMode"] || RCSettings.healthLower != (int)hash["healthLower"] || RCSettings.healthUpper != (int)hash["healthUpper"])
            {
                RCSettings.healthMode = (int)hash["healthMode"];
                RCSettings.healthLower = (int)hash["healthLower"];
                RCSettings.healthUpper = (int)hash["healthUpper"];
                var mode = "Static ";
                if (RCSettings.healthMode == 2)
                {
                    mode = "Scaled ";
                }

                string[] msg = { mode + "Health amount is ", $"[{Convert.ToString(RCSettings.healthLower)} - {Convert.ToString(RCSettings.healthUpper)}]", "." };
                InRoomChat.SystemMessageLocal(msg);
            }
        }
        else if (RCSettings.healthMode != 0 || RCSettings.healthLower != 0 || RCSettings.healthUpper != 0)
        {
            RCSettings.healthMode = 0;
            RCSettings.healthLower = 0;
            RCSettings.healthUpper = 0;
            string[] msg = { "Health mode is disabled." };
            InRoomChat.SystemMessageLocal(msg);
        }

        if (hash.ContainsKey("infection"))
        {
            if (RCSettings.infectionMode != (int)hash["infection"])
            {
                RCSettings.infectionMode = (int)hash["infection"];
                name = LoginFengKAI.player.name;
                hashtable = new Hashtable();
                hashtable.Add(PhotonPlayerProperty.RCteam, 0);
                PhotonNetwork.player.SetCustomProperties(hashtable);
                string[] msg = { "Infection mode with ", $"[{Convert.ToString(RCSettings.infectionMode)}]", " infected on start." };
                InRoomChat.SystemMessageLocal(msg);
            }
        }
        else if (RCSettings.infectionMode != 0)
        {
            RCSettings.infectionMode = 0;
            hashtable = new Hashtable();
            hashtable.Add(PhotonPlayerProperty.isTitan, 1);
            PhotonNetwork.player.SetCustomProperties(hashtable);
            string[] msg = { "Infection mode is disabled." };
            InRoomChat.SystemMessageLocal(msg);

            if (PhotonNetwork.isMasterClient)
            {
                restartingTitan = true;
            }
        }

        if (hash.ContainsKey("eren"))
        {
            if (RCSettings.banEren != (int)hash["eren"])
            {
                RCSettings.banEren = (int)hash["eren"];
                string[] msg = { "Anti-Eren mode is enabled." };
                InRoomChat.SystemMessageLocal(msg);
                if (PhotonNetwork.isMasterClient)
                {
                    restartingEren = true;
                }
            }
        }
        else if (RCSettings.banEren != 0)
        {
            RCSettings.banEren = 0;
            string[] msg = { "Anti-Eren mode is disabled." };
            InRoomChat.SystemMessageLocal(msg);
        }

        if (hash.ContainsKey("titanc"))
        {
            if (RCSettings.moreTitans != (int)hash["titanc"])
            {
                RCSettings.moreTitans = (int)hash["titanc"];
                string[] msg = { "Custom Titans Amount is ", $"[{Convert.ToString(RCSettings.moreTitans)}]", "." };
                InRoomChat.SystemMessageLocal(msg);
            }
        }
        else if (RCSettings.moreTitans != 0)
        {
            RCSettings.moreTitans = 0;
            string[] msg = { "Custom Titans Amount mode is disabled." };
            InRoomChat.SystemMessageLocal(msg);
        }

        if (hash.ContainsKey("damage"))
        {
            if (RCSettings.damageMode != (int)hash["damage"])
            {
                RCSettings.damageMode = (int)hash["damage"];
                string[] msg = { "Minimum Nape Damage is ", $"[{Convert.ToString(RCSettings.damageMode)}]", "." };
                InRoomChat.SystemMessageLocal(msg);
            }
        }
        else if (RCSettings.damageMode != 0)
        {
            RCSettings.damageMode = 0;
            string[] msg = { "Minimum Nape Damage mode is disabled." };
            InRoomChat.SystemMessageLocal(msg);
        }

        if (hash.ContainsKey("sizeMode") && hash.ContainsKey("sizeLower") && hash.ContainsKey("sizeUpper"))
        {
            if (RCSettings.sizeMode != (int)hash["sizeMode"] || RCSettings.sizeLower != (float)hash["sizeLower"] || RCSettings.sizeUpper != (float)hash["sizeUpper"])
            {
                RCSettings.sizeMode = (int)hash["sizeMode"];
                RCSettings.sizeLower = (float)hash["sizeLower"];
                RCSettings.sizeUpper = (float)hash["sizeUpper"];
                string[] msg = { "Custom Titans Size is ", $"[{RCSettings.sizeLower.ToString("F2")} - {RCSettings.sizeUpper.ToString("F2")}]", "." };
                InRoomChat.SystemMessageLocal(msg);
            }
        }
        else if (RCSettings.sizeMode != 0 || RCSettings.sizeLower != 0f || RCSettings.sizeUpper != 0f)
        {
            RCSettings.sizeMode = 0;
            RCSettings.sizeLower = 0f;
            RCSettings.sizeUpper = 0f;
            string[] msg = { "Custom Titans Size mode is enabled." };
            InRoomChat.SystemMessageLocal(msg);
        }

        if (hash.ContainsKey("spawnMode") && hash.ContainsKey("nRate") && hash.ContainsKey("aRate") && hash.ContainsKey("jRate") && hash.ContainsKey("cRate") && hash.ContainsKey("pRate"))
        {
            if (RCSettings.spawnMode != (int)hash["spawnMode"] || RCSettings.nRate != (float)hash["nRate"] || RCSettings.aRate != (float)hash["aRate"] || RCSettings.jRate != (float)hash["jRate"] || RCSettings.cRate != (float)hash["cRate"] || RCSettings.pRate != (float)hash["pRate"])
            {
                RCSettings.spawnMode = (int)hash["spawnMode"];
                RCSettings.nRate = (float)hash["nRate"];
                RCSettings.aRate = (float)hash["aRate"];
                RCSettings.jRate = (float)hash["jRate"];
                RCSettings.cRate = (float)hash["cRate"];
                RCSettings.pRate = (float)hash["pRate"];
                string[] msg = { "Custom Spawn Rate is:", $"\n[{RCSettings.nRate.ToString("F2")}% Normal]" + $"\n[{RCSettings.aRate.ToString("F2")}% Abnormal]" + $"\n[{RCSettings.jRate.ToString("F2")}% Jumper]" + $"\n[{RCSettings.cRate.ToString("F2")}% Crawler]" + $"\n[{RCSettings.pRate.ToString("F2")}% Punk]" };
                InRoomChat.SystemMessageLocal(msg);
            }
        }
        else if (RCSettings.spawnMode != 0 || RCSettings.nRate != 0f || RCSettings.aRate != 0f || RCSettings.jRate != 0f || RCSettings.cRate != 0f || RCSettings.pRate != 0f)
        {
            RCSettings.spawnMode = 0;
            RCSettings.nRate = 0f;
            RCSettings.aRate = 0f;
            RCSettings.jRate = 0f;
            RCSettings.cRate = 0f;
            RCSettings.pRate = 0f;
            string[] msg = { "Custom Spawn Rate mode is disabled." };
            InRoomChat.SystemMessageLocal(msg);
        }

        if (hash.ContainsKey("waveModeOn") && hash.ContainsKey("waveModeNum"))
        {
            if (RCSettings.waveModeOn != (int)hash["waveModeOn"] || RCSettings.waveModeNum != (int)hash["waveModeNum"])
            {
                RCSettings.waveModeOn = (int)hash["waveModeOn"];
                RCSettings.waveModeNum = (int)hash["waveModeNum"];
                string[] msg = { "Custom Titans/Wave amount is ", $"[{Convert.ToString(RCSettings.waveModeNum)}]", "." };
                InRoomChat.SystemMessageLocal(msg);
            }
        }
        else if (RCSettings.waveModeOn != 0 || RCSettings.waveModeNum != 0)
        {
            RCSettings.waveModeOn = 0;
            RCSettings.waveModeNum = 0;
            string[] msg = { "Custom Titans/Wave mode is disabled." };
            InRoomChat.SystemMessageLocal(msg);
        }

        if (hash.ContainsKey("friendly"))
        {
            if (RCSettings.friendlyMode != (int)hash["friendly"])
            {
                RCSettings.friendlyMode = (int)hash["friendly"];
                string[] msg = { "Friendly mode is enabled." };
                InRoomChat.SystemMessageLocal(msg);
            }
        }
        else if (RCSettings.friendlyMode != 0)
        {
            RCSettings.friendlyMode = 0;
            string[] msg = { "Friendly mode is disabled." };
            InRoomChat.SystemMessageLocal(msg);
        }

        if (hash.ContainsKey("pvp"))
        {
            if (RCSettings.pvpMode != (int)hash["pvp"])
            {
                RCSettings.pvpMode = (int)hash["pvp"];
                var mode = string.Empty;
                if (RCSettings.pvpMode == 1)
                {
                    mode = "Team-Based ";
                }
                else if (RCSettings.pvpMode == 2)
                {
                    mode = "FFA ";
                }

                string[] msg = { mode + "PVP mode is enabled." };
                InRoomChat.SystemMessageLocal(msg);
            }
        }
        else if (RCSettings.pvpMode != 0)
        {
            RCSettings.pvpMode = 0;
            string[] msg = { "PVP mode is disabled." };
            InRoomChat.SystemMessageLocal(msg);
        }

        if (hash.ContainsKey("maxwave"))
        {
            if (RCSettings.maxWave != (int)hash["maxwave"])
            {
                RCSettings.maxWave = (int)hash["maxwave"];
                string[] msg = { "Custom Maximum Wave is ", $"[{RCSettings.maxWave.ToString()}]", "." };
                InRoomChat.SystemMessageLocal(msg);
            }
        }
        else if (RCSettings.maxWave != 0)
        {
            RCSettings.maxWave = 0;
            string[] msg = { "Custom Maximum Wave mode is disabled." };
            InRoomChat.SystemMessageLocal(msg);
        }

        if (hash.ContainsKey("endless"))
        {
            if (RCSettings.endlessMode != (int)hash["endless"])
            {
                RCSettings.endlessMode = (int)hash["endless"];
                string[] msg = { "Endless Respawn is ", $"[{RCSettings.endlessMode.ToString()}]", " seconds." };
                InRoomChat.SystemMessageLocal(msg);
            }
        }
        else if (RCSettings.endlessMode != 0)
        {
            RCSettings.endlessMode = 0;
            string[] msg = { "Endless Respawn mode is disabled." };
            InRoomChat.SystemMessageLocal(msg);
        }

        if (hash.ContainsKey("deadlycannons"))
        {
            if (RCSettings.deadlyCannons != (int)hash["deadlycannons"])
            {
                RCSettings.deadlyCannons = (int)hash["deadlycannons"];
                string[] msg = { "Deadly Cannons mode is enabled." };
                InRoomChat.SystemMessageLocal(msg);
            }
        }
        else if (RCSettings.deadlyCannons != 0)
        {
            RCSettings.deadlyCannons = 0;
            string[] msg = { "Deadly Cannons mode is disabled." };
            InRoomChat.SystemMessageLocal(msg);
        }

        if (hash.ContainsKey("asoracing"))
        {
            if (RCSettings.racingStatic != (int)hash["asoracing"])
            {
                RCSettings.racingStatic = (int)hash["asoracing"];
                string[] msg = { "Racing will not restart on finish." };
                InRoomChat.SystemMessageLocal(msg);
            }
        }
        else if (RCSettings.racingStatic != 0)
        {
            RCSettings.racingStatic = 0;
            string[] msg = { "Racing will restart on finish." };
            InRoomChat.SystemMessageLocal(msg);
        }

        if (hash.ContainsKey("motd"))
        {
            if (RCSettings.motd != (string)hash["motd"])
            {
                RCSettings.motd = (string)hash["motd"];
                string[] msg = { "MOTD:\n", RCSettings.motd };
                InRoomChat.SystemMessageLocal(msg, false);
            }
        }
        else if (RCSettings.motd != string.Empty)
        {
            RCSettings.motd = string.Empty;
        }
    }

    private IEnumerator setGuildFeng()
    {
        WWW iteratorVariable1;
        var form = new WWWForm();
        form.AddField("name", LoginFengKAI.player.name);
        form.AddField("guildname", LoginFengKAI.player.guildname);
        if (Application.isWebPlayer)
        {
            iteratorVariable1 = new WWW("http://aotskins.com/version/guild.php", form);
        }
        else
        {
            iteratorVariable1 = new WWW("http://fenglee.com/game/aog/change_guild_name.php", form);
        }

        yield return iteratorVariable1;
    }

    [RPC]
    private void setMasterRC(PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            masterRC = true;
        }
    }

    private void setTeam(int setting)
    {
        if (setting == 0)
        {
            name = LoginFengKAI.player.name;
            var propertiesToSet = new Hashtable();
            propertiesToSet.Add(PhotonPlayerProperty.RCteam, 0);
            propertiesToSet.Add(PhotonPlayerProperty.name, name);
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        }
        else if (setting == 1)
        {
            var hashtable2 = new Hashtable();
            hashtable2.Add(PhotonPlayerProperty.RCteam, 1);
            var name = LoginFengKAI.player.name;
            while (name.Contains("[") && name.Length >= name.IndexOf("[") + 8)
            {
                var index = name.IndexOf("[");
                name = name.Remove(index, 8);
            }

            if (!name.StartsWith("[00FFFF]"))
            {
                name = "[00FFFF]" + name;
            }

            this.name = name;
            hashtable2.Add(PhotonPlayerProperty.name, this.name);
            PhotonNetwork.player.SetCustomProperties(hashtable2);
        }
        else if (setting == 2)
        {
            var hashtable3 = new Hashtable();
            hashtable3.Add(PhotonPlayerProperty.RCteam, 2);
            var str2 = LoginFengKAI.player.name;
            while (str2.Contains("[") && str2.Length >= str2.IndexOf("[") + 8)
            {
                var startIndex = str2.IndexOf("[");
                str2 = str2.Remove(startIndex, 8);
            }

            if (!str2.StartsWith("[FF00FF]"))
            {
                str2 = "[FF00FF]" + str2;
            }

            name = str2;
            hashtable3.Add(PhotonPlayerProperty.name, name);
            PhotonNetwork.player.SetCustomProperties(hashtable3);
        }
        else if (setting == 3)
        {
            var num3 = 0;
            var num4 = 0;
            var num5 = 1;
            foreach (var player in PhotonNetwork.playerList)
            {
                var num7 = RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.RCteam]);
                if (num7 > 0)
                {
                    if (num7 == 1)
                    {
                        num3++;
                    }
                    else if (num7 == 2)
                    {
                        num4++;
                    }
                }
            }

            if (num3 > num4)
            {
                num5 = 2;
            }

            setTeam(num5);
        }

        if (setting == 0 || setting == 1 || setting == 2)
        {
            foreach (var obj2 in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (obj2.GetPhotonView().isMine)
                {
                    photonView.RPC("labelRPC", PhotonTargets.All, obj2.GetPhotonView().viewID);
                }
            }
        }
    }

    [RPC]
    private void setTeamRPC(int setting, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient || info.sender.isLocal)
        {
            setTeam(setting);
        }
    }

    [RPC]
    private void settingRPC(Hashtable hash, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            setGameSettings(hash);
        }
    }

    [RPC]
    private void showChatContent(string content)
    {
        chatContent.Add(content);
        if (chatContent.Count > 10)
        {
            chatContent.RemoveAt(0);
        }

        GameObjectCache.Find("LabelChatContent").GetComponent<UILabel>().text = string.Empty;
        for (var i = 0; i < chatContent.Count; i++)
        {
            var component = GameObjectCache.Find("LabelChatContent").GetComponent<UILabel>();
            component.text = component.text + chatContent[i];
        }
    }

    #region UILabels

    public void ShowHUDInfoCenter(string content)
    {
        if (Settings.LegacyLabelsSetting)
        {
            var obj2 = GameObjectCache.Find("LabelInfoCenter");
            if (obj2 != null)
            {
                obj2.GetComponent<UILabel>().text = content;
            }
        }
        else
        {
            Labels.Center = content.ToHTML();
        }
    }

    public void ShowHUDInfoCenterADD(string content)
    {
        if (Settings.LegacyLabelsSetting)
        {
            var obj2 = GameObjectCache.Find("LabelInfoCenter");
            if (obj2 != null)
            {
                var component = obj2.GetComponent<UILabel>();
                component.text = component.text + content;
            }
        }
        else
        {
            Labels.Center = content.ToHTML();
        }
    }

    public void ShowHUDInfoTopCenter(string content)
    {
        if (Settings.LegacyLabelsSetting)
        {
            var obj2 = GameObjectCache.Find("LabelInfoTopCenter");
            if (obj2 != null)
            {
                obj2.GetComponent<UILabel>().text = content;
            }
        }
        else
        {
            Labels.TopCenter = content.ToHTML();
        }
    }

    public void ShowHUDInfoTopCenterADD(string content)
    {
        if (Settings.LegacyLabelsSetting)
        {
            var obj2 = GameObjectCache.Find("LabelInfoTopCenter");
            if (obj2 != null)
            {
                var component = obj2.GetComponent<UILabel>();
                component.text = component.text + content;
            }
        }
        else
        {
            Labels.TopCenter += content.ToHTML();
        }
    }

    public void ShowHUDInfoTopLeft(string content)
    {
        if (Settings.LegacyLabelsSetting)
        {
            var obj2 = GameObjectCache.Find("LabelInfoTopLeft");
            if (obj2 != null)
            {
                obj2.GetComponent<UILabel>().text = content;
            }
        }
        else
        {
            Labels.TopLeft = content.ToHTML();
        }
    }

    public void ShowHUDInfoTopRight(string content)
    {
        if (Settings.LegacyLabelsSetting)
        {
            var obj2 = GameObjectCache.Find("LabelInfoTopRight");
            if (obj2 != null)
            {
                obj2.GetComponent<UILabel>().text = content;
            }
        }
        else
        {
            Labels.TopRight = content.ToHTML();
        }
    }

    public void ShowHUDInfoTopRightMAPNAME(string content)
    {
        if (Settings.LegacyLabelsSetting)
        {
            var obj2 = GameObjectCache.Find("LabelInfoTopRight");
            if (obj2 != null)
            {
                var component = obj2.GetComponent<UILabel>();
                component.text = component.text + content;
            }
        }
        else
        {
            Labels.TopRight += content.ToHTML();
        }
    }

    #endregion UILabels

    [RPC]
    private void showResult(string text0, string text1, string text2, string text3, string text4, string text6, PhotonMessageInfo t)
    {
        if (!(gameTimesUp || !t.sender.isMasterClient))
        {
            gameTimesUp = true;
            var obj2 = GameObjectCache.Find("UI_IN_GAME");
            NGUITools.SetActive(obj2.GetComponent<UIReferArray>().panels[0], false);
            NGUITools.SetActive(obj2.GetComponent<UIReferArray>().panels[1], false);
            NGUITools.SetActive(obj2.GetComponent<UIReferArray>().panels[2], true);
            NGUITools.SetActive(obj2.GetComponent<UIReferArray>().panels[3], false);
            GameObjectCache.Find("LabelName").GetComponent<UILabel>().text = text0;
            GameObjectCache.Find("LabelKill").GetComponent<UILabel>().text = text1;
            GameObjectCache.Find("LabelDead").GetComponent<UILabel>().text = text2;
            GameObjectCache.Find("LabelMaxDmg").GetComponent<UILabel>().text = text3;
            GameObjectCache.Find("LabelTotalDmg").GetComponent<UILabel>().text = text4;
            GameObjectCache.Find("LabelResultTitle").GetComponent<UILabel>().text = text6;
            Screen.lockCursor = false;
            Screen.showCursor = true;
            IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
            gameStart = false;
        }
        else if (!(t.sender.isMasterClient || !PhotonNetwork.player.isMasterClient))
        {
            kickPlayerRC(t.sender, true, "false game end.");
        }
    }

    private void SingleShowHUDInfoTopCenter(string content)
    {
        var obj2 = GameObjectCache.Find("LabelInfoTopCenter");
        if (obj2 != null)
        {
            obj2.GetComponent<UILabel>().text = content;
        }
    }

    private void SingleShowHUDInfoTopLeft(string content)
    {
        var obj2 = GameObjectCache.Find("LabelInfoTopLeft");
        if (obj2 != null)
        {
            content = content.Replace("[0]", "[*^_^*]");
            obj2.GetComponent<UILabel>().text = content;
        }
    }

    private void spawnPlayerCustomMap()
    {
        if (!needChooseSide && GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().gameOver)
        {
            Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().gameOver = false;
            if (RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.isTitan]) == 2)
            {
                SpawnNonAITitan2(myLastHero);
            }
            else
            {
                SpawnPlayer(myLastHero, myLastRespawnTag);
            }

            ShowHUDInfoCenter(string.Empty);
        }
    }

    private GameObject spawnTitanRaw(Vector3 position, Quaternion rotation)
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            return (GameObject)Instantiate(Resources.Load("TITAN_VER3.1"), position, rotation);
        }

        return PhotonNetwork.Instantiate("TITAN_VER3.1", position, rotation, 0);
    }

    [RPC]
    private void spawnTitanRPC(PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            foreach (TITAN titan in titans)
            {
                if (titan.photonView.isMine && !(PhotonNetwork.isMasterClient && !titan.nonAI))
                {
                    PhotonNetwork.Destroy(titan.gameObject);
                }
            }

            SpawnNonAITitan2(myLastHero);
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        FGM = this;
        gameObject.name = "MultiplayerManager";
        HeroCostume.init2();
        CharacterMaterials.init();
        DontDestroyOnLoad(gameObject);
        heroes = new ArrayList();
        eT = new ArrayList();
        titans = new ArrayList();
        fT = new ArrayList();
        cT = new ArrayList();
        hooks = new ArrayList();
        name = string.Empty;

        FengGameManagerMKII.nameField = PlayerPrefs.GetString("Name", string.Empty);
        LoginFengKAI.player.guildname = PlayerPrefs.GetString("Guild", string.Empty);

        if (privateServerField == null)
        {
            privateServerField = string.Empty;
        }

        resetGameSettings();
        banHash = new Hashtable();
        imatitan = new Hashtable();
        oldScript = string.Empty;
        currentLevel = string.Empty;
        if (currentScript == null)
        {
            currentScript = string.Empty;
        }

        titanSpawns = new List<Vector3>();
        playerSpawnsC = new List<Vector3>();
        playerSpawnsM = new List<Vector3>();
        playersRPC = new List<PhotonPlayer>();
        levelCache = new List<string[]>();
        titanSpawners = new List<TitanSpawner>();
        restartCount = new List<float>();
        ignoreList = new List<int>();
        Settings.MutedPlayers = new List<string>();
        groundList = new List<GameObject>();
        noRestart = false;
        masterRC = false;
        isSpawning = false;
        intVariables = new Hashtable();
        heroHash = new Hashtable();
        boolVariables = new Hashtable();
        stringVariables = new Hashtable();
        floatVariables = new Hashtable();
        globalVariables = new Hashtable();
        RCRegions = new Hashtable();
        RCEvents = new Hashtable();
        RCVariableNames = new Hashtable();
        RCRegionTriggers = new Hashtable();
        playerVariables = new Hashtable();
        titanVariables = new Hashtable();
        logicLoaded = false;
        customLevelLoaded = false;
        oldScriptLogic = string.Empty;
        currentScriptLogic = string.Empty;
        retryTime = 0f;
        playerList = string.Empty;
        updateTime = 0f;
        if (textureBackgroundBlack == null)
        {
            textureBackgroundBlack = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            textureBackgroundBlack.SetPixel(0, 0, new Color(0f, 0f, 0f, 1f));
            textureBackgroundBlack.Apply();
        }

        if (textureBackgroundBlue == null)
        {
            textureBackgroundBlue = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            textureBackgroundBlue.SetPixel(0, 0, new Color(0.08f, 0.3f, 0.4f, 1f));
            textureBackgroundBlue.Apply();
        }

        loadconfig();
        var list2 = new List<string>
        {
            "AOTTG_HERO",
            "Colossal",
            "Icosphere",
            "Cube",
            "colossal",
            "CITY",
            "city",
            "rock",
            "PanelLogin",
            "LOGIN",
            "BG_TITLE",
            "ButtonOPTION",
            "ButtonSINGLE",
            "ButtonLAN",
            "ButtonCREDITS",
            "PopupListLang"
        };
        foreach (GameObject obj2 in FindObjectsOfType(typeof(GameObject)))
        {
            foreach (var str in list2)
            {
                if (obj2.name.Contains(str) || obj2.name == "Button" || obj2.name == "Label" && obj2.GetComponent<UILabel>().text.Contains("Snap"))
                {
                    Destroy(obj2);
                }
                else if (obj2.name == "Checkbox")
                {
                    Destroy(obj2);
                }
            }
        }

        StartCoroutine(LoadBackground());
        gameObject.AddComponent<HotKeys>();
        if (SpectatorMode.Instance == null)
        {
            var gm = new GameObject("SpectatorMode");
            gm.AddComponent<SpectatorMode>();
            DontDestroyOnLoad(gm);
        }
    }

    private void tryKick(KickState tmp)
    {
        InRoomChat.SystemMessageLocal(string.Concat("kicking #", tmp.name, ", ", tmp.getKickCount(), "/", (int)(PhotonNetwork.playerList.Length * 0.5f), "vote"));
        if (tmp.getKickCount() >= (int)(PhotonNetwork.playerList.Length * 0.5f))
        {
            kickPhotonPlayer(tmp.name);
        }
    }

    private void Update()
    {
        FPS.Update();
        Settings.Update();
        if (Settings.LegacyLabelsSetting)
        {
            if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE && GameObjectCache.Find("LabelNetworkStatus"))
            {
                GameObjectCache.Find("LabelNetworkStatus").GetComponent<UILabel>().text = PhotonNetwork.connectionStateDetailed.ToString();
                if (PhotonNetwork.connected)
                {
                    UILabel expr_5A = GameObjectCache.Find("LabelNetworkStatus").GetComponent<UILabel>();
                    expr_5A.text = expr_5A.text + " ping:" + PhotonNetwork.GetPing();
                }
            }
        }
        else
        {
            Labels.NetworkStatus = PhotonNetwork.connectionState != ConnectionState.Disconnected ? PhotonNetwork.connectionState.ToString() + (PhotonNetwork.connected ? " ping: " + PhotonNetwork.GetPing() : string.Empty) : string.Empty;
        }

        if (gameStart)
        {
            foreach (HERO hERO in heroes)
            {
                hERO.update();
            }

            foreach (Bullet bullet in hooks)
            {
                bullet.update();
            }

            if (mainCamera != null)
            {
                mainCamera.snapShotUpdate();
            }

            foreach (TITAN_EREN tITAN_EREN in eT)
            {
                tITAN_EREN.update();
            }

            foreach (TITAN tITAN in titans)
            {
                tITAN.update();
            }

            foreach (FEMALE_TITAN fEMALE_TITAN in fT)
            {
                fEMALE_TITAN.update();
            }

            foreach (COLOSSAL_TITAN cOLOSSAL_TITAN in cT)
            {
                cOLOSSAL_TITAN.update();
            }

            if (mainCamera != null)
            {
                mainCamera.update();
            }
        }

        if (Settings.DamageFeedUISetting)
        {
            for (var i = 0; i < DamageFeedTime.Count; i++)
            {
                if (DamageFeedTime[i] <= 0f)
                {
                    DamageFeedCurrent -= DamageFeed[0];
                    DamageFeedTime.RemoveAt(0);
                    DamageFeed.RemoveAt(0);
                }
                else
                {
                    DamageFeedTime[i] -= Time.deltaTime;
                }
            }
        }
    }

    private void UpdateDamageFeedLabel(int damage)
    {
        if (!Settings.DamageFeedUISetting)
        {
            return;
        }

        if (DamageFeed == null)
        {
            DamageFeed = new List<int>();
        }
        else if (DamageFeedTime == null)
        {
            DamageFeedTime = new List<float>();
        }

        DamageFeed.Add(damage);
        DamageFeedTime.Add(8f);
        DamageFeedCurrent = 0;
        foreach (var dmg in DamageFeed)
        {
            DamageFeedCurrent += dmg;
        }

        if (DamageFeedCurrent > DamageFeedHighest)
        {
            DamageFeedHighest = DamageFeedCurrent;
        }
    }

    [RPC]
    private void updateKillInfo(bool t1, string killer, bool t2, string victim, int dmg)
    {
        GameObject obj4;
        var obj2 = GameObjectCache.Find("UI_IN_GAME");
        var obj3 = (GameObject)Instantiate(Resources.Load("UI/KillInfo"));
        for (var i = 0; i < killInfoGO.Count; i++)
        {
            obj4 = (GameObject)killInfoGO[i];
            if (obj4 != null)
            {
                obj4.GetComponent<KillInfoComponent>().moveOn();
            }
        }

        if (killInfoGO.Count > 4)
        {
            obj4 = (GameObject)killInfoGO[0];
            if (obj4 != null)
            {
                obj4.GetComponent<KillInfoComponent>().destory();
            }

            killInfoGO.RemoveAt(0);
        }

        obj3.transform.parent = obj2.GetComponent<UIReferArray>().panels[0].transform;
        obj3.GetComponent<KillInfoComponent>().show(t1, killer, t2, victim, dmg);
        killInfoGO.Add(obj3);
        if (Settings.ChatFeedSetting)
        {
            if (Settings.LegacyChatSetting)
            {
                string str2 = ("<color=#FFC000>(" + this.roundTime.ToString("F2") + ")</color> ") + killer.hexColor() + (killer.Contains("Server") ? string.Empty : " ") + "killed ";
                string newLine = str2 + victim.hexColor() + " for " + dmg.ToString() + " damage.";
                if (!Settings.ChatFeedSeparateSetting)
                    InRoomChat.AddLineChat(newLine);
                else
                    InRoomChat.AddLineChatFeed(newLine);
            }
            else
            {
                var msg = InRoomChat.ChatFormatting($"({roundTime.ToString("F2")}) ", Settings.ChatMinorColorSetting, Settings.ChatMinorFormatSettings[0], Settings.ChatMinorFormatSettings[1]) + killer.hexColor() + (killer.Contains("Server") ? string.Empty : " ") + InRoomChat.ChatFormatting("killed ", Settings.ChatMajorColorSetting, Settings.ChatMajorFormatSettings[0], Settings.ChatMajorFormatSettings[1]) + (victim.Contains("[") ? victim.hexColor() : InRoomChat.ChatFormatting(victim, Settings.ChatMinorColorSetting, Settings.ChatMinorFormatSettings[0], Settings.ChatMinorFormatSettings[1])) + InRoomChat.ChatFormatting(" for ", Settings.ChatMajorColorSetting, Settings.ChatMajorFormatSettings[0], Settings.ChatMajorFormatSettings[1]) + InRoomChat.ChatFormatting(dmg.ToString(), Settings.ChatMinorColorSetting, Settings.ChatMinorFormatSettings[0], Settings.ChatMinorFormatSettings[1]) + InRoomChat.ChatFormatting(" damage.", Settings.ChatMajorColorSetting, Settings.ChatMajorFormatSettings[0], Settings.ChatMajorFormatSettings[1]);
                if (!Settings.ChatFeedSeparateSetting)
                    InRoomChat.AddLineChat($"<size={Settings.ChatSizeSetting}>{msg}</size>");
                else
                    InRoomChat.AddLineChatFeed($"<size={Settings.ChatSizeSetting}>{msg}</size>");
            }
        }
    }
}