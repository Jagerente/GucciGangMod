using System.Reflection.Emit;
using UnityEngine;

namespace GGM.GUI
{
    class Styles : MonoBehaviour
    {
        private static bool Inited;
        private static string StylePath = "Gucci/";
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

        private void Start()
        {
            var logo = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}Logo.png");
            if (Logo == null && logo != null)
            {
                Logo = logo.texture;
                Logo.Apply();
            }
            var box = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}Box.png");
            if (Box == null && box != null)
            {
                Box = box.texture;
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
            var textON = new WWW("file:///" + Application.dataPath + $"/Styles/{StylePath}TextON.png");
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
        }

        public static void Init()
        {
            if (Inited) return;
            UnityEngine.GUI.skin.button.normal.textColor = Color.white;
            UnityEngine.GUI.skin.button.active.textColor = Color.white;
            UnityEngine.GUI.skin.button.hover.textColor = Color.white;
            UnityEngine.GUI.skin.button.onNormal.textColor = Color.white;
            UnityEngine.GUI.skin.button.onActive.textColor = Color.white;
            UnityEngine.GUI.skin.button.onHover.textColor = Color.white;
            UnityEngine.GUI.skin.button.normal.background = ButtonN;
            UnityEngine.GUI.skin.button.active.background = ButtonA;
            UnityEngine.GUI.skin.button.hover.background = ButtonH;
            UnityEngine.GUI.skin.button.onNormal.background = ButtonON;
            UnityEngine.GUI.skin.button.onActive.background = ButtonOA;
            UnityEngine.GUI.skin.button.onHover.background = ButtonOH;
            UnityEngine.GUI.skin.button.fontStyle = FontStyle.Bold;


            UnityEngine.GUI.skin.textField.normal.background = TextN;
            UnityEngine.GUI.skin.textField.hover.background = TextH;
            UnityEngine.GUI.skin.textField.focused.background = TextF;
            UnityEngine.GUI.skin.textField.onNormal.background = TextON;

            UnityEngine.GUI.skin.textArea.normal.background = TextN;
            UnityEngine.GUI.skin.textArea.hover.background = TextH;
            UnityEngine.GUI.skin.textArea.focused.background = TextF;
            UnityEngine.GUI.skin.textArea.onNormal.background = TextON;
            UnityEngine.GUI.skin.textArea.focused.textColor = Color.white;

            UnityEngine.GUI.skin.verticalSlider.normal.background = Slider;
            UnityEngine.GUI.skin.verticalSliderThumb.normal.background = ThumbN;
            UnityEngine.GUI.skin.verticalSliderThumb.active.background = ThumbA;
            UnityEngine.GUI.skin.verticalSliderThumb.hover.background = ThumbH;

            UnityEngine.GUI.skin.horizontalSlider.normal.background = Slider;

            UnityEngine.GUI.skin.horizontalSliderThumb.normal.background = ThumbN;
            UnityEngine.GUI.skin.horizontalSliderThumb.active.background = ThumbA;
            UnityEngine.GUI.skin.horizontalSliderThumb.hover.background = ThumbH;

            UnityEngine.GUI.skin.horizontalScrollbar.normal.background = HorizontalScrollBar;
            UnityEngine.GUI.skin.verticalScrollbar.normal.background = VerticalScrollBar;

            //UnityEngine.GUI.skin.verticalScrollbarThumb.normal.background = ScrollBarThumbN;
            //UnityEngine.GUI.skin.verticalScrollbarThumb.active.background = ScrollBarThumbA;
            //UnityEngine.GUI.skin.verticalScrollbarThumb.hover.background = ScrollBarThumbH;
            //UnityEngine.GUI.skin.horizontalScrollbarThumb.normal.background = ScrollBarThumbN;
            //UnityEngine.GUI.skin.horizontalScrollbarThumb.active.background = ScrollBarThumbA;
            //UnityEngine.GUI.skin.horizontalScrollbarThumb.hover.background = ScrollBarThumbH;

            UnityEngine.GUI.skin.verticalScrollbarThumb.normal.background = HorizontalScrollBar;
            UnityEngine.GUI.skin.verticalScrollbarThumb.active.background = HorizontalScrollBar;
            UnityEngine.GUI.skin.verticalScrollbarThumb.hover.background = HorizontalScrollBar;
            UnityEngine.GUI.skin.horizontalScrollbarThumb.normal.background = HorizontalScrollBar;
            UnityEngine.GUI.skin.horizontalScrollbarThumb.active.background = HorizontalScrollBar;
            UnityEngine.GUI.skin.horizontalScrollbarThumb.hover.background = HorizontalScrollBar;

            //UnityEngine.GUI.skin.horizontalScrollbarLeftButton.normal.background = Slider;
            //UnityEngine.GUI.skin.horizontalScrollbarLeftButton.active.background = Slider;
            //UnityEngine.GUI.skin.horizontalScrollbarLeftButton.hover.background = Slider;
            //UnityEngine.GUI.skin.horizontalScrollbarRightButton.normal.background = Slider;
            //UnityEngine.GUI.skin.horizontalScrollbarRightButton.active.background = Slider;
            //UnityEngine.GUI.skin.horizontalScrollbarRightButton.hover.background = Slider;

            UnityEngine.GUI.skin.toggle.normal.textColor = Color.white;
            UnityEngine.GUI.skin.toggle.active.textColor = Color.white;
            UnityEngine.GUI.skin.toggle.hover.textColor = Color.white;
            UnityEngine.GUI.skin.toggle.onNormal.textColor = Color.white;
            UnityEngine.GUI.skin.toggle.onActive.textColor = Color.white;
            UnityEngine.GUI.skin.toggle.onHover.textColor = Color.white;
            UnityEngine.GUI.skin.toggle.normal.background = ToggleN;
            UnityEngine.GUI.skin.toggle.active.background = ToggleA;
            UnityEngine.GUI.skin.toggle.hover.background = ToggleH;
            UnityEngine.GUI.skin.toggle.onNormal.background = ToggleON;
            UnityEngine.GUI.skin.toggle.onActive.background = ToggleOA;
            UnityEngine.GUI.skin.toggle.onHover.background = ToggleOH;

            UnityEngine.GUI.skin.box.normal.textColor = Color.white;
            UnityEngine.GUI.skin.box.normal.background = Box;

            UnityEngine.GUI.skin.window.normal.background = Window;
            UnityEngine.GUI.skin.window.active.background = Window;
            UnityEngine.GUI.skin.window.onNormal.background = Window;
            UnityEngine.GUI.skin.window.onActive.background = Window;
            Inited = true;
        }

    }
}
