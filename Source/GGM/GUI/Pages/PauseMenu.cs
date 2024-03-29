﻿using GGM.Caching;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using ExitGames.Client.Photon;
using GGM.Storage;
using UnityEngine;
using static GGM.Config.Settings;
using static GGM.GUI.Elements;
using static GGM.GUI.Settings;

namespace GGM.GUI.Pages
{
    internal class PauseMenu : Page
    {
        private static PhotonPlayer ChosenPlayer;
        private static float[] ControlPanelProportion = { 0.35f, 0.4f, 0.25f };
        private static int InfoPanelPageSetting = 0;

        #region Switchers

        private static int ambientDayTimeSwitch;
        private static int locationSkinsSwitch;
        private static int pauseMenuSwitchInt;
        private static int rebindsSwitch;
        private static int serverSwitch;

        #endregion Switchers

        #region Strings

        private static readonly string[] ahssSpeedometerTypes = { "Both", "Single", "Double" };
        private static readonly string[] anisotropicFiltering = { "Off", "On", "Forced" };
        private static readonly string[] antiAliasing = { "Off", "2x", "4x", "8x" };
        private static readonly string[] bladeTrailsAppearance = { "Legacy", "Liquid" };
        private static readonly string[] blendWeights = { "One", "Two", "Four" };
        private static readonly string[] bombStats = { "Radius", "Range", "Speed", "Cooldown" };
        private static readonly string[] cameraTypes = { "Original", "TPS", "WOW", "OldTPS" };
        private static readonly string[] cannonRebinds = { "Rotate Up:", "Rotate Down:", "Rotate Left:", "Rotate Right:", "Fire:", "Mount:", "Slow Rotate:" };
        private static readonly string[] cannonTypes = { "Ground", "Wall" };
        private static readonly string[] dayTime = { "Day", "Dawn", "Night" };
        private static readonly string[] formatOptions = { "Bold", "Italic" };
        private static readonly string[] healthTypes = { "Fixed", "Static" };
        private static readonly string[] horseRebinds = { "Forward", "Backward", "Left", "Right", "Walk", "Jump", "Mount" };
        private static readonly string[] humanRebinds = { "Forward", "Backward", "Left", "Right", "Jump", "Dodge", "Left Hook", "Right Hook", "Both Hooks", "Lock", "Attack", "Special", "Salute", "Change Camera", "Restart/Suicide", "Menu", "Show/Hide Cursor", "Fullscreen", "Reload", "Flare Green", "Flare Red", "Flare Black", "Reel In", "Reel Out", "Dash", "Minimap Maximize", "Minimap Toggle", "Minimap Reset", "Chat", "Live Spectate" };
        private static readonly string[] humanSkinFields = { "Horse", "Hair", "Eyes", "Glass", "Face", "Skin", "Costume", "Hoodie", "Left 3DMG", "Right 3DMG", "Gas", "Logo & Cape", "Weapon Trail" };
        private static readonly string[] infoPanel = { "Info", "RPC", "Events", "Props" };
        private static readonly string[] locationSkinCityFields = { "Ground", "Wall", "Gate", "Houses #1", "Houses #2", "Houses #3", "Houses #4", "Houses #5", "Houses #6", "Houses #7", "Houses #8", "Skybox Front", "Skybox Back", "Skybox Left", "Skybox Right", "Skybox Up", "Skybox Down", };
        private static readonly string[] locationSkinForestFields = { "Ground", "Forest Trunk #1", "Forest Trunk #2", "Forest Trunk #3", "Forest Trunk #4", "Forest Trunk #5", "Forest Trunk #6", "Forest Trunk #7", "Forest Trunk #8", "Forest Leave #1", "Forest Leave #2", "Forest Leave #3", "Forest Leave #4", "Forest Leave #5", "Forest Leave #6", "Forest Leave #7", "Forest Leave #8", "Skybox Front", "Skybox Back", "Skybox Left", "Skybox Right", "Skybox Up", "Skybox Down" };
        private static readonly string[] locationSkinsLocation = { "Forest", "City" };
        private static readonly string[] pauseMenuPages = { "Game", "Server", "Video & Audio", "Rebinds", "Bombs", "Human Skins", "Titan Skins", "Location Skins", "Custom Map", "Custom Logic" };
        private static readonly string[] pvpTypes = { "Teams", "FFA" };
        private static readonly string[] rebindsPages = { "Human", "Titan", "Mount", "Misc" };
        private static readonly string[] serverPages = { "Titans", "Humans", "Misc", "Control Panel" };
        private static readonly string[] shadowCascades = { "0", "2", "4" };
        private static readonly string[] shadowProjection = { "Stable Fit", "Close Fit" };
        private static readonly string[] skinsAppearanceType = { "Off", "Local", "Global" };
        private static readonly string[] speedometerTypes = { "Off", "SPD", "DMG" };
        private static readonly string[] teamTypes = { "Off", "Size", "Skill" };
        private static readonly string[] textures = { "Low", "Medium", "High" };
        private static readonly string[] titanRebinds = { "Forward", "Backward", "Left", "Right", "Walk", "Jump", "Punch", "Slam", "Grab Front", "Grab Back", "Grab Nape", "Slap", "Bite", "Cover Nape" };
        private static readonly string[] customMapTypes= { "1 Round", "Waves", "PVP", "Racing", "Custom" };
        private static readonly string[] customMapSkinFields = { "Ground", "Skybox Front", "Skybox Back", "Skybox Left", "Skybox Right", "Skybox Up", "Skybox Down" };

        #endregion Strings

        #region Scrolls

        private static Vector2 scrollControlPanelCenter = Vector2.zero;
        private static Vector2 scrollControlPanelLeft = Vector2.zero;
        private static Vector2 scrollControlPanelRight = Vector2.zero;
        private static Vector2 scrollHumanSkinsRight = Vector2.zero;
        private static Vector2 scrollCustomMapSkins = Vector2.zero;
        private static Vector2 scrollGameLeft = Vector2.zero;
        private static Vector2 scrollGameRight = Vector2.zero;
        private static Vector2 scrollHumanSkinsLeft = Vector2.zero;
        private static Vector2 scrollLocationSkinsCityLeft = Vector2.zero;
        private static Vector2 scrollLocationSkinsCityRight = Vector2.zero;
        private static Vector2 scrollLocationSkinsForestLeft = Vector2.zero;
        private static Vector2 scrollLocationSkinsForestRight = Vector2.zero;
        private static Vector2 scrollServerMiscLeft = Vector2.zero;
        private static Vector2 scrollServerTitansLeft = Vector2.zero;
        private static Vector2 scrollVideoLeft = Vector2.zero;
        private static Vector2 scrollAudioLeft = Vector2.zero;

        #endregion Scrolls

        private static bool[] HotKeysToWait;

        private static int HotKeyToRebind = -1;

