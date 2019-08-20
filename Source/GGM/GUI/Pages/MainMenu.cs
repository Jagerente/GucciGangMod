using System;
using System.Net;
using System.Reflection.Emit;
using System.Security.AccessControl;
using GGM.Caching;
using UnityEngine;
using UnityEngine.UI;
using static GGM.GUI.Elements;

namespace GGM.GUI.Pages
{
    internal class MainMenu : Page
    {
        private static int loginSwitchInt;
        private static Rect Panel = GUIHelpers.AlignRect(250f, 190f, GUIHelpers.Alignment.BOTTOMLEFT, 5, -5f);
        private static readonly Rect VersionPanel = GUIHelpers.AlignRect(300f, 20f, GUIHelpers.Alignment.BOTTOMRIGHT);
        private static float width = 250f;
        private static float height = 180f;
        private static float leftElement = width * 0.4f;
        private static float rightElement = width * 0.6f - 5f;
        private static readonly string[] leftPanelSwitcher = {"User".SetSize(24), "Servers".SetSize(24)};
        private static readonly string[] connectionProtocols = { "UDP", "TCP", "WS" };


        private void Update()
        {
            if (PhotonNetwork.connected && !FengGameManagerMKII.NeedRejoin)
            {
                PhotonNetwork.Disconnect();
            }
        }

        private void OnGUI()
        {
            if(Styles.LabelStyle == null)
            {
                return;
            }

            UnityEngine.GUI.Label(VersionPanel, "GucciGangMod " + UIMainReferences.Version, Styles.LabelStyle[2]);
            GUILayout.BeginArea(Panel);
            {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Single".SetSize(24), GUILayout.Height(50f), GUILayout.Width(250f)))
                {
                    NGUITools.SetActive(UIMainReferences.instance.panelMain.gameObject, false);
                    GetInstance<Single>().Enable();
                }

                GUILayout.Space(15f);
                if (GUILayout.Button("Multiplayer".SetSize(24), GUILayout.Height(50f), GUILayout.Width(250f)))
                {
                    NGUITools.SetActive(UIMainReferences.instance.panelMain.gameObject, false);
                    GetInstance<Multiplayer>().Enable();
                }

                GUILayout.Space(15f);
                if (GUILayout.Button("Quit".SetSize(24), GUILayout.Height(50f), GUILayout.Width(250f)))
                {
                    Application.Quit();
                }
            }
            GUILayout.EndArea();

            UnityEngine.GUI.Box(GUIHelpers.AlignRect(width + 10f, height + 10f, GUIHelpers.Alignment.TOPLEFT, 5f, 5f), ColorCache.Textures[ColorCache.PurpleMunsell]);

            GUILayout.BeginArea(GUIHelpers.AlignRect(width, height, GUIHelpers.Alignment.TOPLEFT, 10, 10));
            {
                Grid(string.Empty, ref loginSwitchInt, leftPanelSwitcher, width: width - 5f, height: 35f);

                switch (loginSwitchInt)
                {
                    case 0:
                    {
                        height = 130f + (LoginFengKAI.player.guildname == string.Empty ? 0f : LoginFengKAI.player.guildname.Split('\n').Length * Settings.SubHeaderHeight) + (FengGameManagerMKII.nameField == string.Empty ? 0 : Settings.SubHeaderHeight);

                        if (FengGameManagerMKII.nameField != string.Empty)
                        {
                            Label(FengGameManagerMKII.nameField.SetColor("FFFFFF").ToHTML(), Settings.LabelType.SubHeader, width: width);
                        }

                        if (LoginFengKAI.player.guildname != string.Empty)
                        {
                            for (var i = 0; i < LoginFengKAI.player.guildname.Split('\n').Length; i++)
                            {
                                Label(LoginFengKAI.player.guildname.Split('\n')[i].SetColor("FFFF00").ToHTML(), Settings.LabelType.SubHeader, width: width);
                            }
                        }

                        GUILayout.BeginHorizontal();
                        {
                            TextField("Name", ref FengGameManagerMKII.nameField, rightElement, labelWidth: leftElement);
                        }
                        GUILayout.EndHorizontal();

                        GUILayout.Space(Settings.space);

                        GUILayout.BeginHorizontal();
                        {
                            TextArea("Guild", ref LoginFengKAI.player.guildname, rightElement, labelWidth: leftElement);
                        }
                        GUILayout.EndHorizontal();

                        GUILayout.FlexibleSpace();

                        GUILayout.BeginHorizontal();
                        {
                            if (Button("Save", width / 2f - 5f))
                            {
                                PlayerPrefs.SetString("Name", FengGameManagerMKII.nameField);
                                PlayerPrefs.SetString("Guild", LoginFengKAI.player.guildname);
                            }

                            if (Button("Load", width / 2f - 5f))
                            {
                                FengGameManagerMKII.nameField = PlayerPrefs.GetString("Name", string.Empty);
                                LoginFengKAI.player.guildname = PlayerPrefs.GetString("Guild", string.Empty);
                            }
                        }
                        GUILayout.EndHorizontal();

                        break;
                    }

                    case 1:
                    {
                        height = 185f;
                        string server = UIMainReferences.ServerKey == UIMainReferences.PublicKey ? "Connected to Public server." : UIMainReferences.ServerKey == FengGameManagerMKII.s[0] ? "Connected to RC Private server." : FengGameManagerMKII.privateServerField == string.Empty ? "Connected to Custom server." : $"Connected to {UIMainReferences.ServerKey}.";
                        Label(server.SetColor("FFFFFF"), Settings.LabelType.SubHeader, width: width);
                        Grid("Protocol", ref Config.Settings.ConnectionProtocolSettings.Value, connectionProtocols, width: rightElement, labelWidth: leftElement);
                        GUILayout.BeginHorizontal();
                        {
                            Label("Public", width: leftElement);
                            if (Button("Connect", width: rightElement))
                            {
                                UIMainReferences.ServerKey = UIMainReferences.PublicKey;
                            }
                        }
                        GUILayout.EndHorizontal();

                        GUILayout.Space(Settings.space);

                        GUILayout.BeginHorizontal();
                        {
                            Label("RCPrivate", width: leftElement);
                            if (Button("Connect", width: rightElement))
                            {
                                UIMainReferences.ServerKey = FengGameManagerMKII.s[0];
                            }
                        }
                        GUILayout.EndHorizontal();

                        GUILayout.Space(Settings.space);

                        GUILayout.BeginHorizontal();
                        {
                            TextField(string.Empty, ref FengGameManagerMKII.privateServerField, leftElement - 10f);
                            GUILayout.Space(1f);
                            if (Button("Connect", width: rightElement))
                            {
                                UIMainReferences.ServerKey = FengGameManagerMKII.privateServerField;
                            }
                        }
                        GUILayout.EndHorizontal();
                        break;
                    }
                }
            }
            GUILayout.EndArea();


            if (UnityEngine.GUI.Button(GUIHelpers.AlignRect(175f, Settings.ButtonHeight, GUIHelpers.Alignment.TOPRIGHT, -5f, 5f), "Custom Characters"))
            {
                Application.LoadLevel("characterCreation");
            }

            if (UnityEngine.GUI.Button(GUIHelpers.AlignRect(175f, Settings.ButtonHeight, GUIHelpers.Alignment.TOPRIGHT, -5f, 5f + Settings.ButtonHeight + Settings.space), "Snapshot Reviewer"))
            {
                Application.LoadLevel("SnapShot");
            }
        }
    }
}