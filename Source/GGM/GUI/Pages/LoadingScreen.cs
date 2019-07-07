using GGM.Caching;
using UnityEngine;

namespace GGM.GUI.Pages
{
    internal class LoadingScreen : MonoBehaviour
    {
        private void OnGUI()
        {
            UnityEngine.GUI.backgroundColor = ColorCache.Black;
            UnityEngine.GUI.Box(new Rect(0, 0, Screen.width, Screen.height), string.Empty);
            UnityEngine.GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), ColorCache.Textures[ColorCache.Black]);
            UnityEngine.GUI.DrawTexture(GUIHelpers.AlignRect(192, 192, GUIHelpers.Alignment.CENTER), Styles.Logo);
            UnityEngine.GUI.Label(GUIHelpers.AlignRect(600, 150, GUIHelpers.Alignment.BOTTOMCENTER), "<size=64>GucciGangMod</size>\n" + "<size=32>Loading</size>", GGM.GUI.Settings.LabelStyle[1]);
        }
    }
}
