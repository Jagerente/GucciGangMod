using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GGP
{
    class Pages : MonoBehaviour
    {
        #region Variables
        static readonly float leftpos = ((Screen.width) / 2f) - 350f;
        static readonly float toppos = ((Screen.height) / 2f) - 250f;
        static readonly float w = 730f;
        static readonly float h = 550f;
        static readonly Rect full = new Rect(leftpos, toppos - 25f, h, w);
        static readonly Rect topcenter = new Rect(leftpos, toppos + 35f, 730f, 35f);
        static readonly Rect left_top = new Rect(leftpos + 20f, toppos + 75f, 355f, 500 - 95f);
        static readonly Rect right_top = new Rect(leftpos + 380f, toppos + 75f, 355f, 500 - 95f);
        static readonly Rect left = new Rect(leftpos + 20f, toppos + 35f, 355f, 500f - 55f);
        static readonly Rect right = new Rect(leftpos + 380f, toppos + 35f, 355f, 500f - 55f);

        static string size = "72";
        static string color1 = "D6B1DE";
        static string color2 = "FFFFFF";
        static string SingleButton = "";
        static Rect Single = GUIHelpers.AlignRect(375f, 100f, GUIHelpers.Alignment.BOTTOMCENTER, 0f, -300f);
        static string MultiplayerButton = "";
        static Rect Multiplayer = GUIHelpers.AlignRect(715f, 100f, GUIHelpers.Alignment.BOTTOMCENTER, 0f, -175f);
        static string QuitButton = "";
        static Rect Quit = GUIHelpers.AlignRect(245f, 100f, GUIHelpers.Alignment.BOTTOMCENTER, 0f, -50f);
        #endregion

        #region Scrolls
        public static Vector2 VideoScroll = Vector2.zero;
        public static Vector2 AudioScroll = Vector2.zero;
        public static Vector2 LevelSkinForestScrollLeft = Vector2.zero;
        public static Vector2 LevelSkinForestScrollRight = Vector2.zero;
        public static Vector2 LevelSkinCityScrollLeft = Vector2.zero;
        public static Vector2 LevelSkinCityScrollRight = Vector2.zero;
        public static Vector2 GameSettingsScrollLeft = Vector2.zero;
        public static Vector2 GameSettingsScrollRight = Vector2.zero;
        #endregion

        #region Grids
        public static int TopNavigationPanelInt = 0;
        public static string[] TopNavigationPanelStr = new[]
{
        BetterGUI.ButtonStyle("Game Settings"),//0
        BetterGUI.ButtonStyle("Server Settings"),//1
        BetterGUI.ButtonStyle("Video & Audio"),//2
        BetterGUI.ButtonStyle("Rebinds"),//3
        BetterGUI.ButtonStyle("Bombs"),//4
        BetterGUI.ButtonStyle("Human Skins"),//5
        BetterGUI.ButtonStyle("Titan Skins"),//6
        BetterGUI.ButtonStyle("Level Skins"),//7
        BetterGUI.ButtonStyle("Custom Map"),//8
        BetterGUI.ButtonStyle("Custom Logic")//9
        };
        private static int TopLeftNavigationInt = 1;
        private static string[] TopLeftNavigationStr = new string[] { BetterGUI.ButtonStyle("Account"), BetterGUI.ButtonStyle("Name"), BetterGUI.ButtonStyle("Servers") };
        static readonly string[] SwitcherStr = { BetterGUI.ButtonStyle("Off"), BetterGUI.ButtonStyle("On") };
        static readonly string[] Speedometer = new[] { BetterGUI.ButtonStyle("Off"), BetterGUI.ButtonStyle("Speed"), BetterGUI.ButtonStyle("Damage") };
        static readonly string[] AnisotropicFilteringStr = new[] { BetterGUI.ButtonStyle("Off"), BetterGUI.ButtonStyle("On"), BetterGUI.ButtonStyle("Forced") };
        static readonly string[] AntiAliasingStr = new[] { BetterGUI.ButtonStyle("Off"), BetterGUI.ButtonStyle("2x"), BetterGUI.ButtonStyle("4x"), BetterGUI.ButtonStyle("8x") };
        static readonly string[] BlendWeightsStr = new[] { BetterGUI.ButtonStyle("1"), BetterGUI.ButtonStyle("2"), BetterGUI.ButtonStyle("4") };
        static readonly string[] ShadowCascadesStr = new[] { BetterGUI.ButtonStyle("0"), BetterGUI.ButtonStyle("2"), BetterGUI.ButtonStyle("4") };
        static readonly string[] ShadowProjectionStr = new[] { BetterGUI.ButtonStyle("Close Fit"), BetterGUI.ButtonStyle("Stable Fit") };
        static readonly string[] TextureQualityStr = new[] { BetterGUI.ButtonStyle("Low"), BetterGUI.ButtonStyle("Medium"), BetterGUI.ButtonStyle("High") };
        static readonly string[] LevelSkinPageStr = new[] { BetterGUI.ButtonStyle("Forest"), BetterGUI.ButtonStyle("City") };
        static readonly string[] CannonCooldownStr = new[] { BetterGUI.ButtonStyle("3.5"), BetterGUI.ButtonStyle("1"), BetterGUI.ButtonStyle("0.1") };
        static readonly string[] CannonRotateStr = new[] { BetterGUI.ButtonStyle("25"), BetterGUI.ButtonStyle("50"), BetterGUI.ButtonStyle("75"), BetterGUI.ButtonStyle("100") };
        static readonly string[] CannonSpeedStr = new[] { BetterGUI.ButtonStyle("50"), BetterGUI.ButtonStyle("75"), BetterGUI.ButtonStyle("100"), BetterGUI.ButtonStyle("200"), BetterGUI.ButtonStyle("500") };
        #endregion

        #region Pages
        public void Main_Menu()
        {
            #region Single
            if (GUI.Button(Single, SingleButton, "label"))
            {
                NGUITools.SetActive(GameObject.Find("UIRefer").GetComponent<UIMainReferences>().panelSingleSet, true);
                NGUITools.SetActive(GameObject.Find("UIRefer").GetComponent<UIMainReferences>().panelMain, false);
            }
            if (Single.Contains(GUIHelpers.mousePos))
            {
                SingleButton = "<color=#" + color1 + "><size=" + size + "><b><i>S I N G L E</i></b></size></color>";
            }
            else
            {
                SingleButton = "<color=#" + color2 + "><size=" + size + "><b><i>S I N G L E</i></b></size></color>";
            }
            #endregion

            #region Multiplayer
            if (GUI.Button(Multiplayer, MultiplayerButton, "label"))
            {
                NGUITools.SetActive(GameObject.Find("UIRefer").GetComponent<UIMainReferences>().panelMultiStart, true);
                NGUITools.SetActive(GameObject.Find("UIRefer").GetComponent<UIMainReferences>().panelMain, false);
            }
            if (Multiplayer.Contains(GUIHelpers.mousePos))
            {
                MultiplayerButton = "<color=#" + color1 + "><size=" + size + "><b><i>M U L T I P L A Y E R</i></b></size></color>";
            }
            else
            {
                MultiplayerButton = "<color=#" + color2 + "><size=" + size + "><b><i>M U L T I P L A Y E R</i></b></size></color>";
            }
            #endregion

            #region Quit
            if (GUI.Button(Quit, QuitButton, "label"))
            {
                Application.Quit();
            }
            if (Quit.Contains(GUIHelpers.mousePos))
            {
                QuitButton = "<color=#" + color1 + "><size=" + size + "><b><i>Q U I T</i></b></size></color>";
            }
            else
            {
                QuitButton = "<color=#" + color2 + "><size=" + size + "><b><i>Q U I T</i></b></size></color>";
            }
            #endregion

            #region Top Left Navigation Panel
            GUILayout.BeginArea(GUIHelpers.AlignRect(460f, 400f, GUIHelpers.Alignment.TOPLEFT, 10, 10));
            GUILayout.BeginHorizontal();

            TopLeftNavigationInt = GUILayout.SelectionGrid(TopLeftNavigationInt, TopLeftNavigationStr, 3, GUILayout.Width(460), GUILayout.Height(50));;
            GUILayout.EndHorizontal();

            switch (TopLeftNavigationInt)
            {
                case 0:
                    #region Account
                    //Loggining
                    if (FengGameManagerMKII.loginstate != 3)
                        {
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("<size=16>Username:</size>", GUILayout.Width(115));
                            FengGameManagerMKII.usernameField = GUILayout.TextField(FengGameManagerMKII.usernameField, GUILayout.Width(105));
                            GUILayout.EndHorizontal();
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("<size=16>Password:</size>", GUILayout.Width(115));
                            FengGameManagerMKII.passwordField = GUILayout.PasswordField(FengGameManagerMKII.passwordField, "*"[0], GUILayout.Width(105));
                            GUILayout.EndHorizontal();
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("", GUILayout.Width(115));
                            Settings.Remember = GUILayout.Toggle(Settings.Remember, "<size=16> Remember</size>");
                            GUILayout.EndHorizontal();
                            if (GUILayout.Button("<size=18><b>Login</b></size>", GUILayout.Width(225)) && (FengGameManagerMKII.loginstate != 1))
                            {
                                PlayerPrefs.SetInt("Remember", Settings.Remember ? 1 : 0);
                                if (!Settings.Remember)
                                {
                                    base.StartCoroutine(FengGameManagerMKII.instance.loginFeng());
                                    FengGameManagerMKII.loginstate = 1;
                                }
                                else
                                {
                                    PlayerPrefs.SetString("Login", FengGameManagerMKII.usernameField);
                                    PlayerPrefs.SetString("Password", FengGameManagerMKII.passwordField);
                                    base.StartCoroutine(FengGameManagerMKII.instance.loginFeng());
                                    FengGameManagerMKII.loginstate = 1;
                                }
                            }
                            if (FengGameManagerMKII.loginstate == 1)
                            {
                                GUILayout.Label("<size=18><i>Loging in...</i></size>", GUILayout.Width(115));
                            }
                            else if (FengGameManagerMKII.loginstate == 2)
                            {
                                GUILayout.Label("<size=18><i>Login failed.</i></size>", GUILayout.Width(115));
                            }
                        }
                        //In Account
                        else
                        {
                            GUILayout.Label("<size=16>Name: " + LoginFengKAI.player.name + "</size>", GUILayout.Width(300));
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("<size=16>Guild:</size>", GUILayout.Width(45));
                            LoginFengKAI.player.guildname = GUILayout.TextField(LoginFengKAI.player.guildname, GUILayout.Width(175));
                            GUILayout.EndHorizontal();
                            GUILayout.BeginHorizontal();
                            if (GUILayout.Button("<b><size=18>Set Guild</size></b>", GUILayout.Width(125)))
                            {
                                base.StartCoroutine(FengGameManagerMKII.instance.setGuildFeng());
                            }
                            if (GUILayout.Button("<b><size=18>Logout</size></b>", GUILayout.Width(125)))
                            {
                                FengGameManagerMKII.loginstate = 0;
                            }
                            GUILayout.EndHorizontal();
                        }
                        break;
                #endregion
                case 1:
                    #region Custom Name
                    if (FengGameManagerMKII.loginstate == 3)
                        {
                            GUILayout.Label("<size=30><b><i>You are already logged in!</i></b></size>", GUILayout.Width(400));
                        }
                        //Custom Name
                        else
                        {
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("<size=16>Name:</size>", GUILayout.Width(115));
                            FengGameManagerMKII.nameField = GUILayout.TextField(FengGameManagerMKII.nameField, GUILayout.Width(180));
                            GUILayout.EndHorizontal();
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("<size=16>Guild:</size>", GUILayout.Width(115));
                            LoginFengKAI.player.guildname = GUILayout.TextArea(LoginFengKAI.player.guildname, GUILayout.Width(180));
                            GUILayout.EndHorizontal();
                            GUILayout.BeginHorizontal();
                            if (GUILayout.Button(BetterGUI.Bold("Save"), GUILayout.Width(125)))
                            {
                                PlayerPrefs.SetString("Name", FengGameManagerMKII.nameField);
                                PlayerPrefs.SetString("Guild", LoginFengKAI.player.guildname);
                            }
                            if (GUILayout.Button(BetterGUI.Bold("Load"), GUILayout.Width(125)))
                            {
                                FengGameManagerMKII.nameField = PlayerPrefs.GetString("Name", string.Empty);
                                LoginFengKAI.player.guildname = PlayerPrefs.GetString("Guild", string.Empty);
                            }
                            GUILayout.EndHorizontal();
                        }
                        break;
                #endregion
                case 2:
                    #region Server Type
                    if (UIMainReferences.version == UIMainReferences.fengVersion)
                        {
                            GUILayout.Label("<size=24><b><i>Connected to Public server.</i></b></size>", GUILayout.Width(400));
                        }
                        else if (UIMainReferences.version == FengGameManagerMKII.s[0])
                        {
                            GUILayout.Label("<size=24><b><i>Connected to RC Private server.</i></b></size>", GUILayout.Width(400));
                        }
                        else if (UIMainReferences.version == "DontUseThisVersionPlease173")
                        {
                            GUILayout.Label("<size=24><b><i>Connecting to Crypto server...</i></b></size>", GUILayout.Width(400));
                        }
                        else
                        {
                            if (FengGameManagerMKII.privateServerField != "")
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
                            UIMainReferences.version = UIMainReferences.fengVersion;
                        }
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("<size=16>RC Private</size>", GUILayout.Width(115));
                        if (GUILayout.Button("<size=18><b>Connect</b></size>", GUILayout.Width(280)))
                        {
                            UIMainReferences.version = FengGameManagerMKII.s[0];
                        }
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        FengGameManagerMKII.privateServerField = GUILayout.TextField(FengGameManagerMKII.privateServerField, 50, GUILayout.Width(115));
                        if (GUILayout.Button("<size=18><b>Connect</b></size>", GUILayout.Width(280)))
                        {
                            UIMainReferences.version = FengGameManagerMKII.privateServerField;
                        }
                        GUILayout.EndHorizontal();
                        break;
                    #endregion
            }
            GUILayout.EndArea();
            #endregion

            #region Top Right Navigation Panel
            if (GUI.Button(GUIHelpers.AlignRect(128f, 25f, GUIHelpers.Alignment.TOPRIGHT, -5f, 15f), "Level Editor"))//15,128,25
            {
                FengGameManagerMKII.settings[0x40] = 0x65;
                Application.LoadLevel(2);
            }
            else if (UnityEngine.GUI.Button(GUIHelpers.AlignRect(128f, 25f, GUIHelpers.Alignment.TOPRIGHT, -5f, 45f), "Custom Characters"))//45f, 128,25f
            {
                Application.LoadLevel("characterCreation");
            }
            else if (UnityEngine.GUI.Button(GUIHelpers.AlignRect(128f, 25f, GUIHelpers.Alignment.TOPRIGHT, -5f, 75f), "Snapshot Reviewer"))//75
            {
                Application.LoadLevel("SnapShot");
            }
            #endregion
        }
        public static void Game_Settings()
        {
            #region Left
            GUILayout.BeginArea(left);
            GUILayout.BeginVertical();
            BetterGUI.Header("General");
            GameSettingsScrollLeft = GUILayout.BeginScrollView(GameSettingsScrollLeft, false, false);
            BetterGUI.SubHeader("Mouse");
            BetterGUI.Slider("Mouse Sensivity", true, 100, ref Settings.MouseSensitivity, 0.1f, 1f);
            BetterGUI.Grid("Invert Mouse", ref Settings.InvertMouse, SwitcherStr);
            BetterGUI.SubHeader("Camera");
            BetterGUI.Slider("Camera Distance", true, 100, ref Settings.CameraDistance, 0f, 1f);
            BetterGUI.Grid("Camera Tilt", ref Settings.CameraTilt, SwitcherStr);
            BetterGUI.SubHeader("Snapshots");
            BetterGUI.Grid("Snapshots", ref Settings.Snapshots, SwitcherStr);
            BetterGUI.Grid("Show In Game", ref Settings.SnapshotsInGame, SwitcherStr);
            BetterGUI.TextField("Minimum Damage", ref Settings.SnapshotsMinDamage);
            BetterGUI.SubHeader("GUI");
            BetterGUI.Grid("UI", ref Settings.UI, SwitcherStr);
            BetterGUI.Grid("Chat", ref Settings.Chat, SwitcherStr);
            BetterGUI.Grid("RC Formatting", ref Settings.RCFormatting, SwitcherStr);
            BetterGUI.TextField("Major Color", ref Settings.ChatMajorColor);
            BetterGUI.TextField("Minor Color", ref Settings.ChatMinorColor);
            BetterGUI.TextField("Size", ref Settings.ChatSize);
            BetterGUI.Grid("Major Bold", ref Settings.ChatMajorBold, SwitcherStr);
            BetterGUI.Grid("Minor Bold", ref Settings.ChatMinorBold, SwitcherStr);
            BetterGUI.Grid("Major Italic", ref Settings.ChatMajorItalic, SwitcherStr);
            BetterGUI.Grid("Minor Italic", ref Settings.ChatMinorItalic, SwitcherStr);
            BetterGUI.Grid("FPS", ref Settings.FPS, SwitcherStr);
            BetterGUI.Grid("Game Feed", ref Settings.GameFeed, SwitcherStr);
            BetterGUI.Grid("Damage Feed", ref Settings.DamageFeed, SwitcherStr);
            BetterGUI.SubHeader("Other");
            BetterGUI.Grid("Speedometer", ref Settings.Speedometer, Speedometer);
            BetterGUI.Grid("Minimap", ref Settings.Minimap, SwitcherStr);
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.EndArea();
            #endregion

            #region Right
            GUILayout.BeginArea(right);
            GUILayout.BeginVertical();
            BetterGUI.Header("Character");
            GameSettingsScrollRight = GUILayout.BeginScrollView(GameSettingsScrollRight, false, false);
            BetterGUI.SubHeader("Physics");
            BetterGUI.Grid("Body Lean", ref Settings.BodyLean, SwitcherStr);
            BetterGUI.Grid("No Gravity [F4]", ref Settings.NoGravity, SwitcherStr);
            BetterGUI.Grid("No Clip [V]", ref Settings.NoClip, SwitcherStr);
            BetterGUI.SubHeader("Gas Burst");
            BetterGUI.Grid("Rebind + Double Tap", ref Settings.DoubleBurstRebind, SwitcherStr);
            BetterGUI.TextField("Force", ref Settings.DashForce);
            BetterGUI.TextField("Animation Delay", ref Settings.DashDelay);
            BetterGUI.SubHeader("Resources");
            BetterGUI.Grid("Infinite Blades", ref Settings.InfiniteBlades, SwitcherStr);
            BetterGUI.Grid("Infinite Bullets", ref Settings.InfiniteBullets, SwitcherStr);
            BetterGUI.Grid("Infinite Gas", ref Settings.InfiniteGas, SwitcherStr);
            BetterGUI.SubHeader("Cannon");
            BetterGUI.TextField("Speed", ref Settings.CannonSpeed);
            BetterGUI.TextField("Rotation", ref Settings.CannonRotate);
            BetterGUI.TextField("Cooldown", ref Settings.CannonCooldown);
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.EndArea();
            #endregion
        }
        public static void Server_Settings()
        {
            string[] strArray16;
            bool flag35;
            bool flag36;
            GUI.Label(new Rect(leftpos + 200f, toppos + 382f, 400f, 22f), "Master Client only. Changes will take effect upon restart.");
            if (GUI.Button(new Rect(leftpos + 267.5f, toppos + 50f, 60f, 25f), "Titans"))
            {
                FengGameManagerMKII.settings[230] = 0;
            }
            else if (GUI.Button(new Rect(leftpos + 332.5f, toppos + 50f, 40f, 25f), "PVP"))
            {
                FengGameManagerMKII.settings[230] = 1;
            }
            else if (GUI.Button(new Rect(leftpos + 377.5f, toppos + 50f, 50f, 25f), "Misc"))
            {
                FengGameManagerMKII.settings[230] = 2;
            }
            else if (GUI.Button(new Rect(leftpos + 320f, toppos + 415f, 60f, 30f), "Reset"))
            {
                FengGameManagerMKII.settings[192] = 0;
                FengGameManagerMKII.settings[193] = 0;
                FengGameManagerMKII.settings[194] = 0;
                FengGameManagerMKII.settings[195] = 0;
                FengGameManagerMKII.settings[196] = "30";
                FengGameManagerMKII.settings[197] = 0;
                FengGameManagerMKII.settings[198] = "100";
                FengGameManagerMKII.settings[199] = "200";
                FengGameManagerMKII.settings[200] = 0;
                FengGameManagerMKII.settings[201] = "1";
                FengGameManagerMKII.settings[202] = 0;
                FengGameManagerMKII.settings[203] = 0;
                FengGameManagerMKII.settings[204] = "1";
                FengGameManagerMKII.settings[205] = 0;
                FengGameManagerMKII.settings[206] = "1000";
                FengGameManagerMKII.settings[207] = 0;
                FengGameManagerMKII.settings[208] = "1.0";
                FengGameManagerMKII.settings[209] = "3.0";
                FengGameManagerMKII.settings[210] = 0;
                FengGameManagerMKII.settings[211] = "20.0";
                FengGameManagerMKII.settings[212] = "20.0";
                FengGameManagerMKII.settings[213] = "20.0";
                FengGameManagerMKII.settings[214] = "20.0";
                FengGameManagerMKII.settings[215] = "20.0";
                FengGameManagerMKII.settings[216] = 0;
                FengGameManagerMKII.settings[217] = 0;
                FengGameManagerMKII.settings[218] = "1";
                FengGameManagerMKII.settings[219] = 0;
                FengGameManagerMKII.settings[220] = 0;
                FengGameManagerMKII.settings[221] = 0;
                FengGameManagerMKII.settings[222] = "20";
                FengGameManagerMKII.settings[223] = 0;
                FengGameManagerMKII.settings[224] = "10";
                FengGameManagerMKII.settings[225] = string.Empty;
                FengGameManagerMKII.settings[226] = 0;
                FengGameManagerMKII.settings[227] = "50";
                FengGameManagerMKII.settings[228] = 0;
                FengGameManagerMKII.settings[229] = 0;
                FengGameManagerMKII.settings[235] = 0;
            }
            if (((int)FengGameManagerMKII.settings[230]) == 0)
            {
                GUI.Label(new Rect(leftpos + 100f, toppos + 90f, 160f, 22f), "Custom Titan Number:", "Label");
                GUI.Label(new Rect(leftpos + 100f, toppos + 112f, 200f, 22f), "Amount (Integer):", "Label");
                FengGameManagerMKII.settings[204] = GUI.TextField(new Rect(leftpos + 250f, toppos + 112f, 50f, 22f), (string)FengGameManagerMKII.settings[204]);
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[203]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(leftpos + 250f, toppos + 90f, 40f, 20f), flag35, "On");
                if (flag35 != flag36)
                {
                    if (flag36)
                    {
                        FengGameManagerMKII.settings[203] = 1;
                    }
                    else
                    {
                        FengGameManagerMKII.settings[203] = 0;
                    }
                }
                GUI.Label(new Rect(leftpos + 100f, toppos + 152f, 160f, 22f), "Custom Titan Spawns:", "Label");
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[210]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(leftpos + 250f, toppos + 152f, 40f, 20f), flag35, "On");
                if (flag35 != flag36)
                {
                    if (flag36)
                    {
                        FengGameManagerMKII.settings[210] = 1;
                    }
                    else
                    {
                        FengGameManagerMKII.settings[210] = 0;
                    }
                }
                GUI.Label(new Rect(leftpos + 100f, toppos + 174f, 150f, 22f), "Normal (Decimal):", "Label");
                GUI.Label(new Rect(leftpos + 100f, toppos + 196f, 150f, 22f), "Aberrant (Decimal):", "Label");
                GUI.Label(new Rect(leftpos + 100f, toppos + 218f, 150f, 22f), "Jumper (Decimal):", "Label");
                GUI.Label(new Rect(leftpos + 100f, toppos + 240f, 150f, 22f), "Crawler (Decimal):", "Label");
                GUI.Label(new Rect(leftpos + 100f, toppos + 262f, 150f, 22f), "Punk (Decimal):", "Label");
                FengGameManagerMKII.settings[211] = GUI.TextField(new Rect(leftpos + 250f, toppos + 174f, 50f, 22f), (string)FengGameManagerMKII.settings[211]);
                FengGameManagerMKII.settings[212] = GUI.TextField(new Rect(leftpos + 250f, toppos + 196f, 50f, 22f), (string)FengGameManagerMKII.settings[212]);
                FengGameManagerMKII.settings[213] = GUI.TextField(new Rect(leftpos + 250f, toppos + 218f, 50f, 22f), (string)FengGameManagerMKII.settings[213]);
                FengGameManagerMKII.settings[214] = GUI.TextField(new Rect(leftpos + 250f, toppos + 240f, 50f, 22f), (string)FengGameManagerMKII.settings[214]);
                FengGameManagerMKII.settings[215] = GUI.TextField(new Rect(leftpos + 250f, toppos + 262f, 50f, 22f), (string)FengGameManagerMKII.settings[215]);
                GUI.Label(new Rect(leftpos + 100f, toppos + 302f, 160f, 22f), "Titan Size Mode:", "Label");
                GUI.Label(new Rect(leftpos + 100f, toppos + 324f, 150f, 22f), "Minimum (Decimal):", "Label");
                GUI.Label(new Rect(leftpos + 100f, toppos + 346f, 150f, 22f), "Maximum (Decimal):", "Label");
                FengGameManagerMKII.settings[208] = GUI.TextField(new Rect(leftpos + 250f, toppos + 324f, 50f, 22f), (string)FengGameManagerMKII.settings[208]);
                FengGameManagerMKII.settings[209] = GUI.TextField(new Rect(leftpos + 250f, toppos + 346f, 50f, 22f), (string)FengGameManagerMKII.settings[209]);
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[207]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(leftpos + 250f, toppos + 302f, 40f, 20f), flag35, "On");
                if (flag36 != flag35)
                {
                    if (flag36)
                    {
                        FengGameManagerMKII.settings[207] = 1;
                    }
                    else
                    {
                        FengGameManagerMKII.settings[207] = 0;
                    }
                }
                GUI.Label(new Rect(leftpos + 400f, toppos + 90f, 160f, 22f), "Titan Health Mode:", "Label");
                GUI.Label(new Rect(leftpos + 400f, toppos + 161f, 150f, 22f), "Minimum (Integer):", "Label");
                GUI.Label(new Rect(leftpos + 400f, toppos + 183f, 150f, 22f), "Maximum (Integer):", "Label");
                FengGameManagerMKII.settings[198] = GUI.TextField(new Rect(leftpos + 550f, toppos + 161f, 50f, 22f), (string)FengGameManagerMKII.settings[198]);
                FengGameManagerMKII.settings[199] = GUI.TextField(new Rect(leftpos + 550f, toppos + 183f, 50f, 22f), (string)FengGameManagerMKII.settings[199]);
                strArray16 = new[] { "Off", "Fixed", "Scaled" };
                FengGameManagerMKII.settings[197] = GUI.SelectionGrid(new Rect(leftpos + 550f, toppos + 90f, 100f, 66f), (int)FengGameManagerMKII.settings[197], strArray16, 1, GUI.skin.toggle);
                GUI.Label(new Rect(leftpos + 400f, toppos + 223f, 160f, 22f), "Titan Damage Mode:", "Label");
                GUI.Label(new Rect(leftpos + 400f, toppos + 245f, 150f, 22f), "Damage (Integer):", "Label");
                FengGameManagerMKII.settings[206] = GUI.TextField(new Rect(leftpos + 550f, toppos + 245f, 50f, 22f), (string)FengGameManagerMKII.settings[206]);
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[205]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(leftpos + 550f, toppos + 223f, 40f, 20f), flag35, "On");
                if (flag35 != flag36)
                {
                    if (flag36)
                    {
                        FengGameManagerMKII.settings[205] = 1;
                    }
                    else
                    {
                        FengGameManagerMKII.settings[205] = 0;
                    }
                }
                GUI.Label(new Rect(leftpos + 400f, toppos + 285f, 160f, 22f), "Titan Explode Mode:", "Label");
                GUI.Label(new Rect(leftpos + 400f, toppos + 307f, 160f, 22f), "Radius (Integer):", "Label");
                FengGameManagerMKII.settings[196] = GUI.TextField(new Rect(leftpos + 550f, toppos + 307f, 50f, 22f), (string)FengGameManagerMKII.settings[196]);
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[195]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(leftpos + 550f, toppos + 285f, 40f, 20f), flag35, "On");
                if (flag35 != flag36)
                {
                    if (flag36)
                    {
                        FengGameManagerMKII.settings[195] = 1;
                    }
                    else
                    {
                        FengGameManagerMKII.settings[195] = 0;
                    }
                }
                GUI.Label(new Rect(leftpos + 400f, toppos + 347f, 160f, 22f), "Disable Rock Throwing:", "Label");
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[194]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(leftpos + 550f, toppos + 347f, 40f, 20f), flag35, "On");
                if (flag35 != flag36)
                {
                    if (flag36)
                    {
                        FengGameManagerMKII.settings[194] = 1;
                    }
                    else
                    {
                        FengGameManagerMKII.settings[194] = 0;
                    }
                }
            }
            else if (((int)FengGameManagerMKII.settings[230]) == 1)
            {
                GUI.Label(new Rect(leftpos + 100f, toppos + 90f, 160f, 22f), "Point Mode:", "Label");
                GUI.Label(new Rect(leftpos + 100f, toppos + 112f, 160f, 22f), "Max Points (Integer):", "Label");
                FengGameManagerMKII.settings[227] = GUI.TextField(new Rect(leftpos + 250f, toppos + 112f, 50f, 22f), (string)FengGameManagerMKII.settings[227]);
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[226]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(leftpos + 250f, toppos + 90f, 40f, 20f), flag35, "On");
                if (flag35 != flag36)
                {
                    if (flag36)
                    {
                        FengGameManagerMKII.settings[226] = 1;
                    }
                    else
                    {
                        FengGameManagerMKII.settings[226] = 0;
                    }
                }
                GUI.Label(new Rect(leftpos + 100f, toppos + 152f, 160f, 22f), "PVP Bomb Mode:", "Label");
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[192]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(leftpos + 250f, toppos + 152f, 40f, 20f), flag35, "On");
                if (flag35 != flag36)
                {
                    if (flag36)
                    {
                        FengGameManagerMKII.settings[192] = 1;
                    }
                    else
                    {
                        FengGameManagerMKII.settings[192] = 0;
                    }
                }
                GUI.Label(new Rect(leftpos + 100f, toppos + 182f, 100f, 66f), "Team Mode:", "Label");
                strArray16 = new[] { "Off", "No Sort", "Size-Lock", "Skill-Lock" };
                FengGameManagerMKII.settings[193] = GUI.SelectionGrid(new Rect(leftpos + 250f, toppos + 182f, 120f, 88f), (int)FengGameManagerMKII.settings[193], strArray16, 1, GUI.skin.toggle);
                GUI.Label(new Rect(leftpos + 100f, toppos + 278f, 160f, 22f), "Infection Mode:", "Label");
                GUI.Label(new Rect(leftpos + 100f, toppos + 300f, 160f, 22f), "Starting Titans (Integer):", "Label");
                FengGameManagerMKII.settings[201] = GUI.TextField(new Rect(leftpos + 250f, toppos + 300f, 50f, 22f), (string)FengGameManagerMKII.settings[201]);
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[200]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(leftpos + 250f, toppos + 278f, 40f, 20f), flag35, "On");
                if (flag35 != flag36)
                {
                    if (flag36)
                    {
                        FengGameManagerMKII.settings[200] = 1;
                    }
                    else
                    {
                        FengGameManagerMKII.settings[200] = 0;
                    }
                }
                GUI.Label(new Rect(leftpos + 100f, toppos + 330f, 160f, 22f), "Friendly Mode:", "Label");
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[219]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(leftpos + 250f, toppos + 330f, 40f, 20f), flag35, "On");
                if (flag35 != flag36)
                {
                    if (flag36)
                    {
                        FengGameManagerMKII.settings[219] = 1;
                    }
                    else
                    {
                        FengGameManagerMKII.settings[219] = 0;
                    }
                }
                GUI.Label(new Rect(leftpos + 400f, toppos + 90f, 160f, 22f), "Sword/AHSS PVP:", "Label");
                strArray16 = new[] { "Off", "Teams", "FFA" };
                FengGameManagerMKII.settings[220] = GUI.SelectionGrid(new Rect(leftpos + 550f, toppos + 90f, 100f, 66f), (int)FengGameManagerMKII.settings[220], strArray16, 1, GUI.skin.toggle);
                GUI.Label(new Rect(leftpos + 400f, toppos + 164f, 160f, 22f), "No AHSS Air-Reloading:", "Label");
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[228]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(leftpos + 550f, toppos + 164f, 40f, 20f), flag35, "On");
                if (flag35 != flag36)
                {
                    if (flag36)
                    {
                        FengGameManagerMKII.settings[228] = 1;
                    }
                    else
                    {
                        FengGameManagerMKII.settings[228] = 0;
                    }
                }
                GUI.Label(new Rect(leftpos + 400f, toppos + 194f, 160f, 22f), "Cannons kill humans:", "Label");
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[261]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(leftpos + 550f, toppos + 194f, 40f, 20f), flag35, "On");
                if (flag35 != flag36)
                {
                    if (flag36)
                    {
                        FengGameManagerMKII.settings[261] = 1;
                    }
                    else
                    {
                        FengGameManagerMKII.settings[261] = 0;
                    }
                }
            }
            else if (((int)FengGameManagerMKII.settings[230]) == 2)
            {
                GUI.Label(new Rect(leftpos + 100f, toppos + 90f, 160f, 22f), "Custom Titans/Wave:", "Label");
                GUI.Label(new Rect(leftpos + 100f, toppos + 112f, 160f, 22f), "Amount (Integer):", "Label");
                FengGameManagerMKII.settings[218] = GUI.TextField(new Rect(leftpos + 250f, toppos + 112f, 50f, 22f), (string)FengGameManagerMKII.settings[218]);
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[217]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(leftpos + 250f, toppos + 90f, 40f, 20f), flag35, "On");
                if (flag35 != flag36)
                {
                    if (flag36)
                    {
                        FengGameManagerMKII.settings[217] = 1;
                    }
                    else
                    {
                        FengGameManagerMKII.settings[217] = 0;
                    }
                }
                GUI.Label(new Rect(leftpos + 100f, toppos + 152f, 160f, 22f), "Maximum Waves:", "Label");
                GUI.Label(new Rect(leftpos + 100f, toppos + 174f, 160f, 22f), "Amount (Integer):", "Label");
                FengGameManagerMKII.settings[222] = GUI.TextField(new Rect(leftpos + 250f, toppos + 174f, 50f, 22f), (string)FengGameManagerMKII.settings[222]);
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[221]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(leftpos + 250f, toppos + 152f, 40f, 20f), flag35, "On");
                if (flag35 != flag36)
                {
                    if (flag36)
                    {
                        FengGameManagerMKII.settings[221] = 1;
                    }
                    else
                    {
                        FengGameManagerMKII.settings[221] = 0;
                    }
                }
                GUI.Label(new Rect(leftpos + 100f, toppos + 214f, 160f, 22f), "Punks every 5 waves:", "Label");
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[229]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(leftpos + 250f, toppos + 214f, 40f, 20f), flag35, "On");
                if (flag35 != flag36)
                {
                    if (flag36)
                    {
                        FengGameManagerMKII.settings[229] = 1;
                    }
                    else
                    {
                        FengGameManagerMKII.settings[229] = 0;
                    }
                }
                GUI.Label(new Rect(leftpos + 100f, toppos + 244f, 160f, 22f), "Global Minimap Disable:", "Label");
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[235]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(leftpos + 250f, toppos + 274f, 40f, 20f), flag35, "On");
                if (flag35 != flag36)
                {
                    if (flag36)
                    {
                        FengGameManagerMKII.settings[235] = 1;
                    }
                    else
                    {
                        FengGameManagerMKII.settings[235] = 0;
                    }
                }
                GUI.Label(new Rect(leftpos + 400f, toppos + 90f, 160f, 22f), "Endless Respawn:", "Label");
                GUI.Label(new Rect(leftpos + 400f, toppos + 112f, 160f, 22f), "Respawn Time (Integer):", "Label");
                FengGameManagerMKII.settings[224] = GUI.TextField(new Rect(leftpos + 550f, toppos + 112f, 50f, 22f), (string)FengGameManagerMKII.settings[224]);
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[223]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(leftpos + 550f, toppos + 90f, 40f, 20f), flag35, "On");
                if (flag35 != flag36)
                {
                    if (flag36)
                    {
                        FengGameManagerMKII.settings[223] = 1;
                    }
                    else
                    {
                        FengGameManagerMKII.settings[223] = 0;
                    }
                }
                GUI.Label(new Rect(leftpos + 400f, toppos + 152f, 160f, 22f), "Kick Eren Titan:", "Label");
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[202]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(leftpos + 550f, toppos + 152f, 40f, 20f), flag35, "On");
                if (flag35 != flag36)
                {
                    if (flag36)
                    {
                        FengGameManagerMKII.settings[202] = 1;
                    }
                    else
                    {
                        FengGameManagerMKII.settings[202] = 0;
                    }
                }
                GUI.Label(new Rect(leftpos + 400f, toppos + 182f, 160f, 22f), "Allow Horses:", "Label");
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[216]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(leftpos + 550f, toppos + 182f, 40f, 20f), flag35, "On");
                if (flag35 != flag36)
                {
                    if (flag36)
                    {
                        FengGameManagerMKII.settings[216] = 1;
                    }
                    else
                    {
                        FengGameManagerMKII.settings[216] = 0;
                    }
                }
                GUI.Label(new Rect(leftpos + 400f, toppos + 212f, 180f, 22f), "Message of the day:", "Label");
                FengGameManagerMKII.settings[225] = GUI.TextField(new Rect(leftpos + 400f, toppos + 234f, 200f, 22f), (string)FengGameManagerMKII.settings[225]);
            }
        }
        public static void Video_and_Audio()
        {
            #region Left
            GUILayout.BeginArea(left);
            GUILayout.BeginVertical();

            BetterGUI.Header("Video");
            VideoScroll = GUILayout.BeginScrollView(VideoScroll, false, false);
            BetterGUI.SubHeader("General");
            GUILayout.BeginHorizontal();
            BetterGUI.Label("Quality");
            Settings.OverallQuality = GUILayout.HorizontalSlider(Settings.OverallQuality, 0f, 5f, GUILayout.Width(125));
            BetterGUI.Label(QualitySettings.names[QualitySettings.GetQualityLevel()], 1, 60f);
            GUILayout.EndHorizontal();
            BetterGUI.Grid("Textures", ref Settings.TextureQuality, TextureQualityStr);
            BetterGUI.TextField("FPS Lock", ref Settings.FPSLock);
            BetterGUI.SubHeader("Advanced");
            var mipmap = 0;
            mipmap = Settings.MipMapping == 1 ? 1 : 0;
            BetterGUI.Grid("MIP Mapping", ref Settings.MipMapping, SwitcherStr);
            FengGameManagerMKII.settings[63] = Settings.MipMapping == 0 ? 1 : 0;
            if (Settings.MipMapping != mipmap)
            {
                FengGameManagerMKII.linkHash[0].Clear();
                FengGameManagerMKII.linkHash[1].Clear();
                FengGameManagerMKII.linkHash[2].Clear();
            }
            BetterGUI.Grid("Anisotropic Filtering", ref Settings.AnisotropicFiltreing, AnisotropicFilteringStr);
            BetterGUI.Grid("Anti-Aliasing", ref Settings.AntiAliasing, AntiAliasingStr);
            BetterGUI.Grid("Blend Weights", ref Settings.BlendWeights, BlendWeightsStr);
            BetterGUI.Slider("LOD Level", true, 0, ref Settings.MaximumLODLevel, 0f, 7f);
            BetterGUI.Slider("LOD Bias", false, 0, ref Settings.LODBias, 0f, 2f, "0.#");
            BetterGUI.Slider("Draw Distance", true, 100, ref Settings.DrawDistance, 15f, 50f);
            BetterGUI.Slider("Shadow Distance", true, 100, ref Settings.ShadowDistance, 0f, 50f);
            BetterGUI.Grid("Shadow Projection", ref Settings.ShadowProjection, ShadowProjectionStr);
            BetterGUI.Grid("Shadow Cascades", ref Settings.ShadowCascades, ShadowCascadesStr);
            //Ambient
            BetterGUI.Grid("Ambient", ref Settings.Ambient, SwitcherStr);
            if (Settings.Ambient == 1)
            {
                BetterGUI.Slider("Day Color R:", false, 0, ref Settings.AmbientDayColorR, 0f, 1f, "0.###", 160f, 25f);
                BetterGUI.Slider("Day Color G:", false, 0, ref Settings.AmbientDayColorG, 0f, 1f, "0.###", 160f, 25f);
                BetterGUI.Slider("Day Color B:", false, 0, ref Settings.AmbientDayColorB, 0f, 1f, "0.###", 160f, 25f);

                BetterGUI.Slider("Dawn Color R:", false, 0, ref Settings.AmbientDawnColorR, 0f, 1f, "0.###", 160f, 25f);
                BetterGUI.Slider("Dawn Color G:", false, 0, ref Settings.AmbientDawnColorG, 0f, 1f, "0.###", 160f, 25f);
                BetterGUI.Slider("Dawn Color B:", false, 0, ref Settings.AmbientDawnColorB, 0f, 1f, "0.###", 160f, 25f);

                BetterGUI.Slider("Night Color R:", false, 0, ref Settings.AmbientNightColorR, 0f, 1f, "0.###", 160f, 25f);
                BetterGUI.Slider("Night Color G:", false, 0, ref Settings.AmbientNightColorG, 0f, 1f, "0.###", 160f, 25f);
                BetterGUI.Slider("Night Color B:", false, 0, ref Settings.AmbientNightColorB, 0f, 1f, "0.###", 160f, 25f);
            }
            //Fog
            BetterGUI.Grid("Fog", ref Settings.Fog, SwitcherStr);
            if (Settings.Fog == 1)
            {
                BetterGUI.Slider("Color R:", false, 0, ref Settings.FogColorR, 0f, 1f, "0.###", 160f, 25f);
                BetterGUI.Slider("Color G:", false, 0, ref Settings.FogColorG, 0f, 1f, "0.###", 160f, 25f);
                BetterGUI.Slider("Color B:", false, 0, ref Settings.FogColorB, 0f, 1f, "0.###", 160f, 25f);
                BetterGUI.Slider("Start Distance", true, 0, ref Settings.FogStartDistance, 0f, 1000f, "0", 160f, 25f);
                BetterGUI.Slider("End Distance", true, 0, ref Settings.FogEndDistance, 0f, 1000f, "0", 160f, 25f);
            }
            BetterGUI.Grid("Wind", ref Settings.Wind, SwitcherStr);
            BetterGUI.Grid("Blur", ref Settings.Blur, SwitcherStr);
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.EndArea();
            #endregion

            #region Right
            GUILayout.BeginArea(right);
            GUILayout.BeginVertical();
            BetterGUI.Header("Audio");
            AudioScroll = GUILayout.BeginScrollView(AudioScroll, false, false);
            BetterGUI.SubHeader("General");
            BetterGUI.Slider("Global Volume", false, 100f, ref Settings.GlobalVolume, 0f, 1f);
            BetterGUI.SubHeader("Human");
            BetterGUI.Slider("AHSS Shoot Volume:", true, 100, ref Settings.AHSSVolume, 0f, 1f);
            BetterGUI.Slider("Air Slash Volume:", true, 100, ref Settings.AirSlashVolume, 0f, 1f);
            BetterGUI.Slider("Nape Slash Volume:", true, 100, ref Settings.NapeSlashVolume, 0f, 1f);
            BetterGUI.Slider("Body Slash Volume:", true, 100, ref Settings.BodySlashVolume, 0f, 1f);
            BetterGUI.Slider("Hook Volume:", true, 100, ref Settings.HookVolume, 0f, 1f);
            BetterGUI.SubHeader("Titan");
            BetterGUI.Slider("Titan Eren Roar Volume:", true, 100, ref Settings.TitanErenRoarVolume, 0f, 1f);
            BetterGUI.Slider("Swing Volume:", true, 100, ref Settings.SwingVolume, 0f, 1f);
            BetterGUI.Slider("Thunder Volume:", true, 100, ref Settings.ThunderVolume, 0f, 1f);
            BetterGUI.Slider("Head Explosion Volume:", true, 100, ref Settings.HeadExplosionVolume, 0f, 1f);
            BetterGUI.Slider("Head Punch Volume:", true, 100, ref Settings.HeadPunchVolume, 0f, 1f);
            BetterGUI.Slider("Boom Volume:", true, 100, ref Settings.BoomVolume, 0f, 1f);
            BetterGUI.Slider("Step Volume:", true, 100, ref Settings.StepVolume, 0f, 1f);
            GUILayout.BeginHorizontal();

            GUILayout.EndHorizontal();
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.EndArea();
            #endregion
        }
        public static void Rebinds()
        {
            List<string> list7;
            int i;
            int j;
            float k;
            int num23;
            Event current;
            string str4;
            bool flag4;

            if (GUI.Button(new Rect(leftpos + 233f, toppos + 51f, 55f, 25f), "Human"))
            {
                FengGameManagerMKII.settings[190] = 0;
            }
            else if (GUI.Button(new Rect(leftpos + 293f, toppos + 51f, 52f, 25f), "Titan"))
            {
                FengGameManagerMKII.settings[190] = 1;
            }
            else if (GUI.Button(new Rect(leftpos + 350f, toppos + 51f, 53f, 25f), "Horse"))
            {
                FengGameManagerMKII.settings[190] = 2;
            }
            else if (GUI.Button(new Rect(leftpos + 408f, toppos + 51f, 59f, 25f), "Cannon"))
            {
                FengGameManagerMKII.settings[190] = 3;
            }
            if (((int)FengGameManagerMKII.settings[190]) == 0)
            {
                list7 = new List<string> {
                                        "Forward:", "Backward:", "Left:", "Right:", "Jump:", "Dodge:", "Left Hook:", "Right Hook:", "Both Hooks:", "Lock:", "Attack:", "Special:", "Salute:", "Change Camera:", "Reset:", "Pause:",
                                        "Show/Hide Cursor:", "Fullscreen:", "Change Blade:", "Flare Green:", "Flare Red:", "Flare Black:", "Reel in:", "Reel out:", "Gas Burst:", "Minimap Max:", "Minimap Toggle:", "Minimap Reset:", "Open Chat:", "Live Spectate"
                                     };
                for (i = 0; i < list7.Count; i++)
                {
                    j = i;
                    k = 80f;
                    if (j > 14)
                    {
                        k = 390f;
                        j -= 15;
                    }
                    GUI.Label(new Rect(leftpos + k, (toppos + 86f) + (j * 25f), 145f, 22f), list7[i], "Label");
                }
                bool flag37 = false;
                if (((int)FengGameManagerMKII.settings[97]) == 1)
                {
                    flag37 = true;
                }
                bool flag38 = false;
                if (((int)FengGameManagerMKII.settings[116]) == 1)
                {
                    flag38 = true;
                }
                bool flag39 = false;
                if (((int)FengGameManagerMKII.settings[181]) == 1)
                {
                    flag39 = true;
                }
                bool flag40 = GUI.Toggle(new Rect(leftpos + 457f, toppos + 261f, 40f, 20f), flag37, "On");
                if (flag37 != flag40)
                {
                    if (flag40)
                    {
                        FengGameManagerMKII.settings[97] = 1;
                    }
                    else
                    {
                        FengGameManagerMKII.settings[97] = 0;
                    }
                }
                bool flag41 = GUI.Toggle(new Rect(leftpos + 457f, toppos + 286f, 40f, 20f), flag38, "On");
                if (flag38 != flag41)
                {
                    if (flag41)
                    {
                        FengGameManagerMKII.settings[116] = 1;
                    }
                    else
                    {
                        FengGameManagerMKII.settings[116] = 0;
                    }
                }
                bool flag42 = GUI.Toggle(new Rect(leftpos + 457f, toppos + 311f, 40f, 20f), flag39, "On");
                if (flag39 != flag42)
                {
                    if (flag42)
                    {
                        FengGameManagerMKII.settings[181] = 1;
                    }
                    else
                    {
                        FengGameManagerMKII.settings[181] = 0;
                    }
                }
                for (i = 0; i < 22; i++)
                {
                    j = i;
                    k = 190f;
                    if (j > 14)
                    {
                        k = 500f;
                        j -= 15;
                    }
                    if (GUI.Button(new Rect(leftpos + k, (toppos + 86f) + (j * 25f), 120f, 20f), FengGameManagerMKII.inputManager.getKeyRC(i), "box"))
                    {
                        FengGameManagerMKII.settings[100] = i + 1;
                        FengGameManagerMKII.inputManager.setNameRC(i, "waiting...");
                    }
                }
                if (GUI.Button(new Rect(leftpos + 500f, toppos + 261f, 120f, 20f), (string)FengGameManagerMKII.settings[98], "box"))
                {
                    FengGameManagerMKII.settings[98] = "waiting...";
                    FengGameManagerMKII.settings[100] = 98;
                }
                else if (GUI.Button(new Rect(leftpos + 500f, toppos + 286f, 120f, 20f), (string)FengGameManagerMKII.settings[99], "box"))
                {
                    FengGameManagerMKII.settings[99] = "waiting...";
                    FengGameManagerMKII.settings[100] = 99;
                }
                else if (GUI.Button(new Rect(leftpos + 500f, toppos + 311f, 120f, 20f), (string)FengGameManagerMKII.settings[182], "box"))
                {
                    FengGameManagerMKII.settings[182] = "waiting...";
                    FengGameManagerMKII.settings[100] = 182;
                }
                else if (GUI.Button(new Rect(leftpos + 500f, toppos + 336f, 120f, 20f), (string)FengGameManagerMKII.settings[232], "box"))
                {
                    FengGameManagerMKII.settings[232] = "waiting...";
                    FengGameManagerMKII.settings[100] = 232;
                }
                else if (GUI.Button(new Rect(leftpos + 500f, toppos + 361f, 120f, 20f), (string)FengGameManagerMKII.settings[233], "box"))
                {
                    FengGameManagerMKII.settings[233] = "waiting...";
                    FengGameManagerMKII.settings[100] = 233;
                }
                else if (GUI.Button(new Rect(leftpos + 500f, toppos + 386f, 120f, 20f), (string)FengGameManagerMKII.settings[234], "box"))
                {
                    FengGameManagerMKII.settings[234] = "waiting...";
                    FengGameManagerMKII.settings[100] = 234;
                }
                else if (GUI.Button(new Rect(leftpos + 500f, toppos + 411f, 120f, 20f), (string)FengGameManagerMKII.settings[236], "box"))
                {
                    FengGameManagerMKII.settings[236] = "waiting...";
                    FengGameManagerMKII.settings[100] = 236;
                }
                else if (GUI.Button(new Rect(leftpos + 500f, toppos + 436f, 120f, 20f), (string)FengGameManagerMKII.settings[262], "box"))
                {
                    FengGameManagerMKII.settings[262] = "waiting...";
                    FengGameManagerMKII.settings[100] = 262;
                }
                if (((int)FengGameManagerMKII.settings[100]) != 0)
                {
                    current = Event.current;
                    flag4 = false;
                    str4 = "waiting...";
                    if ((current.type == EventType.KeyDown) && (current.keyCode != KeyCode.None))
                    {
                        flag4 = true;
                        str4 = current.keyCode.ToString();
                    }
                    else if (Input.GetKey(KeyCode.LeftShift))
                    {
                        flag4 = true;
                        str4 = KeyCode.LeftShift.ToString();
                    }
                    else if (Input.GetKey(KeyCode.RightShift))
                    {
                        flag4 = true;
                        str4 = KeyCode.RightShift.ToString();
                    }
                    else if (Input.GetAxis("Mouse ScrollWheel") != 0f)
                    {
                        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                        {
                            flag4 = true;
                            str4 = "Scroll Up";
                        }
                        else
                        {
                            flag4 = true;
                            str4 = "Scroll Down";
                        }
                    }
                    else
                    {
                        i = 0;
                        while (i < 7)
                        {
                            if (Input.GetKeyDown((KeyCode)(323 + i)))
                            {
                                flag4 = true;
                                str4 = "Mouse" + Convert.ToString(i);
                            }
                            i++;
                        }
                    }
                    if (flag4)
                    {
                        if (((int)FengGameManagerMKII.settings[100]) == 98)
                        {
                            FengGameManagerMKII.settings[98] = str4;
                            FengGameManagerMKII.settings[100] = 0;
                            FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.reelin, str4);
                        }
                        else if (((int)FengGameManagerMKII.settings[100]) == 99)
                        {
                            FengGameManagerMKII.settings[99] = str4;
                            FengGameManagerMKII.settings[100] = 0;
                            FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.reelout, str4);
                        }
                        else if (((int)FengGameManagerMKII.settings[100]) == 182)
                        {
                            FengGameManagerMKII.settings[182] = str4;
                            FengGameManagerMKII.settings[100] = 0;
                            FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.dash, str4);
                        }
                        else if (((int)FengGameManagerMKII.settings[100]) == 232)
                        {
                            FengGameManagerMKII.settings[232] = str4;
                            FengGameManagerMKII.settings[100] = 0;
                            FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.mapMaximize, str4);
                        }
                        else if (((int)FengGameManagerMKII.settings[100]) == 233)
                        {
                            FengGameManagerMKII.settings[233] = str4;
                            FengGameManagerMKII.settings[100] = 0;
                            FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.mapToggle, str4);
                        }
                        else if (((int)FengGameManagerMKII.settings[100]) == 234)
                        {
                            FengGameManagerMKII.settings[234] = str4;
                            FengGameManagerMKII.settings[100] = 0;
                            FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.mapReset, str4);
                        }
                        else if (((int)FengGameManagerMKII.settings[100]) == 236)
                        {
                            FengGameManagerMKII.settings[236] = str4;
                            FengGameManagerMKII.settings[100] = 0;
                            FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.chat, str4);
                        }
                        else if (((int)FengGameManagerMKII.settings[100]) == 262)
                        {
                            FengGameManagerMKII.settings[262] = str4;
                            FengGameManagerMKII.settings[100] = 0;
                            FengGameManagerMKII.inputRC.setInputHuman(InputCodeRC.liveCam, str4);
                        }
                        else
                        {
                            for (i = 0; i < 22; i++)
                            {
                                num23 = i + 1;
                                if (((int)FengGameManagerMKII.settings[100]) == num23)
                                {
                                    GameObject.Find("InputManagerController").GetComponent<FengCustomInputs>().setKeyRC(i, str4);
                                    FengGameManagerMKII.settings[100] = 0;
                                }
                            }
                        }
                    }
                }
            }
            else if (((int)FengGameManagerMKII.settings[190]) == 1)
            {
                list7 = new List<string> { "Forward:", "Back:", "Left:", "Right:", "Walk:", "Jump:", "Punch:", "Slam:", "Grab (front):", "Grab (back):", "Grab (nape):", "Slap:", "Bite:", "Cover Nape:" };
                for (i = 0; i < list7.Count; i++)
                {
                    j = i;
                    k = 80f;
                    if (j > 6)
                    {
                        k = 390f;
                        j -= 7;
                    }
                    GUI.Label(new Rect(leftpos + k, (toppos + 86f) + (j * 25f), 145f, 22f), list7[i], "Label");
                }
                for (i = 0; i < 14; i++)
                {
                    num23 = 101 + i;
                    j = i;
                    k = 190f;
                    if (j > 6)
                    {
                        k = 500f;
                        j -= 7;
                    }
                    if (GUI.Button(new Rect(leftpos + k, (toppos + 86f) + (j * 25f), 120f, 20f), (string)FengGameManagerMKII.settings[num23], "box"))
                    {
                        FengGameManagerMKII.settings[num23] = "waiting...";
                        FengGameManagerMKII.settings[100] = num23;
                    }
                }
                if (((int)FengGameManagerMKII.settings[100]) != 0)
                {
                    current = Event.current;
                    flag4 = false;
                    str4 = "waiting...";
                    if ((current.type == EventType.KeyDown) && (current.keyCode != KeyCode.None))
                    {
                        flag4 = true;
                        str4 = current.keyCode.ToString();
                    }
                    else if (Input.GetKey(KeyCode.LeftShift))
                    {
                        flag4 = true;
                        str4 = KeyCode.LeftShift.ToString();
                    }
                    else if (Input.GetKey(KeyCode.RightShift))
                    {
                        flag4 = true;
                        str4 = KeyCode.RightShift.ToString();
                    }
                    else if (Input.GetAxis("Mouse ScrollWheel") != 0f)
                    {
                        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                        {
                            flag4 = true;
                            str4 = "Scroll Up";
                        }
                        else
                        {
                            flag4 = true;
                            str4 = "Scroll Down";
                        }
                    }
                    else
                    {
                        i = 0;
                        while (i < 7)
                        {
                            if (Input.GetKeyDown((KeyCode)(323 + i)))
                            {
                                flag4 = true;
                                str4 = "Mouse" + Convert.ToString(i);
                            }
                            i++;
                        }
                    }
                    if (flag4)
                    {
                        for (i = 0; i < 14; i++)
                        {
                            num23 = 101 + i;
                            if (((int)FengGameManagerMKII.settings[100]) == num23)
                            {
                                FengGameManagerMKII.settings[num23] = str4;
                                FengGameManagerMKII.settings[100] = 0;
                                FengGameManagerMKII.inputRC.setInputTitan(i, str4);
                            }
                        }
                    }
                }
            }
            else if (((int)FengGameManagerMKII.settings[190]) == 2)
            {
                list7 = new List<string> { "Forward:", "Back:", "Left:", "Right:", "Walk:", "Jump:", "Mount:" };
                for (i = 0; i < list7.Count; i++)
                {
                    j = i;
                    k = 80f;
                    if (j > 3)
                    {
                        k = 390f;
                        j -= 4;
                    }
                    GUI.Label(new Rect(leftpos + k, (toppos + 86f) + (j * 25f), 145f, 22f), list7[i], "Label");
                }
                for (i = 0; i < 7; i++)
                {
                    num23 = 237 + i;
                    j = i;
                    k = 190f;
                    if (j > 3)
                    {
                        k = 500f;
                        j -= 4;
                    }
                    if (GUI.Button(new Rect(leftpos + k, (toppos + 86f) + (j * 25f), 120f, 20f), (string)FengGameManagerMKII.settings[num23], "box"))
                    {
                        FengGameManagerMKII.settings[num23] = "waiting...";
                        FengGameManagerMKII.settings[100] = num23;
                    }
                }
                if (((int)FengGameManagerMKII.settings[100]) != 0)
                {
                    current = Event.current;
                    flag4 = false;
                    str4 = "waiting...";
                    if ((current.type == EventType.KeyDown) && (current.keyCode != KeyCode.None))
                    {
                        flag4 = true;
                        str4 = current.keyCode.ToString();
                    }
                    else if (Input.GetKey(KeyCode.LeftShift))
                    {
                        flag4 = true;
                        str4 = KeyCode.LeftShift.ToString();
                    }
                    else if (Input.GetKey(KeyCode.RightShift))
                    {
                        flag4 = true;
                        str4 = KeyCode.RightShift.ToString();
                    }
                    else if (Input.GetAxis("Mouse ScrollWheel") != 0f)
                    {
                        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                        {
                            flag4 = true;
                            str4 = "Scroll Up";
                        }
                        else
                        {
                            flag4 = true;
                            str4 = "Scroll Down";
                        }
                    }
                    else
                    {
                        i = 0;
                        while (i < 7)
                        {
                            if (Input.GetKeyDown((KeyCode)(323 + i)))
                            {
                                flag4 = true;
                                str4 = "Mouse" + Convert.ToString(i);
                            }
                            i++;
                        }
                    }
                    if (flag4)
                    {
                        for (i = 0; i < 7; i++)
                        {
                            num23 = 237 + i;
                            if (((int)FengGameManagerMKII.settings[100]) == num23)
                            {
                                FengGameManagerMKII.settings[num23] = str4;
                                FengGameManagerMKII.settings[100] = 0;
                                FengGameManagerMKII.inputRC.setInputHorse(i, str4);
                            }
                        }
                    }
                }
            }
            else if (((int)FengGameManagerMKII.settings[190]) == 3)
            {
                list7 = new List<string> { "Rotate Up:", "Rotate Down:", "Rotate Left:", "Rotate Right:", "Fire:", "Mount:", "Slow Rotate:" };
                for (i = 0; i < list7.Count; i++)
                {
                    j = i;
                    k = 80f;
                    if (j > 3)
                    {
                        k = 390f;
                        j -= 4;
                    }
                    GUI.Label(new Rect(leftpos + k, (toppos + 86f) + (j * 25f), 145f, 22f), list7[i], "Label");
                }
                for (i = 0; i < 7; i++)
                {
                    num23 = 254 + i;
                    j = i;
                    k = 190f;
                    if (j > 3)
                    {
                        k = 500f;
                        j -= 4;
                    }
                    if (GUI.Button(new Rect(leftpos + k, (toppos + 86f) + (j * 25f), 120f, 20f), (string)FengGameManagerMKII.settings[num23], "box"))
                    {
                        FengGameManagerMKII.settings[num23] = "waiting...";
                        FengGameManagerMKII.settings[100] = num23;
                    }
                }
                if (((int)FengGameManagerMKII.settings[100]) != 0)
                {
                    current = Event.current;
                    flag4 = false;
                    str4 = "waiting...";
                    if ((current.type == EventType.KeyDown) && (current.keyCode != KeyCode.None))
                    {
                        flag4 = true;
                        str4 = current.keyCode.ToString();
                    }
                    else if (Input.GetKey(KeyCode.LeftShift))
                    {
                        flag4 = true;
                        str4 = KeyCode.LeftShift.ToString();
                    }
                    else if (Input.GetKey(KeyCode.RightShift))
                    {
                        flag4 = true;
                        str4 = KeyCode.RightShift.ToString();
                    }
                    else if (Input.GetAxis("Mouse ScrollWheel") != 0f)
                    {
                        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                        {
                            flag4 = true;
                            str4 = "Scroll Up";
                        }
                        else
                        {
                            flag4 = true;
                            str4 = "Scroll Down";
                        }
                    }
                    else
                    {
                        i = 0;
                        while (i < 6)
                        {
                            if (Input.GetKeyDown((KeyCode)(323 + i)))
                            {
                                flag4 = true;
                                str4 = "Mouse" + Convert.ToString(i);
                            }
                            i++;
                        }
                    }
                    if (flag4)
                    {
                        for (i = 0; i < 7; i++)
                        {
                            num23 = 254 + i;
                            if (((int)FengGameManagerMKII.settings[100]) == num23)
                            {
                                FengGameManagerMKII.settings[num23] = str4;
                                FengGameManagerMKII.settings[100] = 0;
                                FengGameManagerMKII.inputRC.setInputCannon(i, str4);
                            }
                        }
                    }
                }
            }
        }
        public static void Bombs()
        {
            GUI.Label(new Rect(leftpos + 150f, toppos + 80f, 185f, 22f), "Bomb Mode", "Label");
            GUI.Label(new Rect(leftpos + 80f, toppos + 110f, 80f, 22f), "Color:", "Label");
            var textured = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            textured.SetPixel(0, 0, new Color((float)FengGameManagerMKII.settings[246], (float)FengGameManagerMKII.settings[247], (float)FengGameManagerMKII.settings[248], (float)FengGameManagerMKII.settings[249]));
            textured.Apply();
            GUI.DrawTexture(new Rect(leftpos + 120f, toppos + 113f, 40f, 15f), textured, ScaleMode.StretchToFill);
            UnityEngine.Object.Destroy(textured);
            GUI.Label(new Rect(leftpos + 72f, toppos + 135f, 20f, 22f), "R:", "Label");
            GUI.Label(new Rect(leftpos + 72f, toppos + 160f, 20f, 22f), "G:", "Label");
            GUI.Label(new Rect(leftpos + 72f, toppos + 185f, 20f, 22f), "B:", "Label");
            GUI.Label(new Rect(leftpos + 72f, toppos + 210f, 20f, 22f), "A:", "Label");
            FengGameManagerMKII.settings[246] = GUI.HorizontalSlider(new Rect(leftpos + 92f, toppos + 138f, 100f, 20f), (float)FengGameManagerMKII.settings[246], 0f, 1f);
            FengGameManagerMKII.settings[247] = GUI.HorizontalSlider(new Rect(leftpos + 92f, toppos + 163f, 100f, 20f), (float)FengGameManagerMKII.settings[247], 0f, 1f);
            FengGameManagerMKII.settings[248] = GUI.HorizontalSlider(new Rect(leftpos + 92f, toppos + 188f, 100f, 20f), (float)FengGameManagerMKII.settings[248], 0f, 1f);
            FengGameManagerMKII.settings[249] = GUI.HorizontalSlider(new Rect(leftpos + 92f, toppos + 213f, 100f, 20f), (float)FengGameManagerMKII.settings[249], 0.5f, 1f);
            GUI.Label(new Rect(leftpos + 72f, toppos + 235f, 95f, 22f), "Bomb Radius:", "Label");
            GUI.Label(new Rect(leftpos + 72f, toppos + 260f, 95f, 22f), "Bomb Range:", "Label");
            GUI.Label(new Rect(leftpos + 72f, toppos + 285f, 95f, 22f), "Bomb Speed:", "Label");
            GUI.Label(new Rect(leftpos + 72f, toppos + 310f, 95f, 22f), "Bomb CD:", "Label");
            GUI.Label(new Rect(leftpos + 72f, toppos + 335f, 95f, 22f), "Unused Points:", "Label");
            var num30 = (int)FengGameManagerMKII.settings[250];
            GUI.Label(new Rect(leftpos + 168f, toppos + 235f, 20f, 22f), num30.ToString(), "Label");
            num30 = (int)FengGameManagerMKII.settings[251];
            GUI.Label(new Rect(leftpos + 168f, toppos + 260f, 20f, 22f), num30.ToString(), "Label");
            num30 = (int)FengGameManagerMKII.settings[252];
            GUI.Label(new Rect(leftpos + 168f, toppos + 285f, 20f, 22f), num30.ToString(), "Label");
            GUI.Label(new Rect(leftpos + 168f, toppos + 310f, 20f, 22f), ((int)FengGameManagerMKII.settings[253]).ToString(), "Label");
            int num43 = (((20 - ((int)FengGameManagerMKII.settings[250])) - ((int)FengGameManagerMKII.settings[251])) - ((int)FengGameManagerMKII.settings[252])) - ((int)FengGameManagerMKII.settings[253]);
            GUI.Label(new Rect(leftpos + 168f, toppos + 335f, 20f, 22f), num43.ToString(), "Label");
            if (GUI.Button(new Rect(leftpos + 190f, toppos + 235f, 20f, 20f), "-"))
            {
                if (((int)FengGameManagerMKII.settings[250]) > 0)
                {
                    FengGameManagerMKII.settings[250] = ((int)FengGameManagerMKII.settings[250]) - 1;
                }
            }
            else if (GUI.Button(new Rect(leftpos + 215f, toppos + 235f, 20f, 20f), "+") && ((((int)FengGameManagerMKII.settings[250]) < 10) && (num43 > 0)))
            {
                FengGameManagerMKII.settings[250] = ((int)FengGameManagerMKII.settings[250]) + 1;
            }
            if (GUI.Button(new Rect(leftpos + 190f, toppos + 260f, 20f, 20f), "-"))
            {
                if (((int)FengGameManagerMKII.settings[251]) > 0)
                {
                    FengGameManagerMKII.settings[251] = ((int)FengGameManagerMKII.settings[251]) - 1;
                }
            }
            else if (GUI.Button(new Rect(leftpos + 215f, toppos + 260f, 20f, 20f), "+") && ((((int)FengGameManagerMKII.settings[251]) < 10) && (num43 > 0)))
            {
                FengGameManagerMKII.settings[251] = ((int)FengGameManagerMKII.settings[251]) + 1;
            }
            if (GUI.Button(new Rect(leftpos + 190f, toppos + 285f, 20f, 20f), "-"))
            {
                if (((int)FengGameManagerMKII.settings[252]) > 0)
                {
                    FengGameManagerMKII.settings[252] = ((int)FengGameManagerMKII.settings[252]) - 1;
                }
            }
            else if (GUI.Button(new Rect(leftpos + 215f, toppos + 285f, 20f, 20f), "+") && ((((int)FengGameManagerMKII.settings[252]) < 10) && (num43 > 0)))
            {
                FengGameManagerMKII.settings[252] = ((int)FengGameManagerMKII.settings[252]) + 1;
            }
            if (GUI.Button(new Rect(leftpos + 190f, toppos + 310f, 20f, 20f), "-"))
            {
                if (((int)FengGameManagerMKII.settings[253]) > 0)
                {
                    FengGameManagerMKII.settings[253] = ((int)FengGameManagerMKII.settings[253]) - 1;
                }
            }
            else if (GUI.Button(new Rect(leftpos + 215f, toppos + 310f, 20f, 20f), "+") && ((((int)FengGameManagerMKII.settings[253]) < 10) && (num43 > 0)))
            {
                FengGameManagerMKII.settings[253] = ((int)FengGameManagerMKII.settings[253]) + 1;
            }
        }
        public static void Human_Skins()
        {
            GUILayout.BeginArea(left);
            GUILayout.BeginVertical();
            BetterGUI.Header("Settings");
            BetterGUI.SubHeader("General");
            BetterGUI.Grid("Human Skins", ref Settings.HumanSkins, SwitcherStr);
            BetterGUI.Grid("Blade Trail:", ref Settings.BladeTrails, SwitcherStr);
            BetterGUI.Grid("Custom Gas:", ref Settings.CustomGas, SwitcherStr);
            BetterGUI.SubHeader("Advanced");
            BetterGUI.Grid("HD Trails:", ref Settings.BladeTrailsQuality, SwitcherStr);
            BetterGUI.Slider("Blade Trail Frame Rate:", true, 0, ref Settings.BladeTrailsFrameRate, 60f, 1000f);
            BetterGUI.TextField("Blade Trail Fade Time:", ref Settings.BladeTrailsFadeTime);
            GUILayout.EndVertical();
            GUILayout.EndArea();

            GUILayout.BeginArea(right);
            GUILayout.BeginVertical();
            BetterGUI.Header("Skins");
            GUILayout.BeginHorizontal();
            Settings.HumanCurrentSkin = GUILayout.SelectionGrid(Settings.HumanCurrentSkin, Settings.HumanSetTitles.ToArray(), 5, GUILayout.Width(330f), GUILayout.Height(30));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            BetterGUI.Label("Title:", 0, 105f);
            Settings.HumanSetTitles[Settings.HumanCurrentSkin] = GUILayout.TextField(Settings.HumanSetTitles[Settings.HumanCurrentSkin], GUILayout.Width(220f));
            GUILayout.EndHorizontal();
            for (int i = 0; i < 13; i++)
            {
                BetterGUI.TextField(Settings.HumanSkinLabels[i], ref Settings.HumanSkinFields[Settings.HumanCurrentSkin][i], 220f, 105f);
            }
            GUILayout.EndVertical();
            GUILayout.EndArea();

            //OLD
            {
                //GUI.Label(new Rect(leftpos + 205f, toppos + 52f, 120f, 30f), "Human Skin Mode:", "Label");
                //var flag2 = false;
                //if (((int)FengGameManagerMKII.settings[0]) == 1)
                //{
                //    flag2 = true;
                //}
                //var flag5 = GUI.Toggle(new Rect(leftpos + 325f, toppos + 52f, 40f, 20f), flag2, "On");
                //if (flag2 != flag5)
                //{
                //    if (flag5)
                //    {
                //        FengGameManagerMKII.settings[0] = 1;
                //    }
                //    else
                //    {
                //        FengGameManagerMKII.settings[0] = 0;
                //    }
                //}
                //float num44 = 44f;
                //if (((int)FengGameManagerMKII.settings[0x85]) == 0)
                //{
                //    if (GUI.Button(new Rect(leftpos + 375f, toppos + 51f, 120f, 22f), "Human Set 1"))
                //    {
                //        FengGameManagerMKII.settings[0x85] = 1;
                //    }
                //    FengGameManagerMKII.settings[3] = GUI.TextField(new Rect(leftpos + 80f, (toppos + 114f) + (num44 * 0f), 230f, 20f), (string)FengGameManagerMKII.settings[3]);
                //    FengGameManagerMKII.settings[4] = GUI.TextField(new Rect(leftpos + 80f, (toppos + 114f) + (num44 * 1f), 230f, 20f), (string)FengGameManagerMKII.settings[4]);
                //    FengGameManagerMKII.settings[5] = GUI.TextField(new Rect(leftpos + 80f, (toppos + 114f) + (num44 * 2f), 230f, 20f), (string)FengGameManagerMKII.settings[5]);
                //    FengGameManagerMKII.settings[6] = GUI.TextField(new Rect(leftpos + 80f, (toppos + 114f) + (num44 * 3f), 230f, 20f), (string)FengGameManagerMKII.settings[6]);
                //    FengGameManagerMKII.settings[7] = GUI.TextField(new Rect(leftpos + 80f, (toppos + 114f) + (num44 * 4f), 230f, 20f), (string)FengGameManagerMKII.settings[7]);
                //    FengGameManagerMKII.settings[8] = GUI.TextField(new Rect(leftpos + 80f, (toppos + 114f) + (num44 * 5f), 230f, 20f), (string)FengGameManagerMKII.settings[8]);
                //    FengGameManagerMKII.settings[14] = GUI.TextField(new Rect(leftpos + 80f, (toppos + 114f) + (num44 * 6f), 230f, 20f), (string)FengGameManagerMKII.settings[14]);
                //    FengGameManagerMKII.settings[9] = GUI.TextField(new Rect(leftpos + 390f, (toppos + 114f) + (num44 * 0f), 230f, 20f), (string)FengGameManagerMKII.settings[9]);
                //    FengGameManagerMKII.settings[10] = GUI.TextField(new Rect(leftpos + 390f, (toppos + 114f) + (num44 * 1f), 230f, 20f), (string)FengGameManagerMKII.settings[10]);
                //    FengGameManagerMKII.settings[11] = GUI.TextField(new Rect(leftpos + 390f, (toppos + 114f) + (num44 * 2f), 230f, 20f), (string)FengGameManagerMKII.settings[11]);
                //    FengGameManagerMKII.settings[12] = GUI.TextField(new Rect(leftpos + 390f, (toppos + 114f) + (num44 * 3f), 230f, 20f), (string)FengGameManagerMKII.settings[12]);
                //    FengGameManagerMKII.settings[13] = GUI.TextField(new Rect(leftpos + 390f, (toppos + 114f) + (num44 * 4f), 230f, 20f), (string)FengGameManagerMKII.settings[13]);
                //    FengGameManagerMKII.settings[0x5e] = GUI.TextField(new Rect(leftpos + 390f, (toppos + 114f) + (num44 * 5f), 230f, 20f), (string)FengGameManagerMKII.settings[0x5e]);
                //}
                //else if (((int)FengGameManagerMKII.settings[0x85]) == 1)
                //{
                //    if (GUI.Button(new Rect(leftpos + 375f, toppos + 51f, 120f, 22f), "Human Set 2"))
                //    {
                //        FengGameManagerMKII.settings[0x85] = 2;
                //    }
                //    FengGameManagerMKII.settings[0x86] = GUI.TextField(new Rect(leftpos + 80f, (toppos + 114f) + (num44 * 0f), 230f, 20f), (string)FengGameManagerMKII.settings[0x86]);
                //    FengGameManagerMKII.settings[0x87] = GUI.TextField(new Rect(leftpos + 80f, (toppos + 114f) + (num44 * 1f), 230f, 20f), (string)FengGameManagerMKII.settings[0x87]);
                //    FengGameManagerMKII.settings[0x88] = GUI.TextField(new Rect(leftpos + 80f, (toppos + 114f) + (num44 * 2f), 230f, 20f), (string)FengGameManagerMKII.settings[0x88]);
                //    FengGameManagerMKII.settings[0x89] = GUI.TextField(new Rect(leftpos + 80f, (toppos + 114f) + (num44 * 3f), 230f, 20f), (string)FengGameManagerMKII.settings[0x89]);
                //    FengGameManagerMKII.settings[0x8a] = GUI.TextField(new Rect(leftpos + 80f, (toppos + 114f) + (num44 * 4f), 230f, 20f), (string)FengGameManagerMKII.settings[0x8a]);
                //    FengGameManagerMKII.settings[0x8b] = GUI.TextField(new Rect(leftpos + 80f, (toppos + 114f) + (num44 * 5f), 230f, 20f), (string)FengGameManagerMKII.settings[0x8b]);
                //    FengGameManagerMKII.settings[0x91] = GUI.TextField(new Rect(leftpos + 80f, (toppos + 114f) + (num44 * 6f), 230f, 20f), (string)FengGameManagerMKII.settings[0x91]);
                //    FengGameManagerMKII.settings[140] = GUI.TextField(new Rect(leftpos + 390f, (toppos + 114f) + (num44 * 0f), 230f, 20f), (string)FengGameManagerMKII.settings[140]);
                //    FengGameManagerMKII.settings[0x8d] = GUI.TextField(new Rect(leftpos + 390f, (toppos + 114f) + (num44 * 1f), 230f, 20f), (string)FengGameManagerMKII.settings[0x8d]);
                //    FengGameManagerMKII.settings[0x8e] = GUI.TextField(new Rect(leftpos + 390f, (toppos + 114f) + (num44 * 2f), 230f, 20f), (string)FengGameManagerMKII.settings[0x8e]);
                //    FengGameManagerMKII.settings[0x8f] = GUI.TextField(new Rect(leftpos + 390f, (toppos + 114f) + (num44 * 3f), 230f, 20f), (string)FengGameManagerMKII.settings[0x8f]);
                //    FengGameManagerMKII.settings[0x90] = GUI.TextField(new Rect(leftpos + 390f, (toppos + 114f) + (num44 * 4f), 230f, 20f), (string)FengGameManagerMKII.settings[0x90]);
                //    FengGameManagerMKII.settings[0x92] = GUI.TextField(new Rect(leftpos + 390f, (toppos + 114f) + (num44 * 5f), 230f, 20f), (string)FengGameManagerMKII.settings[0x92]);
                //}
                //else if (((int)FengGameManagerMKII.settings[0x85]) == 2)
                //{
                //    if (GUI.Button(new Rect(leftpos + 375f, toppos + 51f, 120f, 22f), "Human Set 3"))
                //    {
                //        FengGameManagerMKII.settings[0x85] = 0;
                //    }
                //    FengGameManagerMKII.settings[0x93] = GUI.TextField(new Rect(leftpos + 80f, (toppos + 114f) + (num44 * 0f), 230f, 20f), (string)FengGameManagerMKII.settings[0x93]);
                //    FengGameManagerMKII.settings[0x94] = GUI.TextField(new Rect(leftpos + 80f, (toppos + 114f) + (num44 * 1f), 230f, 20f), (string)FengGameManagerMKII.settings[0x94]);
                //    FengGameManagerMKII.settings[0x95] = GUI.TextField(new Rect(leftpos + 80f, (toppos + 114f) + (num44 * 2f), 230f, 20f), (string)FengGameManagerMKII.settings[0x95]);
                //    FengGameManagerMKII.settings[150] = GUI.TextField(new Rect(leftpos + 80f, (toppos + 114f) + (num44 * 3f), 230f, 20f), (string)FengGameManagerMKII.settings[150]);
                //    FengGameManagerMKII.settings[0x97] = GUI.TextField(new Rect(leftpos + 80f, (toppos + 114f) + (num44 * 4f), 230f, 20f), (string)FengGameManagerMKII.settings[0x97]);
                //    FengGameManagerMKII.settings[0x98] = GUI.TextField(new Rect(leftpos + 80f, (toppos + 114f) + (num44 * 5f), 230f, 20f), (string)FengGameManagerMKII.settings[0x98]);
                //    FengGameManagerMKII.settings[0x9e] = GUI.TextField(new Rect(leftpos + 80f, (toppos + 114f) + (num44 * 6f), 230f, 20f), (string)FengGameManagerMKII.settings[0x9e]);
                //    FengGameManagerMKII.settings[0x99] = GUI.TextField(new Rect(leftpos + 390f, (toppos + 114f) + (num44 * 0f), 230f, 20f), (string)FengGameManagerMKII.settings[0x99]);
                //    FengGameManagerMKII.settings[0x9a] = GUI.TextField(new Rect(leftpos + 390f, (toppos + 114f) + (num44 * 1f), 230f, 20f), (string)FengGameManagerMKII.settings[0x9a]);
                //    FengGameManagerMKII.settings[0x9b] = GUI.TextField(new Rect(leftpos + 390f, (toppos + 114f) + (num44 * 2f), 230f, 20f), (string)FengGameManagerMKII.settings[0x9b]);
                //    FengGameManagerMKII.settings[0x9c] = GUI.TextField(new Rect(leftpos + 390f, (toppos + 114f) + (num44 * 3f), 230f, 20f), (string)FengGameManagerMKII.settings[0x9c]);
                //    FengGameManagerMKII.settings[0x9d] = GUI.TextField(new Rect(leftpos + 390f, (toppos + 114f) + (num44 * 4f), 230f, 20f), (string)FengGameManagerMKII.settings[0x9d]);
                //    FengGameManagerMKII.settings[0x9f] = GUI.TextField(new Rect(leftpos + 390f, (toppos + 114f) + (num44 * 5f), 230f, 20f), (string)FengGameManagerMKII.settings[0x9f]);
                //}
                //GUI.Label(new Rect(leftpos + 80f, (toppos + 92f) + (num44 * 0f), 150f, 20f), "Horse:", "Label");
                //GUI.Label(new Rect(leftpos + 80f, (toppos + 92f) + (num44 * 1f), 227f, 20f), "Hair (model dependent):", "Label");
                //GUI.Label(new Rect(leftpos + 80f, (toppos + 92f) + (num44 * 2f), 150f, 20f), "Eyes:", "Label");
                //GUI.Label(new Rect(leftpos + 80f, (toppos + 92f) + (num44 * 3f), 240f, 20f), "Glass (must have a glass enabled):", "Label");
                //GUI.Label(new Rect(leftpos + 80f, (toppos + 92f) + (num44 * 4f), 150f, 20f), "Face:", "Label");
                //GUI.Label(new Rect(leftpos + 80f, (toppos + 92f) + (num44 * 5f), 150f, 20f), "Skin:", "Label");
                //GUI.Label(new Rect(leftpos + 80f, (toppos + 92f) + (num44 * 6f), 240f, 20f), "Hoodie (costume dependent):", "Label");
                //GUI.Label(new Rect(leftpos + 390f, (toppos + 92f) + (num44 * 0f), 240f, 20f), "Costume (model dependent):", "Label");
                //GUI.Label(new Rect(leftpos + 390f, (toppos + 92f) + (num44 * 1f), 150f, 20f), "Logo & Cape:", "Label");
                //GUI.Label(new Rect(leftpos + 390f, (toppos + 92f) + (num44 * 2f), 240f, 20f), "3DMG Center & 3DMG/Blade/Gun(left):", "Label");
                //GUI.Label(new Rect(leftpos + 390f, (toppos + 92f) + (num44 * 3f), 227f, 20f), "3DMG/Blade/Gun(right):", "Label");
                //GUI.Label(new Rect(leftpos + 390f, (toppos + 92f) + (num44 * 4f), 150f, 20f), "Gas:", "Label");
                //GUI.Label(new Rect(leftpos + 390f, (toppos + 92f) + (num44 * 5f), 150f, 20f), "Weapon Trail:", "Label");
            }
        }
        public static void Titan_Skins()
        {
            int num45;
            int num46;
            GUI.Label(new Rect(leftpos + 270f, toppos + 52f, 120f, 30f), "Titan Skin Mode:", "Label");
            var flag6 = false;
            if (Settings.TitanSkins == 1)
            {
                flag6 = true;
            }
            bool flag11 = GUI.Toggle(new Rect(leftpos + 390f, toppos + 52f, 40f, 20f), flag6, "On");
            if (flag6 != flag11)
            {
                if (flag11)
                {
                    Settings.TitanSkins = 1;
                }
                else
                {
                    Settings.TitanSkins = 0;
                }
            }
            GUI.Label(new Rect(leftpos + 270f, toppos + 77f, 120f, 30f), "Randomized Pairs:", "Label");
            flag6 = false;
            if (((int)FengGameManagerMKII.settings[32]) == 1)
            {
                flag6 = true;
            }
            flag11 = GUI.Toggle(new Rect(leftpos + 390f, toppos + 77f, 40f, 20f), flag6, "On");
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
            GUI.Label(new Rect(leftpos + 158f, toppos + 112f, 150f, 20f), "Titan Hair:", "Label");
            FengGameManagerMKII.settings[21] = GUI.TextField(new Rect(leftpos + 80f, toppos + 134f, 165f, 20f), (string)FengGameManagerMKII.settings[21]);
            FengGameManagerMKII.settings[22] = GUI.TextField(new Rect(leftpos + 80f, toppos + 156f, 165f, 20f), (string)FengGameManagerMKII.settings[22]);
            FengGameManagerMKII.settings[23] = GUI.TextField(new Rect(leftpos + 80f, toppos + 178f, 165f, 20f), (string)FengGameManagerMKII.settings[23]);
            FengGameManagerMKII.settings[24] = GUI.TextField(new Rect(leftpos + 80f, toppos + 200f, 165f, 20f), (string)FengGameManagerMKII.settings[24]);
            FengGameManagerMKII.settings[25] = GUI.TextField(new Rect(leftpos + 80f, toppos + 222f, 165f, 20f), (string)FengGameManagerMKII.settings[25]);
            if (GUI.Button(new Rect(leftpos + 250f, toppos + 134f, 60f, 20f), FengGameManagerMKII.hairtype((int)FengGameManagerMKII.settings[16])))
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
            else if (GUI.Button(new Rect(leftpos + 250f, toppos + 156f, 60f, 20f), FengGameManagerMKII.hairtype((int)FengGameManagerMKII.settings[17])))
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
            else if (GUI.Button(new Rect(leftpos + 250f, toppos + 178f, 60f, 20f), FengGameManagerMKII.hairtype((int)FengGameManagerMKII.settings[18])))
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
            else if (GUI.Button(new Rect(leftpos + 250f, toppos + 200f, 60f, 20f), FengGameManagerMKII.hairtype((int)FengGameManagerMKII.settings[19])))
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
            else if (GUI.Button(new Rect(leftpos + 250f, toppos + 222f, 60f, 20f), FengGameManagerMKII.hairtype((int)FengGameManagerMKII.settings[20])))
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
            GUI.Label(new Rect(leftpos + 158f, toppos + 252f, 150f, 20f), "Titan Eye:", "Label");
            FengGameManagerMKII.settings[26] = GUI.TextField(new Rect(leftpos + 80f, toppos + 274f, 230f, 20f), (string)FengGameManagerMKII.settings[26]);
            FengGameManagerMKII.settings[27] = GUI.TextField(new Rect(leftpos + 80f, toppos + 296f, 230f, 20f), (string)FengGameManagerMKII.settings[27]);
            FengGameManagerMKII.settings[28] = GUI.TextField(new Rect(leftpos + 80f, toppos + 318f, 230f, 20f), (string)FengGameManagerMKII.settings[28]);
            FengGameManagerMKII.settings[29] = GUI.TextField(new Rect(leftpos + 80f, toppos + 340f, 230f, 20f), (string)FengGameManagerMKII.settings[29]);
            FengGameManagerMKII.settings[30] = GUI.TextField(new Rect(leftpos + 80f, toppos + 362f, 230f, 20f), (string)FengGameManagerMKII.settings[30]);
            GUI.Label(new Rect(leftpos + 455f, toppos + 112f, 150f, 20f), "Titan Body:", "Label");
            FengGameManagerMKII.settings[86] = GUI.TextField(new Rect(leftpos + 390f, toppos + 134f, 230f, 20f), (string)FengGameManagerMKII.settings[86]);
            FengGameManagerMKII.settings[87] = GUI.TextField(new Rect(leftpos + 390f, toppos + 156f, 230f, 20f), (string)FengGameManagerMKII.settings[87]);
            FengGameManagerMKII.settings[88] = GUI.TextField(new Rect(leftpos + 390f, toppos + 178f, 230f, 20f), (string)FengGameManagerMKII.settings[88]);
            FengGameManagerMKII.settings[89] = GUI.TextField(new Rect(leftpos + 390f, toppos + 200f, 230f, 20f), (string)FengGameManagerMKII.settings[89]);
            FengGameManagerMKII.settings[90] = GUI.TextField(new Rect(leftpos + 390f, toppos + 222f, 230f, 20f), (string)FengGameManagerMKII.settings[90]);
            GUI.Label(new Rect(leftpos + 472f, toppos + 252f, 150f, 20f), "Eren:", "Label");
            FengGameManagerMKII.settings[65] = GUI.TextField(new Rect(leftpos + 390f, toppos + 274f, 230f, 20f), (string)FengGameManagerMKII.settings[65]);
            GUI.Label(new Rect(leftpos + 470f, toppos + 296f, 150f, 20f), "Annie:", "Label");
            FengGameManagerMKII.settings[66] = GUI.TextField(new Rect(leftpos + 390f, toppos + 318f, 230f, 20f), (string)FengGameManagerMKII.settings[66]);
            GUI.Label(new Rect(leftpos + 465f, toppos + 340f, 150f, 20f), "Colossal:", "Label");
            FengGameManagerMKII.settings[67] = GUI.TextField(new Rect(leftpos + 390f, toppos + 362f, 230f, 20f), (string)FengGameManagerMKII.settings[67]);
        }
        public static void Level_Skins()
        {
            var levelskinpage = 0;
            if (levelskinpage == 0)
            {
                GUILayout.BeginArea(left);
                GUILayout.BeginVertical();
                LevelSkinForestScrollLeft = GUILayout.BeginScrollView(LevelSkinForestScrollLeft, false, false);

                BetterGUI.Header("Forest");
                BetterGUI.SubHeader("Settings");
                BetterGUI.Grid("Level Skins:", ref Settings.LocationSkins, SwitcherStr);
                BetterGUI.Grid("Map:", ref levelskinpage, LevelSkinPageStr);
                BetterGUI.Grid("Randomized Pairs:", ref Settings.ForestRandomizedPairs, SwitcherStr);
                BetterGUI.Grid("Particles", ref Settings.Particles, SwitcherStr);
                if (Settings.Particles == 1)
                {
                    BetterGUI.Slider("Count:", true, 0, ref Settings.ParticlesCount, 0f, 15000);
                    BetterGUI.Slider("Height:", true, 0, ref Settings.ParticlesHeight, 0f, 1000f);
                    BetterGUI.Slider("Life Time Min:", true, 0, ref Settings.ParticlesLifeTimeStart, 0f, 600f);
                    BetterGUI.Slider("Life Time Max:", true, 0, ref Settings.ParticlesLifeTimeEnd, 0f, 600f);
                    BetterGUI.Slider("Gravity:", false, 0, ref Settings.ParticlesGravity, 0f, 5f, "0.###");
                    BetterGUI.Slider("R:", false, 0,
                        ref Settings.ParticlesColorR, 0f, 1f, "0.###", 160f, 25f);
                    BetterGUI.Slider("G:", false, 0,
                        ref Settings.ParticlesColorG, 0f, 1f, "0.###", 160f, 25f);
                    BetterGUI.Slider("B:", false, 0,
                        ref Settings.ParticlesColorB, 0f, 1f, "0.###", 160f, 25f);
                    BetterGUI.Slider("A:", false, 0,
                        ref Settings.ParticlesColorA, 0f, 1f, "0.###", 160f, 25f);
                }

                BetterGUI.SubHeader("Presets");
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                Settings.ForestCurrentSkin = GUILayout.SelectionGrid(Settings.ForestCurrentSkin,
                    Settings.ForestSetTitles.ToArray(), 1, GUILayout.Width(115f));
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (BetterGUI.Button("+", false))
                {
                    Settings.ForestSkinCount++;
                    Settings.ForestSkinFields.Add(new string[23].Select(x => "").ToArray());
                    Settings.ForestSetTitles.Add("Set " + Settings.ForestSkinCount.ToString());
                    Settings.ForestAmbient.Add(0);
                    Settings.ForestAmbientColor.Add(new[] { 0.5f, 0.5f, 0.5f });
                    Settings.ForestFog.Add(0);
                    Settings.ForestFogColor.Add(new[] { 0.066f, 0.066f, 0.066f });
                    Settings.ForestFogStartDistance.Add(0f);
                    Settings.ForestFogEndDistance.Add(1000f);
                    Settings.ForestCurrentSkin = Settings.ForestSkinCount - 1;
                }

                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                GUILayout.EndScrollView();
                GUILayout.EndVertical();
                GUILayout.EndArea();

                GUILayout.BeginArea(right);
                GUILayout.BeginVertical();

                BetterGUI.Header(Settings.ForestSetTitles[Settings.ForestCurrentSkin]);
                LevelSkinForestScrollRight = GUILayout.BeginScrollView(LevelSkinForestScrollRight, false, false);
                BetterGUI.SubHeader("Skins");
                GUILayout.BeginHorizontal();
                BetterGUI.Label("Title:");
                Settings.ForestSetTitles[Settings.ForestCurrentSkin] = GUILayout.TextField(
                    Settings.ForestSetTitles[Settings.ForestCurrentSkin], GUILayout.Width(190f));
                GUILayout.EndHorizontal();
                for (int i = 0; i < 23; i++)
                {
                    BetterGUI.TextField(Settings.ForestSkinLabels[i],
                        ref Settings.ForestSkinFields[Settings.ForestCurrentSkin][i]);
                }

                BetterGUI.SubHeader("Effects");
                GUILayout.BeginHorizontal();
                BetterGUI.Label("Ambient");
                Settings.ForestAmbient[Settings.ForestCurrentSkin] = GUILayout.SelectionGrid(
                    Settings.ForestAmbient[Settings.ForestCurrentSkin], SwitcherStr, 2,
                    GUILayout.Width(190f));
                GUILayout.EndHorizontal();
                if (Settings.ForestAmbient[Settings.ForestCurrentSkin] == 1)
                {
                    BetterGUI.Slider("R:", false, 0,
                        ref Settings.ForestAmbientColor[Settings.ForestCurrentSkin][0], 0f, 1f, "0.###", 160f, 25f);
                    BetterGUI.Slider("G:", false, 0,
                        ref Settings.ForestAmbientColor[Settings.ForestCurrentSkin][1], 0f, 1f, "0.###", 160f, 25f);
                    BetterGUI.Slider("B:", false, 0,
                        ref Settings.ForestAmbientColor[Settings.ForestCurrentSkin][2], 0f, 1f, "0.###", 160f, 25f);
                }

                GUILayout.BeginHorizontal();
                BetterGUI.Label("Fog");
                Settings.ForestFog[Settings.ForestCurrentSkin] = GUILayout.SelectionGrid(
                    Settings.ForestFog[Settings.ForestCurrentSkin], SwitcherStr, 2, GUILayout.Width(190f));
                GUILayout.EndHorizontal();
                if (Settings.ForestFog[Settings.ForestCurrentSkin] == 1)
                {
                    BetterGUI.Slider("R:", false, 0,
                        ref Settings.ForestFogColor[Settings.ForestCurrentSkin][0], 0f, 1f, "0.###", 160f, 25f);
                    BetterGUI.Slider("G:", false, 0,
                        ref Settings.ForestFogColor[Settings.ForestCurrentSkin][1], 0f, 1f, "0.###", 160f, 25f);
                    BetterGUI.Slider("B:", false, 0,
                        ref Settings.ForestFogColor[Settings.ForestCurrentSkin][2], 0f, 1f, "0.###", 160f, 25f);
                    GUILayout.BeginHorizontal();
                    BetterGUI.Label("Start Distance:");
                    Settings.ForestFogStartDistance[Settings.ForestCurrentSkin] =
                        GUILayout.HorizontalSlider(
                            Settings.ForestFogStartDistance[Settings.ForestCurrentSkin], 0f, 1000f,
                            GUILayout.Width(160f));
                    BetterGUI.Label(Mathf.Round(Settings.ForestFogStartDistance[Settings.ForestCurrentSkin]).ToString(), 1, 25f);
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    BetterGUI.Label("End Distance:");
                    Settings.ForestFogEndDistance[Settings.ForestCurrentSkin] = GUILayout.HorizontalSlider(
                        Settings.ForestFogEndDistance[Settings.ForestCurrentSkin], 0f, 1000f,
                        GUILayout.Width(160f));
                    BetterGUI.Label(Mathf.Round(Settings.ForestFogEndDistance[Settings.ForestCurrentSkin]).ToString(), 1, 25f);
                    GUILayout.EndHorizontal();
                }

                GUILayout.EndScrollView();
                GUILayout.EndVertical();
                GUILayout.EndArea();
            }
            else
            {
                GUILayout.BeginArea(left);
                GUILayout.BeginVertical();
                LevelSkinCityScrollLeft = GUILayout.BeginScrollView(LevelSkinCityScrollLeft, false, false);

                BetterGUI.Header("City");
                BetterGUI.SubHeader("Settings");
                BetterGUI.Grid("Level Skins:", ref Settings.LocationSkins, SwitcherStr);
                BetterGUI.Grid("Map:", ref levelskinpage, LevelSkinPageStr);
                BetterGUI.SubHeader("Presets");
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                Settings.CityCurrentSkin = GUILayout.SelectionGrid(Settings.CityCurrentSkin,
                    Settings.CitySetTitles.ToArray(), 1, GUILayout.Width(115f));
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (BetterGUI.Button("+", false))
                {
                    Settings.CitySkinCount++;
                    Settings.CitySkinFields.Add(new string[17].Select(x => "").ToArray());
                    Settings.CitySetTitles.Add("Set " + Settings.CitySkinCount.ToString());
                    Settings.CityAmbient.Add(0);
                    Settings.CityAmbientColor.Add(new[] { 0.5f, 0.5f, 0.5f });
                    Settings.CityFog.Add(0);
                    Settings.CityFogColor.Add(new[] { 0.066f, 0.066f, 0.066f });
                    Settings.CityFogStartDistance.Add(0f);
                    Settings.CityFogEndDistance.Add(1000f);
                    Settings.CityCurrentSkin = Settings.CitySkinCount - 1;
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                GUILayout.EndScrollView();
                GUILayout.EndVertical();
                GUILayout.EndArea();

                GUILayout.BeginArea(right);
                GUILayout.BeginVertical();

                BetterGUI.Header(Settings.CitySetTitles[Settings.CityCurrentSkin]);
                LevelSkinCityScrollRight = GUILayout.BeginScrollView(LevelSkinCityScrollRight, false, false);
                BetterGUI.SubHeader("Skins");
                GUILayout.BeginHorizontal();
                BetterGUI.Label("Title:");
                Settings.CitySetTitles[Settings.CityCurrentSkin] = GUILayout.TextField(Settings.CitySetTitles[Settings.CityCurrentSkin], GUILayout.Width(190f));
                GUILayout.EndHorizontal();
                for (int i = 0; i < 17; i++)
                {
                    BetterGUI.TextField(Settings.CitySkinLabels[i],
                        ref Settings.CitySkinFields[Settings.CityCurrentSkin][i]);
                }
                BetterGUI.SubHeader("Effects");
                GUILayout.BeginHorizontal();
                BetterGUI.Label("Ambient");
                Settings.CityAmbient[Settings.CityCurrentSkin] = GUILayout.SelectionGrid(
                    Settings.CityAmbient[Settings.CityCurrentSkin], SwitcherStr, 2,
                    GUILayout.Width(190f));
                GUILayout.EndHorizontal();
                if (Settings.CityAmbient[Settings.CityCurrentSkin] == 1)
                {
                    BetterGUI.Slider("R:", false, 0,
                        ref Settings.CityAmbientColor[Settings.CityCurrentSkin][0], 0f, 1f, "0.###", 160f, 25f);
                    BetterGUI.Slider("G:", false, 0,
                        ref Settings.CityAmbientColor[Settings.CityCurrentSkin][1], 0f, 1f, "0.###", 160f, 25f);
                    BetterGUI.Slider("B:", false, 0,
                        ref Settings.CityAmbientColor[Settings.CityCurrentSkin][2], 0f, 1f, "0.###", 160f, 25f);
                }
                GUILayout.BeginHorizontal();
                BetterGUI.Label("Fog");
                Settings.CityFog[Settings.CityCurrentSkin] = GUILayout.SelectionGrid(Settings.CityFog[Settings.CityCurrentSkin], SwitcherStr, 2, GUILayout.Width(190f));
                GUILayout.EndHorizontal();
                if (Settings.CityFog[Settings.CityCurrentSkin] == 1)
                {
                    BetterGUI.Slider("R:", false, 0,
                        ref Settings.CityFogColor[Settings.CityCurrentSkin][0], 0f, 1f, "0.###", 160f, 25f);
                    BetterGUI.Slider("G:", false, 0,
                        ref Settings.CityFogColor[Settings.CityCurrentSkin][1], 0f, 1f, "0.###", 160f, 25f);
                    BetterGUI.Slider("B:", false, 0,
                        ref Settings.CityFogColor[Settings.CityCurrentSkin][2], 0f, 1f, "0.###", 160f, 25f);
                    GUILayout.BeginHorizontal();
                    BetterGUI.Label("Start Distance:");
                    Settings.CityFogStartDistance[Settings.CityCurrentSkin] = GUILayout.HorizontalSlider(
                        Settings.CityFogStartDistance[Settings.CityCurrentSkin], 0f, 1000f,
                        GUILayout.Width(160f));
                    BetterGUI.Label(Mathf.Round(Settings.CityFogStartDistance[Settings.CityCurrentSkin]).ToString(), 1, 25f);
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    BetterGUI.Label("End Distance:");
                    Settings.CityFogEndDistance[Settings.CityCurrentSkin] = GUILayout.HorizontalSlider(
                        Settings.CityFogEndDistance[Settings.CityCurrentSkin], 0f, 1000f,
                        GUILayout.Width(160f));
                    BetterGUI.Label(Mathf.Round(Settings.CityFogEndDistance[Settings.CityCurrentSkin]).ToString(), 1, 25f);
                    GUILayout.EndHorizontal();
                }

                GUILayout.EndScrollView();
                GUILayout.EndVertical();
                GUILayout.EndArea();

            }
        }
        public static void Custom_Map()
        {
            GUI.Label(new Rect(leftpos + 150f, toppos + 51f, 120f, 22f), "Map Settings", "Label");
            GUI.Label(new Rect(leftpos + 50f, toppos + 81f, 140f, 20f), "Titan Spawn Cap:", "Label");
            FengGameManagerMKII.settings[85] = GUI.TextField(new Rect(leftpos + 155f, toppos + 81f, 30f, 20f), (string)FengGameManagerMKII.settings[85]);
            var strArray16 = new[] { "1 Round", "Waves", "PVP", "Racing", "Custom" };
            RCSettings.gameType = GUI.SelectionGrid(new Rect(leftpos + 190f, toppos + 80f, 140f, 60f), RCSettings.gameType, strArray16, 2, GUI.skin.toggle);
            GUI.Label(new Rect(leftpos + 150f, toppos + 155f, 150f, 20f), "Level Script:", "Label");
            FengGameManagerMKII.currentScript = GUI.TextField(new Rect(leftpos + 50f, toppos + 180f, 275f, 220f), FengGameManagerMKII.currentScript);
            if (GUI.Button(new Rect(leftpos + 100f, toppos + 410f, 50f, 25f), "Copy"))
            {
                var editor = new TextEditor
                {
                    content = new GUIContent(FengGameManagerMKII.currentScript)
                };
                editor.SelectAll();
                editor.Copy();
            }
            else if (GUI.Button(new Rect(leftpos + 225f, toppos + 410f, 50f, 25f), "Clear"))
            {
                FengGameManagerMKII.currentScript = string.Empty;
            }
            GUI.Label(new Rect(leftpos + 455f, toppos + 51f, 180f, 20f), "Custom Textures", "Label");
            GUI.Label(new Rect(leftpos + 375f, toppos + 81f, 180f, 20f), "Ground Skin:", "Label");
            FengGameManagerMKII.settings[162] = GUI.TextField(new Rect(leftpos + 375f, toppos + 103f, 275f, 20f), (string)FengGameManagerMKII.settings[162]);
            GUI.Label(new Rect(leftpos + 375f, toppos + 125f, 150f, 20f), "Skybox Front:", "Label");
            FengGameManagerMKII.settings[175] = GUI.TextField(new Rect(leftpos + 375f, toppos + 147f, 275f, 20f), (string)FengGameManagerMKII.settings[175]);
            GUI.Label(new Rect(leftpos + 375f, toppos + 169f, 150f, 20f), "Skybox Back:", "Label");
            FengGameManagerMKII.settings[176] = GUI.TextField(new Rect(leftpos + 375f, toppos + 191f, 275f, 20f), (string)FengGameManagerMKII.settings[176]);
            GUI.Label(new Rect(leftpos + 375f, toppos + 213f, 150f, 20f), "Skybox Left:", "Label");
            FengGameManagerMKII.settings[177] = GUI.TextField(new Rect(leftpos + 375f, toppos + 235f, 275f, 20f), (string)FengGameManagerMKII.settings[177]);
            GUI.Label(new Rect(leftpos + 375f, toppos + 257f, 150f, 20f), "Skybox Right:", "Label");
            FengGameManagerMKII.settings[178] = GUI.TextField(new Rect(leftpos + 375f, toppos + 279f, 275f, 20f), (string)FengGameManagerMKII.settings[178]);
            GUI.Label(new Rect(leftpos + 375f, toppos + 301f, 150f, 20f), "Skybox Up:", "Label");
            FengGameManagerMKII.settings[179] = GUI.TextField(new Rect(leftpos + 375f, toppos + 323f, 275f, 20f), (string)FengGameManagerMKII.settings[179]);
            GUI.Label(new Rect(leftpos + 375f, toppos + 345f, 150f, 20f), "Skybox Down:", "Label");
            FengGameManagerMKII.settings[180] = GUI.TextField(new Rect(leftpos + 375f, toppos + 367f, 275f, 20f), (string)FengGameManagerMKII.settings[180]);
        }
        public static void Custom_Logic()
        {
            FengGameManagerMKII.currentScriptLogic = GUI.TextField(new Rect(leftpos + 50f, toppos + 82f, 600f, 270f), FengGameManagerMKII.currentScriptLogic);
            if (GUI.Button(new Rect(leftpos + 250f, toppos + 365f, 50f, 20f), "Copy"))
            {
                var editor = new TextEditor
                {
                    content = new GUIContent(FengGameManagerMKII.currentScriptLogic)
                };
                editor.SelectAll();
                editor.Copy();
            }
            else if (GUI.Button(new Rect(leftpos + 400f, toppos + 365f, 50f, 20f), "Clear"))
            {
                FengGameManagerMKII.currentScriptLogic = string.Empty;
            }

        }
        #endregion
    }
}
