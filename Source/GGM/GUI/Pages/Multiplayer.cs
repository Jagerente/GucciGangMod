﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GGM.Caching;
using GGM.Config;
using UnityEngine;
using static GGM.GUI.Elements;
using static GGM.GUI.Settings;
using Random = UnityEngine.Random;

namespace GGM.GUI.Pages
{
    internal class Multiplayer : Page
    {
        private const float BoxWidth = 880;
        private const float BoxHeight = 425;
        private const float UpdateTime = 1f;
        private static readonly float[] Proportion = { 0.2f, 0.8f, 0.4f, 0.3f, 0.3f };
        private static Vector2 LeftSlider;
        private static Vector2 RightSlider;
        private static Vector2 CreateMapSlider;
        private static Vector2 PresetsSlider;

        private static Regions Server;
        private static int ChosenServer;

        private enum Regions
        {
            Offline,
            EU,
            USA,
            Asia,
            SouthAmerica
        }

        private static readonly string[] Servers = { "Offline", "Europe", "USA", "Asia", "South America" };
        private static string KeyWords;
        private static bool[] Map;
        private static readonly string[] Maps = { "The City", "The City III", "The Forest", "The Forest II", "The Forest III", "The Forest IV  - LAVA", "Annie", "Annie II", "Colossal Titan", "Colossal Titan II", "Trost", "Trost II", "Racing - Akina", "Outside The Walls", "Cave Fight", "House Fight", "Custom", "Custom (No PT)" };
        private static bool[] Difficulty;
        private static readonly string[] Difficulties = { "Normal", "Hard", "Abnormal" };
        private static bool[] DayTime;
        private static readonly string[] DayTimes = { "Day", "Dawn", "Night" };
        private static bool[] ExtendedSetting;
        private static readonly string[] ExtendedSettings = { "Hide Passworded Rooms", "Hide Full Rooms" };
        private float timeToUpdate;
        private List<RoomInfo> servers = new List<RoomInfo>();
        private bool connected;
        private Regions connectedServer;
        private static int Page;
        private static int MapToCreate;
        private static string ServerNameToCreate;
        private static string PasswordToCreate;
        private static int ServerTimeToCreate;
        private static int MaxPlayersToCreate;
        private static int DayTimeToCreate;
        private static int DifficultyToCreate;

        private static int PresetsCount;
        private static int CurrentPreset;
        private static string PresetTitle = "";
        private static List<string> PresetsTitles;
        private static List<int> MapPresets;
        private static List<string> ServerNamePresets;
        private static List<string> PasswordPresets;
        private static List<int> ServerTimePresets;
        private static List<int> MaxPlayersPresets;
        private static List<int> DayTimePresets;
        private static List<int> DifficultyPresets;

        public void Save()
        {
            PlayerPrefs.SetInt("GGM_MapToCreate", MapToCreate);
            PlayerPrefs.SetString("GGM_ServerNameToCreate", ServerNameToCreate);
            PlayerPrefs.SetString("GGM_PasswordToCreate", PasswordToCreate);
            PlayerPrefs.SetInt("GGM_ServerTimeToCreate", ServerTimeToCreate);
            PlayerPrefs.SetInt("GGM_MaxPlayersToCreate", MaxPlayersToCreate);
            PlayerPrefs.SetInt("GGM_DayTimeToCreate", DayTimeToCreate);
            PlayerPrefs.SetInt("GGM_DifficultyToCreate", DifficultyToCreate);
            PlayerPrefs.SetInt("GGM_PresetsCount", PresetsCount);
            for (var i = 0; i < PresetsCount; i++)
            {
                PlayerPrefs.SetString("GGM_PresetsTitle_" + i, PresetsTitles[i]);
                PlayerPrefs.SetInt("GGM_MapPresets_" + i, MapPresets[i]);
                PlayerPrefs.SetString("GGM_ServerNamePresets_" + i, ServerNamePresets[i]);
                PlayerPrefs.SetString("GGM_PasswordPresets_" + i, PasswordPresets[i]);
                PlayerPrefs.SetInt("GGM_ServerTimePresets_" + i, ServerTimePresets[i]);
                PlayerPrefs.SetInt("GGM_MaxPlayersPresets_" + i, MaxPlayersPresets[i]);
                PlayerPrefs.SetInt("GGM_DayTimePresets_" + i, DayTimePresets[i]);
                PlayerPrefs.SetInt("GGM_DifficultyPresets_" + i, DifficultyPresets[i]);
            }
        }

