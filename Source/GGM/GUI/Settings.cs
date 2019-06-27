using GGM.Caching;
using UnityEngine;

namespace GGM.GUI
{
    internal class Settings
    {
        protected static readonly string[] Switcher = { "Off", "On" };

        internal static readonly float leftPos = Screen.width / 2f - 350f;
        internal static readonly float topPos = Screen.height / 2f - 250f;
        private const float width = 730f;
        private const float height = 550f;
        /// <summary>
        /// 0 - full,
        /// 1 - top,
        /// 2 - bottom,
        /// </summary>
        protected static readonly Rect[] center =
        {
            new Rect(leftPos + 20f, topPos + 35f, width - 20f, 445f),
            new Rect(leftPos + 20f, topPos + 35f, width - 20f, 35f),
            new Rect(leftPos + 20f, topPos + 70f, width - 20f, 410f)
        };
        /// <summary>
        /// 0 - full,
        /// 1 - top,
        /// 2 - bottom,
        /// </summary>
        protected static readonly Rect[] left =
        {
            new Rect(leftPos + 20f, topPos + 35f, (width - 20f) / 2, 445f),
            new Rect(leftPos + 20f, topPos + 35f, (width - 20f) / 2, 35f),
            new Rect(leftPos + 20f, topPos + 70f, (width - 20f) / 2, 410f)
        };
        /// <summary>
        /// 0 - full,
        /// 1 - top,
        /// 2 - bottom,
        /// </summary>
        protected static readonly Rect[] right =
        {
            new Rect(leftPos + 380f, topPos + 35f, (width - 20f) / 2, 445f),
            new Rect(leftPos + 380f, topPos + 35f, (width - 20f) / 2, 35f),
            new Rect(leftPos + 380f, topPos + 70f, (width - 20f) / 2, 410f)
        };

        protected const int HeaderFontSize = 20;
        protected const float HeaderWidth = 70f;
        protected const float HeaderHeight = 30f;

        protected const int SubHeaderFontSize = 16;
        protected const float SubHeaderWidth = 90f;
        protected const float SubHeaderHeight = 25f;

        protected const int LabelFontSize = 13;
        protected const float LabelWidth = 135f;
        protected const float LabelHeight = 25f;

        protected const float GridWidth = 190f;
        protected const float GridHeight = 20f;

        protected const float TextFieldWidth = 190f;

        protected const float SliderWidth = 170f;
        protected const float SliderValueWidth = 15f;

        protected const float ButtonWidth = 100f;
        protected const float ButtonHeight = 50f;

        protected static readonly Color ColorMajor = Colors.melon;
        protected static readonly Color ColorMinor = Colors.froly;

        protected static readonly GUIStyle[] LabelStyle =
        {
            TextStyle(TextAnchor.MiddleLeft, FontStyle.Normal, LabelFontSize, false, ColorMajor, ColorMajor, ColorMajor),
            TextStyle(TextAnchor.MiddleCenter, FontStyle.Normal, LabelFontSize, false, ColorMajor, ColorMajor, ColorMajor),
            TextStyle(TextAnchor.MiddleRight, FontStyle.Normal, LabelFontSize, false, ColorMajor, ColorMajor, ColorMajor)
        };
        protected static readonly GUIStyle HeaderStyle = TextStyle(TextAnchor.UpperCenter, FontStyle.Bold, HeaderFontSize, false, ColorMajor, ColorMajor, ColorMajor);
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