        private static void Bombs()
        {
            GUILayout.BeginArea(left[3]);
            {
                GUILayout.Space(5f);
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

                GUILayout.BeginHorizontal();
                {
                    if (Button("Apply", width: leftElementWidth + rightElementWidth))
                    {
                        HERO.Instance.bombRadius = BombSettings[0] * 4f + 20f;
                        HERO.Instance.bombTimeMax = (BombSettings[1] * 60f + 200f) / (BombSettings[2] * 60f + 200f);
                        HERO.Instance.bombSpeed = BombSettings[2] * 60f + 200f;
                        HERO.Instance.bombCD = BombSettings[3] * -0.4f + 5f;
                        var propertiesToSet = new Hashtable();
                        propertiesToSet.Add(PhotonPlayerProperty.RCBombR, BombColorSetting[0].Value);
                        propertiesToSet.Add(PhotonPlayerProperty.RCBombG, BombColorSetting[1].Value);
                        propertiesToSet.Add(PhotonPlayerProperty.RCBombB, BombColorSetting[2].Value);
                        propertiesToSet.Add(PhotonPlayerProperty.RCBombA, 1f);
                        propertiesToSet.Add(PhotonPlayerProperty.RCBombRadius, HERO.Instance.bombRadius);
                        propertiesToSet.Add(PhotonPlayerProperty.RCBombRange, BombSettings[1].Value);
                        propertiesToSet.Add(PhotonPlayerProperty.RCBombSpeed, BombSettings[2].Value);
                        propertiesToSet.Add(PhotonPlayerProperty.RCBombCooldown, BombSettings[3].Value);
                        PhotonNetwork.player.SetCustomProperties(propertiesToSet);
                    }

                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndArea();

            GUILayout.BeginArea(right[3]);
            {
                GUILayout.Space(5f);
                Label("Color", LabelType.Header);
                Slider("R", ref BombColorSetting[0].Value, 0f, 1f);
                Slider("G", ref BombColorSetting[1].Value, 0f, 1f);
                Slider("B", ref BombColorSetting[2].Value, 0f, 1f);
                var txt = new Texture2D(1, 1);
                txt.SetPixel(0, 0, new Color(BombColorSetting[0], BombColorSetting[1], BombColorSetting[2]));
                txt.Apply();
                UnityEngine.GUI.DrawTexture(new Rect(45f, 45f, 70f, 70f), txt, ScaleMode.StretchToFill);

                Grid("Random Color", ref RandomBombColorSetting.Value);
            }
            GUILayout.EndArea();

            GUILayout.BeginArea(center[3]);
            {
                GUILayout.Space(5f);
                Label("Color Presets", LabelType.Header, width: fullAreaWidth);
                var style = new GUIStyle();
                const float size = 13f;
                GUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    for (var i = 0; i < 35; i++)
                    {
                        style.normal.background = style.hover.background = style.active.background = ColorCache.Textures[ColorCache.White];
                        if (GUILayout.Button(string.Empty, style, GUILayout.Height(size), GUILayout.Width(size)))
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

        private static void CustomLogic()
        {
            GUILayout.BeginArea(center[0]);
            {
                GUILayout.Space(5f);
                Label("Script", LabelType.Header);
                GUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    UnityEngine.GUI.SetNextControlName("LogicScript");
                    TextArea(string.Empty, ref FengGameManagerMKII.currentScriptLogic, fullAreaWidth * 0.85f, fullAreaHeight * 0.65f);
                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();

                    if (Button("Clear", 100))
                    {
                        FengGameManagerMKII.currentScriptLogic = string.Empty;
                    }

                    GUILayout.FlexibleSpace();

                    if (Button("Copy", 100))
                    {
                        var editor = new TextEditor { content = new GUIContent(FengGameManagerMKII.currentScriptLogic) };
                        editor.SelectAll();
                        editor.Copy();
                    }

                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndArea();
        }

        private static void CustomMap()
        {
            GUILayout.BeginArea(left[0]);
            {
                GUILayout.Space(5f);

                Label("Map Settings", LabelType.Header);
                TextField("Titan Spawn Cap", ref TitansSpawnCapSetting.Value);
                Grid("Game Type", ref RCSettings.gameType, customMapTypes, false, 2);
                UnityEngine.GUI.SetNextControlName("LevelScript");
                Label("Script", LabelType.Header);
                CustomMapScriptsList[CustomMapSkinsCurrentSetSetting] = GUILayout.TextArea(CustomMapScriptsList[CustomMapSkinsCurrentSetSetting], GUILayout.Width(leftElementWidth + rightElementWidth), GUILayout.Height(220f));
                GUILayout.FlexibleSpace();
                GUILayout.BeginHorizontal();
                {
                    if (Button("Clear", (leftElementWidth + rightElementWidth) / 3f - 5f / 3f))
                    {
                        CustomMapScriptsList[CustomMapSkinsCurrentSetSetting] = string.Empty;
                    }
                    if (Button("Request", (leftElementWidth + rightElementWidth) / 3f - 5f / 3f))
                    {
                        FengGameManagerMKII.FGM.photonView.RPC("ChatPM", PhotonNetwork.masterClient, FengGameManagerMKII.nameField.ToHTML(), $"{FengGameManagerMKII.nameField.ToHTML()} requests to get your map script.\n/pm {PhotonNetwork.player.ID} Y/N");
                        PhotonNetwork.masterClient.WaitForMapScript = true;
                        InRoomChat.SystemMessageLocal("Map Script request sent to", PhotonNetwork.masterClient);
                    }
                    if (Button("Copy", (leftElementWidth + rightElementWidth) / 3f - 5f / 3f))
                    {
                        var editor = new TextEditor { content = new GUIContent(CustomMapScriptsList[CustomMapSkinsCurrentSetSetting]) };
                        editor.SelectAll();
                        editor.Copy();
                    }
                }
                FengGameManagerMKII.currentScript = CustomMapScriptsList[CustomMapSkinsCurrentSetSetting];
                GUILayout.EndHorizontal();
            }
            GUILayout.EndArea();
            GUILayout.BeginArea(right[0]);
            {
                GUILayout.Space(5f);

                Label("Custom Skins", LabelType.Header);
                Grid("Skins Appearance", ref CustomMapSkinsSetting.Value);
                GUILayout.BeginHorizontal();
                {
                    Label("Title");
                    CustomMapSkinsTitlesList[CustomMapSkinsCurrentSetSetting] = GUILayout.TextField(CustomMapSkinsTitlesList[CustomMapSkinsCurrentSetSetting], GUILayout.Width(TextFieldWidth), GUILayout.Height(TextFieldHeight));
                }
                GUILayout.EndHorizontal();
                GUILayout.Space(space);
                for (var i = 0; i < customMapSkinFields.Length; i++)
                {
                    TextField(customMapSkinFields[i], ref CustomMapSkinsList[CustomMapSkinsCurrentSetSetting][i]);
                }

                Label("Presets", LabelType.SubHeader);
                scrollCustomMapSkins = GUILayout.BeginScrollView(scrollCustomMapSkins, false, true);
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.FlexibleSpace();
                        Grid(string.Empty, ref CustomMapSkinsCurrentSetSetting.Value, CustomMapSkinsTitlesList.ToArray(), false, width: 175f);
                        GUILayout.FlexibleSpace();
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Space(5f);
                }
                GUILayout.EndScrollView();

                GUILayout.FlexibleSpace();

                GUILayout.BeginHorizontal();
                {
                    if (Button("Add", (leftElementWidth + rightElementWidth) / 2f))
                    {
                        CustomMapSkinsTitlesList.Add("Preset " + (CustomMapSkinsTitlesList.Count + 1));
                        CustomMapSkinsCurrentSetSetting.Value = CustomMapSkinsTitlesList.Count - 1;
                        CustomMapSkinsList.Add("``````".Split('`'));
                        CustomMapScriptsList.Add(string.Empty);
                        CustomMapSkinsCountSetting.Value++;
                        scrollCustomMapSkins.y = 9999f;
                    }

                    if (Button("Remove", (leftElementWidth + rightElementWidth) / 2f))
                    {
                        if (CustomMapSkinsCountSetting == 1)
                        {
                            CustomMapSkinsTitlesList[CustomMapSkinsCurrentSetSetting] = "Preset 1";
                            CustomMapSkinsList[CustomMapSkinsCurrentSetSetting] = "``````".Split('`');
                            CustomMapScriptsList[CustomMapSkinsCurrentSetSetting] = string.Empty;
                        }
                        else
                        {
                            int setToRemove = CustomMapSkinsCurrentSetSetting;
                            if (setToRemove != 0) CustomMapSkinsCurrentSetSetting.Value--;
                            CustomMapSkinsList.RemoveAt(setToRemove);
                            CustomMapScriptsList.RemoveAt(setToRemove);
                            CustomMapSkinsTitlesList.RemoveAt(setToRemove);
                            CustomMapSkinsCountSetting.Value--;
                        }
                    }
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndArea();
        }

        private static void Game()
        {
            GUILayout.BeginArea(left[0]);
            {
                GUILayout.Space(5f);
                scrollGameLeft = GUILayout.BeginScrollView(scrollGameLeft);
                {
                    Label("Mouse", LabelType.Header);
                    Slider("Sensitivity", ref MouseSensitivitySetting.Value, 0.1f, 1f, multiplier: 100f, round: true);
                    Grid("Invert Y", ref MouseInvertYSetting.Value);
                    Label("Camera", LabelType.Header);
                    ButtonToggle(string.Empty, cameraTypes, CameraTypeSettings, width: leftElementWidth + rightElementWidth);
                    Slider("Distance", ref CameraDistanceSetting.Value, 0f, 1, multiplier: 100f, round: true);
                    Grid("Tilt", ref CameraTiltSetting.Value);
                    Grid("Static FOV", ref CameraStaticFOVSetting.Value);
                    if (CameraStaticFOVSetting) Slider("FOV", ref CameraFOVSetting.Value, 60f, 120f, round: true);
                    Label("Snapshots", LabelType.Header);
                    Grid("Snapshots", ref SnapshotsSetting.Value);
                    if (SnapshotsSetting)
                    {
                        Grid("Show In Game", ref SnapshotsShowInGameSetting.Value);
                        TextField("Minimum Damage", ref SnapshotsMinimumDamageSetting.Value);
                    }

                    Grid("Interpolation", ref InterporlationSetting.Value);

                    Label("Resources", LabelType.Header);
                    Grid("Infinite Blades", ref InfiniteBladesSetting.Value);
                    Grid("Infinite Bullets", ref InfiniteBulletsSetting.Value);
                    if (InfiniteBulletsSetting) Grid("No Reloading", ref InfiniteBulletsNoReloadingSetting.Value);
                    Grid("Infinite Gas", ref InfiniteGasSetting.Value);
                    GUILayout.Space(5f);
                }
                GUILayout.EndScrollView();
            }
            GUILayout.EndArea();

            GUILayout.BeginArea(right[0]);
            {
                GUILayout.Space(5f);
                scrollGameRight = GUILayout.BeginScrollView(scrollGameRight);
                {
                    Label("User Interface", LabelType.Header);
                    Grid("Legacy Labels", ref LegacyLabelsSetting.Value);
                    Grid("Hide Everything", ref UserInterfaceSetting.Value);
                    if (!UserInterfaceSetting)
                    {
                        Grid("Player List", ref PlayerListUISetting.Value);
                        Grid("Damage Feed", ref DamageFeedUISetting.Value);
                        Grid("Game Info", ref GameInfoUISetting.Value);
                        Grid("Chat", ref ChatUISetting.Value);
                        Grid("Crosshair Helper", ref CrosshairHelperUISetting.Value);
                        Grid("Sprites", ref SpritesUISetting.Value);
                    }

                    Label("Misc", LabelType.Header);
                    Grid("Body Lean", ref BodyLean.Value);
                    Grid("Chat Feed", ref ChatFeedSetting.Value);
                    if (ChatFeedSetting) Grid("Separate Window", ref ChatFeedSeparateSetting.Value);
                    Grid("Minimap", ref MinimapSetting.Value);
                    Grid("Speedometer", ref SpeedometerSetting.Value, speedometerTypes);
                    if (SpeedometerSetting == 2) Grid("AHSS Damage", ref SpeedometerAHSSSetting.Value, ahssSpeedometerTypes);
                    Grid("Cannon Type", ref CannonTypeSetting.Value, cannonTypes);
                    TextField("Movement Speed", ref CannonMovementSpeedSetting.Value);
                    TextField("Rotate Speed", ref CannonRotateSpeedSetting.Value);
                    TextField("Fire Cooldown", ref CannonCooldown.Value);
                    GUILayout.Space(5f);
                }
                GUILayout.EndScrollView();
            }
            GUILayout.EndArea();
        }

        private static void HumanSkins()
        {
            GUILayout.BeginArea(left[0]);
            {
                GUILayout.Space(5f);

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
                scrollHumanSkinsLeft = GUILayout.BeginScrollView(scrollHumanSkinsLeft, false, true);
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.FlexibleSpace();
                        Grid(string.Empty, ref HumanSkinsCurrentSetSetting.Value, HumanSkinsTitlesList.ToArray(), false, width: 175f);
                        GUILayout.FlexibleSpace();
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Space(5f);
                }
                GUILayout.EndScrollView();

                GUILayout.FlexibleSpace();

                GUILayout.BeginHorizontal();
                {
                    if (Button("Add", (leftElementWidth + rightElementWidth) / 3f - 5f / 3f))
                    {
                        HumanSkinsTitlesList.Add("Set " + (HumanSkinsTitlesList.Count + 1));
                        HumanSkinsCurrentSetSetting.Value = HumanSkinsTitlesList.Count - 1;
                        HumanSkinsList.Add("````````````".Split('`'));
                        HumanSkinsCountSetting.Value++;
                        scrollHumanSkinsLeft.y = 9999f;
                    }

                    if (Button("Remove", (leftElementWidth + rightElementWidth) / 3f - 5f / 3f))
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

                    if (Button("Apply", (leftElementWidth + rightElementWidth) / 3f - 5f / 3f))
                    {
                        HERO.Instance.loadskin();
                    }
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndArea();

            GUILayout.BeginArea(right[0]);
            {
                GUILayout.Space(5f);

                Label("Skins", LabelType.Header);
                Label(HumanSkinsTitlesList[HumanSkinsCurrentSetSetting], LabelType.SubHeader);
                scrollHumanSkinsRight = GUILayout.BeginScrollView(scrollHumanSkinsRight);
                GUILayout.BeginHorizontal();
                {
                    Label("Title");
                    HumanSkinsTitlesList[HumanSkinsCurrentSetSetting] = GUILayout.TextField(HumanSkinsTitlesList[HumanSkinsCurrentSetSetting], GUILayout.Width(TextFieldWidth), GUILayout.Height(TextFieldHeight));
                }
                GUILayout.EndHorizontal();
                GUILayout.Space(space);
                for (var i = 0; i < humanSkinFields.Length; i++)
                {
                    TextField(humanSkinFields[i], ref HumanSkinsList[HumanSkinsCurrentSetSetting][i]);
                }
                GUILayout.EndScrollView();
                GUILayout.FlexibleSpace();

                GUILayout.BeginHorizontal();
                {
                    if (Button("Copy", (leftElementWidth + rightElementWidth) / 3f - 5f / 3f))
                    {
                        CopiedHumanSkins = HumanSkinsList[HumanSkinsCurrentSetSetting];
                    }

                    if (Button("Paste", (leftElementWidth + rightElementWidth) / 3f - 5f / 3f))
                    {
                        if (CopiedHumanSkins != null)
                            HumanSkinsList[HumanSkinsCurrentSetSetting] = CopiedHumanSkins;
                    }

                    if (Button("Reset", (leftElementWidth + rightElementWidth) / 3f - 5f / 3f))
                    {
                        HumanSkinsTitlesList[HumanSkinsCurrentSetSetting] = "Set " + (HumanSkinsCurrentSetSetting + 1);
                        HumanSkinsList[HumanSkinsCurrentSetSetting] = "````````````".Split('`');
                    }
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndArea();
        }

        private static void LocationSkins()
        {
            switch (locationSkinsSwitch)
            {
                case 0:
                    {
                        GUILayout.BeginArea(left[0]);
                        {
                            GUILayout.Space(5f);

                            Label("Forest", LabelType.Header);
                            Label("Settings", LabelType.SubHeader);
                            Grid("Skins Appearance", ref LocationSkinsSetting.Value, skinsAppearanceType);
                            Grid("Location", ref locationSkinsSwitch, locationSkinsLocation);
                            Grid("Randomized Pairs", ref LocationSkinsRandomizedPairsSetting.Value);

                            Label("Presets", LabelType.SubHeader);
                            scrollLocationSkinsForestLeft = GUILayout.BeginScrollView(scrollLocationSkinsForestLeft, false, true);
                            {
                                GUILayout.BeginHorizontal();
                                {
                                    GUILayout.FlexibleSpace();
                                    Grid(string.Empty, ref LocationSkinsForestCurrentSetSetting.Value, LocationSkinsForestTitlesList.ToArray(), false, width: 175f);
                                    GUILayout.FlexibleSpace();
                                }
                                GUILayout.EndHorizontal();
                                GUILayout.Space(5f);
                            }
                            GUILayout.EndScrollView();

                            GUILayout.FlexibleSpace();

                            GUILayout.BeginHorizontal();
                            {
                                var path = Application.dataPath + "/Skins/Forest";

                                if (Button("Import", (leftElementWidth + rightElementWidth) / 3f - 5f / 3f))
                                {
                                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                                    foreach (var filePath in Directory.GetFiles(path))
                                    {
                                        var file = File.ReadAllText(filePath);
                                        var fileName = new FileInfo(filePath).Name.Replace(".txt", string.Empty);

                                        if (LocationSkinsForestTitlesList.Any(x => x.Equals(fileName)) || file.Split('`').Length != 9) continue;

                                        LocationSkinsForestList.Add(file.Split('`')[0].Split(','));
                                        LocationSkinsForestTitlesList.Add(fileName);
                                        LocationSkinsForestAmbientList.Add(int.Parse(file.Split('`')[1]));
                                        LocationSkinsForestAmbientSettingsList.Add( file.Split('`')[2].Split(',').ToFloatArray());
                                        LocationSkinsForestFogList.Add(int.Parse(file.Split('`')[3]));
                                        LocationSkinsForestFogSettingsList.Add(file.Split('`')[4].Split(',').ToFloatArray());
                                        LocationSkinsForestLightList.Add(int.Parse(file.Split('`')[5]));
                                        LocationSkinsForestLightSettingsList.Add(file.Split('`')[6].Split(',').ToFloatArray());
                                        LocationSkinsForestParticlesList.Add(int.Parse(file.Split('`')[7]));
                                        LocationSkinsForestParticlesSettingsList.Add(file.Split('`')[8].Split(',').ToFloatArray());
                                        LocationSkinsForestCountSetting.Value++;
                                        scrollLocationSkinsForestLeft.y = 9999f;
                                    }
                                    LocationSkinsForestCurrentSetSetting.Value = LocationSkinsForestTitlesList.Count - 1;
                                }
                                if (Button("Export", (leftElementWidth + rightElementWidth) / 3f - 5f / 3f))
                                {
                                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                                    for (var i = 0; i < LocationSkinsForestTitlesList.Count; i++)
                                    {
                                        File.WriteAllText($"{path}/{LocationSkinsForestTitlesList[i]}.txt", 
                                            $"{string.Join(",", LocationSkinsForestList[i])}`" + 
                                            $"{LocationSkinsForestAmbientList[i].ToString()}`" +
                                            $"{LocationSkinsForestAmbientSettingsList[i][0]},{LocationSkinsForestAmbientSettingsList[i][1]},{LocationSkinsForestAmbientSettingsList[i][2]}`" + 
                                            $"{LocationSkinsForestFogList[i].ToString()}`" + 
                                            $"{LocationSkinsForestFogSettingsList[i][0]},{LocationSkinsForestFogSettingsList[i][1]},{LocationSkinsForestFogSettingsList[i][2]},{LocationSkinsForestFogSettingsList[i][3]},{LocationSkinsForestFogSettingsList[i][4]}`" + 
                                            $"{LocationSkinsForestLightList[i].ToString()}`" + 
                                            $"{LocationSkinsForestLightSettingsList[i][0]},{LocationSkinsForestLightSettingsList[i][1]},{LocationSkinsForestLightSettingsList[i][2]}`" + 
                                            $"{LocationSkinsForestParticlesList[i]}`" + 
                                            $"{LocationSkinsForestParticlesSettingsList[i][0]},{LocationSkinsForestParticlesSettingsList[i][1]},{LocationSkinsForestParticlesSettingsList[i][2]},{LocationSkinsForestParticlesSettingsList[i][3]},{LocationSkinsForestParticlesSettingsList[i][4]},{LocationSkinsForestParticlesSettingsList[i][5]},{LocationSkinsForestParticlesSettingsList[i][6]},{LocationSkinsForestParticlesSettingsList[i][7]},{LocationSkinsForestParticlesSettingsList[i][8]}");
                                    }
                                }
                                if (Button("Request", (leftElementWidth + rightElementWidth) / 3f - 5f / 3f))
                                {
                                    FengGameManagerMKII.FGM.photonView.RPC("ChatPM", PhotonNetwork.masterClient, FengGameManagerMKII.nameField.ToHTML(), $"{FengGameManagerMKII.nameField.ToHTML()} requests to get your location skins.\n/pm {PhotonNetwork.player.ID} Y/N");
                                    PhotonNetwork.masterClient.WaitForLocationSkin = true;
                                    InRoomChat.SystemMessageLocal("Location Skins request sent to", PhotonNetwork.masterClient);
                                }
                            }
                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            {
                                if (Button("Copy", (leftElementWidth + rightElementWidth) / 3f - 5f / 3f))
                                {
                                    LocationSkinsForestCopiedSet = LocationSkinsForestList[LocationSkinsForestCurrentSetSetting];
                                }

                                if (Button("Paste", (leftElementWidth + rightElementWidth) / 3f - 5f / 3f))
                                {
                                    if (LocationSkinsForestCopiedSet != null)
                                        LocationSkinsForestList[LocationSkinsForestCurrentSetSetting] = LocationSkinsForestCopiedSet;
                                }

                                if (Button("Reset", (leftElementWidth + rightElementWidth) / 3f - 5f / 3f))
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
                            }
                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            {
                                if (Button("Add", (leftElementWidth + rightElementWidth) / 3f - 5f / 3f))
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
                                    LocationSkinsForestLightList.Add(0);
                                    LocationSkinsForestLightSettingsList.Add(new []{1f, 1f, 1f});
                                    LocationSkinsForestCountSetting.Value++;
                                    scrollLocationSkinsForestLeft.y = 9999f;
                                }

                                if (Button("Remove", (leftElementWidth + rightElementWidth) / 3f - 5f / 3f))
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
                                        LocationSkinsForestLightList.RemoveAt(setToRemove);
                                        LocationSkinsForestLightSettingsList.RemoveAt(setToRemove);
                                        LocationSkinsForestCountSetting.Value--;
                                    }
                                }

                                if (Button("Apply", (leftElementWidth + rightElementWidth) / 3f - 5f / 3f))
                                {
                                    FengGameManagerMKII.FGM.loadskin();
                                }
                            }
                            GUILayout.EndHorizontal();
                        }
                        GUILayout.EndArea();

                        GUILayout.BeginArea(right[0]);
                        {
                            GUILayout.Space(5f);

                            Label(LocationSkinsForestTitlesList[LocationSkinsForestCurrentSetSetting], LabelType.Header);
                            scrollLocationSkinsForestRight = GUILayout.BeginScrollView(scrollLocationSkinsForestRight, false, true);
                            {
                                GUILayout.BeginHorizontal();
                                {
                                    Label("Title");
                                    LocationSkinsForestTitlesList[LocationSkinsForestCurrentSetSetting] = GUILayout.TextField(LocationSkinsForestTitlesList[LocationSkinsForestCurrentSetSetting], GUILayout.Width(TextFieldWidth), GUILayout.Height(TextFieldHeight));
                                }
                                GUILayout.EndHorizontal();
                                GUILayout.Space(space);
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
                                GUILayout.Space(space);
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
                                GUILayout.Space(space);
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
                                GUILayout.Space(space);
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
                                GUILayout.Space(space);
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

                                GUILayout.Space(5f);
                            }
                            GUILayout.EndScrollView();
                        }
                        GUILayout.EndArea();
                    }
                    break;

                case 1:
                    {
                        GUILayout.BeginArea(left[0]);
                        {
                            GUILayout.Space(5f);

                            Label("City", LabelType.Header);
                            Label("Settings", LabelType.SubHeader);
                            Grid("Skins Appearance", ref LocationSkinsSetting.Value, skinsAppearanceType);
                            Grid("Location", ref locationSkinsSwitch, locationSkinsLocation);

                            Label("Presets", LabelType.SubHeader);
                            scrollLocationSkinsCityLeft = GUILayout.BeginScrollView(scrollLocationSkinsCityLeft, false, true);
                            {
                                GUILayout.BeginHorizontal();
                                {
                                    GUILayout.FlexibleSpace();
                                    Grid(string.Empty, ref LocationSkinsCityCurrentSetSetting.Value, LocationSkinsCityTitlesList.ToArray(), false, width: 175f);
                                    GUILayout.FlexibleSpace();
                                }
                                GUILayout.EndHorizontal();
                                GUILayout.Space(5f);
                            }
                            GUILayout.EndScrollView();

                            GUILayout.BeginHorizontal();
                            {
                                var path = Application.dataPath + "/Skins/City";

                                if (Button("Import", (leftElementWidth + rightElementWidth) / 3f - 5f / 3f))
                                {
                                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                                    foreach (var filePath in Directory.GetFiles(path))
                                    {
                                        var file = File.ReadAllText(filePath);
                                        var fileName = new FileInfo(filePath).Name.Replace(".txt", string.Empty);

                                        if (LocationSkinsCityTitlesList.Any(x => x.Equals(fileName)) || file.Split('`').Length != 9) continue;


                                        LocationSkinsCityList.Add(file.Split('`')[0].Split(','));
                                        LocationSkinsCityTitlesList.Add(fileName);
                                        LocationSkinsCityAmbientList.Add(int.Parse(file.Split('`')[1]));
                                        LocationSkinsCityAmbientSettingsList.Add(file.Split('`')[2].Split(',').ToFloatArray());
                                        LocationSkinsCityFogList.Add(int.Parse(file.Split('`')[3]));
                                        LocationSkinsCityFogSettingsList.Add(file.Split('`')[4].Split(',').ToFloatArray());
                                        LocationSkinsCityLightList.Add(int.Parse(file.Split('`')[5]));
                                        LocationSkinsCityLightSettingsList.Add(file.Split('`')[6].Split(',').ToFloatArray());
                                        LocationSkinsCityParticlesList.Add(int.Parse(file.Split('`')[7]));
                                        LocationSkinsCityParticlesSettingsList.Add(file.Split('`')[8].Split(',').ToFloatArray());
                                        LocationSkinsCityCountSetting.Value++;
                                        scrollLocationSkinsCityLeft.y = 9999f;
                                    }
                                    LocationSkinsCityCurrentSetSetting.Value = LocationSkinsCityTitlesList.Count - 1;
                                }
                                if (Button("Export", (leftElementWidth + rightElementWidth) / 3f - 5f / 3f))
                                {
                                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                                    for (var i = 0; i < LocationSkinsCityTitlesList.Count; i++)
                                    {
                                        File.WriteAllText($"{path}/{LocationSkinsCityTitlesList[i]}.txt",
                                            $"{string.Join(",", LocationSkinsCityList[i])}`" +
                                            $"{LocationSkinsCityAmbientList[i].ToString()}`" +
                                            $"{LocationSkinsCityAmbientSettingsList[i][0]},{LocationSkinsCityAmbientSettingsList[i][1]},{LocationSkinsCityAmbientSettingsList[i][2]}`" +
                                            $"{LocationSkinsCityFogList[i].ToString()}`" +
                                            $"{LocationSkinsCityFogSettingsList[i][0]},{LocationSkinsCityFogSettingsList[i][1]},{LocationSkinsCityFogSettingsList[i][2]},{LocationSkinsCityFogSettingsList[i][3]},{LocationSkinsCityFogSettingsList[i][4]}`" +
                                            $"{LocationSkinsCityLightList[i].ToString()}`" +
                                            $"{LocationSkinsCityLightSettingsList[i][0]},{LocationSkinsCityLightSettingsList[i][1]},{LocationSkinsCityLightSettingsList[i][2]}`" +
                                            $"{LocationSkinsCityParticlesList[i]}`" +
                                            $"{LocationSkinsCityParticlesSettingsList[i][0]},{LocationSkinsCityParticlesSettingsList[i][1]},{LocationSkinsCityParticlesSettingsList[i][2]},{LocationSkinsCityParticlesSettingsList[i][3]},{LocationSkinsCityParticlesSettingsList[i][4]},{LocationSkinsCityParticlesSettingsList[i][5]},{LocationSkinsCityParticlesSettingsList[i][6]},{LocationSkinsCityParticlesSettingsList[i][7]},{LocationSkinsCityParticlesSettingsList[i][8]}");
                                    }
                                }
                                if (Button("Request", (leftElementWidth + rightElementWidth) / 3f - 5f / 3f))
                                {
                                    FengGameManagerMKII.FGM.photonView.RPC("ChatPM", PhotonNetwork.masterClient, FengGameManagerMKII.nameField.ToHTML(), $"{FengGameManagerMKII.nameField.ToHTML()} requests to get your location skins.\n/pm {PhotonNetwork.player.ID} Y/N");
                                    PhotonNetwork.masterClient.WaitForLocationSkin = true;
                                    InRoomChat.SystemMessageLocal("Location Skins request sent to", PhotonNetwork.masterClient);
                                }
                            }
                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            {
                                if (Button("Copy", (leftElementWidth + rightElementWidth) / 3f - 5f / 3f))
                                {
                                    LocationSkinsCityCopiedSet = LocationSkinsCityList[LocationSkinsCityCurrentSetSetting];
                                }

                                if (Button("Paste", (leftElementWidth + rightElementWidth) / 3f - 5f / 3f))
                                {
                                    if (LocationSkinsCityCopiedSet != null)
                                        LocationSkinsCityList[LocationSkinsCityCurrentSetSetting] = LocationSkinsCityCopiedSet;
                                }

                                if (Button("Reset", (leftElementWidth + rightElementWidth) / 3f - 5f / 3f))
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
                            }
                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            {
                                if (Button("Add", (leftElementWidth + rightElementWidth) / 3f - 5f / 3f))
                                {
                                    LocationSkinsCityTitlesList.Add("Set " + (LocationSkinsCityTitlesList.Count + 1));
                                    LocationSkinsCityList.Add("````````````````".Split('`'));
                                    LocationSkinsCityCurrentSetSetting.Value = LocationSkinsCityTitlesList.Count - 1;
                                    LocationSkinsCityAmbientList.Add(0);
                                    LocationSkinsCityAmbientSettingsList.Add(new float[] { CustomAmbientColorSetting[0][0], CustomAmbientColorSetting[0][1], CustomAmbientColorSetting[0][2] });
                                    LocationSkinsCityFogList.Add(0);
                                    LocationSkinsCityFogSettingsList.Add(new float[] { 0.066f, 0.066f, 0.066f, 0f, 1000f });
                                    LocationSkinsCityParticlesList.Add(0);
                                    LocationSkinsCityParticlesSettingsList.Add(new float[] { 1500f, 125f, 60f, 120f, 0.001f, 0f, 1f, 1f, 1f, 1f });
                                    LocationSkinsCityLightList.Add(0);
                                    LocationSkinsCityLightSettingsList.Add(new []{1f, 1f, 1f});
                                    LocationSkinsCityCountSetting.Value++;
                                    scrollLocationSkinsCityLeft.y = 9999f;
                                }
                                if (Button("Remove", (leftElementWidth + rightElementWidth) / 3f - 5f / 3f))
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
                                        LocationSkinsCityLightList.RemoveAt(setToRemove);
                                        LocationSkinsCityLightSettingsList.RemoveAt(setToRemove);
                                        LocationSkinsCityCountSetting.Value--;
                                    }
                                }

                                if (Button("Apply", (leftElementWidth + rightElementWidth) / 3f - 5f / 3f))
                                {
                                    FengGameManagerMKII.FGM.loadskin();
                                }
                            }
                            GUILayout.EndHorizontal();
                        }
                        GUILayout.EndArea();

                        GUILayout.BeginArea(right[0]);
                        {
                            GUILayout.Space(5f);

                            Label(LocationSkinsCityTitlesList[LocationSkinsCityCurrentSetSetting], LabelType.Header);
                            scrollLocationSkinsCityRight = GUILayout.BeginScrollView(scrollLocationSkinsCityRight, false, true);
                            {
                                GUILayout.BeginHorizontal();
                                {
                                    Label("Title");
                                    LocationSkinsCityTitlesList[LocationSkinsCityCurrentSetSetting] = GUILayout.TextField(LocationSkinsCityTitlesList[LocationSkinsCityCurrentSetSetting], GUILayout.Width(TextFieldWidth), GUILayout.Height(TextFieldHeight));
                                }
                                GUILayout.EndHorizontal();
                                GUILayout.Space(space);
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
                                GUILayout.Space(space);
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
                                GUILayout.Space(space);
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
                                GUILayout.Space(space);
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
                                GUILayout.Space(space);
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

                                GUILayout.Space(5f);
                            }
                            GUILayout.EndScrollView();
                        }
                        GUILayout.EndArea();
                    }
                    break;
            }
        }

        private static void Rebinds()
        {
            if (FengGameManagerMKII.inputManager == null) return;

            GUILayout.BeginArea(center[1]);
            {
                GUILayout.FlexibleSpace();
                GUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    Grid(string.Empty, ref rebindsSwitch, rebindsPages, width: rebindsPages.Length * 75f);
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
                                    Label(humanRebinds[i], width: halfAreaWidth * 0.6f);

                                    if (Button(FengGameManagerMKII.inputManager.getKeyRC(i), halfAreaWidth * 0.4f - 10f))
                                    {
                                        FengGameManagerMKII.settings[100] = i + 1;
                                        FengGameManagerMKII.inputManager.setNameRC(i, "waiting...");
                                    }
                                }
                                GUILayout.EndHorizontal();
                                GUILayout.Space(space);
                            }
                        }
                        GUILayout.EndArea();

                        GUILayout.BeginArea(right[2]);
                        {
                            for (var i = humanRebinds.Length / 2; i < humanRebinds.Length; i++)
                            {
                                GUILayout.BeginHorizontal();
                                {
                                    var width = halfAreaWidth * 0.6f;
                                    if (i == 22 || i == 23 || i == 24) width = halfAreaWidth * 0.4f;
                                    Label(humanRebinds[i], width: width);
                                    if (i < 22)
                                    {
                                        if (Button(FengGameManagerMKII.inputManager.getKeyRC(i), halfAreaWidth * 0.4f - 10f))
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
                                            if (GUILayout.Button(ReelingSettings[0] ? txt[1] : txt[0], style, GUILayout.Width(halfAreaWidth * 0.2f - 5f), GUILayout.Height(ButtonHeight)))
                                            {
                                                ReelingSettings[0].Value = !ReelingSettings[0];
                                            }
                                        }
                                        else if (i == 23)
                                        {
                                            style.normal = ReelingSettings[1] ? UnityEngine.GUI.skin.button.onNormal : UnityEngine.GUI.skin.button.normal;
                                            style.hover = ReelingSettings[1] ? UnityEngine.GUI.skin.button.onHover : UnityEngine.GUI.skin.button.hover;
                                            if (GUILayout.Button(ReelingSettings[1] ? txt[1] : txt[0], style, GUILayout.Width(halfAreaWidth * 0.2f - 5f), GUILayout.Height(ButtonHeight)))
                                            {
                                                ReelingSettings[1].Value = !ReelingSettings[1];
                                            }
                                        }
                                        else if (i == 24)
                                        {
                                            style.normal = DashSetting ? UnityEngine.GUI.skin.button.onNormal : UnityEngine.GUI.skin.button.normal;
                                            style.hover = DashSetting ? UnityEngine.GUI.skin.button.onHover : UnityEngine.GUI.skin.button.hover;
                                            if (GUILayout.Button(DashSetting ? txt[1] : txt[0], style, GUILayout.Width(halfAreaWidth * 0.2f - 5f), GUILayout.Height(ButtonHeight)))
                                            {
                                                DashSetting.Value = !DashSetting;
                                            }
                                        }

                                        if (Button((string)FengGameManagerMKII.settings[k], halfAreaWidth * 0.4f - 10f))
                                        {
                                            FengGameManagerMKII.settings[k] = "waiting...";
                                            FengGameManagerMKII.settings[100] = k;
                                        }
                                    }
                                }
                                GUILayout.EndHorizontal();
                                GUILayout.Space(space);
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
                                    Label(titanRebinds[i], width: halfAreaWidth * 0.6f);
                                    if (Button((string)FengGameManagerMKII.settings[k], halfAreaWidth * 0.4f - 10f))
                                    {
                                        FengGameManagerMKII.settings[k] = "waiting...";
                                        FengGameManagerMKII.settings[100] = k;
                                    }
                                }
                                GUILayout.EndHorizontal();
                                GUILayout.Space(space);
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
                                    Label(titanRebinds[i], width: halfAreaWidth * 0.6f);
                                    if (Button((string)FengGameManagerMKII.settings[k], halfAreaWidth * 0.4f - 10f))
                                    {
                                        FengGameManagerMKII.settings[k] = "waiting...";
                                        FengGameManagerMKII.settings[100] = k;
                                    }
                                }
                                GUILayout.EndHorizontal();
                                GUILayout.Space(space);
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
                                    Label(horseRebinds[i], width: halfAreaWidth * 0.6f);
                                    if (Button((string)FengGameManagerMKII.settings[k], halfAreaWidth * 0.4f - 10f))
                                    {
                                        FengGameManagerMKII.settings[k] = "waiting...";
                                        FengGameManagerMKII.settings[100] = k;
                                    }
                                }
                                GUILayout.EndHorizontal();
                                GUILayout.Space(space);
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
                                    Label(cannonRebinds[i], width: halfAreaWidth * 0.6f - 10f);
                                    if (Button((string)FengGameManagerMKII.settings[k], halfAreaWidth * 0.4f - 10f))
                                    {
                                        FengGameManagerMKII.settings[k] = "waiting...";
                                        FengGameManagerMKII.settings[100] = k;
                                    }
                                }
                                GUILayout.EndHorizontal();
                                GUILayout.Space(space);
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
                                    Label(HotKey.AllHotKeys[i].Name, width: halfAreaWidth * 0.6f);
                                    if (Button(HotKeysToWait[i] ? "waiting..." : HotKey.AllHotKeys[i].Key.ToString(), halfAreaWidth * 0.4f - 10f))
                                    {
                                        HotKeyToRebind = i;
                                        HotKeysToWait[i] = true;
                                    }
                                }
                                GUILayout.EndHorizontal();
                                GUILayout.Space(space);
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

