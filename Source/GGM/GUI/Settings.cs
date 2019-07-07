using GGM.Caching;
using UnityEngine;

namespace GGM.GUI
{
    internal class Settings : MonoBehaviour
    {
        internal static string[] SwitcherStr = {"Off", "On"};
        internal const float width = 775f;
        internal const float height = 575f;
        internal static readonly float leftPos = Screen.width / 2f - width / 2f;
        internal static readonly float topPos = Screen.height / 2f - height / 2f;
        internal const float fullAreaWidth = width - 40f;
        internal const float fullAreaHeight = height - 80f - 25f;
        internal const float topAreaHeight = 35f;
        internal const float bottomAreaHeight = height - 115f - 25f;
        internal const float halfAreaWidth = width / 2f - 20f;
        internal const float leftElementWidth = halfAreaWidth * 0.4f;
        internal const float rightElementWidth = halfAreaWidth * 0.6f - 25f;

        /// <summary>
        /// 0 - full,
        /// 1 - top,
        /// 2 - bottom,
        /// </summary>
        protected static readonly Rect[] center =
        {
            new Rect(leftPos + 20f, topPos + 60f, fullAreaWidth, fullAreaHeight),
            new Rect(leftPos + 20f, topPos + 60f, fullAreaWidth, topAreaHeight),
            new Rect(leftPos + 20f, topPos + 100f, fullAreaWidth, bottomAreaHeight)
        };
        /// <summary>
        /// 0 - full,
        /// 1 - top,
        /// 2 - bottom,
        /// </summary>
        protected static readonly Rect[] left =
        {
            new Rect(leftPos + 20f, topPos + 60f, halfAreaWidth, fullAreaHeight),
            new Rect(leftPos + 20f, topPos + 60f, halfAreaWidth, topAreaHeight),
            new Rect(leftPos + 20f, topPos + 100f, halfAreaWidth, bottomAreaHeight)
        };
        /// <summary>
        /// 0 - full,
        /// 1 - top,
        /// 2 - bottom,
        /// </summary>
        protected static readonly Rect[] right =
        {
            new Rect(leftPos + halfAreaWidth + 40f, topPos + 60f, halfAreaWidth, fullAreaHeight),
            new Rect(leftPos + halfAreaWidth + 40f, topPos + 60f, halfAreaWidth, topAreaHeight),
            new Rect(leftPos + halfAreaWidth + 40f, topPos + 100f, halfAreaWidth, bottomAreaHeight)
        };

        protected const int HeaderFontSize = 20;
        protected const float HeaderWidth = halfAreaWidth;
        protected const float HeaderHeight = 35f;

        protected const int SubHeaderFontSize = 16;
        protected const float SubHeaderWidth = halfAreaWidth;
        protected const float SubHeaderHeight = 25f;

        protected const int LabelFontSize = 14;
        protected const float LabelWidth = leftElementWidth;
        protected const float LabelHeight = 25f;

        protected const float GridWidth = rightElementWidth;
        protected const float GridHeight = 21f;

        protected const float TextFieldWidth = rightElementWidth;

        protected const float SliderWidth = rightElementWidth - 40f;
        protected const float SliderValueWidth = 35f;

        protected const float ButtonWidth = 100f;
        protected const float ButtonHeight = 50f;

        protected static readonly Color ColorMajor = ColorCache.Melon;
        protected static readonly Color ColorMinor = ColorCache.Froly;

        internal static readonly GUIStyle[] LabelStyle =
        {
            TextStyle(TextAnchor.MiddleLeft, FontStyle.Normal, LabelFontSize, false, ColorMajor, ColorMajor, ColorMajor),
            TextStyle(TextAnchor.MiddleCenter, FontStyle.Normal, LabelFontSize, false, ColorMajor, ColorMajor, ColorMajor),
            TextStyle(TextAnchor.MiddleRight, FontStyle.Normal, LabelFontSize, false, ColorMajor, ColorMajor, ColorMajor)
        };
        protected static readonly GUIStyle HeaderStyle = TextStyle(TextAnchor.MiddleCenter, FontStyle.Bold, HeaderFontSize, false, ColorMajor, ColorMajor, ColorMajor);
        protected static readonly GUIStyle SubHeaderStyle = TextStyle(TextAnchor.MiddleCenter, FontStyle.Bold, SubHeaderFontSize, false, ColorMajor, ColorMajor, ColorMajor);
        protected static readonly GUIStyle SliderStatusStyle = TextStyle(TextAnchor.MiddleCenter, FontStyle.Bold, LabelFontSize, false, ColorMinor, ColorMinor, ColorMinor);
        protected static readonly GUIStyle ButtonStyle = TextStyle(TextAnchor.MiddleCenter, FontStyle.Normal, 24, false, ColorMinor, ColorMinor, ColorMinor);

        protected static GUIStyle TextStyle(TextAnchor alignment, FontStyle fontStyle, int fontSize, bool wordWrap, Color normalColor, Color hoverColor, Color activeColor)
        {
            return new GUIStyle
            {
                alignment = alignment,
                fontStyle = fontStyle,
                fontSize = fontSize,
                wordWrap = wordWrap,
                normal =
                {
                    textColor = normalColor
                },
                hover =
                {
                    textColor = hoverColor
                },
                active =
                {
                    textColor = activeColor
                }
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
