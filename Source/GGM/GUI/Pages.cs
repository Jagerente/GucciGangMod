using System;
using System.Collections.Generic;
using System.Configuration;
using GGM.Caching;
using UnityEngine;
using static GGM.Config.Settings;

namespace GGM.GUI
{
    internal class Pages : Elements
    {
        #region MainMenu
        private const string size = "72";

        private const string color1 = "D6B1DE";

        private const string color2 = "FFFFFF";

        private static string singleButton = string.Empty;
        private static Rect single = GUIHelpers.AlignRect(375f, 100f, GUIHelpers.Alignment.BOTTOMCENTER, 0f, -300f);

        private static string multiplayerButton = string.Empty;
        private static Rect multiplayer = GUIHelpers.AlignRect(715f, 100f, GUIHelpers.Alignment.BOTTOMCENTER, 0f, -175f);

        private static string quitButton = string.Empty;
        private static Rect quit = GUIHelpers.AlignRect(245f, 100f, GUIHelpers.Alignment.BOTTOMCENTER, 0f, -50f);
        #endregion

        #region SinglePanel

        public static string Page = "Loading Screen";

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
        private static readonly string[] charsStr = {"LEVI", "MIKASA"};

        #endregion

        #region Switchers
        private static int pauseMenuSwitchInt;
        private static int loginSwitchInt;
        private static int serverSwitch;
        private static int videoAndAudioSwitch;
        private static int ambientDayTimeSwitch;
        private static int rebindsSwitch;
        private static int locationSkinsSwitch;
        #endregion

        #region Strings

        private static readonly string[] pauseMenuPages =
        {
            "Game", "Server", "Video & Audio", "Rebinds", "Bombs", "Human Skins", "Titan Skins", "Location Skins",
            "Custom Map", "Custom Logic"
        };
        private static readonly string[] cameraTypes = {"Original", "TPS", "WOW", "OldTPS"};
        private static readonly string[] speedometerTypes = {"Off", "Speed", "Damage"};
        private static readonly string[] ahssSpeedometerTypes = {"Both", "Single", "Double"};
        private static readonly string[] serverPages = {"Titans", "Humans", "Misc"};
        private static readonly string[] healthTypes = {"Fixed", "Static"};
        private static readonly string[] pvpTypes = {"Teams", "FFA"};
        private static readonly string[] teamTypes = {"Off", "Size", "Skill"};
        private static readonly string[] formatOptions = {"Bold", "Italic"};
        private static readonly string[] videoAndAudioPages = {"Video", "Audio"};
        private static readonly string[] textures = {"Low", "Medium", "High"};
        private static readonly string[] anisotropicFiltering = {"Off", "On", "Forced"};
        private static readonly string[] antiAliasing = {"Off", "2x", "4x", "8x"};
        private static readonly string[] blendWeights = {"One", "Two", "Four"};
        private static readonly string[] shadowProjection = {"Stable Fit", "Close Fit"};
        private static readonly string[] shadowCascades = {"0", "2", "4"};
        private static readonly string[] dayTime = {"Day", "Dawn", "Night"};
        private static readonly string[] rebindsPages = {"Human", "Titan", "Mount"};
        private static readonly string[] humanRebinds =
        {
            "Forward", "Backward", "Left", "Right", "Jump", "Dodge", "Left Hook", "Right Hook", "Both Hooks",
            "Lock", "Attack", "Special", "Salute", "Change Camera", "Restart/Suicide", "Menu", "Show/Hide Cursor",
            "Fullscreen", "Reload", "Flare Green", "Flare Red", "Flare Black", "Reel In", "Reel Out", "Dash",
            "Minimap Maximize", "Minimap Toggle", "Minimap Reset", "Chat", "Live Spectate"
        };
        private static readonly string[] titanRebinds =
        {
            "Forward", "Backward", "Left", "Right", "Walk", "Jump", "Punch", "Slam", "Grab Front", "Grab Back",
            "Grab Nape", "Slap", "Bite", "Cover Nape"
        };
        private static readonly string[] horseRebinds =
        {
            "Forward", "Backward", "Left", "Right", "Walk", "Jump", "Mount"
        };
        private static readonly string[] cannonRebinds =
        {
            "Rotate Up:", "Rotate Down:", "Rotate Left:", "Rotate Right:", "Fire:", "Mount:", "Slow Rotate:"
        };
        private static readonly string[] bombStats = {"Radius", "Range", "Speed", "Cooldown"};
        private static readonly string[] skinsAppearanceType = {"Off", "Local", "Global"};
        private static readonly string[] bladeTrailsAppearance = {"Legacy", "Liquid"};
        private static readonly string[] humanSkinFields =
        {
            "Horse", "Hair", "Eyes", "Glass", "Face", "Skin", "Costume", "Hoodie", "Left 3DMG", "Right 3DMG",
            "Gas", "Logo & Cape", "Weapon Trail"
        };

        private static readonly string[] locationSkinsLocation = {"Forest", "City"};

        private static readonly string[] locationSkinForestFields =
            {
                "Ground", "Forest Trunk #1", "Forest Trunk #2", "Forest Trunk #3", "Forest Trunk #4",
                "Forest Trunk #5", "Forest Trunk #6", "Forest Trunk #7", "Forest Trunk #8", "Forest Leave #1",
                "Forest Leave #2", "Forest Leave #3", "Forest Leave #4", "Forest Leave #5", "Forest Leave #6",
                "Forest Leave #7", "Forest Leave #8", "Skybox Front", "Skybox Back", "Skybox Left", "Skybox Right",
                "Skybox Up", "Skybox Down"
            };

        private static readonly string[] locationSkinCityFields =
        {
            "Ground", "Wall", "Gate", "Houses #1", "Houses #2", "Houses #3", "Houses #4", "Houses #5",
            "Houses #6", "Houses #7", "Houses #8", "Skybox Front", "Skybox Back", "Skybox Left", "Skybox Right",
            "Skybox Up", "Skybox Down", 
        };
        #endregion

        #region Scrolls
        private static Vector2 scrollGameLeft = Vector2.zero;
        private static Vector2 scrollServerTitansLeft = Vector2.zero;
        private static Vector2 scrollServerMiscLeft = Vector2.zero;
        private static Vector2 scrollHumanSkins = Vector2.zero;
        private static Vector2 scrollLocationSkinsForestLeft = Vector2.zero;
        private static Vector2 scrollLocationSkinsForestRight = Vector2.zero;
        private static Vector2 scrollLocationSkinsCityLeft = Vector2.zero;
        private static Vector2 scrollLocationSkinsCityRight = Vector2.zero;
        #endregion


