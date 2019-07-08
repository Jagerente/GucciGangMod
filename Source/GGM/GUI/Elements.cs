using System;
using GGM.Config;
using UnityEngine;
using static GGM.GUI.GUIHelpers;
using static GGM.GUI.Settings;

namespace GGM.GUI
{
    internal class Elements
    {
        /// <summary>
        /// GUILayout.Label
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="type">Label;Sub Header;Header;Slider Value</param>
        /// <param name="align">LEFT;CENTER,RIGHT. Label only.</param>
        /// <param name="width">Label type only.</param>
        /// <param name="height">Label & Slider Value types only.</param>
        public static void Label(string text = "", LabelType type = LabelType.Label, Alignment align = Alignment.LEFT, float width = LabelWidth, float height = LabelHeight)
        {
            switch (type)
            {
                case LabelType.Label:
                    switch (align)
                    {
                        case Alignment.LEFT:
                            GUILayout.Label(text, LabelStyle[0], GUILayout.Width(width), GUILayout.Height(height));
                            return;
                        case Alignment.CENTER:
                            GUILayout.Label(text, LabelStyle[1], GUILayout.Width(width), GUILayout.Height(height));
                            return;
                        case Alignment.RIGHT:
                            GUILayout.Label(text, LabelStyle[2], GUILayout.Width(width), GUILayout.Height(height));
                            return;
                    }
                    return;
                case LabelType.SliderStatus:
                    GUILayout.Label(text, SliderStatusStyle, GUILayout.Width(SliderValueWidth), GUILayout.Height(height));
                    return;
                case LabelType.SubHeader:
                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    GUILayout.Label(text, SubHeaderStyle, GUILayout.Width(SubHeaderWidth), GUILayout.Height(SubHeaderHeight));
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                    return;
                case LabelType.Header:
                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    GUILayout.Label(text, HeaderStyle, GUILayout.Width(width == LabelWidth ? HeaderWidth : width), GUILayout.Height(HeaderHeight));
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                    return;
            }
        }

        public static bool Button(string text, float width = ButtonWidth, float height = ButtonHeight)
        {
            return GUILayout.Button(text, ButtonStyle, GUILayout.Width(width), GUILayout.Height(height));
        }

        public static void TextArea(string text, ref string value, float width = TextFieldWidth, float labelWidth = LabelWidth)
        {
            GUILayout.BeginHorizontal();
            if (text != string.Empty) Label(text, 0, width: labelWidth);
            value = GUILayout.TextArea(value, GUILayout.Width(width));
            GUILayout.EndHorizontal();
        }

        public static void TextField(string text, ref string value, float width = TextFieldWidth, float labelWidth = LabelWidth)
        {
            GUILayout.BeginHorizontal();
            if (text != string.Empty) Label(text, 0, width: labelWidth);
            value = GUILayout.TextField(value, GUILayout.Width(width));
            GUILayout.EndHorizontal();
        }

        public static void TextField(string text, ref int value, float width = TextFieldWidth, float labelWidth = LabelWidth)
        {
            var v = value.ToString();
            GUILayout.BeginHorizontal();
            if (text != string.Empty) Label(text, 0, width: labelWidth);
            v = GUILayout.TextField(v, GUILayout.Width(width));
            value = v != string.Empty ? Convert.ToInt32(v) : 0;
            GUILayout.EndHorizontal();
        }

        public static void TextField(string text, ref float value, float width = TextFieldWidth, float labelWidth = LabelWidth)
        {
            var v = value.ToString();
            GUILayout.BeginHorizontal();
            if (text != string.Empty) Label(text, 0, width: labelWidth);
            v = GUILayout.TextField(v, GUILayout.Width(width));
            value = v != string.Empty ? Convert.ToSingle(v) : 0f;
            GUILayout.EndHorizontal();
        }