        private void Awake()
        {
            KeyWords = string.Empty;

            Map = new bool[Maps.Length];
            for (var i = 0; i < Maps.Length; i++)
            {
                Map[i] = false;
            }

            Difficulty = new bool[Difficulties.Length];
            for (var i = 0; i < Difficulties.Length; i++)
            {
                Difficulty[i] = false;
            }

            DayTime = new bool[DayTimes.Length];
            for (var i = 0; i < DayTimes.Length; i++)
            {
                DayTime[i] = false;
            }

            ExtendedSetting = new bool[ExtendedSettings.Length];
            for (var i = 0; i < ExtendedSettings.Length; i++)
            {
                ExtendedSetting[i] = false;
            }
        }

        private bool CheckFilters(RoomInfo info)
        {
            var serverName = info.name.Split('`')[0];
            var serverMap = info.name.Split('`')[1];
            var serverDifficulty = info.name.Split('`')[2];
            var serverDayTime = info.name.Split('`')[4];

            if (KeyWords != string.Empty && !serverName.ToUpper().Contains(KeyWords.ToUpper()) && !KeyWords.Contains(";"))
            {
                return false;
            }

            if (KeyWords.ToUpper().Contains(";"))
            {
                if (!KeyWords.ToUpper().Split(';').Any(key => serverName.ToUpper().Contains(key)))
                {
                    return false;
                }
            }

            for (var map = 0; map < Map.Length; map++)
            {
                if (Map[map])
                {
                    if (serverMap.ToUpper().Equals(Maps[map].ToUpper()))
                    {
                        goto MapPass;
                    }
                }
            }

            if (Map.Any(i => i)) return false;


            MapPass:
            for (var difficulty = 0; difficulty < Difficulty.Length; difficulty++)
            {
                if (Difficulty[difficulty])
                {
                    if (serverDifficulty.ToUpper().Equals(Difficulties[difficulty].ToUpper()))
                    {
                        goto DifficultyPass;
                    }
                }
            }

            if (Difficulty.Any(i => i)) return false;

            DifficultyPass:
            for (var dayTime = 0; dayTime < DayTime.Length; dayTime++)
            {
                if (DayTime[dayTime])
                {
                    if (serverDayTime.ToUpper().Equals(DayTimes[dayTime].ToUpper()))
                    {
                        goto DayTimePass;
                    }
                }
            }

            if (DayTime.Any(i => i)) return false;

            DayTimePass:
            if (ExtendedSetting[0] && info.name.Split('`')[5] != string.Empty)
            {
                return false;
            }

            if (ExtendedSetting[1] && info.playerCount >= info.maxPlayers)
            {
                return false;
            }

            return true;
        }

        private void CheckIfNeedConnect()
        {
            if (connectedServer != Server)
            {
                connected = false;
                if(PhotonNetwork.connected)
                    PhotonNetwork.Disconnect();
                if (Server == 0 && !connected)
                {
                    connectedServer = Server;
                    PhotonNetwork.Disconnect();
                    PhotonNetwork.offlineMode = true;
                    connected = true;
                }

                connectedServer = Server;
                PhotonNetwork.Disconnect();
                if (Server > 0)
                    connected = false;
            }
        }