        private static void Server()
        {
            GUILayout.BeginArea(center[1]);
            {
                GUILayout.FlexibleSpace();
                GUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    Grid(string.Empty, ref serverSwitch, serverPages, width: serverPages.Length * 110f);
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
                                if (CustomStarterTitansSetting) TextField("Amount", ref StarterAmountSetting.Value);
                                Grid("Custom Titans/Wave", ref CustomTitansPerWaveSetting.Value);
                                if (CustomTitansPerWaveSetting) TextField("Amount", ref TitansPerWaveSetting.Value);
                                Grid("Custom Spawn Rate", ref CustomSpawnRateSetting.Value);
                                if (CustomSpawnRateSetting)
                                {
                                    float[] freePoints = { 100f - (SpawnRateSettings[1] + SpawnRateSettings[2] + SpawnRateSettings[3] + SpawnRateSettings[4]), 100f - (SpawnRateSettings[0] + SpawnRateSettings[2] + SpawnRateSettings[3] + SpawnRateSettings[4]), 100f - (SpawnRateSettings[0] + SpawnRateSettings[1] + SpawnRateSettings[3] + SpawnRateSettings[4]), 100f - (SpawnRateSettings[0] + SpawnRateSettings[1] + SpawnRateSettings[2] + SpawnRateSettings[4]), 100f - (SpawnRateSettings[0] + SpawnRateSettings[1] + SpawnRateSettings[2] + SpawnRateSettings[3]) };
                                    string[] types = { "Normal", "Abnormal", "Jumper", "Crawler", "Punk" };
                                    for (var i = 0; i < SpawnRateSettings.Length; i++)
                                    {
                                        Slider(types[i], ref SpawnRateSettings[i].Value, 0f, freePoints[i], customValueText: true, valueText: Math.Round(SpawnRateSettings[i]) + "%");
                                    }

                                    Grid("Punk Waves", ref PunkWavesSetting.Value);
                                }

                                Grid("Custom Size", ref CustomSizeSetting.Value);
                                if (CustomSizeSetting)
                                {
                                    TextField("Minimum", ref SizeSettings[0].Value);
                                    TextField("Maximum", ref SizeSettings[1].Value);
                                }

                                Grid("Custom Waves", ref CustomWavesSetting.Value);
                                if (CustomWavesSetting) TextField("Waves", ref MaximumWavesSetting.Value);
                                Grid("Disable Rock-Throwing", ref DisableRockThrowingSetting.Value);
                                GUILayout.Space(5f);
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
                                Grid("Type", ref HealthSettings[0].Value, healthTypes);
                                TextField("Minimum", ref HealthSettings[1].Value);
                                TextField("Maximum", ref HealthSettings[2].Value);
                            }

                            Grid("Armor Mode", ref ArmorModeSetting.Value);
                            if (ArmorModeSetting) TextField("Damage", ref ArmorSetting.Value);
                            Grid("Explode Mode", ref ExplodeModeSetting.Value);
                            if (ExplodeModeSetting) TextField("Radius", ref ExplodeRadiusSetting.Value);
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
                            if (PVPModeSetting) Grid("Type", ref PVPTypeSetting.Value, pvpTypes);
                            Grid("Points Mode", ref PointsModeSetting.Value);
                            if (PointsModeSetting) TextField("Limit", ref PointsLimitSetting.Value);
                            Grid("Team Mode", ref TeamModeSetting.Value);
                            if (TeamModeSetting)
                            {
                                Grid("Sort", ref TeamSortSetting.Value, teamTypes);
                            }

                            Grid("Bombs Mode", ref BombsModeSetting.Value);
                            Grid("Infection Mode", ref InfectionModeSetting.Value);
                            if (InfectionModeSetting) TextField("Infected", ref InfectedTitansSetting.Value);
                            Grid("Friendly Mode", ref FriendlyModeSetting.Value);
                        }
                        GUILayout.EndArea();

