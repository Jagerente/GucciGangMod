using GGM.Caching;
using UnityEngine;

namespace GGM.GUI
{
    internal class Settings
    {
        internal static string[] SwitcherStr = { "Off", "On" };
        internal const float width = 775f;
        internal const float height = 575f;
        internal static readonly float leftPos = Screen.width / 2f - width / 2f;
        internal static readonly float topPos = Screen.height / 2f - height / 2f;
        internal const float fullAreaWidth = width - 40f;
        internal const float fullAreaHeight = height - 80f - 15f;
        internal const float topAreaHeight = 35f;
        internal const float bottomAreaHeight = height - 115f - 15f;
        internal const float halfAreaWidth = width / 2f - 20f;
        internal const float leftElementWidth = halfAreaWidth * 0.4f;
        internal const float rightElementWidth = halfAreaWidth * 0.6f - 30f;

        /// <summary>
        /// 0 - full,
        /// 1 - top,
        /// 2 - bottom,
        /// </summary>
        internal static readonly Rect[] center = { new Rect(leftPos + 20f, topPos + 60f, fullAreaWidth, fullAreaHeight), new Rect(leftPos + 20f, topPos + 60f, fullAreaWidth, topAreaHeight), new Rect(leftPos + 20f, topPos + 100f, fullAreaWidth, bottomAreaHeight), new Rect(leftPos + 20f, topPos + height / 2.7f, fullAreaWidth, height / 1.7f) };

        /// <summary>
        /// 0 - full,
        /// 1 - top,
        /// 2 - bottom,
        /// </summary>
        internal static readonly Rect[] left = { new Rect(leftPos + 20f, topPos + 60f, halfAreaWidth, fullAreaHeight), new Rect(leftPos + 20f, topPos + 60f, halfAreaWidth, topAreaHeight), new Rect(leftPos + 20f, topPos + 100f, halfAreaWidth, bottomAreaHeight), new Rect(leftPos + 20f, topPos + 60f, halfAreaWidth, bottomAreaHeight / 2f), };

        /// <summary>
        /// 0 - full,
        /// 1 - top,
        /// 2 - bottom,
        /// </summary>
        internal static readonly Rect[] right = { new Rect(leftPos + halfAreaWidth + 40f, topPos + 60f, halfAreaWidth, fullAreaHeight), new Rect(leftPos + halfAreaWidth + 40f, topPos + 60f, halfAreaWidth, topAreaHeight), new Rect(leftPos + halfAreaWidth + 40f, topPos + 100f, halfAreaWidth, bottomAreaHeight), new Rect(leftPos + halfAreaWidth + 40f, topPos + 60f, halfAreaWidth, bottomAreaHeight / 2f) };

        internal const int HeaderFontSize = 20;
        internal const float HeaderWidth = halfAreaWidth;
        internal const float HeaderHeight = 35f;

        internal const int SubHeaderFontSize = 16;
        internal const float SubHeaderWidth = halfAreaWidth;
        internal const float SubHeaderHeight = 30f;

        internal const int LabelFontSize = 14;
        internal const float LabelWidth = leftElementWidth;
        internal const float LabelHeight = 25f;

        internal const float ButtonWidth = rightElementWidth;
        internal const float ButtonHeight = 22f;

        internal const float TextFieldWidth = rightElementWidth;
        internal const float TextFieldHeight = 21f;

        internal const float SliderWidth = rightElementWidth - 50f;
        internal const float SliderValueWidth = 31f;

        internal static readonly Color ColorMajor = ColorCache.White;
        internal static readonly Color ColorMinor = new Color(1f, 0.702f, 0.8f);

        internal static readonly GUIStyle[] LabelStyle = { TextStyle(TextAnchor.MiddleLeft, FontStyle.Normal, LabelFontSize, false, ColorMajor, ColorMajor, ColorMajor), TextStyle(TextAnchor.MiddleCenter, FontStyle.Normal, LabelFontSize, false, ColorMajor, ColorMajor, ColorMajor), TextStyle(TextAnchor.MiddleRight, FontStyle.Normal, LabelFontSize, false, ColorMajor, ColorMajor, ColorMajor) };

        internal static readonly GUIStyle HeaderStyle = TextStyle(TextAnchor.MiddleCenter, FontStyle.Bold, HeaderFontSize, false, ColorMinor, ColorMinor, ColorMinor);
        internal static readonly GUIStyle SubHeaderStyle = TextStyle(TextAnchor.MiddleCenter, FontStyle.Bold, SubHeaderFontSize, false, ColorMinor, ColorMinor, ColorMinor);
        internal static readonly GUIStyle SliderStatusStyle = TextStyle(TextAnchor.MiddleCenter, FontStyle.Bold, LabelFontSize, false, ColorMinor, ColorMinor, ColorMinor);
        internal static readonly GUIStyle ButtonStyle = TextStyle(TextAnchor.MiddleCenter, FontStyle.Normal, 24, false, ColorMinor, ColorMinor, ColorMinor);

        internal static GUIStyle TextStyle(TextAnchor alignment, FontStyle fontStyle, int fontSize, bool wordWrap, Color normalColor, Color hoverColor, Color activeColor)
        {
            return new GUIStyle
            {
                alignment = alignment,
                fontStyle = fontStyle,
                fontSize = fontSize,
                wordWrap = wordWrap,
                normal = { textColor = normalColor },
                hover = { textColor = hoverColor },
                active = { textColor = activeColor }
            };
        }

        public enum LabelType
        {
            Label,
            SliderStatus,
            SubHeader,
            Header
        }
    }
}