        private void OnEnable()
        {
            Page = 0;
            timeToUpdate = UpdateTime;
            MapToCreate = PlayerPrefs.GetInt("GGM_MapToCreate", 0);
            ServerNameToCreate = PlayerPrefs.GetString("GGM_ServerNameToCreate", "FoodForTitan");
            PasswordToCreate = PlayerPrefs.GetString("GGM_PasswordToCreate", string.Empty);
            ServerTimeToCreate = PlayerPrefs.GetInt("GGM_ServerTimeToCreate", 600);
            MaxPlayersToCreate = PlayerPrefs.GetInt("GGM_MaxPlayersToCreate", 10);
            DayTimeToCreate = PlayerPrefs.GetInt("GGM_DayTimeToCreate", 0);
            DifficultyToCreate = PlayerPrefs.GetInt("GGM_DifficultyToCreate", 0);
            PresetsTitles = new List<string>();
            MapPresets = new List<int>();
            ServerNamePresets = new List<string>();
            PasswordPresets = new List<string>();
            ServerTimePresets = new List<int>();
            MaxPlayersPresets = new List<int>();
            DayTimePresets = new List<int>();
            DifficultyPresets = new List<int>();

            PresetsCount = PlayerPrefs.GetInt("GGM_PresetsCount", 1);
            for (var i = 0; i < PresetsCount; i++)
            {
                PresetsTitles.Add(PlayerPrefs.GetString("GGM_PresetsTitle_" + i, "Preset"));
                MapPresets.Add(PlayerPrefs.GetInt("GGM_MapPresets_" + i, 0));
                ServerNamePresets.Add(PlayerPrefs.GetString("GGM_ServerNamePresets_" + i, "FoodForTitan"));
                PasswordPresets.Add(PlayerPrefs.GetString("GGM_PasswordPresets_" + i, string.Empty));
                ServerTimePresets.Add(PlayerPrefs.GetInt("GGM_ServerTimePresets_" + i, 600));
                MaxPlayersPresets.Add(PlayerPrefs.GetInt("GGM_MaxPlayersPresets_" + i, 10));
                DayTimePresets.Add(PlayerPrefs.GetInt("GGM_DayTimePresets_" + i, 0));
                DifficultyPresets.Add(PlayerPrefs.GetInt("GGM_DifficultyPresets_" + i, 0));
            }
        }

