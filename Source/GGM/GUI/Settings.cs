using System.Runtime.CompilerServices;
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
        internal const float space = 5f;

        /// <summary>
        /// 0 - full,
        /// 1 - top,
        /// 2 - bottom,
        /// 3 - bombs special
        /// 4 - bottom panel
        /// </summary>
        internal static readonly Rect[] center =
        {
            new Rect(leftPos + 20f, topPos + 60f, fullAreaWidth, fullAreaHeight),
            new Rect(leftPos + 20f, topPos + 60f, fullAreaWidth, topAreaHeight),
            new Rect(leftPos + 20f, topPos + 100f, fullAreaWidth, bottomAreaHeight),
            new Rect(leftPos + 20f, topPos + height / 2.7f, fullAreaWidth, height / 1.7f),
            new Rect(leftPos + 20f, topPos + fullAreaHeight, fullAreaWidth, height - fullAreaHeight - 80)
        };

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
        internal const float HeaderWidth = leftElementWidth + rightElementWidth;
        internal const float HeaderHeight = 35f;

        internal const int SubHeaderFontSize = 16;
        internal const float SubHeaderWidth = leftElementWidth + rightElementWidth;
        internal const float SubHeaderHeight = 28f;

        internal const int LabelFontSize = 15;
        internal const float LabelWidth = leftElementWidth;
        internal const float LabelHeight = 22f;

        internal const float ButtonWidth = rightElementWidth;
        internal const float ButtonHeight = 22f;

        internal const float TextFieldWidth = rightElementWidth;
        internal const float TextFieldHeight = 22f;

        internal const float SliderWidth = rightElementWidth - 50f;
        internal const float SliderValueWidth = 31f;

        internal static readonly Color colorMajor = ColorCache.White;
        internal static readonly Color colorMinor = new Color(1f, 0.702f, 0.8f);

        internal static int headerFont = 4;
        internal static int labelFont = 1;
        internal static int buttonFont = 2;

        public enum LabelType
        {
            Label,
            SliderStatus,
            SubHeader,
            Header
        }
    }
}