        public static void Slider(string text, ref float value, float left, float right, float sliderWidth = SliderWidth, float valueWidth = SliderValueWidth, bool customValueText = false, string valueText = "", string valueTextFormat = "0.###", bool round = false, float multiplier = 1)
        {
            GUILayout.BeginHorizontal();
            if (text != string.Empty) Label(text);
            value = GUILayout.HorizontalSlider(value, left, right, GUILayout.Width(sliderWidth));
            if (!customValueText)
            {
                Label(round ? Mathf.Round(value * multiplier).ToString(valueTextFormat) : (value * multiplier).ToString(valueTextFormat), LabelType.SliderStatus, width: valueWidth);
            }
            else
            {
                Label(valueText, LabelType.SliderStatus, width: valueWidth);
            }
            GUILayout.EndHorizontal();
        }

        public static void Slider(string text, ref float value, float left, ref float right, float sliderWidth = SliderWidth, float valueWidth = SliderValueWidth, bool customValueText = false, string valueText = "", string valueTextFormat = "0.###", bool round = false, float multiplier = 1)
        {
            GUILayout.BeginHorizontal();
            if (text != string.Empty) Label(text);
            value = GUILayout.HorizontalSlider(value, left, right, GUILayout.Width(sliderWidth));
            if (!customValueText)
            {
                Label(round ? Mathf.Round(value * multiplier).ToString(valueTextFormat) : (value * multiplier).ToString(valueTextFormat), LabelType.SliderStatus, width: valueWidth);
            }
            else
            {   
                Label(valueText, LabelType.SliderStatus, width: valueWidth);
            }
            GUILayout.EndHorizontal();
        }

        public static void Slider(string text, ref int value, int left, int right, float sliderWidth = SliderWidth, float valueWidth = SliderValueWidth, bool customValueText = false, string valueText = "")
        {
            var l = Convert.ToSingle(left);
            var r = Convert.ToSingle(right);
            var v = Convert.ToSingle(value);
            GUILayout.BeginHorizontal();
            if (text != string.Empty) Label(text);
            value = Convert.ToInt32(GUILayout.HorizontalSlider(v, l, r, GUILayout.Width(sliderWidth)));
            Label(!customValueText ? Mathf.Round(value).ToString() : valueText, LabelType.SliderStatus, width: valueWidth);
            GUILayout.EndHorizontal();
        }

        public static void Grid(string text, ref int INT, string[] str, bool sameCount = true, int count = 0, float width = GridWidth, float height = GridHeight)
        {
            GUILayout.BeginHorizontal();
            if (text != string.Empty) Label(text);
            INT = GUILayout.SelectionGrid(INT, str, sameCount ? str.Length : count, GUILayout.Width(width), GUILayout.Height(height));
            GUILayout.EndHorizontal();
        }

        public static void Grid(string text, ref bool value, bool horizontal = true, float width = GridWidth, float height = GridHeight)
        {
            var i = value ? 1 : 0;
            GUILayout.BeginHorizontal();
            if (text != string.Empty) Label(text);
            i = GUILayout.SelectionGrid(i, SwitcherStr, horizontal ? 2 : 1, GUILayout.Width(width), GUILayout.Height(height));
            value = i != 0;
            GUILayout.EndHorizontal();
        }

        public static void ButtonToggle(string text, string[] buttonsText, BoolSetting[] bools, float width = GridWidth)
        {
            var style = new GUIStyle(UnityEngine.GUI.skin.button);
            var w = text != string.Empty ? width : leftElementWidth + rightElementWidth - 75f;
            GUILayout.BeginHorizontal();
            if (text != string.Empty) Label(text);
            if (text == string.Empty) GUILayout.FlexibleSpace();
            for (var i = 0; i < bools.Length; i++)
            {
                style.normal = bools[i] ? UnityEngine.GUI.skin.button.onNormal : UnityEngine.GUI.skin.button.normal;
                style.hover = bools[i] ? UnityEngine.GUI.skin.button.onHover : UnityEngine.GUI.skin.button.hover;
                if (GUILayout.Button(buttonsText[i], style, GUILayout.Width((w - 5 * (bools.Length - 1)) / bools.Length)))
                {
                    bools[i].Value = !bools[i];
                }
            }
            if (text == string.Empty) GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}