                        GUILayout.BeginArea(right[2]);
                        {
                            Label("Other", LabelType.Header);
                            Grid("Auto Revive", ref AutoReviveSetting.Value);
                            if (AutoReviveSetting) TextField("Seconds", ref AutoReviveTimeSetting.Value);
                            Grid("Horses", ref HorsesSetting.Value);
                            Grid("Disable Minimaps", ref DisableMinimapsSetting.Value);
                            Grid("No AHSS Air-Reload", ref DisableAHSSAirReloadingSetting.Value);
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
                                Grid("Log To File", ref LogChatSetting.Value);
                                if (LogChatSetting && File.Exists(Logger.ChatLogPath))
                                {
                                    if (Button($"File Size {new FileInfo(Logger.ChatLogPath).Length.BytesToMegabytes():0.###} MB", "Clear"))
                                    {
                                        File.WriteAllText(Logger.ChatLogPath, string.Empty);
                                    }
                                }
                                Grid("Announce Arrivals", ref AnnounceArrivalsSetting.Value);
                                Grid("Legacy Chat", ref LegacyChatSetting.Value);
                                Grid("Background", ref ChatBackground.Value);
                                if (ChatBackground) Slider("Opacity", ref ChatOpacitySetting.Value, 0f, 1f);
                                Slider("Messages Cache", ref ChatMessagesCache.Value, 15, 512);
                                Slider("Chat Height", ref ChatHeightSetting.Value, 275f, Screen.height - 30f);
                                Slider("Chat Width", ref ChatWidthSetting.Value, 300f, Screen.width / 2f - 5f);
                                TextField("Size", ref ChatSizeSetting.Value);
                                TextField("Major Color", ref ChatMajorColorSetting.Value);
                                TextField("Minor Color", ref ChatMinorColorSetting.Value);
                                ButtonToggle("Major Format", formatOptions, ChatMajorFormatSettings);
                                ButtonToggle("Minor Format", formatOptions, ChatMinorFormatSettings);
                                Label("Welcome Message", LabelType.Header);
                                GUILayout.BeginHorizontal();
                                {
                                    GUILayout.FlexibleSpace();
                                    if (Button("Bold", 75f))
                                    {
                                        WelcomeMessageSetting.Value = $"<b>{WelcomeMessageSetting}</b>";
                                    }

                                    if (Button("Italic", 75f))
                                    {
                                        WelcomeMessageSetting.Value = $"<i>{WelcomeMessageSetting}</i>";
                                    }

                                    GUILayout.FlexibleSpace();
                                }
                                GUILayout.EndHorizontal();
                                GUILayout.BeginHorizontal();
                                {
                                    UnityEngine.GUI.SetNextControlName("WelcomeMessage");
                                    TextArea(string.Empty, ref WelcomeMessageSetting.Value, halfAreaWidth - 25f, 185f);
                                }
                                GUILayout.EndHorizontal();
                                GUILayout.Space(5f);
                            }
                            GUILayout.EndScrollView();
                        }
                        GUILayout.EndArea();

