using GGM.Caching;
using System;
using System.Linq;
using UnityEngine;
using static GGM.Config.Settings;
using static GGM.GUI.Elements;
using static GGM.GUI.Settings;

namespace GGM.GUI.Pages
{
    internal class PauseMenu : Page
    {
        #region Switchers

        private static int pauseMenuSwitchInt;
        private static int serverSwitch;
        private static int videoAndAudioSwitch;
        private static int ambientDayTimeSwitch;
        private static int rebindsSwitch;
        private static int locationSkinsSwitch;

        #endregion Switchers

        #region Strings

        private static readonly string[] pauseMenuPages = { "Game", "Server", "Video & Audio", "Rebinds", "Bombs", "Human Skins", "Titan Skins", "Location Skins", "Custom Map", "Custom Logic" };

        private static readonly string[] cameraTypes = { "Original", "TPS", "WOW", "OldTPS" };
        private static readonly string[] speedometerTypes = { "Off", "Speed", "Damage" };
        private static readonly string[] ahssSpeedometerTypes = { "Both", "Single", "Double" };
        private static readonly string[] serverPages = { "Titans", "Humans", "Misc" };
        private static readonly string[] healthTypes = { "Fixed", "Static" };
        private static readonly string[] pvpTypes = { "Teams", "FFA" };
        private static readonly string[] teamTypes = { "Off", "Size", "Skill" };
        private static readonly string[] formatOptions = { "Bold", "Italic" };
        private static readonly string[] videoAndAudioPages = { "Video", "Audio" };
        private static readonly string[] textures = { "Low", "Medium", "High" };
        private static readonly string[] anisotropicFiltering = { "Off", "On", "Forced" };
        private static readonly string[] antiAliasing = { "Off", "2x", "4x", "8x" };
        private static readonly string[] blendWeights = { "One", "Two", "Four" };
        private static readonly string[] shadowProjection = { "Stable Fit", "Close Fit" };
        private static readonly string[] shadowCascades = { "0", "2", "4" };
        private static readonly string[] dayTime = { "Day", "Dawn", "Night" };
        private static readonly string[] rebindsPages = { "Human", "Titan", "Mount", "Misc" };

        private static readonly string[] humanRebinds = { "Forward", "Backward", "Left", "Right", "Jump", "Dodge", "Left Hook", "Right Hook", "Both Hooks", "Lock", "Attack", "Special", "Salute", "Change Camera", "Restart/Suicide", "Menu", "Show/Hide Cursor", "Fullscreen", "Reload", "Flare Green", "Flare Red", "Flare Black", "Reel In", "Reel Out", "Dash", "Minimap Maximize", "Minimap Toggle", "Minimap Reset", "Chat", "Live Spectate" };

        private static readonly string[] titanRebinds = { "Forward", "Backward", "Left", "Right", "Walk", "Jump", "Punch", "Slam", "Grab Front", "Grab Back", "Grab Nape", "Slap", "Bite", "Cover Nape" };

        private static readonly string[] horseRebinds = { "Forward", "Backward", "Left", "Right", "Walk", "Jump", "Mount" };

        private static readonly string[] cannonRebinds = { "Rotate Up:", "Rotate Down:", "Rotate Left:", "Rotate Right:", "Fire:", "Mount:", "Slow Rotate:" };

        private static readonly string[] bombStats = { "Radius", "Range", "Speed", "Cooldown" };
        private static readonly string[] skinsAppearanceType = { "Off", "Local", "Global" };
        private static readonly string[] bladeTrailsAppearance = { "Legacy", "Liquid" };

        private static readonly string[] humanSkinFields = { "Horse", "Hair", "Eyes", "Glass", "Face", "Skin", "Costume", "Hoodie", "Left 3DMG", "Right 3DMG", "Gas", "Logo & Cape", "Weapon Trail" };

        private static readonly string[] locationSkinsLocation = { "Forest", "City" };

        private static readonly string[] locationSkinForestFields = { "Ground", "Forest Trunk #1", "Forest Trunk #2", "Forest Trunk #3", "Forest Trunk #4", "Forest Trunk #5", "Forest Trunk #6", "Forest Trunk #7", "Forest Trunk #8", "Forest Leave #1", "Forest Leave #2", "Forest Leave #3", "Forest Leave #4", "Forest Leave #5", "Forest Leave #6", "Forest Leave #7", "Forest Leave #8", "Skybox Front", "Skybox Back", "Skybox Left", "Skybox Right", "Skybox Up", "Skybox Down" };

        private static readonly string[] locationSkinCityFields = { "Ground", "Wall", "Gate", "Houses #1", "Houses #2", "Houses #3", "Houses #4", "Houses #5", "Houses #6", "Houses #7", "Houses #8", "Skybox Front", "Skybox Back", "Skybox Left", "Skybox Right", "Skybox Up", "Skybox Down", };

        private static readonly string[] connectionProtocols = { "UDP", "TCP", "WS" };

        #endregion Strings

        #region Scrolls

        private static Vector2 scrollGameLeft = Vector2.zero;
        private static Vector2 scrollServerTitansLeft = Vector2.zero;
        private static Vector2 scrollServerMiscLeft = Vector2.zero;
        private static Vector2 scrollVideoLeft = Vector2.zero;
        private static Vector2 scrollHumanSkins = Vector2.zero;
        private static Vector2 scrollLocationSkinsForestLeft = Vector2.zero;
        private static Vector2 scrollLocationSkinsForestRight = Vector2.zero;
        private static Vector2 scrollLocationSkinsCityLeft = Vector2.zero;
        private static Vector2 scrollLocationSkinsCityRight = Vector2.zero;

        #endregion Scrolls

        private void OnGUI()
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

            if (UnityEngine.GUI.Button(new Rect(leftPos + 408f + 65f, topPos + height / 1.055f, 42f, 25f), "Save"))
            {
                Save();
                HotKey.Save();
                PlayerPrefs.SetInt("titan", (int)FengGameManagerMKII.settings[1]);
                PlayerPrefs.SetInt("titantype1", (int)FengGameManagerMKII.settings[16]);
                PlayerPrefs.SetInt("titantype2", (int)FengGameManagerMKII.settings[17]);
                PlayerPrefs.SetInt("titantype3", (int)FengGameManagerMKII.settings[18]);
                PlayerPrefs.SetInt("titantype4", (int)FengGameManagerMKII.settings[19]);
                PlayerPrefs.SetInt("titantype5", (int)FengGameManagerMKII.settings[20]);
                PlayerPrefs.SetString("titanhair1", (string)FengGameManagerMKII.settings[21]);
                PlayerPrefs.SetString("titanhair2", (string)FengGameManagerMKII.settings[22]);
                PlayerPrefs.SetString("titanhair3", (string)FengGameManagerMKII.settings[23]);
                PlayerPrefs.SetString("titanhair4", (string)FengGameManagerMKII.settings[24]);
                PlayerPrefs.SetString("titanhair5", (string)FengGameManagerMKII.settings[25]);
                PlayerPrefs.SetString("titaneye1", (string)FengGameManagerMKII.settings[26]);
                PlayerPrefs.SetString("titaneye2", (string)FengGameManagerMKII.settings[27]);
                PlayerPrefs.SetString("titaneye3", (string)FengGameManagerMKII.settings[28]);
                PlayerPrefs.SetString("titaneye4", (string)FengGameManagerMKII.settings[29]);
                PlayerPrefs.SetString("titaneye5", (string)FengGameManagerMKII.settings[30]);
                PlayerPrefs.SetInt("titanR", (int)FengGameManagerMKII.settings[32]);
                PlayerPrefs.SetString("eren", (string)FengGameManagerMKII.settings[65]);
                PlayerPrefs.SetString("annie", (string)FengGameManagerMKII.settings[66]);
                PlayerPrefs.SetString("colossal", (string)FengGameManagerMKII.settings[67]);
                PlayerPrefs.SetString("cnumber", (string)FengGameManagerMKII.settings[82]);
                PlayerPrefs.SetString("cmax", (string)FengGameManagerMKII.settings[85]);
                PlayerPrefs.SetString("titanbody1", (string)FengGameManagerMKII.settings[86]);
                PlayerPrefs.SetString("titanbody2", (string)FengGameManagerMKII.settings[87]);
                PlayerPrefs.SetString("titanbody3", (string)FengGameManagerMKII.settings[88]);
                PlayerPrefs.SetString("titanbody4", (string)FengGameManagerMKII.settings[89]);
                PlayerPrefs.SetString("titanbody5", (string)FengGameManagerMKII.settings[90]);
                PlayerPrefs.SetInt("customlevel", (int)FengGameManagerMKII.settings[91]);
                PlayerPrefs.SetString("reelin", (string)FengGameManagerMKII.settings[98]);
                PlayerPrefs.SetString("reelout", (string)FengGameManagerMKII.settings[99]);
                PlayerPrefs.SetString("tforward", (string)FengGameManagerMKII.settings[101]);
                PlayerPrefs.SetString("tback", (string)FengGameManagerMKII.settings[102]);
                PlayerPrefs.SetString("tleft", (string)FengGameManagerMKII.settings[103]);
                PlayerPrefs.SetString("tright", (string)FengGameManagerMKII.settings[104]);
                PlayerPrefs.SetString("twalk", (string)FengGameManagerMKII.settings[105]);
                PlayerPrefs.SetString("tjump", (string)FengGameManagerMKII.settings[106]);
                PlayerPrefs.SetString("tpunch", (string)FengGameManagerMKII.settings[107]);
                PlayerPrefs.SetString("tslam", (string)FengGameManagerMKII.settings[108]);
                PlayerPrefs.SetString("tgrabfront", (string)FengGameManagerMKII.settings[109]);
                PlayerPrefs.SetString("tgrabback", (string)FengGameManagerMKII.settings[110]);
                PlayerPrefs.SetString("tgrabnape", (string)FengGameManagerMKII.settings[111]);
                PlayerPrefs.SetString("tantiae", (string)FengGameManagerMKII.settings[112]);
                PlayerPrefs.SetString("tbite", (string)FengGameManagerMKII.settings[113]);
                PlayerPrefs.SetString("tcover", (string)FengGameManagerMKII.settings[114]);
                PlayerPrefs.SetString("tsit", (string)FengGameManagerMKII.settings[115]);
                PlayerPrefs.SetInt("humangui", (int)FengGameManagerMKII.settings[133]);
                PlayerPrefs.SetString("customGround", (string)FengGameManagerMKII.settings[162]);
                PlayerPrefs.SetString("forestskyfront", (string)FengGameManagerMKII.settings[163]);
                PlayerPrefs.SetString("forestskyback", (string)FengGameManagerMKII.settings[164]);
                PlayerPrefs.SetString("forestskyleft", (string)FengGameManagerMKII.settings[165]);
                PlayerPrefs.SetString("forestskyright", (string)FengGameManagerMKII.settings[166]);
                PlayerPrefs.SetString("forestskyup", (string)FengGameManagerMKII.settings[167]);
                PlayerPrefs.SetString("forestskydown", (string)FengGameManagerMKII.settings[168]);
                PlayerPrefs.SetString("cityskyfront", (string)FengGameManagerMKII.settings[169]);
                PlayerPrefs.SetString("cityskyback", (string)FengGameManagerMKII.settings[170]);
                PlayerPrefs.SetString("cityskyleft", (string)FengGameManagerMKII.settings[171]);
                PlayerPrefs.SetString("cityskyright", (string)FengGameManagerMKII.settings[172]);
                PlayerPrefs.SetString("cityskyup", (string)FengGameManagerMKII.settings[173]);
                PlayerPrefs.SetString("cityskydown", (string)FengGameManagerMKII.settings[174]);
                PlayerPrefs.SetString("customskyfront", (string)FengGameManagerMKII.settings[175]);
                PlayerPrefs.SetString("customskyback", (string)FengGameManagerMKII.settings[176]);
                PlayerPrefs.SetString("customskyleft", (string)FengGameManagerMKII.settings[177]);
                PlayerPrefs.SetString("customskyright", (string)FengGameManagerMKII.settings[178]);
                PlayerPrefs.SetString("customskyup", (string)FengGameManagerMKII.settings[179]);
                PlayerPrefs.SetString("customskydown", (string)FengGameManagerMKII.settings[180]);
                PlayerPrefs.SetString("dashkey", (string)FengGameManagerMKII.settings[182]);
                PlayerPrefs.SetString("mapMaximize", (string)FengGameManagerMKII.settings[232]);
                PlayerPrefs.SetString("mapToggle", (string)FengGameManagerMKII.settings[233]);
                PlayerPrefs.SetString("mapReset", (string)FengGameManagerMKII.settings[234]);
                PlayerPrefs.SetString("chatRebind", (string)FengGameManagerMKII.settings[236]);
                PlayerPrefs.SetString("hforward", (string)FengGameManagerMKII.settings[237]);
                PlayerPrefs.SetString("hback", (string)FengGameManagerMKII.settings[238]);
                PlayerPrefs.SetString("hleft", (string)FengGameManagerMKII.settings[239]);
                PlayerPrefs.SetString("hright", (string)FengGameManagerMKII.settings[240]);
                PlayerPrefs.SetString("hwalk", (string)FengGameManagerMKII.settings[241]);
                PlayerPrefs.SetString("hjump", (string)FengGameManagerMKII.settings[242]);
                PlayerPrefs.SetString("hmount", (string)FengGameManagerMKII.settings[243]);
                PlayerPrefs.SetString("cannonUp", (string)FengGameManagerMKII.settings[254]);
                PlayerPrefs.SetString("cannonDown", (string)FengGameManagerMKII.settings[255]);
                PlayerPrefs.SetString("cannonLeft", (string)FengGameManagerMKII.settings[256]);
                PlayerPrefs.SetString("cannonRight", (string)FengGameManagerMKII.settings[257]);
                PlayerPrefs.SetString("cannonFire", (string)FengGameManagerMKII.settings[258]);
                PlayerPrefs.SetString("cannonMount", (string)FengGameManagerMKII.settings[259]);
                PlayerPrefs.SetString("cannonSlow", (string)FengGameManagerMKII.settings[260]);
                PlayerPrefs.SetString("liveCam", (string)FengGameManagerMKII.settings[262]);
                FengGameManagerMKII.settings[64] = 4;
            }

