using GGM.Caching;
using System.Linq;
using UnityEngine;

namespace GGM.GUI
{
    internal class Styles : MonoBehaviour
    {
        private static bool isInited;
        private const string StylePath = "GGM/";
        internal static Font[] Fonts;
        public static Texture2D Box;
        private static Texture2D Window;
        private static Texture2D ButtonN;
        private static Texture2D ButtonA;
        private static Texture2D ButtonH;
        private static Texture2D ButtonON;
        private static Texture2D ButtonOA;
        private static Texture2D ButtonOH;
        private static Texture2D TextN;
        private static Texture2D TextH;
        private static Texture2D TextF;
        private static Texture2D TextON;
        private static Texture2D ThumbN;
        private static Texture2D ThumbA;
        private static Texture2D ThumbH;
        private static Texture2D ToggleN;
        private static Texture2D ToggleA;
        private static Texture2D ToggleH;
        private static Texture2D ToggleON;
        private static Texture2D ToggleOA;
        private static Texture2D ToggleOH;
        private static Texture2D Slider;
        private static Texture2D VerticalScrollBar;
        private static Texture2D HorizontalScrollBar;
        private static Texture2D ScrollBarThumbN;
        private static Texture2D ScrollBarThumbA;
        private static Texture2D ScrollBarThumbH;
        public static Texture2D Logo;
        public static Texture2D Tutorial;
        public static Texture2D ForestDay;
        public static Texture2D ForestDawn;
        public static Texture2D ForestNight;
        public static Texture2D CityDay;
        public static Texture2D CityDawn;
        public static Texture2D CityNight;
        public static Texture2D Akina;
        public static GUIStyle TextFieldStyle;

         public static void ApplyStyle(GUIStyle style, TextAnchor anchor, FontStyle fontStyle, int fontSize, bool wordWrap)
        {
            if (style == null)
                return;
            style.alignment = anchor;
            style.fontStyle = fontStyle;
            style.fontSize = fontSize;
            style.wordWrap = wordWrap;
            style.padding = new RectOffset(1, 1, 1, 1);
            style.margin = new RectOffset(5, 5, 5, 5);
            style.border = new RectOffset(2, 2, 2, 2);
        }


        public static void ApplyStyle(GUIStyle style, TextAnchor anchor, FontStyle fstyle, int fontSize, bool wordWrap, Color color)
        {
            if (style == null)
                return;
            ApplyStyle(style, anchor, fstyle, fontSize, wordWrap, new Color[6].Select(x => color).ToArray());
        }

        public static void ApplyStyle(GUIStyle res, TextAnchor anchor, FontStyle style, int fontSize, bool wordWrap, Color[] colors)
        {
            if (res == null)
                return;
            ApplyStyle(res, anchor, style, fontSize, wordWrap);
            res.normal.textColor = colors[0];
            res.hover.textColor = colors[1];
            res.active.textColor = colors[2];
            res.onNormal.textColor = colors[3];
            res.onHover.textColor = colors[4];
            res.onActive.textColor = colors[5];
        }

        public static GUIStyle CreateStyle(TextAnchor anchor, FontStyle fontStyle, int fontSize, bool wordWrap)
        {
            GUIStyle style = new GUIStyle();
            ApplyStyle(style, anchor, fontStyle, fontSize, wordWrap);
            return style;
        }

        public static GUIStyle CreateStyle(TextAnchor anchor, FontStyle style, int fontSize, bool wordWrap, Color color)
        {
            return CreateStyle(anchor, style, fontSize, wordWrap, new Color[6].Select(x => color).ToArray());
        }

        public static GUIStyle CreateStyle(TextAnchor anchor, FontStyle style, int fontSize, bool wordWrap, Color[] colors)
        {
            GUIStyle res = CreateStyle(anchor, style, fontSize, wordWrap);
            res.normal.textColor = colors[0];
            res.hover.textColor = colors[1];
            res.active.textColor = colors[2];
            res.onNormal.textColor = colors[3];
            res.onHover.textColor = colors[4];
            res.onActive.textColor = colors[5];
            return res;
        }

