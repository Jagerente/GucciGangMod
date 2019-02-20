using UnityEngine;

namespace GGM
{
    internal static class BetterGUI
    {
        private const int _headerFontSize = 20;
        private const float _headerWidth = 70f;
        private const float _headerHeight = 30f;

        private const int _subHeaderFontSize = 16;
        private const float _subHeaderWidth = 90f;
        private const float _subHeaderHeight = 25f;

        private const int _labelFontSize = 13;
        private const float _labelWidth = 135f;
        private const float _labelHeight = 25f;

        private const float _gridWidth = 190f;
        private const float _gridHeight = 20f;

        private const float _textFieldWidth = 190f;

        private const float _sliderWidth = 170f;
        private const float _sliderTextWidth = 15f;

        private const float _buttonWidth = 100f;
        private const float _buttonHeight = 50f;

        private static readonly Color _rgbColor1 = new Color(0.992f, 0.737f, 0.706f);//#FDBCB4
        private static readonly Color _rgbColor2 = new Color(0.941f, 0.502f, 0.502f);//#F08080

        private static readonly Color _mainMenuColor = new Color(0.941f, 0.502f, 0.502f);//#F08080

        public static readonly GUIStyle TextLabelMiddleLeft = TextStyle(TextAnchor.MiddleLeft, FontStyle.Normal, _labelFontSize, false, _rgbColor1, _rgbColor1, _rgbColor1);
        public static readonly GUIStyle TextLabelMiddleCenter = TextStyle(TextAnchor.MiddleCenter, FontStyle.Normal, _labelFontSize, false, _rgbColor1, _rgbColor1, _rgbColor1);
        public static readonly GUIStyle TextLabelMiddleRight = TextStyle(TextAnchor.MiddleRight, FontStyle.Normal, _labelFontSize, false, _rgbColor1, _rgbColor1, _rgbColor1);
        public static readonly GUIStyle TextSubHeader = TextStyle(TextAnchor.MiddleCenter, FontStyle.Bold, _subHeaderFontSize, false, _rgbColor1, _rgbColor1, _rgbColor1);
        public static readonly GUIStyle TextHeader = TextStyle(TextAnchor.UpperCenter, FontStyle.Bold, _headerFontSize, false, _rgbColor1, _rgbColor1, _rgbColor1);
        public static readonly GUIStyle TextStatus = TextStyle(TextAnchor.MiddleCenter, FontStyle.Bold, _labelFontSize, false, _rgbColor2, _rgbColor2, _rgbColor2);
        public static readonly GUIStyle TextButtonNormal = TextStyle(TextAnchor.MiddleCenter, FontStyle.Normal, 24, false, _rgbColor2, _rgbColor2, _rgbColor2);
        public static readonly GUIStyle TextButtonBold = TextStyle(TextAnchor.MiddleCenter, FontStyle.Bold, 24, false, _rgbColor2, _rgbColor2, _rgbColor2);

        private static GUIStyle TextStyle(TextAnchor alignment, FontStyle fontStyle, int fontSize, bool wordWrap, Color normalColor, Color hoverColor, Color activeColor)
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

        private static GUIStyle TextStyle(TextAnchor alignment, FontStyle fontStyle, int fontSize, bool wordWrap)
        {
            return new GUIStyle
            {
                alignment = alignment,
                fontStyle = fontStyle,
                fontSize = fontSize,
                wordWrap = wordWrap,
            };
        }

        public static string ButtonStyle(string input)
        {
            return string.Concat("<b><color=#FFFFFF><size=" + _labelFontSize + ">", input, "</size></color></b>");
        }
        public static string Bold(string input)
        {
            return string.Concat("<b>", input, "</b>");
        }

        #region GUI Elements
        public static void Header(string text)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(text, TextHeader, GUILayout.Width(_headerWidth), GUILayout.Height(_headerHeight));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        public static void SubHeader(string text)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(text, TextSubHeader, GUILayout.Width(_subHeaderWidth), GUILayout.Height(_subHeaderHeight));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        /// <param name="align">
        /// 0 - Middle Left
        /// 1 - Middle Center
        /// 2 - Middle Right
        /// </param>
        public static void Label(string text, int align = 0, float width = _labelWidth, float height = _labelHeight)
        {
            switch (align)
            {
                case 0:
                    GUILayout.Label(text, TextLabelMiddleLeft, GUILayout.Width(width), GUILayout.Height(height));
                    return;
                case 1:
                    GUILayout.Label(text, TextLabelMiddleCenter, GUILayout.Width(width), GUILayout.Height(height));
                    return;
                case 2:
                    GUILayout.Label(text, TextLabelMiddleRight, GUILayout.Width(width), GUILayout.Height(height));
                    return;
            }
        }

        public static bool Button(string text, bool bold, float width = _buttonWidth, float height = _buttonHeight)
        {
            return GUILayout.Button(text, bold ? TextButtonBold : TextButtonNormal, GUILayout.Width(width), GUILayout.Height(height));
        }

        public static void TextField(string text, ref string str, float width = _textFieldWidth, float labelWidth = _labelWidth)
        {
            GUILayout.BeginHorizontal();
            Label(text, 0, labelWidth);
            str = GUILayout.TextField(str, GUILayout.Width(width));
            GUILayout.EndHorizontal();
        }

        public static void Slider(string text, bool round, float multiplier, ref float value, float left, float right, string textFormat = "0", float width = _sliderWidth, float textWidth = _sliderTextWidth)
        {
            GUILayout.BeginHorizontal();
            Label(text);
            value = GUILayout.HorizontalSlider(value, left, right, GUILayout.Width(width));
            Label(round ? Mathf.Round(multiplier == 0 ? value : value * multiplier).ToString(textFormat) : (multiplier == 0 ? value : value * multiplier).ToString(textFormat), 1, textWidth);
            GUILayout.EndHorizontal();
        }

        public static void Grid(string text, ref int INT, string[] str, bool sameCount = true, int count = 0, float width = _gridWidth, float height = _gridHeight)
        {
            GUILayout.BeginHorizontal();
            Label(text);
            INT = GUILayout.SelectionGrid(INT, str, sameCount ? str.Length : count, GUILayout.Width(width), GUILayout.Height(height));
            GUILayout.EndHorizontal();
        }
        #endregion
    }
}
