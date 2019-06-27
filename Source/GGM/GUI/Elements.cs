using UnityEngine;
using static GGM.GUI.GUIHelpers;

namespace GGM.GUI
{
    internal class Elements : Settings
    {
        /// <summary>
        /// GUILayout.Label
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="type">Label;Sub Header;Header;Slider Value</param>
        /// <param name="align">LEFT;CENTER,RIGHT. Label only.</param>
        /// <param name="width">Label type only.</param>
        /// <param name="height">Label & Slider Value types only.</param>
        public static void Label(string text, LabelType type = LabelType.Label, Alignment align = Alignment.LEFT, float width = LabelWidth, float height = LabelHeight)
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
                    GUILayout.Label(text, HeaderStyle, GUILayout.Width(HeaderWidth), GUILayout.Height(HeaderHeight));
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                    return;
            }
        }

        public static bool Button(string text, float width = ButtonWidth, float height = ButtonHeight)
        {
            return GUILayout.Button(text, ButtonStyle, GUILayout.Width(width), GUILayout.Height(height));
        }

        public static void TextField(string text, ref string value, float width = TextFieldWidth, float labelWidth = LabelWidth)
        {
            GUILayout.BeginHorizontal();
            Label(text, 0, width: labelWidth);
            value = GUILayout.TextField(value, GUILayout.Width(width));
            GUILayout.EndHorizontal();
        }

        public static void Slider(string text, ref float value, float left, float right, float sliderWidth = SliderWidth, float valueWidth = SliderValueWidth, bool customValueText = false, string valueText = "", string valueTextFormat = ".###", bool round = false, float multiplier = 1)
        {
            GUILayout.BeginHorizontal();
            Label(text);
            value = GUILayout.HorizontalSlider(value, left, right, GUILayout.Width(sliderWidth));
            if (!customValueText)
                Label(round ? Mathf.Round(value * multiplier).ToString(valueTextFormat) : (value * multiplier).ToString(valueTextFormat), LabelType.SliderStatus, width: valueWidth);
            else
            {
                Label(valueText, LabelType.SliderStatus, width: valueWidth);
            }
            GUILayout.EndHorizontal();
        }

        public static void Grid(string text, ref int INT, string[] str, bool sameCount = true, int count = 0, float width = GridWidth, float height = GridHeight)
        {
            GUILayout.BeginHorizontal();
            Label(text);
            INT = GUILayout.SelectionGrid(INT, str, sameCount ? str.Length : count, GUILayout.Width(width), GUILayout.Height(height));
            GUILayout.EndHorizontal();
        }
    }
}
