using GGM.Caching;
using UnityEngine;

namespace GGM.GUI.Pages
{
    internal class MainMenu : Page
    {
        private static int loginSwitchInt;
        private static Rect panel = GUIHelpers.AlignRect(250f, 190f, GUIHelpers.Alignment.BOTTOMLEFT, 5, -5f);

        private void OnGUI()
        {
            GUILayout.BeginArea(panel);
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

            #region Top Left Navigation Panel

            GUILayout.BeginArea(GUIHelpers.AlignRect(400, 400, GUIHelpers.Alignment.TOPLEFT, 10, 10));
            GUILayout.BeginHorizontal();

            loginSwitchInt = GUILayout.SelectionGrid(loginSwitchInt, new[] { "<size=18>User</size>", "<size=18>Servers</size>" }, 2, GUILayout.Width(400), GUILayout.Height(50));
            GUILayout.EndHorizontal();

            switch (loginSwitchInt)
            {
                case 0:

                    #region Custom Name

                    GUILayout.BeginHorizontal();
                    GUILayout.Label("<size=16>Name:</size>", GUILayout.Width(115));
                    FengGameManagerMKII.nameField = GUILayout.TextField(FengGameManagerMKII.nameField, Styles.TextFieldStyle, GUILayout.Width(180));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("<size=16>Guild:</size>", GUILayout.Width(115));
                    LoginFengKAI.player.guildname = GUILayout.TextArea(LoginFengKAI.player.guildname, Styles.TextFieldStyle, GUILayout.Width(180));
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

                #endregion Custom Name

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

                    #endregion Server Type
            }

            GUILayout.EndArea();

            #endregion Top Left Navigation Panel

            #region Top Right Navigation Panel

            if (UnityEngine.GUI.Button(GUIHelpers.AlignRect(135f, 25f, GUIHelpers.Alignment.TOPRIGHT, -5f, 5f), "Custom Characters")) //45f, 128,25f
            {
                Application.LoadLevel("characterCreation");
            }
            else if (UnityEngine.GUI.Button(GUIHelpers.AlignRect(135f, 25f, GUIHelpers.Alignment.TOPRIGHT, -5f, 35f), "Snapshot Reviewer")) //75
            {
                Application.LoadLevel("SnapShot");
            }

            #endregion Top Right Navigation Panel
        }
    }
}