            if (UnityEngine.GUI.Button(new Rect(leftPos + 455f + 65f, topPos + height / 1.055f, 45f, 25f), "Load"))
            {
                Load();
                HotKey.Load();
                FengGameManagerMKII.FGM.loadconfig();
                FengGameManagerMKII.settings[64] = 5;
            }

            if (UnityEngine.GUI.Button(new Rect(leftPos + 500f + 70f, topPos + height / 1.055f, 60f, 25f), "Default"))
            {
                GameObjectCache.Find("InputManagerController").GetComponent<FengCustomInputs>().setToDefault();
                PlayerPrefs.DeleteAll();
                Load();
            }

            if (UnityEngine.GUI.Button(new Rect(leftPos + 565f + 70f, topPos + height / 1.055f, 75f, 25f), "Continue"))
            {
                GetInstance<PauseMenu>().Disable();
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                {
                    Time.timeScale = 1f;
                }

                if (!Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().enabled)
                {
                    Screen.showCursor = true;
                    Screen.lockCursor = true;
                    GameObjectCache.Find("InputManagerController").GetComponent<FengCustomInputs>().menuOn = false;
                    Camera.main.GetComponent<SpectatorMovement>().disable = false;
                    Camera.main.GetComponent<MouseLook>().disable = false;
                }
                else
                {
                    IN_GAME_MAIN_CAMERA.isPausing = false;
                    if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.OLDTPS)
                    {
                        Screen.showCursor = false;
                        Screen.lockCursor = true;
                    }
                    else
                    {
                        Screen.showCursor = false;
                        Screen.lockCursor = false;
                    }

                    GameObjectCache.Find("InputManagerController").GetComponent<FengCustomInputs>().menuOn = false;
                    GameObjectCache.Find("InputManagerController").GetComponent<FengCustomInputs>().justUPDATEME();
                }
            }

