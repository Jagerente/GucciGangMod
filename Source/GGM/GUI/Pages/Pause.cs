using System.Reflection.Emit;
using GGM.Caching;
using UnityEngine;
using static FengGameManagerMKII;
using static GGM.GUI.Elements;

namespace GGM.GUI.Pages
{
    public class Pause : Page
    {
        private static readonly Rect Box = GUIHelpers.AlignRect(200, 100, GUIHelpers.Alignment.CENTER);

        private void OnGUI()
        {
            if (Time.timeScale <= 0.1f)
            {
                UnityEngine.GUI.Box(Box, ColorCache.Textures[ColorCache.PurpleMunsell]);
                GUILayout.BeginArea(Box);
                {
                    if (FGM.pauseWaitTime <= 3f)
                    {
                        GUILayout.FlexibleSpace();
                        Label("Unpausing in:", Settings.LabelType.Header, width: 200f);
                        Label(FGM.pauseWaitTime.ToString("F1"), Settings.LabelType.SubHeader, width: 200f);
                        GUILayout.FlexibleSpace();
                    }
                    else
                    {
                        GUILayout.FlexibleSpace();
                        Label("Game Paused", Settings.LabelType.Header, width: 200f);
                        GUILayout.FlexibleSpace();
                    }
                }
                GUILayout.EndArea();
            }

            if (FGM.pauseWaitTime == 0f) GetInstance<Pause>().Disable();
        }
    }
}