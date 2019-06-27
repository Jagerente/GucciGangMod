using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using GGM.Caching;
using UnityEngine;
using static GGM.Config.Settings;

namespace GGM.GUI
{
    internal class Pages : Elements
    {
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
        private static readonly string[] charsStr = { "LEVI", "MIKASA" };

        public static int TopNavigationPanelInt;
        public static string[] TopNavigationPanelStr =
        {
        "Game Settings",//0
        "Server Settings",//1
        "Video & Audio",//2
        "Rebinds",//3
        "Bombs",//4
        "Human Skins",//5
        "Titan Skins",//6
        "Level Skins",//7
        "Custom Map",//8
        "Custom Logic"//9
        };
        private static int topLeftNavigationInt;
        private static readonly string[] topLeftNavigationStr =
        {
            "<size=18>User</size>",
            "<size=18>Servers</size>"
        };
        private static readonly string[] switcherStr =
        {
            "Off",
            "On"
        };
        private static readonly string[] speedometer =
        {
            "Off",
            "Speed",
            "Damage"
        };
        private static readonly string[] anisotropicFilteringStr =
        {
            "Off",
            "On",
            "Forced"
        };
        private static readonly string[] antiAliasingStr =
        {
            "Off",
            "2x",
            "4x",
            "8x"
        };
        private static readonly string[] blendWeightsStr =
        {
            "1",
            "2",
            "4"
        };
        private static readonly string[] shadowCascadesStr =
        {
            "0",
            "2",
            "4"
        };
        private static readonly string[] shadowProjectionStr =
        {
            "Close Fit",
            "Stable Fit"
        };
        private static readonly string[] texturesStr =
        {
            "Low",
            "Medium",
            "High"
        };
        private static readonly string[] levelSkinPageStr =
        {
            "Forest",
            "City"
        };
        private static readonly string[] cannonCooldownStr =
        {
            "3.5",
            "1",
            "0.1"
        };
        private static readonly string[] cannonRotateStr =
        {
            "25",
            "50",
            "75",
            "100"
        };
        private static readonly string[] cannonSpeedStr =
        {
            "50",
            "75",
            "100",
            "200",
            "500"
        };
        private static readonly string[] bloomQuality =
        {
            "Cheap",
            "High"
        };
        private static readonly string[] lensFlareMode =
        {
            "Ghosting",
            "Anamorphic",
            "Combined"
        };
        private static readonly string[] bloomHDRStr =
        {
            "Off",
            "On",
            "Auto"
        };

        private const bool gridBold = true;
        private const string size = "72";
        private const string color1 = "D6B1DE";
        private const string color2 = "FFFFFF";
        private static string singleButton = "";
        private static Rect single = GUIHelpers.AlignRect(375f, 100f, GUIHelpers.Alignment.BOTTOMCENTER, 0f, -300f);
        private static string multiplayerButton = "";
        private static Rect multiplayer = GUIHelpers.AlignRect(715f, 100f, GUIHelpers.Alignment.BOTTOMCENTER, 0f, -175f);
        private static string quitButton = "";
        private static Rect quit = GUIHelpers.AlignRect(245f, 100f, GUIHelpers.Alignment.BOTTOMCENTER, 0f, -50f);