        public static void LoadingScreen()
        {
            UnityEngine.GUI.backgroundColor = new Color(0f, 0f, 0f, 1f);
            UnityEngine.GUI.Box(new Rect(0, 0, Screen.width, Screen.height), string.Empty);
            UnityEngine.GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height),
                FengGameManagerMKII.FGM.textureBackgroundBlack);
            UnityEngine.GUI.DrawTexture(GUIHelpers.AlignRect(192, 192, GUIHelpers.Alignment.CENTER), Styles.Logo);
            UnityEngine.GUI.Label(GUIHelpers.AlignRect(600, 150, GUIHelpers.Alignment.BOTTOMCENTER),
                "<size=64>GucciGangMod</size>\n" + "<size=32>Loading</size>", LabelStyle[1]);
        }

        public static void MainMenu()
        {
            #region Single

            if (UnityEngine.GUI.Button(single, singleButton, "label"))
            {
                Page = "Singlee";
                NGUITools.SetActive(GameObjectCache.Find("UIRefer").GetComponent<UIMainReferences>().panelSingleSet,
                    true);
                NGUITools.SetActive(UIMainReferences.instance.panelMain.gameObject, false);
            }

            singleButton = single.Contains(GUIHelpers.mousePos)
                ? $"<color=#{color1}><size={size}><b><i>S I N G L E</i></b></size></color>"
                : $"<color=#{color2}><size={size}><b><i>S I N G L E</i></b></size></color>";

            #endregion

            #region Multiplayer

            if (UnityEngine.GUI.Button(multiplayer, multiplayerButton, "label"))
            {
                NGUITools.SetActive(GameObjectCache.Find("UIRefer").GetComponent<UIMainReferences>().panelMultiStart,
                    true);
                NGUITools.SetActive(UIMainReferences.instance.panelMain.gameObject, false);
                Page = "Multiplayer";
            }

            multiplayerButton = multiplayer.Contains(GUIHelpers.mousePos)
                ? $"<color=#{color1}><size={size}><b><i>M U L T I P L A Y E R</i></b></size></color>"
                : $"<color=#{color2}><size={size}><b><i>M U L T I P L A Y E R</i></b></size></color>";

            #endregion

            #region Quit

            if (UnityEngine.GUI.Button(quit, quitButton, "label"))
            {
                Application.Quit();
            }

            quitButton = quit.Contains(GUIHelpers.mousePos)
                ? $"<color=#{color1}><size={size}><b><i>Q U I T</i></b></size></color>"
                : $"<color=#{color2}><size={size}><b><i>Q U I T</i></b></size></color>";

            #endregion

            #region Top Left Navigation Panel

            GUILayout.BeginArea(GUIHelpers.AlignRect(400, 400, GUIHelpers.Alignment.TOPLEFT, 10, 10));
            GUILayout.BeginHorizontal();

            loginSwitchInt = GUILayout.SelectionGrid(loginSwitchInt, new []{ "<size=18>User</size>", "<size=18>Servers</size>" }, 2, GUILayout.Width(400), GUILayout.Height(50));
            GUILayout.EndHorizontal();

            switch (loginSwitchInt)
            {
                case 0:

                    #region Custom Name

                    GUILayout.BeginHorizontal();
                    GUILayout.Label("<size=16>Name:</size>", GUILayout.Width(115));
                    FengGameManagerMKII.nameField =
                        GUILayout.TextField(FengGameManagerMKII.nameField, GUILayout.Width(180));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("<size=16>Guild:</size>", GUILayout.Width(115));
                    LoginFengKAI.player.guildname =
                        GUILayout.TextArea(LoginFengKAI.player.guildname, GUILayout.Width(180));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("<size=16>Save</size>", GUILayout.Width(147)))
                    {
                        PlayerPrefs.SetString("Name", FengGameManagerMKII.nameField);
                        PlayerPrefs.SetString("Guild", LoginFengKAI.player.guildname);
                    }

                    if (GUILayout.Button("<size=16>Load</size>", GUILayout.Width(147)))
                    {
                        FengGameManagerMKII.nameField = PlayerPrefs.GetString("Name", string.Empty);
                        LoginFengKAI.player.guildname = PlayerPrefs.GetString("Guild", string.Empty);
                    }

                    GUILayout.EndHorizontal();
                    break;

                #endregion

                case 1:

                    #region Server Type

                    if (UIMainReferences.ServerKey == UIMainReferences.PublicKey)
                    {
                        GUILayout.Label("<size=24><b><i>Connected to Public server.</i></b></size>",
                            GUILayout.Width(400));
                    }
                    else if (UIMainReferences.ServerKey == FengGameManagerMKII.s[0])
                    {
                        GUILayout.Label("<size=24><b><i>Connected to RC Private server.</i></b></size>",
                            GUILayout.Width(400));
                    }
                    else
                    {
                        if (FengGameManagerMKII.privateServerField != string.Empty)
                        {
                            GUILayout.Label(
                                "<size=24><b><i>Connected to " + FengGameManagerMKII.privateServerField +
                                " server.</i></b></size>", GUILayout.Width(400));
                        }
                        else
                        {
                            GUILayout.Label("<size=24><b><i>Connected to Custom server.</i></b></size>",
                                GUILayout.Width(400));
                        }
                    }

                    GUILayout.BeginHorizontal();
                    GUILayout.Label("<size=16>Public Server</size>", GUILayout.Width(115));
                    if (GUILayout.Button("<size=18><b>Connect</b></size>", GUILayout.Width(280)))
                    {
                        UIMainReferences.ServerKey = UIMainReferences.PublicKey;
                    }

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("<size=16>RC Private</size>", GUILayout.Width(115));
                    if (GUILayout.Button("<size=18><b>Connect</b></size>", GUILayout.Width(280)))
                    {
                        UIMainReferences.ServerKey = FengGameManagerMKII.s[0];
                    }

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    FengGameManagerMKII.privateServerField = GUILayout.TextField(FengGameManagerMKII.privateServerField,
                        50, GUILayout.Width(115));
                    if (GUILayout.Button("<size=18><b>Connect</b></size>", GUILayout.Width(280)))
                    {
                        UIMainReferences.ServerKey = FengGameManagerMKII.privateServerField;
                    }

                    GUILayout.EndHorizontal();
                    break;

                #endregion
            }

            GUILayout.EndArea();

            #endregion

            #region Top Right Navigation Panel

            if (UnityEngine.GUI.Button(GUIHelpers.AlignRect(128f, 25f, GUIHelpers.Alignment.TOPRIGHT, -5f, 15f),
                "Level Editor")) //15,128,25
            {
                FengGameManagerMKII.settings[64] = 101;
                Application.LoadLevel(2);
                Page = "Level Editor";
            }
            else if (UnityEngine.GUI.Button(GUIHelpers.AlignRect(128f, 25f, GUIHelpers.Alignment.TOPRIGHT, -5f, 45f),
                "Custom Characters")) //45f, 128,25f
            {
                Application.LoadLevel("characterCreation");
                Page = "Custom Characters";
            }
            else if (UnityEngine.GUI.Button(GUIHelpers.AlignRect(128f, 25f, GUIHelpers.Alignment.TOPRIGHT, -5f, 75f),
                "Snapshot Reviewer")) //75
            {
                Application.LoadLevel("SnapShot");
                Page = "Snapshot Reviewer";
            }

            #endregion
        }

        public static void Single()
        {
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 440, Screen.height / 2 - 250, 880, 500));
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(GUILayout.Width(250f));
            Label("Map", LabelType.Header);
            map = GUILayout.SelectionGrid(map, mapStr, 1);
            GUILayout.EndVertical();

            GUILayout.BeginVertical(GUILayout.Width(120));
            Label("Camera Type", LabelType.Header);
            int ss = (int) IN_GAME_MAIN_CAMERA.cameraMode;
            ss = GUILayout.SelectionGrid(ss, new[] {"ORIGINAL", "WOW", "TPS"}, 1);
            IN_GAME_MAIN_CAMERA.cameraMode = (CAMERA_TYPE) ss;

            Label("Daytime", LabelType.Header);
            daytime = GUILayout.SelectionGrid(daytime, new[] {"Day", "Dawn", "Night"}, 1);
            IN_GAME_MAIN_CAMERA.dayLight = (DayLight) daytime;


            Label("Difficulty", LabelType.Header);
            IN_GAME_MAIN_CAMERA.difficulty = GUILayout.SelectionGrid(IN_GAME_MAIN_CAMERA.difficulty,
                new[] {"Normal", "Hard", "Abnormal"}, 1);
            GUILayout.EndVertical();

            GUILayout.Label("", GUILayout.Width(100f));
            GUILayout.BeginVertical();
            Label("Character", LabelType.Header);
            costume = GUILayout.SelectionGrid(costume, new[] {"Cos 1", "Cos 2", "Cos 3"}, 3);
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

                Page = string.Empty;
            }

            GUILayout.Label("");
            if (GUILayout.Button("Back", GUILayout.Width(120f), GUILayout.Height(35f)))
            {
                NGUITools.SetActive(UIMainReferences.instance.panelMain.gameObject, true);
            }

            GUILayout.EndHorizontal();
            GUILayout.EndArea();

        }

        public static void Multiplayer()
        {

        }

        public static void PauseMenu()
        {
            UnityEngine.GUI.Box(new Rect(leftPos, topPos, width, height), string.Empty);

            pauseMenuSwitchInt = UnityEngine.GUI.SelectionGrid(new Rect(leftPos + 5f, topPos + 5f, width - 10f, 50f), pauseMenuSwitchInt, pauseMenuPages, 5);

            switch (pauseMenuSwitchInt)
            {
                case 0:
                    Game();
                    break;
                case 1:
                    Server();
                    break;
                case 2:
                    VideoAndAudio();
                    break;
                case 3:
                    Rebinds();
                    break;
                case 4:
                    Bombs();
                    break;
                case 5:
                    HumanSkins();
                    break;
                case 6:
                    TitanSkins();
                    break;
                case 7:
                    LocationSkins();
                    break;
                case 8:
                    CustomMap();
                    break;
                case 9:
                    CustomLogic();
                    break;
            }

        }

        public static void CharacterCreation()
        {

        }

        private static void Game()
        {
            GUILayout.BeginArea(left[0]);
            GUILayout.Space(15f);
            scrollGameLeft = GUILayout.BeginScrollView(scrollGameLeft);

            Label("Mouse", LabelType.Header);
            Slider("Sensitivity", ref MouseSensitivitySetting.Value, 10f, 100f, round: true);
            Grid("Invert Y", ref MouseInvertYSetting.Value);
            Label("Camera", LabelType.Header);
            ButtonToggle(string.Empty, cameraTypes, CameraTypeSettings);
            Slider("Distance", ref CameraDistanceSetting.Value, 0f, 100f, round: true);
            Grid("Tilt", ref CameraTiltSetting.Value);
            Grid("Static FOV", ref CameraStaticFOVSetting.Value);
            if (CameraStaticFOVSetting) Slider("• FOV", ref CameraFOVSetting.Value, 60f, 120f, round: true);
            Label("Snapshots", LabelType.Header);
            Grid("Snapshots", ref SnapshotsSetting.Value);
            if (SnapshotsSetting)
            {
                Grid("• Show In Game", ref SnapshotsShowInGameSetting.Value);
                TextField("• Minimum Damage", ref SnapshotsMinimumDamageSetting.Value);
            }
            Label("Resources", LabelType.Header);
            Grid("Infinite Blades", ref InfiniteBladesSetting.Value);
            Grid("Infinite Bullets", ref InfiniteBulletsSetting.Value);
            Grid("Infinite Gas", ref InfiniteGasSetting.Value);

            GUILayout.EndScrollView();
            GUILayout.EndArea();

            GUILayout.BeginArea(right[0]);
            GUILayout.Space(15f);

            Label("User Interface", LabelType.Header);
            Grid("Hide Everything", ref UserInterfaceSetting.Value);
            if (!UserInterfaceSetting)
            {
                Grid("Player List", ref PlayerListUISetting.Value);
                Grid("Damage Feed", ref DamageFeedUISetting.Value);
                Grid("Game Info", ref GameInfoUISetting.Value);
                Grid("Chat", ref ChatUISetting.Value);
                Grid("Crosshair", ref CrosshairUISetting.Value);
                Grid("Sprites", ref SpritesUISetting.Value);
            }
            Label("Misc", LabelType.Header);
            Grid("Chat Feed", ref ChatFeedSetting.Value);
            Grid("Minimap", ref MinimapSetting.Value);
            Grid("Speedometer", ref SpeedometerSetting.Value, speedometerTypes);
            if (SpeedometerSetting == 2) Grid("• AHSS Damage", ref SpeedometerAHSSSetting.Value, ahssSpeedometerTypes);

            GUILayout.EndArea();
        }

        private static void Server()
        {
            GUILayout.BeginArea(center[1]);
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            serverSwitch = GUILayout.SelectionGrid(serverSwitch, serverPages, 3, GUILayout.Width(225f), GUILayout.Height(25f));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.EndArea();

            switch (serverSwitch)
            {
                case 0:
                    GUILayout.BeginArea(left[2]);
                    scrollServerTitansLeft = GUILayout.BeginScrollView(scrollServerTitansLeft);
                    Label("General", LabelType.Header);
                    Grid("Custom Starter Titans", ref CustomStarterTitansSetting.Value);
                    if (CustomStarterTitansSetting) TextField("• Amount", ref StarterAmountSetting.Value);
                    Grid("Custom Titans/Wave", ref CustomTitansPerWaveSetting.Value);
                    if (CustomTitansPerWaveSetting) TextField("• Amount", ref TitansPerWaveSetting.Value);
                    Grid("Custom Spawn Rate", ref CustomSpawnRateSetting.Value);
                    if (CustomSpawnRateSetting)
                    {
                        float[] freePoints =
                        {
                            100f - (SpawnRateSettings[1] + SpawnRateSettings[2] + SpawnRateSettings[3] + SpawnRateSettings[4]),
                            100f - (SpawnRateSettings[0] + SpawnRateSettings[2] + SpawnRateSettings[3] + SpawnRateSettings[4]),
                            100f - (SpawnRateSettings[0] + SpawnRateSettings[1] + SpawnRateSettings[3] + SpawnRateSettings[4]),
                            100f - (SpawnRateSettings[0] + SpawnRateSettings[1] + SpawnRateSettings[2] + SpawnRateSettings[4]),
                            100f - (SpawnRateSettings[0] + SpawnRateSettings[1] + SpawnRateSettings[2] + SpawnRateSettings[3])
                        };
                        string[] types = { "• Normal", "• Abnormal", "• Jumper", "• Crawler", "• Punk" };
                        for (var i = 0; i < SpawnRateSettings.Length; i++)
                        {
                            Slider(types[i], ref SpawnRateSettings[i].Value, 0f, freePoints[i], customValueText: true, valueText: Math.Round(SpawnRateSettings[i]) + "%");
                        }

                        Grid("• Punk Waves", ref PunkWavesSetting.Value);
                    }
                    Grid("Custom Size", ref CustomSizeSetting.Value);
                    if (CustomSizeSetting)
                    {
                        TextField("• Minimum", ref SizeSettings[0].Value);
                        TextField("• Maximum", ref SizeSettings[1].Value);
                    }
                    Grid("Custom Waves", ref CustomWavesSetting.Value);
                    if (CustomWavesSetting) TextField("• Waves", ref MaximumWavesSetting.Value);
                    Grid("Disable Rock-Throwing", ref DisableRockThrowingSetting.Value);
                    GUILayout.EndScrollView();
                    GUILayout.EndArea();

                    GUILayout.BeginArea(right[2]);
                    Label("Modes", LabelType.Header);
                    Grid("Health Mode", ref HealthModeSetting.Value);
                    if (HealthModeSetting)
                    {
                        Grid("• Type", ref HealthSettings[0].Value, healthTypes);
                        TextField("• Minimum", ref HealthSettings[1].Value);
                        TextField("• Maximum", ref HealthSettings[2].Value);
                    }
                    Grid("Armor Mode", ref ArmorModeSetting.Value);
                    if (ArmorModeSetting) TextField("• Damage", ref ArmorSetting.Value);
                    Grid("Explode Mode", ref ExplodeModeSetting.Value);
                    if (ExplodeModeSetting) TextField("• Radius", ref ExplodeRadiusSetting.Value);
                    GUILayout.EndArea();
                    break;
                case 1:
                    GUILayout.BeginArea(left[2]);
                    Label("PVP", LabelType.Header);
                    Grid("PVP Mode", ref PVPModeSetting.Value);
                    if (PVPModeSetting) Grid("• Type", ref PVPTypeSetting.Value, pvpTypes);
                    Grid("Points Mode", ref PointsModeSetting.Value);
                    if (PointsModeSetting) TextField("• Limit", ref PointsLimitSetting.Value);
                    Grid("Team Mode", ref TeamModeSetting.Value);
                    if (TeamModeSetting)
                    {
                        Grid("• Sort", ref TeamSortSetting.Value, teamTypes);

                    }
                    Grid("Bombs Mode", ref BombsModeSetting.Value);
                    Grid("Infection Mode", ref InfectionModeSetting.Value);
                    if (InfectionModeSetting) TextField("• Infected", ref InfectedTitansSetting.Value);
                    Grid("Friendly Mode", ref FriendlyModeSetting.Value);
                    GUILayout.EndArea();

                    GUILayout.BeginArea(right[2]);
                    Label("Other", LabelType.Header);
                    Grid("Auto Revive", ref AutoReviveSetting.Value);
                    if (AutoReviveSetting) TextField("• Seconds", ref AutoReviveTimeSetting.Value);
                    Grid("Horses", ref HorsesSetting.Value);
                    Grid("Disable Minimaps", ref DisableMinimapsSetting.Value);
                    Grid("No AHSS Air-Reloading", ref DisableAHSSAirReloadingSetting.Value);
                    Grid("Deadly Cannons Mode", ref DeadlyCannonsModeSetting.Value);
                    GUILayout.EndArea();
                    break;
                case 2:
                    GUILayout.BeginArea(left[2]);
                    scrollServerMiscLeft = GUILayout.BeginScrollView(scrollServerMiscLeft);
                    Label("Chat", LabelType.Header);
                    TextField("Size", ref ChatSizeSetting.Value);
                    TextField("Major Color", ref ChatMajorColorSetting.Value);
                    TextField("Minor Color", ref ChatMinorColorSetting.Value);
                    ButtonToggle("Major Format", formatOptions, ChatMajorFormatSettings);
                    ButtonToggle("Minor Format", formatOptions, ChatMinorFormatSettings);
                    Label("Welcome Message", LabelType.Header);
                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Bold", GUILayout.Width(75f)))
                    {
                        WelcomeMessageSetting.Value += "<b></b>";
                    }
                    if (GUILayout.Button("Italic", GUILayout.Width(75f)))
                    {
                        WelcomeMessageSetting.Value += "<i></i>";
                    }
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                    WelcomeMessageSetting.Value = GUILayout.TextArea(WelcomeMessageSetting, GUILayout.Width(halfAreaWidth - 20f));
                    GUILayout.EndScrollView();
                    GUILayout.EndArea();

                    GUILayout.BeginArea(right[2]);
                    Label("Protection", LabelType.Header);
                    Grid("Anti Titan Eren", ref AntiTitanErenSetting.Value);
                    GUILayout.EndArea();
                    break;
            }
        }

        private static void VideoAndAudio()
        {
            GUILayout.BeginArea(center[1]);
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            videoAndAudioSwitch = GUILayout.SelectionGrid(videoAndAudioSwitch, videoAndAudioPages, 2, GUILayout.Width(175f), GUILayout.Height(25f));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.EndArea();

            switch (videoAndAudioSwitch)
            {
                case 0:
                    GUILayout.BeginArea(left[2]);
                    GUILayout.BeginVertical();
                    Label("General", LabelType.Header, GUIHelpers.Alignment.CENTER);
                    Slider("Overall Quality", ref OverallQualitySetting.Value, 0f, 5f, round: true);
                    Grid("Textures", ref TexturesSetting.Value, textures);
                    TextField("FPS Lock", ref FPSLockSetting.Value);
                    Label("Advanced", LabelType.Header, GUIHelpers.Alignment.CENTER);
                    Grid("Anisotropic Filtering", ref AnisotropicFilteringSetting.Value, anisotropicFiltering);
                    Grid("Anti-Aliasing", ref AntiAliasingSetting.Value, antiAliasing);
                    Grid("Blend Weights", ref BlendWeightsSetting.Value, blendWeights);
                    Grid("Mip Mapping", ref MipMappingSetting.Value);
                    Slider("Draw Distance", ref DrawDistanceSetting.Value, 1000f, 15000f, valueTextFormat: "0");
                    Slider("Shadow Distance", ref ShadowDistanceSetting.Value, 0f, 15000f, valueTextFormat: "0");
                    Grid("Shadow Projection", ref ShadowProjectionSetting.Value, shadowProjection);
                    Grid("Shadow Cascades", ref ShadowCascadesSetting.Value, shadowCascades);
                    GUILayout.EndVertical();
                    GUILayout.EndArea();

                    GUILayout.BeginArea(right[2]);
                    GUILayout.BeginVertical();
                    Label("Visuals", LabelType.Header);
                    Grid("Wind", ref WindSetting.Value);
                    Grid("Blur", ref BlurSetting.Value);
                    Grid("Ambient", ref AmbientSetting.Value);
                    if (AmbientSetting)
                    {
                        Grid("Day Time", ref ambientDayTimeSwitch, dayTime);
                        switch (ambientDayTimeSwitch)
                        {
                            case 0:
                                Slider("Color R", ref AmbientColorSetting[0][0].Value, 0f, 1f, 160f, 25f);
                                Slider("Color G", ref AmbientColorSetting[0][1].Value, 0f, 1f, 160f, 25f);
                                Slider("Color B", ref AmbientColorSetting[0][2].Value, 0f, 1f, 160f, 25f);
                                break;
                            case 1:
                                Slider("Color R", ref AmbientColorSetting[1][0].Value, 0f, 1f, 160f, 25f);
                                Slider("Color G", ref AmbientColorSetting[1][1].Value, 0f, 1f, 160f, 25f);
                                Slider("Color B", ref AmbientColorSetting[1][2].Value, 0f, 1f, 160f, 25f);
                                break;
                            case 2:
                                Slider("Color R", ref AmbientColorSetting[2][0].Value, 0f, 1f, 160f, 25f);
                                Slider("Color G", ref AmbientColorSetting[2][1].Value, 0f, 1f, 160f, 25f);
                                Slider("Color B", ref AmbientColorSetting[2][2].Value, 0f, 1f, 160f, 25f);
                                break;
                        }
                    }
                    Grid("Fog", ref FogSetting.Value);
                    if (FogSetting)
                    {
                        Slider("Color R", ref FogColorSettings[0].Value, 0f, 1f, 160f, 25f);
                        Slider("Color G", ref FogColorSettings[1].Value, 0f, 1f, 160f, 25f);
                        Slider("Color B", ref FogColorSettings[2].Value, 0f, 1f, 160f, 25f);
                        if (FogDistanceSettings[0] > FogDistanceSettings[1] && FogDistanceSettings[0] != 0f)
                            FogDistanceSettings[0].Value = FogDistanceSettings[1] - 0.1f;
                        if (FogDistanceSettings[0] < 0)
                            FogDistanceSettings[0].Value = 0;
                        if (FogDistanceSettings[1] > DrawDistanceSetting)
                            FogDistanceSettings[1].Value = DrawDistanceSetting - 0.1f;
                        Slider("Start Distance", ref FogDistanceSettings[0].Value, 0f, ref DrawDistanceSetting.Value, 160f, 25f,
                            valueTextFormat: "0");
                        Slider("End Distance", ref FogDistanceSettings[1].Value, 0f, ref DrawDistanceSetting.Value, 160f, 25f,
                            valueTextFormat: "0");
                    }
                    GUILayout.EndVertical();
                    GUILayout.EndArea();

                    break;
                case 1:
                    GUILayout.BeginArea(left[2]);
                    Label("General", LabelType.Header);
                    Slider("Global Volume", ref GlobalVolumeSetting.Value, 0f, 1f, round: true, multiplier: 100f);
                    Label("Human", LabelType.Header);
                    Slider("AHSS Shot Volume", ref AHSSShotVolumeSetting.Value, 0f, 1f, round: true, multiplier: 100f);
                    Slider("Air Slash Volume", ref AirSlashVolumeSetting.Value, 0f, 1f, round: true, multiplier: 100f);
                    Slider("Nape Slash Volume", ref NapeSlashVolumeSetting.Value, 0f, 1f, round: true, multiplier: 100f);
                    Slider("Body Slash Volume", ref BodySlashVolumeSetting.Value, 0f, 1f, round: true, multiplier: 100f);
                    Slider("Hook Volume", ref HookVolumeSetting.Value, 0f, 1f, round: true, multiplier: 100f);
                    Label("Titan", LabelType.Header);
                    Slider("Titan Eren Roar Volume", ref TitanErenRoarVolumeSetting.Value, 0f, 1f, round: true, multiplier: 100f);
                    Slider("Swing Volume", ref SwingVolumeSetting.Value, 0f, 1f, round: true, multiplier: 100f);
                    Slider("Thunder Volume", ref ThunderVolumeSetting.Value, 0f, 1f, round: true, multiplier: 100f);
                    Slider("Head Explosion Volume", ref HeadExplosionVolumeSetting.Value, 0f, 1f, round: true, multiplier: 100f);
                    Slider("Head Punch Volume", ref HeadPunchVolumeSetting.Value, 0f, 1f, round: true, multiplier: 100f);
                    Slider("Boom Volume", ref BoomVolumeSetting.Value, 0f, 1f, round: true, multiplier: 100f);
                    Slider("Step Volume", ref StepVolumeSetting.Value, 0f, 1f, round: true, multiplier: 100f);
                    GUILayout.EndArea();
                    break;
            }
        }

        private static void Rebinds()
        {
            if (FengGameManagerMKII.inputManager == null) return;

            GUILayout.BeginArea(center[1]);
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            rebindsSwitch = GUILayout.SelectionGrid(rebindsSwitch, rebindsPages, 3, GUILayout.Height(25f));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.EndArea();

            Event current;
            bool isPressed;

            switch (rebindsSwitch)
            {
                case 0:
                {
                    GUILayout.BeginArea(left[2]);
                    for (var i = 0; i < humanRebinds.Length / 2; i++)
                    {
                        GUILayout.BeginHorizontal();
                        var width = 225f;

                        Label(humanRebinds[i], width: width);

                        if (GUILayout.Button(FengGameManagerMKII.inputManager.getKeyRC(i)))
                        {
                            FengGameManagerMKII.settings[100] = i + 1;
                            FengGameManagerMKII.inputManager.setNameRC(i, "waiting...");
                        }

                        GUILayout.EndHorizontal();
                    }

                    GUILayout.EndArea();

                    GUILayout.BeginArea(right[2]);
                    for (var i = humanRebinds.Length / 2; i < humanRebinds.Length; i++)
                    {
                        GUILayout.BeginHorizontal();
                        float width = 225f;
                        if (i == 22 || i == 23 || i == 24) width = 170f;
                        Label(humanRebinds[i], width: width);
                        if (i < 22)
                        {
                            if (GUILayout.Button(FengGameManagerMKII.inputManager.getKeyRC(i)))
                            {
                                FengGameManagerMKII.settings[100] = i + 1;
                                FengGameManagerMKII.inputManager.setNameRC(i, "waiting...");
                            }
                        }
                        else
                        {
                            var k = i == 22 ? 98 :
                                i == 23 ? 99 :
                                i == 24 ? 182 :
                                i == 25 ? 232 :
                                i == 26 ? 233 :
                                i == 27 ? 234 :
                                i == 28 ? 236 : 262;

                            var style = new GUIStyle(UnityEngine.GUI.skin.button);
                            string[] txt = {"Off", "On"};
                            if (i == 22)
                            {
                                style.normal = ReelingSettings[0]
                                    ? UnityEngine.GUI.skin.button.onNormal
                                    : UnityEngine.GUI.skin.button.normal;
                                style.hover = ReelingSettings[0]
                                    ? UnityEngine.GUI.skin.button.onHover
                                    : UnityEngine.GUI.skin.button.hover;
                                if (GUILayout.Button(ReelingSettings[0] ? txt[1] : txt[0], style, GUILayout.Width(50f)))
                                {
                                    ReelingSettings[0].Value = !ReelingSettings[0];
                                }
                            }
                            else if (i == 23)
                            {
                                style.normal = ReelingSettings[1]
                                    ? UnityEngine.GUI.skin.button.onNormal
                                    : UnityEngine.GUI.skin.button.normal;
                                style.hover = ReelingSettings[1]
                                    ? UnityEngine.GUI.skin.button.onHover
                                    : UnityEngine.GUI.skin.button.hover;
                                if (GUILayout.Button(ReelingSettings[1] ? txt[1] : txt[0], style, GUILayout.Width(50f)))
                                {
                                    ReelingSettings[1].Value = !ReelingSettings[1];
                                }
                            }
                            else if (i == 24)
                            {
                                style.normal = DashSetting
                                    ? UnityEngine.GUI.skin.button.onNormal
                                    : UnityEngine.GUI.skin.button.normal;
                                style.hover = DashSetting
                                    ? UnityEngine.GUI.skin.button.onHover
                                    : UnityEngine.GUI.skin.button.hover;
                                if (GUILayout.Button(DashSetting ? txt[1] : txt[0], style, GUILayout.Width(50f)))
                                {
                                    DashSetting.Value = !DashSetting;
                                }
                            }

                            if (GUILayout.Button((string) FengGameManagerMKII.settings[k]))
                            {
                                FengGameManagerMKII.settings[k] = "waiting...";
                                FengGameManagerMKII.settings[100] = k;
                            }
                        }

                        GUILayout.EndHorizontal();
                    }

                    GUILayout.EndArea();

                    if ((int) FengGameManagerMKII.settings[100] != 0)
                    {
                        current = Event.current;
                        isPressed = false;
                        var str4 = "waiting...";
                        if (current.type == EventType.KeyDown && current.keyCode != KeyCode.None)
                        {
                            isPressed = true;
                            str4 = current.keyCode.ToString();
                        }
                        else if (Input.GetKey(KeyCode.LeftShift))
                        {
                            isPressed = true;
                            str4 = KeyCode.LeftShift.ToString();
                        }
                        else if (Input.GetKey(KeyCode.RightShift))
                        {
                            isPressed = true;
                            str4 = KeyCode.RightShift.ToString();
                        }
                        else if (Input.GetAxis("Mouse ScrollWheel") != 0f)
                        {
                            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                            {
                                isPressed = true;
                                str4 = "Scroll Up";
                            }
                            else
                            {
                                isPressed = true;
                                str4 = "Scroll Down";
                            }
                        }
                        else
                        {
                            for (var i = 0; i < 7; i++)
                            {
                                if (Input.GetKeyDown((KeyCode) (323 + i)))
                                {
                                    isPressed = true;
                                    str4 = "Mouse" + Convert.ToString(i);
                                }
                            }
                        }

                        if (isPressed)
                        {
                            if ((int) FengGameManagerMKII.settings[100] == 98)
                            {
                                FengGameManagerMKII.settings[98] = str4;
                                FengGameManagerMKII.settings[100] = 0;
                                FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.reelin, str4);
                            }
                            else if ((int) FengGameManagerMKII.settings[100] == 99)
                            {
                                FengGameManagerMKII.settings[99] = str4;
                                FengGameManagerMKII.settings[100] = 0;
                                FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.reelout, str4);
                            }
                            else if ((int) FengGameManagerMKII.settings[100] == 182)
                            {
                                FengGameManagerMKII.settings[182] = str4;
                                FengGameManagerMKII.settings[100] = 0;
                                FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.dash, str4);
                            }
                            else if ((int) FengGameManagerMKII.settings[100] == 232)
                            {
                                FengGameManagerMKII.settings[232] = str4;
                                FengGameManagerMKII.settings[100] = 0;
                                FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.mapMaximize, str4);
                            }
                            else if ((int) FengGameManagerMKII.settings[100] == 233)
                            {
                                FengGameManagerMKII.settings[233] = str4;
                                FengGameManagerMKII.settings[100] = 0;
                                FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.mapToggle, str4);
                            }
                            else if ((int) FengGameManagerMKII.settings[100] == 234)
                            {
                                FengGameManagerMKII.settings[234] = str4;
                                FengGameManagerMKII.settings[100] = 0;
                                FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.mapReset, str4);
                            }
                            else if ((int) FengGameManagerMKII.settings[100] == 236)
                            {
                                FengGameManagerMKII.settings[236] = str4;
                                FengGameManagerMKII.settings[100] = 0;
                                FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.chat, str4);
                            }
                            else if ((int) FengGameManagerMKII.settings[100] == 262)
                            {
                                FengGameManagerMKII.settings[262] = str4;
                                FengGameManagerMKII.settings[100] = 0;
                                FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.liveCam, str4);
                            }
                            else
                            {
                                for (var i = 0; i < 22; i++)
                                {
                                    var k = i + 1;
                                    if ((int) FengGameManagerMKII.settings[100] == k)
                                    {
                                        FengGameManagerMKII.inputManager.setKeyRC(i, str4);
                                        FengGameManagerMKII.settings[100] = 0;
                                    }
                                }
                            }
                        }
                    }

                    break;
                }

                case 1:
                {
                    GUILayout.BeginArea(left[2]);
                    for (var i = 0; i < titanRebinds.Length / 2; i++)
                    {
                        var k = 101 + i;
                        GUILayout.BeginHorizontal();
                        Label(titanRebinds[i], width: 225f);
                        if (GUILayout.Button((string) FengGameManagerMKII.settings[k]))
                        {
                            FengGameManagerMKII.settings[k] = "waiting...";
                            FengGameManagerMKII.settings[100] = k;
                        }

                        GUILayout.EndHorizontal();
                    }

                    GUILayout.EndArea();

                    GUILayout.BeginArea(right[2]);
                    for (var i = titanRebinds.Length / 2; i < titanRebinds.Length; i++)
                    {
                        var k = 101 + i;
                        GUILayout.BeginHorizontal();
                        Label(titanRebinds[i], width: 225f);
                        if (GUILayout.Button((string) FengGameManagerMKII.settings[k]))
                        {
                            FengGameManagerMKII.settings[k] = "waiting...";
                            FengGameManagerMKII.settings[100] = k;
                        }

                        GUILayout.EndHorizontal();
                    }

                    GUILayout.EndArea();

                    if ((int) FengGameManagerMKII.settings[100] != 0)
                    {
                        current = Event.current;
                        isPressed = false;
                        var str4 = "waiting...";
                        if (current.type == EventType.KeyDown && current.keyCode != KeyCode.None)
                        {
                            isPressed = true;
                            str4 = current.keyCode.ToString();
                        }
                        else if (Input.GetKey(KeyCode.LeftShift))
                        {
                            isPressed = true;
                            str4 = KeyCode.LeftShift.ToString();
                        }
                        else if (Input.GetKey(KeyCode.RightShift))
                        {
                            isPressed = true;
                            str4 = KeyCode.RightShift.ToString();
                        }
                        else if (Input.GetAxis("Mouse ScrollWheel") != 0f)
                        {
                            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                            {
                                isPressed = true;
                                str4 = "Scroll Up";
                            }
                            else
                            {
                                isPressed = true;
                                str4 = "Scroll Down";
                            }
                        }
                        else
                        {
                            for (var i = 0; i < 7; i++)
                            {
                                if (Input.GetKeyDown((KeyCode) (323 + i)))
                                {
                                    isPressed = true;
                                    str4 = "Mouse" + Convert.ToString(i);
                                }
                            }
                        }

                        if (isPressed)
                        {
                            for (var i = 0; i < 14; i++)
                            {
                                var k = 101 + i;
                                if ((int) FengGameManagerMKII.settings[100] == k)
                                {
                                    FengGameManagerMKII.settings[k] = str4;
                                    FengGameManagerMKII.settings[100] = 0;
                                    FengGameManagerMKII.inputRC.setInputTitan(i, str4);
                                }
                            }
                        }
                    }

                    break;
                }

                case 2:
                {
                    GUILayout.BeginArea(left[2]);
                    Label("Horse", LabelType.Header);
                    for (var i = 0; i < horseRebinds.Length; i++)
                    {
                        var k = 237 + i;
                        GUILayout.BeginHorizontal();
                        Label(horseRebinds[i], width: 225f);
                        if (GUILayout.Button((string) FengGameManagerMKII.settings[k]))
                        {
                            FengGameManagerMKII.settings[k] = "waiting...";
                            FengGameManagerMKII.settings[100] = k;
                        }

                        GUILayout.EndHorizontal();
                    }

                    GUILayout.EndArea();

                    GUILayout.BeginArea(right[2]);
                    Label("Cannon", LabelType.Header);
                    for (var i = 0; i < cannonRebinds.Length; i++)
                    {
                        var k = 254 + i;
                        GUILayout.BeginHorizontal();
                        Label(horseRebinds[i], width: 225f);
                        if (GUILayout.Button((string) FengGameManagerMKII.settings[k]))
                        {
                            FengGameManagerMKII.settings[k] = "waiting...";
                            FengGameManagerMKII.settings[100] = k;
                        }

                        GUILayout.EndHorizontal();
                    }

                    GUILayout.EndArea();

                    if ((int) FengGameManagerMKII.settings[100] != 0)
                    {
                        current = Event.current;
                        isPressed = false;
                        var str4 = "waiting...";
                        if (current.type == EventType.KeyDown && current.keyCode != KeyCode.None)
                        {
                            isPressed = true;
                            str4 = current.keyCode.ToString();
                        }
                        else if (Input.GetKey(KeyCode.LeftShift))
                        {
                            isPressed = true;
                            str4 = KeyCode.LeftShift.ToString();
                        }
                        else if (Input.GetKey(KeyCode.RightShift))
                        {
                            isPressed = true;
                            str4 = KeyCode.RightShift.ToString();
                        }
                        else if (Input.GetAxis("Mouse ScrollWheel") != 0f)
                        {
                            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                            {
                                isPressed = true;
                                str4 = "Scroll Up";
                            }
                            else
                            {
                                isPressed = true;
                                str4 = "Scroll Down";
                            }
                        }
                        else
                        {
                            for (var i = 0; i < 7; i++)
                            {
                                if (Input.GetKeyDown((KeyCode) (323 + i)))
                                {
                                    isPressed = true;
                                    str4 = "Mouse" + Convert.ToString(i);
                                }
                            }
                        }

                        if (isPressed)
                        {
                            for (var i = 0; i < 7; i++)
                            {
                                var k = 237 + i;
                                if ((int) FengGameManagerMKII.settings[100] == k)
                                {
                                    FengGameManagerMKII.settings[k] = str4;
                                    FengGameManagerMKII.settings[100] = 0;
                                    FengGameManagerMKII.inputRC.setInputHorse(i, str4);
                                }

                                k = 254 + i;
                                if ((int) FengGameManagerMKII.settings[100] == k)
                                {
                                    FengGameManagerMKII.settings[k] = str4;
                                    FengGameManagerMKII.settings[100] = 0;
                                    FengGameManagerMKII.inputRC.setInputCannon(i, str4);
                                }
                            }
                        }
                    }

                    break;
                }
            }
        }

        private static void Bombs()
        {
            GUILayout.BeginArea(left[0]);
            GUILayout.Space(15f);
            Label("Stats", LabelType.Header);
            int[] freePoints =
            {
                20 - (BombSettings[1] + BombSettings[2] + BombSettings[3]),
                20 - (BombSettings[0] + BombSettings[2] + BombSettings[3]),
                20 - (BombSettings[0] + BombSettings[1] + BombSettings[3]),
                20 - (BombSettings[0] + BombSettings[1] + BombSettings[2])
            };
            for (int i = 0; i < 4; i++)
            {
                GUILayout.BeginHorizontal();
                Slider(bombStats[i], ref BombSettings[i].Value, 0, freePoints[i] > 10 ? 10 : freePoints[i]);
                GUILayout.EndHorizontal();
            }

            Label("Color", LabelType.Header);
            Slider("R", ref BombColorSetting[0].Value, 0f, 1f);
            Slider("G", ref BombColorSetting[1].Value, 0f, 1f);
            Slider("B", ref BombColorSetting[2].Value, 0f, 1f);
            //Slider("A", ref BombColorSetting[3].Value, 0f, 1f);
            Texture2D txt = new Texture2D(1, 1);
            txt.SetPixel(0,0, new Color(BombColorSetting[0], BombColorSetting[1], BombColorSetting[2]/*, BombColorSetting[3]*/));
            txt.Apply();
            UnityEngine.GUI.DrawTexture(new Rect(50f, 175f, 70f, 70f), txt, ScaleMode.StretchToFill);
            GUILayout.EndArea();

            GUILayout.BeginArea(right[0]);
            GUILayout.Space(15f);
            GUILayout.Label("Color Presets", HeaderStyle);
            foreach (var texture in ColorCache.Textures)
            {
                GUILayout.Button(texture.Value);
            }
            GUILayout.EndArea();
        }

        private static void HumanSkins()
        {
            GUILayout.BeginArea(left[0]);
            GUILayout.Space(15f);

            Label("Settings", LabelType.Header);
            Label("General", LabelType.SubHeader);
            Grid("Skins Appearance", ref HumanSkinsSetting.Value, skinsAppearanceType);
            Grid("Blade Trails", ref BladeTrailsSetting.Value);
            Grid("Custom Gas", ref CustomGasSetting.Value);

            Label("Blade Trails", LabelType.SubHeader);
            Grid("Appearance", ref BladeTrailsAppearanceSetting.Value, bladeTrailsAppearance);
            Slider("Frame Rate", ref BladeTrailsFrameRateSetting.Value, 60, 240);
            Grid("Infinite Lifetime", ref BladeTrailsInfiniteLifetimeSetting.Value);

            Label("Presets", LabelType.SubHeader);
            scrollHumanSkins = GUILayout.BeginScrollView(scrollHumanSkins);
            GUILayout.BeginHorizontal(GUILayout.Width(leftElementWidth + rightElementWidth + 15f));
            GUILayout.FlexibleSpace();
            HumanSkinsCurrentSetSetting.Value = GUILayout.SelectionGrid(HumanSkinsCurrentSetSetting, HumanSkinsTitlesList.ToArray(), 1, GUILayout.Width(175f));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndScrollView();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add"))
            {
                HumanSkinsTitlesList.Add("Set " + (HumanSkinsTitlesList.Count + 1));
                HumanSkinsCurrentSetSetting.Value = HumanSkinsTitlesList.Count - 1;
                HumanSkinsList.Add(("````````````").Split('`'));
                HumanSkinsCountSetting.Value++;
                scrollHumanSkins.y = 9999f;
            }
            if (GUILayout.Button("Remove"))
            {
                if (HumanSkinsCountSetting == 1)
                {
                    HumanSkinsTitlesList[HumanSkinsCurrentSetSetting] = "Set 1";
                    HumanSkinsList[HumanSkinsCurrentSetSetting] = ("````````````").Split('`');
                }
                else
                {
                    int setToRemove = HumanSkinsCurrentSetSetting;
                    if (setToRemove != 0) HumanSkinsCurrentSetSetting.Value--;
                    HumanSkinsList.RemoveAt(setToRemove);
                    HumanSkinsTitlesList.RemoveAt(setToRemove);
                    HumanSkinsCountSetting.Value--;
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.EndArea();

            GUILayout.BeginArea(right[0]);
            GUILayout.Space(15f);

            Label("Skins", LabelType.Header);
            Label(HumanSkinsTitlesList[HumanSkinsCurrentSetSetting], LabelType.SubHeader);
            GUILayout.BeginHorizontal();
            Label("Title");
            HumanSkinsTitlesList[HumanSkinsCurrentSetSetting] = GUILayout.TextField(HumanSkinsTitlesList[HumanSkinsCurrentSetSetting], GUILayout.Width(TextFieldWidth));
            GUILayout.EndHorizontal();
            for (int i = 0; i < humanSkinFields.Length; i++)
            {
                TextField(humanSkinFields[i], ref HumanSkinsList[HumanSkinsCurrentSetSetting][i]);
            }
            GUILayout.Space(15f);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Copy"))
            {
                CopiedHumanSkins = HumanSkinsList[HumanSkinsCurrentSetSetting];
            }
            if (GUILayout.Button("Paste"))
            {
                if (CopiedHumanSkins != null) HumanSkinsList[HumanSkinsCurrentSetSetting] = CopiedHumanSkins;
            }
            if (GUILayout.Button("Reset"))
            {
                HumanSkinsTitlesList[HumanSkinsCurrentSetSetting] = "Set " + (HumanSkinsCurrentSetSetting + 1);
                HumanSkinsList[HumanSkinsCurrentSetSetting] = ("````````````").Split('`');
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }

        private static void TitanSkins()
        {
            throw new NotImplementedException();
        }

        private static void LocationSkins()
        {
            switch (locationSkinsSwitch)
            {
                case 0:
                {
                    GUILayout.BeginArea(left[0]);
                    GUILayout.Space(15f);

                    Label("Forest", LabelType.Header);
                    Label("Settings", LabelType.SubHeader);
                    Grid("Skins Appearance", ref LocationSkinsSetting.Value, skinsAppearanceType);
                    Grid("Location", ref locationSkinsSwitch, locationSkinsLocation);
                    Grid("Randomized Pairs", ref LocationSkinsRandomizedPairsSetting.Value);

                    Label("Presets", LabelType.SubHeader);
                    scrollLocationSkinsForestLeft = GUILayout.BeginScrollView(scrollLocationSkinsForestLeft);
                    GUILayout.BeginHorizontal(GUILayout.Width(leftElementWidth + rightElementWidth + 15f));
                    GUILayout.FlexibleSpace();
                    LocationSkinsForestCurrentSetSetting.Value = GUILayout.SelectionGrid(LocationSkinsForestCurrentSetSetting, LocationSkinsForestTitlesList.ToArray(), 1, GUILayout.Width(175f));
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                    GUILayout.EndScrollView();

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Add"))
                    {
                        LocationSkinsForestList.Add(("``````````````````````").Split('`'));
                        LocationSkinsForestTitlesList.Add("Set " + (LocationSkinsForestTitlesList.Count + 1));
                        LocationSkinsForestCurrentSetSetting.Value = LocationSkinsForestTitlesList.Count - 1;
                        LocationSkinsForestAmbientList.Add(0);
                        LocationSkinsForestAmbientSettingsList.Add(new float[] {AmbientColorSetting[0][0], AmbientColorSetting[0][1], AmbientColorSetting[0][2]});
                        LocationSkinsForestFogList.Add(0);
                        LocationSkinsForestFogSettingsList.Add(new float[]{0.066f, 0.066f, 0.066f, 0f, 1000f});
                        LocationSkinsForestParticlesList.Add(0);
                        LocationSkinsForestParticlesSettingsList.Add(new float[]{1500f, 125f, 60f, 120f, 0.001f, 0f, 1f, 1f, 1f, 1f});
                        LocationSkinsForestCountSetting.Value++;
                        scrollLocationSkinsForestLeft.y = 9999f;
                    }
                    if (GUILayout.Button("Remove"))
                    {
                        if (LocationSkinsForestCountSetting == 1)
                        {
                            LocationSkinsForestTitlesList[LocationSkinsForestCurrentSetSetting] = "Set 1";
                            LocationSkinsForestList[LocationSkinsForestCurrentSetSetting] = ("``````````````````````").Split('`');
                            LocationSkinsForestAmbientList[LocationSkinsForestCurrentSetSetting] = 0;
                            LocationSkinsForestAmbientSettingsList[LocationSkinsForestCurrentSetSetting] = new float[] {AmbientColorSetting[0][0], AmbientColorSetting[0][1], AmbientColorSetting[0][2]};
                            LocationSkinsForestFogList[LocationSkinsForestCurrentSetSetting] = 0;
                            LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting] = new float[] {0.066f, 0.066f, 0.066f, 0f, 1000f};
                            LocationSkinsForestParticlesList[LocationSkinsForestCurrentSetSetting] = 0;
                            LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting] = new float[] {1500f, 125f, 60f, 120f, 0.001f, 0f, 1f, 1f, 1f, 1f};
                        }
                        else
                        {
                            int setToRemove = LocationSkinsForestCurrentSetSetting;
                            if (setToRemove != 0) LocationSkinsForestCurrentSetSetting.Value--;
                            LocationSkinsForestList.RemoveAt(setToRemove);
                            LocationSkinsForestTitlesList.RemoveAt(setToRemove);
                            LocationSkinsForestAmbientList.RemoveAt(setToRemove);
                            LocationSkinsForestAmbientSettingsList.RemoveAt(setToRemove);
                            LocationSkinsForestFogList.RemoveAt(setToRemove);
                            LocationSkinsForestFogSettingsList.RemoveAt(setToRemove);
                            LocationSkinsForestParticlesList.RemoveAt(setToRemove);
                            LocationSkinsForestParticlesSettingsList.RemoveAt(setToRemove);
                            LocationSkinsForestCountSetting.Value--;
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.EndArea();

                    GUILayout.BeginArea(right[0]);
                    GUILayout.Space(15f);

                    Label(LocationSkinsForestTitlesList[LocationSkinsForestCurrentSetSetting], LabelType.Header);
                    scrollLocationSkinsForestRight = GUILayout.BeginScrollView(scrollLocationSkinsForestRight);

                    GUILayout.BeginHorizontal();
                    Label("Title");
                    LocationSkinsForestTitlesList[LocationSkinsForestCurrentSetSetting] = GUILayout.TextField(LocationSkinsForestTitlesList[LocationSkinsForestCurrentSetSetting], GUILayout.Width(TextFieldWidth));
                    GUILayout.EndHorizontal();
                    for (int i = 0; i < locationSkinForestFields.Length; i++)
                    {
                        TextField(locationSkinForestFields[i], ref LocationSkinsForestList[LocationSkinsForestCurrentSetSetting][i]);
                    }

                    GUILayout.BeginHorizontal();
                    Label("Ambient");
                    LocationSkinsForestAmbientList[LocationSkinsForestCurrentSetSetting] = GUILayout.SelectionGrid(LocationSkinsForestAmbientList[LocationSkinsForestCurrentSetSetting], SwitcherStr, 2, GUILayout.Width(GridWidth));
                    GUILayout.EndHorizontal();
                    if (LocationSkinsForestAmbientList[LocationSkinsForestCurrentSetSetting] == 1)
                    {
                        Slider("R", ref LocationSkinsForestAmbientSettingsList[LocationSkinsForestCurrentSetSetting][0], 0f, 1f);
                        Slider("G", ref LocationSkinsForestAmbientSettingsList[LocationSkinsForestCurrentSetSetting][1], 0f, 1f);
                        Slider("B", ref LocationSkinsForestAmbientSettingsList[LocationSkinsForestCurrentSetSetting][2], 0f, 1f);
                    }

                    GUILayout.BeginHorizontal();
                    Label("Fog");
                    LocationSkinsForestFogList[LocationSkinsForestCurrentSetSetting] =GUILayout.SelectionGrid(LocationSkinsForestFogList[LocationSkinsForestCurrentSetSetting], SwitcherStr, 2, GUILayout.Width(GridWidth));
                    GUILayout.EndHorizontal();
                    if (LocationSkinsForestFogList[LocationSkinsForestCurrentSetSetting] == 1)
                    {
                        Slider("R", ref LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][0], 0f,1f);
                        Slider("G", ref LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][1], 0f,1f);
                        Slider("B", ref LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][2], 0f,1f);
                        if (LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][3] > LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][4] && LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][3] != 0f) LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][3] = LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][4] - 0.1f;
                        if (LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][3] < 0) LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][3] = 0;
                        Slider("Start Distance", ref LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][3], 0f, 1000f);
                        Slider("End Distance", ref LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][4], 0f, 1000f);
                    }

                    GUILayout.BeginHorizontal();
                    Label("Particles");
                    LocationSkinsForestParticlesList[LocationSkinsForestCurrentSetSetting] = GUILayout.SelectionGrid(LocationSkinsForestParticlesList[LocationSkinsForestCurrentSetSetting], SwitcherStr, 2, GUILayout.Width(GridWidth));
                    GUILayout.EndHorizontal();
                    if (LocationSkinsForestParticlesList[LocationSkinsForestCurrentSetSetting] == 1)
                    {
                        Slider("Count", ref LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][0], 100f, 15000f);
                        Slider("Height", ref LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][1], 0F, 1000f);
                        if (LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][2] > LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][3] && LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][2] != 0f) LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][2] = LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][3] - 0.1f;
                        if (LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][2] < 0) LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][2] = 0;
                        Slider("Lifetime Minimum", ref LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][2], 0f, 600f);
                        Slider("Lifetime Maximum", ref LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][3], 0f, 600f);
                        Slider("Gravity", ref LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][4], 0f, 5f);
                        Slider("R", ref LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][5], 0f, 1f);
                        Slider("G", ref LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][6], 0f, 1f);
                        Slider("B", ref LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][7], 0f, 1f);
                        Slider("A", ref LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][8], 0f, 1f);
                    }

                    GUILayout.EndScrollView();

                    GUILayout.Space(15f);
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Copy"))
                    {
                        LocationSkinsForestCopiedSet = LocationSkinsForestList[LocationSkinsForestCurrentSetSetting];
                    }
                    if (GUILayout.Button("Paste"))
                    {
                        if (LocationSkinsForestCopiedSet != null)
                            LocationSkinsForestList[LocationSkinsForestCurrentSetSetting] = LocationSkinsForestCopiedSet;
                    }
                    if (GUILayout.Button("Reset"))
                    {
                        LocationSkinsForestTitlesList[LocationSkinsForestCurrentSetSetting] = "Set " + (LocationSkinsForestCurrentSetSetting + 1);
                        LocationSkinsForestList[LocationSkinsForestCurrentSetSetting] = ("``````````````````````").Split('`');
                        LocationSkinsForestAmbientList[LocationSkinsForestCountSetting] = 0;
                        LocationSkinsForestAmbientSettingsList[LocationSkinsForestCountSetting] = new float[] {AmbientColorSetting[0][0], AmbientColorSetting[0][1], AmbientColorSetting[0][2]};
                        LocationSkinsForestFogList[LocationSkinsForestCountSetting] = 0;
                        LocationSkinsForestFogSettingsList[LocationSkinsForestCountSetting] = new float[] {0.066f, 0.066f, 0.066f, 0f, 1000f};
                        LocationSkinsForestParticlesList[LocationSkinsForestCountSetting] = 0;
                        LocationSkinsForestParticlesSettingsList[LocationSkinsForestCountSetting] = new float[] {1500f, 125f, 60f, 120f, 0.001f, 0f, 1f, 1f, 1f, 1f};
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.EndArea();
                }
                    break;
                case 1:
                {
                    GUILayout.BeginArea(left[0]);
                    GUILayout.Space(15f);

                    Label("City", LabelType.Header);
                    Label("Settings", LabelType.SubHeader);
                    Grid("Skins Appearance", ref LocationSkinsSetting.Value, skinsAppearanceType);
                    Grid("Location", ref locationSkinsSwitch, locationSkinsLocation);

                    Label("Presets", LabelType.SubHeader);
                    scrollLocationSkinsCityLeft = GUILayout.BeginScrollView(scrollLocationSkinsCityLeft, GUILayout.Width(0f));
                    GUILayout.BeginHorizontal(GUILayout.Width(leftElementWidth + rightElementWidth + 15f));
                    GUILayout.FlexibleSpace();
                    LocationSkinsCityCurrentSetSetting.Value = GUILayout.SelectionGrid(LocationSkinsCityCurrentSetSetting, LocationSkinsCityTitlesList.ToArray(), 1, GUILayout.Width(175f));
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                    GUILayout.EndScrollView();

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Add"))
                    {
                        LocationSkinsCityTitlesList.Add("Set " + (LocationSkinsCityTitlesList.Count + 1));
                        LocationSkinsCityCurrentSetSetting.Value = LocationSkinsCityTitlesList.Count - 1;
                        LocationSkinsCityList.Add(("````````````````").Split('`'));
                        LocationSkinsCityAmbientList.Add(0);
                        LocationSkinsCityAmbientSettingsList.Add(new float[] {AmbientColorSetting[0][0], AmbientColorSetting[0][1], AmbientColorSetting[0][2]});
                        LocationSkinsCityFogList.Add(0);
                        LocationSkinsCityFogSettingsList.Add(new float[] {0.066f, 0.066f, 0.066f, 0f, 1000f});
                        LocationSkinsCityParticlesList.Add(0);
                        LocationSkinsCityParticlesSettingsList.Add(new float[] {1500f, 125f, 60f, 120f, 0.001f, 0f, 1f, 1f, 1f, 1f});
                        LocationSkinsCityCountSetting.Value++;
                        scrollLocationSkinsCityLeft.y = 9999f;
                    }
                    if (GUILayout.Button("Remove"))
                    {
                        if (LocationSkinsCityCountSetting == 1)
                        {
                            LocationSkinsCityTitlesList[LocationSkinsCityCurrentSetSetting] = "Set 1";
                            LocationSkinsCityList[LocationSkinsCityCurrentSetSetting] = ("````````````````").Split('`');
                            LocationSkinsCityAmbientList[LocationSkinsCityCurrentSetSetting] = 0;
                            LocationSkinsCityAmbientSettingsList[LocationSkinsCityCurrentSetSetting] = new float[] {AmbientColorSetting[0][0], AmbientColorSetting[0][1], AmbientColorSetting[0][2]};
                            LocationSkinsCityFogList[LocationSkinsCityCurrentSetSetting] = 0;
                            LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting] = new float[] {0.066f, 0.066f, 0.066f, 0f, 1000f};
                            LocationSkinsCityParticlesList[LocationSkinsCityCurrentSetSetting] = 0;
                            LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting] = new float[] {1500f, 125f, 60f, 120f, 0.001f, 0f, 1f, 1f, 1f, 1f};
                        }
                        else
                        {
                            int setToRemove = LocationSkinsCityCurrentSetSetting;
                            if (setToRemove != 0) LocationSkinsCityCurrentSetSetting.Value--;
                            LocationSkinsCityList.RemoveAt(setToRemove);
                            LocationSkinsCityTitlesList.RemoveAt(setToRemove);
                            LocationSkinsCityAmbientList.RemoveAt(setToRemove);
                            LocationSkinsCityAmbientSettingsList.RemoveAt(setToRemove);
                            LocationSkinsCityFogList.RemoveAt(setToRemove);
                            LocationSkinsCityFogSettingsList.RemoveAt(setToRemove);
                            LocationSkinsCityParticlesList.RemoveAt(setToRemove);
                            LocationSkinsCityParticlesSettingsList.RemoveAt(setToRemove);
                            LocationSkinsCityCountSetting.Value--;
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.EndArea();

                    GUILayout.BeginArea(right[0]);
                    GUILayout.Space(15f);

                    Label(LocationSkinsCityTitlesList[LocationSkinsCityCurrentSetSetting], LabelType.Header);
                    scrollLocationSkinsCityRight = GUILayout.BeginScrollView(scrollLocationSkinsCityRight, GUILayout.Width(0f));

                    GUILayout.BeginHorizontal();
                    Label("Title");
                    LocationSkinsCityTitlesList[LocationSkinsCityCurrentSetSetting] = GUILayout.TextField(LocationSkinsCityTitlesList[LocationSkinsCityCurrentSetSetting], GUILayout.Width(TextFieldWidth));
                    GUILayout.EndHorizontal();
                    for (int i = 0; i < locationSkinCityFields.Length; i++)
                    {
                        TextField(locationSkinCityFields[i], ref LocationSkinsCityList[LocationSkinsCityCurrentSetSetting][i]);
                    }

                    GUILayout.BeginHorizontal();
                    Label("Ambient");
                    LocationSkinsCityAmbientList[LocationSkinsCityCurrentSetSetting] = GUILayout.SelectionGrid(LocationSkinsCityAmbientList[LocationSkinsCityCurrentSetSetting], SwitcherStr, 2, GUILayout.Width(GridWidth));
                    GUILayout.EndHorizontal();
                    if (LocationSkinsCityAmbientList[LocationSkinsCityCurrentSetSetting] == 1)
                    {
                        Slider("R", ref LocationSkinsCityAmbientSettingsList[LocationSkinsCityCurrentSetSetting][0], 0f, 1f);
                        Slider("G", ref LocationSkinsCityAmbientSettingsList[LocationSkinsCityCurrentSetSetting][1], 0f, 1f);
                        Slider("B", ref LocationSkinsCityAmbientSettingsList[LocationSkinsCityCurrentSetSetting][2], 0f, 1f);
                    }

                    GUILayout.BeginHorizontal();
                    Label("Fog");
                    LocationSkinsCityFogList[LocationSkinsCityCurrentSetSetting] = GUILayout.SelectionGrid(LocationSkinsCityFogList[LocationSkinsCityCurrentSetSetting], SwitcherStr, 2, GUILayout.Width(GridWidth));
                    GUILayout.EndHorizontal();
                    if (LocationSkinsCityFogList[LocationSkinsCityCurrentSetSetting] == 1)
                    {
                        Slider("R", ref LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][0], 0f, 1f);
                        Slider("G", ref LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][1], 0f, 1f);
                        Slider("B", ref LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][2], 0f, 1f);
                        if (LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][3] > LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][4] && LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][3] != 0f) LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][3] = LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][4] - 0.1f;
                        if (LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][3] < 0) LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][3] = 0;
                        Slider("Start Distance", ref LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][3], 0f, 1000f);
                        Slider("End Distance", ref LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][4], 0f, 1000f);
                    }

                    GUILayout.BeginHorizontal();
                    Label("Particles");
                    LocationSkinsCityParticlesList[LocationSkinsCityCurrentSetSetting] = GUILayout.SelectionGrid(LocationSkinsCityParticlesList[LocationSkinsCityCurrentSetSetting], SwitcherStr, 2, GUILayout.Width(GridWidth));
                    GUILayout.EndHorizontal();
                    if (LocationSkinsCityParticlesList[LocationSkinsCityCurrentSetSetting] == 1)
                    {
                        Slider("Count", ref LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][0], 100f, 15000f);
                        Slider("Height", ref LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][1], 0F, 1000f);
                        if (LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][2] > LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][3] && LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][2] != 0f) LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][2] = LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][3] - 0.1f;
                        if (LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][2] < 0) LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][2] = 0;
                        Slider("Lifetime Minimum", ref LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][2], 0f, 600f);
                        Slider("Lifetime Maximum", ref LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][3], 0f, 600f);
                        Slider("Gravity", ref LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][4], 0f, 5f);
                        Slider("R", ref LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][5], 0f, 1f);
                        Slider("G", ref LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][6], 0f, 1f);
                        Slider("B", ref LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][7], 0f, 1f);
                        Slider("A", ref LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][8], 0f, 1f);
                    }

                    GUILayout.EndScrollView();

                    GUILayout.Space(15f);
                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Copy"))
                    {
                        LocationSkinsCityCopiedSet = LocationSkinsCityList[LocationSkinsCityCurrentSetSetting];
                    }
                    if (GUILayout.Button("Paste"))
                    {
                        if (LocationSkinsCityCopiedSet != null) LocationSkinsCityList[LocationSkinsCityCurrentSetSetting] = LocationSkinsCityCopiedSet;
                    }
                    if (GUILayout.Button("Reset"))
                    {
                        LocationSkinsCityTitlesList[LocationSkinsCityCurrentSetSetting] = "Set " + (LocationSkinsCityCurrentSetSetting + 1);
                        LocationSkinsCityList[LocationSkinsCityCurrentSetSetting] = ("````````````````").Split('`');
                        LocationSkinsCityAmbientList[LocationSkinsCityCountSetting] = 0;
                        LocationSkinsCityAmbientSettingsList[LocationSkinsCityCountSetting] = new float[] {AmbientColorSetting[0][0], AmbientColorSetting[0][1], AmbientColorSetting[0][2]};
                        LocationSkinsCityFogList[LocationSkinsCityCountSetting] = 0;
                        LocationSkinsCityFogSettingsList[LocationSkinsCityCountSetting] = new float[] {0.066f, 0.066f, 0.066f, 0f, 1000f};
                        LocationSkinsCityParticlesList[LocationSkinsCityCountSetting] = 0;
                        LocationSkinsCityParticlesSettingsList[LocationSkinsCityCountSetting] = new float[] {1500f, 125f, 60f, 120f, 0.001f, 0f, 1f, 1f, 1f, 1f};
                    }
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();

                    GUILayout.EndArea();
                }
                    break;
            }
        }

        private static void CustomMap()
        {
            throw new NotImplementedException();
        }

        private static void CustomLogic()
        {
            throw new NotImplementedException();
        }
    }
}