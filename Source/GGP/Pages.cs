using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GGP
{
    class Pages : MonoBehaviour
    {
        static int levelskinpage = 0;

        private static bool remember = false;
        private static int rememberint;
        private static bool firstlaunch = true;

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

        public static Vector2 VideoScroll = Vector2.zero;
        public static Vector2 AudioScroll = Vector2.zero;
        public static Vector2 LevelSkinForestScrollLeft = Vector2.zero;
        public static Vector2 LevelSkinForestScrollRight = Vector2.zero;
        public static Vector2 LevelSkinCityScrollLeft = Vector2.zero;
        public static Vector2 LevelSkinCityScrollRight = Vector2.zero;
        public static Vector2 GameSettingsScrollLeft = Vector2.zero;
        public static Vector2 GameSettingsScrollRight = Vector2.zero;

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

        static string SingleButton = "";
        static Rect Single = GUIHelpers.AlignRect(375f, 100f, GUIHelpers.Alignment.BOTTOMCENTER, 0f, -300f);
        static string MultiplayerButton = "";
        static Rect Multiplayer = GUIHelpers.AlignRect(715f, 100f, GUIHelpers.Alignment.BOTTOMCENTER, 0f, -175f);
        static string QuitButton = "";
        static Rect Quit = GUIHelpers.AlignRect(245f, 100f, GUIHelpers.Alignment.BOTTOMCENTER, 0f, -50f);
        static string size = "72";
        static string color1 = "D6B1DE";
        static string color2 = "FFFFFF";

        public void Main_Menu()
        {
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

            //Top Left Navigation Panel
            GUILayout.BeginArea(GUIHelpers.AlignRect(460f, 400f, GUIHelpers.Alignment.TOPLEFT, 10, 10));
            GUILayout.BeginHorizontal();

            TopLeftNavigationInt = GUILayout.SelectionGrid(TopLeftNavigationInt, TopLeftNavigationStr, 3, GUILayout.Width(460), GUILayout.Height(50));
            FengGameManagerMKII.settings[187] = TopLeftNavigationInt == 0 ? 0 : ((TopLeftNavigationInt == 1) ? 1 : 2);
            GUILayout.EndHorizontal();

            switch (FengGameManagerMKII.settings[187])
            {
                case 0:
                    {
                        //In Account
                        if (FengGameManagerMKII.loginstate == 3)
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
                        //Account
                        else
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
                            remember = GUILayout.Toggle(remember, "<size=16> Remember</size>");
                            if (remember)
                            {
                                rememberint = 1;
                            }
                            else
                            {
                                rememberint = 0;
                            }
                            PlayerPrefs.SetInt("autologin", rememberint);
                            GUILayout.EndHorizontal();
                            if (GUILayout.Button("<size=18><b>Login</b></size>", GUILayout.Width(225)) && (FengGameManagerMKII.loginstate != 1))
                            {
                                if (!remember)
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
                        break;
                    }
                case 1:
                    {
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
                    }
                case 2:
                    {
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
                    }
            }
            GUILayout.EndArea();

            if (UnityEngine.GUI.Button(GUIHelpers.AlignRect(128f, 25f, GUIHelpers.Alignment.TOPRIGHT, -5f, 15f), "Level Editor"))//15,128,25
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
        }


    }
}
