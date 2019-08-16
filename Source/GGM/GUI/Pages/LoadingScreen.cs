using GGM.Caching;
using UnityEngine;

namespace GGM.GUI.Pages
{
    internal class LoadingScreen : Page
    {
        private void OnGUI()
        {
            UnityEngine.GUI.backgroundColor = ColorCache.Black;
            UnityEngine.GUI.Box(new Rect(0, 0, Screen.width, Screen.height), string.Empty);
            UnityEngine.GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), ColorCache.Textures[ColorCache.Black]);
            UnityEngine.GUI.DrawTexture(GUIHelpers.AlignRect(192, 192, GUIHelpers.Alignment.CENTER), Styles.Logo);
            UnityEngine.GUI.Label(GUIHelpers.AlignRect(600, 150, GUIHelpers.Alignment.BOTTOMCENTER), "GucciGangMod\n" + "Loading".SetSize(32), Styles.TextStyle(TextAnchor.MiddleCenter, FontStyle.Bold, 64, false, ColorCache.White, ColorCache.White, ColorCache.White));
        }
    }
}