                        GUILayout.BeginArea(right[2]);
                        {
                            Label("Protection", LabelType.Header);
                            Grid("Anti Revive", ref AntiRevive.Value);
                            Grid("Anti Titan Eren", ref AntiTitanErenSetting.Value);
                            Grid("Anti Guests", ref AntiGuestsSetting.Value);
                            Grid("Anti Abusive Mods", ref AntiAbusiveModsSetting.Value);
                        }
                        GUILayout.EndArea();
                        break;
                    }

                case 3:
                    {
                        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE && !PhotonNetwork.offlineMode)
                        {
                            if (PhotonPlayer.AllProps == null) PhotonPlayer.AllProps = System.IO.File.ReadAllLines(Application.dataPath + "/props.txt");
                            if (ChosenPlayer == null) ChosenPlayer = PhotonNetwork.player;

                            GUILayout.BeginArea(new Rect(leftPos + 20f, topPos + 100f, fullAreaWidth * ControlPanelProportion[0] - 10f, fullAreaHeight - 40f));
                            {
                                Label("Players", LabelType.Header, width: fullAreaWidth * ControlPanelProportion[0] - 10f);
                                scrollControlPanelLeft = GUILayout.BeginScrollView(scrollControlPanelLeft);
                                {
                                    foreach (var player in PhotonNetwork.playerList)
                                    {
                                        if (Button("[" + player.ID + "] " + player.Name.hexColor(), fullAreaWidth * ControlPanelProportion[0] - 20f))
                                        {
                                            ChosenPlayer = player;
                                        }
                                    }
                                }
                                GUILayout.EndScrollView();
                            }
                            GUILayout.EndArea();

                            GUILayout.BeginArea(new Rect(leftPos + 20f + fullAreaWidth * ControlPanelProportion[0], topPos + 100f, fullAreaWidth * ControlPanelProportion[1] - 10f, fullAreaHeight - 40f));
                            {
                                Label(infoPanel[InfoPanelPageSetting], LabelType.Header, width: fullAreaWidth * ControlPanelProportion[0] - 20f);
                                scrollControlPanelCenter = GUILayout.BeginScrollView(scrollControlPanelCenter);
                                {
                                    switch (InfoPanelPageSetting)
                                    {
                                        case 0:
                                            {
                                                Label("ID: " + ChosenPlayer.ID, width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                                                Label("Name: " + ChosenPlayer.Name.hexColor(), width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                                                Label("Mod: " + ChosenPlayer.CheckMod(), width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                                                var split = ((string)ChosenPlayer.customProperties[PhotonPlayerProperty.guildName]).Split('\n');
                                                for (var i = 0; i < split.Length; i++)
                                                {
                                                    Label($"Guild {i + 1}: {split[i].StripHTML().hexColor()}", width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                                                }
                                                Label("Unusual Properties:", width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                                                foreach (var property in ChosenPlayer.CheckProps().Split('\n'))
                                                {
                                                    Label(property, width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                                                }
                                                Label("Character: " + RCextensions.returnStringFromObject(ChosenPlayer.customProperties[PhotonPlayerProperty.character]), width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                                                Label("SPD: " + RCextensions.returnIntFromObject(ChosenPlayer.customProperties[PhotonPlayerProperty.statSPD]), width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                                                Label("BLA: " + RCextensions.returnIntFromObject(ChosenPlayer.customProperties[PhotonPlayerProperty.statBLA]), width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                                                Label("GAS: " + RCextensions.returnIntFromObject(ChosenPlayer.customProperties[PhotonPlayerProperty.statGAS]), width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                                                Label("ACL: " + RCextensions.returnIntFromObject(ChosenPlayer.customProperties[PhotonPlayerProperty.statACL]), width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                                                Label("Bomb R Color: " + RCextensions.returnFloatFromObject(ChosenPlayer.customProperties[PhotonPlayerProperty.RCBombR]).ToString("0.###"), width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                                                Label("Bomb G Color: " + RCextensions.returnFloatFromObject(ChosenPlayer.customProperties[PhotonPlayerProperty.RCBombG]).ToString("0.###"), width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                                                Label("Bomb B Color: " + RCextensions.returnFloatFromObject(ChosenPlayer.customProperties[PhotonPlayerProperty.RCBombB]).ToString("0.###"), width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                                                Label("Bomb Radius: " + (RCextensions.returnFloatFromObject(ChosenPlayer.customProperties[PhotonPlayerProperty.RCBombRadius]) - 20f) / 4f, width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                                                if (ChosenPlayer.GucciGangMod)
                                                {
                                                    Label("Bomb Range: " + RCextensions.returnIntFromObject(ChosenPlayer.customProperties[PhotonPlayerProperty.RCBombRange]), width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                                                    Label("Bomb Speed: " + RCextensions.returnIntFromObject(ChosenPlayer.customProperties[PhotonPlayerProperty.RCBombSpeed]), width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                                                    Label("Bomb Cooldown: " + RCextensions.returnIntFromObject(ChosenPlayer.customProperties[PhotonPlayerProperty.RCBombCooldown]), width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                                                }
                                                break;
                                            }

                                        case 1:
                                            {
                                                Label("ID: " + ChosenPlayer.ID, width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                                                Label("Name: " + ChosenPlayer.Name.hexColor(), width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                                                foreach (var RPC in ChosenPlayer.RPCs.Split('\n'))
                                                {
                                                    Label(RPC, width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                                                }
                                                break;
                                            }

                                        case 2:
                                            {
                                                Label("ID: " + ChosenPlayer.ID, width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                                                Label("Name: " + ChosenPlayer.Name.hexColor(), width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                                                foreach (var events in ChosenPlayer.Events.Split('\n'))
                                                {
                                                    Label(events, width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                                                }
                                                break;
                                            }

                                        case 3:
                                            {
                                                Label("ID: " + ChosenPlayer.ID, width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                                                Label("Name: " + ChosenPlayer.Name.hexColor(), width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                                                foreach (var prop in ChosenPlayer.Props.Split('\n'))
                                                {
                                                    Label(prop, width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                                                }
                                                break;
                                            }
                                    }

                                    GUILayout.Space(5f);
                                }
                                GUILayout.EndScrollView();
                                Grid(string.Empty, ref InfoPanelPageSetting, infoPanel, width: fullAreaWidth * ControlPanelProportion[1] - 20f);
                            }
                            GUILayout.EndArea();

                            GUILayout.BeginArea(new Rect(leftPos + 20f + fullAreaWidth * ControlPanelProportion[0] + fullAreaWidth * ControlPanelProportion[1], topPos + 100f, fullAreaWidth * ControlPanelProportion[2] - 10f, fullAreaHeight - 40f));
                            {
                                Label("Actions", LabelType.Header, width: fullAreaWidth * ControlPanelProportion[2] - 20f);
                                Label("Players", LabelType.SubHeader, width: fullAreaWidth * ControlPanelProportion[2] - 20f);
                                if (Button("Kick", fullAreaWidth * ControlPanelProportion[2] - 20f))
                                {
                                    Commands.Kick(ChosenPlayer.ID.ToString());
                                }

                                if (Button("Ban", fullAreaWidth * ControlPanelProportion[2] - 20f))
                                {
                                    Commands.Ban(ChosenPlayer.ID.ToString());
                                }


                                if (Button("Ignore", fullAreaWidth * ControlPanelProportion[2] - 20f))
                                {
                                    Commands.Ignore(ChosenPlayer.ID.ToString());
                                }

                                if (Button(ChosenPlayer.isMuted ? "Unmute" : "Mute", fullAreaWidth * ControlPanelProportion[2] - 20f))
                                {
                                    if (ChosenPlayer.isMuted)
                                    {
                                        Commands.Unmute(ChosenPlayer);
                                    }
                                    else
                                    {
                                        Commands.Mute(ChosenPlayer);
                                    }
                                }
                                if (Button("Reset Stats", fullAreaWidth * ControlPanelProportion[2] - 20f))
                                {
                                    Commands.ResetKD(ChosenPlayer.ID.ToString(), false);
                                }

                                if (Button("Revive", fullAreaWidth * ControlPanelProportion[2] - 20f))
                                {
                                    Commands.Revive(ChosenPlayer.ID.ToString());
                                }

                                if (Button("Request Skin", fullAreaWidth * ControlPanelProportion[2] - 20f))
                                {
                                    if (!HERO.PlayersSkins.ContainsKey(ChosenPlayer.ID)) return;
                                    FengGameManagerMKII.FGM.photonView.RPC("ChatPM", ChosenPlayer, FengGameManagerMKII.nameField.ToHTML(), $"{FengGameManagerMKII.nameField.ToHTML()} requests to get your human skins.\n/pm {PhotonNetwork.player.ID} Y/N");
                                    ChosenPlayer.WaitForHumanSkin = true;
                                    InRoomChat.SystemMessageLocal("Human Skins request sent to", ChosenPlayer);
                                }
                                Label("Server", LabelType.SubHeader, width: fullAreaWidth * ControlPanelProportion[2] - 20f);
                                if (Button("Reset All Stats", fullAreaWidth * ControlPanelProportion[2] - 20f))
                                {
                                    Commands.ResetKD(global:true);
                                }
                                if (Button("Revive All", fullAreaWidth * ControlPanelProportion[2] - 20f))
                                {
                                    Commands.Revive(all: true);
                                }
                                if (Button(PhotonNetwork.room.visible ? "Hide" : "Show", fullAreaWidth * ControlPanelProportion[2] - 20f))
                                {
                                    Commands.RoomHide(PhotonNetwork.room.visible);
                                }
                                if (Button(PhotonNetwork.room.open ? "Close" : "Open", fullAreaWidth * ControlPanelProportion[2] - 20f))
                                {
                                    Commands.RoomClose(PhotonNetwork.room.open);
                                }
                            }
                            GUILayout.EndArea();
                        }
                        else
                        {
                            GUILayout.BeginArea(center[0]);
                            GUILayout.FlexibleSpace();
                            Label("Online Only".SetSize(64), LabelType.Header, width: fullAreaWidth);
                            GUILayout.FlexibleSpace();
                            GUILayout.EndArea();
                        }
                        break;
                    }
            }
        }

        private static void TitanSkins()
        {
            int num45;
            int num46;
            UnityEngine.GUI.Label(new Rect(leftPos + 40f + 270f, topPos + 20f + 52f, 120f, 30f), "Titan Skin Mode:", Styles.LabelStyle[1]);
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

            UnityEngine.GUI.Label(new Rect(leftPos + 40f + 270f, topPos + 20f + 77f, 120f, 30f), "Randomized Pairs:", Styles.LabelStyle[1]);
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

            UnityEngine.GUI.Label(new Rect(leftPos + 160f, topPos + 25f + 112f, 150f, 20f), "Titan Hair:", Styles.LabelStyle[1]);

            FengGameManagerMKII.settings[21] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 80f, topPos + 25f + 134f, 155f, 20f), (string)FengGameManagerMKII.settings[21]);
            FengGameManagerMKII.settings[22] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 80f, topPos + 25f + 156f, 155f, 20f), (string)FengGameManagerMKII.settings[22]);
            FengGameManagerMKII.settings[23] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 80f, topPos + 25f + 178f, 155f, 20f), (string)FengGameManagerMKII.settings[23]);
            FengGameManagerMKII.settings[24] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 80f, topPos + 25f + 200f, 155f, 20f), (string)FengGameManagerMKII.settings[24]);
            FengGameManagerMKII.settings[25] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 80f, topPos + 25f + 222f, 155f, 20f), (string)FengGameManagerMKII.settings[25]);

            if (UnityEngine.GUI.Button(new Rect(leftPos + 40f + 243f, topPos + 25f + 134f, 70f, 20f), FengGameManagerMKII.FGM.hairtype((int)FengGameManagerMKII.settings[16])))
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
            else if (UnityEngine.GUI.Button(new Rect(leftPos + 40f + 243f, topPos + 25f + 156f, 70f, 20f), FengGameManagerMKII.FGM.hairtype((int)FengGameManagerMKII.settings[17])))
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
            else if (UnityEngine.GUI.Button(new Rect(leftPos + 40f + 243f, topPos + 25f + 178f, 70f, 20f), FengGameManagerMKII.FGM.hairtype((int)FengGameManagerMKII.settings[18])))
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
            else if (UnityEngine.GUI.Button(new Rect(leftPos + 40f + 243f, topPos + 25f + 200f, 70f, 20f), FengGameManagerMKII.FGM.hairtype((int)FengGameManagerMKII.settings[19])))
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
            else if (UnityEngine.GUI.Button(new Rect(leftPos + 40f + 243f, topPos + 25f + 222f, 70f, 20f), FengGameManagerMKII.FGM.hairtype((int)FengGameManagerMKII.settings[20])))
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

            UnityEngine.GUI.Label(new Rect(leftPos + 160f, topPos + 25f + 252f, 150f, 20f), "Titan Eye:", Styles.LabelStyle[1]);
            FengGameManagerMKII.settings[26] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 80f, topPos + 25f + 274f, 230f, 20f), (string)FengGameManagerMKII.settings[26]);
            FengGameManagerMKII.settings[27] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 80f, topPos + 25f + 296f, 230f, 20f), (string)FengGameManagerMKII.settings[27]);
            FengGameManagerMKII.settings[28] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 80f, topPos + 25f + 318f, 230f, 20f), (string)FengGameManagerMKII.settings[28]);
            FengGameManagerMKII.settings[29] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 80f, topPos + 25f + 340f, 230f, 20f), (string)FengGameManagerMKII.settings[29]);
            FengGameManagerMKII.settings[30] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 80f, topPos + 25f + 362f, 230f, 20f), (string)FengGameManagerMKII.settings[30]);
            UnityEngine.GUI.Label(new Rect(leftPos + 470f, topPos + 25f + 112f, 150f, 20f), "Titan Body:", Styles.LabelStyle[1]);
            FengGameManagerMKII.settings[86] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 390f, topPos + 25f + 134f, 230f, 20f), (string)FengGameManagerMKII.settings[86]);
            FengGameManagerMKII.settings[87] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 390f, topPos + 25f + 156f, 230f, 20f), (string)FengGameManagerMKII.settings[87]);
            FengGameManagerMKII.settings[88] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 390f, topPos + 25f + 178f, 230f, 20f), (string)FengGameManagerMKII.settings[88]);
            FengGameManagerMKII.settings[89] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 390f, topPos + 25f + 200f, 230f, 20f), (string)FengGameManagerMKII.settings[89]);
            FengGameManagerMKII.settings[90] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 390f, topPos + 25f + 222f, 230f, 20f), (string)FengGameManagerMKII.settings[90]);
            UnityEngine.GUI.Label(new Rect(leftPos + 470f, topPos + 25f + 252f, 150f, 20f), "Eren:", Styles.LabelStyle[1]);
            FengGameManagerMKII.settings[65] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 390f, topPos + 25f + 274f, 230f, 20f), (string)FengGameManagerMKII.settings[65]);
            UnityEngine.GUI.Label(new Rect(leftPos + 470f, topPos + 25f + 296f, 150f, 20f), "Annie:", Styles.LabelStyle[1]);
            FengGameManagerMKII.settings[66] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 390f, topPos + 25f + 318f, 230f, 20f), (string)FengGameManagerMKII.settings[66]);
            UnityEngine.GUI.Label(new Rect(leftPos + 470f, topPos + 25f + 340f, 150f, 20f), "Colossal:", Styles.LabelStyle[1]);
            FengGameManagerMKII.settings[67] = UnityEngine.GUI.TextField(new Rect(leftPos + 40f + 390f, topPos + 25f + 362f, 230f, 20f), (string)FengGameManagerMKII.settings[67]);
        }

        private static void VideoAndAudio()
        {

            GUILayout.BeginArea(left[0]);
            {
                GUILayout.Space(5f);

                GUILayout.BeginVertical();
                {
                    Label("General", LabelType.Header, GUIHelpers.Alignment.CENTER);
                    Slider("Volume", ref GlobalVolumeSetting.Value, 0f, 1f, round: true, multiplier: 100f);
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
            GUILayout.Space(15f);
            GUILayout.BeginArea(right[0]);
            {
                GUILayout.Space(5f);

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

                        GUILayout.Space(5f);
                    }
                    GUILayout.EndScrollView();
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndArea();

        }

        private void OnDisable()
        {
            GameObjectCache.Find("InputManagerController").GetComponent<FengCustomInputs>().menuOn = false;
        }

        private void OnGUI()
        {
            Screen.showCursor = true;

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
                FengGameManagerMKII.FGM.DestroyAllExistingCloths();
                Destroy(GameObjectCache.Find("MultiplayerManager"));
                Application.LoadLevel("menu");
            }
        }
    }
}