            if (UnityEngine.GUI.Button(new Rect(leftPos + 645f + 70f, topPos + height / 1.055f, 40f, 25f), "Quit"))
            {
                GetInstance<PauseMenu>().Disable();
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                {
                    Time.timeScale = 1f;
                }
                else
                {
                    PhotonNetwork.Disconnect();
                }

                Screen.lockCursor = false;
                Screen.showCursor = true;
                IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
                FengGameManagerMKII.FGM.gameStart = false;
                GameObjectCache.Find("InputManagerController").GetComponent<FengCustomInputs>().menuOn = false;
                FengGameManagerMKII.FGM.DestroyAllExistingCloths();
                Destroy(GameObjectCache.Find("MultiplayerManager"));
                Application.LoadLevel("menu");
            }
        }

        private static void Game()
        {
            GUILayout.BeginArea(left[0]);
            {
                GUILayout.Space(15f);
                scrollGameLeft = GUILayout.BeginScrollView(scrollGameLeft);
                {
                    Label("Mouse", LabelType.Header);
                    Slider("Sensitivity", ref MouseSensitivitySetting.Value, 0.1f, 1f, multiplier: 100f, round: true);
                    Grid("Invert Y", ref MouseInvertYSetting.Value);
                    Label("Camera", LabelType.Header);
                    ButtonToggle(string.Empty, cameraTypes, CameraTypeSettings);
                    Slider("Distance", ref CameraDistanceSetting.Value, 0f, 1, multiplier: 100f, round: true);
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
                    if (InfiniteBulletsSetting) Grid("No Reloading", ref InfiniteBulletsNoReloadingSetting.Value);
                    Grid("Infinite Gas", ref InfiniteGasSetting.Value);
                    GUILayout.Space(1f);
                }
                GUILayout.EndScrollView();
            }
            GUILayout.EndArea();

            GUILayout.BeginArea(right[0]);
            {
                GUILayout.Space(15f);

                Label("User Interface", LabelType.Header);
                Grid("Legacy Labels", ref LegacyLabelsSetting.Value);
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
                Grid("Body Lean", ref BodyLean.Value);
                Grid("Chat Feed", ref ChatFeedSetting.Value);
                Grid("Minimap", ref MinimapSetting.Value);
                Grid("Speedometer", ref SpeedometerSetting.Value, speedometerTypes);
                if (SpeedometerSetting == 2) Grid("• AHSS Damage", ref SpeedometerAHSSSetting.Value, ahssSpeedometerTypes);
            }
            GUILayout.EndArea();
        }

        private static void Server()
        {
            GUILayout.BeginArea(center[1]);
            {
                GUILayout.FlexibleSpace();
                GUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    Grid(string.Empty, ref serverSwitch, serverPages, width: 225f, height: 25f);
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                }
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndArea();

            switch (serverSwitch)
            {
                case 0:
                    {
                        GUILayout.BeginArea(left[2]);
                        {
                            scrollServerTitansLeft = GUILayout.BeginScrollView(scrollServerTitansLeft);
                            {
                                Label("General", LabelType.Header);
                                Grid("Custom Starter Titans", ref CustomStarterTitansSetting.Value);
                                if (CustomStarterTitansSetting) TextField("• Amount", ref StarterAmountSetting.Value);
                                Grid("Custom Titans/Wave", ref CustomTitansPerWaveSetting.Value);
                                if (CustomTitansPerWaveSetting) TextField("• Amount", ref TitansPerWaveSetting.Value);
                                Grid("Custom Spawn Rate", ref CustomSpawnRateSetting.Value);
                                if (CustomSpawnRateSetting)
                                {
                                    float[] freePoints = { 100f - (SpawnRateSettings[1] + SpawnRateSettings[2] + SpawnRateSettings[3] + SpawnRateSettings[4]), 100f - (SpawnRateSettings[0] + SpawnRateSettings[2] + SpawnRateSettings[3] + SpawnRateSettings[4]), 100f - (SpawnRateSettings[0] + SpawnRateSettings[1] + SpawnRateSettings[3] + SpawnRateSettings[4]), 100f - (SpawnRateSettings[0] + SpawnRateSettings[1] + SpawnRateSettings[2] + SpawnRateSettings[4]), 100f - (SpawnRateSettings[0] + SpawnRateSettings[1] + SpawnRateSettings[2] + SpawnRateSettings[3]) };
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
                                GUILayout.Space(1f);
                            }
                            GUILayout.EndScrollView();
                        }
                        GUILayout.EndArea();

                        GUILayout.BeginArea(right[2]);
                        {
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
                        }
                        GUILayout.EndArea();
                        break;
                    }

                case 1:
                    {
                        GUILayout.BeginArea(left[2]);
                        {
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
                        }
                        GUILayout.EndArea();

                        GUILayout.BeginArea(right[2]);
                        {
                            Label("Other", LabelType.Header);
                            Grid("Auto Revive", ref AutoReviveSetting.Value);
                            if (AutoReviveSetting) TextField("• Seconds", ref AutoReviveTimeSetting.Value);
                            Grid("Horses", ref HorsesSetting.Value);
                            Grid("Disable Minimaps", ref DisableMinimapsSetting.Value);
                            Grid("No AHSS Air-Reloading", ref DisableAHSSAirReloadingSetting.Value);
                            Grid("Deadly Cannons Mode", ref DeadlyCannonsModeSetting.Value);
                        }
                        GUILayout.EndArea();
                        break;
                    }

                case 2:
                    {
                        GUILayout.BeginArea(left[2]);
                        {
                            scrollServerMiscLeft = GUILayout.BeginScrollView(scrollServerMiscLeft);
                            {
                                Label("Chat", LabelType.Header);
                                Grid("Legacy Chat", ref LegacyChatSetting.Value);
                                TextField("Size", ref ChatSizeSetting.Value);
                                TextField("Major Color", ref ChatMajorColorSetting.Value);
                                TextField("Minor Color", ref ChatMinorColorSetting.Value);
                                ButtonToggle("Major Format", formatOptions, ChatMajorFormatSettings);
                                ButtonToggle("Minor Format", formatOptions, ChatMinorFormatSettings);
                                Label("Welcome Message", LabelType.Header);
                                GUILayout.BeginHorizontal();
                                {
                                    GUILayout.FlexibleSpace();
                                    if (GUILayout.Button("Bold", GUILayout.Width(75f)))
                                    {
                                        WelcomeMessageSetting.Value = $"<b>{WelcomeMessageSetting}</b>";
                                    }

                                    if (GUILayout.Button("Italic", GUILayout.Width(75f)))
                                    {
                                        WelcomeMessageSetting.Value = $"<i>{WelcomeMessageSetting}</i>";
                                    }

                                    GUILayout.FlexibleSpace();
                                }
                                GUILayout.EndHorizontal();
                                GUILayout.BeginHorizontal();
                                {
                                    GUILayout.FlexibleSpace();
                                    TextArea(string.Empty, ref WelcomeMessageSetting.Value, halfAreaWidth - 15f, 185f);
                                    GUILayout.FlexibleSpace();
                                }
                                GUILayout.EndHorizontal();
                                GUILayout.Space(1f);
                            }
                            GUILayout.EndScrollView();
                        }
                        GUILayout.EndArea();

                        GUILayout.BeginArea(right[2]);
                        {
                            Label("Protection", LabelType.Header);
                            Grid("Connection Protocol", ref ConnectionProtocolSettings.Value, connectionProtocols);
                            Grid("Anti Titan Eren", ref AntiTitanErenSetting.Value);
                        }
                        GUILayout.EndArea();
                        break;
                    }
            }
        }

        private static void VideoAndAudio()
        {
            GUILayout.BeginArea(center[1]);
            {
                GUILayout.FlexibleSpace();
                GUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    videoAndAudioSwitch = GUILayout.SelectionGrid(videoAndAudioSwitch, videoAndAudioPages, 2, GUILayout.Width(175f), GUILayout.Height(25f));
                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndHorizontal();
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndArea();

            switch (videoAndAudioSwitch)
            {
                case 0:
                    GUILayout.BeginArea(left[2]);
                    {
                        GUILayout.BeginVertical();
                        {
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
                        }
                        GUILayout.EndVertical();
                    }
                    GUILayout.EndArea();

                    GUILayout.BeginArea(right[2]);
                    {
                        GUILayout.BeginVertical();
                        {
                            scrollVideoLeft = GUILayout.BeginScrollView(scrollVideoLeft);
                            {
                                Label("Visuals", LabelType.Header);
                                Grid("Wind", ref WindSetting.Value);
                                Grid("Blur", ref BlurSetting.Value);
                                Grid("Custom Ambient", ref CustomAmbientSetting.Value);
                                if (CustomAmbientSetting)
                                {
                                    Grid("Day Time", ref ambientDayTimeSwitch, dayTime);
                                    switch (ambientDayTimeSwitch)
                                    {
                                        case 0:
                                            Slider("Color R", ref CustomAmbientColorSetting[0][0].Value, 0f, 1f, 160f, 25f);
                                            Slider("Color G", ref CustomAmbientColorSetting[0][1].Value, 0f, 1f, 160f, 25f);
                                            Slider("Color B", ref CustomAmbientColorSetting[0][2].Value, 0f, 1f, 160f, 25f);
                                            break;

                                        case 1:
                                            Slider("Color R", ref CustomAmbientColorSetting[1][0].Value, 0f, 1f, 160f, 25f);
                                            Slider("Color G", ref CustomAmbientColorSetting[1][1].Value, 0f, 1f, 160f, 25f);
                                            Slider("Color B", ref CustomAmbientColorSetting[1][2].Value, 0f, 1f, 160f, 25f);
                                            break;

                                        case 2:
                                            Slider("Color R", ref CustomAmbientColorSetting[2][0].Value, 0f, 1f, 160f, 25f);
                                            Slider("Color G", ref CustomAmbientColorSetting[2][1].Value, 0f, 1f, 160f, 25f);
                                            Slider("Color B", ref CustomAmbientColorSetting[2][2].Value, 0f, 1f, 160f, 25f);
                                            break;
                                    }
                                }

                                Grid("Custom Fog", ref CustomFogSetting.Value);
                                if (CustomFogSetting)
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
                                    Slider("Start Distance", ref FogDistanceSettings[0].Value, 0f, ref DrawDistanceSetting.Value, 160f, 25f, valueTextFormat: "0");
                                    Slider("End Distance", ref FogDistanceSettings[1].Value, 0f, ref DrawDistanceSetting.Value, 160f, 25f, valueTextFormat: "0");
                                }

                                Grid("Custom Light", ref CustomLightSetting.Value);
                                if (CustomLightSetting)
                                {
                                    Grid("Day Time", ref ambientDayTimeSwitch, dayTime);
                                    switch (ambientDayTimeSwitch)
                                    {
                                        case 0:
                                            Slider("Color R", ref CustomLightColorSettings[0][0].Value, 0f, 1f, 160f, 25f);
                                            Slider("Color G", ref CustomLightColorSettings[0][1].Value, 0f, 1f, 160f, 25f);
                                            Slider("Color B", ref CustomLightColorSettings[0][2].Value, 0f, 1f, 160f, 25f);
                                            break;

                                        case 1:
                                            Slider("Color R", ref CustomLightColorSettings[1][0].Value, 0f, 1f, 160f, 25f);
                                            Slider("Color G", ref CustomLightColorSettings[1][1].Value, 0f, 1f, 160f, 25f);
                                            Slider("Color B", ref CustomLightColorSettings[1][2].Value, 0f, 1f, 160f, 25f);
                                            break;

                                        case 2:
                                            Slider("Color R", ref CustomLightColorSettings[2][0].Value, 0f, 1f, 160f, 25f);
                                            Slider("Color G", ref CustomLightColorSettings[2][1].Value, 0f, 1f, 160f, 25f);
                                            Slider("Color B", ref CustomLightColorSettings[2][2].Value, 0f, 1f, 160f, 25f);
                                            break;
                                    }
                                }

                                GUILayout.Space(1f);
                            }
                            GUILayout.EndScrollView();
                        }
                        GUILayout.EndVertical();
                    }
                    GUILayout.EndArea();

                    break;

                case 1:
                    GUILayout.BeginArea(left[2]);
                    {
                        Label("General", LabelType.Header);
                        Slider("Global", ref GlobalVolumeSetting.Value, 0f, 1f, round: true, multiplier: 100f);
                        Label("Human", LabelType.Header);
                        Slider("AHSS Shot", ref AHSSShotVolumeSetting.Value, 0f, 1f, round: true, multiplier: 100f);
                        Slider("Air Slash", ref AirSlashVolumeSetting.Value, 0f, 1f, round: true, multiplier: 100f);
                        Slider("Nape Slash", ref NapeSlashVolumeSetting.Value, 0f, 1f, round: true, multiplier: 100f);
                        Slider("Body Slash", ref BodySlashVolumeSetting.Value, 0f, 1f, round: true, multiplier: 100f);
                        Slider("Hooks", ref HookVolumeSetting.Value, 0f, 1f, round: true, multiplier: 100f);
                        Label("Titan", LabelType.Header);
                        Slider("Titan Eren Roar", ref TitanErenRoarVolumeSetting.Value, 0f, 1f, round: true, multiplier: 100f);
                        Slider("Swing", ref SwingVolumeSetting.Value, 0f, 1f, round: true, multiplier: 100f);
                        Slider("Thunder", ref ThunderVolumeSetting.Value, 0f, 1f, round: true, multiplier: 100f);
                        Slider("Head Explosion", ref HeadExplosionVolumeSetting.Value, 0f, 1f, round: true, multiplier: 100f);
                        Slider("Head Punch", ref HeadPunchVolumeSetting.Value, 0f, 1f, round: true, multiplier: 100f);
                        Slider("Explosion", ref BoomVolumeSetting.Value, 0f, 1f, round: true, multiplier: 100f);
                        Slider("Steps", ref StepVolumeSetting.Value, 0f, 1f, round: true, multiplier: 100f);
                    }
                    GUILayout.EndArea();
                    break;
            }
        }

        private static bool[] HotKeysToWait;
        private static int HotKeyToRebind = -1;

        private static void Rebinds()
        {
            if (FengGameManagerMKII.inputManager == null) return;

            GUILayout.BeginArea(center[1]);
            {
                GUILayout.FlexibleSpace();
                GUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    rebindsSwitch = GUILayout.SelectionGrid(rebindsSwitch, rebindsPages, 4, GUILayout.Height(25f));
                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndHorizontal();
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndArea();

            Event current;
            bool isPressed;

            switch (rebindsSwitch)
            {
                case 0:
                    {
                        GUILayout.BeginArea(left[2]);
                        {
                            for (var i = 0; i < humanRebinds.Length / 2; i++)
                            {
                                GUILayout.BeginHorizontal();
                                {
                                    var width = 225f;

                                    Label(humanRebinds[i], width: width);

                                    if (GUILayout.Button(FengGameManagerMKII.inputManager.getKeyRC(i)))
                                    {
                                        FengGameManagerMKII.settings[100] = i + 1;
                                        FengGameManagerMKII.inputManager.setNameRC(i, "waiting...");
                                    }
                                }
                                GUILayout.EndHorizontal();
                            }
                        }
                        GUILayout.EndArea();

                        GUILayout.BeginArea(right[2]);
                        {
                            for (var i = humanRebinds.Length / 2; i < humanRebinds.Length; i++)
                            {
                                GUILayout.BeginHorizontal();
                                {
                                    var width = 225f;
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
                                        var k = i == 22 ? 98 : i == 23 ? 99 : i == 24 ? 182 : i == 25 ? 232 : i == 26 ? 233 : i == 27 ? 234 : i == 28 ? 236 : 262;

                                        var style = new GUIStyle(UnityEngine.GUI.skin.button);
                                        string[] txt = { "Off", "On" };
                                        if (i == 22)
                                        {
                                            style.normal = ReelingSettings[0] ? UnityEngine.GUI.skin.button.onNormal : UnityEngine.GUI.skin.button.normal;
                                            style.hover = ReelingSettings[0] ? UnityEngine.GUI.skin.button.onHover : UnityEngine.GUI.skin.button.hover;
                                            if (GUILayout.Button(ReelingSettings[0] ? txt[1] : txt[0], style, GUILayout.Width(50f)))
                                            {
                                                ReelingSettings[0].Value = !ReelingSettings[0];
                                            }
                                        }
                                        else if (i == 23)
                                        {
                                            style.normal = ReelingSettings[1] ? UnityEngine.GUI.skin.button.onNormal : UnityEngine.GUI.skin.button.normal;
                                            style.hover = ReelingSettings[1] ? UnityEngine.GUI.skin.button.onHover : UnityEngine.GUI.skin.button.hover;
                                            if (GUILayout.Button(ReelingSettings[1] ? txt[1] : txt[0], style, GUILayout.Width(50f)))
                                            {
                                                ReelingSettings[1].Value = !ReelingSettings[1];
                                            }
                                        }
                                        else if (i == 24)
                                        {
                                            style.normal = DashSetting ? UnityEngine.GUI.skin.button.onNormal : UnityEngine.GUI.skin.button.normal;
                                            style.hover = DashSetting ? UnityEngine.GUI.skin.button.onHover : UnityEngine.GUI.skin.button.hover;
                                            if (GUILayout.Button(DashSetting ? txt[1] : txt[0], style, GUILayout.Width(50f)))
                                            {
                                                DashSetting.Value = !DashSetting;
                                            }
                                        }

                                        if (GUILayout.Button((string)FengGameManagerMKII.settings[k]))
                                        {
                                            FengGameManagerMKII.settings[k] = "waiting...";
                                            FengGameManagerMKII.settings[100] = k;
                                        }
                                    }
                                }
                                GUILayout.EndHorizontal();
                            }
                        }
                        GUILayout.EndArea();

                        if ((int)FengGameManagerMKII.settings[100] != 0)
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
                                    if (Input.GetKeyDown((KeyCode)(323 + i)))
                                    {
                                        isPressed = true;
                                        str4 = "Mouse" + Convert.ToString(i);
                                    }
                                }
                            }

                            if (isPressed)
                            {
                                if ((int)FengGameManagerMKII.settings[100] == 98)
                                {
                                    FengGameManagerMKII.settings[98] = str4;
                                    FengGameManagerMKII.settings[100] = 0;
                                    FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.reelin, str4);
                                }
                                else if ((int)FengGameManagerMKII.settings[100] == 99)
                                {
                                    FengGameManagerMKII.settings[99] = str4;
                                    FengGameManagerMKII.settings[100] = 0;
                                    FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.reelout, str4);
                                }
                                else if ((int)FengGameManagerMKII.settings[100] == 182)
                                {
                                    FengGameManagerMKII.settings[182] = str4;
                                    FengGameManagerMKII.settings[100] = 0;
                                    FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.dash, str4);
                                }
                                else if ((int)FengGameManagerMKII.settings[100] == 232)
                                {
                                    FengGameManagerMKII.settings[232] = str4;
                                    FengGameManagerMKII.settings[100] = 0;
                                    FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.mapMaximize, str4);
                                }
                                else if ((int)FengGameManagerMKII.settings[100] == 233)
                                {
                                    FengGameManagerMKII.settings[233] = str4;
                                    FengGameManagerMKII.settings[100] = 0;
                                    FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.mapToggle, str4);
                                }
                                else if ((int)FengGameManagerMKII.settings[100] == 234)
                                {
                                    FengGameManagerMKII.settings[234] = str4;
                                    FengGameManagerMKII.settings[100] = 0;
                                    FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.mapReset, str4);
                                }
                                else if ((int)FengGameManagerMKII.settings[100] == 236)
                                {
                                    FengGameManagerMKII.settings[236] = str4;
                                    FengGameManagerMKII.settings[100] = 0;
                                    FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.chat, str4);
                                }
                                else if ((int)FengGameManagerMKII.settings[100] == 262)
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
                                        if ((int)FengGameManagerMKII.settings[100] == k)
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
                        {
                            for (var i = 0; i < titanRebinds.Length / 2; i++)
                            {
                                var k = 101 + i;
                                GUILayout.BeginHorizontal();
                                {
                                    Label(titanRebinds[i], width: 225f);
                                    if (GUILayout.Button((string)FengGameManagerMKII.settings[k]))
                                    {
                                        FengGameManagerMKII.settings[k] = "waiting...";
                                        FengGameManagerMKII.settings[100] = k;
                                    }
                                }
                                GUILayout.EndHorizontal();
                            }
                        }
                        GUILayout.EndArea();

                        GUILayout.BeginArea(right[2]);
                        {
                            for (var i = titanRebinds.Length / 2; i < titanRebinds.Length; i++)
                            {
                                var k = 101 + i;
                                GUILayout.BeginHorizontal();
                                {
                                    Label(titanRebinds[i], width: 225f);
                                    if (GUILayout.Button((string)FengGameManagerMKII.settings[k]))
                                    {
                                        FengGameManagerMKII.settings[k] = "waiting...";
                                        FengGameManagerMKII.settings[100] = k;
                                    }
                                }
                                GUILayout.EndHorizontal();
                            }
                        }
                        GUILayout.EndArea();

                        if ((int)FengGameManagerMKII.settings[100] != 0)
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
                                    if (Input.GetKeyDown((KeyCode)(323 + i)))
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
                                    if ((int)FengGameManagerMKII.settings[100] == k)
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
                        {
                            Label("Horse", LabelType.Header);
                            for (var i = 0; i < horseRebinds.Length; i++)
                            {
                                var k = 237 + i;
                                GUILayout.BeginHorizontal();
                                {
                                    Label(horseRebinds[i], width: 225f);
                                    if (GUILayout.Button((string)FengGameManagerMKII.settings[k]))
                                    {
                                        FengGameManagerMKII.settings[k] = "waiting...";
                                        FengGameManagerMKII.settings[100] = k;
                                    }
                                }
                                GUILayout.EndHorizontal();
                            }
                        }
                        GUILayout.EndArea();

                        GUILayout.BeginArea(right[2]);
                        {
                            Label("Cannon", LabelType.Header);
                            for (var i = 0; i < cannonRebinds.Length; i++)
                            {
                                var k = 254 + i;
                                GUILayout.BeginHorizontal();
                                {
                                    Label(horseRebinds[i], width: 225f);
                                    if (GUILayout.Button((string)FengGameManagerMKII.settings[k]))
                                    {
                                        FengGameManagerMKII.settings[k] = "waiting...";
                                        FengGameManagerMKII.settings[100] = k;
                                    }
                                }
                                GUILayout.EndHorizontal();
                            }
                        }
                        GUILayout.EndArea();

                        if ((int)FengGameManagerMKII.settings[100] != 0)
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
                                    if (Input.GetKeyDown((KeyCode)(323 + i)))
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
                                    if ((int)FengGameManagerMKII.settings[100] == k)
                                    {
                                        FengGameManagerMKII.settings[k] = str4;
                                        FengGameManagerMKII.settings[100] = 0;
                                        FengGameManagerMKII.inputRC.setInputHorse(i, str4);
                                    }

                                    k = 254 + i;
                                    if ((int)FengGameManagerMKII.settings[100] == k)
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

                case 3:
                    {
                        GUILayout.BeginArea(left[2]);
                        {
                            Label("Special", LabelType.Header);
                            if (HotKeysToWait == null) HotKeysToWait = new bool[HotKey.AllHotKeys.Count];
                            for (var i = 0; i < HotKey.AllHotKeys.Count; i++)
                            {
                                GUILayout.BeginHorizontal();
                                {
                                    Label(HotKey.AllHotKeys[i].Name, width: 225f);
                                    if (GUILayout.Button(HotKeysToWait[i] ? "waiting..." : HotKey.AllHotKeys[i].Key.ToString()))
                                    {
                                        HotKeyToRebind = i;
                                        HotKeysToWait[i] = true;
                                    }
                                }
                                GUILayout.EndHorizontal();
                            }

                            var eventCurrent = Event.current;

                            if (HotKeyToRebind != -1 && eventCurrent.type == EventType.KeyDown && eventCurrent.keyCode != KeyCode.None)
                            {
                                HotKey.AllHotKeys[HotKeyToRebind].Rebind(eventCurrent.keyCode);
                                HotKeysToWait[HotKeyToRebind] = false;
                                HotKeyToRebind = -1;
                            }
                        }
                        GUILayout.EndArea();
                        break;
                    }
            }
        }

        private static void Bombs()
        {
            GUILayout.BeginArea(left[3]);
            {
                GUILayout.Space(15f);
                Label("Stats", LabelType.Header);
                int[] freePoints = { 20 - (BombSettings[1] + BombSettings[2] + BombSettings[3]), 20 - (BombSettings[0] + BombSettings[2] + BombSettings[3]), 20 - (BombSettings[0] + BombSettings[1] + BombSettings[3]), 20 - (BombSettings[0] + BombSettings[1] + BombSettings[2]) };
                for (var i = 0; i < 4; i++)
                {
                    GUILayout.BeginHorizontal();
                    {
                        Slider(bombStats[i], ref BombSettings[i].Value, 0, freePoints[i] > 10 ? 10 : freePoints[i]);
                    }
                    GUILayout.EndHorizontal();
                }
            }
            GUILayout.EndArea();

            GUILayout.BeginArea(right[3]);
            {
                GUILayout.Space(15f);
                Label("Color", LabelType.Header);
                Slider("R", ref BombColorSetting[0].Value, 0f, 1f);
                Slider("G", ref BombColorSetting[1].Value, 0f, 1f);
                Slider("B", ref BombColorSetting[2].Value, 0f, 1f);
                var txt = new Texture2D(1, 1);
                txt.SetPixel(0, 0, new Color(BombColorSetting[0], BombColorSetting[1], BombColorSetting[2]));
                txt.Apply();
                UnityEngine.GUI.DrawTexture(new Rect(45f, 55f, 70f, 70f), txt, ScaleMode.StretchToFill);
            }
            GUILayout.EndArea();

            GUILayout.BeginArea(center[3]);
            {
                GUILayout.Space(15f);
                GUILayout.Label("Color Presets", HeaderStyle);
                var style = new GUIStyle();
                const float size = 13f;
                GUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    for (var i = 0; i < 35; i++)
                    {
                        style.normal.background = style.hover.background = style.active.background = ColorCache.Textures[ColorCache.White];
                        if (GUILayout.Button("", style, GUILayout.Height(size), GUILayout.Width(size)))
                        {
                            BombColorSetting[0].Value = ColorCache.Textures.First().Key.r;
                            BombColorSetting[1].Value = ColorCache.Textures.First().Key.g;
                            BombColorSetting[2].Value = ColorCache.Textures.First().Key.b;
                        }
                    }

                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndHorizontal();

                for (var i = 1; i < ColorCache.Textures.Count - 36; i++)
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.FlexibleSpace();
                        for (var j = 0; j < 35; j++, i++)
                        {
                            if (i == ColorCache.Textures.Count) break;

                            var texture = ColorCache.Textures.ElementAt(i).Value;
                            style.normal.background = style.hover.background = style.active.background = texture;
                            if (GUILayout.Button("", style, GUILayout.Height(size), GUILayout.Width(size)))
                            {
                                BombColorSetting[0].Value = ColorCache.Textures.ElementAt(i).Key.r;
                                BombColorSetting[1].Value = ColorCache.Textures.ElementAt(i).Key.g;
                                BombColorSetting[2].Value = ColorCache.Textures.ElementAt(i).Key.b;
                            }
                        }

                        GUILayout.FlexibleSpace();
                    }
                    GUILayout.EndHorizontal();
                }

                GUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    for (var i = 0; i < 35; i++)
                    {
                        style.normal.background = style.hover.background = style.active.background = ColorCache.Textures[ColorCache.Black];
                        if (GUILayout.Button("", style, GUILayout.Height(size), GUILayout.Width(size)))
                        {
                            BombColorSetting[0].Value = ColorCache.Textures.Last().Key.r;
                            BombColorSetting[1].Value = ColorCache.Textures.Last().Key.g;
                            BombColorSetting[2].Value = ColorCache.Textures.Last().Key.b;
                        }
                    }

                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndArea();
        }

        private static void HumanSkins()
        {
            GUILayout.BeginArea(left[0]);
            {
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
                {
                    GUILayout.BeginHorizontal(GUILayout.Width(leftElementWidth + rightElementWidth + 15f));
                    {
                        GUILayout.FlexibleSpace();
                        HumanSkinsCurrentSetSetting.Value = GUILayout.SelectionGrid(HumanSkinsCurrentSetSetting, HumanSkinsTitlesList.ToArray(), 1, GUILayout.Width(175f));
                        GUILayout.FlexibleSpace();
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Space(1f);
                }
                GUILayout.EndScrollView();

                GUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("Add"))
                    {
                        HumanSkinsTitlesList.Add("Set " + (HumanSkinsTitlesList.Count + 1));
                        HumanSkinsCurrentSetSetting.Value = HumanSkinsTitlesList.Count - 1;
                        HumanSkinsList.Add("````````````".Split('`'));
                        HumanSkinsCountSetting.Value++;
                        scrollHumanSkins.y = 9999f;
                    }

                    if (GUILayout.Button("Remove"))
                    {
                        if (HumanSkinsCountSetting == 1)
                        {
                            HumanSkinsTitlesList[HumanSkinsCurrentSetSetting] = "Set 1";
                            HumanSkinsList[HumanSkinsCurrentSetSetting] = "````````````".Split('`');
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
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndArea();

            GUILayout.BeginArea(right[0]);
            {
                GUILayout.Space(15f);

                Label("Skins", LabelType.Header);
                Label(HumanSkinsTitlesList[HumanSkinsCurrentSetSetting], LabelType.SubHeader);
                GUILayout.BeginHorizontal();
                {
                    Label("Title");
                    HumanSkinsTitlesList[HumanSkinsCurrentSetSetting] = GUILayout.TextField(HumanSkinsTitlesList[HumanSkinsCurrentSetSetting], GUILayout.Width(TextFieldWidth));
                }
                GUILayout.EndHorizontal();
                for (var i = 0; i < humanSkinFields.Length; i++)
                {
                    TextField(humanSkinFields[i], ref HumanSkinsList[HumanSkinsCurrentSetSetting][i]);
                }

                GUILayout.Space(10f);

                GUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Copy"))
                    {
                        CopiedHumanSkins = HumanSkinsList[HumanSkinsCurrentSetSetting];
                    }

                    if (GUILayout.Button("Paste"))
                    {
                        if (CopiedHumanSkins != null)
                            HumanSkinsList[HumanSkinsCurrentSetSetting] = CopiedHumanSkins;
                    }

                    if (GUILayout.Button("Reset"))
                    {
                        HumanSkinsTitlesList[HumanSkinsCurrentSetSetting] = "Set " + (HumanSkinsCurrentSetSetting + 1);
                        HumanSkinsList[HumanSkinsCurrentSetSetting] = "````````````".Split('`');
                    }

                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndArea();
        }

        private static void TitanSkins()
        {
            int num45;
            int num46;
            UnityEngine.GUI.Label(new Rect(leftPos + 40f + 270f, topPos + 25f + 52f, 120f, 30f), "Titan Skin Mode:", "Label");
            var flag6 = false;
            if ((int)FengGameManagerMKII.settings[1] == 1)
            {
                flag6 = true;
            }

            var flag11 = UnityEngine.GUI.Toggle(new Rect(leftPos + 40f + 390f, topPos + 25f + 52f, 40f, 20f), flag6, "On");
            if (flag6 != flag11)
            {
                if (flag11)
                {
                    FengGameManagerMKII.settings[1] = 1;
                }
                else
                {
                    FengGameManagerMKII.settings[1] = 0;
                }
            }

            UnityEngine.GUI.Label(new Rect(leftPos + 40f + 270f, topPos + 25f + 77f, 120f, 30f), "Randomized Pairs:", "Label");
            flag6 = false;
            if ((int)FengGameManagerMKII.settings[32] == 1)
            {
                flag6 = true;
            }

            flag11 = UnityEngine.GUI.Toggle(new Rect(leftPos + 40f + 390f, topPos + 25f + 77f, 40f, 20f), flag6, "On");
            if (flag6 != flag11)
            {
                if (flag11)
                {
                    FengGameManagerMKII.settings[32] = 1;
                }
                else
                {
                    FengGameManagerMKII.settings[32] = 0;
                }
            }

            UnityEngine.GUI.Label(new Rect(leftPos + 40f + 158f, topPos + 25f + 112f, 150f, 20f), "Titan Hair:", "Label");
            FengGameManagerMKII.settings[21] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 80f, topPos + 25f + 134f, 160f, 20f), (string)FengGameManagerMKII.settings[21]);
            FengGameManagerMKII.settings[22] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 80f, topPos + 25f + 156f, 160f, 20f), (string)FengGameManagerMKII.settings[22]);
            FengGameManagerMKII.settings[23] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 80f, topPos + 25f + 178f, 160f, 20f), (string)FengGameManagerMKII.settings[23]);
            FengGameManagerMKII.settings[24] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 80f, topPos + 25f + 200f, 160f, 20f), (string)FengGameManagerMKII.settings[24]);
            FengGameManagerMKII.settings[25] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 80f, topPos + 25f + 222f, 160f, 20f), (string)FengGameManagerMKII.settings[25]);
            if (UnityEngine.GUI.Button(new Rect(leftPos + 40f + 250f, topPos + 25f + 134f, 65f, 20f), FengGameManagerMKII.FGM.hairtype((int)FengGameManagerMKII.settings[16])))
            {
                num45 = 16;
                num46 = (int)FengGameManagerMKII.settings[num45];
                if (num46 >= 9)
                {
                    num46 = -1;
                }
                else
                {
                    num46++;
                }

                FengGameManagerMKII.settings[num45] = num46;
            }
            else if (UnityEngine.GUI.Button(new Rect(leftPos + 40f + 250f, topPos + 25f + 156f, 65f, 20f), FengGameManagerMKII.FGM.hairtype((int)FengGameManagerMKII.settings[17])))
            {
                num45 = 17;
                num46 = (int)FengGameManagerMKII.settings[num45];
                if (num46 >= 9)
                {
                    num46 = -1;
                }
                else
                {
                    num46++;
                }

                FengGameManagerMKII.settings[num45] = num46;
            }
            else if (UnityEngine.GUI.Button(new Rect(leftPos + 40f + 250f, topPos + 25f + 178f, 65f, 20f), FengGameManagerMKII.FGM.hairtype((int)FengGameManagerMKII.settings[18])))
            {
                num45 = 18;
                num46 = (int)FengGameManagerMKII.settings[num45];
                if (num46 >= 9)
                {
                    num46 = -1;
                }
                else
                {
                    num46++;
                }

                FengGameManagerMKII.settings[num45] = num46;
            }
            else if (UnityEngine.GUI.Button(new Rect(leftPos + 40f + 250f, topPos + 25f + 200f, 65f, 20f), FengGameManagerMKII.FGM.hairtype((int)FengGameManagerMKII.settings[19])))
            {
                num45 = 19;
                num46 = (int)FengGameManagerMKII.settings[num45];
                if (num46 >= 9)
                {
                    num46 = -1;
                }
                else
                {
                    num46++;
                }

                FengGameManagerMKII.settings[num45] = num46;
            }
            else if (UnityEngine.GUI.Button(new Rect(leftPos + 40f + 250f, topPos + 25f + 222f, 65f, 20f), FengGameManagerMKII.FGM.hairtype((int)FengGameManagerMKII.settings[20])))
            {
                num45 = 20;
                num46 = (int)FengGameManagerMKII.settings[num45];
                if (num46 >= 9)
                {
                    num46 = -1;
                }
                else
                {
                    num46++;
                }

                FengGameManagerMKII.settings[num45] = num46;
            }

            UnityEngine.GUI.Label(new Rect(leftPos + 40f + 158f, topPos + 25f + 252f, 150f, 20f), "Titan Eye:", "Label");
            FengGameManagerMKII.settings[26] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 80f, topPos + 25f + 274f, 230f, 20f), (string)FengGameManagerMKII.settings[26]);
            FengGameManagerMKII.settings[27] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 80f, topPos + 25f + 296f, 230f, 20f), (string)FengGameManagerMKII.settings[27]);
            FengGameManagerMKII.settings[28] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 80f, topPos + 25f + 318f, 230f, 20f), (string)FengGameManagerMKII.settings[28]);
            FengGameManagerMKII.settings[29] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 80f, topPos + 25f + 340f, 230f, 20f), (string)FengGameManagerMKII.settings[29]);
            FengGameManagerMKII.settings[30] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 80f, topPos + 25f + 362f, 230f, 20f), (string)FengGameManagerMKII.settings[30]);
            UnityEngine.GUI.Label(new Rect(leftPos + 40f + 455f, topPos + 25f + 112f, 150f, 20f), "Titan Body:", "Label");
            FengGameManagerMKII.settings[86] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 390f, topPos + 25f + 134f, 230f, 20f), (string)FengGameManagerMKII.settings[86]);
            FengGameManagerMKII.settings[87] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 390f, topPos + 25f + 156f, 230f, 20f), (string)FengGameManagerMKII.settings[87]);
            FengGameManagerMKII.settings[88] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 390f, topPos + 25f + 178f, 230f, 20f), (string)FengGameManagerMKII.settings[88]);
            FengGameManagerMKII.settings[89] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 390f, topPos + 25f + 200f, 230f, 20f), (string)FengGameManagerMKII.settings[89]);
            FengGameManagerMKII.settings[90] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 390f, topPos + 25f + 222f, 230f, 20f), (string)FengGameManagerMKII.settings[90]);
            UnityEngine.GUI.Label(new Rect(leftPos + 40f + 472f, topPos + 25f + 252f, 150f, 20f), "Eren:", "Label");
            FengGameManagerMKII.settings[65] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 390f, topPos + 25f + 274f, 230f, 20f), (string)FengGameManagerMKII.settings[65]);
            UnityEngine.GUI.Label(new Rect(leftPos + 40f + 470f, topPos + 25f + 296f, 150f, 20f), "Annie:", "Label");
            FengGameManagerMKII.settings[66] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 390f, topPos + 25f + 318f, 230f, 20f), (string)FengGameManagerMKII.settings[66]);
            UnityEngine.GUI.Label(new Rect(leftPos + 40f + 465f, topPos + 25f + 340f, 150f, 20f), "Colossal:", "Label");
            FengGameManagerMKII.settings[67] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 390f, topPos + 25f + 362f, 230f, 20f), (string)FengGameManagerMKII.settings[67]);
        }

        private static void LocationSkins()
        {
            switch (locationSkinsSwitch)
            {
                case 0:
                    {
                        GUILayout.BeginArea(left[0]);
                        {
                            GUILayout.Space(15f);

                            Label("Forest", LabelType.Header);
                            Label("Settings", LabelType.SubHeader);
                            Grid("Skins Appearance", ref LocationSkinsSetting.Value, skinsAppearanceType);
                            Grid("Location", ref locationSkinsSwitch, locationSkinsLocation);
                            Grid("Randomized Pairs", ref LocationSkinsRandomizedPairsSetting.Value);

                            Label("Presets", LabelType.SubHeader);
                            scrollLocationSkinsForestLeft = GUILayout.BeginScrollView(scrollLocationSkinsForestLeft);
                            {
                                GUILayout.BeginHorizontal(GUILayout.Width(leftElementWidth + rightElementWidth + 15f));
                                {
                                    GUILayout.FlexibleSpace();
                                    LocationSkinsForestCurrentSetSetting.Value = GUILayout.SelectionGrid(LocationSkinsForestCurrentSetSetting, LocationSkinsForestTitlesList.ToArray(), 1, GUILayout.Width(175f));
                                    GUILayout.FlexibleSpace();
                                }
                                GUILayout.EndHorizontal();
                                GUILayout.Space(1f);
                            }
                            GUILayout.EndScrollView();

                            GUILayout.BeginHorizontal();
                            {
                                if (GUILayout.Button("Add"))
                                {
                                    LocationSkinsForestList.Add("``````````````````````".Split('`'));
                                    LocationSkinsForestTitlesList.Add("Set " + (LocationSkinsForestTitlesList.Count + 1));
                                    LocationSkinsForestCurrentSetSetting.Value = LocationSkinsForestTitlesList.Count - 1;
                                    LocationSkinsForestAmbientList.Add(0);
                                    LocationSkinsForestAmbientSettingsList.Add(new float[] { CustomAmbientColorSetting[0][0], CustomAmbientColorSetting[0][1], CustomAmbientColorSetting[0][2] });
                                    LocationSkinsForestFogList.Add(0);
                                    LocationSkinsForestFogSettingsList.Add(new float[] { 0.066f, 0.066f, 0.066f, 0f, 1000f });
                                    LocationSkinsForestParticlesList.Add(0);
                                    LocationSkinsForestParticlesSettingsList.Add(new float[] { 1500f, 125f, 60f, 120f, 0.001f, 0f, 1f, 1f, 1f, 1f });
                                    LocationSkinsForestCountSetting.Value++;
                                    scrollLocationSkinsForestLeft.y = 9999f;
                                }

                                if (GUILayout.Button("Remove"))
                                {
                                    if (LocationSkinsForestCountSetting == 1)
                                    {
                                        LocationSkinsForestTitlesList[LocationSkinsForestCurrentSetSetting] = "Set 1";
                                        LocationSkinsForestList[LocationSkinsForestCurrentSetSetting] = "``````````````````````".Split('`');
                                        LocationSkinsForestAmbientList[LocationSkinsForestCurrentSetSetting] = 0;
                                        LocationSkinsForestAmbientSettingsList[LocationSkinsForestCurrentSetSetting] = new float[] { CustomAmbientColorSetting[0][0], CustomAmbientColorSetting[0][1], CustomAmbientColorSetting[0][2] };
                                        LocationSkinsForestFogList[LocationSkinsForestCurrentSetSetting] = 0;
                                        LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting] = new float[] { 0.066f, 0.066f, 0.066f, 0f, 1000f };
                                        LocationSkinsForestParticlesList[LocationSkinsForestCurrentSetSetting] = 0;
                                        LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting] = new float[] { 1500f, 125f, 60f, 120f, 0.001f, 0f, 1f, 1f, 1f, 1f };
                                        LocationSkinsForestLightList[LocationSkinsForestCurrentSetSetting] = 0;
                                        LocationSkinsForestLightSettingsList[LocationSkinsForestCurrentSetSetting] = new[] { 1f, 1f, 1f };
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
                            }
                            GUILayout.EndHorizontal();
                        }
                        GUILayout.EndArea();

                        GUILayout.BeginArea(right[0]);
                        {
                            GUILayout.Space(15f);

                            Label(LocationSkinsForestTitlesList[LocationSkinsForestCurrentSetSetting], LabelType.Header);
                            scrollLocationSkinsForestRight = GUILayout.BeginScrollView(scrollLocationSkinsForestRight);
                            {
                                GUILayout.BeginHorizontal();
                                {
                                    Label("Title");
                                    LocationSkinsForestTitlesList[LocationSkinsForestCurrentSetSetting] = GUILayout.TextField(LocationSkinsForestTitlesList[LocationSkinsForestCurrentSetSetting], GUILayout.Width(TextFieldWidth));
                                }
                                GUILayout.EndHorizontal();
                                for (var i = 0; i < locationSkinForestFields.Length; i++)
                                {
                                    TextField(locationSkinForestFields[i], ref LocationSkinsForestList[LocationSkinsForestCurrentSetSetting][i]);
                                }

                                GUILayout.BeginHorizontal();
                                {
                                    Label("Ambient");
                                    LocationSkinsForestAmbientList[LocationSkinsForestCurrentSetSetting] = GUILayout.SelectionGrid(LocationSkinsForestAmbientList[LocationSkinsForestCurrentSetSetting], SwitcherStr, 2, GUILayout.Width(ButtonWidth));
                                }
                                GUILayout.EndHorizontal();
                                if (LocationSkinsForestAmbientList[LocationSkinsForestCurrentSetSetting] == 1)
                                {
                                    Slider("R", ref LocationSkinsForestAmbientSettingsList[LocationSkinsForestCurrentSetSetting][0], 0f, 1f);
                                    Slider("G", ref LocationSkinsForestAmbientSettingsList[LocationSkinsForestCurrentSetSetting][1], 0f, 1f);
                                    Slider("B", ref LocationSkinsForestAmbientSettingsList[LocationSkinsForestCurrentSetSetting][2], 0f, 1f);
                                }

                                GUILayout.BeginHorizontal();
                                {
                                    Label("Fog");
                                    LocationSkinsForestFogList[LocationSkinsForestCurrentSetSetting] = GUILayout.SelectionGrid(LocationSkinsForestFogList[LocationSkinsForestCurrentSetSetting], SwitcherStr, 2, GUILayout.Width(ButtonWidth));
                                }
                                GUILayout.EndHorizontal();
                                if (LocationSkinsForestFogList[LocationSkinsForestCurrentSetSetting] == 1)
                                {
                                    Slider("R", ref LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][0], 0f, 1f);
                                    Slider("G", ref LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][1], 0f, 1f);
                                    Slider("B", ref LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][2], 0f, 1f);
                                    if (LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][3] > LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][4] && LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][3] != 0f)
                                        LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][3] = LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][4] - 0.1f;
                                    if (LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][3] < 0)
                                        LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][3] = 0;
                                    Slider("Start Distance", ref LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][3], 0f, 1000f, round: true);
                                    Slider("End Distance", ref LocationSkinsForestFogSettingsList[LocationSkinsForestCurrentSetSetting][4], 0f, 1000f, round: true);
                                }

                                GUILayout.BeginHorizontal();
                                {
                                    Label("Light");
                                    LocationSkinsForestLightList[LocationSkinsForestCurrentSetSetting] = GUILayout.SelectionGrid(LocationSkinsForestLightList[LocationSkinsForestCurrentSetSetting], SwitcherStr, 2, GUILayout.Width(ButtonWidth));
                                }
                                GUILayout.EndHorizontal();
                                if (LocationSkinsForestLightList[LocationSkinsForestCurrentSetSetting] == 1)
                                {
                                    Slider("R", ref LocationSkinsForestLightSettingsList[LocationSkinsForestCurrentSetSetting][0], 0f, 1f);
                                    Slider("G", ref LocationSkinsForestLightSettingsList[LocationSkinsForestCurrentSetSetting][1], 0f, 1f);
                                    Slider("B", ref LocationSkinsForestLightSettingsList[LocationSkinsForestCurrentSetSetting][2], 0f, 1f);
                                }

                                GUILayout.BeginHorizontal();
                                {
                                    Label("Particles");
                                    LocationSkinsForestParticlesList[LocationSkinsForestCurrentSetSetting] = GUILayout.SelectionGrid(LocationSkinsForestParticlesList[LocationSkinsForestCurrentSetSetting], SwitcherStr, 2, GUILayout.Width(ButtonWidth));
                                }
                                GUILayout.EndHorizontal();
                                if (LocationSkinsForestParticlesList[LocationSkinsForestCurrentSetSetting] == 1)
                                {
                                    Slider("Count", ref LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][0], 100, 15000, round: true);
                                    Slider("Height", ref LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][1], 0F, 1000f, round: true);
                                    if (LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][2] > LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][3] && LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][2] != 0f)
                                        LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][2] = LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][3] - 0.1f;
                                    if (LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][2] < 0)
                                        LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][2] = 0;
                                    Slider("Lifetime Minimum", ref LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][2], 0f, 600f, round: true);
                                    Slider("Lifetime Maximum", ref LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][3], 0f, 600f);
                                    Slider("Gravity", ref LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][4], 0f, 5f);
                                    Slider("R", ref LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][5], 0f, 1f);
                                    Slider("G", ref LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][6], 0f, 1f);
                                    Slider("B", ref LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][7], 0f, 1f);
                                    Slider("A", ref LocationSkinsForestParticlesSettingsList[LocationSkinsForestCurrentSetSetting][8], 0f, 1f);
                                }

                                GUILayout.Space(1f);
                            }
                            GUILayout.EndScrollView();

                            GUILayout.Space(10f);
                            GUILayout.BeginHorizontal();
                            {
                                GUILayout.FlexibleSpace();
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
                                    LocationSkinsForestList[LocationSkinsForestCurrentSetSetting] = "``````````````````````".Split('`');
                                    LocationSkinsForestAmbientList[LocationSkinsForestCountSetting] = 0;
                                    LocationSkinsForestAmbientSettingsList[LocationSkinsForestCountSetting] = new float[] { CustomAmbientColorSetting[0][0], CustomAmbientColorSetting[0][1], CustomAmbientColorSetting[0][2] };
                                    LocationSkinsForestFogList[LocationSkinsForestCountSetting] = 0;
                                    LocationSkinsForestFogSettingsList[LocationSkinsForestCountSetting] = new[] { 0.066f, 0.066f, 0.066f, 0f, 1000f };
                                    LocationSkinsForestParticlesList[LocationSkinsForestCountSetting] = 0;
                                    LocationSkinsForestParticlesSettingsList[LocationSkinsForestCountSetting] = new[] { 1500f, 125f, 60f, 120f, 0.001f, 0f, 1f, 1f, 1f, 1f };
                                    LocationSkinsForestLightList[LocationSkinsForestCurrentSetSetting] = 0;
                                    LocationSkinsForestLightSettingsList[LocationSkinsForestCurrentSetSetting] = new[] { 1f, 1f, 1f };
                                }

                                GUILayout.FlexibleSpace();
                            }
                            GUILayout.EndHorizontal();
                        }
                        GUILayout.EndArea();
                    }
                    break;

                case 1:
                    {
                        GUILayout.BeginArea(left[0]);
                        {
                            GUILayout.Space(15f);

                            Label("City", LabelType.Header);
                            Label("Settings", LabelType.SubHeader);
                            Grid("Skins Appearance", ref LocationSkinsSetting.Value, skinsAppearanceType);
                            Grid("Location", ref locationSkinsSwitch, locationSkinsLocation);

                            Label("Presets", LabelType.SubHeader);
                            scrollLocationSkinsCityLeft = GUILayout.BeginScrollView(scrollLocationSkinsCityLeft, GUILayout.Width(0f));
                            {
                                GUILayout.BeginHorizontal(GUILayout.Width(leftElementWidth + rightElementWidth + 15f));
                                {
                                    GUILayout.FlexibleSpace();
                                    LocationSkinsCityCurrentSetSetting.Value = GUILayout.SelectionGrid(LocationSkinsCityCurrentSetSetting, LocationSkinsCityTitlesList.ToArray(), 1, GUILayout.Width(175f));
                                    GUILayout.FlexibleSpace();
                                }
                                GUILayout.EndHorizontal();
                                GUILayout.Space(1f);
                            }
                            GUILayout.EndScrollView();

                            GUILayout.BeginHorizontal();
                            {
                                if (GUILayout.Button("Add"))
                                {
                                    LocationSkinsCityTitlesList.Add("Set " + (LocationSkinsCityTitlesList.Count + 1));
                                    LocationSkinsCityCurrentSetSetting.Value = LocationSkinsCityTitlesList.Count - 1;
                                    LocationSkinsCityList.Add("````````````````".Split('`'));
                                    LocationSkinsCityAmbientList.Add(0);
                                    LocationSkinsCityAmbientSettingsList.Add(new float[] { CustomAmbientColorSetting[0][0], CustomAmbientColorSetting[0][1], CustomAmbientColorSetting[0][2] });
                                    LocationSkinsCityFogList.Add(0);
                                    LocationSkinsCityFogSettingsList.Add(new float[] { 0.066f, 0.066f, 0.066f, 0f, 1000f });
                                    LocationSkinsCityParticlesList.Add(0);
                                    LocationSkinsCityParticlesSettingsList.Add(new float[] { 1500f, 125f, 60f, 120f, 0.001f, 0f, 1f, 1f, 1f, 1f });
                                    LocationSkinsCityCountSetting.Value++;
                                    scrollLocationSkinsCityLeft.y = 9999f;
                                }

                                if (GUILayout.Button("Remove"))
                                {
                                    if (LocationSkinsCityCountSetting == 1)
                                    {
                                        LocationSkinsCityTitlesList[LocationSkinsCityCurrentSetSetting] = "Set 1";
                                        LocationSkinsCityList[LocationSkinsCityCurrentSetSetting] = "````````````````".Split('`');
                                        LocationSkinsCityAmbientList[LocationSkinsCityCurrentSetSetting] = 0;
                                        LocationSkinsCityAmbientSettingsList[LocationSkinsCityCurrentSetSetting] = new float[] { CustomAmbientColorSetting[0][0], CustomAmbientColorSetting[0][1], CustomAmbientColorSetting[0][2] };
                                        LocationSkinsCityFogList[LocationSkinsCityCurrentSetSetting] = 0;
                                        LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting] = new float[] { 0.066f, 0.066f, 0.066f, 0f, 1000f };
                                        LocationSkinsCityParticlesList[LocationSkinsCityCurrentSetSetting] = 0;
                                        LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting] = new float[] { 1500f, 125f, 60f, 120f, 0.001f, 0f, 1f, 1f, 1f, 1f };
                                        LocationSkinsCityLightList[LocationSkinsCityCurrentSetSetting] = 0;
                                        LocationSkinsCityLightSettingsList[LocationSkinsCityCurrentSetSetting] = new[] { 1f, 1f, 1f };
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
                            }
                            GUILayout.EndHorizontal();
                        }
                        GUILayout.EndArea();

                        GUILayout.BeginArea(right[0]);
                        {
                            GUILayout.Space(15f);

                            Label(LocationSkinsCityTitlesList[LocationSkinsCityCurrentSetSetting], LabelType.Header);
                            scrollLocationSkinsCityRight = GUILayout.BeginScrollView(scrollLocationSkinsCityRight, GUILayout.Width(0f));
                            {
                                GUILayout.BeginHorizontal();
                                {
                                    Label("Title");
                                    LocationSkinsCityTitlesList[LocationSkinsCityCurrentSetSetting] = GUILayout.TextField(LocationSkinsCityTitlesList[LocationSkinsCityCurrentSetSetting], GUILayout.Width(TextFieldWidth));
                                }
                                GUILayout.EndHorizontal();
                                for (var i = 0; i < locationSkinCityFields.Length; i++)
                                {
                                    TextField(locationSkinCityFields[i], ref LocationSkinsCityList[LocationSkinsCityCurrentSetSetting][i]);
                                }

                                GUILayout.BeginHorizontal();
                                {
                                    Label("Ambient");
                                    LocationSkinsCityAmbientList[LocationSkinsCityCurrentSetSetting] = GUILayout.SelectionGrid(LocationSkinsCityAmbientList[LocationSkinsCityCurrentSetSetting], SwitcherStr, 2, GUILayout.Width(ButtonWidth));
                                }
                                GUILayout.EndHorizontal();
                                if (LocationSkinsCityAmbientList[LocationSkinsCityCurrentSetSetting] == 1)
                                {
                                    Slider("R", ref LocationSkinsCityAmbientSettingsList[LocationSkinsCityCurrentSetSetting][0], 0f, 1f);
                                    Slider("G", ref LocationSkinsCityAmbientSettingsList[LocationSkinsCityCurrentSetSetting][1], 0f, 1f);
                                    Slider("B", ref LocationSkinsCityAmbientSettingsList[LocationSkinsCityCurrentSetSetting][2], 0f, 1f);
                                }

                                GUILayout.BeginHorizontal();
                                {
                                    Label("Fog");
                                    LocationSkinsCityFogList[LocationSkinsCityCurrentSetSetting] = GUILayout.SelectionGrid(LocationSkinsCityFogList[LocationSkinsCityCurrentSetSetting], SwitcherStr, 2, GUILayout.Width(ButtonWidth));
                                }
                                GUILayout.EndHorizontal();
                                if (LocationSkinsCityFogList[LocationSkinsCityCurrentSetSetting] == 1)
                                {
                                    Slider("R", ref LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][0], 0f, 1f);
                                    Slider("G", ref LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][1], 0f, 1f);
                                    Slider("B", ref LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][2], 0f, 1f);
                                    if (LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][3] > LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][4] && LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][3] != 0f)
                                        LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][3] = LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][4] - 0.1f;
                                    if (LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][3] < 0)
                                        LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][3] = 0;
                                    Slider("Start Distance", ref LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][3], 0f, 1000f, round: true);
                                    Slider("End Distance", ref LocationSkinsCityFogSettingsList[LocationSkinsCityCurrentSetSetting][4], 0f, 1000f, round: true);
                                }

                                GUILayout.BeginHorizontal();
                                {
                                    Label("Light");
                                    LocationSkinsCityLightList[LocationSkinsCityCurrentSetSetting] = GUILayout.SelectionGrid(LocationSkinsCityLightList[LocationSkinsCityCurrentSetSetting], SwitcherStr, 2, GUILayout.Width(ButtonWidth));
                                }
                                GUILayout.EndHorizontal();
                                if (LocationSkinsCityLightList[LocationSkinsCityCurrentSetSetting] == 1)
                                {
                                    Slider("R", ref LocationSkinsCityLightSettingsList[LocationSkinsCityCurrentSetSetting][0], 0f, 1f);
                                    Slider("G", ref LocationSkinsCityLightSettingsList[LocationSkinsCityCurrentSetSetting][1], 0f, 1f);
                                    Slider("B", ref LocationSkinsCityLightSettingsList[LocationSkinsCityCurrentSetSetting][2], 0f, 1f);
                                }

                                GUILayout.BeginHorizontal();
                                {
                                    Label("Particles");
                                    LocationSkinsCityParticlesList[LocationSkinsCityCurrentSetSetting] = GUILayout.SelectionGrid(LocationSkinsCityParticlesList[LocationSkinsCityCurrentSetSetting], SwitcherStr, 2, GUILayout.Width(ButtonWidth));
                                }
                                GUILayout.EndHorizontal();
                                if (LocationSkinsCityParticlesList[LocationSkinsCityCurrentSetSetting] == 1)
                                {
                                    Slider("Count", ref LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][0], 100f, 15000f, round: true);
                                    Slider("Height", ref LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][1], 0F, 1000f, round: true);
                                    if (LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][2] > LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][3] && LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][2] != 0f)
                                        LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][2] = LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][3] - 0.1f;
                                    if (LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][2] < 0)
                                        LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][2] = 0;
                                    Slider("Lifetime Minimum", ref LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][2], 0f, 600f, round: true);
                                    Slider("Lifetime Maximum", ref LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][3], 0f, 600f, round: true);
                                    Slider("Gravity", ref LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][4], 0f, 5f);
                                    Slider("R", ref LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][5], 0f, 1f);
                                    Slider("G", ref LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][6], 0f, 1f);
                                    Slider("B", ref LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][7], 0f, 1f);
                                    Slider("A", ref LocationSkinsCityParticlesSettingsList[LocationSkinsCityCurrentSetSetting][8], 0f, 1f);
                                }

                                GUILayout.Space(1f);
                            }
                            GUILayout.EndScrollView();

                            GUILayout.Space(10f);
                            GUILayout.BeginHorizontal();
                            {
                                GUILayout.FlexibleSpace();
                                if (GUILayout.Button("Copy"))
                                {
                                    LocationSkinsCityCopiedSet = LocationSkinsCityList[LocationSkinsCityCurrentSetSetting];
                                }

                                if (GUILayout.Button("Paste"))
                                {
                                    if (LocationSkinsCityCopiedSet != null)
                                        LocationSkinsCityList[LocationSkinsCityCurrentSetSetting] = LocationSkinsCityCopiedSet;
                                }

                                if (GUILayout.Button("Reset"))
                                {
                                    LocationSkinsCityTitlesList[LocationSkinsCityCurrentSetSetting] = "Set " + (LocationSkinsCityCurrentSetSetting + 1);
                                    LocationSkinsCityList[LocationSkinsCityCurrentSetSetting] = "````````````````".Split('`');
                                    LocationSkinsCityAmbientList[LocationSkinsCityCountSetting] = 0;
                                    LocationSkinsCityAmbientSettingsList[LocationSkinsCityCountSetting] = new float[] { CustomAmbientColorSetting[0][0], CustomAmbientColorSetting[0][1], CustomAmbientColorSetting[0][2] };
                                    LocationSkinsCityFogList[LocationSkinsCityCountSetting] = 0;
                                    LocationSkinsCityFogSettingsList[LocationSkinsCityCountSetting] = new float[] { 0.066f, 0.066f, 0.066f, 0f, 1000f };
                                    LocationSkinsCityParticlesList[LocationSkinsCityCountSetting] = 0;
                                    LocationSkinsCityParticlesSettingsList[LocationSkinsCityCountSetting] = new float[] { 1500f, 125f, 60f, 120f, 0.001f, 0f, 1f, 1f, 1f, 1f };
                                    LocationSkinsCityLightList[LocationSkinsCityCurrentSetSetting] = 0;
                                    LocationSkinsCityLightSettingsList[LocationSkinsCityCurrentSetSetting] = new[] { 1f, 1f, 1f };
                                }

                                GUILayout.FlexibleSpace();
                            }
                            GUILayout.EndHorizontal();
                        }
                        GUILayout.EndArea();
                    }
                    break;
            }
        }

        private static void CustomMap()
        {
            UnityEngine.GUI.Label(new Rect(leftPos + 40f + 150f, topPos + 25f + 51f, 120f, 22f), "Map Settings", "Label");
            UnityEngine.GUI.Label(new Rect(leftPos + 40f + 50f, topPos + 25f + 81f, 140f, 20f), "Titan Spawn Cap:", "Label");
            FengGameManagerMKII.settings[85] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 155f, topPos + 25f + 81f, 30f, 20f), (string)FengGameManagerMKII.settings[85]);
            var strArray16 = new[] { "1 Round", "Waves", "PVP", "Racing", "Custom" };
            RCSettings.gameType = UnityEngine.GUI.SelectionGrid(new Rect(leftPos + 40f + 190f, topPos + 25f + 80f, 140f, 60f), RCSettings.gameType, strArray16, 2, UnityEngine.GUI.skin.toggle);
            UnityEngine.GUI.Label(new Rect(leftPos + 40f + 150f, topPos + 25f + 155f, 150f, 20f), "Level Script:", "Label");
            FengGameManagerMKII.currentScript = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 50f, topPos + 25f + 180f, 275f, 220f), FengGameManagerMKII.currentScript);
            if (UnityEngine.GUI.Button(new Rect(leftPos + 40f + 100f, topPos + 25f + 410f, 50f, 25f), "Copy"))
            {
                var editor = new TextEditor { content = new GUIContent(FengGameManagerMKII.currentScript) };
                editor.SelectAll();
                editor.Copy();
            }
            else if (UnityEngine.GUI.Button(new Rect(leftPos + 40f + 225f, topPos + 25f + 410f, 50f, 25f), "Clear"))
            {
                FengGameManagerMKII.currentScript = string.Empty;
            }

            UnityEngine.GUI.Label(new Rect(leftPos + 40f + 455f, topPos + 25f + 51f, 180f, 20f), "Custom Textures", "Label");
            UnityEngine.GUI.Label(new Rect(leftPos + 40f + 375f, topPos + 25f + 81f, 180f, 20f), "Ground Skin:", "Label");
            FengGameManagerMKII.settings[162] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 375f, topPos + 25f + 103f, 275f, 20f), (string)FengGameManagerMKII.settings[162]);
            UnityEngine.GUI.Label(new Rect(leftPos + 40f + 375f, topPos + 25f + 125f, 150f, 20f), "Skybox Front:", "Label");
            FengGameManagerMKII.settings[175] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 375f, topPos + 25f + 147f, 275f, 20f), (string)FengGameManagerMKII.settings[175]);
            UnityEngine.GUI.Label(new Rect(leftPos + 40f + 375f, topPos + 25f + 169f, 150f, 20f), "Skybox Back:", "Label");
            FengGameManagerMKII.settings[176] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 375f, topPos + 25f + 191f, 275f, 20f), (string)FengGameManagerMKII.settings[176]);
            UnityEngine.GUI.Label(new Rect(leftPos + 40f + 375f, topPos + 25f + 213f, 150f, 20f), "Skybox Left:", "Label");
            FengGameManagerMKII.settings[177] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 375f, topPos + 25f + 235f, 275f, 20f), (string)FengGameManagerMKII.settings[177]);
            UnityEngine.GUI.Label(new Rect(leftPos + 40f + 375f, topPos + 25f + 257f, 150f, 20f), "Skybox Right:", "Label");
            FengGameManagerMKII.settings[178] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 375f, topPos + 25f + 279f, 275f, 20f), (string)FengGameManagerMKII.settings[178]);
            UnityEngine.GUI.Label(new Rect(leftPos + 40f + 375f, topPos + 25f + 301f, 150f, 20f), "Skybox Up:", "Label");
            FengGameManagerMKII.settings[179] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 375f, topPos + 25f + 323f, 275f, 20f), (string)FengGameManagerMKII.settings[179]);
            UnityEngine.GUI.Label(new Rect(leftPos + 40f + 375f, topPos + 25f + 345f, 150f, 20f), "Skybox Down:", "Label");
            FengGameManagerMKII.settings[180] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 375f, topPos + 25f + 367f, 275f, 20f), (string)FengGameManagerMKII.settings[180]);
        }

        private static void CustomLogic()
        {
            GUILayout.BeginArea(center[0]);
            {
                GUILayout.Space(15f);
                Label("Script", LabelType.Header);
                GUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    TextArea(string.Empty, ref FengGameManagerMKII.currentScriptLogic, fullAreaWidth * 0.85f, fullAreaHeight * 0.65f);
                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Copy"))
                    {
                        var editor = new TextEditor { content = new GUIContent(FengGameManagerMKII.currentScriptLogic) };
                        editor.SelectAll();
                        editor.Copy();
                    }

                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Clear"))
                    {
                        FengGameManagerMKII.currentScriptLogic = string.Empty;
                    }

                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndArea();
        }
    }
}