        private void OnGUI()
        {
            UnityEngine.GUI.Box(new Rect(Screen.width / 2f - (BoxWidth + 10f) / 2f, Screen.height / 2f - (BoxHeight + 10f) / 2f, BoxWidth + 10f, BoxHeight + 10f), ColorCache.Textures[ColorCache.PurpleMunsell]);
            switch (Page)
            {
                case 0:
                    {
                        GUILayout.BeginArea(new Rect(Screen.width / 2f - BoxWidth / 2f, Screen.height / 2f - BoxHeight / 2f, BoxWidth, 25f));
                        {
                            GUILayout.FlexibleSpace();
                            GUILayout.BeginHorizontal();
                            {
                                GUILayout.FlexibleSpace();
                                Grid(string.Empty, ref ChosenServer, Servers, width: BoxWidth / 2f, height: 25);
                                Server = (Regions)ChosenServer;
                                CheckIfNeedConnect();
                                GUILayout.FlexibleSpace();
                            }
                            GUILayout.EndHorizontal();
                            GUILayout.FlexibleSpace();
                        }
                        GUILayout.EndArea();

                        GUILayout.BeginArea(new Rect(Screen.width / 2f - BoxWidth / 2f, Screen.height / 2f - BoxHeight / 2f + 25f, BoxWidth * Proportion[0] + 10f, BoxHeight - 60f));
                        {
                            GUILayout.BeginVertical();
                            {
                                GUILayout.BeginHorizontal();
                                {
                                    Label("Filter", LabelType.Header, width: BoxWidth * Proportion[0] - 10f);
                                    GUILayout.Space(10f);
                                }
                                GUILayout.EndHorizontal();

                                LeftSlider = GUILayout.BeginScrollView(LeftSlider);
                                {
                                    Label("Key Words", LabelType.SubHeader, width: BoxWidth * Proportion[0] - 10f);
                                    TextField(string.Empty, ref KeyWords, BoxWidth * Proportion[0] - 10f);
                                    Label("Map", LabelType.SubHeader, width: BoxWidth * Proportion[0] - 10f);
                                    ButtonToggle(string.Empty, Maps, Map, false, BoxWidth * Proportion[0] - 10f);
                                    Label("Difficulty", LabelType.SubHeader, width: BoxWidth * Proportion[0] - 10f);
                                    ButtonToggle(string.Empty, Difficulties, Difficulty, false, BoxWidth * Proportion[0] - 10f);
                                    Label("Day Time", LabelType.SubHeader, width: BoxWidth * Proportion[0] - 10f);
                                    ButtonToggle(string.Empty, DayTimes, DayTime, false, width: BoxWidth * Proportion[0] - 10f);
                                    Label("Extended", LabelType.SubHeader, width: BoxWidth * Proportion[0] - 10f);
                                    ButtonToggle(string.Empty, ExtendedSettings, ExtendedSetting, false, width: BoxWidth * Proportion[0] - 10f);
                                    GUILayout.Space(5f);
                                }
                                GUILayout.EndScrollView();
                            }
                            GUILayout.EndVertical();
                        }
                        GUILayout.EndArea();

                        GUILayout.BeginArea(new Rect(Screen.width / 2f - BoxWidth / 2f + BoxWidth * Proportion[0], Screen.height / 2f - BoxHeight / 2f + 25f, BoxWidth * Proportion[1] + 10f, BoxHeight - 60f));
                        {
                            RightSlider = GUILayout.BeginScrollView(RightSlider);

                            foreach (var server in servers)
                            {
                                var data = server.name.Split('`');
                                if (GUILayout.Button((data[5] != string.Empty ? "[PWD]" : string.Empty) + (data[0].Length > 40 ? data[0].Remove(40, data[0].Length - 40).ToHTML() : data[0].ToHTML()) + "/" + data[1] + "/" + data[2].ToUpperCase() + "/" + data[4].ToUpperCase() + "    " + server.playerCount + "/" + server.maxPlayers, GUILayout.Width(BoxWidth * Proportion[1] - 10f)))
                                {
                                    if (server.playerCount < server.maxPlayers)
                                    {
                                        GetInstance<Multiplayer>().Disable();
                                        PhotonNetwork.JoinRoom(server.name);
                                    }
                                }
                            }

                            GUILayout.EndScrollView();
                        }
                        GUILayout.EndArea();

                        GUILayout.BeginArea(new Rect(Screen.width / 2f - BoxWidth / 2f, Screen.height / 2f - BoxHeight / 2f + BoxHeight - 35f, BoxWidth, 35f));
                        {
                            GUILayout.BeginHorizontal();
                            {
                                if (GUILayout.Button("Back", GUILayout.Width(120f), GUILayout.Height(35f)))
                                {
                                    if (PhotonNetwork.connected) PhotonNetwork.Disconnect();
                                    NGUITools.SetActive(UIMainReferences.instance.panelMain.gameObject, true);
                                    GetInstance<Multiplayer>().Disable();
                                }

                                GUILayout.FlexibleSpace();

                                if (GUILayout.Button("Create", GUILayout.Width(120f), GUILayout.Height(35f)))
                                {
                                    Page = 1;
                                }
                            }
                            GUILayout.EndHorizontal();
                        }
                        GUILayout.EndArea();
                        break;
                    }

                case 1:
                    {
                        GUILayout.BeginArea(new Rect(Screen.width / 2f - BoxWidth / 2f, Screen.height / 2f - BoxHeight / 2f, BoxWidth * Proportion[2] + 10f, BoxHeight - 35f));
                        {
                            Label("Map", LabelType.Header, width: BoxWidth * Proportion[2] - 10f);

                            CreateMapSlider = GUILayout.BeginScrollView(CreateMapSlider);

                            Grid(string.Empty, ref MapToCreate, Maps, false, width: BoxWidth * Proportion[2] - 10f);

                            GUILayout.EndScrollView();
                        }
                        GUILayout.EndArea();

                        GUILayout.BeginArea(new Rect(Screen.width / 2f - BoxWidth / 2f + BoxWidth * Proportion[2], Screen.height / 2f - BoxHeight / 2f, BoxWidth * Proportion[3], BoxHeight - 35f));
                        {
                            Label("Server Name", LabelType.Header, width: BoxWidth * Proportion[3]);
                            TextField(string.Empty, ref ServerNameToCreate, BoxWidth * Proportion[3]);
                            Label("Password", LabelType.Header, width: BoxWidth * Proportion[3] - 10f);
                            TextField(string.Empty, ref PasswordToCreate, BoxWidth * Proportion[3]);
                            Label("Difficulty", LabelType.Header, width: BoxWidth * Proportion[3] - 10f);
                            Grid(string.Empty, ref DifficultyToCreate, Difficulties, false, width: BoxWidth * Proportion[3] - 10f);
                            Label("Day Time", LabelType.Header, width: BoxWidth * Proportion[3] - 10f);
                            Grid(string.Empty, ref DayTimeToCreate, DayTimes, false, width: BoxWidth * Proportion[3] - 10f);
                        }
                        GUILayout.EndArea();

                        GUILayout.BeginArea(new Rect(Screen.width / 2f - BoxWidth / 2f + BoxWidth * Proportion[2] + BoxWidth * Proportion[3], Screen.height / 2f - BoxHeight / 2f, BoxWidth * Proportion[4] + 10f, BoxHeight - 40f));
                        {
                            GUILayout.BeginHorizontal();
                            {
                                Label("Time", LabelType.Header, width: BoxWidth * Proportion[4] / 2f - 10f);
                                Label("Players", LabelType.Header, width: BoxWidth * Proportion[4] / 2f);
                            }
                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            {
                                GUILayout.Space((BoxWidth * Proportion[4] / 2f - 50f) / 2f);
                                TextField(string.Empty, ref ServerTimeToCreate, 50f);
                                GUILayout.Space(BoxWidth * Proportion[4] / 2f - 50f);
                                TextField(string.Empty, ref MaxPlayersToCreate, 50f);
                                GUILayout.Space((BoxWidth * Proportion[4] / 2f - 50f) / 2f);
                            }
                            GUILayout.EndHorizontal();
                            Label("Presets", LabelType.Header, width: BoxWidth * Proportion[4] - 10f);
                            TextField(string.Empty, ref PresetTitle, BoxWidth * Proportion[4] - 10f);
                            PresetsSlider = GUILayout.BeginScrollView(PresetsSlider);
                            {
                                for (var i = 0; i < PresetsCount; i++)
                                {
                                    if (GUILayout.Button(PresetsTitles[i], GUILayout.Width(BoxWidth * Proportion[4] - 10f)))
                                    {
                                        PresetTitle = PresetsTitles[i];
                                        MapToCreate = MapPresets[i];
                                        ServerNameToCreate = ServerNamePresets[i];
                                        PasswordToCreate = PasswordPresets[i];
                                        DifficultyToCreate = DifficultyPresets[i];
                                        DayTimeToCreate = DayTimePresets[i];
                                        ServerTimeToCreate = ServerTimePresets[i];
                                        MaxPlayersToCreate = MaxPlayersPresets[i];
                                        CurrentPreset = i;
                                    }
                                }
                            }
                            GUILayout.EndScrollView();
                            GUILayout.BeginHorizontal();
                            {
                                if (GUILayout.Button("Add", GUILayout.Width((BoxWidth * Proportion[4] - 10f) / 3f)))
                                {
                                    PresetsCount++;
                                    CurrentPreset = PresetsCount - 1;
                                    PresetsTitles.Add(PlayerPrefs.GetString("GGM_PresetsTitle_" + CurrentPreset, "Preset"));
                                    MapPresets.Add(PlayerPrefs.GetInt("GGM_MapPresets_" + CurrentPreset, 0));
                                    ServerNamePresets.Add(PlayerPrefs.GetString("GGM_ServerNamePresets_" + CurrentPreset, "FoodForTitan"));
                                    PasswordPresets.Add(PlayerPrefs.GetString("GGM_PasswordPresets_" + CurrentPreset, string.Empty));
                                    ServerTimePresets.Add(PlayerPrefs.GetInt("GGM_ServerTimePresets_" + CurrentPreset, 600));
                                    MaxPlayersPresets.Add(PlayerPrefs.GetInt("GGM_MaxPlayersPresets_" + CurrentPreset, 10));
                                    DayTimePresets.Add(PlayerPrefs.GetInt("GGM_DayTimePresets_" + CurrentPreset, 0));
                                    DifficultyPresets.Add(PlayerPrefs.GetInt("GGM_DifficultyPresets_" + CurrentPreset, 0));
                                    Save();
                                }

                                if (GUILayout.Button("Save", GUILayout.Width((BoxWidth * Proportion[4] - 10f) / 3f)))
                                {
                                    PresetsTitles[CurrentPreset] = PresetTitle;
                                    MapPresets[CurrentPreset] = MapToCreate;
                                    ServerNamePresets[CurrentPreset] = ServerNameToCreate;
                                    PasswordPresets[CurrentPreset] = PasswordToCreate;
                                    ServerTimePresets[CurrentPreset] = ServerTimeToCreate;
                                    MaxPlayersPresets[CurrentPreset] = MaxPlayersToCreate;
                                    DayTimePresets[CurrentPreset] = DayTimeToCreate;
                                    DifficultyPresets[CurrentPreset] = DifficultyToCreate;
                                    Save();
                                }

                                if (GUILayout.Button("Remove", GUILayout.Width((BoxWidth * Proportion[4] - 10f) / 3f)))
                                {
                                    if (PresetsCount > 1)
                                    {
                                        PresetsTitles.RemoveAt(CurrentPreset);
                                        MapPresets.RemoveAt(CurrentPreset);
                                        ServerNamePresets.RemoveAt(CurrentPreset);
                                        PasswordPresets.RemoveAt(CurrentPreset);
                                        ServerTimePresets.RemoveAt(CurrentPreset);
                                        MaxPlayersPresets.RemoveAt(CurrentPreset);
                                        DayTimePresets.RemoveAt(CurrentPreset);
                                        DifficultyPresets.RemoveAt(CurrentPreset);
                                        CurrentPreset--;
                                        PresetsCount--;
                                        Save();
                                    }
                                }
                            }
                            GUILayout.EndHorizontal();
                        }
                        GUILayout.EndArea();

                        GUILayout.BeginArea(new Rect(Screen.width / 2f - BoxWidth / 2f, Screen.height / 2f - BoxHeight / 2f + BoxHeight - 35f, BoxWidth, 35f));
                        {
                            GUILayout.BeginHorizontal();
                            {
                                if (GUILayout.Button("Back", GUILayout.Width(120f), GUILayout.Height(35f)))
                                {
                                    Page = 0;
                                }

                                GUILayout.FlexibleSpace();

                                if (GUILayout.Button("Start", GUILayout.Width(120f), GUILayout.Height(35f)))
                                {
                                    PhotonNetwork.offlineMode = Server == 0;
                                    PhotonNetwork.CreateRoom(string.Concat(ServerNameToCreate, "`", Maps[MapToCreate], "`", Difficulties[DifficultyToCreate], "`", ServerTimeToCreate, "`", DayTimes[DayTimeToCreate].ToLower(), "`", PasswordToCreate.Length > 0 ? new SimpleAES().Encrypt(PasswordToCreate) : string.Empty, "`", Random.Range(0, 50000)), new RoomOptions { isOpen = true, isVisible = true, maxPlayers = MaxPlayersToCreate }, null);
                                    GetInstance<Multiplayer>().Disable();
                                }
                            }
                            GUILayout.EndHorizontal();
                        }
                        GUILayout.EndArea();
                        break;
                    }
            }
        }