        private void Start()
        {
            var tutorial = new WWW("file:///" + Application.dataPath + $"/Resources/Locations/tutorial.png");
            if (Tutorial == null && tutorial != null)
            {
                Tutorial = tutorial.texture;
                Tutorial.Apply();
            }

            var forestDay = new WWW("file:///" + Application.dataPath + $"/Resources/Locations/forest_day.png");
            if (ForestDay == null && forestDay != null)
            {
                ForestDay = forestDay.texture;
                ForestDay.Apply();
            }

            var forestDawn = new WWW("file:///" + Application.dataPath + $"/Resources/Locations/forest_dawn.png");
            if (ForestDawn == null && forestDawn != null)
            {
                ForestDawn = forestDawn.texture;
                ForestDawn.Apply();
            }

            var forestNight = new WWW("file:///" + Application.dataPath + $"/Resources/Locations/forest_night.png");
            if (ForestNight == null && forestNight != null)
            {
                ForestNight = forestNight.texture;
                ForestNight.Apply();
            }

            var cityDay = new WWW("file:///" + Application.dataPath + $"/Resources/Locations/city_day.png");
            if (CityDay == null && cityDay != null)
            {
                CityDay = cityDay.texture;
                CityDay.Apply();
            }

            var cityDawn = new WWW("file:///" + Application.dataPath + $"/Resources/Locations/city_dawn.png");
            if (CityDawn == null && cityDawn != null)
            {
                CityDawn = cityDawn.texture;
                CityDawn.Apply();
            }

            var cityNight = new WWW("file:///" + Application.dataPath + $"/Resources/Locations/city_night.png");
            if (CityNight == null && cityNight != null)
            {
                CityNight = cityNight.texture;
                CityNight.Apply();
            }

            var akina = new WWW("file:///" + Application.dataPath + $"/Resources/Locations/akina.png");
            if (Akina == null && akina != null)
            {
                Akina = akina.texture;
                Akina.Apply();
            }

            var logo = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}Logo.png");
            if (Logo == null && logo != null)
            {
                Logo = logo.texture;
                Logo.Apply();
            }

            if (Box == null)
            {
                Box = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                Box.SetPixel(0, 0, new Color(ColorCache.DarkScarlet.Value.r, ColorCache.DarkScarlet.Value.g, ColorCache.DarkScarlet.Value.b, 0.5f));
                Box.Apply();
            }

            var window = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}Window.png");
            if (Window == null && window != null)
            {
                Window = window.texture;
                Window.Apply();
            }

