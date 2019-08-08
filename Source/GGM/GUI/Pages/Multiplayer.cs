using System;
using System.Collections;
using GGM.Caching;
using GGM.Config;
using UnityEngine;
using static GGM.GUI.Elements;
using static GGM.GUI.Settings;

namespace GGM.GUI.Pages
{
    internal class Multiplayer : Page
    {
        private const float BoxWidth = 880;

        private const float BoxHeight = 425;

        private static readonly float[] Proportion = { 0.2f, 0.8f };

        private static Vector2 LeftSlider;

        private static Vector2 RightSlider;

        private static int Server;

        private static readonly string[] Servers = { "Europe", "US", "Asia", "Japan" };

        private static bool Filtered;

        private static string KeyWords;

        private static bool[] Map;

        private static readonly string[] Maps = { "The City", "The City III", "The Forest", "The Forest II", "The Forest III", "The Forest IV - LAVA", "Annie", "Annie II", "Colossal Titan", "Colossal Titan II", "Trost", "Trost II", "Racing - Akina", "Outside The Walls", "Cave Fight", "House Fight", "Custom", "Custom (No PT)" };

        private static bool[] Difficulty;

        private static readonly string[] Difficulties = { "Normal", "Hard", "Abnormal" };

        private static bool[] DayTime;

        private static readonly string[] DayTimes = { "Day", "Dawn", "Night" };

        private static bool HideFullRooms;

        private static bool HidePasswordedRooms;

        private void Start()
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
        }

        private void Update()
        {
            if (KeyWords != string.Empty)
            {
                Filtered = true;
            }
            else
            {
                foreach (var map in Map)
                {
                    if (map)
                    {
                        Filtered = true;
                    }
                }
            }
        }

        private void OnGUI()
        {
            UnityEngine.GUI.Box(new Rect(Screen.width / 2f - (BoxWidth + 10f) / 2f, Screen.height / 2f - (BoxHeight + 10f) / 2f, BoxWidth + 10f, BoxHeight + 10f), ColorCache.Textures[ColorCache.PurpleMunsell]);
            GUILayout.BeginArea(new Rect(Screen.width / 2f - BoxWidth / 2f, Screen.height / 2f - BoxHeight / 2f, BoxWidth, 25f));

            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            Grid(string.Empty, ref Server, Servers, width: BoxWidth / 2f);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();

            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(Screen.width / 2f - BoxWidth / 2f, Screen.height / 2f - BoxHeight / 2f + 25f, BoxWidth * Proportion[0] + 10f, BoxHeight - 10f));

            GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();
            Label("Filter", LabelType.Header, width: BoxWidth * Proportion[0] - 10f);
            GUILayout.Space(10f);
            GUILayout.EndHorizontal();

            LeftSlider = GUILayout.BeginScrollView(LeftSlider);

            Label("Key Words", LabelType.SubHeader, width: BoxWidth * Proportion[0] - 10f);
            TextField(string.Empty, ref KeyWords, BoxWidth * Proportion[0] - 10f);
            Label("Map", LabelType.SubHeader, width: BoxWidth * Proportion[0] - 10f);
            ButtonToggle(string.Empty, Maps, Map, false, BoxWidth * Proportion[0] - 10f);
            Label("Difficulty", LabelType.SubHeader, width: BoxWidth * Proportion[0] - 10f);
            ButtonToggle(string.Empty, Difficulties, Difficulty, false, BoxWidth * Proportion[0] - 10f);
            Label("Day Time", LabelType.SubHeader, width: BoxWidth * Proportion[0] - 10f);
            ButtonToggle(string.Empty, DayTimes, DayTime, false, width: BoxWidth * Proportion[0] - 10f);
            GUILayout.Space(1f);
            GUILayout.EndScrollView();

            GUILayout.EndVertical();

            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(Screen.width / 2f - BoxWidth / 2f + BoxWidth * Proportion[0], Screen.height / 2f - BoxHeight / 2f + 25f, BoxWidth * Proportion[1] + 10f, BoxHeight - 10f));

            for (var i = 0; i < GetServers().Count; i++)
            {
                var server = (RoomInfo)GetServers()[i];
                var data = server.name.Split('`');
                if (GUILayout.Button((data[5] != string.Empty ? "[PWD]" : string.Empty) + data[0] + "/" + data[1] + "/" + data[2] + "/" + data[4] + "    " + server.playerCount + "/" + server.maxPlayers)
                {
                    Debug.Log("Connection");
                }
            }

            GUILayout.EndArea();
        }

        private static ArrayList GetServers()
        {
            var rooms = new ArrayList();
            var skip = false;
            foreach (var info in PhotonNetwork.GetRoomList())
            {
                if (KeyWords != string.Empty && !info.name.ToUpper().Contains(KeyWords.ToUpper()))
                {
                    skip = true;
                }

                for (var map = 0; map < Map.Length; map++)
                {
                    if (Map[map])
                    {
                        if (!info.name.ToUpper().Contains(Maps[map]))
                        {
                            skip = true;
                        }
                        else
                        {
                            skip = false;
                            break;
                        }
                    }
                }

                for (var difficulty = 0; difficulty < Difficulty.Length; difficulty++)
                {
                    if (Difficulty[difficulty])
                    {
                        if (!info.name.ToUpper().Contains(Difficulties[difficulty]))
                        {
                            skip = true;
                        }
                        else
                        {
                            skip = false;
                            break;
                        }
                    }
                }

                for (var dayTime = 0; dayTime < DayTime.Length; dayTime++)
                {
                    if (DayTime[dayTime])
                    {
                        if (!info.name.ToUpper().Contains(DayTimes[dayTime]))
                        {
                            skip = true;
                        }
                        else
                        {
                            skip = false;
                            break;
                        }
                    }
                }

                if (!skip)
                {
                    rooms.Add(info);
                }
            }

            return rooms;
        }
    }
}