        private void OnJoinedLobby()
        {
            timeToUpdate = 0.8f;
        }

        private void Update()
        {
            timeToUpdate -= Time.deltaTime;
            if (timeToUpdate <= 0f)
            {
                if (!connected && Server > 0)
                {
                    switch (Server)
                    {
                        case Regions.EU:
                            PhotonNetwork.ConnectToMaster("135.125.239.180", NetworkingPeer.ProtocolToNameServerPort[PhotonNetwork.networkingPeer.UsedProtocol], FengGameManagerMKII.applicationId, UIMainReferences.ServerKey);
                            break;
                        case Regions.USA:
                            PhotonNetwork.ConnectToMaster("142.44.242.29", NetworkingPeer.ProtocolToNameServerPort[PhotonNetwork.networkingPeer.UsedProtocol], FengGameManagerMKII.applicationId, UIMainReferences.ServerKey);
                            break;
                        case Regions.Asia:
                            PhotonNetwork.ConnectToMaster("51.79.164.137", NetworkingPeer.ProtocolToNameServerPort[PhotonNetwork.networkingPeer.UsedProtocol], FengGameManagerMKII.applicationId, UIMainReferences.ServerKey);
                            break;
                        case Regions.SouthAmerica:
                            PhotonNetwork.ConnectToMaster("172.107.193.233", NetworkingPeer.ProtocolToNameServerPort[PhotonNetwork.networkingPeer.UsedProtocol], FengGameManagerMKII.applicationId, UIMainReferences.ServerKey);
                            break;
                        default:
                            Server = Regions.EU;
                            break;
                    }
                }

                UpdateRoomList();
                timeToUpdate = UpdateTime;
            }
        }

        private void UpdateRoomList()
        {
            lock (servers)
            {
                servers.Clear();
                foreach (var info in PhotonNetwork.GetRoomList())
                {
                    if (CheckFilters(info))
                    {
                        servers.Add(info);
                    }
                }
            }
        }
    }
}