using System;
using UnityEngine;

namespace GGM
{
    class BetterGUI
    {
        const string HeaderFontSize = "20";
        const float HeaderWidth = 70f;
        const float HeaderHeight = 30f;

        const string SubHeaderFontSize = "16";
        const float SubHeaderWidth = 90f;
        const float SubHeaderHeight = 25f;

        const string LabelFontSize = "13";
        const float LabelWidth = 135f;
        const float LabelHeight = 25f;

        const float GridWidth = 190f;
        const float GridHeight = 20f;

        const float TextFieldWidth = 190f;

        const float SliderWidth = 170f;
        const float SliderTextWidth = 15f;

        const float ButtonWidth = 100f;
        const float ButtonHeight = 50f;

        public static string FontColor_1 = "FDBCB4";
        public static Color RGBColor_1 = new Color(0.992f, 0.737f, 0.706f);//#FDBCB4
        public static string FontColor_2 = "F08080";
        public static Color RGBColor_2 = new Color(0.941f, 0.502f, 0.502f);//#F08080

        public static Color MainMenuColor = new Color(0.941f, 0.502f, 0.502f);//#F08080

        public static GUIStyle main_menu = 
            TextStyle(TextAnchor.MiddleRight, FontStyle.Normal, 32, false, MainMenuColor, MainMenuColor, MainMenuColor);
        public static GUIStyle main_menu_top = 
            TextStyle(TextAnchor.MiddleCenter, FontStyle.Normal, 18, false, Color.white, Color.white, Color.white);
        static GUIStyle text_labelmiddleleft = 
            TextStyle(TextAnchor.MiddleLeft, FontStyle.Normal, Convert.ToInt32(LabelFontSize), false, RGBColor_1, RGBColor_1, RGBColor_1);
        public static GUIStyle text_labelmiddlecenter = 
            TextStyle(TextAnchor.MiddleCenter, FontStyle.Normal, Convert.ToInt32(LabelFontSize), false, RGBColor_1, RGBColor_1, RGBColor_1);
        static GUIStyle text_labelmiddleright = 
            TextStyle(TextAnchor.MiddleRight, FontStyle.Normal, Convert.ToInt32(LabelFontSize), false, RGBColor_1, RGBColor_1, RGBColor_1);
        static GUIStyle text_subheader = 
            TextStyle(TextAnchor.MiddleCenter, FontStyle.Bold, Convert.ToInt32(SubHeaderFontSize), false, RGBColor_1, RGBColor_1, RGBColor_1);
        static GUIStyle text_header = 
            TextStyle(TextAnchor.UpperCenter, FontStyle.Bold, Convert.ToInt32(HeaderFontSize), false, RGBColor_1, RGBColor_1, RGBColor_1);
        static GUIStyle text_status = 
            TextStyle(TextAnchor.MiddleCenter, FontStyle.Bold, Convert.ToInt32(LabelFontSize), false, RGBColor_2, RGBColor_2, RGBColor_2);
        static GUIStyle text_button_normal = 
            TextStyle(TextAnchor.MiddleCenter, FontStyle.Normal, 24, false, RGBColor_2, RGBColor_2, RGBColor_2);
        static GUIStyle text_button_bold = 
            TextStyle(TextAnchor.MiddleCenter, FontStyle.Bold, 24, false, RGBColor_2, RGBColor_2, RGBColor_2);
        static GUIStyle menulog = 
            TextStyle(TextAnchor.LowerLeft, FontStyle.BoldAndItalic, 15, false, RGBColor_2, RGBColor_2, RGBColor_2);

        static GUIStyle TextStyle(TextAnchor alignment, FontStyle fontStyle, int fontSize, bool wordWrap, Color normalColor, Color hoverColor, Color activeColor)
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

        static GUIStyle TextStyle(TextAnchor alignment, FontStyle fontStyle, int fontSize, bool wordWrap)
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
            return string.Concat("<b><color=#FFFFFF><size=" + LabelFontSize + ">", input, "</size></color></b>");
        }
        public static string Bold(string input)
        {
            return string.Concat("<b>", input, "</b>");
        }

        //GUI Elements
        public static void Header(string text)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(text, text_header, GUILayout.Width(HeaderWidth), GUILayout.Height(HeaderHeight));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            return;
        }

        public static void SubHeader(string text)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(text, text_subheader, GUILayout.Width(SubHeaderWidth), GUILayout.Height(SubHeaderHeight));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            return;
        }

        /// <param name="align">
        /// 0 - Middle Left
        /// 1 - Middle Center
        /// 2 - Middle Right
        /// </param>
        public static void Label(string text, int align = 0, float width = LabelWidth, float height = LabelHeight)
        {
            switch (align)
            {
                case 0:
                    GUILayout.Label(text, text_labelmiddleleft, GUILayout.Width(width), GUILayout.Height(height));
                    return;
                case 1:
                    GUILayout.Label(text, text_labelmiddlecenter, GUILayout.Width(width), GUILayout.Height(height));
                    return;
                case 2:
                    GUILayout.Label(text, text_labelmiddleright, GUILayout.Width(width), GUILayout.Height(height));
                    return;
            }
        }

        public static bool Button(string text, bool bold, float width = ButtonWidth, float height = ButtonHeight)
        {
            return GUILayout.Button(text, bold ? text_button_bold : text_button_normal, GUILayout.Width(width), GUILayout.Height(height));
        }

        public static void TextField(string text, ref string STR, float width = TextFieldWidth, float lwidth = LabelWidth)
        {
            GUILayout.BeginHorizontal();
            Label(text, 0, lwidth);
            STR = GUILayout.TextField(STR, GUILayout.Width(width));
            GUILayout.EndHorizontal();
            return;
        }

        public static void Slider(string text, bool round, float multiplier, ref float value, float left, float right, string textformat = "0", float width = SliderWidth, float twidth = SliderTextWidth)
        {
            GUILayout.BeginHorizontal();
            Label(text);
            value = GUILayout.HorizontalSlider(value, left, right, GUILayout.Width(width));
            Label(round ? Mathf.Round(multiplier == 0 ? value : value * multiplier).ToString(textformat) : (multiplier == 0 ? value : value * multiplier).ToString(textformat), 1, twidth);
            GUILayout.EndHorizontal();
            return;
        }

        public static void Grid(string text, ref int INT, string[] STR, bool samecount = true, int count = 0, float width = GridWidth, float height = GridHeight)
        {
            GUILayout.BeginHorizontal();
            Label(text, 0);
            INT = GUILayout.SelectionGrid(INT, STR, samecount ? STR.Length : count, GUILayout.Width(width), GUILayout.Height(height));
            GUILayout.EndHorizontal();
            return;
        }
    }
}
