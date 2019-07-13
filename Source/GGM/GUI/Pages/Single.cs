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

        private static readonly float[] Proportion = new[] { 0.4f, 0.25f, 0.35f };

        private static int map;

        private static readonly string[] MapStr =
        {
            "[S]Tutorial",
            "[S]Battle training",
            "[S]City",
            "[S]Forest",
            "[S]Forest Survive(no crawler)",
            "[S]Forest Survive(no crawler no punk)",
            "[S]Racing - Akina"
        };

        private static readonly string[] MapNameStr =
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

        private static readonly string[] DaytimeStr = { "Day", "Dawn", "Night" };

        private static int costume;

        private static readonly string[] CostumeStr = { "Cos 1", "Cos 2", "Cos 3" };

        private static int character;

        private static readonly string[] CharacterStr = { "Levi", "Mikasa", "Armin", "Marco", "Jean", "Eren", "Titan_Eren", "Petra", "Sasha", "Set 1", "Set 2", "Set 3" };

        private static int difficulty;

        private static readonly string[] difficultyStr = { "Normal", "Hard", "Abnormal" };

        private static int camera;

        private static readonly string[] CameraStr = { "Original", "WOW", "TPS", "Old TPS" };

        private static GUIStyle box;

        private void OnEnable() 
        {
            if (PlayerPrefs.HasKey("GGM_SingleMap"))
                map = PlayerPrefs.GetInt("GGM_SingleMap");
            if (PlayerPrefs.HasKey("GGM_SingleCamera"))
                camera = PlayerPrefs.GetInt("GGM_SingleCamera");
            if (PlayerPrefs.HasKey("GGM_SingleDaytime"))
                daytime = PlayerPrefs.GetInt("GGM_SingleDaytime");
            if (PlayerPrefs.HasKey("GGM_SingleDifficulty"))
                difficulty = PlayerPrefs.GetInt("GGM_SingleDifficulty");
            if (PlayerPrefs.HasKey("GGM_SingleCharacter"))
                character = PlayerPrefs.GetInt("GGM_SingleCharacter");
            if (PlayerPrefs.HasKey("GGM_SingleMCostume"))
                costume = PlayerPrefs.GetInt("GGM_SingleMCostume");
        }

        public void Save()
        {
            PlayerPrefs.SetInt("GGM_SingleMap", map);
            PlayerPrefs.SetInt("GGM_SingleCamera", camera);
            PlayerPrefs.SetInt("GGM_SingleDaytime", daytime);
            PlayerPrefs.SetInt("GGM_SingleDifficulty", difficulty);
            PlayerPrefs.SetInt("GGM_SingleCharacter", character);
            PlayerPrefs.SetInt("GGM_SingleMCostume", costume);
        }

        private void OnGUI()
        {
            var txt = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            txt.SetPixel(0, 0, new Color(ColorCache.DarkScarlet.Value.r, ColorCache.DarkScarlet.Value.g, ColorCache.DarkScarlet.Value.b, 0.5f));
            txt.Apply();
            box = new GUIStyle { normal = { background = txt } };

            UnityEngine.GUI.Box(new Rect(Screen.width / 2f - (BoxWidth + 10f) / 2f, Screen.height / 2f - (BoxHeight + 10f) / 2f, BoxWidth + 10f, BoxHeight + 10f), ColorCache.Textures[ColorCache.PurpleMunsell], box);
            GUILayout.BeginArea(new Rect(Screen.width / 2f - BoxWidth / 2f, Screen.height / 2f - BoxHeight / 2f, BoxWidth, BoxHeight));

            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();

            Label("Map", LabelType.Header, width: BoxWidth * Proportion[0] - 15f);
            Grid(string.Empty, ref map, MapStr, false, 1, BoxWidth * Proportion[0] - 15f, GridHeight * MapStr.Length + 5 * (MapStr.Length - 1));

            UnityEngine.GUI.DrawTexture(new Rect(4f, GridHeight * MapStr.Length + 5 * (MapStr.Length - 1) + 10f + HeaderHeight, BoxWidth * Proportion[0] - 15f, 148f), ColorCache.Textures[ColorCache.Black]);
            UnityEngine.GUI.DrawTexture(new Rect(4f + 1f, GridHeight * MapStr.Length + 5 * (MapStr.Length - 1) + 10f + HeaderHeight + 1f, BoxWidth * Proportion[0] - 15f - 2f, 148f - 2f), GetImage());

            GUILayout.EndVertical();

            GUILayout.BeginVertical();

            Label("Camera Type", LabelType.Header, width: BoxWidth * Proportion[1] - 5f);
            Grid(string.Empty, ref camera, CameraStr, false, 1, BoxWidth * Proportion[1] - 5f, GridHeight * CameraStr.Length + 5 * (CameraStr.Length - 1));
            Label("Daytime", LabelType.Header, width: BoxWidth * Proportion[1] - 5f);
            Grid(string.Empty, ref daytime, DaytimeStr, false, 1, BoxWidth * Proportion[1] - 5f, GridHeight * DaytimeStr.Length + 5 * (DaytimeStr.Length - 1));
            Label("Difficulty", LabelType.Header, width: BoxWidth * Proportion[1] - 5f);
            Grid(string.Empty, ref difficulty, difficultyStr, false, 1, BoxWidth * Proportion[1] - 5f, GridHeight * difficultyStr.Length + 5 * (difficultyStr.Length - 1));

            GUILayout.EndVertical();

            GUILayout.BeginVertical();

            Label("Character", LabelType.Header, width: BoxWidth * Proportion[2] - 12.5f);
            Grid(string.Empty, ref costume, CostumeStr, width: BoxWidth * Proportion[2] - 12.5f);
            Grid(string.Empty, ref character, CharacterStr, false, 1, BoxWidth * Proportion[2] - 12.5f, GridHeight * CharacterStr.Length + 5 * (CharacterStr.Length - 1));

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Start", GUILayout.Width(120f), GUILayout.Height(35f)))
            {
                Save();
                IN_GAME_MAIN_CAMERA.cameraMode = (CAMERA_TYPE)camera;
                CheckBoxCostume.costumeSet = costume + 1;
                IN_GAME_MAIN_CAMERA.difficulty = difficulty;
                IN_GAME_MAIN_CAMERA.dayLight = (DayLight)daytime;

                IN_GAME_MAIN_CAMERA.singleCharacter = CharacterStr[character].ToUpper();
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
                FengGameManagerMKII.level = MapStr[map];
                Application.LoadLevel(MapNameStr[map]);
                GetInstance<Single>().Disable();
            }
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Back", GUILayout.Width(120f), GUILayout.Height(35f)))
            {
                Save();
                GetInstance<Single>().Disable();
                NGUITools.SetActive(UIMainReferences.instance.panelMain.gameObject, true);
            }

            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }

        private static Texture2D GetImage()
        {
            switch (map)
            {
                case 0:
                case 1:
                    return Styles.Tutorial;

                case 2:
                    Texture2D city;
                    switch (daytime)
                    {
                        case 0:
                            return Styles.CityDay;

                        case 1:
                            return Styles.CityDawn;
                    }
                    return Styles.CityNight;

                case 3:
                case 4:
                case 5:
                    switch (daytime)
                    {
                        case 0:
                            return Styles.ForestDay;

                        case 1:
                            return Styles.ForestDawn;
                    }
                    return Styles.ForestNight;

                default:
                    return Styles.Akina;
            }
        }
    }
}