        public static void LoadingScreen()
        {
            UnityEngine.GUI.backgroundColor = new Color(0f, 0f, 0f, 1f);
            UnityEngine.GUI.Box(new Rect(0, 0, Screen.width, Screen.height), string.Empty);
            UnityEngine.GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), FengGameManagerMKII.FGM.textureBackgroundBlack);
            UnityEngine.GUI.DrawTexture(GUIHelpers.AlignRect(192, 192, GUIHelpers.Alignment.CENTER), Styles.Logo);
            UnityEngine.GUI.Label(GUIHelpers.AlignRect(600, 150, GUIHelpers.Alignment.BOTTOMCENTER), "<size=64>GucciGangMod</size>\n" + "<size=32>Loading</size>", LabelStyle[1]);
        }

        public static void MainMenu()
        {
            #region Single
            if (UnityEngine.GUI.Button(single, singleButton, "label"))
            {
                Page = "Singlee";
                NGUITools.SetActive(GameObjectCache.Find("UIRefer").GetComponent<UIMainReferences>().panelSingleSet, true);
                NGUITools.SetActive(UIMainReferences.instance.panelMain.gameObject, false);
            }

            singleButton = single.Contains(GUIHelpers.mousePos) ? $"<color=#{color1}><size={size}><b><i>S I N G L E</i></b></size></color>" : $"<color=#{color2}><size={size}><b><i>S I N G L E</i></b></size></color>";
            #endregion

            #region Multiplayer
            if (UnityEngine.GUI.Button(multiplayer, multiplayerButton, "label"))
            {
                Page = "Multiplayer";
                NGUITools.SetActive(GameObjectCache.Find("UIRefer").GetComponent<UIMainReferences>().panelMultiStart, true);
                NGUITools.SetActive(UIMainReferences.instance.panelMain.gameObject, false);
            }

            multiplayerButton = multiplayer.Contains(GUIHelpers.mousePos) ? $"<color=#{color1}><size={size}><b><i>M U L T I P L A Y E R</i></b></size></color>" : $"<color=#{color2}><size={size}><b><i>M U L T I P L A Y E R</i></b></size></color>";
            #endregion

            #region Quit
            if (UnityEngine.GUI.Button(quit, quitButton, "label"))
            {
                Application.Quit();
            }

            quitButton = quit.Contains(GUIHelpers.mousePos) ? $"<color=#{color1}><size={size}><b><i>Q U I T</i></b></size></color>" : $"<color=#{color2}><size={size}><b><i>Q U I T</i></b></size></color>";
            #endregion

            #region Top Left Navigation Panel
            GUILayout.BeginArea(GUIHelpers.AlignRect(400, 400, GUIHelpers.Alignment.TOPLEFT, 10, 10));
            GUILayout.BeginHorizontal();

            topLeftNavigationInt = GUILayout.SelectionGrid(topLeftNavigationInt, topLeftNavigationStr, 2, GUILayout.Width(400), GUILayout.Height(50));
            GUILayout.EndHorizontal();

            switch (topLeftNavigationInt)
            {
                case 0:
                    #region Custom Name
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("<size=16>Name:</size>", GUILayout.Width(115));
                    FengGameManagerMKII.nameField = GUILayout.TextField(FengGameManagerMKII.nameField, GUILayout.Width(180));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("<size=16>Guild:</size>", GUILayout.Width(115));
                    LoginFengKAI.player.guildname = GUILayout.TextArea(LoginFengKAI.player.guildname, GUILayout.Width(180));
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
                        GUILayout.Label("<size=24><b><i>Connected to Public server.</i></b></size>", GUILayout.Width(400));
                    }
                    else if (UIMainReferences.ServerKey == FengGameManagerMKII.s[0])
                    {
                        GUILayout.Label("<size=24><b><i>Connected to RC Private server.</i></b></size>", GUILayout.Width(400));
                    }
                    else
                    {
                        if (FengGameManagerMKII.privateServerField != string.Empty)
                        {
                            GUILayout.Label("<size=24><b><i>Connected to " + FengGameManagerMKII.privateServerField + " server.</i></b></size>", GUILayout.Width(400));
                        }
                        else
                        {
                            GUILayout.Label("<size=24><b><i>Connected to Custom server.</i></b></size>", GUILayout.Width(400));
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
                    FengGameManagerMKII.privateServerField = GUILayout.TextField(FengGameManagerMKII.privateServerField, 50, GUILayout.Width(115));
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
            if (UnityEngine.GUI.Button(GUIHelpers.AlignRect(128f, 25f, GUIHelpers.Alignment.TOPRIGHT, -5f, 15f), "Level Editor"))//15,128,25
            {
                FengGameManagerMKII.settings[64] = 101;
                Application.LoadLevel(2);
                Page = "Level Editor";
            }
            else if (UnityEngine.GUI.Button(GUIHelpers.AlignRect(128f, 25f, GUIHelpers.Alignment.TOPRIGHT, -5f, 45f), "Custom Characters"))//45f, 128,25f
            {
                Application.LoadLevel("characterCreation");
                Page = "Custom Characters";
            }
            else if (UnityEngine.GUI.Button(GUIHelpers.AlignRect(128f, 25f, GUIHelpers.Alignment.TOPRIGHT, -5f, 75f), "Snapshot Reviewer"))//75
            {
                Application.LoadLevel("SnapShot");
                Page = "Snapshot Reviewer";
            }
            #endregion
        }

        public static void Single()
        {
            //UnityEngine.GUI.Box(new Rect(leftPos, topPos, 700f, 500f), string.Empty);
            //GUILayout.BeginArea(new Rect(leftPos, topPos, 310f, 400f));
            //GUILayout.BeginVertical();
            //Label("Map", LabelType.Header, GUIHelpers.Alignment.CENTER);
            //map = GUILayout.SelectionGrid(map, mapStr, 1);
            //FengGameManagerMKII.level = mapNameStr[map];

            //GUILayout.EndVertical();
            //GUILayout.EndArea();

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
            IN_GAME_MAIN_CAMERA.difficulty = GUILayout.SelectionGrid(IN_GAME_MAIN_CAMERA.difficulty, new[] { "Normal", "Hard", "Abnormal" }, 1);
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
                if (IN_GAME_MAIN_CAMERA.singleCharacter.StartsWith("SET") || IN_GAME_MAIN_CAMERA.singleCharacter.StartsWith("AHSS"))
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

        //public static void PauseMenu()
        //{

        //}

        //public static void CharacterCreation()
        //{

        //}

        //private static void Game()
        //{

        //}

        //private static void Server()
        //{

        //}

        //private static void VideoAudio()
        //{
        //    GUILayout.BeginArea(center[1]);
        //    GUILayout.SelectionGrid(VideoAudioSwitch, AudioVideoStr, 2);
        //    GUILayout.EndArea();

        //    if (VideoAudioSwitch == 0)
        //    {
        //        GUILayout.BeginArea(left[2]);
        //        GUILayout.BeginVertical();
        //        Label("General", LabelType.Header);
        //        Slider("Overall Quality", ref Config.Settings.OverallQuality, 0f, 5f, true, overallQualityStr);
        //        Grid("Textures", ref Config.Settings.Textures, texturesStr);
        //        TextField("FPS Lock", ref Config.Settings.FPSLock);
        //        Label("Advanced", LabelType.Header);
        //        Grid("Anisotropic Filtering", ref Config.Settings.AnisotropicFiltering, anisotropicFilteringStr);
        //        Grid("Anti-Aliasing", ref Config.Settings.AntiAliasing, antiAliasingStr);
        //        Grid("Blend Weights", ref Config.Settings.BlendWeights, blendWeightsStr);
        //        Grid("Mip Mapping", ref Config.Settings.MipMapping, Switcher);
        //        Slider("Draw Distance", ref Config.Settings.DrawDistance, 1000f, 15000, valueTextFormat: "0");
        //        Slider("Shadow Distance", ref Config.Settings.ShadowDistance, 0f, 15000, valu   eTextFormat: "0");
        //        Grid("Shadow Projection", ref Config.Settings.ShadowProjection, shadowProjectionStr);
        //        Grid("Shadow Cascades", ref Config.Settings.ShadowCascades, shadowCascadesStr);
        //        GUILayout.EndVertical();
        //        GUILayout.EndArea();

        //        GUILayout.BeginArea(right[2]);
        //        GUILayout.BeginVertical();
        //        Label("Visuals", LabelType.Header);
        //        Grid("Wind", ref Wind, Switcher);
        //        Grid("Blur", ref Blur, Switcher);
        //        Grid("Ambient", ref Ambient, Switcher);
        //        if (Ambient == 1)
        //        {
        //            Slider("Day Color R:", ref AmbientColor[0][0], 0f, 1f, 160f, 25f);
        //            Slider("Day Color G:", ref AmbientColor[0][1], 0f, 1f, 160f, 25f);
        //            Slider("Day Color B:", ref AmbientColor[0][2], 0f, 1f, 160f, 25f);

        //            Slider("Dawn Color R:", ref AmbientColor[1][0], 0f, 1f, 160f, 25f);
        //            Slider("Dawn Color G:", ref AmbientColor[1][1], 0f, 1f, 160f, 25f);
        //            Slider("Dawn Color B:", ref AmbientColor[1][2], 0f, 1f, 160f, 25f);

        //            Slider("Night Color R:", ref AmbientColor[2][0], 0f, 1f, 160f, 25f);
        //            Slider("Night Color G:", ref AmbientColor[2][1], 0f, 1f, 160f, 25f);
        //            Slider("Night Color B:", ref AmbientColor[2][2], 0f, 1f, 160f, 25f);
        //        }
        //        Grid("Fog", ref Fog, Switcher);
        //        if (Fog == 1)
        //        {
        //            Slider("Color R:", ref FogColor[0], 0f, 1f, 160f, 25f);
        //            Slider("Color G:", ref FogColor[1], 0f, 1f, 160f, 25f);
        //            Slider("Color B:", ref FogColor[2], 0f, 1f, 160f, 25f);
        //            Slider("Start Distance", ref FogStartDistance, 0f, ref DrawDistance, 160f, 25f, valueTextFormat: "0");
        //            Slider("End Distance", ref FogEndDistance, 0f, ref DrawDistance, 160f, 25f, valueTextFormat: "0");
        //        }
        //        Grid("Bloom Quality", ref BloomQuality, BloomQualityStr);
        //        Slider("Bloom Intensity", false, 0, ref BloomIntensity, 0f, 2f);
        //        Slider("Bloom Threshhold", false, 0, ref BloomThreshold, 0f, 1f);
        //        Slider("Inversed R:", false, 0, ref BloomThresholdColor[0], 0f, 1f, 160f, 25f);
        //        Slider("Inversed G:", false, 0, ref BloomThresholdColor[1], 0f, 1f, 160f, 25f);
        //        Slider("Inversed B:", false, 0, ref BloomThresholdColor[2], 0f, 1f, 160f, 25f);
        //        Slider("Bloom Blur Iterations", false, 0, ref BloomBlurIterations, 0f, 5f);
        //        Slider("Bloom Blur Spread", false, 0, ref BloomBlurSpread, 0f, 10f);
        //        Grid("Lens Flare Mode", ref LensFlareMode, lensFlareMode);
        //        Slider("Lens Flare Intensity", false, 0, ref LensFlareIntensity, 0f, 1f);
        //        Grid("Bloom HDR", ref BloomHDR, bloomHDRStr);
        //        GUILayout.EndVertical();
        //        GUILayout.EndArea();
        //    }
        //    else
        //    {

        //    }
        //}


    }
}