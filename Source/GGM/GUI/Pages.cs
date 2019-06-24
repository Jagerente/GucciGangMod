using UnityEngine;
using static GGM.Config.Settings;

namespace GGM.GUI
{
    class Pages : Elements
    {
        public static void LoadingScreen()
        {
            UnityEngine.GUI.backgroundColor = new Color(0f, 0f, 0f, 1f);
            UnityEngine.GUI.Box(new Rect(0, 0, Screen.width, Screen.height), string.Empty);
            UnityEngine.GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), FengGameManagerMKII.FGM.textureBackgroundBlack);
            UnityEngine.GUI.DrawTexture(GUIHelpers.AlignRect(192, 192, GUIHelpers.Alignment.CENTER), Styles.Logo);
            UnityEngine.GUI.Label(GUIHelpers.AlignRect(600, 150, GUIHelpers.Alignment.BOTTOMCENTER), "<size=64>GucciGangMod</size>\n" + "<size=32>Loading</size>", LabelStyle[1]);
        }
    }
}