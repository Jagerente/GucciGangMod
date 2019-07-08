using System;
using UnityEngine;
using static GGM.GUI.Elements;
using static GGM.GUI.Settings;

namespace GGM.GUI.Pages
{
    internal class Single : Page
    {
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

        private static int map;

        private static int daytime;

        private static int costume;

        private static int chars;
        private static readonly string[] charsStr = { "LEVI", "MIKASA", "ARMIN", "MARCO", "JEAN", "EREN", "TITAN_EREN", "PETRA", "SASHA", "Set 1", "Set 2", "Set 3" };

        private static string[] daytimeStr = { "Day", "Dawn", "Night" };
        private static string[] difficulties = {"Normal", "Hard", "Abnormal"};
        private static string[] camera = {"ORIGINAL", "WOW", "TPS"};

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 440, Screen.height / 2 - 250, 880, 500));
            GUILayout.BeginHorizontal(GUILayout.Width(275f));

            GUILayout.BeginVertical();
            Label("Map", LabelType.Header, width: 310f);
            map = GUILayout.SelectionGrid(map, mapStr, 1);
            GUILayout.EndVertical();

            GUILayout.BeginVertical(GUILayout.Width(120f));
            Label("Camera Type", LabelType.Header, width: 120f);
            var ss = (int)IN_GAME_MAIN_CAMERA.cameraMode;
            Grid(string.Empty, ref ss, camera, false, 1, height: GridHeight * 3);
            IN_GAME_MAIN_CAMERA.cameraMode = (CAMERA_TYPE)ss;
            Label("Daytime", LabelType.Header, width: 120f);
            Grid(string.Empty, ref daytime, daytimeStr, false, 1, height: GridHeight * 3);
            IN_GAME_MAIN_CAMERA.dayLight = (DayLight)daytime;
            Label("Difficulty", LabelType.Header, width: 120f);
            Grid(string.Empty, ref IN_GAME_MAIN_CAMERA.difficulty, difficulties, false, 1, height: GridHeight * 3);
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            Label("Character", LabelType.Header);
            costume = GUILayout.SelectionGrid(costume, new[] { "Cos 1", "Cos 2", "Cos 3" }, 3);
            CheckBoxCostume.costumeSet = costume + 1;
            chars = GUILayout.SelectionGrid(chars, charsStr, 1);
            IN_GAME_MAIN_CAMERA.singleCharacter = charsStr[chars];
            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Start", GUILayout.Width(120f), GUILayout.Height(35f)))
            {
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
