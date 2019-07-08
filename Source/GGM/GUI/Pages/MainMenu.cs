using GGM.Caching;
using UnityEngine;

namespace GGM.GUI.Pages
{
    internal class MainMenu : Page
    {
        #region Settings
        private static int loginSwitchInt;

        private const string Size = "72";

        private const string Color1 = "D6B1DE";

        private const string Color2 = "FFFFFF";

        private static string singleButton = string.Empty;
        private static Rect single = GUIHelpers.AlignRect(375f, 100f, GUIHelpers.Alignment.BOTTOMCENTER, 0f, -300f);

        private static string multiplayerButton = string.Empty;
        private static Rect multiplayer = GUIHelpers.AlignRect(715f, 100f, GUIHelpers.Alignment.BOTTOMCENTER, 0f, -175f);

        private static string quitButton = string.Empty;
        private static Rect quit = GUIHelpers.AlignRect(245f, 100f, GUIHelpers.Alignment.BOTTOMCENTER, 0f, -50f);
        #endregion

        private void OnGUI()
        {
            #region Single

            if (UnityEngine.GUI.Button(single, singleButton, "label"))
            {
                NGUITools.SetActive(GameObjectCache.Find("UIRefer").GetComponent<UIMainReferences>().panelSingleSet,
                    false);
                NGUITools.SetActive(UIMainReferences.instance.panelMain.gameObject, false);
                GetInstance<Single>().Enable();
            }

            singleButton = single.Contains(GUIHelpers.mousePos)
                ? $"<color=#{Color1}><size={Size}><b><i>S I N G L E</i></b></size></color>"
                : $"<color=#{Color2}><size={Size}><b><i>S I N G L E</i></b></size></color>";

            #endregion

            #region Multiplayer

            if (UnityEngine.GUI.Button(multiplayer, multiplayerButton, "label"))
            {
                NGUITools.SetActive(GameObjectCache.Find("UIRefer").GetComponent<UIMainReferences>().panelMultiStart,
                    true);
                NGUITools.SetActive(UIMainReferences.instance.panelMain.gameObject, false);
            }

            multiplayerButton = multiplayer.Contains(GUIHelpers.mousePos)
                ? $"<color=#{Color1}><size={Size}><b><i>M U L T I P L A Y E R</i></b></size></color>"
                : $"<color=#{Color2}><size={Size}><b><i>M U L T I P L A Y E R</i></b></size></color>";

            #endregion

            #region Quit

            if (UnityEngine.GUI.Button(quit, quitButton, "label"))
            {
                Application.Quit();
            }

            quitButton = quit.Contains(GUIHelpers.mousePos)
                ? $"<color=#{Color1}><size={Size}><b><i>Q U I T</i></b></size></color>"
                : $"<color=#{Color2}><size={Size}><b><i>Q U I T</i></b></size></color>";

            #endregion

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
            if (UnityEngine.GUI.Button(GUIHelpers.AlignRect(128f, 25f, GUIHelpers.Alignment.TOPRIGHT, -5f, 45f),
                "Custom Characters")) //45f, 128,25f
            {
                Application.LoadLevel("characterCreation");
            }
            else if (UnityEngine.GUI.Button(GUIHelpers.AlignRect(128f, 25f, GUIHelpers.Alignment.TOPRIGHT, -5f, 75f),
                "Snapshot Reviewer")) //75
            {
                Application.LoadLevel("SnapShot");
            }

            #endregion
        }
    }
}
