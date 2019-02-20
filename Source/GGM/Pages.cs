using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GGM
{
    internal class Pages : MonoBehaviour
    {
        #region Variables

        private static readonly float _leftPos = ((Screen.width) / 2f) - 350f;
        private static readonly float _topPos = ((Screen.height) / 2f) - 250f;
        private const float _width = 730f;
        private const float _height = 550f;
        private static readonly Rect _full = new Rect(_leftPos, _topPos - 25f, _height, _width);
        private static readonly Rect _topCenter = new Rect(_leftPos, _topPos + 35f, 730f, 35f);
        private static readonly Rect _topLeft = new Rect(_leftPos + 20f, _topPos + 75f, 355f, 500 - 95f);
        private static readonly Rect _topRight = new Rect(_leftPos + 380f, _topPos + 75f, 355f, 500 - 95f);
        private static readonly Rect _left = new Rect(_leftPos + 20f, _topPos + 35f, 355f, 500f - 55f);
        private static readonly Rect _right = new Rect(_leftPos + 380f, _topPos + 35f, 355f, 500f - 55f);

        private const string _size = "72";
        private const string _color1 = "D6B1DE";
        private const string _color2 = "FFFFFF";
        private static string _singleButton = "";
        private static Rect _single = GUIHelpers.AlignRect(375f, 100f, GUIHelpers.Alignment.BOTTOMCENTER, 0f, -300f);
        private static string _multiplayerButton = "";
        private static Rect _multiplayer = GUIHelpers.AlignRect(715f, 100f, GUIHelpers.Alignment.BOTTOMCENTER, 0f, -175f);
        private static string _quitButton = "";
        private static Rect _quit = GUIHelpers.AlignRect(245f, 100f, GUIHelpers.Alignment.BOTTOMCENTER, 0f, -50f);
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
        public static string[] TopNavigationPanelStr = 
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
        private static int _topLeftNavigationInt;
        private static readonly string[] _topLeftNavigationStr =
        {
            BetterGUI.ButtonStyle("<size=18>User</size>"),
            BetterGUI.ButtonStyle("<size=18>Servers</size>")
        };
        private static readonly string[] _switcherStr =
        {
            BetterGUI.ButtonStyle("Off"),
            BetterGUI.ButtonStyle("On")
        };
        private static readonly string[] _speedometer =
        {
            BetterGUI.ButtonStyle("Off"),
            BetterGUI.ButtonStyle("Speed"),
            BetterGUI.ButtonStyle("Damage")
        };
        private static readonly string[] _anisotropicFilteringStr =
        {
            BetterGUI.ButtonStyle("Off"),
            BetterGUI.ButtonStyle("On"),
            BetterGUI.ButtonStyle("Forced")
        };
        private static readonly string[] _antiAliasingStr =
        {
            BetterGUI.ButtonStyle("Off"),
            BetterGUI.ButtonStyle("2x"),
            BetterGUI.ButtonStyle("4x"),
            BetterGUI.ButtonStyle("8x")
        };
        private static readonly string[] _blendWeightsStr =
        {
            BetterGUI.ButtonStyle("1"),
            BetterGUI.ButtonStyle("2"),
            BetterGUI.ButtonStyle("4")
        };
        private static readonly string[] _shadowCascadesStr =
        {
            BetterGUI.ButtonStyle("0"),
            BetterGUI.ButtonStyle("2"),
            BetterGUI.ButtonStyle("4")
        };
        private static readonly string[] _shadowProjectionStr =
        {
            BetterGUI.ButtonStyle("Close Fit"),
            BetterGUI.ButtonStyle("Stable Fit")
        };
        private static readonly string[] _textureQualityStr =
        {
            BetterGUI.ButtonStyle("Low"),
            BetterGUI.ButtonStyle("Medium"),
            BetterGUI.ButtonStyle("High")
        };
        private static readonly string[] _levelSkinPageStr =
        {
            BetterGUI.ButtonStyle("Forest"),
            BetterGUI.ButtonStyle("City")
        };
        private static readonly string[] _cannonCooldownStr =
        {
            BetterGUI.ButtonStyle("3.5"),
            BetterGUI.ButtonStyle("1"),
            BetterGUI.ButtonStyle("0.1")
        };
        private static readonly string[] _cannonRotateStr =
        {
            BetterGUI.ButtonStyle("25"),
            BetterGUI.ButtonStyle("50"),
            BetterGUI.ButtonStyle("75"),
            BetterGUI.ButtonStyle("100")
        };
        private static readonly string[] _cannonSpeedStr =
        {
            BetterGUI.ButtonStyle("50"),
            BetterGUI.ButtonStyle("75"),
            BetterGUI.ButtonStyle("100"),
            BetterGUI.ButtonStyle("200"),
            BetterGUI.ButtonStyle("500")
        };
        #endregion

        #region Pages
        public void Main_Menu()
        {
            #region Single
            if (GUI.Button(_single, _singleButton, "label"))
            {
                NGUITools.SetActive(GameObject.Find("UIRefer").GetComponent<UIMainReferences>().panelSingleSet, true);
                NGUITools.SetActive(GameObject.Find("UIRefer").GetComponent<UIMainReferences>().panelMain, false);
            }
            if (_single.Contains(GUIHelpers.mousePos))
            {
                _singleButton = "<color=#" + _color1 + "><size=" + _size + "><b><i>S I N G L E</i></b></size></color>";
            }
            else
            {
                _singleButton = "<color=#" + _color2 + "><size=" + _size + "><b><i>S I N G L E</i></b></size></color>";
            }
            #endregion

            #region Multiplayer
            if (GUI.Button(_multiplayer, _multiplayerButton, "label"))
            {
                NGUITools.SetActive(GameObject.Find("UIRefer").GetComponent<UIMainReferences>().panelMultiStart, true);
                NGUITools.SetActive(GameObject.Find("UIRefer").GetComponent<UIMainReferences>().panelMain, false);
            }
            if (_multiplayer.Contains(GUIHelpers.mousePos))
            {
                _multiplayerButton = "<color=#" + _color1 + "><size=" + _size + "><b><i>M U L T I P L A Y E R</i></b></size></color>";
            }
            else
            {
                _multiplayerButton = "<color=#" + _color2 + "><size=" + _size + "><b><i>M U L T I P L A Y E R</i></b></size></color>";
            }
            #endregion

            #region Quit
            if (GUI.Button(_quit, _quitButton, "label"))
            {
                Application.Quit();
            }
            if (_quit.Contains(GUIHelpers.mousePos))
            {
                _quitButton = "<color=#" + _color1 + "><size=" + _size + "><b><i>Q U I T</i></b></size></color>";
            }
            else
            {
                _quitButton = "<color=#" + _color2 + "><size=" + _size + "><b><i>Q U I T</i></b></size></color>";
            }
            #endregion

            #region Top Left Navigation Panel
            GUILayout.BeginArea(GUIHelpers.AlignRect(400, 400, GUIHelpers.Alignment.TOPLEFT, 10, 10));
            GUILayout.BeginHorizontal();

            _topLeftNavigationInt = GUILayout.SelectionGrid(_topLeftNavigationInt, _topLeftNavigationStr, 2, GUILayout.Width(400), GUILayout.Height(50));
            GUILayout.EndHorizontal();

            switch (_topLeftNavigationInt)
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
                    if (GUILayout.Button(BetterGUI.Bold("<size=16>Save</size>"), GUILayout.Width(147)))
                    {
                        PlayerPrefs.SetString("Name", FengGameManagerMKII.nameField);
                        PlayerPrefs.SetString("Guild", LoginFengKAI.player.guildname);
                    }
                    if (GUILayout.Button(BetterGUI.Bold("<size=16>Load</size>"), GUILayout.Width(147)))
                    {
                        FengGameManagerMKII.nameField = PlayerPrefs.GetString("Name", string.Empty);
                        LoginFengKAI.player.guildname = PlayerPrefs.GetString("Guild", string.Empty);
                    }
                    GUILayout.EndHorizontal();
                    break;
                #endregion
                case 1:
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
            else if (GUI.Button(GUIHelpers.AlignRect(128f, 25f, GUIHelpers.Alignment.TOPRIGHT, -5f, 45f), "Custom Characters"))//45f, 128,25f
            {
                Application.LoadLevel("characterCreation");
            }
            else if (GUI.Button(GUIHelpers.AlignRect(128f, 25f, GUIHelpers.Alignment.TOPRIGHT, -5f, 75f), "Snapshot Reviewer"))//75
            {
                Application.LoadLevel("SnapShot");
            }
            #endregion
        }
        public static void Game_Settings()
        {
            #region Left
            GUILayout.BeginArea(_left);
            GUILayout.BeginVertical();
            BetterGUI.Header("General");
            GameSettingsScrollLeft = GUILayout.BeginScrollView(GameSettingsScrollLeft, false, false);
            BetterGUI.SubHeader("Mouse");
            BetterGUI.Slider("Mouse Sensivity", true, 100, ref Settings.MouseSensitivity, 0.1f, 1f);
            BetterGUI.Grid("Invert Mouse", ref Settings.InvertMouse, _switcherStr);
            BetterGUI.SubHeader("Camera");
            BetterGUI.Slider("Camera Distance", true, 100, ref Settings.CameraDistance, 0f, 1f);
            BetterGUI.Grid("Camera Tilt", ref Settings.CameraTilt, _switcherStr);
            BetterGUI.Grid("Static FOV", ref Settings.FOV, _switcherStr);
            if (Settings.FOV == 1)
            {
                BetterGUI.Slider("FOV value", true, 0, ref Settings.FOVvalue, 60f, 120f);
            }
            BetterGUI.SubHeader("Snapshots");
            BetterGUI.Grid("Snapshots", ref Settings.Snapshots, _switcherStr);
            BetterGUI.Grid("Show In Game", ref Settings.SnapshotsInGame, _switcherStr);
            BetterGUI.TextField("Minimum Damage", ref Settings.SnapshotsMinDamage);
            BetterGUI.SubHeader("GUI");
            BetterGUI.Grid("UI", ref Settings.UI, _switcherStr);
            BetterGUI.Grid("Chat", ref Settings.Chat, _switcherStr);
            BetterGUI.Grid("RC Formatting", ref Settings.RCFormatting, _switcherStr);
            BetterGUI.TextField("Major Color", ref Settings.ChatMajorColor);
            BetterGUI.TextField("Minor Color", ref Settings.ChatMinorColor);
            BetterGUI.TextField("Size", ref Settings.ChatSize);
            BetterGUI.Grid("Major Bold", ref Settings.ChatMajorBold, _switcherStr);
            BetterGUI.Grid("Minor Bold", ref Settings.ChatMinorBold, _switcherStr);
            BetterGUI.Grid("Major Italic", ref Settings.ChatMajorItalic, _switcherStr);
            BetterGUI.Grid("Minor Italic", ref Settings.ChatMinorItalic, _switcherStr);
            BetterGUI.Grid("Game Feed", ref Settings.GameFeed, _switcherStr);
            BetterGUI.Grid("Damage Feed", ref Settings.DamageFeed, _switcherStr);
            BetterGUI.SubHeader("Other");
            BetterGUI.Grid("Speedometer", ref Settings.Speedometer, _speedometer);
            BetterGUI.Grid("Minimap", ref Settings.Minimap, _switcherStr);
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.EndArea();
            #endregion

            #region Right
            GUILayout.BeginArea(_right);
            GUILayout.BeginVertical();
            BetterGUI.Header("Character");
            GameSettingsScrollRight = GUILayout.BeginScrollView(GameSettingsScrollRight, false, false);
            BetterGUI.SubHeader("Physics");
            BetterGUI.Grid("Body Lean", ref Settings.BodyLean, _switcherStr);
            BetterGUI.Grid("No Gravity [F4]", ref Settings.NoGravity, _switcherStr);
            BetterGUI.Grid("No Clip [V]", ref Settings.NoClip, _switcherStr);
            BetterGUI.Grid("Bouncy", ref Settings.Bouncy, _switcherStr);
            BetterGUI.SubHeader("Gas Burst");
            BetterGUI.Grid("Rebind + Double Tap", ref Settings.DoubleBurstRebind, _switcherStr);
            BetterGUI.TextField("Force", ref Settings.DashForce);
            BetterGUI.TextField("Animation Delay", ref Settings.DashDelay);
            BetterGUI.SubHeader("Resources");
            BetterGUI.Grid("Infinite Blades", ref Settings.InfiniteBlades, _switcherStr);
            BetterGUI.Grid("Infinite Bullets", ref Settings.InfiniteBullets, _switcherStr);
            BetterGUI.Grid("Infinite Gas", ref Settings.InfiniteGas, _switcherStr);
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
            GUI.Label(new Rect(_leftPos + 200f, _topPos + 382f, 400f, 22f), "Master Client only. Changes will take effect upon restart.");
            if (GUI.Button(new Rect(_leftPos + 267.5f, _topPos + 50f, 60f, 25f), "Titans"))
            {
                FengGameManagerMKII.settings[230] = 0;
            }
            else if (GUI.Button(new Rect(_leftPos + 332.5f, _topPos + 50f, 40f, 25f), "PVP"))
            {
                FengGameManagerMKII.settings[230] = 1;
            }
            else if (GUI.Button(new Rect(_leftPos + 377.5f, _topPos + 50f, 50f, 25f), "Misc"))
            {
                FengGameManagerMKII.settings[230] = 2;
            }
            else if (GUI.Button(new Rect(_leftPos + 320f, _topPos + 415f, 60f, 30f), "Reset"))
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
                GUI.Label(new Rect(_leftPos + 100f, _topPos + 90f, 160f, 22f), "Custom Titan Number:", "Label");
                GUI.Label(new Rect(_leftPos + 100f, _topPos + 112f, 200f, 22f), "Amount (Integer):", "Label");
                FengGameManagerMKII.settings[204] = GUI.TextField(new Rect(_leftPos + 250f, _topPos + 112f, 50f, 22f), (string)FengGameManagerMKII.settings[204]);
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[203]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(_leftPos + 250f, _topPos + 90f, 40f, 20f), flag35, "On");
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
                GUI.Label(new Rect(_leftPos + 100f, _topPos + 152f, 160f, 22f), "Custom Titan Spawns:", "Label");
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[210]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(_leftPos + 250f, _topPos + 152f, 40f, 20f), flag35, "On");
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
                GUI.Label(new Rect(_leftPos + 100f, _topPos + 174f, 150f, 22f), "Normal (Decimal):", "Label");
                GUI.Label(new Rect(_leftPos + 100f, _topPos + 196f, 150f, 22f), "Aberrant (Decimal):", "Label");
                GUI.Label(new Rect(_leftPos + 100f, _topPos + 218f, 150f, 22f), "Jumper (Decimal):", "Label");
                GUI.Label(new Rect(_leftPos + 100f, _topPos + 240f, 150f, 22f), "Crawler (Decimal):", "Label");
                GUI.Label(new Rect(_leftPos + 100f, _topPos + 262f, 150f, 22f), "Punk (Decimal):", "Label");
                FengGameManagerMKII.settings[211] = GUI.TextField(new Rect(_leftPos + 250f, _topPos + 174f, 50f, 22f), (string)FengGameManagerMKII.settings[211]);
                FengGameManagerMKII.settings[212] = GUI.TextField(new Rect(_leftPos + 250f, _topPos + 196f, 50f, 22f), (string)FengGameManagerMKII.settings[212]);
                FengGameManagerMKII.settings[213] = GUI.TextField(new Rect(_leftPos + 250f, _topPos + 218f, 50f, 22f), (string)FengGameManagerMKII.settings[213]);
                FengGameManagerMKII.settings[214] = GUI.TextField(new Rect(_leftPos + 250f, _topPos + 240f, 50f, 22f), (string)FengGameManagerMKII.settings[214]);
                FengGameManagerMKII.settings[215] = GUI.TextField(new Rect(_leftPos + 250f, _topPos + 262f, 50f, 22f), (string)FengGameManagerMKII.settings[215]);
                GUI.Label(new Rect(_leftPos + 100f, _topPos + 302f, 160f, 22f), "Titan Size Mode:", "Label");
                GUI.Label(new Rect(_leftPos + 100f, _topPos + 324f, 150f, 22f), "Minimum (Decimal):", "Label");
                GUI.Label(new Rect(_leftPos + 100f, _topPos + 346f, 150f, 22f), "Maximum (Decimal):", "Label");
                FengGameManagerMKII.settings[208] = GUI.TextField(new Rect(_leftPos + 250f, _topPos + 324f, 50f, 22f), (string)FengGameManagerMKII.settings[208]);
                FengGameManagerMKII.settings[209] = GUI.TextField(new Rect(_leftPos + 250f, _topPos + 346f, 50f, 22f), (string)FengGameManagerMKII.settings[209]);
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[207]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(_leftPos + 250f, _topPos + 302f, 40f, 20f), flag35, "On");
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
                GUI.Label(new Rect(_leftPos + 400f, _topPos + 90f, 160f, 22f), "Titan Health Mode:", "Label");
                GUI.Label(new Rect(_leftPos + 400f, _topPos + 161f, 150f, 22f), "Minimum (Integer):", "Label");
                GUI.Label(new Rect(_leftPos + 400f, _topPos + 183f, 150f, 22f), "Maximum (Integer):", "Label");
                FengGameManagerMKII.settings[198] = GUI.TextField(new Rect(_leftPos + 550f, _topPos + 161f, 50f, 22f), (string)FengGameManagerMKII.settings[198]);
                FengGameManagerMKII.settings[199] = GUI.TextField(new Rect(_leftPos + 550f, _topPos + 183f, 50f, 22f), (string)FengGameManagerMKII.settings[199]);
                strArray16 = new[] { "Off", "Fixed", "Scaled" };
                FengGameManagerMKII.settings[197] = GUI.SelectionGrid(new Rect(_leftPos + 550f, _topPos + 90f, 100f, 66f), (int)FengGameManagerMKII.settings[197], strArray16, 1, GUI.skin.toggle);
                GUI.Label(new Rect(_leftPos + 400f, _topPos + 223f, 160f, 22f), "Titan Damage Mode:", "Label");
                GUI.Label(new Rect(_leftPos + 400f, _topPos + 245f, 150f, 22f), "Damage (Integer):", "Label");
                FengGameManagerMKII.settings[206] = GUI.TextField(new Rect(_leftPos + 550f, _topPos + 245f, 50f, 22f), (string)FengGameManagerMKII.settings[206]);
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[205]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(_leftPos + 550f, _topPos + 223f, 40f, 20f), flag35, "On");
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
                GUI.Label(new Rect(_leftPos + 400f, _topPos + 285f, 160f, 22f), "Titan Explode Mode:", "Label");
                GUI.Label(new Rect(_leftPos + 400f, _topPos + 307f, 160f, 22f), "Radius (Integer):", "Label");
                FengGameManagerMKII.settings[196] = GUI.TextField(new Rect(_leftPos + 550f, _topPos + 307f, 50f, 22f), (string)FengGameManagerMKII.settings[196]);
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[195]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(_leftPos + 550f, _topPos + 285f, 40f, 20f), flag35, "On");
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
                GUI.Label(new Rect(_leftPos + 400f, _topPos + 347f, 160f, 22f), "Disable Rock Throwing:", "Label");
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[194]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(_leftPos + 550f, _topPos + 347f, 40f, 20f), flag35, "On");
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
                GUI.Label(new Rect(_leftPos + 100f, _topPos + 90f, 160f, 22f), "Point Mode:", "Label");
                GUI.Label(new Rect(_leftPos + 100f, _topPos + 112f, 160f, 22f), "Max Points (Integer):", "Label");
                FengGameManagerMKII.settings[227] = GUI.TextField(new Rect(_leftPos + 250f, _topPos + 112f, 50f, 22f), (string)FengGameManagerMKII.settings[227]);
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[226]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(_leftPos + 250f, _topPos + 90f, 40f, 20f), flag35, "On");
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
                GUI.Label(new Rect(_leftPos + 100f, _topPos + 152f, 160f, 22f), "PVP Bomb Mode:", "Label");
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[192]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(_leftPos + 250f, _topPos + 152f, 40f, 20f), flag35, "On");
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
                GUI.Label(new Rect(_leftPos + 100f, _topPos + 182f, 100f, 66f), "Team Mode:", "Label");
                strArray16 = new[] { "Off", "No Sort", "Size-Lock", "Skill-Lock" };
                FengGameManagerMKII.settings[193] = GUI.SelectionGrid(new Rect(_leftPos + 250f, _topPos + 182f, 120f, 88f), (int)FengGameManagerMKII.settings[193], strArray16, 1, GUI.skin.toggle);
                GUI.Label(new Rect(_leftPos + 100f, _topPos + 278f, 160f, 22f), "Infection Mode:", "Label");
                GUI.Label(new Rect(_leftPos + 100f, _topPos + 300f, 160f, 22f), "Starting Titans (Integer):", "Label");
                FengGameManagerMKII.settings[201] = GUI.TextField(new Rect(_leftPos + 250f, _topPos + 300f, 50f, 22f), (string)FengGameManagerMKII.settings[201]);
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[200]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(_leftPos + 250f, _topPos + 278f, 40f, 20f), flag35, "On");
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
                GUI.Label(new Rect(_leftPos + 100f, _topPos + 330f, 160f, 22f), "Friendly Mode:", "Label");
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[219]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(_leftPos + 250f, _topPos + 330f, 40f, 20f), flag35, "On");
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
                GUI.Label(new Rect(_leftPos + 400f, _topPos + 90f, 160f, 22f), "Sword/AHSS PVP:", "Label");
                strArray16 = new[] { "Off", "Teams", "FFA" };
                FengGameManagerMKII.settings[220] = GUI.SelectionGrid(new Rect(_leftPos + 550f, _topPos + 90f, 100f, 66f), (int)FengGameManagerMKII.settings[220], strArray16, 1, GUI.skin.toggle);
                GUI.Label(new Rect(_leftPos + 400f, _topPos + 164f, 160f, 22f), "No AHSS Air-Reloading:", "Label");
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[228]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(_leftPos + 550f, _topPos + 164f, 40f, 20f), flag35, "On");
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
                GUI.Label(new Rect(_leftPos + 400f, _topPos + 194f, 160f, 22f), "Cannons kill humans:", "Label");
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[261]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(_leftPos + 550f, _topPos + 194f, 40f, 20f), flag35, "On");
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
                GUI.Label(new Rect(_leftPos + 100f, _topPos + 90f, 160f, 22f), "Custom Titans/Wave:", "Label");
                GUI.Label(new Rect(_leftPos + 100f, _topPos + 112f, 160f, 22f), "Amount (Integer):", "Label");
                FengGameManagerMKII.settings[218] = GUI.TextField(new Rect(_leftPos + 250f, _topPos + 112f, 50f, 22f), (string)FengGameManagerMKII.settings[218]);
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[217]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(_leftPos + 250f, _topPos + 90f, 40f, 20f), flag35, "On");
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
                GUI.Label(new Rect(_leftPos + 100f, _topPos + 152f, 160f, 22f), "Maximum Waves:", "Label");
                GUI.Label(new Rect(_leftPos + 100f, _topPos + 174f, 160f, 22f), "Amount (Integer):", "Label");
                FengGameManagerMKII.settings[222] = GUI.TextField(new Rect(_leftPos + 250f, _topPos + 174f, 50f, 22f), (string)FengGameManagerMKII.settings[222]);
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[221]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(_leftPos + 250f, _topPos + 152f, 40f, 20f), flag35, "On");
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
                GUI.Label(new Rect(_leftPos + 100f, _topPos + 214f, 160f, 22f), "Punks every 5 waves:", "Label");
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[229]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(_leftPos + 250f, _topPos + 214f, 40f, 20f), flag35, "On");
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
                GUI.Label(new Rect(_leftPos + 100f, _topPos + 244f, 160f, 22f), "Global Minimap Disable:", "Label");
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[235]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(_leftPos + 250f, _topPos + 274f, 40f, 20f), flag35, "On");
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
                GUI.Label(new Rect(_leftPos + 400f, _topPos + 90f, 160f, 22f), "Endless Respawn:", "Label");
                GUI.Label(new Rect(_leftPos + 400f, _topPos + 112f, 160f, 22f), "Respawn Time (Integer):", "Label");
                FengGameManagerMKII.settings[224] = GUI.TextField(new Rect(_leftPos + 550f, _topPos + 112f, 50f, 22f), (string)FengGameManagerMKII.settings[224]);
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[223]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(_leftPos + 550f, _topPos + 90f, 40f, 20f), flag35, "On");
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
                GUI.Label(new Rect(_leftPos + 400f, _topPos + 152f, 160f, 22f), "Kick Eren Titan:", "Label");
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[202]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(_leftPos + 550f, _topPos + 152f, 40f, 20f), flag35, "On");
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
                GUI.Label(new Rect(_leftPos + 400f, _topPos + 182f, 160f, 22f), "Allow Horses:", "Label");
                flag35 = false;
                if (((int)FengGameManagerMKII.settings[216]) == 1)
                {
                    flag35 = true;
                }
                flag36 = GUI.Toggle(new Rect(_leftPos + 550f, _topPos + 182f, 40f, 20f), flag35, "On");
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
                GUI.Label(new Rect(_leftPos + 400f, _topPos + 212f, 180f, 22f), "Message of the day:", "Label");
                FengGameManagerMKII.settings[225] = GUI.TextField(new Rect(_leftPos + 400f, _topPos + 234f, 200f, 22f), (string)FengGameManagerMKII.settings[225]);
            }
        }
        public static void Video_and_Audio()
        {
            #region Left
            GUILayout.BeginArea(_left);
            GUILayout.BeginVertical();

            BetterGUI.Header("Video");
            VideoScroll = GUILayout.BeginScrollView(VideoScroll, false, false);
            BetterGUI.SubHeader("General");
            GUILayout.BeginHorizontal();
            BetterGUI.Label("Quality");
            Settings.OverallQuality = GUILayout.HorizontalSlider(Settings.OverallQuality, 0f, 5f, GUILayout.Width(125));
            BetterGUI.Label(QualitySettings.names[QualitySettings.GetQualityLevel()], 1, 60f);
            GUILayout.EndHorizontal();
            BetterGUI.Grid("Textures", ref Settings.TextureQuality, _textureQualityStr);
            BetterGUI.Grid("FPS", ref Settings.FPS, _switcherStr);
            BetterGUI.TextField("FPS Lock", ref Settings.FPSLock);
            BetterGUI.SubHeader("Advanced");
            var mipmap = Settings.MipMapping == 1 ? 1 : 0;
            BetterGUI.Grid("MIP Mapping", ref Settings.MipMapping, _switcherStr);
            FengGameManagerMKII.settings[63] = Settings.MipMapping == 0 ? 1 : 0;
            if (Settings.MipMapping != mipmap)
            {
                FengGameManagerMKII.linkHash[0].Clear();
                FengGameManagerMKII.linkHash[1].Clear();
                FengGameManagerMKII.linkHash[2].Clear();
            }
            BetterGUI.Grid("Anisotropic Filtering", ref Settings.AnisotropicFiltering, _anisotropicFilteringStr);
            BetterGUI.Grid("Anti-Aliasing", ref Settings.AntiAliasing, _antiAliasingStr);
            BetterGUI.Grid("Blend Weights", ref Settings.BlendWeights, _blendWeightsStr);
            BetterGUI.Slider("LOD Level", true, 0, ref Settings.MaximumLODLevel, 0f, 7f);
            BetterGUI.Slider("LOD Bias", false, 0, ref Settings.LODBias, 0f, 2f, "0.#");
            BetterGUI.Slider("Draw Distance", true, 100, ref Settings.DrawDistance, 15f, 50f);
            BetterGUI.Slider("Shadow Distance", true, 100, ref Settings.ShadowDistance, 0f, 50f);
            BetterGUI.Grid("Shadow Projection", ref Settings.ShadowProjection, _shadowProjectionStr);
            BetterGUI.Grid("Shadow Cascades", ref Settings.ShadowCascades, _shadowCascadesStr);
            //Ambient
            BetterGUI.Grid("Ambient", ref Settings.Ambient, _switcherStr);
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
            BetterGUI.Grid("Fog", ref Settings.Fog, _switcherStr);
            if (Settings.Fog == 1)
            {
                BetterGUI.Slider("Color R:", false, 0, ref Settings.FogColorR, 0f, 1f, "0.###", 160f, 25f);
                BetterGUI.Slider("Color G:", false, 0, ref Settings.FogColorG, 0f, 1f, "0.###", 160f, 25f);
                BetterGUI.Slider("Color B:", false, 0, ref Settings.FogColorB, 0f, 1f, "0.###", 160f, 25f);
                BetterGUI.Slider("Start Distance", true, 0, ref Settings.FogStartDistance, 0f, 1000f, "0", 160f, 25f);
                BetterGUI.Slider("End Distance", true, 0, ref Settings.FogEndDistance, 0f, 1000f, "0", 160f, 25f);
            }
            BetterGUI.Grid("Wind", ref Settings.Wind, _switcherStr);
            BetterGUI.Grid("Blur", ref Settings.Blur, _switcherStr);
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.EndArea();
            #endregion

            #region Right
            GUILayout.BeginArea(_right);
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

            if (GUI.Button(new Rect(_leftPos + 233f, _topPos + 51f, 55f, 25f), "Human"))
            {
                FengGameManagerMKII.settings[190] = 0;
            }
            else if (GUI.Button(new Rect(_leftPos + 293f, _topPos + 51f, 52f, 25f), "Titan"))
            {
                FengGameManagerMKII.settings[190] = 1;
            }
            else if (GUI.Button(new Rect(_leftPos + 350f, _topPos + 51f, 53f, 25f), "Horse"))
            {
                FengGameManagerMKII.settings[190] = 2;
            }
            else if (GUI.Button(new Rect(_leftPos + 408f, _topPos + 51f, 59f, 25f), "Cannon"))
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
                    GUI.Label(new Rect(_leftPos + k, (_topPos + 86f) + (j * 25f), 145f, 22f), list7[i], "Label");
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
                bool flag40 = GUI.Toggle(new Rect(_leftPos + 457f, _topPos + 261f, 40f, 20f), flag37, "On");
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
                bool flag41 = GUI.Toggle(new Rect(_leftPos + 457f, _topPos + 286f, 40f, 20f), flag38, "On");
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
                bool flag42 = GUI.Toggle(new Rect(_leftPos + 457f, _topPos + 311f, 40f, 20f), flag39, "On");
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
                    if (GUI.Button(new Rect(_leftPos + k, (_topPos + 86f) + (j * 25f), 120f, 20f), FengGameManagerMKII.inputManager.getKeyRC(i), "box"))
                    {
                        FengGameManagerMKII.settings[100] = i + 1;
                        FengGameManagerMKII.inputManager.setNameRC(i, "waiting...");
                    }
                }
                if (GUI.Button(new Rect(_leftPos + 500f, _topPos + 261f, 120f, 20f), (string)FengGameManagerMKII.settings[98], "box"))
                {
                    FengGameManagerMKII.settings[98] = "waiting...";
                    FengGameManagerMKII.settings[100] = 98;
                }
                else if (GUI.Button(new Rect(_leftPos + 500f, _topPos + 286f, 120f, 20f), (string)FengGameManagerMKII.settings[99], "box"))
                {
                    FengGameManagerMKII.settings[99] = "waiting...";
                    FengGameManagerMKII.settings[100] = 99;
                }
                else if (GUI.Button(new Rect(_leftPos + 500f, _topPos + 311f, 120f, 20f), (string)FengGameManagerMKII.settings[182], "box"))
                {
                    FengGameManagerMKII.settings[182] = "waiting...";
                    FengGameManagerMKII.settings[100] = 182;
                }
                else if (GUI.Button(new Rect(_leftPos + 500f, _topPos + 336f, 120f, 20f), (string)FengGameManagerMKII.settings[232], "box"))
                {
                    FengGameManagerMKII.settings[232] = "waiting...";
                    FengGameManagerMKII.settings[100] = 232;
                }
                else if (GUI.Button(new Rect(_leftPos + 500f, _topPos + 361f, 120f, 20f), (string)FengGameManagerMKII.settings[233], "box"))
                {
                    FengGameManagerMKII.settings[233] = "waiting...";
                    FengGameManagerMKII.settings[100] = 233;
                }
                else if (GUI.Button(new Rect(_leftPos + 500f, _topPos + 386f, 120f, 20f), (string)FengGameManagerMKII.settings[234], "box"))
                {
                    FengGameManagerMKII.settings[234] = "waiting...";
                    FengGameManagerMKII.settings[100] = 234;
                }
                else if (GUI.Button(new Rect(_leftPos + 500f, _topPos + 411f, 120f, 20f), (string)FengGameManagerMKII.settings[236], "box"))
                {
                    FengGameManagerMKII.settings[236] = "waiting...";
                    FengGameManagerMKII.settings[100] = 236;
                }
                else if (GUI.Button(new Rect(_leftPos + 500f, _topPos + 436f, 120f, 20f), (string)FengGameManagerMKII.settings[262], "box"))
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
                    GUI.Label(new Rect(_leftPos + k, (_topPos + 86f) + (j * 25f), 145f, 22f), list7[i], "Label");
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
                    if (GUI.Button(new Rect(_leftPos + k, (_topPos + 86f) + (j * 25f), 120f, 20f), (string)FengGameManagerMKII.settings[num23], "box"))
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
                    GUI.Label(new Rect(_leftPos + k, (_topPos + 86f) + (j * 25f), 145f, 22f), list7[i], "Label");
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
                    if (GUI.Button(new Rect(_leftPos + k, (_topPos + 86f) + (j * 25f), 120f, 20f), (string)FengGameManagerMKII.settings[num23], "box"))
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
                    GUI.Label(new Rect(_leftPos + k, (_topPos + 86f) + (j * 25f), 145f, 22f), list7[i], "Label");
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
                    if (GUI.Button(new Rect(_leftPos + k, (_topPos + 86f) + (j * 25f), 120f, 20f), (string)FengGameManagerMKII.settings[num23], "box"))
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
            GUI.Label(new Rect(_leftPos + 150f, _topPos + 80f, 185f, 22f), "Bomb Mode", "Label");
            GUI.Label(new Rect(_leftPos + 80f, _topPos + 110f, 80f, 22f), "Color:", "Label");
            var textured = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            textured.SetPixel(0, 0, new Color((float)FengGameManagerMKII.settings[246], (float)FengGameManagerMKII.settings[247], (float)FengGameManagerMKII.settings[248], (float)FengGameManagerMKII.settings[249]));
            textured.Apply();
            GUI.DrawTexture(new Rect(_leftPos + 120f, _topPos + 113f, 40f, 15f), textured, ScaleMode.StretchToFill);
            Destroy(textured);
            GUI.Label(new Rect(_leftPos + 72f, _topPos + 135f, 20f, 22f), "R:", "Label");
            GUI.Label(new Rect(_leftPos + 72f, _topPos + 160f, 20f, 22f), "G:", "Label");
            GUI.Label(new Rect(_leftPos + 72f, _topPos + 185f, 20f, 22f), "B:", "Label");
            GUI.Label(new Rect(_leftPos + 72f, _topPos + 210f, 20f, 22f), "A:", "Label");
            FengGameManagerMKII.settings[246] = GUI.HorizontalSlider(new Rect(_leftPos + 92f, _topPos + 138f, 100f, 20f), (float)FengGameManagerMKII.settings[246], 0f, 1f);
            FengGameManagerMKII.settings[247] = GUI.HorizontalSlider(new Rect(_leftPos + 92f, _topPos + 163f, 100f, 20f), (float)FengGameManagerMKII.settings[247], 0f, 1f);
            FengGameManagerMKII.settings[248] = GUI.HorizontalSlider(new Rect(_leftPos + 92f, _topPos + 188f, 100f, 20f), (float)FengGameManagerMKII.settings[248], 0f, 1f);
            FengGameManagerMKII.settings[249] = GUI.HorizontalSlider(new Rect(_leftPos + 92f, _topPos + 213f, 100f, 20f), (float)FengGameManagerMKII.settings[249], 0.5f, 1f);
            GUI.Label(new Rect(_leftPos + 72f, _topPos + 235f, 95f, 22f), "Bomb Radius:", "Label");
            GUI.Label(new Rect(_leftPos + 72f, _topPos + 260f, 95f, 22f), "Bomb Range:", "Label");
            GUI.Label(new Rect(_leftPos + 72f, _topPos + 285f, 95f, 22f), "Bomb Speed:", "Label");
            GUI.Label(new Rect(_leftPos + 72f, _topPos + 310f, 95f, 22f), "Bomb CD:", "Label");
            GUI.Label(new Rect(_leftPos + 72f, _topPos + 335f, 95f, 22f), "Unused Points:", "Label");
            var num30 = (int)FengGameManagerMKII.settings[250];
            GUI.Label(new Rect(_leftPos + 168f, _topPos + 235f, 20f, 22f), num30.ToString(), "Label");
            num30 = (int)FengGameManagerMKII.settings[251];
            GUI.Label(new Rect(_leftPos + 168f, _topPos + 260f, 20f, 22f), num30.ToString(), "Label");
            num30 = (int)FengGameManagerMKII.settings[252];
            GUI.Label(new Rect(_leftPos + 168f, _topPos + 285f, 20f, 22f), num30.ToString(), "Label");
            GUI.Label(new Rect(_leftPos + 168f, _topPos + 310f, 20f, 22f), ((int)FengGameManagerMKII.settings[253]).ToString(), "Label");
            int num43 = (((20 - ((int)FengGameManagerMKII.settings[250])) - ((int)FengGameManagerMKII.settings[251])) - ((int)FengGameManagerMKII.settings[252])) - ((int)FengGameManagerMKII.settings[253]);
            GUI.Label(new Rect(_leftPos + 168f, _topPos + 335f, 20f, 22f), num43.ToString(), "Label");
            if (GUI.Button(new Rect(_leftPos + 190f, _topPos + 235f, 20f, 20f), "-"))
            {
                if (((int)FengGameManagerMKII.settings[250]) > 0)
                {
                    FengGameManagerMKII.settings[250] = ((int)FengGameManagerMKII.settings[250]) - 1;
                }
            }
            else if (GUI.Button(new Rect(_leftPos + 215f, _topPos + 235f, 20f, 20f), "+") && ((((int)FengGameManagerMKII.settings[250]) < 10) && (num43 > 0)))
            {
                FengGameManagerMKII.settings[250] = ((int)FengGameManagerMKII.settings[250]) + 1;
            }
            if (GUI.Button(new Rect(_leftPos + 190f, _topPos + 260f, 20f, 20f), "-"))
            {
                if (((int)FengGameManagerMKII.settings[251]) > 0)
                {
                    FengGameManagerMKII.settings[251] = ((int)FengGameManagerMKII.settings[251]) - 1;
                }
            }
            else if (GUI.Button(new Rect(_leftPos + 215f, _topPos + 260f, 20f, 20f), "+") && ((((int)FengGameManagerMKII.settings[251]) < 10) && (num43 > 0)))
            {
                FengGameManagerMKII.settings[251] = ((int)FengGameManagerMKII.settings[251]) + 1;
            }
            if (GUI.Button(new Rect(_leftPos + 190f, _topPos + 285f, 20f, 20f), "-"))
            {
                if (((int)FengGameManagerMKII.settings[252]) > 0)
                {
                    FengGameManagerMKII.settings[252] = ((int)FengGameManagerMKII.settings[252]) - 1;
                }
            }
            else if (GUI.Button(new Rect(_leftPos + 215f, _topPos + 285f, 20f, 20f), "+") && ((((int)FengGameManagerMKII.settings[252]) < 10) && (num43 > 0)))
            {
                FengGameManagerMKII.settings[252] = ((int)FengGameManagerMKII.settings[252]) + 1;
            }
            if (GUI.Button(new Rect(_leftPos + 190f, _topPos + 310f, 20f, 20f), "-"))
            {
                if (((int)FengGameManagerMKII.settings[253]) > 0)
                {
                    FengGameManagerMKII.settings[253] = ((int)FengGameManagerMKII.settings[253]) - 1;
                }
            }
            else if (GUI.Button(new Rect(_leftPos + 215f, _topPos + 310f, 20f, 20f), "+") && ((((int)FengGameManagerMKII.settings[253]) < 10) && (num43 > 0)))
            {
                FengGameManagerMKII.settings[253] = ((int)FengGameManagerMKII.settings[253]) + 1;
            }
        }
        public static void Human_Skins()
        {
            GUILayout.BeginArea(_left);
            GUILayout.BeginVertical();
            BetterGUI.Header("Settings");
            BetterGUI.SubHeader("General");
            BetterGUI.Grid("Human Skins", ref Settings.HumanSkins, _switcherStr);
            BetterGUI.Grid("Blade Trail:", ref Settings.BladeTrails, _switcherStr);
            BetterGUI.Grid("Custom Gas:", ref Settings.CustomGas, _switcherStr);
            BetterGUI.SubHeader("Advanced");
            BetterGUI.Grid("HD Trails:", ref Settings.BladeTrailsQuality, _switcherStr);
            BetterGUI.Slider("Blade Trail Frame Rate:", true, 0, ref Settings.BladeTrailsFrameRate, 60f, 1000f);
            BetterGUI.TextField("Blade Trail Fade Time:", ref Settings.BladeTrailsFadeTime);
            GUILayout.EndVertical();
            GUILayout.EndArea();

            GUILayout.BeginArea(_right);
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
        }
        public static void Titan_Skins()
        {
            int num45;
            int num46;
            GUI.Label(new Rect(_leftPos + 270f, _topPos + 52f, 120f, 30f), "Titan Skin Mode:", "Label");
            var flag6 = Settings.TitanSkins == 1;
            var flag11 = GUI.Toggle(new Rect(_leftPos + 390f, _topPos + 52f, 40f, 20f), flag6, "On");
            if (flag6 != flag11)
            {
                Settings.TitanSkins = flag11 ? 1 : 0;
            }
            GUI.Label(new Rect(_leftPos + 270f, _topPos + 77f, 120f, 30f), "Randomized Pairs:", "Label");
            flag6 = (int)FengGameManagerMKII.settings[32] == 1;
            flag11 = GUI.Toggle(new Rect(_leftPos + 390f, _topPos + 77f, 40f, 20f), flag6, "On");
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
            GUI.Label(new Rect(_leftPos + 158f, _topPos + 112f, 150f, 20f), "Titan Hair:", "Label");
            FengGameManagerMKII.settings[21] = GUI.TextField(new Rect(_leftPos + 80f, _topPos + 134f, 165f, 20f), (string)FengGameManagerMKII.settings[21]);
            FengGameManagerMKII.settings[22] = GUI.TextField(new Rect(_leftPos + 80f, _topPos + 156f, 165f, 20f), (string)FengGameManagerMKII.settings[22]);
            FengGameManagerMKII.settings[23] = GUI.TextField(new Rect(_leftPos + 80f, _topPos + 178f, 165f, 20f), (string)FengGameManagerMKII.settings[23]);
            FengGameManagerMKII.settings[24] = GUI.TextField(new Rect(_leftPos + 80f, _topPos + 200f, 165f, 20f), (string)FengGameManagerMKII.settings[24]);
            FengGameManagerMKII.settings[25] = GUI.TextField(new Rect(_leftPos + 80f, _topPos + 222f, 165f, 20f), (string)FengGameManagerMKII.settings[25]);
            if (GUI.Button(new Rect(_leftPos + 250f, _topPos + 134f, 60f, 20f), FengGameManagerMKII.hairtype((int)FengGameManagerMKII.settings[16])))
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
            else if (GUI.Button(new Rect(_leftPos + 250f, _topPos + 156f, 60f, 20f), FengGameManagerMKII.hairtype((int)FengGameManagerMKII.settings[17])))
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
            else if (GUI.Button(new Rect(_leftPos + 250f, _topPos + 178f, 60f, 20f), FengGameManagerMKII.hairtype((int)FengGameManagerMKII.settings[18])))
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
            else if (GUI.Button(new Rect(_leftPos + 250f, _topPos + 200f, 60f, 20f), FengGameManagerMKII.hairtype((int)FengGameManagerMKII.settings[19])))
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
            else if (GUI.Button(new Rect(_leftPos + 250f, _topPos + 222f, 60f, 20f), FengGameManagerMKII.hairtype((int)FengGameManagerMKII.settings[20])))
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
            GUI.Label(new Rect(_leftPos + 158f, _topPos + 252f, 150f, 20f), "Titan Eye:", "Label");
            FengGameManagerMKII.settings[26] = GUI.TextField(new Rect(_leftPos + 80f, _topPos + 274f, 230f, 20f), (string)FengGameManagerMKII.settings[26]);
            FengGameManagerMKII.settings[27] = GUI.TextField(new Rect(_leftPos + 80f, _topPos + 296f, 230f, 20f), (string)FengGameManagerMKII.settings[27]);
            FengGameManagerMKII.settings[28] = GUI.TextField(new Rect(_leftPos + 80f, _topPos + 318f, 230f, 20f), (string)FengGameManagerMKII.settings[28]);
            FengGameManagerMKII.settings[29] = GUI.TextField(new Rect(_leftPos + 80f, _topPos + 340f, 230f, 20f), (string)FengGameManagerMKII.settings[29]);
            FengGameManagerMKII.settings[30] = GUI.TextField(new Rect(_leftPos + 80f, _topPos + 362f, 230f, 20f), (string)FengGameManagerMKII.settings[30]);
            GUI.Label(new Rect(_leftPos + 455f, _topPos + 112f, 150f, 20f), "Titan Body:", "Label");
            FengGameManagerMKII.settings[86] = GUI.TextField(new Rect(_leftPos + 390f, _topPos + 134f, 230f, 20f), (string)FengGameManagerMKII.settings[86]);
            FengGameManagerMKII.settings[87] = GUI.TextField(new Rect(_leftPos + 390f, _topPos + 156f, 230f, 20f), (string)FengGameManagerMKII.settings[87]);
            FengGameManagerMKII.settings[88] = GUI.TextField(new Rect(_leftPos + 390f, _topPos + 178f, 230f, 20f), (string)FengGameManagerMKII.settings[88]);
            FengGameManagerMKII.settings[89] = GUI.TextField(new Rect(_leftPos + 390f, _topPos + 200f, 230f, 20f), (string)FengGameManagerMKII.settings[89]);
            FengGameManagerMKII.settings[90] = GUI.TextField(new Rect(_leftPos + 390f, _topPos + 222f, 230f, 20f), (string)FengGameManagerMKII.settings[90]);
            GUI.Label(new Rect(_leftPos + 472f, _topPos + 252f, 150f, 20f), "Eren:", "Label");
            FengGameManagerMKII.settings[65] = GUI.TextField(new Rect(_leftPos + 390f, _topPos + 274f, 230f, 20f), (string)FengGameManagerMKII.settings[65]);
            GUI.Label(new Rect(_leftPos + 470f, _topPos + 296f, 150f, 20f), "Annie:", "Label");
            FengGameManagerMKII.settings[66] = GUI.TextField(new Rect(_leftPos + 390f, _topPos + 318f, 230f, 20f), (string)FengGameManagerMKII.settings[66]);
            GUI.Label(new Rect(_leftPos + 465f, _topPos + 340f, 150f, 20f), "Colossal:", "Label");
            FengGameManagerMKII.settings[67] = GUI.TextField(new Rect(_leftPos + 390f, _topPos + 362f, 230f, 20f), (string)FengGameManagerMKII.settings[67]);
        }
        public static void Level_Skins()
        {
            var levelSkinPage = 0;
            if (levelSkinPage == 0)
            {
                GUILayout.BeginArea(_left);
                GUILayout.BeginVertical();
                LevelSkinForestScrollLeft = GUILayout.BeginScrollView(LevelSkinForestScrollLeft, false, false);

                BetterGUI.Header("Forest");
                BetterGUI.SubHeader("Settings");
                BetterGUI.Grid("Level Skins:", ref Settings.LocationSkins, _switcherStr);
                BetterGUI.Grid("Map:", ref levelSkinPage, _levelSkinPageStr);
                BetterGUI.Grid("Randomized Pairs:", ref Settings.ForestRandomizedPairs, _switcherStr);
                BetterGUI.Grid("Particles", ref Settings.Particles, _switcherStr);
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

                GUILayout.BeginArea(_right);
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
                    Settings.ForestAmbient[Settings.ForestCurrentSkin], _switcherStr, 2,
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
                    Settings.ForestFog[Settings.ForestCurrentSkin], _switcherStr, 2, GUILayout.Width(190f));
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
                GUILayout.BeginArea(_left);
                GUILayout.BeginVertical();
                LevelSkinCityScrollLeft = GUILayout.BeginScrollView(LevelSkinCityScrollLeft, false, false);

                BetterGUI.Header("City");
                BetterGUI.SubHeader("Settings");
                BetterGUI.Grid("Level Skins:", ref Settings.LocationSkins, _switcherStr);
                BetterGUI.Grid("Map:", ref levelSkinPage, _levelSkinPageStr);
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

                GUILayout.BeginArea(_right);
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
                    Settings.CityAmbient[Settings.CityCurrentSkin], _switcherStr, 2,
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
                Settings.CityFog[Settings.CityCurrentSkin] = GUILayout.SelectionGrid(Settings.CityFog[Settings.CityCurrentSkin], _switcherStr, 2, GUILayout.Width(190f));
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
            GUI.Label(new Rect(_leftPos + 150f, _topPos + 51f, 120f, 22f), "Map Settings", "Label");
            GUI.Label(new Rect(_leftPos + 50f, _topPos + 81f, 140f, 20f), "Titan Spawn Cap:", "Label");
            FengGameManagerMKII.settings[85] = GUI.TextField(new Rect(_leftPos + 155f, _topPos + 81f, 30f, 20f), (string)FengGameManagerMKII.settings[85]);
            var strArray16 = new[] { "1 Round", "Waves", "PVP", "Racing", "Custom" };
            RCSettings.gameType = GUI.SelectionGrid(new Rect(_leftPos + 190f, _topPos + 80f, 140f, 60f), RCSettings.gameType, strArray16, 2, GUI.skin.toggle);
            GUI.Label(new Rect(_leftPos + 150f, _topPos + 155f, 150f, 20f), "Level Script:", "Label");
            FengGameManagerMKII.currentScript = GUI.TextField(new Rect(_leftPos + 50f, _topPos + 180f, 275f, 220f), FengGameManagerMKII.currentScript);
            if (GUI.Button(new Rect(_leftPos + 100f, _topPos + 410f, 50f, 25f), "Copy"))
            {
                var editor = new TextEditor
                {
                    content = new GUIContent(FengGameManagerMKII.currentScript)
                };
                editor.SelectAll();
                editor.Copy();
            }
            else if (GUI.Button(new Rect(_leftPos + 225f, _topPos + 410f, 50f, 25f), "Clear"))
            {
                FengGameManagerMKII.currentScript = string.Empty;
            }
            GUI.Label(new Rect(_leftPos + 455f, _topPos + 51f, 180f, 20f), "Custom Textures", "Label");
            GUI.Label(new Rect(_leftPos + 375f, _topPos + 81f, 180f, 20f), "Ground Skin:", "Label");
            FengGameManagerMKII.settings[162] = GUI.TextField(new Rect(_leftPos + 375f, _topPos + 103f, 275f, 20f), (string)FengGameManagerMKII.settings[162]);
            GUI.Label(new Rect(_leftPos + 375f, _topPos + 125f, 150f, 20f), "Skybox Front:", "Label");
            FengGameManagerMKII.settings[175] = GUI.TextField(new Rect(_leftPos + 375f, _topPos + 147f, 275f, 20f), (string)FengGameManagerMKII.settings[175]);
            GUI.Label(new Rect(_leftPos + 375f, _topPos + 169f, 150f, 20f), "Skybox Back:", "Label");
            FengGameManagerMKII.settings[176] = GUI.TextField(new Rect(_leftPos + 375f, _topPos + 191f, 275f, 20f), (string)FengGameManagerMKII.settings[176]);
            GUI.Label(new Rect(_leftPos + 375f, _topPos + 213f, 150f, 20f), "Skybox Left:", "Label");
            FengGameManagerMKII.settings[177] = GUI.TextField(new Rect(_leftPos + 375f, _topPos + 235f, 275f, 20f), (string)FengGameManagerMKII.settings[177]);
            GUI.Label(new Rect(_leftPos + 375f, _topPos + 257f, 150f, 20f), "Skybox Right:", "Label");
            FengGameManagerMKII.settings[178] = GUI.TextField(new Rect(_leftPos + 375f, _topPos + 279f, 275f, 20f), (string)FengGameManagerMKII.settings[178]);
            GUI.Label(new Rect(_leftPos + 375f, _topPos + 301f, 150f, 20f), "Skybox Up:", "Label");
            FengGameManagerMKII.settings[179] = GUI.TextField(new Rect(_leftPos + 375f, _topPos + 323f, 275f, 20f), (string)FengGameManagerMKII.settings[179]);
            GUI.Label(new Rect(_leftPos + 375f, _topPos + 345f, 150f, 20f), "Skybox Down:", "Label");
            FengGameManagerMKII.settings[180] = GUI.TextField(new Rect(_leftPos + 375f, _topPos + 367f, 275f, 20f), (string)FengGameManagerMKII.settings[180]);
        }
        public static void Custom_Logic()
        {
            FengGameManagerMKII.currentScriptLogic = GUI.TextField(new Rect(_leftPos + 50f, _topPos + 82f, 600f, 270f), FengGameManagerMKII.currentScriptLogic);
            if (GUI.Button(new Rect(_leftPos + 250f, _topPos + 365f, 50f, 20f), "Copy"))
            {
                var editor = new TextEditor
                {
                    content = new GUIContent(FengGameManagerMKII.currentScriptLogic)
                };
                editor.SelectAll();
                editor.Copy();
            }
            else if (GUI.Button(new Rect(_leftPos + 400f, _topPos + 365f, 50f, 20f), "Clear"))
            {
                FengGameManagerMKII.currentScriptLogic = string.Empty;
            }

        }
        #endregion

        public static void Singleplayer()
        {
            //GUI.Window(83, new Rect());
        }
    }
}
