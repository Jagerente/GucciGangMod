using System;
using GGM.Caching;
using UnityEngine;
using static GGM.GUI.Elements;
using static GGM.GUI.Settings;

namespace GGM.GUI.Pages
{
    internal class Single : Page
    {
        private const float BoxWidth = 880;

        private const float BoxHeight = 425;

        private static readonly float[] proportion = new[] { 0.4f, 0.25f, 0.35f };

        private static int map;

        private static readonly string[] mapStr =
        {
            "[S]Tutorial",
            "[S]Battle training",
            "[S]City",
            "[S]Forest",
            "[S]Forest Survive(no crawler)",
            "[S]Forest Survive(no crawler no punk)",
            "[S]Racing - Akina"
        };

        private static readonly string[] mapNameStr =
        {
            "tutorial",
            "tutorial 1",
            "The City I",
            "The Forest",
            "The Forest",
            "The Forest",
            "track - akina"
        };

        private static int daytime;

        private static string[] daytimeStr = { "Day", "Dawn", "Night" };

        private static int costume;

        private static readonly string[] cosStr = { "Cos 1", "Cos 2", "Cos 3" };

        private static int chars;

        private static readonly string[] charsStr = { "Levi", "Mikasa", "Armin", "Marco", "Jean", "Eren", "Titan_Eren", "Petra", "Sasha", "Set 1", "Set 2", "Set 3" };

        private static string[] difficultiesStr = {"Normal", "Hard", "Abnormal"};

        private static string[] cameraStr = {"ORIGINAL", "WOW", "TPS", "OldTPS"};

        private static GUIStyle box;

        private void OnGUI()
        {
            var txt = new Texture2D(1,1, TextureFormat.ARGB32, false);
            txt.SetPixel(0,0, new Color(ColorCache.DarkScarlet.Value.r, ColorCache.DarkScarlet.Value.g, ColorCache.DarkScarlet.Value.b, 0.5f));
            txt.Apply();
            box = new GUIStyle {normal = {background = txt}};
            var cameraMode = (int)IN_GAME_MAIN_CAMERA.cameraMode;

            UnityEngine.GUI.Box(new Rect(Screen.width / 2f - (BoxWidth + 10f)/ 2f, Screen.height / 2f - (BoxHeight + 10f) / 2f, BoxWidth + 10f, BoxHeight + 10f), ColorCache.Textures[ColorCache.PurpleMunsell], box);
            GUILayout.BeginArea(new Rect(Screen.width / 2f - BoxWidth / 2f, Screen.height / 2f - BoxHeight / 2f, BoxWidth, BoxHeight));

            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();

            Label("Map", LabelType.Header, width: BoxWidth * proportion[0] - 15f);
            Grid(string.Empty, ref map, mapStr, false, 1, BoxWidth * proportion[0] - 15f, GridHeight * mapStr.Length + 5 * (mapStr.Length - 1));

            GUILayout.EndVertical();

            GUILayout.BeginVertical();

            Label("Camera Type", LabelType.Header, width: BoxWidth * proportion[1] - 5f);
            Grid(string.Empty, ref cameraMode, cameraStr, false, 1, BoxWidth * proportion[1] - 5f, GridHeight * cameraStr.Length + 5 * (cameraStr.Length - 1));
            IN_GAME_MAIN_CAMERA.cameraMode = (CAMERA_TYPE)cameraMode;
            Label("Daytime", LabelType.Header, width: BoxWidth * proportion[1] - 5f);
            Grid(string.Empty, ref daytime, daytimeStr, false, 1, BoxWidth * proportion[1] - 5f, GridHeight * daytimeStr.Length + 5 * (daytimeStr.Length - 1));
            IN_GAME_MAIN_CAMERA.dayLight = (DayLight)daytime;
            Label("Difficulty", LabelType.Header, width: BoxWidth * proportion[1] - 5f);
            Grid(string.Empty, ref IN_GAME_MAIN_CAMERA.difficulty, difficultiesStr, false, 1, BoxWidth * proportion[1] - 5f, GridHeight * difficultiesStr.Length + 5 * (difficultiesStr.Length - 1));

            GUILayout.EndVertical();

            GUILayout.BeginVertical();

            Label("Character", LabelType.Header, width: BoxWidth * proportion[2] - 12.5f);
            Grid(string.Empty, ref costume, cosStr, width: BoxWidth * proportion[2] - 12.5f);
            Grid(string.Empty, ref chars, charsStr, false, 1, BoxWidth * proportion[2] - 12.5f, GridHeight * charsStr.Length + 5 * (charsStr.Length - 1));

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Start", GUILayout.Width(120f), GUILayout.Height(35f)))
            {
                CheckBoxCostume.costumeSet = costume + 1;
                IN_GAME_MAIN_CAMERA.singleCharacter = charsStr[chars].ToUpper();
                if (IN_GAME_MAIN_CAMERA.singleCharacter.StartsWith("SET") ||
                    IN_GAME_MAIN_CAMERA.singleCharacter.StartsWith("AHSS"))
                {
                    CheckBoxCostume.costumeSet = 1;
                }

                IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.SINGLE;
                if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS)
                {
                    Screen.lockCursor = true;
                }

                Screen.showCursor = false;
                FengGameManagerMKII.level = mapStr[map];
                Application.LoadLevel(mapNameStr[map]);
                GetInstance<Single>().Disable();
            }
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Back", GUILayout.Width(120f), GUILayout.Height(35f)))
            {
                GetInstance<Single>().Disable();
                NGUITools.SetActive(UIMainReferences.instance.panelMain.gameObject, true);
            }

            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }

    }
}
