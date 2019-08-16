using GGM.Config;
using System;
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
                            GUILayout.Label(text, Styles.LabelStyle[0], GUILayout.Width(width), GUILayout.Height(height));
                            return;

                        case Alignment.CENTER:
                            GUILayout.Label(text, Styles.LabelStyle[1], GUILayout.Width(width), GUILayout.Height(height));
                            return;

                        case Alignment.RIGHT:
                            GUILayout.Label(text, Styles.LabelStyle[2], GUILayout.Width(width), GUILayout.Height(height));
                            return;
                    }

                    return;

                case LabelType.SliderStatus:
                    GUILayout.Label(text, Styles.SliderStatusStyle, GUILayout.Width(SliderValueWidth), GUILayout.Height(height));
                    return;

                case LabelType.SubHeader:
                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    GUILayout.Label(text, Styles.SubHeaderStyle, GUILayout.Width(width == LabelWidth ? SubHeaderWidth : width), GUILayout.Height(SubHeaderHeight));
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                    return;

                case LabelType.Header:
                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    GUILayout.Label(text, Styles.HeaderStyle, GUILayout.Width(width == LabelWidth ? HeaderWidth : width), GUILayout.Height(HeaderHeight));
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                    return;
            }
        }

        public static void TextArea(string text, ref string value, float width = TextFieldWidth, float height = TextFieldHeight, float labelWidth = LabelWidth, float labelHeight = LabelHeight)
        {
            GUILayoutOption[] options = { GUILayout.Width(width), GUILayout.Height(height) };

            if (text != string.Empty) GUILayout.BeginHorizontal();
            {
                if (text != string.Empty) Label(text, 0, width: labelWidth, height: labelHeight);
                value = GUILayout.TextArea(value, options);
            }
            if (text != string.Empty) GUILayout.EndHorizontal();
            GUILayout.Space(space);
        }

        public static void TextField(string text, ref string value, float width = TextFieldWidth, float height = TextFieldHeight, float labelWidth = LabelWidth, float labelHeight = LabelHeight)
        {
            GUILayoutOption[] options = { GUILayout.Width(width), GUILayout.Height(height) };

            if (text != string.Empty) GUILayout.BeginHorizontal();
            {
                if (text != string.Empty) Label(text, 0, width: labelWidth, height: labelHeight);
                value = GUILayout.TextField(value, options);
            }
            if (text != string.Empty) GUILayout.EndHorizontal();

            GUILayout.Space(space);
        }

        public static void TextField(string text, ref int value, float width = TextFieldWidth, float height = TextFieldHeight, float labelWidth = LabelWidth, float labelHeight = LabelHeight)
        {
            GUILayoutOption[] options = { GUILayout.Width(width), GUILayout.Height(height) };
            var v = value.ToString();

            if (text != string.Empty) GUILayout.BeginHorizontal();
            {
                if (text != string.Empty) Label(text, 0, width: labelWidth, height: labelHeight);
                v = GUILayout.TextField(v, options);
                value = v != string.Empty ? Convert.ToInt32(v) : 0;
            }
            if (text != string.Empty) GUILayout.EndHorizontal();
            GUILayout.Space(space);
        }

        public static void TextField(string text, ref float value, float width = TextFieldWidth, float height = TextFieldHeight, float labelWidth = LabelWidth, float labelHeight = LabelHeight)
        {
            GUILayoutOption[] options = { GUILayout.Width(width), GUILayout.Height(height) };
            var v = value.ToString();

            if (text != string.Empty) GUILayout.BeginHorizontal();
            {
                if (text != string.Empty) Label(text, 0, width: labelWidth, height: labelHeight);
                v = GUILayout.TextField(v, options);
                value = v != string.Empty ? Convert.ToSingle(v) : 0f;
            }
            if (text != string.Empty) GUILayout.EndHorizontal();
            GUILayout.Space(space);
        }

        public static void Slider(string text, ref float value, float left, float right, float sliderWidth = SliderWidth, float valueWidth = SliderValueWidth, bool customValueText = false, string valueText = "", string valueTextFormat = "0.###", bool round = false, float multiplier = 1)
        {
            GUILayoutOption[] options = { GUILayout.Width(sliderWidth) };

            GUILayout.BeginHorizontal();
            if (text != string.Empty) Label(text);
            value = GUILayout.HorizontalSlider(value, left, right, options);
            if (!customValueText)
            {
                Label(round ? Mathf.Round(value * multiplier).ToString(valueTextFormat).SetSize(11) : (value * multiplier).ToString(valueTextFormat).SetSize(11), LabelType.SliderStatus, width: valueWidth);
            }
            else
            {
                Label(valueText.SetSize(11), LabelType.SliderStatus, width: valueWidth);
            }

            GUILayout.EndHorizontal();
            GUILayout.Space(space);
        }

        public static void Slider(string text, ref float value, float left, ref float right, float sliderWidth = SliderWidth, float valueWidth = SliderValueWidth, bool customValueText = false, string valueText = "", string valueTextFormat = "0.###", bool round = false, float multiplier = 1)
        {
            GUILayoutOption[] options = { GUILayout.Width(sliderWidth) };

            GUILayout.BeginHorizontal();
            {
                if (text != string.Empty) Label(text);
                value = GUILayout.HorizontalSlider(value, left, right, options);
                if (!customValueText)
                {
                    Label(round ? Mathf.Round(value * multiplier).ToString(valueTextFormat).SetSize(11) : (value * multiplier).ToString(valueTextFormat).SetSize(11), LabelType.SliderStatus, width: valueWidth);
                }
                else
                {
                    Label(valueText.SetSize(11), LabelType.SliderStatus, width: valueWidth);
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(space);
        }

        public static void Slider(string text, ref int value, int left, int right, float sliderWidth = SliderWidth, float valueWidth = SliderValueWidth, bool customValueText = false, string valueText = "")
        {
            GUILayoutOption[] options = {GUILayout.Width(sliderWidth)};
            var l = Convert.ToSingle(left);
            var r = Convert.ToSingle(right);
            var v = Convert.ToSingle(value);

            GUILayout.BeginHorizontal();
            {
                if (text != string.Empty) Label(text);
                value = Convert.ToInt32(GUILayout.HorizontalSlider(v, l, r, options));
                Label(!customValueText ? Mathf.Round(value).ToString().SetSize(11) : valueText.SetSize(11), LabelType.SliderStatus, width: valueWidth);
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(space);
        }

        public static void Grid(string text, ref int INT, string[] str, bool sameCount = true, int count = 1, float width = ButtonWidth, float height = ButtonHeight)
        {
            GUILayoutOption[] options = { GUILayout.Width(width), GUILayout.Height(sameCount ? height : (height + 5f) * Mathf.Round(str.Length*1f / count*1f + 0.1f) - 5f) };
            if (text != string.Empty) GUILayout.BeginHorizontal();
            {
                if (text != string.Empty) Label(text);
                INT = GUILayout.SelectionGrid(INT, str, sameCount ? str.Length : count, options);
            }
            if (text != string.Empty) GUILayout.EndHorizontal();
            GUILayout.Space(space);
        }

        public static void Grid(string text, ref bool value, bool horizontal = true, float width = ButtonWidth, float height = ButtonHeight)
        {
            GUILayoutOption[] options = { GUILayout.Width(width), GUILayout.Height(horizontal ? height : height * 2f + 5f) };
            var i = value ? 1 : 0;

            if (text != string.Empty) GUILayout.BeginHorizontal();
            if (text != string.Empty) Label(text);
            i = GUILayout.SelectionGrid(i, SwitcherStr, horizontal ? 2 : 1, options);
            value = i != 0;
            if (text != string.Empty) GUILayout.EndHorizontal();
            GUILayout.Space(space);
        }

        public static void ButtonToggle(string text, string[] buttonsText, BoolSetting[] bools, bool horizontal = true, float width = ButtonWidth, float height = ButtonHeight)
        {
            GUIStyle style = new GUIStyle(UnityEngine.GUI.skin.button);
            var w = text != string.Empty ? width : leftElementWidth + rightElementWidth - 75f;
            GUILayoutOption[] options = { GUILayout.Width(horizontal ? (w - 5 * (bools.Length - 1)) / bools.Length : width), GUILayout.Height(height), GUILayout.Height(height) };
            if (horizontal) GUILayout.BeginHorizontal();
            if (text != string.Empty) Label(text);
            if (text == string.Empty) GUILayout.FlexibleSpace();

            for (var i = 0; i < bools.Length; i++)
            {
                style.normal = bools[i] ? UnityEngine.GUI.skin.button.onNormal : UnityEngine.GUI.skin.button.normal;
                style.hover = bools[i] ? UnityEngine.GUI.skin.button.onHover : UnityEngine.GUI.skin.button.hover;
                if (GUILayout.Button(buttonsText[i], style, options))
                {
                    bools[i].Value = !bools[i];
                }
            }

            if (text == string.Empty) GUILayout.FlexibleSpace();
            if (horizontal) GUILayout.EndHorizontal();
            GUILayout.Space(space);
        }

        public static void ButtonToggle(string text, string[] buttonsText, bool[] bools, bool horizontal = true, float width = ButtonWidth, float height = ButtonHeight)
        {
            var style = new GUIStyle(UnityEngine.GUI.skin.button);
            var w = text != string.Empty ? width : leftElementWidth + rightElementWidth - 75f;
            GUILayoutOption[] options = { GUILayout.Width(horizontal ? (w - 5 * (bools.Length - 1)) / bools.Length : width), GUILayout.Height(height)};

            if (horizontal) GUILayout.BeginHorizontal();
            {
                if (text != string.Empty) Label(text);
                if (text == string.Empty) GUILayout.FlexibleSpace();
                for (var i = 0; i < bools.Length; i++)
                {
                    style.normal = bools[i] ? UnityEngine.GUI.skin.button.onNormal : UnityEngine.GUI.skin.button.normal;
                    style.hover = bools[i] ? UnityEngine.GUI.skin.button.onHover : UnityEngine.GUI.skin.button.hover;
                    if (GUILayout.Button(buttonsText[i], style, options))
                    {
                        bools[i] = !bools[i];
                    }
                }

                if (text == string.Empty) GUILayout.FlexibleSpace();
            }
            if (horizontal) GUILayout.EndHorizontal();
            GUILayout.Space(space);
        }

        public static bool Button(string text, float width = ButtonWidth, float height = ButtonHeight)
        {
            GUILayoutOption[] options = {GUILayout.Width(width), GUILayout.Height(height)};

            return GUILayout.Button(text, options);
        }
    }
}