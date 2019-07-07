using UnityEngine;
using static GGM.GUI.Elements;
using static GGM.GUI.Settings;

namespace GGM.GUI.Pages
{
    internal class Single
    {
        private static readonly string[] mapStr =
        {
            "[S]Tutorial",
            "[S]Battle training",
            "[S]City",
            "[S]Forest",
            "[S]Forest survive(no crawler)",
            "[S]Forest survive(no crawler no punk)",
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
        private static readonly string[] charsStr = { "LEVI", "MIKASA" };

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 440, Screen.height / 2 - 250, 880, 500));
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(GUILayout.Width(250f));
            Label("Map", LabelType.Header);
            map = GUILayout.SelectionGrid(map, mapStr, 1);
            GUILayout.EndVertical();

            GUILayout.BeginVertical(GUILayout.Width(120));
            Label("Camera Type", LabelType.Header);
            int ss = (int)IN_GAME_MAIN_CAMERA.cameraMode;
            ss = GUILayout.SelectionGrid(ss, new[] { "ORIGINAL", "WOW", "TPS" }, 1);
            IN_GAME_MAIN_CAMERA.cameraMode = (CAMERA_TYPE)ss;

            Label("Daytime", LabelType.Header);
            daytime = GUILayout.SelectionGrid(daytime, new[] { "Day", "Dawn", "Night" }, 1);
            IN_GAME_MAIN_CAMERA.dayLight = (DayLight)daytime;


            Label("Difficulty", LabelType.Header);
            IN_GAME_MAIN_CAMERA.difficulty = GUILayout.SelectionGrid(IN_GAME_MAIN_CAMERA.difficulty,
                new[] { "Normal", "Hard", "Abnormal" }, 1);
            GUILayout.EndVertical();

            GUILayout.Label("", GUILayout.Width(100f));
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
            }

            GUILayout.Label("");
            if (GUILayout.Button("Back", GUILayout.Width(120f), GUILayout.Height(35f)))
            {
                NGUITools.SetActive(UIMainReferences.instance.panelMain.gameObject, true);
            }

            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

    }
}