            var buttonN = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}ButtonN.png");
            if (ButtonN == null && buttonN != null)
            {
                ButtonN = buttonN.texture;
                ButtonN.Apply();
            }

            var buttonA = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}ButtonA.png");
            if (ButtonA == null && buttonA != null)
            {
                ButtonA = buttonA.texture;
                ButtonA.Apply();
            }

            var buttonH = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}ButtonH.png");
            if (ButtonH == null && buttonH != null)
            {
                ButtonH = buttonH.texture;
                ButtonH.Apply();
            }

            var buttonON = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}ButtonON.png");
            if (ButtonON == null && buttonON != null)
            {
                ButtonON = buttonON.texture;
                ButtonON.Apply();
            }

            var buttonOH = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}ButtonOH.png");
            if (ButtonOH == null && buttonOH != null)
            {
                ButtonOH = buttonOH.texture;
                ButtonOH.Apply();
            }

            var buttonOA = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}ButtonOA.png");
            if (ButtonOA == null && buttonOA != null)
            {
                ButtonOA = buttonOA.texture;
                ButtonOA.Apply();
            }

            var textN = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}TextN.png");
            if (TextN == null && textN != null)
            {
                TextN = textN.texture;
                TextN.Apply();
            }

            var textH = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}TextH.png");
            if (TextH == null && textH != null)
            {
                TextH = textH.texture;
                TextH.Apply();
            }

            var textF = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}TextF.png");
            if (TextF == null && textF != null)
            {
                TextF = textF.texture;
                TextF.Apply();
            }

            var textON = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}TextA.png");
            if (TextON == null && textON != null)
            {
                TextON = textON.texture;
                TextON.Apply();
            }

            var thumbN = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}ThumbN.png");
            if (ThumbN == null && thumbN != null)
            {
                ThumbN = thumbN.texture;
                ThumbN.Apply();
            }

            var thumbA = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}ThumbA.png");
            if (ThumbA == null && thumbA != null)
            {
                ThumbA = thumbA.texture;
                ThumbA.Apply();
            }

            var thumbH = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}ThumbH.png");
            if (ThumbH == null && thumbH != null)
            {
                ThumbH = thumbH.texture;
                ThumbH.Apply();
            }

            var toggleN = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}ToggleN.png");
            if (ToggleN == null && toggleN != null)
            {
                ToggleN = toggleN.texture;
                ToggleN.Apply();
            }

            var toggleA = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}ToggleA.png");
            if (ToggleA == null && toggleA != null)
            {
                ToggleA = toggleA.texture;
                ToggleA.Apply();
            }

            var toggleH = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}ToggleH.png");
            if (ToggleH == null && toggleH != null)
            {
                ToggleH = toggleH.texture;
                ToggleH.Apply();
            }

            var toggleON = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}ToggleON.png");
            if (ToggleON == null && toggleON != null)
            {
                ToggleON = toggleON.texture;
                ToggleON.Apply();
            }

            var toggleOA = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}ToggleOA.png");
            if (ToggleOA == null && toggleOA != null)
            {
                ToggleOA = toggleOA.texture;
                ToggleOA.Apply();
            }

            var toggleOH = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}ToggleOH.png");
            if (ToggleOH == null && toggleOH != null)
            {
                ToggleOH = toggleOH.texture;
                ToggleOH.Apply();
            }

            var verticalScrollBar = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}ScrollBarH.png");
            if (VerticalScrollBar == null && verticalScrollBar != null)
            {
                VerticalScrollBar = verticalScrollBar.texture;
                VerticalScrollBar.Apply();
            }

            var horizontalScrollBar = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}ScrollBarV.png");
            if (HorizontalScrollBar == null && horizontalScrollBar != null)
            {
                HorizontalScrollBar = horizontalScrollBar.texture;
                HorizontalScrollBar.Apply();
            }

            var scrollBarThumbN = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}ScrollBarThumbN.png");
            if (ScrollBarThumbN == null && scrollBarThumbN != null)
            {
                ScrollBarThumbN = scrollBarThumbN.texture;
                ScrollBarThumbN.Apply();
            }

            var scrollBarThumbA = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}ScrollBarThumbA.png");
            if (ScrollBarThumbA == null && scrollBarThumbA != null)
            {
                ScrollBarThumbA = scrollBarThumbA.texture;
                ScrollBarThumbA.Apply();
            }

            var scrollBarThumbH = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}ScrollBarThumbH.png");
            if (ScrollBarThumbH == null && scrollBarThumbH != null)
            {
                ScrollBarThumbH = scrollBarThumbH.texture;
                ScrollBarThumbH.Apply();
            }

            var slider = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}Slider.png");
            if (Slider == null && slider != null)
            {
                Slider = slider.texture;
                Slider.Apply();
            }
            TextFieldStyle = CreateStyle(TextAnchor.MiddleLeft, FontStyle.Normal, 16, false, new Color[6].Select(x => ColorCache.White.Value).ToArray());
            TextFieldStyle.normal.background = TextN;
            TextFieldStyle.hover.background = TextH;
            TextFieldStyle.active.background = TextON;
            TextFieldStyle.focused.background = TextF;
            TextFieldStyle.focused.textColor = ColorCache.White.Value;
            TextFieldStyle.clipping = TextClipping.Clip;
            TextFieldStyle.richText = false;
        }

        public static void Init()
        {
            if (isInited) return;
            //Button
            UnityEngine.GUI.skin.button.normal.textColor = Caching.ColorCache.White;
            UnityEngine.GUI.skin.button.active.textColor = Caching.ColorCache.White;
            UnityEngine.GUI.skin.button.hover.textColor = Caching.ColorCache.White;
            UnityEngine.GUI.skin.button.onNormal.textColor = Caching.ColorCache.White;
            UnityEngine.GUI.skin.button.onActive.textColor = Caching.ColorCache.White;
            UnityEngine.GUI.skin.button.onHover.textColor = Caching.ColorCache.White;
            UnityEngine.GUI.skin.button.normal.background = ButtonN;
            UnityEngine.GUI.skin.button.active.background = ButtonA;
            UnityEngine.GUI.skin.button.hover.background = ButtonH;
            UnityEngine.GUI.skin.button.onNormal.background = ButtonON;
            UnityEngine.GUI.skin.button.onActive.background = ButtonOA;
            UnityEngine.GUI.skin.button.onHover.background = ButtonOH;
            UnityEngine.GUI.skin.button.fontStyle = FontStyle.Bold;
            //TextField
            UnityEngine.GUI.skin.textField.normal.background = TextN;
            UnityEngine.GUI.skin.textField.hover.background = TextH;
            UnityEngine.GUI.skin.textField.focused.background = TextF;
            UnityEngine.GUI.skin.textField.onNormal.background = TextON;
            UnityEngine.GUI.skin.textField.focused.textColor = Caching.ColorCache.White;
            //TextArea
            UnityEngine.GUI.skin.textArea.normal.background = TextN;
            UnityEngine.GUI.skin.textArea.hover.background = TextH;
            UnityEngine.GUI.skin.textArea.focused.background = TextF;
            UnityEngine.GUI.skin.textArea.onNormal.background = TextON;
            UnityEngine.GUI.skin.textArea.focused.textColor = Caching.ColorCache.White;
            //VerticalSlider
            UnityEngine.GUI.skin.verticalSlider.normal.background = Slider;
            UnityEngine.GUI.skin.verticalSliderThumb.normal.background = ThumbN;
            UnityEngine.GUI.skin.verticalSliderThumb.active.background = ThumbA;
            UnityEngine.GUI.skin.verticalSliderThumb.hover.background = ThumbH;
            //HorizontalSlider
            UnityEngine.GUI.skin.horizontalSlider.normal.background = Slider;
            UnityEngine.GUI.skin.horizontalSliderThumb.normal.background = ThumbN;
            UnityEngine.GUI.skin.horizontalSliderThumb.active.background = ThumbA;
            UnityEngine.GUI.skin.horizontalSliderThumb.hover.background = ThumbH;
            //VerticalScrollBar
            UnityEngine.GUI.skin.verticalScrollbar.normal.background = VerticalScrollBar;
            UnityEngine.GUI.skin.verticalScrollbarThumb.normal.background = HorizontalScrollBar;
            UnityEngine.GUI.skin.verticalScrollbarThumb.active.background = HorizontalScrollBar;
            UnityEngine.GUI.skin.verticalScrollbarThumb.hover.background = HorizontalScrollBar;
            //HorizontalScrollBar
            UnityEngine.GUI.skin.horizontalScrollbar.normal.background = HorizontalScrollBar;
            UnityEngine.GUI.skin.horizontalScrollbarThumb.normal.background = HorizontalScrollBar;
            UnityEngine.GUI.skin.horizontalScrollbarThumb.active.background = HorizontalScrollBar;
            UnityEngine.GUI.skin.horizontalScrollbarThumb.hover.background = HorizontalScrollBar;
            //Toggle
            UnityEngine.GUI.skin.toggle.normal.textColor = Caching.ColorCache.White;
            UnityEngine.GUI.skin.toggle.active.textColor = Caching.ColorCache.White;
            UnityEngine.GUI.skin.toggle.hover.textColor = Caching.ColorCache.White;
            UnityEngine.GUI.skin.toggle.onNormal.textColor = Caching.ColorCache.White;
            UnityEngine.GUI.skin.toggle.onActive.textColor = Caching.ColorCache.White;
            UnityEngine.GUI.skin.toggle.onHover.textColor = Caching.ColorCache.White;
            UnityEngine.GUI.skin.toggle.normal.background = ToggleN;
            UnityEngine.GUI.skin.toggle.active.background = ToggleA;
            UnityEngine.GUI.skin.toggle.hover.background = ToggleH;
            UnityEngine.GUI.skin.toggle.onNormal.background = ToggleON;
            UnityEngine.GUI.skin.toggle.onActive.background = ToggleOA;
            UnityEngine.GUI.skin.toggle.onHover.background = ToggleOH;
            //Box
            UnityEngine.GUI.skin.box.normal.textColor = Caching.ColorCache.White;
            UnityEngine.GUI.skin.box.normal.background = Box;
            //Window
            UnityEngine.GUI.skin.window.normal.background = Window;
            UnityEngine.GUI.skin.window.active.background = Window;
            UnityEngine.GUI.skin.window.onNormal.background = Window;
            UnityEngine.GUI.skin.window.onActive.background = Window;
            isInited = true;
        }
    }
}